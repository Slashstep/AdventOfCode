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
using System.Runtime.CompilerServices;

class Day08{
    static public List<string> Input = new List<string>();
    static public List<Antenna> Antennas = new List<Antenna>();

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

    static void FindAntennas(){
        for (int y = 0; y < Input.Count(); y++){
            for (int x = 0; x < Input[y].Length; x++){
                if (Input[y][x] == '.') continue;

                int index = Antennas.FindIndex(an => an.Freq == Input[y][x]);
                if (index < 0){
                    Antenna newAntenna = new Antenna(Input[y][x], x, y);
                    Antennas.Add(newAntenna);
                }
                else{
                    Antennas[index].AddPosition(x, y);
                }
            }
        }
    }

    static void Part1(){
        FindAntennas();
        
        HashSet<(int, int)> antinode = new HashSet<(int, int)>();
        foreach (Antenna a in Antennas){
            if (a.Positions.Count() < 2) continue;

            for (int i = 0; i < a.Positions.Count() - 1; i++){
                for (int j = i + 1; j < a.Positions.Count(); j++){
                    //Calc Manhattan Dist
                    int xD = a.Positions[j].Item1 - a.Positions[i].Item1;
                    int yD = a.Positions[j].Item2 - a.Positions[i].Item2;

                    //Get Antinode
                    (int x1, int y1) first = (a.Positions[j].Item1 + xD, a.Positions[j].Item2 + yD);
                    (int x2, int y2) second = (a.Positions[i].Item1 - xD, a.Positions[i].Item2 - yD);

                    //Check for Outof Bounds than add to HashSet
                    if (first.Item1 < 0 || first.Item1 >= Input[0].Length || first.Item2 < 0 || first.Item2 >= Input.Count()) first = (-1, -1);

                    if (second.Item1 < 0 || second.Item1 >= Input[0].Length || second.Item2 < 0 || second.Item2 >= Input.Count()) second = (-1, -1);

                    antinode.Add(first);
                    antinode.Add(second);
                }
            }
        }

        antinode.Remove((-1, -1));
        Console.WriteLine(antinode.Count());
    }

    static void Part2(){
        HashSet<(int, int)> antinode = new HashSet<(int, int)>();
        foreach (Antenna a in Antennas){
            if (a.Positions.Count() < 2) continue;

            for (int i = 0; i < a.Positions.Count() - 1; i++){
                for (int j = i + 1; j < a.Positions.Count(); j++){
                    antinode.Add(a.Positions[i]);
                    antinode.Add(a.Positions[j]);

                    //Calc Manhattan Dist
                    int xD = a.Positions[j].Item1 - a.Positions[i].Item1;
                    int yD = a.Positions[j].Item2 - a.Positions[i].Item2;

                    //Get Antinode
                    (int x, int y) first = a.Positions[j];
                    (int x, int y) second = a.Positions[i];
                    int count = 1;

                    while (first != (-1, -1)){
                        first = (a.Positions[j].Item1 + xD * count, a.Positions[j].Item2 + yD * count);
                        //Check for Outof Bounds than add to HashSet
                        if (first.x < 0 || first.x >= Input[0].Length || first.y < 0 || first.y >= Input.Count()) 
                            first = (-1, -1);
                        
                        antinode.Add(first);
                        count++;
                    }

                    count = 1;
                    while (second != (-1, -1)){
                        second = (a.Positions[i].Item1 - xD * count, a.Positions[i].Item2 - yD * count);

                        //Check for Outof Bounds than add to HashSet
                        if (second.x < 0 || second.x >= Input[0].Length || second.y < 0 || second.y >= Input.Count()) 
                            second = (-1, -1);

                        antinode.Add(second);
                        count++;
                    }
                }
            }
        }

        antinode.Remove((-1, -1));
        Console.WriteLine(antinode.Count());
    }

    //Part 1: 398
    //Part 2: 1333
    
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}

class Antenna{
    public char Freq;
    public List<(int, int)> Positions = new List<(int, int)> ();

    public Antenna(char freq, int x, int y){
        Freq = freq;
        AddPosition(x, y);
    }

    public void AddPosition(int x, int y){
        Positions.Add((x, y));
    }
}