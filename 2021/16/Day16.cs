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
        catch (Exception ex){
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        
        return lines;
    }

    static string ToBinary(string input){
        string binaryDigit = "";
        foreach (char c in input){
            binaryDigit += Convert.ToString(Convert.ToInt64(c.ToString(), 16), 2).PadLeft(4, '0');
        }

        return binaryDigit;
    }

    static long ToDec(string input){
        input = input.PadLeft((input.Length + 3)/4 *4, '0');
        return Convert.ToInt64(input, 2);
    }

    static string ToHex(string input){
        string hexDigit = "";
        input = input.PadLeft((input.Length + 3)/4 *4, '0');

        for (int i = 0; i < input.Length; i += 4)
            hexDigit += Convert.ToInt64(input.Substring(i, 4), 2).ToString("X");

        return hexDigit;
    }

    static (long, int) VersionSum(string input, int pos){
        if (input.Length - pos < 6) return (0, pos);
        
        //Get the version number of the string
        long vNum = ToDec(input.Substring(pos, 3));

        //Check the type of packet
        string type = ToHex(input.Substring(pos + 3, 3));

        //Check for literal packet
        if (type == "4"){
            string value;
            int counter = 0;
            long val = 0;

            do{
                value = input.Substring(pos + 6 + counter*5, 5);
                val += ToDec(value.Substring(1, 4));
                counter += 1;

            }
            while(value[0] == '1');

            return (vNum, pos + 6 + counter*5);
        }

        long packetLength;
        if (input[pos + 6] == '0'){
            packetLength = ToDec(input.Substring(pos + 7, 15));
            pos += 7 + 15;

            while (packetLength > 0){
                (long, int) result = VersionSum(input, pos);
                vNum += result.Item1;
                packetLength = packetLength - (long)MathF.Abs(result.Item2 - pos);
                pos = result.Item2;
            }
        }
        else{
            packetLength = ToDec(input.Substring(pos + 7, 11));
            pos += 7 + 11;

            for (long i = 0; i < packetLength; i++){
                (long, int) result = VersionSum(input, pos);
                vNum += result.Item1;
                pos = result.Item2;
            }
        } 

        return (vNum, pos);
    }


    static (long, int) Operator(string input, int pos){
        if (input.Length - pos < 6) return (0, pos);

        List<long> totals = new List<long>();

        //Check the type of packet
        string type = ToHex(input.Substring(pos + 3, 3));

        //Check for literal packet
        if (type == "4"){
            string value;
            int counter = 0;
            string val = "";

            do{
                value = input.Substring(pos + 6 + counter*5, 5);
                val += value.Substring(1, 4);
                counter += 1;
            }
            while(value[0] == '1');

            return (ToDec(val), pos + 6 + counter*5);
        }

        long packetLength;
        if (input[pos + 6] == '0'){
            packetLength = ToDec(input.Substring(pos + 7, 15));
            pos += 7 + 15;

            while (packetLength > 0){
                (long, int) result = Operator(input, pos);
                totals.Add(result.Item1);
                packetLength = packetLength - (long)MathF.Abs(result.Item2 - pos);
                pos = result.Item2;
            }
        }
        else{
            packetLength = ToDec(input.Substring(pos + 7, 11));
            pos += 7 + 11;

            for (long i = 0; i < packetLength; i++){
                (long, int) result = Operator(input, pos);
                totals.Add(result.Item1);
                pos = result.Item2;
            }
        } 

        long res;
        switch (type){
            case "0":
                long sum = 0;
                foreach(long i in totals) sum += i;
                res = sum;
                break;
            case "1":
                long prod = 1;
                foreach(long i in totals) prod *= i;
                res = prod;
                break;
            case "2":
                res = totals.Min();
                break;
            case "3":
                res = totals.Max();
                break;
            case "5":
                if (totals[0] > totals[1]) res = 1;
                else res = 0;
                break;
            case "6":
                if (totals[0] < totals[1]) res = 1;
                else res = 0;
                break;
            case "7":
                if (totals[0] == totals[1]) res = 1;
                else res = 0;
                break;
            default:
                res = 0;
                break;
        }

        return (res, pos);
    }

    static void Part1(){
        string binString = ToBinary(Input[0]);

        Console.WriteLine(VersionSum(binString, 0));
    }

    static void Part2(){
        string binString = ToBinary(Input[0]);

        Console.WriteLine(Operator(binString, 0));
    }

    //Part 1: 927
    //Part 2: 1725277876501
              
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}
