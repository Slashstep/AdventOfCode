using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;

class Day15{

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

    static int[,] CreateGrid()
    {
        int[,] Grid = new int[Input[0].Length, Input.Count];
        for (int i = 0; i < Input.Count; i++){
            for (int j = 0; j < Input[i].Length; j++){
                Grid[j, i] = int.Parse(Input[i][j].ToString());
            }
        }

        return Grid;
    }

    static int[,] ExpandGrid(int[,] G){
        int[,] eGrid = new int[Input[0].Length * 5, Input.Count *5];
        for (int y = 0; y < Input.Count; y++){
            for (int j = 0; j < 5; j++){
                for (int x = 0; x < Input[0].Length; x++){
                    for (int i = 0; i < 5; i++){
                        int nVal = (G[x, y] + i + j);
                        if (nVal >= 10) nVal += 1;
                        nVal = nVal % 10;
                        eGrid[Input[0].Length*i+x, Input.Count*j+y] = nVal;
                    }
                }
            }
        }

        return eGrid;
    }

    static int FindPath((int, int) sNode, (int, int) fNode, int[,] G){
        List<(int, int, int, int)> openSet = new List<(int, int, int, int)>();
        HashSet<(int, int, int, int)> closedSet = new HashSet<(int, int, int, int)>();
        (int, int, int, int) cNode;

        openSet.Add(new (sNode.Item1, sNode.Item2, 0, 0));

        while (openSet.Count > 0){
            openSet = openSet.OrderBy(n => n.Item3 + n.Item4).ToList();
            cNode = openSet[0];
            openSet.RemoveAt(0);

            if (cNode.Item1 == fNode.Item1 && cNode.Item2 == fNode.Item2) return cNode.Item3 + cNode.Item4;

            closedSet.Add(cNode);

            //Add Neighbours to openSet
            for (int i = -1; i < 2; i++){
                for (int j = -1; j < 2; j++){
                    if (MathF.Abs(i) + MathF.Abs(j) > 1) continue;
                    if (i == 0 && j == 0) continue;

                    int nX = cNode.Item1 + i;
                    if (nX < 0 || nX > G.GetUpperBound(0)) continue;

                    int nY = cNode.Item2 + j;
                    if (nY < 0 || nY > G.GetLength(0) - 1) continue;

                    int nG = G[nX, nY] + cNode.Item3;
                    int nH = fNode.Item1 - nX + fNode.Item2 - nY;

                    (int, int, int, int) nNode = new (nX, nY, nG, nH);

                    if (openSet.Any(t => t.Item1 == nNode.Item1 && t.Item2 == nNode.Item2 && t.Item3 <= nNode.Item3)) continue;
                    if (closedSet.Any(t => t.Item1 == nNode.Item1 && t.Item2 == nNode.Item2)) continue;

                    Console.WriteLine(nH);
                    
                    openSet.Add(nNode);
                }
            }
        }

        return -1;
    }

    static void Part1(){
        int[,] Grid = CreateGrid();
        Console.WriteLine(FindPath((0,0), (Input[0].Length - 1, Input.Count - 1), Grid));
    }

    static void Part2(){
        int[,] Grid = ExpandGrid(CreateGrid());
        //PrintArray(Grid);
        Console.WriteLine(FindPath((0,0), (Grid.GetUpperBound(0), Grid.GetLength(0)-1), Grid));
    }

    static void PrintArray(int[,] G){
        string s = "";
        for (int y = 0; y < G.GetLength(0); y++){
            for (int x = 0; x < G.GetUpperBound(0) + 1; x++){
                s += G[x, y].ToString();
            }

            Console.WriteLine(s);
            s = "";
        }
    }

    //Part 1: 527
    //Part 2: 2887
              
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}
