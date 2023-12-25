using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Day25
{
    static List<string> Input = new List<string>();
    static List<Node> Nodes = new List<Node>();
    static List<(Node, Node)> Edges = new List<(Node, Node)>();

    static List<string> ReadFile()
    {
        List<string> lines = new List<string>();

        try
        {
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

    static List<Node> GetNodes()
    {
        Nodes = new List<Node>();
        Edges = new List<(Node, Node)>();

        bool firstExists = false;
        bool secondExists = false;
        foreach(string s in Input)
        {
            string[] leftRight = s.Split(": ");
            string[] right = leftRight[1].Split(" ");

            Node? nNode = new Node(leftRight[0]);

            if (!Nodes.Contains(nNode))
                Nodes.Add(nNode);
            else
                nNode = Nodes.Find(n => n.Name == leftRight[0]);

            foreach (string n in right)
            {
                Node? nnNode = new Node(n);

                if (!Nodes.Contains(nnNode))
                    Nodes.Add(nnNode);
                else
                    nnNode = Nodes.Find(nn => nn.Name == n);

                nNode.Neighbours.Add(nnNode);
                nnNode.Neighbours.Add(nNode);
                Edges.Add((nNode, nnNode));
            }
        }
        return Nodes;
    }

    static (int, int) BFS(Node sNode)
    {
        Queue<Node> toVisit = new Queue<Node>();
        HashSet<Node> visited = new HashSet<Node>();
        toVisit.Enqueue(sNode);
        visited.Add(sNode);
        Node cNode;
        int curMaxLevel = 0;
        int counter = 0;

        while (toVisit.Count > 0)
        {
            cNode = toVisit.Dequeue();
            counter++;
            if (cNode.Level > curMaxLevel)
                curMaxLevel = cNode.Level;
            foreach (Node n in cNode.Neighbours)
            {
                if (visited.Add(n))
                {
                    n.Level = cNode.Level + 1;
                    toVisit.Enqueue(n);
                }
            }
            cNode.Level = 0;
        }

        return (curMaxLevel, counter);
    }

    static void Part1()
    {
        int maxNodes = Nodes.Count();
        List<(Node n, int l)> depths = new List<(Node, int l)>();
        foreach(Node n in Nodes)
            depths.Add((n, BFS(n).Item1));

        depths = depths.OrderBy(x => x.l).ToList();

        List<(Node, Node)> edgesToRemove = new List<(Node, Node)>();

        for (int i = 0; i < 20; i++)
        {
            foreach (Node n in depths[i].n.Neighbours)
            {
                if (edgesToRemove.Contains((depths[i].n, n)) || edgesToRemove.Contains((n, depths[i].n)))
                    continue;

                edgesToRemove.Add(Edges.Find(e => e.Item1 == depths[i].n && e.Item2 == n || e.Item1 == n && e.Item2 == depths[i].n));
            }
        }

        Console.WriteLine(edgesToRemove.Count);

        for (int i = 0; i < edgesToRemove.Count -2; i++)
        {
            edgesToRemove[i].Item1.Neighbours.Remove(edgesToRemove[i].Item2);
            edgesToRemove[i].Item2.Neighbours.Remove(edgesToRemove[i].Item1);

            for (int j = i + 1; j < edgesToRemove.Count - 1; j++)
            {
                edgesToRemove[j].Item1.Neighbours.Remove(edgesToRemove[j].Item2);
                edgesToRemove[j].Item2.Neighbours.Remove(edgesToRemove[j].Item1);

                for (int k = j + 1; k < edgesToRemove.Count; k++)
                {
                    edgesToRemove[k].Item1.Neighbours.Remove(edgesToRemove[k].Item2);
                    edgesToRemove[k].Item2.Neighbours.Remove(edgesToRemove[k].Item1);

                    int sum = BFS(edgesToRemove[i].Item1).Item2;
                    if (sum < maxNodes)
                    {
                        Console.WriteLine(sum * (maxNodes - sum));
                        return;
                    }

                    edgesToRemove[k].Item1.Neighbours.Add(edgesToRemove[k].Item2);
                    edgesToRemove[k].Item2.Neighbours.Add(edgesToRemove[k].Item1);
                }

                edgesToRemove[j].Item1.Neighbours.Add(edgesToRemove[j].Item2);
                edgesToRemove[j].Item2.Neighbours.Add(edgesToRemove[j].Item1);
            }

            edgesToRemove[i].Item1.Neighbours.Add(edgesToRemove[i].Item2);
            edgesToRemove[i].Item2.Neighbours.Add(edgesToRemove[i].Item1);
        }
    }

    //Part 1: 583632

    public static void Main(string[] args)
    {
        Input = ReadFile();
        Nodes = GetNodes();
        Part1();
    }
}


class Node : IEquatable
{
    public string Name;
    public List<Node> Neighbours;
    public int Level;

    public Node (string name)
    {
        Name = name;
        Neighbours = new List<Node>();
        Level = 0;
    }

    public override bool Equals(object? obj)
    {
        return obj is Node node &&
               Name == node.Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name);
    }
}

internal interface IEquatable
{
}