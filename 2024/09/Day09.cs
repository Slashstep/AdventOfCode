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

class Day09{
    static public List<string> Input = new List<string>();
    static public Stack<Data> Datas = new Stack<Data>();
    static public Queue<long> Spaces = new Queue<long>();
    static public List<Data> SortedDatas = new List<Data>();

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

    static void ReadData(){
        Datas = new Stack<Data>();
        Spaces = new Queue<long>();
        SortedDatas = new List<Data>();

        long posCount = 0;
        for (int i = 0; i < Input[0].Length; i++){
            long l = Convert.ToInt64(Input[0][i].ToString());
            if (i%2 == 0){
                Data newData = new Data(i/2, posCount, l);
                Datas.Push(newData);
                posCount += l;
            }
            else{
                for (int j = 0; j < l; j++)
                    Spaces.Enqueue(posCount++);
            }
        }
    }

    static void Part1(){
        ReadData();

        while(Spaces.Count() > 0 && Datas.Count() > 0){
            Data curData = Datas.Pop();

            for (int i = curData.Positions.Count() - 1; i >= 0; i--){
                if (Spaces.Peek() > curData.Positions[i]) break;
                if (Spaces.Count() <= 0) break;
                curData.Positions[i] = Spaces.Dequeue();
            }

            SortedDatas.Add(curData);
        }

        while (Datas.Count() > 0){
            SortedDatas.Add(Datas.Pop());
        }

        long sum = 0;
        foreach (Data d in SortedDatas){
            foreach (long i in d.Positions){
                sum += d.ID * i;
            }
        }

        Console.WriteLine(sum);
    }

    static void Part2(){
        ReadData();

        while(Spaces.Count() > 0 && Datas.Count() > 0){
            Data curData = Datas.Pop();

            for (int i = curData.Positions.Count() - 1; i >= 0; i--){
                if (Spaces.Peek() > curData.Positions[i]) break;
                if (Spaces.Count() <= 0) break;
                curData.Positions[i] = Spaces.Dequeue();
            }

            SortedDatas.Add(curData);
        }

        while (Datas.Count() > 0){
            SortedDatas.Add(Datas.Pop());
        }

        long sum = 0;
        foreach (Data d in SortedDatas){
            foreach (long i in d.Positions){
                sum += d.ID * i;
            }
        }

        Console.WriteLine(sum);
    }

    //Part 1: 6415184586041
    //Part 2: 
    
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}

class Data{
    public long ID;
    public List<long> Positions = new List<long> ();

    public Data(long id, long p, long l){
        ID = id;
        for (int i = 0; i < l; i++)
            Positions.Add(p++);
    }
}