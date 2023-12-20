using System;
using System.Text;
using System.Xml.Linq;

class Day17{

    static public List<string> Input = new List<string>();
    static public int[,] Grid;
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
        Grid = new int[Input[0].Length, Input.Count];
        for (int i = 0; i < Input.Count; i++)
        {
            for (int j = 0; j < Input[i].Length; j++)
            {
                Grid[j, i] = int.Parse(Input[i][j].ToString());
            }
        }
    }
    

    static void FindPath((int, int) sNode, (int, int) fNode)
    {
        List<(int val, (int x, int y, int dx, int dy, int s))> openSet = new List<(int, (int, int, int, int, int))>();
        HashSet<(int, int, int, int)> closedSet = new HashSet<(int, int, int, int)>();

        (int cost, (int x, int y, int dx, int dy, int s)node) cNode = (0, (sNode.Item1, sNode.Item2, 0, 0, 0));
        openSet.Add(cNode);

        while (openSet.Count > 0)
        {
            openSet = openSet.OrderBy(n => n.Item1).ToList();
            cNode = openSet[0];
            openSet.RemoveAt(0);

            closedSet.Add((cNode.node.x, cNode.node.y, cNode.node.dx, cNode.node.dy));
            Console.WriteLine(cNode);

            if (cNode.node.x == fNode.Item1 && cNode.node.y == fNode.Item2)
            {
                Console.WriteLine(cNode.cost);
                return;
            }

            foreach ((int x, int y) d in Dirs)
            {
                int nX = cNode.node.x + d.x;
                int nY = cNode.node.y + d.y;

                if (nX < 0 || nX >= Input[0].Length || nY < 0 || nY >= Input.Count)
                    continue;

                if (cNode.node.dx * d.x < 0 || cNode.node.dy * d.y < 0)
                    continue;

                int newCost = cNode.cost + Grid[nX, nY];
                int s = 0;
                if (cNode.node.dx == d.x && cNode.node.dy == d.y)
                    s = cNode.node.s + 1;

                (int, int, int, int, int) nNode = (nX, nY, d.x, d.y, s);

                if (closedSet.Contains((nX, nY, d.x, d.y)))
                    continue;

                if (s < 3)
                {
                    if (!openSet.Any(t => t.Item2 == nNode))
                        openSet.Add((newCost, nNode));
                }
            }
        }
    }

    static void Part1()
    {
        FindPath((0, 0), (Input[0].Length - 1, Input.Count - 1));
    }


    static void Part2()
    {

    }

    //Part 1: 738++ soll 742
    //Part 2: soll 918

    public static void Main(string[] args)
    {
        Input = ReadFile();
        CreateGrid();
        Part1();
        Part2();
    }
}
