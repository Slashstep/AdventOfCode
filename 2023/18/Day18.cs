using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

class Day18{

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

    static List<Vector2Int> GetVecs()
    {
        List<Vector2Int> cords = new List<Vector2Int>();

        Vector2Int start = new Vector2Int(0, 0);

        foreach (string s in Input)
        {
            string[] sep = s.Split(' ');
            switch (sep[0])
            {
                case "R":
                    start = new Vector2Int(start.X + int.Parse(sep[1]), start.Y);
                    cords.Add(start);
                    break;
                case "L":
                    start = new Vector2Int(start.X - int.Parse(sep[1]), start.Y);
                    cords.Add(start);
                    break;
                case "U":
                    start = new Vector2Int(start.X, start.Y - int.Parse(sep[1]));
                    cords.Add(start);
                    break;
                case "D":
                    start = new Vector2Int(start.X, start.Y + int.Parse(sep[1]));
                    cords.Add(start);
                    break;
                default:
                    break;
            }
        }

        //cords = cords.OrderBy(v => v.Y).ThenBy(v => v.X).ToList();
        return cords;
    }

    static List<Vector2Int> ConvertVec()
    {
        List<Vector2Int> cords = new List<Vector2Int>();
        Vector2Int start = new Vector2Int(0, 0);

        foreach (string s in Input)
        {
            string[] sep = s.Split(' ');
            int amount = int.Parse(sep[2].Substring(2, 5), System.Globalization.NumberStyles.HexNumber);
            string direction = sep[2].Substring(7, 1);
            switch (direction)
            {
                case "0":
                    start = new Vector2Int(start.X + amount, start.Y);
                    cords.Add(start);
                    break;
                case "2":
                    start = new Vector2Int(start.X - amount, start.Y);
                    cords.Add(start);
                    break;
                case "3":
                    start = new Vector2Int(start.X, start.Y - amount);
                    cords.Add(start);
                    break;
                case "1":
                    start = new Vector2Int(start.X, start.Y + amount);
                    cords.Add(start);
                    break;
                default:
                    break;
            }
        }

        return cords;
    }

    static double GetAllPoints(List<Vector2Int> cords)
    {
        double area = 0;
        double boundry = 0;
        for (int i = 0; i < cords.Count; i++)
        {
            area += cords[i].X * cords[(i + 1) % cords.Count].Y - cords[i].Y * cords[(i + 1) % cords.Count].X;
            boundry += Math.Abs(cords[i].X - cords[(i + 1) % cords.Count].X) + Math.Abs(cords[i].Y - cords[(i + 1) % cords.Count].Y);
        }

        area = area / 2f;

        double inner = area + 1 - boundry / 2f;
        double total = inner + boundry;
        return total;
    }

    static void Part1()
    {
        List<Vector2Int> cords = GetVecs();

        Console.WriteLine(GetAllPoints(cords));
    }

    static void Part2()
    {
        List<Vector2Int> cords = ConvertVec();

        Console.WriteLine(GetAllPoints(cords));
    }

    //Part 1: 45159
    //Part 2: 134549294799713

    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}

class Vector2Int : IEquatable<Vector2Int>
{
    public double X;
    public double Y;

    public Vector2Int(double x, double y){
        X = x;
        Y = y;
    }

    public void PrintVector(){
        Console.WriteLine("(" + X.ToString() + ", " + Y.ToString() + ")");
    }

    public bool Equals(Vector2Int? other)
    {
        if (this.X == other?.X && this.Y == other?.Y) return true;
        else return false;
    }

    public Vector2Int left{
        get{return new Vector2Int((int)X - 1, (int)Y);}
    }
    public Vector2Int right{
        get{return new Vector2Int((int)X + 1, (int)Y);}
    }
    public Vector2Int up{
        get{return new Vector2Int((int)X, (int)Y - 1);}
    }
    public Vector2Int down{
        get{return new Vector2Int((int)X, (int)Y + 1);}
    }
}
