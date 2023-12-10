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

        int counter = 0;
        for (int i = 0; i < 100; i++){
            foreach(Octopus o in Octos){
                o.isFlashing = false;
                o.Value++;
            }
            bool hasFlashed = true;
            while (hasFlashed){
                hasFlashed = false;

                for (int j = 0; j < Octos.Count; j++){
                    if (Octos[j].Value > 9 && !Octos[j].isFlashing){
                        counter++;
                        IncreaseOctopus(Octos[j]);
                        hasFlashed = true;
                        break;
                    }
                }
            }
        }

        Console.WriteLine(counter);
    }

    static void Part2(){
        Octos = new List<Octopus>();
        for (int y = 0; y < 10; y++){
            for (int x = 0; x < 10; x++){
                Octos.Add(new Octopus(int.Parse(Input[y][x].ToString()), new Vector2(x, y)));
            }
        }

        int index = 0;
        bool isSynchronos = false;
        while (!isSynchronos){
            isSynchronos = true;
            index++;
            foreach(Octopus o in Octos){
                o.isFlashing = false;
                o.Value++;
            }
            
            bool hasFlashed = true;
            while (hasFlashed){
                hasFlashed = false;

                for (int j = 0; j < Octos.Count; j++){
                    if (Octos[j].Value > 9 && !Octos[j].isFlashing){
                        IncreaseOctopus(Octos[j]);
                        hasFlashed = true;
                        break;
                    }
                }
            }
            
            foreach (Octopus o in Octos){
                if (!o.isFlashing){
                    isSynchronos = false;
                    break;
                }
            }
        }
        
        Console.WriteLine(index);
    }

    static void IncreaseOctopus(Octopus octo){
        octo.isFlashing = true;
        octo.Value = 0;

        for (int y = -1; y <= 1; y++){
            for (int x = -1; x <= 1; x++){
                Octopus nextOctopus = Octos.Find(o => o.Pos == new Vector2(octo.Pos.X + x, octo.Pos.Y + y));

                if (nextOctopus == null){
                    continue;
                }
                if (!nextOctopus.isFlashing){
                    nextOctopus.Value++;
                }
            }
        }
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

    //Part 1: 1681
    //Part 2: 276
              
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
