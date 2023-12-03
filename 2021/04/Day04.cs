using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Globalization;

class Day04{

    static public List<string> Input = new List<string>();

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

    static List<BingoField> CreateBingoFields(){
        List<BingoField> fields = new List<BingoField>();
        
        for (int i = 2; i < Input.Count() - 4; i += 6){
            BingoField bingo = new BingoField();
            for (int j = i; j < i+5; j++){
                string[] nums = Input[j].Split(" ");
                foreach (string s in nums){
                    if (s == "")
                        continue;

                    bingo.Numbers.Add((int.Parse(s), false));
                }
            }
            fields.Add(bingo);
        }

        return fields;
    }

    static void Part1(){
        List<BingoField> fields = CreateBingoFields();
        string[] draws = Input[0].Split(",");

        for (int i = 0; i < draws.Count(); i++){
            foreach (BingoField b in fields){
                b.CheckForHit(int.Parse(draws[i]));
                if (b.CheckForBingo()){
                    Console.WriteLine(b.UncheckedNumbers() * int.Parse(draws[i]));
                    return;
                }
            }
        }
    }

    static void Part2(){
        List<BingoField> fields = CreateBingoFields();
        string[] draws = Input[0].Split(",");
        List<BingoField> curFields = new List<BingoField>(fields);

        for (int i = 0; i < draws.Count(); i++){
            if (fields.Count() == 1){
                fields[0].CheckForHit(int.Parse(draws[i]));
                if (fields[0].CheckForBingo()){
                    Console.WriteLine(fields[0].UncheckedNumbers() * int.Parse(draws[i]));
                    return;
                }
                else
                    continue;
            }

            curFields = new List<BingoField>(fields);

            foreach (BingoField b in fields){
                b.CheckForHit(int.Parse(draws[i]));
                if (b.CheckForBingo()){
                    curFields.Remove(b);
                }
            }

            fields = curFields;
        }
    }

    //Part 1: 32844
    //Part 2: 4920
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}

class BingoField{
    public List<(int, bool)> Numbers = new List<(int, bool)>();

    public BingoField(){
        Numbers = new List<(int, bool)>();
    }

    public void CheckForHit(int numToCheck){
        int index = Numbers.IndexOf(Numbers.Find(m => m.Item1 == numToCheck));

        if (index >= 0)
            Numbers[index] = (Numbers[index].Item1, true);
    }

    public bool CheckForBingo(){
        
        for (int i = 0; i < Numbers.Count() - 5; i += 5){
            if (Numbers[i].Item2 && Numbers[i + 1].Item2 && Numbers[i + 2].Item2 && Numbers[i + 3].Item2 && Numbers[i + 4].Item2)
                return true;
        }
        for (int i = 0; i < 5; i++){
            if (Numbers[i].Item2 && Numbers[5 + i].Item2 && Numbers[2 * 5 + i].Item2 && Numbers[3 * 5 + i].Item2 && Numbers[4 * 5 + i].Item2)
                return true;
        }

        return false;
    }

    public int UncheckedNumbers(){
        int counter = 0;
        foreach ((int, bool) number in Numbers){
            if (!number.Item2)
                counter += number.Item1;
        }

        return counter;
    }
}
