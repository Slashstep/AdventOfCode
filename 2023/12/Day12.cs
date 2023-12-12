using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Diagnostics;

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

        int[] nums = new int[Right.Length];
        for (int i = 0; i < nums.Length; i++){
            nums[i] = int.Parse(Right[i]);
        }

        HotSpringRow hsr = new HotSpringRow(leftRight[0], nums);
        return hsr;
    }

    static HotSpringRow GetUnfoldedRow(int index){
        string[] leftRight = Input[index].Split(" ");
        string[] Right = leftRight[1].Split(",");
        string left = leftRight[0] + "?" + leftRight[0] + "?" + leftRight[0] + "?" + leftRight[0] + "?" + leftRight[0];

        int[] nums = new int[Right.Length * 5];
        for (int i = 0; i < Right.Length; i++){
            for (int j = 0; j < 5; j++){
                nums[Right.Length * j + i] = int.Parse(Right[i]);
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
            counter += hsr.CheckString("", 0);
        }

        Console.WriteLine(counter);
    }

    static void Part2(){
        long counter = 0;
        HotSpringRow hsr;
        for(int i = 0; i < Input.Count; i++){
            hsr = GetUnfoldedRow(i);
            counter += hsr.CheckString("", 0);
        }

        Console.WriteLine(counter);
    }

    //Part 1: 7286
    //Part 2: 25470469710341 goal
              
    public static void Main(string[] args){
        Input = ReadFile();
        //Part1();
        Part2();
    }
}

class HotSpringRow{
    public string Source;
    public int[] Arrangement;
    public int Possabilities;
    Dictonary<string, int> cache;

    public HotSpringRow(string source, int[] arrangement){
        Source = source;
        Arrangement = arrangement;
        Possabilities = 0;
        cache = new Dictonary<string, int>();
    }

    public long CheckString(string permutation, int index){
        if (index == Source.Length){
            if (IsArrangementValid(permutation))
                return 1;
            else
                return 0;
        }

        long counter = 0;
        if (Source[index] =='.' || Source[index] == '#'){
            if (IsUnfinishedArrangementValid(permutation + Source[index]))
                counter = CheckString(permutation + Source[index], ++index);
        }
        else{
            if (IsUnfinishedArrangementValid(permutation + "."))
                counter = CheckString(permutation + ".", index + 1);
            if (IsUnfinishedArrangementValid(permutation + "#"))
                counter = counter + CheckString(permutation + "#", index + 1);
        }

        return counter;
    }

    public int CheckUnfinishedPermutations(){
        List<string> permutations = new List<string>();
        permutations.Add("");

        for (int i = 0; i < Source.Length; i++){
            if (Source[i] == '.' || Source[i] == '#'){
                for (int j = 0; j < permutations.Count; j++){
                    permutations[j] += Source[i];
                }
            }
            // ?
            else{
                List<string> newPerm = new List<string>();
                for (int j = 0; j < permutations.Count; j++){
                    if (IsUnfinishedArrangementValid(permutations[j] + ".")) newPerm.Add(permutations[j] + ".");
                    if (IsUnfinishedArrangementValid(permutations[j] + "#")) newPerm.Add(permutations[j] + "#");
                }
                newPerm.RemoveAll(p => p.Length == i);
                permutations = newPerm;
            }
        }

        int counter = 0;
        foreach (string s in permutations){
            if (IsArrangementValid(s)) counter++;
        }

        return counter;
    }

    bool IsUnfinishedArrangementValid(string pos){
        bool isWaitingForDot = false;
        int arrangementIndex = -1;
        int arrangementAmount = 0;
        for (int i = 0; i < pos.Length; i++){
            if (pos[i] == '#'){
                //start new sequence
                if (arrangementAmount == 0 && !isWaitingForDot){
                    arrangementIndex++;
                    
                    if (arrangementIndex == Arrangement.Length) return false;
                    arrangementAmount = Arrangement[arrangementIndex] - 1;
                    isWaitingForDot = true;
                }
                //# after sequence End
                else if (arrangementAmount == 0 && isWaitingForDot) return false;
                else {
                    arrangementAmount--;
                }
            }
            else{
                if (arrangementAmount > 0) return false;

                isWaitingForDot = false;
            }
        }

        return true;
    }

    bool IsArrangementValid(string pos){
        bool isWaitingForDot = false;
        int arrangementIndex = -1;
        int arrangementAmount = 0;
        for (int i = 0; i < pos.Length; i++){
            if (pos[i] == '#'){
                //start new sequence
                if (arrangementAmount == 0 && !isWaitingForDot){
                    arrangementIndex++;
                    
                    if (arrangementIndex == Arrangement.Length) return false;
                    arrangementAmount = Arrangement[arrangementIndex] - 1;
                    isWaitingForDot = true;
                }
                //# after sequence End
                else if (arrangementAmount == 0 && isWaitingForDot) return false;
                else {
                    arrangementAmount--;
                }
            }
            else{
                if (arrangementAmount > 0) return false;

                isWaitingForDot = false;
            }
        }

        if (arrangementIndex != Arrangement.Length - 1) return false;
        if (arrangementAmount > 0) return false;

        return true;
    }
}
