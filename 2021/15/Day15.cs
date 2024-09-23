using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

class Day15{

    static public List<string> Input = new List<string>();
    static public int[,] Grid;

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

        static void CreateGrid()
    {
        Grid = new int[Input[0].Length, Input.Count];
        for (int i = 0; i < Input.Count; i++)
        {
            for (int j = 0; j < Input[i].Length; j++)
            {
                Grid[j, i] = int.Parse(Input[i][j].ToString());
            }
        }
    }

    static void Part1(){

    }

    static void Part2(){

    }

    //Part 1: 
    //Part 2: 
              
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}
