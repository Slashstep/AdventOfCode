using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;

class Day02{

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
        int posX = 0;
        int posY = 0;

        for (int i = 0; i < Input.Count(); i++){
            string[] command = Input[i].Split(" ");

            switch (command[0]){
                case "forward":
                    posX += int.Parse(command[1]);
                    break;
                case "up":
                    posY -= int.Parse(command[1]);
                    break;
                case "down":
                    posY += int.Parse(command[1]);
                    break;
                default:
                    break;
            }
        }

        Console.WriteLine(posX * posY);
    }

    static void Part2(){
        int posX = 0;
        int posY = 0;
        int aim = 0;

        for (int i = 0; i < Input.Count(); i++){
            string[] command = Input[i].Split(" ");

            switch (command[0]){
                case "forward":
                    posX += int.Parse(command[1]);
                    posY += aim * int.Parse(command[1]);
                    break;
                case "up":
                    aim -= int.Parse(command[1]);
                    break;
                case "down":
                    aim += int.Parse(command[1]);
                    break;
                default:
                    break;
            }
        }

        Console.WriteLine(posX * posY);
    }

    //Part 1: 1746616
    //Part 2: 1741971043
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}
