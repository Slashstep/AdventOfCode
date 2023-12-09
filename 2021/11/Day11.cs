using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Day10{

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
        int[,] octos = new int[10,10];

        for (int y = 0; y < 10; y++){
            for (int x = 0; x < 10; x++){
                octos[y, x] = int.Parse(Input[y][x].ToString());
            }
        }

        long flashCounter = 0;
        for (int i = 0; i < 100; i++){
            bool hasSomethingChanged = true;
            while (hasSomethingChanged){
                for (int y = 0; y < 10; y++){
                    for (int x = 0; x < 10; x++){
                        octos[y, x]++;

                        if (octos[y, x] > 9){
                            for (int i = -1; i <= 1; i++){
                                for (int j = -1; j <= 1; j++){
                                    
                                }
                            }
                        }
                    }
                }
            }
        }
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
