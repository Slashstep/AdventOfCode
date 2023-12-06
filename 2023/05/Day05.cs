using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Day05{

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

    static List<long> SeparateSeeds(){
        List<long> seeds = new List<long>();

        string[] leftRight = Input[0].Split(": ");
        string[] rightNum = leftRight[1].Split(" ");

        foreach (string s in rightNum){
            seeds.Add(long.Parse(s));
        }

        return seeds;
    }

    static List<List<Map>> GenerateMaps(){
        List<List<Map>> maps = new List<List<Map>>();
        List<Map> curMap = new List<Map>();
        for (int i = 3; i < Input.Count(); i++){
            if (Input[i] == ""){
                curMap = curMap.OrderBy(m => m.Source).ToList();
                maps.Add(curMap);
                curMap = new List<Map>();
                i++;
            }
            else{
                string[] line = Input[i].Split(" ");
                long source = long.Parse(line[1]);
                long dest = long.Parse(line[0]);
                long length = long.Parse(line[2]);
                curMap.Add(new Map(source, dest - source, length));
            }
        }
        curMap = curMap.OrderBy(m => m.Source).ToList();
        maps.Add(curMap);

        return maps;
    }

    static void Part1(){
        List<long> seeds = SeparateSeeds();
        List<List<Map>> maps = GenerateMaps();

        long maxLowest = 10000000000000;
        for (int h = 0; h < seeds.Count(); h++){
            long s = seeds[h];
            for (int i = 0; i < maps.Count(); i++){
                for (int j = 0; j < maps[i].Count(); j++){
                    long diff = maps[i][j].CheckMap(s);
                    if (diff != 0){
                        s = diff;
                        break;
                    }
                }
            }

            if (s < maxLowest){
                maxLowest = s;
            }
        }

        Console.WriteLine(maxLowest);
    }

    static void Part2(){
        List<long> seeds = SeparateSeeds();
        List<List<Map>> maps = GenerateMaps();

        long maxLowest = 10000000000000;
        for (int g = 0; g < seeds.Count(); g += 2){
            List<Range> ranges = new List<Range>();
            ranges.Add(new Range(seeds[g], seeds[g] + seeds[g + 1]));

            foreach (List<Map> l in maps){
                ranges = CheckMapRange(ranges, l);
            }

            ranges = ranges.OrderBy(l => l.Start).ToList();

            if (ranges[0].Start < maxLowest){
                maxLowest = ranges[0].Start;
            }
        }
        Console.WriteLine(maxLowest);
    }

    static List<Range> CheckMapRange(List<Range> ranges, List<Map> maps){
        List<Range> newRanges = new List<Range>();

        foreach (Range r in ranges){
            foreach (Map m in maps){
                //Check if Range is completly in Map
                if (r.Start >= m.Source && r.Start < (m.Source + m.Length) && r.End > m.Source && r.End < (m.Source + m.Length)){
                    newRanges.Add(new Range(r.Start + m.Diff, r.End + m.Diff));
                    r.End = r.Start;
                    break;
                }

                //Range starts left from Map
                if (r.Start < m.Source && r.End > m.Source && r.End < m.Source + m.Length){
                    newRanges.Add(new Range(m.Source + m.Diff, r.End + m.Diff));
                    r.End = m.Source;
                }

                //Range end right from Map
                if (r.Start >= m.Source && r.Start < m.Source + m.Length && r.End >= m.Source + m.Length){
                    newRanges.Add(new Range(r.Start + m.Diff, m.Source + m.Length - 1 + m.Diff));
                    r.Start = m.Source + m.Length;
                }
            }
            if (r.Start - r.End < 0){
                newRanges.Add(r);
            }
        }

        return newRanges;
    }

    //Part 1: 525792406
    //Part 2: 79004094

    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}

class Range{
    public long Start;
    public long End;

    public Range(long start, long end){
        this.Start = start;
        this.End = end;
    }

    public void PrintRange(){
        Console.WriteLine(Start.ToString() + " -> " + End.ToString());
    }
}

class Map{
    public long Source;
    public long Diff;
    public long Length;

    public Map(long source, long diff, long length){
        Source = source;
        Diff = diff;
        Length = length;
    }

    public void PrintMap(){
        Console.WriteLine(Source.ToString() + ", " + Length.ToString() + ", " + Diff.ToString());
    }

    public long CheckMap(long seed){
        if (Source <= seed && seed < Source + Length){
            return seed + Diff;
        }
        else{
            return 0;
        }
    }
}
