using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Globalization;

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

    static void Part1(){
        List<(int, int, int)> dangerousPoints = new List<(int, int, int)>();

        for (int i = 0; i < Input.Count(); i++){
            string[] startEnd = Input[i].Split(" -> ");
            string[] start = startEnd[0].Split(",");
            string[] end = startEnd[1].Split(",");

            Vent newVent = new Vent(int.Parse(start[0]), int.Parse(start[1]), int.Parse(end[0]), int.Parse(end[1]));

            if (!newVent.IsHorizontalOrVertical())
                continue;
            
            foreach ((int, int) point in newVent.PointsInVent()){
                int index = dangerousPoints.IndexOf(dangerousPoints.Find(p => p.Item1 == point.Item1 && p.Item2 == point.Item2));
                
                if (index == -1){
                    dangerousPoints.Add((point.Item1, point.Item2, 1));
                }
                else{
                    dangerousPoints[index] = (point.Item1, point.Item2, dangerousPoints[index].Item3 + 1);
                }
            }
        }

        int counter = 0;
        for(int i = 0; i < dangerousPoints.Count(); i++){
            if (dangerousPoints[i].Item3 >= 2)
                counter++;
        }

        Console.WriteLine(counter);
    }

    static void Part2(){
        List<(int, int, int)> dangerousPoints = new List<(int, int, int)>();

        for (int i = 0; i < Input.Count(); i++){
            string[] startEnd = Input[i].Split(" -> ");
            string[] start = startEnd[0].Split(",");
            string[] end = startEnd[1].Split(",");

            Vent newVent = new Vent(int.Parse(start[0]), int.Parse(start[1]), int.Parse(end[0]), int.Parse(end[1]));

            if (newVent.IsHorizontalOrVertical()){
                foreach ((int, int) point in newVent.PointsInVent()){
                    int index = dangerousPoints.IndexOf(dangerousPoints.Find(p => p.Item1 == point.Item1 && p.Item2 == point.Item2));
                    
                    if (index == -1){
                        dangerousPoints.Add((point.Item1, point.Item2, 1));
                    }
                    else{
                        dangerousPoints[index] = (point.Item1, point.Item2, dangerousPoints[index].Item3 + 1);
                    }
                }
            }
            else{
                foreach ((int, int) point in newVent.DiagonalVents()){
                    int index = dangerousPoints.IndexOf(dangerousPoints.Find(p => p.Item1 == point.Item1 && p.Item2 == point.Item2));
                    
                    if (index == -1){
                        dangerousPoints.Add((point.Item1, point.Item2, 1));
                    }
                    else{
                        dangerousPoints[index] = (point.Item1, point.Item2, dangerousPoints[index].Item3 + 1);
                    }
                }
            }
            
        }

        int counter = 0;
        for(int i = 0; i < dangerousPoints.Count(); i++){
            if (dangerousPoints[i].Item3 >= 2)
                counter++;
        }

        Console.WriteLine(counter);
    }

    //Part 1: 5373
    //Part 2: 21514
    public static void Main(string[] args){
        Input = ReadFile();
        //Part1();
        Part2();
    }
}

class Vent{
    public int StartX;
    public int StartY;
    public int EndX;
    public int EndY;

    public Vent(int startX, int startY, int endX, int endY){
        StartX = startX;
        StartY = startY;
        EndX = endX;
        EndY = endY;
    }

    public bool IsHorizontalOrVertical(){
        if (StartX == EndX || StartY == EndY)
            return true;
        else
            return false;
    }

    public List<(int, int)> PointsInVent(){
        List<(int, int)> points = new List<(int,int)> ();

        for (int x = (int)MathF.Min(StartX, EndX); x <= (int)MathF.Max(StartX, EndX); x++){
            for (int y = (int)MathF.Min(StartY, EndY); y <= (int)MathF.Max(StartY, EndY); y++){
                points.Add((x,y));
            }
        }

        return points;
    }

    public List<(int, int)> DiagonalVents(){
        List<(int, int)> points = new List<(int,int)> ();

        int difX = EndX - StartX;
        int difY = EndY - StartY;

        //right up
        if (difX > 0 && difY > 0){
            for (int i = 0; i <= difX; i++)
                points.Add((StartX + i, StartY + i));
            return points;
        }
        //left up
        else if (difX < 0 && difY > 0){
            for (int i = 0; i <= difY; i++)
                points.Add((StartX - i, StartY + i));
            return points;
        }
        //right down
        else if (difX > 0 && difY < 0){
            for (int i = 0; i <= difX; i++)
                points.Add((StartX + i, StartY - i));
            return points;
        }
        //left down
        else{
            for (int i = 0; i >= difX; i--)
                points.Add((StartX + i, StartY + i));
            return points;
        }
    }
}
