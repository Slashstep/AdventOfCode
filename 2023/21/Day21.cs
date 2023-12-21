using System;
using System.Text;
using System.Xml.Linq;

class Day21{

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
    

    static long FindPath((int, int) sNode, long steps)
    {
        List<(int cost, (int x, int y)node)> openSet = new List<(int, (int, int))>();
        HashSet<(int x, int y)> closedSet = new HashSet<(int x, int y)>();
        long counter = 0;

        (int cost, (int x, int y)node) cNode = (0, (sNode.Item1, sNode.Item2));
        openSet.Add(cNode);

        while (openSet.Count > 0)
        {
            openSet = openSet.OrderBy(n => n.Item1).ToList();
            cNode = openSet[0];
            openSet.RemoveAt(0);
            closedSet.Add(cNode.node);

            if (steps % 2 == cNode.cost % 2)
                counter++;

            if (cNode.cost >= steps)
                continue;

            foreach ((int x, int y) d in Dirs)
            {
                int nX = cNode.node.x + d.x;
                int nY = cNode.node.y + d.y;

                if (closedSet.Contains((nX, nY)))
                    continue;

                if (nX < 0 || nX >= Input[0].Length || nY < 0 || nY >= Input.Count)
                    continue;

                if (Grid[nX, nY] == '#')
                    continue;

                int newCost = cNode.cost + 1;

                (int, int) nNode = (nX, nY);

                if (!openSet.Any(t => t.Item2 == nNode))
                    openSet.Add((newCost, nNode));
            }
        }

        return counter;
    }

    static void Part1()
    {
        Console.WriteLine(FindPath((65, 65), 64));
    }


    static void Part2()
    {
        //Get complete field longs
        long steps = 26501365;
        long fields = 26501365 / Input.Count;
        long maxEven = FindPath((65, 65), 131);
        long maxOdd = FindPath((65, 65), 130);

        //Get amount of total even and odd fields
        long startPair = (fields - 1) * (fields - 1);
        long nonStartPair = fields * fields;

        //Get vertecies
        long straightLeftover = steps % Input.Count;
        long fromLeft = FindPath((0, 65), straightLeftover);
        long fromRight = FindPath((130, 65), straightLeftover);
        long fromUp = FindPath((65, 0), straightLeftover);
        long fromDown = FindPath((65, 130), straightLeftover);

        //Get edges
        long dif = 0;
        //long upLeft = FindPath((0, 0), straightLeftover - dif) * (fields);
        //long upRight = FindPath((130, 0), straightLeftover - dif) * (fields);
        //long downLeft = FindPath((0, 130), straightLeftover - dif) * (fields);
        //long downRight = FindPath((130, 130), straightLeftover - dif) * (fields);

        //Get other edges
        long upLeftBig = FindPath((0, 0), 131) * (fields - 1) * 2;
        long upRightBig = FindPath((130, 0), 131) * (fields - 1) * 2;
        long downLeftBig = FindPath((0, 130), 131) * (fields - 1) * 2;
        long downRightBig = FindPath((130, 130), 131) * (fields - 1) * 2;

        long sum = startPair * maxOdd + nonStartPair * maxEven + fromLeft + fromRight + fromUp + fromDown
                     + upLeftBig + upRightBig + downLeftBig + downRightBig;
        Console.WriteLine(sum);
    }

    //Part 1: 3809
    //Part 2: 629720570456311 goal
    //        629720580960709--
    //        629720710428041


    public static void Main(string[] args)
    {
        Input = ReadFile();
        CreateGrid();
        Part1();
        Part2();
    }
}
