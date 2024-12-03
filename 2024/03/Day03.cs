using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

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
        catch (Exception ex){
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        return lines;
    }

    static void Part1(){
        long sum = 0;
        string pattern = @"mul\(\d{1,3},\d{1,3}\)";
        foreach (string s in Input){
            MatchCollection matches = Regex.Matches(s, pattern);

            foreach (Match match in matches){
                string sub = match.ToString();
                sub = sub.Substring(4, sub.Length - 5);
                sum += Convert.ToInt32(sub.Split(',')[0]) * Convert.ToInt32(sub.Split(',')[1]);
            }
        }

        Console.WriteLine(sum);
    }

    static void Part2(){
        long sum = 0;
        string pattern = @"mul\(\d{1,3},\d{1,3}\)|do\(\)|don\'t\(\)";
        bool isAllowed = true;
        foreach (string s in Input){
            MatchCollection matches = Regex.Matches(s, pattern);

            foreach (Match match in matches){
                string sub = match.ToString();
                if (sub == "do()"){
                    isAllowed = true;
                    continue;
                } 
                else if (sub == "don't()"){
                    isAllowed = false;
                    continue;
                }

                if (isAllowed){
                    sub = sub.Substring(4, sub.Length - 5);
                    sum += Convert.ToInt32(sub.Split(',')[0]) * Convert.ToInt32(sub.Split(',')[1]);
                }
            }
        }

        Console.WriteLine(sum);
    }

    //Part 1: 173731097
    //Part 2: 93729253
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}