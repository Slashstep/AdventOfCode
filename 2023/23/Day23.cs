using System.IO;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;

class Day23{

    static public List<string> Input = new List<string>();
    static public char[,] Grid;
    static public (int, int)[] Dirs = { (-1, 0), (1, 0), (0, -1), (0, 1) };

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

    static void CreateGrid()
    {
        Grid = new char[Input[0].Length, Input.Count];
        for (int i = 0; i < Input.Count; i++)
        {
            for (int j = 0; j < Input[i].Length; j++)
            {
                Grid[j, i] = Input[i][j];
            }
        }
    }

    static int DFS_Wet ((int x, int y) sNode, (int x, int y) fNode, HashSet<(int x, int y)> path, int curMax)
    {
        if (path.Contains(sNode))
        {
            return 0;
        }

        path.Add(sNode);

        if (sNode == fNode)
        {
            return Math.Max(path.Count - 1, curMax);
        }

        foreach ((int x, int y) d in Dirs)
        {
            int nX = sNode.x + d.x;
            int nY = sNode.y + d.y;

            if (nX < 0 || nX >= Input[0].Length || nY < 0 || nY >= Input.Count)
                continue;

            switch (Grid[nX, nY])
            {
                case '#':
                    continue;
                case '>':
                    if (d != (1, 0))
                        continue;
                    break;
                case '<':
                    if (d != (-1, 0))
                        continue;
                    break;
                case 'v':
                    if (d != (0, 1))
                        continue;
                    break;
                case '^':
                    if (d != (0, -1))
                        continue;
                    break;
                default:
                    break;
            }

            (int, int) nNode = (nX, nY);

            curMax = Math.Max(DFS_Wet(nNode, fNode, new HashSet<(int x, int y)>(path), curMax), curMax);
        }

        return curMax;
    }

    static void Part1()
    {
        Console.WriteLine(DFS_Wet((1, 0), (Input[0].Length - 2, Input.Count - 1), new HashSet<(int x, int y)>(), 0));
    }

    static int DFS_Dry((int x, int y) sNode, (int x, int y) cNode, (int x, int y) fNode, HashSet<(int x, int y)> path, int curMax, List<(int, int)> nodes)
    {
        if (path.Contains(cNode))
        {
            return 0;
        }

        if (nodes.Contains(cNode) && sNode != cNode && fNode != cNode)
        {
            return 0;
        }

        path.Add(cNode);

        if (cNode == fNode)
            return Math.Max(path.Count - 1, curMax);

        foreach ((int x, int y) d in Dirs)
        {
            int nX = cNode.x + d.x;
            int nY = cNode.y + d.y;

            if (nX < 0 || nX >= Input[0].Length || nY < 0 || nY >= Input.Count)
                continue;

            if (Grid[nX, nY] == '#')
                continue;

            (int, int) nNode = (nX, nY);

            curMax = Math.Max(DFS_Dry(sNode, nNode, fNode, new HashSet<(int x, int y)>(path), curMax, nodes), curMax);
        }

        return curMax;
    }

    static void GetEdges(Graph g)
    {
        g.Nodes = g.Nodes.OrderBy(n => n.y).ThenBy(n => n.x).ToList();

        foreach ((int, int) from in g.Nodes)
        {
            foreach ((int, int) to in g.Nodes)
            {
                if (from == to)
                    continue;

                int edgeCount = DFS_Dry(from, from, to, new HashSet<(int x, int y)>(), 0, g.Nodes);
                if (edgeCount > 0)
                    g.Edge.Add((from, to, edgeCount));
            }
        }
    }

    static List<(int x, int y)> FindCrossings()
    {
        List<(int x, int y)> crossings = new List<(int x, int y)>();

        for (int i = 0; i < Input.Count; i++)
        {
            for (int j = 0; j < Input[i].Length; j++)
            {
                if (Grid[j, i] == '#')
                    continue;

                int nextCounter = 0;
                foreach ((int x, int y) d in Dirs)
                {
                    int nX = j + d.x;
                    int nY = i + d.y;

                    if (nX < 0 || nX >= Input[0].Length || nY < 0 || nY >= Input.Count)
                        continue;

                    if (Grid[nX, nY] == '#')
                        continue;

                    nextCounter++;
                }

                if (nextCounter > 2)
                    crossings.Add((j, i));
            }
        }

        crossings.Add((1, 0));
        crossings.Add((Input[0].Length - 2, Input.Count - 1));

        return crossings;
    }

    static void Part2()
    {
        Graph newGraph = new Graph();
        newGraph.Nodes = FindCrossings();
        GetEdges(newGraph);

        

        Console.WriteLine(newGraph.EdgeRunner((1, 0), (127, 123), new HashSet<(int, int)>(), 0));

        //Console.WriteLine(BFS_Dry((1, 0), (Input[0].Length - 2, Input.Count - 1), new HashSet<(int x, int y)>(), 0));
    }

    //Part 1: 2186
    //Part 2: 6802 goal

    public static void Main(string[] args)
    {
        Input = ReadFile();
        CreateGrid();
        Part1();
        Part2();
    }
}

class Graph
{
    public List<(int x, int y)> Nodes;
    public List<((int x, int y) from, (int x, int y) to, int amount)> Edge;
    public HashSet<int> CurMax;

    public Graph()
    {
        Nodes = new List<(int x, int y)>();
        Edge = new List<((int x, int y) from, (int x, int y) to, int amount)>();
        CurMax = new HashSet<int>();
    }

    public int EdgeRunner((int x, int y) sNode, (int x, int y) fNode, HashSet<(int, int)> path, int curMax)
    {
        if (path.Contains(sNode))
            return 0;

        if (sNode == fNode){
            if (CurMax.Add(curMax + 145) && curMax + 145 > 6000)
                Console.WriteLine(curMax + 145);
            return curMax;
        }

        path.Add(sNode);

        List<((int x, int y) from, (int x, int y) to, int amount)> validEdges = Edge.FindAll(e => e.from == sNode || e.to == sNode);
        validEdges = validEdges.OrderByDescending(e => e.amount).ToList();

        int c = 0;
        for (int i = 0; i < validEdges.Count; i++)
        {
            (int, int) from;

            if (validEdges[i].from == sNode)
                from = validEdges[i].to;
            else
                from = validEdges[i].from;

            if (path.Contains(from))
                continue;

            c = Math.Max(EdgeRunner(from, fNode, new HashSet<(int, int)>(path), curMax + validEdges[i].amount), c);
        }

        return c;
    }
}
