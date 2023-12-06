using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Day09{

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
        int counter = 0;
        for (int i = 0; i < Input.Count(); i++){
            string[] leftRight = Input[i].Split(" | ");
            string[] nums = leftRight[1].Split(" ");

            for (int j = 0; j < nums.Length; j++){
                if (nums[j].Length == 2 || nums[j].Length == 3 || nums[j].Length == 4 || nums[j].Length == 7){
                    counter++;
                }
            }
        }

        Console.WriteLine(counter);
    }

    static void Part2(){

        long counter = 0;
        for (int i = 0; i < Input.Count(); i++){
            string[] leftRight = Input[i].Split(" | ");
            List<string> code = leftRight[0].Split(" ").ToList();
            List<string> nums = leftRight[1].Split(" ").ToList();
            
            string[]numbers = DecryptCode(code);

            int codeNumber = 0;
            for (int j = 0; j < nums.Count(); j++){
                nums[j] = new string (nums[j].OrderBy(c => c).ToArray());

                codeNumber += Array.IndexOf(numbers, nums[j]) * (int)Math.Pow(10, (3-j));
            }

            counter += codeNumber;
        }
        Console.WriteLine(counter);
    }

    static string[] DecryptCode(List<string> code){
        string[] numbers = new string[10];

        //Sort strings alphabetically
        for (int j = 0; j < code.Count(); j++){
            code[j] = new string (code[j].OrderBy(c => c).ToArray());

            //1. Find 1
            if (code[j].Length == 2){
                numbers[1] = code[j];
            }
            //2. Find 7
            else if (code[j].Length == 3){
                numbers[7] = code[j];
            }
            //3. Find 4
            else if (code[j].Length == 4){
                numbers[4] = code[j];
            }
            //4. Find 8
            else if (code[j].Length == 7){
                numbers[8] = code[j];
            }
        }

        code.Remove(numbers[1]);
        code.Remove(numbers[7]);
        code.Remove(numbers[4]);
        code.Remove(numbers[8]);

        //5. Find 3
        foreach(string s in code){
            if (s.Length == 5 && StringContains(s, numbers[1])){
                numbers[3] = s;
            }
        }

        code.Remove(numbers[3]);

        //6. Find 9
        foreach(string s in code){
            if (s.Length == 6 && StringContains(s, numbers[3])){
                numbers[9] = s;
            }
        }

        code.Remove(numbers[9]);

        //7. Find 5
        foreach (string s in code){
            if (s.Length == 5 && StringContains(numbers[9], s)){
                numbers[5] = s;
            }
        }

        code.Remove(numbers[5]);

        //8. Find 2
        //9. Find 6
        foreach(string s in code){
            if (s.Length == 5){
                numbers[2] = s;
            }

            if (s.Length == 6 && StringContains(s, numbers[5])){
                numbers[6] = s;
            }
        }

        code.Remove(numbers[2]);
        code.Remove(numbers[6]);

        //10. Find 0
        numbers[0] = code[0];

        return numbers;
    }

    static bool StringContains(string toCheck, string checker){
        for (int i = 0; i < checker.Length; i++){
            if (!toCheck.Contains(checker[i])){
                return false;
            }
        }

        return true;
    }

    //Part 1: 
    //Part 2: 
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}
