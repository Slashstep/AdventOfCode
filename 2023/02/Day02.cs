using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;

class Day02{

    static List<string> ReadFile(){
        List<string> lines = new List<string>();

        try{
            if (File.Exists("input.txt")){
                lines.AddRange(File.ReadAllLines("input.txt"));
            }
            else
            {
                Console.WriteLine("The file does not exist.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }

        return lines;
    }

    static List<Game> SetupGames(List<string> input){
        List<Game> Games = new List<Game>();
        List<string> draws = new List<string>();
        for (int i = 0; i < input.Count(); i++){
            Game curGame = new Game(i + 1);

            draws.Clear();
            draws = input[i].Split(": ")[1].Split("; ").ToList();
            
            List<string> cubes = new List<string>();
            for (int j = 0; j < draws.Count(); j++){
                cubes.Clear();
                cubes = draws[j].Split(", ").ToList();

                int red = 0;
                int green = 0;
                int blue = 0;
                for (int k = 0; k < cubes.Count(); k++){

                    if (cubes[k].Contains("red")){
                        red = int.Parse(cubes[k].Split(" ")[0]);
                    }
                    else if (cubes[k].Contains("green")){
                        green = int.Parse(cubes[k].Split(" ")[0]);
                    }
                    else{
                        blue = int.Parse(cubes[k].Split(" ")[0]);
                    }

                }
                curGame.Draws.Add((red, green, blue));
            }

            Games.Add(curGame);
        }

        return Games;
    }

    static void Part1(List<string> input){        
        //Lines to Games
        List<Game> Games = SetupGames(input);
        int counter = 0;
        for (int i = 0; i < Games.Count(); i++){
            for (int j = 0; j < Games[i].Draws.Count(); j++){
                if (Games[i].Draws[j].Item1 > 12){
                    Games[i].isPossible = false;
                    break;
                }
                else if (Games[i].Draws[j].Item2 > 13){
                    Games[i].isPossible = false;
                    break;
                }
                else if (Games[i].Draws[j].Item3 > 14){
                    Games[i].isPossible = false;
                    break;
                }

                Games[i].isPossible = true;
            }

            if (Games[i].isPossible)
                counter += Games[i].ID;
        }

        Console.WriteLine(counter);
    }

    static void Part2(List<string> input){
        List<Game> Games = SetupGames(input);
        int counter = 0;
        for (int i = 0; i < Games.Count; i++){
            int red = 0;
            int green = 0;
            int blue = 0;

            for (int j = 0; j < Games[i].Draws.Count(); j++){
                if (Games[i].Draws[j].Item1 > red)
                    red = Games[i].Draws[j].Item1;
                
                if (Games[i].Draws[j].Item2 > green)
                    green = Games[i].Draws[j].Item2;

                if (Games[i].Draws[j].Item3 > blue)
                    blue = Games[i].Draws[j].Item3;
            }

            counter += red * green * blue;
        }

        Console.WriteLine(counter);
    }

    //Part 1: 2476
    //Part 2: 54911
    public static void Main(string[] args){
        Part1(ReadFile());
        Part2(ReadFile());
    }
}

class Game{
    public int ID;
    public bool isPossible;
    public List<(int, int, int)> Draws;

    public Game (int i){
        ID = i;
        isPossible = false;
        Draws = new List<(int, int,int)>();
    }
}