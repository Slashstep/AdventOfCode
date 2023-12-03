using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;

class Day03{

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
        string minBin = "";
        string maxBin = "";

        for (int i = 0; i < Input[0].Length; i++){
            int count0 = 0;
            int count1 = 0;
            for (int j = 0; j < Input.Count(); j++){
                if (Input[j][i].ToString() == "0")
                    count0++;
                else
                    count1++;
            }

            if (count0 < count1){
                minBin += "0";
                maxBin += "1";
            }
            else{
                minBin += "1";
                maxBin += "0";
            }
        }

        Console.WriteLine(Convert.ToInt32(minBin, 2) * Convert.ToInt32(maxBin, 2));
    }

    static (string, string) CheckBits(List<string> input, int position){
        int count0 = 0;
        int count1 = 0;

        for (int j = 0; j < input.Count(); j++){
            if (input[j][position].ToString() == "0")
                count0++;
            else
                count1++;
        }

        if (count0 <= count1){
            return (("0", "1"));
        }
        else{
            return (("1", "0"));
        }
    }

    static void Part2(){
        List<string> curInput = new List<string>(Input);
        int index = -1;

        //Get maximum entries
        while (curInput.Count() > 1){
            index++;
            (string, string) maxBits = CheckBits(curInput, index);

            List<string> tempInput = new List<string>();
            for(int i = 0; i < curInput.Count(); i++){
                if (curInput[i][index].ToString() == maxBits.Item2){
                    tempInput.Add(curInput[i]);
                }
            }
            curInput = tempInput;
        }

        string maxBin = curInput[0];

        //Get minimum entries
        curInput = new List<string>(Input);
        index = -1;
        while (curInput.Count() > 1){
            index++;
            (string, string) maxBits = CheckBits(curInput, index);

            List<string> tempInput = new List<string>();
            for(int i = 0; i < curInput.Count(); i++){
                if (curInput[i][index].ToString() == maxBits.Item1)
                    tempInput.Add(curInput[i]);
            }
            curInput = tempInput;
        }

        string minBin = curInput[0];

        Console.WriteLine(Convert.ToInt32(minBin, 2) * Convert.ToInt32(maxBin, 2));
    }

    //Part 1: 3277364
    //Part 2: 5736383
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}
