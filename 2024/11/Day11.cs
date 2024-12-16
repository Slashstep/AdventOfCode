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
using System.Runtime.CompilerServices;

class Day11{
    static public List<string> Input = new List<string>();
    static public List<long> Stones = new List<long>();

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

    static List<long> StartStones(){
        List<long> newStones = new List<long>();
        
        string[] s = Input[0].Split(' ');

        foreach (string s2 in s )
            newStones.Add(Convert.ToInt64(s2));

        return newStones;
    }

    static List<long> Blink(){
        List<long> copy = new List<long>();

        for (int i = 0; i < Stones.Count(); i++){
            //0 --> 1
            if (Stones[i] == 0){
                copy.Add(1);
                continue;
            }

            //even digits --> two numbers
            string snum = Stones[i].ToString();
            if (snum.Length%2 == 0){
                long n1 = Convert.ToInt64(snum.Substring(0, snum.Length/2));
                long n2 = Convert.ToInt64(snum.Substring(snum.Length/2, snum.Length/2));

                copy.Add(n1);
                copy.Add(n2);
                continue;
            }

            copy.Add(Stones[i] * 2024);
        }

        return copy;
    }

    static void Part1(){
        Stones = StartStones();

        for (int i = 0; i < 25; i++){
            Stones = Blink();
        }

        Console.WriteLine(Stones.Count());
    }

    static long CheckNum(long numToCheck, int counter, Dictionary<long, long> checkedNums){
        if (counter > 75)
            return 1;

        
    }

    static void Part2(){
        Stones = StartStones();

        for (int i = 0; i < 75; i++){
            Stones = Blink();
        }

        Console.WriteLine(Stones.Count());
    }

    //Part 1: 
    //Part 2: 
    
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}