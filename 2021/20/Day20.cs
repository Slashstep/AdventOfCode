using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Xml.Serialization;


class Day20{

    static public List<string> Input = new List<string>();
    static string Algo = "";
    static int X = 250;
    static int Y = 250;

    static string[,] InputImg = new string[X,Y];
    static string[,] OutputImg = new string[X, Y];



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

    static void FillStrings(){
        Algo = Input[0];

        for (int x = 0; x < X; X++){
            for (int y = 0; y < Y; y++){
                
            }
        }
    }

    static void EnhanceImage(int version){




        OutputImg = "";
        int lineLength = Input[2].Length + version * 2;
        for (int i = 0; i < InputImg.Length; i++){
            string rawBin = "";
            rawBin += i - lineLength - 1 < 0 ? "." : InputImg[i - lineLength - 1];
            rawBin += i - lineLength < 0 ? "." : InputImg[i - lineLength];
            rawBin += i - lineLength + 1 < 0 ? "." : InputImg[i - lineLength + 1];
            rawBin += i - 1 < 0 ? "." : InputImg[i - 1];
            rawBin += InputImg[i];
            rawBin += i + 1 >= InputImg.Length ? "." : InputImg[i + 1];
            rawBin += i + lineLength - 1 >= InputImg.Length ? "." : InputImg[i + lineLength - 1];
            rawBin += i + lineLength >= InputImg.Length ? "." : InputImg[i + lineLength];
            rawBin += i + lineLength + 1 >= InputImg.Length ? "." : InputImg[i + lineLength + 1];

            rawBin = rawBin.Replace('.', '0').Replace('#', '1');
            int position = Convert.ToInt32(rawBin, 2);
            OutputImg += Algo[position];
        }
    }

    static void CheckLights(string toCheck){
        int count = 0;
        foreach (char c in toCheck){
            if (c == '#') count++;
        }

        Console.WriteLine(count);
        Console.WriteLine(toCheck.Length);
    }

    static void ExpandString(int version){
        InputImg = "";
        int lineLength = Input[2].Length + version * 2;
        for (int i = 0; i < lineLength + 2; i++) InputImg += ".";
        for (int i = 0; i < OutputImg.Length; i++){
            if (i % lineLength == 0) InputImg += "." + OutputImg[i];
            else if (i % lineLength == lineLength - 1) InputImg += OutputImg[i] + ".";
            else InputImg += OutputImg[i];
        }
        for (int i = 0; i < lineLength + 2; i++) InputImg += ".";
    }


    static void Part1(){
        FillStrings();
        for (int i = 1; i < 3; i++){
            EnhanceImage(i);
            CheckLights(OutputImg);
            ExpandString(i);
        }
    }

    static void Part2(){

    }

    //Part 1: 4985 < x < 5232
    //Part 2: 
              
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}

