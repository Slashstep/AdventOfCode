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

class Day05{
    static public List<string> Input = new List<string>();
    static public List<string> Rules = new List<string>();
    static public List<string> Copys = new List<string>();
    static public List<List<string>> FalseCopys = new List<List<string>>();

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

    static void InputFiller(){
        int counter = 0;
        foreach (string s in Input){
            if (s == "") break;
            counter++;
            Rules.Add(s);
        }

        for (int i = counter + 1; i < Input.Count(); i++){
            Copys.Add(Input[i]);
        }
    }

    static int CheckCopy(string s){
        string[] sep = s.Split(',');

        for (int i = 1; i < sep.Length; i++){
            for (int j = 0; j < i; j++){
                string test = sep[i] + "|" + sep[j];
                
                if (Rules.Contains(test)){
                    FalseCopys.Add(sep.ToList());
                    return 0;
                }
            }
        }

        return Convert.ToInt32(sep[sep.Length/2]);
    }

    static int FixCopy(List<string> copy){
        for (int i = 1; i < copy.Count(); i++){
            for (int j = 0; j < i; j++){
                string test = copy[i] + "|" + copy[j];
                
                if (Rules.Contains(test)){
                    string b = copy[i];
                    copy[i] = copy[j];
                    copy[j] = b;
                    i = 1;
                    j = 0;
                }
            }
        }

        return Convert.ToInt32(copy[copy.Count()/2]);
    }

    static void Part1(){
        InputFiller();
        int sum = 0;

        foreach(string s in Copys)
            sum += CheckCopy(s);

        Console.WriteLine(sum);
    }

    static void Part2(){
        int sum = 0;   

        foreach (List<string> s in FalseCopys)
            sum += FixCopy(s);

        Console.WriteLine(sum);
    }

    //Part 1: 7074
    //Part 2: 4828
    
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}