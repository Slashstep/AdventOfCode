using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

class Day12{

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

    static List<Node> CreateNodes(){
        List<Node> Nodes = new List<Node>();
        for (int i = 0; i < Input.Count; i++){
            string[] sepNodes = Input[i].Split("-");
            int index = Nodes.IndexOf(Nodes.Find(n => n.Name == sepNodes[0]));
            if (index == -1){
                Nodes.Add(new Node(sepNodes[0], char.IsUpper(sepNodes[0][0])));
            }
            index = Nodes.IndexOf(Nodes.Find(n => n.Name == sepNodes[1]));
            if (index == -1){
                Nodes.Add(new Node(sepNodes[1], char.IsUpper(sepNodes[1][0])));
            }
        }

        for (int i = 0; i < Input.Count; i++){
            string[] sepNodes = Input[i].Split("-");
            Node n1 = Nodes.Find(n => n.Name == sepNodes[0]);
            Node n2 = Nodes.Find(n => n.Name == sepNodes[1]);

            n1.Connections.Add(n2);
            n2.Connections.Add(n1);
        }

        return Nodes;
    }

    static void Part1(){
        List<Node> Nodes = CreateNodes();

        Node startNode = Nodes.Find(n => n.Name == "start");
        int counter = DFS(startNode, new HashSet<Node>(), 0);

        Console.WriteLine(counter);
    }

    static int DFS(Node nodeToVisit, HashSet<Node> smallVisited, int counter){
        if (nodeToVisit.Name == "end"){
            return 1;
        }

        if (!nodeToVisit.IsBig && smallVisited.Contains(nodeToVisit)){
            return 0;
        }
        smallVisited.Add(nodeToVisit);

        int count = 0;
        foreach (Node n in nodeToVisit.Connections){
            count += DFS(n, smallVisited, counter);
        }

        smallVisited.Remove(nodeToVisit);
        return count;
    }

    static void Part2(){
        List<Node> Nodes = CreateNodes();
        Caves caves = new Caves(Nodes);

        Node startNode = Nodes.Find(n => n.Name == "start");
        int counter = DFSPart2(startNode, caves);

        Console.WriteLine(counter);
    }

    static int DFSPart2(Node nodeToVisit, Caves smallVisited){
        if (nodeToVisit.Name == "end"){
            return 1;
        }

        if (!smallVisited.CanBeVisited(nodeToVisit)){
            return 0;
        }
        smallVisited.IncreaseCaveVisit(nodeToVisit);

        int count = 0;
        foreach (Node n in nodeToVisit.Connections){
            count += DFSPart2(n, smallVisited);
        }

        smallVisited.DecreaseCaveVisit(nodeToVisit);
        return count;
    }

    //Part 1: 4691
    //Part 2: 140718
              
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}

class Caves{
    public List<(Node, int)> Nodes = new List<(Node, int)>();

    public Caves(List<Node> nodes){
        foreach (Node n in nodes){
            Nodes.Add((n, 0));
        }
    }

    public bool CanBeVisited(Node n){
        //start not twice
        if (n.Name == "start"){
            (Node, int) start = Nodes.Find(ni => ni.Item1.Name == "start");

            if (start.Item2 < 1) return true;
            else return false;
        }
        if (n.IsBig) return true;
        else{
            (Node, int) test = Nodes.Find(ni => ni.Item1.Name == n.Name);
            if (test.Item2 < 1) return true;
            else{
                for (int i = 0; i < Nodes.Count; i++){
                    if (!Nodes[i].Item1.IsBig && Nodes[i].Item2 > 1) return false;
                }
                return true;
            }
        }
    }

    public void IncreaseCaveVisit(Node n){
        int node = Nodes.IndexOf(Nodes.Find(ni => ni.Item1.Name == n.Name));
        Nodes[node] = new (Nodes[node].Item1, Nodes[node].Item2 + 1);
    }

    public void DecreaseCaveVisit(Node n){
        int node = Nodes.IndexOf(Nodes.Find(ni => ni.Item1.Name == n.Name));
        Nodes[node] = new (Nodes[node].Item1, Nodes[node].Item2 - 1);
    }
}

class Node{
    public string Name;
    public bool IsBig;
    public List<Node> Connections;

    public Node(string name, bool isBig){
        Name = name;
        IsBig = isBig;
        Connections = new List<Node>();
    }

    public void PrintNode(){
        string c = "";
        foreach (Node n in Connections){
            c += n.Name + ", ";
        }
        Console.WriteLine(Name + ": " + c);
    }
}
