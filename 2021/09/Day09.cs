using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Day09{

    static public List<string> Input = new List<string>();
    static public int X = 100;
    static public int Y = 100;

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
        int[,] values = new int[Y, X];

        for (int i = 0; i < Y; i++){
            for (int j = 0; j < X; j++){
                values[i,j] = int.Parse(Input[i][j].ToString());
            }
        }

        int counter = 0;
        for (int i = 0; i < Y; i++){
            for (int j = 0; j < X; j++){
                //Ignore Edge Cases
                if (i - 1 >= 0 && values[i - 1, j] <= values[i, j]){
                    continue;
                }
                
                if (i + 1 < Y && values[i + 1, j] <= values[i, j]){
                    continue;
                }

                if (j - 1 >= 0 && values[i, j - 1] <= values[i, j]){
                    continue;
                }

                if (j + 1 < X && values[i, j + 1] <= values[i, j]){
                    continue;
                }

                counter += values[i, j] + 1;
            }
        }

        Console.WriteLine(counter);
    }

    static void Part2(){
        int[,] values = new int[Y, X];

        for (int i = 0; i < Y; i++){
            for (int j = 0; j < X; j++){
                values[i,j] = int.Parse(Input[i][j].ToString());
            }
        }

        List<int> basinValue = new List<int>();
        for (int i = 0; i < Y; i++){
            for (int j = 0; j < X; j++){
                if (values[i, j] != 9){
                    int val= FillBasin(ref values, i, j, 1);
                    basinValue.Add(val);
                }
            }
        }

        basinValue = basinValue.OrderByDescending(v => v).ToList();
        int counter = basinValue[0] * basinValue[1] * basinValue[2];
        Console.WriteLine(counter);
    }

    static int FillBasin(ref int[,] vals, int i, int j, int counter){
        vals[i, j] = 9;
        if (i - 1 >= 0 && vals[i - 1, j] != 9){
            counter = FillBasin(ref vals, i - 1, j, ++counter);
        }
        
        if (i + 1 < Y && vals[i + 1, j] != 9){
            counter = FillBasin(ref vals, i + 1, j, ++counter);
        }

        if (j - 1 >= 0 && vals[i, j - 1] != 9){
            counter = FillBasin(ref vals, i, j - 1, ++counter);
        }

        if (j + 1 < X && vals[i, j + 1] != 9){
            counter = FillBasin(ref vals, i, j + 1, ++counter);
        }

        return counter;
    }

    //Part 1: 439
    //Part 2: 900900
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}
