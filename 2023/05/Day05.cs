using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

    static ScratchCard ReadScratCard(string input){
        ScratchCard scratchCard= new ScratchCard();

        string[] cutID = input.Split(": ");
        string[] cutWinDraw = cutID[1].Split(" | ");
        string[] wins = cutWinDraw[0].Split(" ");
        string[] draws = cutWinDraw[1].Split(" ");

        //Put in Wins
        for (int i = 0; i < wins.Length; i++){
            if (wins[i] == "")
                continue;

            scratchCard.WinningNumbers.Add(int.Parse(wins[i]));
        }

        for (int i = 0; i < draws.Length; i++){
            if (draws[i] == "")
                continue;

            scratchCard.DrawnNumbers.Add(int.Parse(draws[i]));
        }

        scratchCard.CheckWinning();

        return scratchCard;
    }

    static void Part1(){
        List<ScratchCard> ScratchCards = new List<ScratchCard>();

        for (int i = 0; i < Input.Count(); i++)
            ScratchCards.Add(ReadScratCard(Input[i]));

        int counter = 0;
        for (int i = 0; i < ScratchCards.Count(); i++){
            counter += ScratchCards[i].Value;
        }

        Console.WriteLine(counter);
    }

    static void Part2(){
        List<ScratchCard> ScratchCards = new List<ScratchCard>();

        for (int i = 0; i < Input.Count(); i++)
            ScratchCards.Add(ReadScratCard(Input[i]));

        int counter = 0;
        for (int i = 0; i < ScratchCards.Count(); i++){
            for (int j = 0; j < ScratchCards[i].Copies; j++){
                int val = ScratchCards[i].Wins;
                
                for (int k = i + 1; k <= MathF.Min(val + i, ScratchCards.Count() - 1); k++){
                    ScratchCards[k].Copies++;
                }
            }

            counter += ScratchCards[i].Copies;
        }

        Console.WriteLine(counter);
    }

    //Part 1: 15205
    //Part 2: 6189740
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}

class ScratchCard{
    public List<int> WinningNumbers = new List<int>();
    public List<int> DrawnNumbers = new List<int>();
    public int Wins;
    public int Value;

    public int Copies;

    public ScratchCard(){
        WinningNumbers = new List<int>();
        DrawnNumbers = new List<int>();
        Wins = 0;
        Value = 0;
        Copies = 1;
    }

    public void CheckWinning(){
        int counter = 0;
        
        for (int i = 0; i < WinningNumbers.Count(); i++){
            if (DrawnNumbers.Contains(WinningNumbers[i])){
                counter++;
            }
        }

        if (counter == 0){
            Value = 0;
        }
        else{
            Value = (int)Math.Pow(2, counter - 1);
        }

        Wins = counter;
    }
}
