using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;

class Day01{

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

    static void Part1(){
        int counter = 0;
        List<int> intList = new List<int>();
        
        foreach (string s in Input){
            intList.Add(int.Parse(s));
        }

        counter = 0;
        for (int i = 0; i < intList.Count() - 1; i++){
            if (intList[i] < intList[i+1])
                counter++;
        }

        Console.WriteLine(counter);
    }

    static void Part2(){
        List<int> intList = new List<int>();
        
        foreach (string s in Input){
            intList.Add(int.Parse(s));
        }

        int counter = 0;
        int oldMeasure = 100000000;
        int curMeasure = 0;
        for (int i = 0; i < intList.Count() - 2; i++){
            curMeasure = intList[i] + intList[i+1] + intList[i+2];

            if (curMeasure > oldMeasure)
                counter++;

            oldMeasure = curMeasure;
        }

        Console.WriteLine(counter);
    }

    //Part 1: 1692
    //Part 2: 1724
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}
