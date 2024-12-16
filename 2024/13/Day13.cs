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
using System.Diagnostics.CodeAnalysis;

class Day13{
    static public List<string> Input = new List<string>();
    static public List<ClawMashine> Mashines = new List<ClawMashine>();

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

    static void FillClaws(bool isPart2){
        Mashines = new List<ClawMashine>();
        ClawMashine cM;
        (long, long) buttonA = (0, 0);
        (long, long) buttonB = (0, 0);
        (long, long) prize = (0, 0);

        for (int i = 0; i < Input.Count(); i++){
            if (i%4 == 3){
                cM = new ClawMashine(buttonA, buttonB, prize);
                Mashines.Add(cM);
            }
            else if (i%4 == 0){
                string[] s = Input[i].Split(' ');
                long x = Convert.ToInt64(s[2].Substring(2, s[2].Length - 3));
                long y = Convert.ToInt64(s[3].Substring(2, s[3].Length - 2));
                buttonA = (x, y);
            }
            else if (i%4 == 1){
                string[] s = Input[i].Split(' ');
                long x = Convert.ToInt64(s[2].Substring(2, s[2].Length - 3));
                long y = Convert.ToInt64(s[3].Substring(2, s[3].Length - 2));
                buttonB = (x, y);
            }
            else if (i%4 == 2){
                string[] s = Input[i].Split(' ');
                long x = Convert.ToInt64(s[1].Substring(2, s[1].Length - 3));
                long y = Convert.ToInt64(s[2].Substring(2, s[2].Length - 2));
                if (isPart2)
                    prize = (10000000000000 + x, 10000000000000 + y);
                else
                    prize = (x, y);
            }
        }

        cM = new ClawMashine(buttonA, buttonB, prize);
        Mashines.Add(cM);
    }

    static long CheckPrize(ClawMashine cM, bool isPart2){
        //x_1 * A_x + x_2 * B_x = X
        //x_1 * A_y + x_2 * B_y = Y
        //x_1 = (Y - x_2 * B_y) / A_y
        //((Y - x_2 * B_y) / A_y) * A_x + x_2 * B_x = X
        //A_x * (Y - x_2 * B_y) + A_y * B_x * x_2 = X * A_y
        //A_x * Y - A_x * B_y * x_2 + A_y * B_x * x_2 = X
        //- A_x * B_y * x_2 + A_y * B_x * x_2 = X * A_y - A_x * Y
        //x_2 * (- A_x * B_y + A_y * B_x) = X * A_y - A_x * Y
        //x_2 = (X * A_y - A_x * Y) / (- A_x * B_y + A_y * B_x)

        long f = (cM.ButtonA.y * cM.ButtonB.x - cM.ButtonA.x * cM.ButtonB.y);
        if (f == 0) return 0;
        long x2 = (cM.Prize.x * cM.ButtonA.y - cM.ButtonA.x * cM.Prize.y) / f;

        if (cM.ButtonA.y == 0) return 0;
        long x1 = (cM.Prize.y - x2 * cM.ButtonB.y) / cM.ButtonA.y;

        //check if x1 and x2 < 100
        if (!isPart2 && (x1 > 100 || x2 > 100)) return 0;

        //check if x1 and x2 really get result
        long resX = x1 * cM.ButtonA.x + x2 * cM.ButtonB.x;
        long resY = x1 * cM.ButtonA.y + x2 * cM.ButtonB.y;

        if (resX == cM.Prize.x && resY == cM.Prize.y)
            return x1 * 3 + x2;
        else
            return 0;
    }

    static void Part1(){
        FillClaws(false);

        long sum = 0;
        foreach (ClawMashine cM in Mashines){
            sum += CheckPrize(cM, false);
        }

        Console.WriteLine(sum);
    }

    static void Part2(){
        FillClaws(true);

        long sum = 0;
        foreach (ClawMashine cM in Mashines){
            sum += CheckPrize(cM, true);
        }

        Console.WriteLine(sum);
    }

    //Part 1: 29388
    //Part 2: 99548032866004
    
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}

class ClawMashine{
    public (long x, long y) ButtonA;
    public (long x, long y) ButtonB;
    public (long x, long y) Prize;

    public ClawMashine((long, long) bA, (long, long) bB, (long, long) prize){
        ButtonA = bA;
        ButtonB = bB;
        Prize = prize;
    }

    public void PrintMashine(){
        Console.WriteLine("A: " + ButtonA.ToString() + ", B: " + ButtonB.ToString() + ", Prize: " + Prize.ToString());
    }
}