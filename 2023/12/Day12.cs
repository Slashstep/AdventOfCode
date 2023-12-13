using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Diagnostics;
using Microsoft.Extensions.Caching.Memory;

/*
Credits to https://github.com/LiquidFun
*/

class Day12{

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

    static HotSpringRow GetRow(int index){
        string[] leftRight = Input[index].Split(" ");
        string[] Right = leftRight[1].Split(",");

        Queue<int> nums = new Queue<int>();
        for (int i = 0; i < nums.Count; i++){
            nums.Enqueue(int.Parse(Right[i]));
        }

        HotSpringRow hsr = new HotSpringRow(leftRight[0], nums);
        return hsr;
    }

    static HotSpringRow GetUnfoldedRow(int index){
        string[] leftRight = Input[index].Split(" ");
        string[] Right = leftRight[1].Split(",");
        string left = leftRight[0] + "?" + leftRight[0] + "?" + leftRight[0] + "?" + leftRight[0] + "?" + leftRight[0];

        Queue<int> nums = new Queue<int>();
        for (int i = 0; i < 5; i++){
            for (int j = 0; j < Right.Length; j++){
                nums.Enqueue(int.Parse(Right[j]));
            }
        }

        HotSpringRow hsr = new HotSpringRow(left, nums);
        return hsr;
    }

    static void Part1(){
        long counter = 0;
        HotSpringRow hsr;
        for(int i = 0; i < Input.Count; i++){
            hsr = GetRow(i);
            //counter += hsr.CheckString("", 0);
        }

        Console.WriteLine(counter);
    }

    static void Part2(){
        var watch = new Stopwatch();
        watch.Start();

        long counter = 0;
        HotSpringRow hsr;
        for(int i = 0; i < Input.Count; i++){
            hsr = GetUnfoldedRow(i);
            counter += hsr.CheckStringCache(hsr.Source, hsr.Arrangement, 0);
        }

        Console.WriteLine(counter);
        watch.Stop();
        Console.WriteLine(watch.Elapsed);
    }

    //Part 1: 7286
    //Part 2: 25470469710341
              
    public static void Main(string[] args){
        Input = ReadFile();
        //Part1();
        Part2();
    }
}

class HotSpringRow{
    public string Source;
    public Queue<int> Arrangement;
    public int Possabilities;
    Dictionary<string, long> cache;
    //public IMemoryCache cache;

    public HotSpringRow(string source, Queue<int> arrangement){
        Source = source;
        Arrangement = arrangement;
        Possabilities = 0;
        cache = new Dictionary<string, long>();
        //cache = new MemoryCache(new MemoryCacheOptions());
    }

    private static string GetCacheKey((string, int, int) key)
    {
        return key.Item1 + key.Item2.ToString() + key.Item3.ToString();
    }

    public long CheckStringCache(string permutation, Queue<int> nums, int curHash){
        (string, int, int) newTup = (permutation, nums.Count, curHash);
        if (cache.TryGetValue(GetCacheKey(newTup), out long value)) return value;
        //if (cache.ContainsKey(newTup)) return cache[newTup];

        if (permutation.Length == 0){
            if (nums.Count == 0 && curHash == 0) return 1;
            else if (nums.Count == 1 && nums.Peek() == curHash) return 1;
            else return 0;
        }

        long counter = 0;
        if (permutation[0] == '?'){
            counter += CheckStringCache("." + permutation.Substring(1), nums, curHash);
            counter += CheckStringCache("#" + permutation.Substring(1), nums, curHash);
        }

        if (permutation[0] == '#' && nums.Count > 0 && curHash < nums.Peek()){
            counter += CheckStringCache(permutation.Substring(1), nums, curHash + 1);
        }

        if (permutation[0] == '.'){
            if (nums.Count > 0 && curHash == nums.Peek()){
                Queue<int> newQueue = new Queue<int>(nums);
                newQueue.Dequeue();
                counter += CheckStringCache(permutation.Substring(1), newQueue, 0);
            }
            
            if (nums.Count == 0 || curHash == 0){
                counter += CheckStringCache(permutation.Substring(1), nums, 0);
            }
        }

        cache.Add(GetCacheKey(newTup), counter);
        //cache.Set(GetCacheKey(newTup), counter, TimeSpan.FromMinutes(10));
        return counter;
    }

    // public long CheckString(string permutation, int index){
    //     if (index == Source.Length){
    //         if (IsArrangementValid(permutation))
    //             return 1;
    //         else
    //             return 0;
    //     }

    //     long counter = 0;
    //     if (Source[index] =='.' || Source[index] == '#'){
    //         if (IsUnfinishedArrangementValid(permutation + Source[index]))
    //             counter = CheckString(permutation + Source[index], ++index);
    //     }
    //     else{
    //         if (IsUnfinishedArrangementValid(permutation + "."))
    //             counter = CheckString(permutation + ".", index + 1);
    //         if (IsUnfinishedArrangementValid(permutation + "#"))
    //             counter = counter + CheckString(permutation + "#", index + 1);
    //     }

    //     return counter;
    // }

    // public int CheckUnfinishedPermutations(){
    //     List<string> permutations = new List<string>();
    //     permutations.Add("");

    //     for (int i = 0; i < Source.Length; i++){
    //         if (Source[i] == '.' || Source[i] == '#'){
    //             for (int j = 0; j < permutations.Count; j++){
    //                 permutations[j] += Source[i];
    //             }
    //         }
    //         // ?
    //         else{
    //             List<string> newPerm = new List<string>();
    //             for (int j = 0; j < permutations.Count; j++){
    //                 if (IsUnfinishedArrangementValid(permutations[j] + ".")) newPerm.Add(permutations[j] + ".");
    //                 if (IsUnfinishedArrangementValid(permutations[j] + "#")) newPerm.Add(permutations[j] + "#");
    //             }
    //             newPerm.RemoveAll(p => p.Length == i);
    //             permutations = newPerm;
    //         }
    //     }

    //     int counter = 0;
    //     foreach (string s in permutations){
    //         if (IsArrangementValid(s)) counter++;
    //     }

    //     return counter;
    // }

    // bool IsUnfinishedArrangementValid(string pos){
    //     bool isWaitingForDot = false;
    //     int arrangementIndex = -1;
    //     int arrangementAmount = 0;
    //     for (int i = 0; i < pos.Length; i++){
    //         if (pos[i] == '#'){
    //             //start new sequence
    //             if (arrangementAmount == 0 && !isWaitingForDot){
    //                 arrangementIndex++;
                    
    //                 if (arrangementIndex == Arrangement.Count) return false;
    //                 arrangementAmount = Arrangement[arrangementIndex] - 1;
    //                 isWaitingForDot = true;
    //             }
    //             //# after sequence End
    //             else if (arrangementAmount == 0 && isWaitingForDot) return false;
    //             else {
    //                 arrangementAmount--;
    //             }
    //         }
    //         else{
    //             if (arrangementAmount > 0) return false;

    //             isWaitingForDot = false;
    //         }
    //     }

    //     return true;
    // }

    // bool IsArrangementValid(string pos){
    //     bool isWaitingForDot = false;
    //     int arrangementIndex = -1;
    //     int arrangementAmount = 0;
    //     for (int i = 0; i < pos.Length; i++){
    //         if (pos[i] == '#'){
    //             //start new sequence
    //             if (arrangementAmount == 0 && !isWaitingForDot){
    //                 arrangementIndex++;
                    
    //                 if (arrangementIndex == Arrangement.Length) return false;
    //                 arrangementAmount = Arrangement[arrangementIndex] - 1;
    //                 isWaitingForDot = true;
    //             }
    //             //# after sequence End
    //             else if (arrangementAmount == 0 && isWaitingForDot) return false;
    //             else {
    //                 arrangementAmount--;
    //             }
    //         }
    //         else{
    //             if (arrangementAmount > 0) return false;

    //             isWaitingForDot = false;
    //         }
    //     }

    //     if (arrangementIndex != Arrangement.Length - 1) return false;
    //     if (arrangementAmount > 0) return false;

    //     return true;
    // }
}
