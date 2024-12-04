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

class Day04{
    static public List<string> Input = new List<string>();
    static public Char[,] Riddle;

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

    static void CreateRiddle(){
        int x = Input[0].Length;
        int y = Input.Count();

        Char[,] riddle = new Char[y, x];

        for (int i = 0; i < y; i++){
            for (int j = 0; j < x; j++){
                riddle[i, j] = Input[i][j];
            }
        }

        Riddle = riddle;
    }

    static int Checker(int i, int j){
        //Cheks every direction for XMAS
        char[] xmas = {'X', 'M', 'A', 'S'};
        int counter = 0;
        bool HasFound = true;
        //Checks l -> r
        for (int x = 0; x < 4; x++){
            if (j + x >= Riddle.GetLength(1)) {HasFound = false; break;}
            if (Riddle[i, j + x] != xmas[x]) {HasFound = false; break;}
        }
        if (HasFound) counter++;
        HasFound = true;

        //Checks lu -> rd
        for (int x = 0; x < 4; x++){
            if (j + x >= Riddle.GetLength(1) || i + x >= Riddle.GetLength(0)) {HasFound = false; break;}
            if (Riddle[i + x, j + x] != xmas[x]) {HasFound = false; break;}
        }
        if (HasFound) counter++;
        HasFound = true;

        //Checks u -> d
        for (int x = 0; x < 4; x++){
            if (i + x >= Riddle.GetLength(0)) {HasFound = false; break;}
            if (Riddle[i + x, j] != xmas[x]) {HasFound = false; break;}
        }
        if (HasFound) counter++;
        HasFound = true;

        //Checks ru -> ld
        for (int x = 0; x < 4; x++){
            if (j - x < 0 || i + x >= Riddle.GetLength(0)) {HasFound = false; break;}
            if (Riddle[i + x, j - x] != xmas[x]) {HasFound = false; break;}
        }
        if (HasFound) counter++;
        HasFound = true;

        //Checks r -> l
        for (int x = 0; x < 4; x++){
            if (j - x < 0) {HasFound = false; break;}
            if (Riddle[i, j - x] != xmas[x]) {HasFound = false; break;}
        }
        if (HasFound) counter++;
        HasFound = true;

        //Checks rd -> lu
        for (int x = 0; x < 4; x++){
            if (j - x < 0 || i - x < 0) {HasFound = false; break;}
            if (Riddle[i - x, j - x] != xmas[x]) {HasFound = false; break;}
        }
        if (HasFound) counter++;
        HasFound = true;

        //Checks d -> u
        for (int x = 0; x < 4; x++){
            if (i - x < 0) {HasFound = false; break;}
            if (Riddle[i - x, j] != xmas[x]) {HasFound = false; break;}
        }
        if (HasFound) counter++;
        HasFound = true;

        //Checks ld -> ru
        for (int x = 0; x < 4; x++){
            if (j + x >= Riddle.GetLength(1) || i - x < 0) {HasFound = false; break;}
            if (Riddle[i - x, j + x] != xmas[x]) {HasFound = false; break;}
        }
        if (HasFound) counter++;

        return counter;
    }

    static bool Checker2(int i, int j){
        if (i - 1 < 0 || j - 1 < 0 || i + 1 >= Riddle.GetLength(0) || j + 1 >= Riddle.GetLength(1)) return false;

        char lu = Riddle[i - 1, j - 1];
        char ru = Riddle[i - 1, j + 1];
        char ld = Riddle[i + 1, j - 1];
        char rd = Riddle[i + 1, j + 1];

        if (lu == 'M' && ru == 'M' && rd == 'S' && ld == 'S') return true;
        if (lu == 'M' && ru == 'S' && rd == 'S' && ld == 'M') return true;
        if (lu == 'S' && ru == 'M' && rd == 'M' && ld == 'S') return true;
        if (lu == 'S' && ru == 'S' && rd == 'M' && ld == 'M') return true;
        
        return false;
    }

    static void Part1(){
        CreateRiddle();
        int sum = 0;

        for (int i = 0; i < Riddle.GetLength(0); i++){
            for (int j = 0; j < Riddle.GetLength(1); j++){
                if (Riddle[i, j] == 'X'){
                    sum += Checker(i, j);
                }
            }
        }

        Console.WriteLine(sum);
    }

    static void Part2(){
        int sum = 0;

        for (int i = 0; i < Riddle.GetLength(0); i++){
            for (int j = 0; j < Riddle.GetLength(1); j++){
                if (Riddle[i, j] == 'A' && Checker2(i, j)) sum++;
            }
        }

        Console.WriteLine(sum);
    }

    //Part 1: 2534
    //Part 2: 1866
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}