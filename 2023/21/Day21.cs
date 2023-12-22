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
        long fields = steps / Input.Count;
        long maxEven = FindPath((65, 65), 3*Input.Count + 1);
        long maxOdd = FindPath((65, 65), 3*Input.Count);

        //Get amount of total even and odd fields
        long startPair = (fields - 1) * (fields - 1);
        long nonStartPair = (fields) * (fields);

        //Get vertecies
        long fromLeft = FindPath((0, 65), Input.Count -1);
        long fromRight = FindPath((130, 65), Input.Count -1);
        long fromUp = FindPath((65, 0), Input.Count -1);
        long fromDown = FindPath((65, 130), Input.Count -1);
        long edgeSum = fromLeft + fromRight + fromDown + fromUp;

        //Get edges
        long edgeCount = (3 * Input.Count - 3) / 2;
        long upLeft = FindPath((0, 0), edgeCount);
        long upRight = FindPath((130, 0), edgeCount);
        long downLeft = FindPath((0, 130), edgeCount);
        long downRight = FindPath((130, 130), edgeCount);
        long shortEdge = upLeft + upRight + downLeft + downRight;

        //Get other edges
        edgeCount = (Input.Count - 3) / 2;
        long upLeftBig = FindPath((0, 0), edgeCount);
        long upRightBig = FindPath((130, 0), edgeCount);
        long downLeftBig = FindPath((0, 130), edgeCount);
        long downRightBig = FindPath((130, 130), edgeCount);
        long longEdge = upLeftBig + upRightBig + downLeftBig + downRightBig;

        long sum = startPair * maxOdd + nonStartPair * maxEven + (fields - 1) * shortEdge + fields * longEdge + edgeSum;
        Console.WriteLine(sum);
    }

    //Part 1: 3809
    //Part 2: 629720570456311

    public static void Main(string[] args)
    {
        Input = ReadFile();
        CreateGrid();
        Part1();
        Part2();
    }
}
