using System;
using System.Text;

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

    static int GetHashCode(string s)
    {
        byte[] ascii = Encoding.ASCII.GetBytes(s);

        int counter = 0;
        for (int i = 0; i < ascii.Length; i++)
        {
            counter += ascii[i];
            counter *= 17;
            counter = counter % 256;
        }

        return counter;
    }

    static void Part1(){
        string[] strings = Input[0].Split(",");


        int counter = 0;
        for (int i = 0; i < strings.Length; i++)
            counter += GetHashCode(strings[i]);

        Console.WriteLine(counter);
    }


    static void Part2(){
        string[] strings = Input[0].Split(",");
        List<Box> boxes = new List<Box>();
        for (int i = 0; i < 256; i++)
            boxes.Add(new Box());

        for (int i = 0; i < strings.Length; i++)
        {
            if (strings[i].Contains('-'))
            {
                string letters = strings[i].Substring(0, strings[i].Length - 1);
                int index = GetHashCode(letters);
                (string, int) lens = boxes[index].Lenses.Find(l => l.Item1 == letters);
                if (lens.Item1 == null) continue;

                boxes[index].Lenses.Remove(lens);
            }
            else if (strings[i].Contains('='))
            {
                string letters = strings[i].Split("-")[0].Split("=")[0];
                int index = GetHashCode(letters);
                int num = int.Parse(strings[i].Split("-")[0].Split("=")[1]);
                (string, int) lens = boxes[index].Lenses.Find(l => l.Item1 == letters);

                if (lens.Item1 == null) boxes[index].Lenses.Add((letters, num));
                else
                {
                    int lensIndex = boxes[index].Lenses.IndexOf(lens);
                    boxes[index].Lenses[lensIndex] = (letters, num);
                }
            }
        }

        int counter = 0;
        for (int i = 0; i < boxes.Count; i++)
            counter += (i + 1) * boxes[i].GetValue();

        Console.WriteLine(counter);
    }

    //Part 1: 503154
    //Part 2: 251353

    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}

class Box
{
    public List<(string, int)> Lenses = new List<(string, int)>();

    public Box()
    {
        Lenses = new List<(string, int)>();
    }

    public int GetValue()
    {
        int counter = 0;
        for (int i = 0; i < Lenses.Count; i++)
        {
            counter += (i + 1) * Lenses[i].Item2;
        }

        return counter;
    }
}
