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

class Day07{
    static public List<string> Input = new List<string>();

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

    static long CheckEquation(string s, bool IsPart1){
        long result = Convert.ToInt64(s.Split(':')[0]);
        List<long> numsToAdd = s.Split(':')[1]
                        .Split(' ')
                        .Where(num => !string.IsNullOrWhiteSpace(num)) // Filter out empty strings
                        .Select(long.Parse) // Convert each string to long
                        .ToList();

        
        if (IsPart1){
            if (Checker(result, numsToAdd, numsToAdd[0], 0) > 0)
                return result;
            else
                return 0;
        }
        else{
            if (Checker2(result, numsToAdd, numsToAdd[0], 0) > 0)
                return result;
            else
                return 0;
        }
    }

    static int Checker(long result, List<long> numsToAdd, long curRes, int posCounter){
        if (curRes == result && posCounter == numsToAdd.Count() - 1)
            return 1;

        if (posCounter == numsToAdd.Count() - 1)
            return 0;

        return Checker(result, numsToAdd, curRes + numsToAdd[posCounter + 1], posCounter + 1) + 
                    Checker(result, numsToAdd, curRes * numsToAdd[posCounter + 1], posCounter + 1);
    }

    static int Checker2(long result, List<long> numsToAdd, long curRes, int posCounter){
        if (curRes == result && posCounter >= numsToAdd.Count() - 1)
            return 1;

        if (posCounter >= numsToAdd.Count() - 1)
            return 0;

        return Checker2(result, numsToAdd, curRes + numsToAdd[posCounter + 1], posCounter + 1) + 
                    Checker2(result, numsToAdd, curRes * numsToAdd[posCounter + 1], posCounter + 1) +
                    Checker2(result, numsToAdd, Convert.ToInt64(curRes.ToString() + numsToAdd[posCounter + 1].ToString()), posCounter + 1);
    }




    static void Part1(){
        long count = 0;
        foreach (string s in Input){
            count += CheckEquation(s, true);
        }

        Console.WriteLine(count);
    }

    static void Part2(){
        long count = 0;
        foreach (string s in Input){
            count += CheckEquation(s, false);
        }

        Console.WriteLine(count);
    }

    //Part 1: 20665830408335
    //Part 2: 354060705047464
    
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}