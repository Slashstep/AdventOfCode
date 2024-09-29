using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Xml.Serialization;

class Day15{

    static public List<string> Input = new List<string>();
    static public (int, int, int, int) targetArea;

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

    static void LocateTargetArea(){
        string[] splits = Input[0].Split(' ');
        string xRaw = splits[2];
        string yRaw = splits[3];

        string[] xVals = xRaw.Split('=')[1].Split('.');
        int xMin = Convert.ToInt32(xVals[0]);
        int xMax = Convert.ToInt32(xVals[2].Substring(0, xVals[2].Length - 1));

        string[] yVals = yRaw.Split('=')[1].Split('.');
        int yMin = Convert.ToInt32(yVals[0]);
        int yMax = Convert.ToInt32(yVals[2]);

        targetArea = (xMin, xMax, yMin, yMax);
    }

    static bool IsInTargetArea((int, int) position){
        if (position.Item1 < targetArea.Item1) return false;
        if (position.Item1 > targetArea.Item2) return false;
        if (position.Item2 < targetArea.Item3) return false;
        if (position.Item2 > targetArea.Item4) return false;

        return true;
    }

    static (bool, int) FlightCurve((int, int) trajectory){
        (int, int) curPos = (0, 0);
        int maxY = 0;

        while (curPos.Item1 <= targetArea.Item2 && curPos.Item2 >= targetArea.Item3){
            int newX = curPos.Item1 + trajectory.Item1;
            int newY = curPos.Item2 + trajectory.Item2;

            maxY = (int)MathF.Max(maxY, newY);

            curPos = (newX, newY);

            //Change trajactory
            int trajX = 0;
            if (trajectory.Item1 < 0) trajX = trajectory.Item1 + 1;
            else if (trajectory.Item1 > 0) trajX = trajectory.Item1 -1;

            int trajY = trajectory.Item2 - 1;
            trajectory = (trajX, trajY);

            if (IsInTargetArea(curPos)) return (true, maxY);
        }

            return (false, 0);
    }

    static void Part1(){
        int curMax = 0;

        for (int x = 1; x < 1000; x++){
            for (int y = -1000; y < 1000; y++){
                (bool, int) result = FlightCurve((x, y));

                if (result.Item1) curMax = (int)MathF.Max(curMax, result.Item2);
            }
        }

        Console.WriteLine(curMax);
    }

    static void Part2(){
        int counter = 0;

        for (int x = 1; x < 1000; x++){
            for (int y = -1000; y < 1000; y++){
                if (FlightCurve((x, y)).Item1) counter++;
            }
        }

        Console.WriteLine(counter);
    }

    //Part 1: 5778
    //Part 2: 2576
              
    public static void Main(string[] args){
        Input = ReadFile();
        LocateTargetArea();
        Part1();
        Part2();
    }
}
