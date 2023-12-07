using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Day10{

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
        string openings = "([{<";
        string closings = ")]}>";
        for(int i = 0; i < Input.Count; i++){
            string solution = "";
            for (int j = 0; j < Input[i].Length; j++){
                int openIndex = openings.IndexOf(Input[i][j]);
                if (openIndex >= 0){
                    solution += closings[openIndex];
                }
                else if (closings.Contains(Input[i][j])){
                    if (Input[i][j] == solution[solution.Length - 1]){
                        solution = solution.Remove(solution.Length - 1);
                    }
                    else{
                        switch (Input[i][j].ToString()){
                            case ")":
                                counter += 3;
                                break;
                            case "]":
                                counter += 57;
                                break;
                            case "}":
                                counter += 1197;
                                break;
                            case ">":
                                counter += 25137;
                                break;
                            default:
                                break;
                        }
                        break;
                    }
                }
            }
        }
        Console.WriteLine(counter);
    }

    static void Part2(){
        List<long> vals = new List<long>();
        
        string openings = "([{<";
        string closings = ")]}>";
        bool isValid = true;
        for(int i = 0; i < Input.Count; i++){
            string solution = "";
            isValid = true;
            long counter = 0;
            for (int j = 0; j < Input[i].Length; j++){
                int openIndex = openings.IndexOf(Input[i][j]);
                if (openIndex >= 0){
                    solution += closings[openIndex];
                }
                else if (closings.Contains(Input[i][j])){
                    if (Input[i][j] == solution[solution.Length - 1]){
                        solution = solution.Remove(solution.Length - 1);
                    }
                    else{
                        isValid = false;
                        break;
                    }
                }
            }

            if (!isValid){
                continue;
            }

            char[] charArray = solution.ToCharArray();
            Array.Reverse(charArray);
            solution = new string(charArray);

            for (int j = 0; j < solution.Length; j++){
                switch (solution[j].ToString()){
                    case ")":
                        counter *= 5;
                        counter += 1;
                        break;
                    case "]":
                        counter *= 5;
                        counter += 2;
                        break;
                    case "}":
                        counter *= 5;
                        counter += 3;
                        break;
                    case ">":
                        counter *= 5;
                        counter += 4;
                        break;
                    default:
                        break;
                }
            }
            vals.Add(counter);
        }

        vals.Sort();
        int index = (int)Math.Round((decimal)(vals.Count / 2), MidpointRounding.AwayFromZero);
        Console.WriteLine(vals[index]);

    }

    //Part 1: 318081
    //Part 2: 4361305341
              
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}
