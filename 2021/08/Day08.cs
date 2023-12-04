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

    //Part 1: 
    //Part 2: 
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}
