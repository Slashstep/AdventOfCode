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
using System.Globalization;

class Day17{
    static public List<string> Input = new List<string>();
    static public long regA, regB, regC;
    static public List<string> Prog = new List<string>();

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

    static void InitializeProgram(){
        regA = Convert.ToInt32(Input[0].Substring(12, Input[0].Length - 12));
        regB = Convert.ToInt32(Input[1].Substring(12, Input[1].Length - 12));
        regC = Convert.ToInt32(Input[2].Substring(12, Input[2].Length - 12));
        Prog = Input[4].Split(' ')[1].Split(',').ToList();
    }

    static void Division(ref long storage, long combo){
        storage = (long)(regA / Math.Pow(2, combo));
    }

    static void XOR(long compar){
        regB = regB ^ compar;
    }

    static long GetCombo(int val){
        if (val < 4) return val;
        else if (val == 4) return regA;
        else if (val == 5) return regB;
        else if (val == 6) return regC;
        else return -1;
    }

    static void Part1(){
        InitializeProgram();

        for (int i = 0; i < Prog.Count(); i+= 2){
            int operand = Convert.ToInt32(Prog[i+1]);
            switch (Prog[i]){
                case "0":
                    Division(ref regA, GetCombo(operand));
                    break;
                case "1":
                    XOR(operand);
                    break;
                case "2":
                    regB = GetCombo(operand)%8;
                    break;
                case "3":
                    if (regA != 0) i = operand - 2;
                    break;
                case "4":
                    XOR(regC);
                    break;
                case "5":
                    long res = GetCombo(operand)%8;
                    Console.Write(res.ToString() + ",");
                    break;
                case "6":
                    Division(ref regB, GetCombo(operand));
                    break;
                case "7":
                    Division(ref regC, GetCombo(operand));
                    break;
                default:
                    break;
            }
        }
    }

    static void Part2(){
        
        int listIndex = 0;
        for (long j = 1000000000000000; j < 10000000000000000; j++){
            regA = j;
            regB = 0;
            regC = 0;
            string output = "";
            if (j % 100000000 == 0) Console.WriteLine(j);
            for (int i = 0; i < Prog.Count(); i+= 2){
                int operand = Convert.ToInt32(Prog[i+1]);
                switch (Prog[i]){
                    case "0":
                        Division(ref regA, GetCombo(operand));
                        break;
                    case "1":
                        XOR(operand);
                        break;
                    case "2":
                        regB = GetCombo(operand)%8;
                        break;
                    case "3":
                        if (regA != 0) i = operand - 2;
                        break;
                    case "4":
                        XOR(regC);
                        break;
                    case "5":
                        long res = GetCombo(operand)%8;
                        if (res.ToString() != Prog[listIndex]){
                            listIndex = 0;
                            i = Prog.Count();
                        }
                        else listIndex++;

                        output += res.ToString() + ",";
                        break;
                    case "6":
                        Division(ref regB, GetCombo(operand));
                        break;
                    case "7":
                        Division(ref regC, GetCombo(operand));
                        break;
                    default:
                        break;
                }
            }

            if (output.Substring(0, output.Length - 1) == Input[4].Split(' ')[1]){
                Console.WriteLine(j);
                break;
            }
        }
    }

    //Part 1: 6,0,6,3,0,2,3,1,6
    //Part 2: 
    
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}