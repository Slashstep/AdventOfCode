using System;
using System.Text;
using System.Xml.Linq;

class Day17{

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

    static List<Node> GetNodes()
    {
        List<Node> Nodes = new List<Node>();

        for (int i = 0; i < Input.Count; i++)
        {
            for (int j = 0; j < Input[i].Length; j++)
            {
                Nodes.Add(new Node(j, i, int.Parse(Input[i][j].ToString())));
            }
        }

        foreach(Node n in Nodes)
        {
            //Check left
            Node? nLeft = Nodes.Find(node => node.PosX == n.PosX - 1 && node.PosY == n.PosY);
            if (nLeft != null) n.Neighbours.Add(nLeft);

            //Check right
            Node? nRight = Nodes.Find(node => node.PosX == n.PosX + 1 && node.PosY == n.PosY);
            if (nRight != null) n.Neighbours.Add(nRight);

            //Check up
            Node? nUp = Nodes.Find(node => node.PosX == n.PosX && node.PosY - 1 == n.PosY);
            if (nUp != null) n.Neighbours.Add(nUp);

            //Check down
            Node? nDown = Nodes.Find(node => node.PosX == n.PosX && node.PosY + 1 == n.PosY);
            if (nDown != null) n.Neighbours.Add(nDown);
        }

        return Nodes;
    }

    static void FindPath(Node sNode, Node fNode)
    {
        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(sNode);
        sNode.GCost = 0;

        Node cNode;
        while (openSet.Count > 0)
        {
            openSet = openSet.OrderBy(n => n.GCost).ToList();
            for (int i = 0; i < Math.Min(5, openSet.Count); i++)
                openSet[i].PrintNode();
            Console.WriteLine("");
            cNode = openSet[0];
            if (cNode.StraightDist > 2)
                closedSet.Add(cNode);
            openSet.RemoveAt(0);

            if (cNode == fNode)
            {
                //TracePath(sNode, fNode);
                Console.WriteLine(fNode.GCost);
                return;
            }

            foreach (Node n in cNode.Neighbours)
            {
                if (closedSet.Contains(n))
                    continue;

                if (CheckDirection(cNode, n) > 2)
                    continue;

                int newMovementCostToNeighbour = cNode.GCost + n.Value;

                if (newMovementCostToNeighbour < n.GCost)
                {
                    n.GCost = newMovementCostToNeighbour;
                    n.Parent = cNode;
                    n.StraightDist = CheckDirection(cNode, n);
                    openSet.Add(n);
                }
            }
        }
    }

    static int CheckDirection(Node par, Node next)
    {
        int counter = 0;
        bool isTurn = false;
        int dirX = next.PosX - par.PosX;
        int dirY = next.PosY - par.PosY;
        Node cNode = par;
        while (!isTurn)
        {
            if (cNode.Parent == null)
                return counter;

            int cDirX = cNode.PosX - cNode.Parent.PosX;
            int cDirY = cNode.PosY - cNode.Parent.PosY;

            if (cDirX == dirX && cDirY == dirY) counter++;
            else isTurn = true;
            cNode = cNode.Parent;
        }

        return counter;
    }

    static List<Node> TracePath(Node sNode, Node fNode)
    {
        Console.WriteLine("Path found");
        List<Node> Path = new List<Node>();
        Node currentNode = fNode;

        while (currentNode != sNode)
        {
            Console.WriteLine(currentNode.PosX.ToString() + ", " + currentNode.PosY.ToString());
            Path.Add(currentNode);
            currentNode = currentNode.Parent;
        }

        Path.Reverse();

        return Path;
    }

    static void Part1(){
        List<Node> Nodes = GetNodes();

        Node? startNode = Nodes.Find(n => n.PosX == 0 && n.PosY == 0);
        Node? endNode = Nodes.Find(n => n.PosX == Input[0].Length - 1 && n.PosY == Input.Count - 1);

        FindPath(startNode, endNode);
    }


    static void Part2(){

    }

    //Part 1: 783 soll 742
    //Part 2: soll 918

    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}

class Node : IEquatable<Node>
{
    public int PosX;
    public int PosY;
    public int Value;
    public List<Node> Neighbours;
    public Node? Parent;
    public int StraightDist;
    public int GCost;
    public int HCost;
    public int FCost { get { return GCost + HCost; } }

    public Node (int posX, int posY, int value)
    {
        PosX = posX;
        PosY = posY;
        Value = value;
        Neighbours = new List<Node>();
        GCost = 1000000;
        StraightDist = 0;
    }

    public bool Equals(Node? other)
    {
        if (this.PosX == other?.PosX && this.PosY == other?.PosY) return true;
        else return false;
    }

    public void PrintNode()
    {
        if (Parent == null)
            Console.WriteLine(PosX.ToString() + ", " + PosY.ToString() + ": " + GCost.ToString());
        else
            Console.WriteLine(PosX.ToString() + ", " + PosY.ToString() + ": " + GCost.ToString() + ": " + Parent.PosX.ToString() + ", " + Parent.PosY.ToString());
    }
}
