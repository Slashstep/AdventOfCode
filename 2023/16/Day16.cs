class Day16{

    static public List<string> Input = ReadFile();

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


    static int FollowLaser((int, int) to, (int, int) direction, HashSet<(int, int)> VisitedPoints, HashSet<(int, int, int, int)> LoopChecker)
    {
        //Check if coordinates are valid
        if (to.Item1 < 0 || to.Item1 >= Input[0].Length || to.Item2 < 0 || to.Item2 >= Input.Count)
            return VisitedPoints.Count;

        //Check for loop
        if (!LoopChecker.Add((to.Item1, to.Item2, direction.Item1, direction.Item2)))
            return VisitedPoints.Count;

        int x = to.Item1;
        int y = to.Item2;

        while (Input[y][x] == '.')
        {
            VisitedPoints.Add((x, y));
            x += direction.Item1;
            y += direction.Item2;

            if (x < 0 || x >= Input[0].Length || y < 0 || y >= Input.Count)
                return VisitedPoints.Count;
        }

        VisitedPoints.Add((x, y));

        if (Input[y][x] == '\\' && direction == (1, 0) || Input[y][x] == '/' && direction == (-1, 0))
            FollowLaser((x, y + 1), (0, + 1), VisitedPoints, LoopChecker);
        else if (Input[y][x] == '\\' && direction == (-1, 0) || Input[y][x] == '/' && direction == (1, 0))
            FollowLaser((x, y - 1), (0, -1), VisitedPoints, LoopChecker);
        else if (Input[y][x] == '\\' && direction == (0, 1) || Input[y][x] == '/' && direction == (0, -1))
            FollowLaser((x + 1, y), (1, 0), VisitedPoints, LoopChecker);
        else if (Input[y][x] == '\\' && direction == (0, -1) || Input[y][x] == '/' && direction == (0, 1))
            FollowLaser((x - 1, y), (-1, 0), VisitedPoints, LoopChecker);
        else if (Input[y][x] == '|' && direction == (0, -1) || Input[y][x] == '|' && direction == (0, 1))
            FollowLaser((x, y + direction.Item2), direction, VisitedPoints, LoopChecker);
        else if (Input[y][x] == '-' && direction == (-1, 0) || Input[y][x] == '-' && direction == (1, 0))
            FollowLaser((x + direction.Item1, y), direction, VisitedPoints, LoopChecker);
        else if (Input[y][x] == '|' && direction == (-1, 0) || Input[y][x] == '|' && direction == (1, 0))
        {
            FollowLaser((x, y - 1), (0, -1), VisitedPoints, LoopChecker);
            FollowLaser((x, y + 1), (0, 1), VisitedPoints, LoopChecker);
        }
        else if (Input[y][x] == '-' && direction == (0, 1) || Input[y][x] == '-' && direction == (0, -1))
        {
            FollowLaser((x - 1, y), (-1, 0), VisitedPoints, LoopChecker);
            FollowLaser((x + 1, y), (1, 0), VisitedPoints, LoopChecker);
        }

        return VisitedPoints.Count;
    }

    static void Part1(){
        int counter = FollowLaser((0, 0), (1, 0), new HashSet<(int, int)>(), new HashSet<(int, int, int, int)>());

        Console.WriteLine(counter);
    }

    static void Part2(){
        int curMax = 0;
        //Check all left edges
        for (int i = 0; i < Input.Count; i++)
        {
            int counter = FollowLaser((0, i), (1, 0), new HashSet<(int, int)>(), new HashSet<(int, int, int, int)>());
            if (curMax < counter)
                curMax = counter;
        }
        
        //Check all right edges
        for (int i = 0; i < Input.Count; i++)
        {
            int counter = FollowLaser((0, i), (-1, 0), new HashSet<(int, int)>(), new HashSet<(int, int, int, int)>());
            if (curMax < counter)
                curMax = counter;
        }

        //Check all upper edges
        for (int i = 0; i < Input[0].Length; i++)
        {
            int counter = FollowLaser((i, 0), (0, 1), new HashSet<(int, int)>(), new HashSet<(int, int, int, int)>());
            if (curMax < counter)
                curMax = counter;
        }

        //Check all lower edges
        for (int i = 0; i < Input[0].Length; i++)
        {
            int counter = FollowLaser((Input.Count - 1, i), (0, -1), new HashSet<(int, int)>(), new HashSet<(int, int, int, int)>());
            if (curMax < counter)
                curMax = counter;
        }

        Console.WriteLine(curMax);
    }

    //Part 1: 7498
    //Part 2: 7846

    public static void Main(string[] args){
        Part1();
        Part2();
    }
}
