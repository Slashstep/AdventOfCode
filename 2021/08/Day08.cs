using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Day08{

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
        for (int i = 0; i < Input.Count(); i++){
            string[] leftRight = Input[i].Split(" | ");
            string[] nums = leftRight[1].Split(" ");

            for (int j = 0; j < nums.Length; j++){
                if (nums[j].Length == 2 || nums[j].Length == 3 || nums[j].Length == 4 || nums[j].Length == 7){
                    counter++;
                }
            }
        }

        Console.WriteLine(counter);
    }

    static void Part2(){
        List<string> a = new List<string>();
        List<string> b = new List<string>();
        List<string> c = new List<string>();
        List<string> d = new List<string>();
        List<string> e = new List<string>();
        List<string> f = new List<string>();
        List<string> g = new List<string>();

        int counter = 0;
        for (int i = 0; i < Input.Count(); i++){
            string[] leftRight = Input[i].Split(" | ");
            string[] code = leftRight[0].Split(" ");
            string[] nums = leftRight[1].Split(" ");

            //Sort strings alphabetically
            for (int j = 0; j < code.Length; j++){
                code[j] = new string (code[j].OrderBy(c => c).ToArray());

                //Find 1 --> potential c, f
                if (code[j].Length == 2){
                    c.Add(code[j][0]);
                    c.Add(code[j][1]);
                    f.Add(code[j][0]);
                    f.Add(code[j][1]);
                }
            }

            foreach (string s in code){
                //Find 7 --> save a
                if (s.Length == 3){
                    foreach (string s2 in s){
                        if (!c.Contains(s2)){
                            a.Add(s2);
                        }
                    }
                }
                //Find 4 --> potential b, d
                else if (s.Length == 4){
                    foreach (string s2 in s){
                        if (!c.Contains(s2)){
                            b.Add(s2);
                            d.Add(s2);
                        }
                    }
                }
            }

            //Find 3 --> save b, d, g
            foreach (string s in code){
                if (s.Length != 5){
                    continue;
                }

                if (!s.Contains(c[0]) || ! s.Contains(c[1])){
                    continue;
                }

                


            }



        }
        Console.WriteLine(counter);
    }

    //Part 1: 
    //Part 2: 
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}
