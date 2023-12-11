using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

class Day13{

    static public List<string> Input = new List<string>();
    static Vector2Int start = new Vector2Int(0,0);
    static List<Vector2Int> loop = new List<Vector2Int>();

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

    static HashSet<Vector2Int> FindVectorsFromInput(){
        HashSet<Vector2Int> vecs = new HashSet<Vector2Int>();

        for (int i = 0; i < Input.Count; i++){
            if (Input[i] == "") break;

            string[] coordinats = Input[i].Split(",");
            vecs.Add(new Vector2Int(int.Parse(coordinats[0]), int.Parse(coordinats[1])));
        }

        return vecs;
    }


    static void Part1(){
        HashSet<Vector2Int> Points = FindVectorsFromInput();

        //Fold on first line
        int foldIndex = Points.Count + 1;
        string[] fold = Input[foldIndex].Split(" ")[2].Split("=");
        HashSet<Vector2Int> newPoints = new HashSet<Vector2Int>(new VectorComparer());
        if (fold[0] == "x"){
            foreach(Vector2Int v in Points){
                newPoints.Add(v.FoldX(int.Parse(fold[1])));
            }
        }
        else{
            foreach(Vector2Int v in Points){
                newPoints.Add(v.FoldY(int.Parse(fold[1])));
            }
        }

        Console.WriteLine(newPoints.Count);
    }

    static void Part2(){
        HashSet<Vector2Int> Points = FindVectorsFromInput();

        //Fold on first line
        int foldIndex = Points.Count + 1;
        
        for (int i = foldIndex; i < Input.Count; i++){
            string[] fold = Input[i].Split(" ")[2].Split("=");
            HashSet<Vector2Int> newPoints = new HashSet<Vector2Int>(new VectorComparer());
            if (fold[0] == "x"){
                foreach(Vector2Int v in Points){
                    newPoints.Add(v.FoldX(int.Parse(fold[1])));
                }
            }
            else{
                foreach(Vector2Int v in Points){
                    newPoints.Add(v.FoldY(int.Parse(fold[1])));
                }
            }
            Points = newPoints;
        }

        for (int i = 0; i < 7; i++){
            string s = "";
            for (int j = 0; j < 50; j++){
                if (Points.Contains(new Vector2Int(j, i))) s+= "#";
                else s += ".";
            }
            Console.WriteLine(s);
        }

    }

    //Part 1: 755
    //Part 2: BLKJRBAG
              
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}

class Vector2Int{
    public int X;
    public int Y;

    public Vector2Int(int x, int y){
        X = x;
        Y = y;
    }

    public void PrintVector(){
        Console.WriteLine("(" + X.ToString() + ", " + Y.ToString() + ")");
    }

    public Vector2Int FoldX(int line){
        if (X <= line) return this;

        int dist = X - line;
        return new Vector2Int(X - 2 * dist, Y);
    }

    public Vector2Int FoldY(int line){
        if (Y <= line) return this;

        int dist = Y - line;
        return new Vector2Int(X, Y - 2 * dist);
    }
}

class VectorComparer : IEqualityComparer<Vector2Int>
{
    public bool Equals(Vector2Int first, Vector2Int second)
    {
        return first.X == second.X && first.Y == second.Y;
    }
 
    public int GetHashCode(Vector2Int obj)
    {
        int hash = 17; // Choose prime numbers as initial values
        hash = hash * 23 + obj.X.GetHashCode();
        hash = hash * 23 + obj.Y.GetHashCode();
        return hash;
    }
}
