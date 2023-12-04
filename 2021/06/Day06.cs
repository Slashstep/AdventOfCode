using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Day06{

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
        List<Lanternfish> fish = new List<Lanternfish>();

        for (int i = 0; i < 7; i++){
            fish.Add(new Lanternfish(i));
        }
        
        foreach (string s in nums){
            int num = int.Parse(s);

            int index = fish.IndexOf(fish.Find(f => f.Timer == num));

            if (index == -1)
                fish.Add(new Lanternfish(num));
            else
                fish[index].Copys++;
        }

        fish = fish.OrderBy(f => f.Timer).ToList();

        for (int i = 0; i < 80; i++){
            foreach(Lanternfish f in fish){
                f.Timer--;
            }
            foreach(Lanternfish f in fish){
                if (f.Timer < 0){
                    f.Timer = 6;

                    int ind = fish.IndexOf(f);

                    fish[(ind + 2) % fish.Count()].NewCopys += f.Copys;
                    f.Copys += f.NewCopys;
                    f.NewCopys = 0;
                }
            }
        }
        
        long counter = 0;
        foreach(Lanternfish f in fish){
            counter += f.Copys + f.NewCopys;
        }

        Console.WriteLine(counter.ToString());
    }

    static void Part2(){
        string[] nums = Input[0].Split(",");
        List<Lanternfish> fish = new List<Lanternfish>();

        for (int i = 0; i < 7; i++){
            fish.Add(new Lanternfish(i));
        }
        
        foreach (string s in nums){
            int num = int.Parse(s);

            int index = fish.IndexOf(fish.Find(f => f.Timer == num));

            if (index == -1)
                fish.Add(new Lanternfish(num));
            else
                fish[index].Copys++;
        }

        fish = fish.OrderBy(f => f.Timer).ToList();

        for (int i = 0; i < 256; i++){
            foreach(Lanternfish f in fish){
                f.Timer--;
            }
            foreach(Lanternfish f in fish){
                if (f.Timer < 0){
                    f.Timer = 6;

                    int ind = fish.IndexOf(f);

                    fish[(ind + 2) % fish.Count()].NewCopys += f.Copys;
                    f.Copys += f.NewCopys;
                    f.NewCopys = 0;
                }
            }
        }
        
        long counter = 0;
        foreach(Lanternfish f in fish){
            counter += f.Copys + f.NewCopys;
        }

        Console.WriteLine(counter.ToString());
    }

    //Part 1: 372300
    //Part 2: 1675781200288
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}

class Lanternfish{
    public int Timer;
    public long Copys;
    public long NewCopys;

    public Lanternfish(int timer){
        Timer = timer;
        Copys = 0;
    }
}
