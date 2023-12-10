using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

class Day10{

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

    static Vector2Int FindStart(){
        for (int i = 0; i < Input.Count; i++){
            for (int j = 0; j < Input[i].Length; j++){
                if (Input[i][j].ToString() == "S"){
                    return new Vector2Int(j, i);
                }
            }
        }
        
        return new Vector2Int(0, 0);
    }

    static void FindLoop(){
        start = FindStart();
        HashSet<int> val = new HashSet<int>();
        List<Vector2Int> vecsToCheck = new List<Vector2Int>();
        vecsToCheck.Add(start.left);
        vecsToCheck.Add(start.right);
        vecsToCheck.Add(start.up);
        vecsToCheck.Add(start.down);

        foreach (Vector2Int v in vecsToCheck){
            loop = new List<Vector2Int>();
            loop.Add(start);
            loop = GetLoopPositions(v, 1, loop);
            if (loop.Count > 0 && !val.Add(loop.Count)){
                break;
            }
        }
    }

    static Vector2Int VectorToCheck(Vector2Int from, List<Vector2Int> visited, int counter){
        switch (Input[from.Y][from.X].ToString()){
            case "-":
                if (CheckStart(from.left, counter)) return from.left;
                else if (CheckStart(from.right, counter)) return from.right;
                if (IsVectorInList(from.left, visited)) return from.right;
                else return from.left;
            case "|":
                if (CheckStart(from.up, counter)) return from.up;
                else if (CheckStart(from.down, counter)) return from.down;
                if (IsVectorInList(from.up, visited)) return from.down;
                else return from.up;
            case "L":
                if (CheckStart(from.up, counter)) return from.up;
                else if (CheckStart(from.right, counter)) return from.right;
                if (IsVectorInList(from.up, visited)) return from.right;
                else return from.up;
            case "F":
                if (CheckStart(from.down, counter)) return from.down;
                else if (CheckStart(from.right, counter)) return from.right;
                if (IsVectorInList(from.right, visited)) return from.down;
                else return from.right;
            case "7":
                if (CheckStart(from.left, counter)) return from.left;
                else if (CheckStart(from.down, counter)) return from.down;
                if (IsVectorInList(from.down, visited)) return from.left;
                else return from.down;
            case "J":
                if (CheckStart(from.left, counter)) return from.left;
                else if (CheckStart(from.up, counter)) return from.up;
                if (IsVectorInList(from.left, visited)) return from.up;
                else return from.left;
            default:
                return from;
        }
    }

    static bool CheckStart(Vector2Int from, int counter){
        if (Input[from.Y][from.X].ToString() == "S" && counter > 2){
            return true;
        }
        return false;
    }

    static bool IsVectorInList(Vector2Int vec, List<Vector2Int> list){
        Vector2Int? v = list.Find(v => v.X == vec.X && v.Y == vec.Y);

        if (v == null){
            return false;
        }

        return true;
    }

    static List<Vector2Int> GetLoopPositions(Vector2Int from, int counter, List<Vector2Int> visited){
        if (from.X < 0 || from.X >= Input[0].Length || from.Y < 0 || from.Y >= Input.Count){
            return new List<Vector2Int>();
        }

        visited.Add(new Vector2Int(from.X, from.Y));

        if (Input[from.Y][from.X].ToString() == "S" && counter != 1){
            return visited;
        }

        Vector2Int next = VectorToCheck(from, visited, counter);
        if (next == from){
            return new List<Vector2Int>();
        }
        
        return GetLoopPositions(next, ++counter, visited);
    }

    static void Part1(){
        FindLoop();
        Console.WriteLine(loop.Count / 2);
    }

    static void Part2(){
        int counter = 0;
        bool isOpen = false;
        for (int i = 0; i < Input.Count - 1; i++){
            for (int j = 0; j < Input[i].Length; j++){
                if (isOpen){
                    if (IsVectorInList(new Vector2Int(j, i), loop)) {
                        if ((Input[i][j] == 'S' || Input[i][j] == 'F' || Input[i][j] == '|' || Input[i][j] == '7') && 
                            (Input[i + 1][j] == 'S' || Input[i + 1][j] == '|' || Input[i + 1][j] == 'J' || Input[i + 1][j] == 'L')){
                            isOpen = false;
                        }
                    }
                    else{
                        counter++;
                    }
                }
                else{
                    if (IsVectorInList(new Vector2Int(j, i), loop)) {
                        if ((Input[i][j] == 'S' || Input[i][j] == 'F' || Input[i][j] == '|' || Input[i][j] == '7') && 
                            (Input[i + 1][j] == 'S' || Input[i + 1][j] == '|' || Input[i + 1][j] == 'J' || Input[i + 1][j] == 'L')){
                            isOpen = true;
                        }
                    }
                }
            }
            isOpen = false;
        }

        Console.WriteLine(counter);
    }

    //Part 1: 7066
    //Part 2: 401 goal
              
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
    public Vector2Int left{
        get{return new Vector2Int(X - 1, Y);}
    }
    public Vector2Int right{
        get{return new Vector2Int(X + 1, Y);}
    }
    public Vector2Int up{
        get{return new Vector2Int(X, Y - 1);}
    }
    public Vector2Int down{
        get{return new Vector2Int(X, Y + 1);}
    }
}
