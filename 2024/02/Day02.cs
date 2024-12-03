using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Xml.Serialization;

class Day01{
    static public List<string> Input = new List<string>();


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

    static bool IsSave(List<int> levels){
        int oldDist = 0;

        for (int i = 0; i < levels.Count - 1; i++){
            if (levels[i] == levels[i + 1]) return false;
            if (MathF.Abs(levels[i] - levels[i + 1]) > 3) return false;

            int newDist = levels[i + 1] - levels[i];

            if (newDist < 0 && oldDist > 0 || newDist > 0 && oldDist < 0) return false;
            
            oldDist = newDist;
        }

        return true;
    }

    static bool IsSaveDampend(List<int> levels, int l){       
        if (IsSave(levels)) return true;

        for (int i = 0; i < levels.Count(); i++){
            List<int> newLevels = new List<int>(levels);
            newLevels.RemoveAt(i);

            if (IsSave(newLevels)) return true;
        }

        return false;
    }

    static void Part1(){
        List<int> levels = new List<int>();
        int counter = 0;

        foreach (string s in Input){
            levels.Clear();
            string[] parts = s. Split(' ');
            foreach (string p in parts) levels.Add(Convert.ToInt32(p));

            if (IsSave(levels)) counter++;
            }

        Console.WriteLine(counter);
    }

    //EdgeCase --> Last Integer is bad actor
    static void Part2(){
        List<int> levels = new List<int>();
        int counter = 0;

        foreach (string s in Input){
            levels.Clear();
            string[] parts = s. Split(' ');
            foreach (string p in parts) levels.Add(Convert.ToInt32(p));

            if (IsSaveDampend(levels, 0)) counter++;
            else Console.WriteLine(s);
            }

        Console.WriteLine(counter);
    }

    //Part 1: 202
    //Part 2: 271
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}