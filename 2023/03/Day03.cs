using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;

class Day02{

    static public List<Number> Numbers = new List<Number>();
    static public List<Symbol> Symbols = new List<Symbol>();

    static List<string> ReadFile(){
        List<string> lines = new List<string>();

        try{
            if (File.Exists("input.txt"))
                lines.AddRange(File.ReadAllLines("input.txt"));
            else
                Console.WriteLine("The file does not exist.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        return lines;
    }

    static void FillLists(List<string> input){
        string syms = "/*#+$-%=&@";
        
        //Fill Numbers and Symbols
        for (int i = 0; i < input.Count(); i++){
            string[] numPerLine = input[i].Split('.');
            int lineIndex = 0;

            for (int j = 0; j < numPerLine.Length; j++){
                //empty
                if (numPerLine[j] == ""){
                    lineIndex++;
                    continue;
                }

                bool wasAdded = false;
                for(int k = 0; k < syms.Length; k++){
                    //Check if string is solo symbol
                    if (numPerLine[j] == syms[k].ToString()){
                        Symbols.Add(new Symbol(syms[k].ToString(), lineIndex, i));
                        lineIndex += 2;
                        wasAdded = true;
                        break;
                    }
                    //Check if string is symbol and num like -456, 472$293 or 234@
                    else if (numPerLine[j].Contains(syms[k])){
                        //-456
                        if (numPerLine[j][0] == syms[k]){
                            Symbols.Add(new Symbol(syms[k].ToString(), lineIndex, i));
                            lineIndex++;
                            string nNum = numPerLine[j].Remove(0, 1);
                            Numbers.Add(new Number(int.Parse(nNum), lineIndex, nNum.Length + lineIndex  - 1, i));
                            lineIndex += nNum.Length + 1;
                            wasAdded = true;
                            break;
                        }
                        //234@
                        else if (numPerLine[j].Last() == syms[k]){
                            string nNum = numPerLine[j].Remove(numPerLine[j].Length - 1);
                            Numbers.Add(new Number(int.Parse(nNum), lineIndex, nNum.Length + lineIndex  - 1, i));
                            lineIndex += nNum.Length;
                            Symbols.Add(new Symbol(syms[k].ToString(), lineIndex, i));
                            lineIndex += 2;
                            wasAdded = true;
                            break;
                        }
                        //472$293
                        else{
                            string[] nums = numPerLine[j].Split(syms[k]);
                            Numbers.Add(new Number(int.Parse(nums[0]), lineIndex, nums[0].Length + lineIndex  - 1, i));
                            lineIndex += nums[0].Length;
                            Symbols.Add(new Symbol(syms[k].ToString(), lineIndex, i));
                            lineIndex++;
                            Numbers.Add(new Number(int.Parse(nums[1]), lineIndex, nums[1].Length + lineIndex  - 1, i));
                            lineIndex += nums[1].Length + 1;
                            wasAdded = true;
                            break;
                        }
                    }
                }

                if(!wasAdded){
                    Numbers.Add(new Number(int.Parse(numPerLine[j]), lineIndex, numPerLine[j].Length + lineIndex - 1, i));
                    lineIndex += numPerLine[j].Length + 1;
                    continue;
                }
            }
        }
    }

    static void Part1(List<string> input){
        int counter = 0;
        for (int i = 0; i < Numbers.Count(); i++){
            List<(int, int)> potSymPos = new List<(int, int)>();
            potSymPos.Add((Numbers[i].Start - 1, Numbers[i].Line));         //*123
            potSymPos.Add((Numbers[i].End + 1, Numbers[i].Line));           //123*
            for (int j = Numbers[i].Start - 1; j <= Numbers[i].End + 1; j++){
                    potSymPos.Add((j, Numbers[i].Line - 1));
                    potSymPos.Add((j, Numbers[i].Line + 1));
            }
            for (int j = 0; j < potSymPos.Count(); j++){
                if (Symbols.Find(s => s.X == potSymPos[j].Item1 && s.Y == potSymPos[j].Item2) != null){
                    counter += Numbers[i].Num;
                    break;
                }
            }
        }

        Console.WriteLine(counter);
    }

    static void Part2(List<string> input){
        for (int i = 0; i < Numbers.Count(); i++){
            List<(int, int)> potSymPos = new List<(int, int)>();
            potSymPos.Add((Numbers[i].Start - 1, Numbers[i].Line));         //*123
            potSymPos.Add((Numbers[i].End + 1, Numbers[i].Line));           //123*
            for (int j = Numbers[i].Start - 1; j <= Numbers[i].End + 1; j++){
                    potSymPos.Add((j, Numbers[i].Line - 1));
                    potSymPos.Add((j, Numbers[i].Line + 1));
            }
            
            for (int j = 0; j < potSymPos.Count(); j++){
                Symbol? find = Symbols.Find(s => s.X == potSymPos[j].Item1 && s.Y == potSymPos[j].Item2);
                if (find != null){         
                    find.Num.Add(Numbers[i]);
                    break;
                }
            }
        }

        int counter = 0;
        for (int i = 0; i < Symbols.Count(); i++){
            if (Symbols[i].Sym != "*")
                continue;

            if (Symbols[i].Num.Count() != 2)
                continue;

            counter += Symbols[i].Num[0].Num * Symbols[i].Num[1].Num;
        }

        Console.WriteLine(counter);
    }

    //Part 1: 528799
    //Part 2: 84907174
    public static void Main(string[] args){
        FillLists(ReadFile());
        Part1(ReadFile());
        Part2(ReadFile());
    }
}

class Number{
    public int Num;
    public int Start;
    public int End;
    public int Line;

    public Number(int num, int start, int end, int line){
        Num = num;
        Start = start;
        End = end;
        Line = line;
    }
}

class Symbol{
    public string Sym;
    public int X;
    public int Y;
    public List<Number> Num;

    public Symbol(string sym, int x, int y){
        Sym = sym;
        X = x;
        Y = y;
        Num = new List<Number>();
    }
}