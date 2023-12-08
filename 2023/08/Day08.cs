using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Day08{

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
        string lr = Input[0];
        List<Instruction> instructions = new List<Instruction>();

        for (int i = 2; i < Input.Count; i++){
            string[] nodeLR = Input[i].Split(" = ");
            string[] lrNode = nodeLR[1].Split(", ");
            Instruction newInstruction = new Instruction(nodeLR[0], lrNode[0].Substring(1), lrNode[1].Substring(0, 3));
            instructions.Add(newInstruction);
        }

        instructions = instructions.OrderBy(i => i.Node).ToList();
        
        Console.WriteLine(FindInstruction(instructions, lr, instructions[0], 0));
    }

    static long FindInstruction(List<Instruction> inst, string lr, Instruction next, long counter){
        if (next.Node[2].ToString() == "Z"){
            return counter;
        }

        if (next.Left == next.Node && next.Right == next.Node){
            Console.WriteLine("Emergeny Stop");
            return counter;
        }


        int nextString = (int)(counter % (long)lr.Length);
        if (lr[nextString].ToString() == "L"){
            Instruction newInstruction = inst.Find(n => n.Node == next.Left);
            counter = FindInstruction(inst, lr, newInstruction, ++counter);
        }
        else{
            Instruction newInstruction = inst.Find(n => n.Node == next.Right);
            counter = FindInstruction(inst, lr, newInstruction, ++counter);
        }

        return counter;
    }

    static void Part2(){
        string lr = Input[0];
        List<Instruction> instructions = new List<Instruction>();

        for (int i = 2; i < Input.Count; i++){
            string[] nodeLR = Input[i].Split(" = ");
            string[] lrNode = nodeLR[1].Split(", ");
            Instruction newInstruction = new Instruction(nodeLR[0], lrNode[0].Substring(1), lrNode[1].Substring(0, 3));
            instructions.Add(newInstruction);
        }

        foreach(Instruction i in instructions){
            i.LeftI = instructions.IndexOf(instructions.Find(n => n.Node == i.Left));
            i.RightI = instructions.IndexOf(instructions.Find(n => n.Node == i.Right));
        }

        instructions = instructions.OrderBy(i => i.Node).ToList();

        List<long> longvals = new List<long>();
        foreach (Instruction inst in instructions){
            if (inst.Node[2].ToString() == "A"){
                long c = FindInstruction(instructions, lr, inst, 0);
                longvals.Add(c);
            }
        }

        while (longvals.Count > 1){
            longvals[0] = lcm(longvals[0], longvals[1]);
            longvals.RemoveAt(1);
        }

        Console.WriteLine(longvals[0]);
    }

    static long gcf(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    static long lcm(long a, long b)
    {
        return (a / gcf(a, b)) * b;
    }

    //Part 1: 20569
    //Part 2: 21366921060721
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}

class Instruction{
    public string Node;
    public string Left;
    public string Right;
    public int LeftI;
    public int RightI;


    public Instruction(string node, string left, string right){
        this.Node = node;
        this.Left = left;
        this.Right = right;
        LeftI = 0;
        RightI = 0;
    }

    public void PrintInstruction(){
        Console.WriteLine(Node + ": " + Left + ", " + Right);
    }
}
