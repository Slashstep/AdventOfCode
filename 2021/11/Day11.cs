using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

class Day11{

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

    static List<Octopus> Octos = new List<Octopus>();
    static void Part1(){

        for (int y = 0; y < 10; y++){
            for (int x = 0; x < 10; x++){
                Octos.Add(new Octopus(int.Parse(Input[y][x].ToString()), new Vector2(x, y)));
            }
        }

        PrintList(Octos);

        for (int i = 0; i < 10; i++){
            foreach(Octopus o in Octos){
                o.Value++;
            }
            foreach(Octopus o in Octos){
                IncreaseOctopus(o);
            }

            PrintList(Octos);
        }
    }

    static void Part2(){
        
    }

    static void IncreaseOctopus(Octopus octo){
        if (octo.isFlashing){
            return;
        }

        if (octo.Value > 9){
            octo.isFlashing = true;
            octo.Value = 0;

            for (int y = -1; y <= 1; y++){
                for (int x = -1; x <= 1; x++){
                    Octopus nextOctopus = Octos.Find(o => o.Pos == new Vector2(o.Pos.X + x, o.Pos.Y + y));

                    if (nextOctopus == null){
                        continue;
                    }

                    IncreaseOctopus(nextOctopus);
                }
            }
        }

        return;
    }

    static void PrintList(List<Octopus> o){
        
        string s = "";
        for (int i = 0; i < o.Count; i++){
            if (i % 10 == 0){
                Console.WriteLine(s);
                s = "";
            }
            s += o[i].Value.ToString();
            
        }
        Console.WriteLine(s);
    }

    //Part 1: 
    //Part 2: 
              
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}

class Octopus{
    public int Value;
    public bool isFlashing;
    public Vector2 Pos;

    public Octopus(int value, Vector2 pos){
        this.Value = value;
        isFlashing = false;
        this.Pos = pos;
    }
}
