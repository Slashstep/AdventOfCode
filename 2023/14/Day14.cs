class Day14{

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

    static List<(int, int)> GetRocks(char c)
    {
        List<(int, int)> rocks = new List<(int, int)>();

        for (int y = 0; y < Input.Count; y++)
        {
            for (int x = 0; x < Input.Count; x++)
            {
                if (Input[y][x] == c)
                    rocks.Add((x, y));
            }
        }

        return rocks;
    }

    static HashSet<(int, int)> GetRocksHash(char c)
    {
        HashSet<(int, int)> rocks = new HashSet<(int, int)>();

        for (int y = 0; y < Input.Count; y++)
        {
            for (int x = 0; x < Input.Count; x++)
            {
                if (Input[y][x] == c)
                    rocks.Add((x, y));
            }
        }

        return rocks;
    }

    static void Part1(){
        HashSet<(int, int)> Cubes = GetRocksHash('#');
        List<(int, int)> Spheres = GetRocks('O');

        //Tilt North
        bool stillRolling = true;
        while (stillRolling)
        {
            stillRolling = false;
            for (int i = 0; i < Spheres.Count; i++)
            {
                if (Spheres[i].Item2 - 1 < 0) continue;
                if (Cubes.Contains((Spheres[i].Item1, Spheres[i].Item2 - 1))) continue;
                if (Spheres.Contains((Spheres[i].Item1, Spheres[i].Item2 - 1))) continue;

                Spheres[i] = (Spheres[i].Item1, Spheres[i].Item2 - 1);
                stillRolling = true;
            }
        }

        int counter = 0;
        foreach ((int, int) v in Spheres)
            counter += Input.Count - v.Item2;

        Console.WriteLine(counter);
    }

    static string GetHashCode(List<(int, int)> list)
    {
        string s = "";
        foreach ((int, int) v in list)
        {
            s += v.Item1.ToString() + "," + v.Item2.ToString() + ";";
        }

        return s;
    }

    static void Part2(){
        HashSet<(int, int)> Cubes = GetRocksHash('#');
        List<(int, int)> Spheres = GetRocks('O');
        Dictionary<string, int> rockDic = new Dictionary<string, int>();

        for (int c = 0; c < 1000000000; c++)
        {
            Spheres = Spheres.OrderBy(v => v.Item1).ThenBy(v => v.Item2).ToList();
            if (rockDic.TryGetValue(GetHashCode(Spheres), out int jump))
            {
                int cycleLength = c - jump;
                while (c + cycleLength < 1000000000)
                    c += cycleLength;
            }
            else
                rockDic.Add(GetHashCode(Spheres), c);

            //Tilt North
            bool stillRolling = true;
            while (stillRolling)
            {
                stillRolling = false;
                for (int i = 0; i < Spheres.Count; i++)
                {
                    if (Spheres[i].Item2 - 1 < 0) continue;
                    if (Cubes.Contains((Spheres[i].Item1, Spheres[i].Item2 - 1))) continue;
                    if (Spheres.Contains((Spheres[i].Item1, Spheres[i].Item2 - 1))) continue;

                    Spheres[i] = (Spheres[i].Item1, Spheres[i].Item2 - 1);
                    stillRolling = true;
                }
            }

            //Tilt West
            stillRolling = true;
            while (stillRolling)
            {
                stillRolling = false;
                for (int i = 0; i < Spheres.Count; i++)
                {
                    if (Spheres[i].Item1 - 1 < 0) continue;
                    if (Cubes.Contains((Spheres[i].Item1 - 1, Spheres[i].Item2))) continue;
                    if (Spheres.Contains((Spheres[i].Item1 - 1, Spheres[i].Item2))) continue;

                    Spheres[i] = (Spheres[i].Item1 - 1, Spheres[i].Item2);
                    stillRolling = true;
                }
            }

            //Tilt South
            stillRolling = true;
            while (stillRolling)
            {
                stillRolling = false;
                for (int i = 0; i < Spheres.Count; i++)
                {
                    if (Spheres[i].Item2 + 1 >= Input.Count) continue;
                    if (Cubes.Contains((Spheres[i].Item1, Spheres[i].Item2 + 1))) continue;
                    if (Spheres.Contains((Spheres[i].Item1, Spheres[i].Item2 + 1))) continue;

                    Spheres[i] = (Spheres[i].Item1, Spheres[i].Item2 + 1);
                    stillRolling = true;
                }
            }

            //Tilt East
            stillRolling = true;
            while (stillRolling)
            {
                stillRolling = false;
                for (int i = 0; i < Spheres.Count; i++)
                {
                    if (Spheres[i].Item1 + 1 >= Input[0].Length) continue;
                    if (Cubes.Contains((Spheres[i].Item1 + 1, Spheres[i].Item2))) continue;
                    if (Spheres.Contains((Spheres[i].Item1 + 1, Spheres[i].Item2))) continue;

                    Spheres[i] = (Spheres[i].Item1 + 1, Spheres[i].Item2);
                    stillRolling = true;
                }
            }

        }

        int counter = 0;
        foreach ((int, int) v in Spheres)
            counter += Input.Count - v.Item2;

        Console.WriteLine(counter);
    }

    //Part 1: 108918
    //Part 2: 100310

    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}