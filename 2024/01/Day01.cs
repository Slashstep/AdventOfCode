using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Xml.Serialization;

class Day01{
    static public List<string> Input = new List<string>();
    static public List<int> Left = new List<int>();
    static public List<int> Right = new List<int>();


    static List<string> ReadFile(){
        List<string> lines = new List<string>();

        try{
            if (File.Exists("input.txt")){
                lines.AddRange(File.ReadAllLines("input.txt"));
            }
            else
            {
                Console.WriteLine("The file does not exist.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        return lines;
    }

    static void FillLists(List<string> input){
        foreach (string s in input){
            Left.Add(Convert.ToInt32(s.Split("   ")[0]));
            Right.Add(Convert.ToInt32(s.Split("   ")[1]));
        }

        Left.Sort();
        Right.Sort();
    }

    static void Part1(){
        FillLists(ReadFile());

        long difSum = 0;

        for (int i = 0; i < Left.Count(); i++)
            difSum += (int)MathF.Abs(Left[i]- Right[i]);

        Console.WriteLine(difSum);
    }

    static void Part2(){
        long totalSum = 0;

        foreach (int i in Left){
            int counter = 0;

            foreach (int j in Right){
                if (i == j) counter++;
            }

            totalSum += i * counter;
        }

        Console.WriteLine(totalSum);
    }

    //Part 1: 1223326
    //Part 2: 
    public static void Main(string[] args){
        Part1();
        Part2();
    }
}