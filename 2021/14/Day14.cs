using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

class Day14{

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

    static List<Rule> GetRules(){
        List<Rule> rules = new List<Rule>();

        for (int i = 2; i < Input.Count; i++){
            string[] stuff = Input[i].Split(" -> ");
            rules.Add(new Rule(stuff[0], stuff[1]));
        }

        return rules;
    }


    static void Part1(){
        List<Rule> Rules = GetRules();

        string polymer = Input[0];
        for (int i = 0; i < 10; i++){
            string newPolymer = polymer[0].ToString();
            for (int j = 0; j < polymer.Length - 1; j++){
                newPolymer += Rules.Find(r => r.In == polymer.Substring(j, 2))?.Out;
            }
            polymer = newPolymer;
        }

        Dictionary<char, int> letters = new Dictionary<char, int>();
        for (int i = 0; i < polymer.Length; i++){
            if (letters.ContainsKey(polymer[i])) letters[polymer[i]]++;
            else letters.Add(polymer[i], 1);
        }

        Console.WriteLine(letters.Values.Max() - letters.Values.Min());
    }

    static void Part2(){
        List<Rule> Rules = GetRules();
        Dictionary<string, long> pairs = new Dictionary<string, long>();
        Dictionary<char, long> let = new Dictionary<char, long>();
        string polymer = Input[0];

        for (int i = 0; i < polymer.Length - 1; i++){
            pairs.Add(polymer.Substring(i, 2), 1);
        }

        for (int j = 0; j < 40; j++){
            Dictionary<string, long> newPairs = new Dictionary<string, long>();

            foreach (string p in pairs.Keys){
                string sub = p[0] + Rules.Find(r => r.In == p.Substring(0, 2))?.Out;
                string s1 = sub.Substring(0, 2);
                string s2 = sub.Substring(1, 2);

                if (newPairs.ContainsKey(s1)) newPairs[s1] += pairs[p];
                else newPairs.Add(s1, pairs[p]);

                if (newPairs.ContainsKey(s2)) newPairs[s2] += pairs[p];
                else newPairs.Add(s2, pairs[p]);

                if (let.ContainsKey(s2[0])) let[s2[0]] += pairs[p];
                else let.Add(s2[0], pairs[p]);
                
            }
            
            pairs = newPairs;
        }

        Console.WriteLine((let.Values.Max() - let.Values.Min() + 1));
    }

    //Part 1: 2408
    //Part 2: 2651311098752
              
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}

class Rule{
    public string In;
    public string Out;

    public Rule(string input, string output){
        In = input;
        Out = output + input[1];
    }
}
