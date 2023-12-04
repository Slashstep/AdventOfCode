using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        return lines;
    }

    static void Part1(){
        string[] nums = Input[0].Split(",");
        List<int> crabs = new List<int>();

        for (int i = 0; i < nums.Length; i++){
            crabs.Add(int.Parse(nums[i]));
        }

        crabs.Sort();

        long maxFuel = 1000000000;
        long curFuel = 0;

        for (int i = 0; i < crabs.Last(); i++){
            curFuel = 0;
            for (int j = 0; j < crabs.Count(); j++){
                curFuel += Math.Abs(crabs[j] - i);
                if (curFuel > maxFuel){
                    break;
                }
            }
            if (curFuel < maxFuel){
                maxFuel = curFuel;
            }
        }

        Console.WriteLine(maxFuel);
    }

    static void Part2(){
        string[] nums = Input[0].Split(",");
        List<int> crabs = new List<int>();

        for (int i = 0; i < nums.Length; i++){
            crabs.Add(int.Parse(nums[i]));
        }

        crabs.Sort();

        long maxFuel = 1000000000;
        long curFuel = 0;

        for (int i = 0; i < crabs.Last(); i++){
            curFuel = 0;
            for (int j = 0; j < crabs.Count(); j++){
                for (int k = 1; k <= Math.Abs(crabs[j] - i); k++){
                    curFuel += k;
                }

                if (curFuel > maxFuel){
                    break;
                }
            }
            if (curFuel < maxFuel){
                maxFuel = curFuel;
            }
        }

        Console.WriteLine(maxFuel);
    }

    //Part 1: 
    //Part 2: 
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}
