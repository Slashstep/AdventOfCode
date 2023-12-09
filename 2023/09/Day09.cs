using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Day09{

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

        long counter = 0;
        for (int i = 0; i < Input.Count; i++){
            List<int> numbers = new List<int>();
            string[] num = Input[i].Split(" ");
            foreach (string s in num){
                numbers.Add(int.Parse(s));
            }

            numbers = FindMissingLastNumbers(numbers);
            counter += numbers.Last();
        }

        Console.WriteLine(counter);
    }

    static List<int> FindMissingLastNumbers(List<int> val){
        if (val.Count == 1){
            val.Add(0);
            return val;
        }

        bool isAllZero = true;
        foreach (int i in val){
            if (i != 0){
                isAllZero = false;
            }
        }

        if (isAllZero){
            val.Add(0);
            return val;
        }

        List<int> res = new List<int>();
        for (int i = 0; i < val.Count - 1; i++){
            res.Add(val[i + 1] - val[i]);
        }

        val.Add(val.Last() + FindMissingLastNumbers(res).Last());

        return val;
    }

    static void Part2(){
        long counter = 0;
        for (int i = 0; i < Input.Count; i++){
            List<int> numbers = new List<int>();
            string[] num = Input[i].Split(" ");
            foreach (string s in num){
                numbers.Add(int.Parse(s));
            }

            numbers = FindMissingFirstNumbers(numbers);
            counter += numbers[0];
        }

        Console.WriteLine(counter);
    }

    static List<int> FindMissingFirstNumbers(List<int> val){
        if (val.Count == 1){
            val.Insert(0, 0);
            return val;
        }

        bool isAllZero = true;
        foreach (int i in val){
            if (i != 0){
                isAllZero = false;
            }
        }

        if (isAllZero){
            val.Insert(0, 0);
            return val;
        }

        List<int> res = new List<int>();
        for (int i = 0; i < val.Count - 1; i++){
            res.Add(val[i + 1] - val[i]);
        }

        val.Insert(0, val[0] - FindMissingFirstNumbers(res)[0]);

        return val;
    }

    //Part 1: 1861775706
    //Part 2: 1082
              
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}
