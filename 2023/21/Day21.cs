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
    

    static void FindPath((int, int) sNode, int steps)
    {
        List<(int cost, (int x, int y))> openSet = new List<(int, (int, int))>();
        long counter = 0;

        (int cost, (int x, int y)node) cNode = (0, (sNode.Item1, sNode.Item2));
        openSet.Add(cNode);

        while (openSet.Count > 0)
        {
            openSet = openSet.OrderBy(n => n.Item1).ToList();
            cNode = openSet[0];
            openSet.RemoveAt(0);

            Console.WriteLine(cNode);

            if (cNode.cost >= steps)
            {
                counter++;
                continue;
            }

            foreach ((int x, int y) d in Dirs)
            {
                int nX = cNode.node.x + d.x;
                int nY = cNode.node.y + d.y;

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

        Console.WriteLine(counter);
    }

    static void Part1()
    {
        
        
        FindPath((65, 65), 64);
    }


    static void Part2()
    {
        
    }

    //Part 1: 3809
    //Part 2: 918

    public static void Main(string[] args)
    {
        Input = ReadFile();
        CreateGrid();
        Part1();
        Part2();
    }
}
