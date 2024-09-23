using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;

class Day01{

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

    static void Part1(List<string> input){
        var watch = new Stopwatch();
        watch.Start();
        
        int counter = 0;
        List<string> digits = new List<string>{"1", "2", "3", "4", "5", "6", "7", "8", "9"};
        List<(int, int)> numbers = new List<(int, int)>();

        for (int i = 0; i < input.Count(); i++){
            numbers.Clear();
            for (int j = 0; j < digits.Count(); j++){
                for (int k = 0; k < input[i].Count(); k++){
                    if (input[i].Substring(k, digits[j].Count()) == digits[j])
                        numbers.Add((j + 1, k));
                }
            }

            numbers = numbers.OrderBy(tuple => tuple.Item2).ToList();
            counter += numbers[0].Item1 * 10 + numbers.Last().Item1;
        }

        Console.WriteLine("Part 1: " + counter.ToString());
        watch.Stop();
        Console.WriteLine(watch.Elapsed);
    }

    static void Part2(List<string> input){
        var watch = new Stopwatch();
        watch.Start();
        
        int counter = 0;
        List<string> digits = new List<string>{"1", "2", "3", "4", "5", "6", "7", "8", "9"};
        List<string> nums = new List<string>{"one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};
        List<(int, int)> numbers = new List<(int, int)>();

        for (int i = 0; i < input.Count(); i++){
            numbers.Clear();
            for (int j = 0; j < digits.Count(); j++){
                for (int k = 0; k < input[i].Count(); k++){
                    if (input[i].Substring(k, digits[j].Count()) == digits[j])
                        numbers.Add((j + 1, k));
                }
            }
            for (int j = 0; j < nums.Count(); j++){
                for (int k = 0; k <= input[i].Count() - nums[j].Count(); k++){
                    if (input[i].Substring(k, nums[j].Count()) == nums[j])
                        numbers.Add((j + 1, k));
                }
            }

            numbers = numbers.OrderBy(tuple => tuple.Item2).ToList();
            counter += numbers[0].Item1 * 10 + numbers.Last().Item1;
        }

        Console.WriteLine("Part2: " + counter.ToString());
        watch.Stop();
        Console.WriteLine(watch.Elapsed);
    }

    //Part 1: 54630
    //Part 2: 54770
    public static void Main(string[] args){
        Part1(ReadFile());
        Part2(ReadFile());
    }
}