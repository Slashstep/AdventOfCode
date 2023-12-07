using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Day07{

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
        List<Hand> hands = new List<Hand>();

        foreach (string s in Input){
            string[] cardsBid = s.Split(" ");
            hands.Add(new Hand(cardsBid[0], int.Parse(cardsBid[1])));
        }

        hands = hands.OrderBy(h => h.Type).ThenBy(h => h.ReplacedCards).ToList();

        long counter = 0;
        for (int i = 0; i < hands.Count; i++){
            hands[i].Rank = i+1;
            counter += hands[i].Rank * hands[i].Bid;
        }

        Console.WriteLine(counter);
    }

    static void Part2(){
        List<JokerHand> hands = new List<JokerHand>();

        foreach (string s in Input){
            string[] cardsBid = s.Split(" ");
            hands.Add(new JokerHand(cardsBid[0], int.Parse(cardsBid[1])));
        }

        hands = hands.OrderBy(h => h.Type).ThenBy(h => h.ReplacedCards).ToList();

        long counter = 0;
        for (int i = 0; i < hands.Count; i++){
            hands[i].Rank = i+1;
            counter += hands[i].Rank * hands[i].Bid;
        }

        Console.WriteLine(counter);
    }

    //Part 1: 250347426
    //Part 2: 251224870
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}

class Hand{
    public string Cards;
    public string ReplacedCards;
    public int Bid;
    public int Rank;
    public int Type;

    public Hand(string cards, int bid){
        Cards = cards;
        ReplacedCards = ReplaceChars(cards);
        Bid = bid;
        Type = GetTypeOfHand();
    }

    int GetTypeOfHand(){
        List<(string, int)> checkedCards = new List<(string, int)>();

        for (int i = 0; i < Cards.Length; i++){
            int index = checkedCards.IndexOf(checkedCards.Find(c => c.Item1 == Cards[i].ToString()));

            if (index == -1){
                checkedCards.Add((Cards[i].ToString(), 1));
            }
            else{
                checkedCards[index] = new (checkedCards[index].Item1, checkedCards[index].Item2 + 1);
            }
        }

        //Five of a Kind
        if (checkedCards.Count == 1){
            return 6;
        }
        else if (checkedCards.Count == 2){
        //Four of a kind
            if (checkedCards[0].Item2 == 4 || checkedCards[1].Item2 == 4){
                return 5;
            }
        //Full House
            else{
                return 4;
            }
        }
        else if (checkedCards.Count == 3){
        //Three of a kind
            if (checkedCards[0].Item2 == 3 || checkedCards[1].Item2 == 3 || checkedCards[2].Item2 == 3){
                return 3;
            }
        //Two pair
            else{
                return 2;
            }
        }
        //One pair
        else if (checkedCards.Count == 4){
            return 1;
        }
        else{
            return 0;
        }
    }

    public void PrintHand(){
        Console.WriteLine(Cards + ": " + Bid.ToString() + ", " + Type.ToString() + ", " + Rank.ToString());
    }

    string ReplaceChars(string str){
        string output = "";

        for (int i = 0; i < str.Length; i++){
            switch (str[i].ToString()){
                case "T":
                    output += "A";
                    break;
                case "J":
                    output += "B";
                    break;
                case "Q":
                    output += "C";
                    break;
                case "K":
                    output += "D";
                    break;
                case "A":
                    output += "E";
                    break;
                default:
                    output += str[i].ToString();
                    break;
            }

        }
        
        return output;
    }
}

class JokerHand{
    public string Cards;
    public string ReplacedCards;
    public int Bid;
    public int Rank;
    public int Type;

    public JokerHand(string cards, int bid){
        Cards = cards;
        ReplacedCards = ReplaceChars(cards);
        Bid = bid;
        Type = GetTypeOfHand();
    }

    int GetTypeOfHand(){
        List<(string, int)> checkedCards = new List<(string, int)>();

        int jokerCounter = 0;
        for (int i = 0; i < Cards.Length; i++){
            if (Cards[i].ToString() == "J"){
                jokerCounter++;
                continue;
            }
            
            int index = checkedCards.IndexOf(checkedCards.Find(c => c.Item1 == Cards[i].ToString()));

            if (index == -1){
                checkedCards.Add((Cards[i].ToString(), 1));
            }
            else{
                checkedCards[index] = new (checkedCards[index].Item1, checkedCards[index].Item2 + 1);
            }
        }

        if (checkedCards.Count == 0){
            return 6;
        }

        checkedCards = checkedCards.OrderByDescending(c => c.Item2).ToList();
        checkedCards[0] = new (checkedCards[0].Item1, checkedCards[0].Item2 + jokerCounter);

        //Five of a Kind
        if (checkedCards.Count == 1){
            return 6;
        }
        else if (checkedCards.Count == 2){
        //Four of a kind
            if (checkedCards[0].Item2 == 4 || checkedCards[1].Item2 == 4){
                return 5;
            }
        //Full House
            else{
                return 4;
            }
        }
        else if (checkedCards.Count == 3){
        //Three of a kind
            if (checkedCards[0].Item2 == 3 || checkedCards[1].Item2 == 3 || checkedCards[2].Item2 == 3){
                return 3;
            }
        //Two pair
            else{
                return 2;
            }
        }
        //One pair
        else if (checkedCards.Count == 4){
            return 1;
        }
        else{
            return 0;
        }
    }

    public void PrintHand(){
        Console.WriteLine(Cards + ": " + Bid.ToString() + ", " + Type.ToString() + ", " + Rank.ToString());
    }

    string ReplaceChars(string str){
        string output = "";

        for (int i = 0; i < str.Length; i++){
            switch (str[i].ToString()){
                case "T":
                    output += "A";
                    break;
                case "J":
                    output += "1";
                    break;
                case "Q":
                    output += "C";
                    break;
                case "K":
                    output += "D";
                    break;
                case "A":
                    output += "E";
                    break;
                default:
                    output += str[i].ToString();
                    break;
            }

        }
        
        return output;
    }
}