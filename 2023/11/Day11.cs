using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

class Day10{

    static public List<string> Input = new List<string>();
    static List<Vector2Int> Galaxys = new List<Vector2Int>();
    static List<int> expansionPosX = new List<int>();
    static List<int> expansionPosY = new List<int>();

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

    static void CreateGalaxy(int expansionFactor){
        //Add horizontal Expansion
        expansionPosY = new List<int>();
        for (int i = 0; i < Input.Count; i++){
            if (!Input[i].Contains("#")){
                expansionPosY.Add(i);
            }
        }

        //Add vertical expansion
        expansionPosX = new List<int>();
        for (int i = 0; i < Input[0].Length; i++){
            string s = "";
            for (int j = 0; j < Input.Count; j++){
                s += Input[j][i];
            }
            if (!s.Contains("#")){
                expansionPosX.Add(i);
            }
        }

        Galaxys = new List<Vector2Int>();
        for (int i = 0; i < Input.Count; i++){
            for(int j = 0; j < Input[i].Length; j++){
                if (Input[i][j] == '#'){
                    int exFacX = 0;
                    for (int k = 0; k < expansionPosX.Count; k++){
                        exFacX = k;
                        if (expansionPosX[k] > j){
                            break;
                        }
                        else if (j > expansionPosX[expansionPosX.Count - 1]){
                            exFacX = expansionPosX.Count;
                            break;
                        }
                    }
                    int exFacY = 0;
                    for (int k = 0; k < expansionPosY.Count; k++){
                        exFacY = k;
                        if (expansionPosY[k] > i){
                            break;
                        }
                        else if (i > expansionPosY[expansionPosY.Count - 1]){
                            exFacY = expansionPosY.Count;
                            break;
                        }
                    }
                    

                    Galaxys.Add(new Vector2Int(j, i, exFacX, exFacY, expansionFactor));
                }
            }
        }
    }

    static void Part1(){
        CreateGalaxy(2);

        int counter = 0;
        for (int i = 0; i < Galaxys.Count - 1; i++){
            for (int j = i + 1; j < Galaxys.Count; j++){
                counter += Math.Abs(Galaxys[j].expandedY - Galaxys[i].expandedY) + Math.Abs(Galaxys[j].expandedX - Galaxys[i].expandedX);
            }
        }

        Console.WriteLine(counter);
    }

    static void Part2(){
        CreateGalaxy(1000000);

        long counter = 0;
        for (int i = 0; i < Galaxys.Count - 1; i++){
            for (int j = i + 1; j < Galaxys.Count; j++){
                counter += Math.Abs(Galaxys[j].expandedY - Galaxys[i].expandedY) + Math.Abs(Galaxys[j].expandedX - Galaxys[i].expandedX);
            }
        }

        Console.WriteLine(counter);
    }

    //Part 1: 9769724
    //Part 2: 603020563700
              
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}

class Vector2Int{
    public int expandedX;
    public int expandedY;

    public Vector2Int(int x, int y, int exFactorX, int exFactorY, int expansionFactor){
        if (exFactorX == 0) expandedX = x;
        else expandedX = x - exFactorX + exFactorX * expansionFactor;

        if (exFactorY == 0) expandedY = y;
        else expandedY = y - exFactorY + exFactorY * expansionFactor;
    }
}
