using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Reflection.PortableExecutable;

class Day06{
    static public List<string> Input = new List<string>();
    static public (int, int)[] dirs = {(0, -1), (1, 0), (0, 1), (-1, 0)};
    static public HashSet<(int, int)> Visited = new HashSet<(int, int)> ();

    static List<string> ReadFile(){
        List<string> lines = new List<string>();

        try{
            if (File.Exists("input.txt"))
                lines.AddRange(File.ReadAllLines("input.txt"));
            else
                Console.WriteLine("The file does not exist.");
        }
        catch (Exception ex){
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        return lines;
    }

    static (int, int) GetStartPos(){
        for (int i = 0; i < Input.Count(); i++){
            int index = Input[i].IndexOf('^');

            if (index > -1) return (index, i);
        }

        return (0, 0);
    }

    static List<string> PlaceObstical(int x, int y){
        List<string> newInput = new List<string>(Input);

        newInput[y] = newInput[y].Substring(0, x) + "#" + newInput[y].Substring(x + 1, newInput[y].Length - x - 1);

        return newInput;
    }

    static void Part1(){
        (int, int) curPos = GetStartPos();
        Visited.Add(curPos);
        int dirPointer = 0;

        do{
            (int, int) newPos = (curPos.Item1 + dirs[dirPointer].Item1, curPos.Item2 + dirs[dirPointer].Item2);

            //Check for Turns
            if (newPos.Item1 < 0 || newPos.Item1 >= Input[0].Length || newPos.Item2 < 0 || newPos.Item2 >= Input.Count())
                break;

            if (Input[newPos.Item2][newPos.Item1] == '#'){
                dirPointer = (dirPointer + 1)% dirs.Length;
                continue;
            }

            curPos = newPos;
            Visited.Add(curPos);

        }
        while(true);

        Console.WriteLine(Visited.Count());
    }

    static void PrintList(List<string> toPrint){
        foreach(string s in toPrint)
            Console.WriteLine(s);
        
        Console.WriteLine("\n");
    }

    static void Part2(){
        int counter;
        int loopCounter = 0;
        for (int y = 0; y < Input.Count(); y++){
            for (int x = 0; x < Input[y].Length; x++){
                (int, int) curPos = GetStartPos();
                if (curPos == (x, y)) continue;
                List<string> ToCheck = PlaceObstical(x, y);
                counter = 0;
                int dirPointer = 0;

                do{
                    (int, int) newPos = (curPos.Item1 + dirs[dirPointer].Item1, curPos.Item2 + dirs[dirPointer].Item2);

                    //Check for Turns
                    if (newPos.Item1 < 0 || newPos.Item1 >= ToCheck[0].Length || newPos.Item2 < 0 || newPos.Item2 >= ToCheck.Count())
                        break;

                    if (ToCheck[newPos.Item2][newPos.Item1] == '#'){
                        dirPointer = (dirPointer + 1)% dirs.Length;
                        continue;
                    }

                    curPos = newPos;
                    counter++;
                    if (counter >= Input.Count() * Input.Count()){
                        Console.WriteLine("Loop");
                        loopCounter++;
                    }

                }
                while(counter < Input.Count() * Input.Count());

            }
        }

        Console.WriteLine(loopCounter);
    }

    //Part 1: 5461
    //Part 2: 1836
    
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}