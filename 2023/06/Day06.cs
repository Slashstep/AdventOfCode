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

    static List<Race> CreateRaces(){
        string[] timesString = Input[0].Split(" ");
        string[] distancesString = Input[1].Split(" ");

        List<long> times = new List<long>();
        List<long> distances = new List<long>();

        List<Race> races = new List<Race>();

        foreach(string s in timesString){
            long res;
            if (long.TryParse(s, out res)){
                times.Add(res);
            }
        }

        foreach(string s in distancesString){
            long res;
            if (long.TryParse(s, out res)){
                distances.Add(res);
            }
        }

        for(int i = 0; i < times.Count(); i++){
            races.Add(new Race(times[i], distances[i]));
        }

        return races;
    }

    static Race TheOneRace(){
        string times = Input[0].Replace(" ", "");
        string distances = Input[1].Replace(" ", "");

        string[] tString = times.Split(":");
        string[] dString = distances.Split(":");

        return new Race(long.Parse(tString[1]), long.Parse(dString[1]));
    }

    static void Part1(){
        List<Race> races = CreateRaces();

        long counter = 1;

        foreach(Race r in races){
            counter *= r.SimulateRace();
        }

        Console.WriteLine(counter);
    }

    static void Part2(){
        Console.WriteLine(TheOneRace().SimulateRace());
    }

    //Part 1: 2374848
    //Part 2: 39132886

    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}

class Race{
    public long Time;
    public long RecordDistance;

    public Race(long time, long recordDistance){
        this.Time = time;
        this.RecordDistance = recordDistance;
    }

    public long SimulateRace(){
        long counter = 0;

        for (long i = 0; i <= Time; i++){
            long curSpeed = i;
            long curDistance = (Time - i) * curSpeed;

            if (curDistance > RecordDistance){
                counter++;
            }
        }

        return counter;
    }
}