using System;
using System.Text;
using System.Xml.Linq;

class Day22{

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

    static List<((int, int, int)[] brick, List<(int, int, int)[]> under)> CreateBricks()
    {
        List<((int, int, int)[], List<(int, int, int)[]?>)> Bricks = new List<((int, int, int)[], List<(int, int, int)[]?>)> ();

        foreach (string s in Input)
        {
            //TODO Tilde einfügen
            string[] split = s.Split('~');
            string[] left = split[0].Split(',');
            string[] right = split[1].Split(',');

            (int x, int y, int z) from = (int.Parse(left[0]), int.Parse(left[1]), int.Parse(left[2]));
            (int x, int y, int z) to = (int.Parse(right[0]), int.Parse(right[1]), int.Parse(right[2]));

            if (to.x - from.x > 0)
            {
                (int, int, int)[] brick = new (int, int, int)[to.x + 1 - from.x];
                for (int i = 0; i < to.x + 1 - from.x; i++)
                    brick[i] = (from.x + i, from.y, from.z);
                Bricks.Add((brick, new List<(int, int, int)[]?>()));
            }
            else if (to.y - from.y > 0)
            {
                (int, int, int)[] brick = new (int, int, int)[to.y + 1 - from.y];
                for (int i = 0; i < to.y + 1 - from.y; i++)
                    brick[i] = (from.x, from.y + i, from.z);
                Bricks.Add((brick, new List<(int, int, int)[]?>()));
            }
            if (to.z - from.z > 0)
            {
                (int, int, int)[] brick = new (int, int, int)[to.z + 1 - from.z];
                for (int i = 0; i < to.z + 1 - from.z; i++)
                    brick[i] = (from.x, from.y, from.z +1);
                Bricks.Add((brick, new List<(int, int, int)[]?>()));
            }
        }

        return Bricks;
    }

    static bool IsBrickUnderneath((int x, int y, int z)[] brick, List<((int x, int y, int z)[] brick, List<(int x, int y, int z)[]> under)> Bricks)
    {
        for (int i = 0; i < brick.Length; i++)
        {
            foreach (((int x, int y, int z)[] brick, List<(int x, int y, int z)[]> under) b in Bricks)
            {
                if (b.brick.Last().z != brick[i].z - 1)
                    continue;

                if (brick == b.brick)
                    continue;

                if (b.brick.Contains((brick[i].x, brick[i].y, brick[i].z - 1)))
                    return true;
            }
        }

        return false;
    }

    static void Part1()
    {
        List<((int x, int y, int z)[] brick, List<(int x, int y, int z)[]> under)> Bricks = CreateBricks();
        HashSet<(int x, int y, int z)[]> notToBeRemoved = new HashSet<(int x, int y, int z)[]>();
        HashSet<(int x, int y, int z)[]> toBeRemoved = new HashSet<(int x, int y, int z)[]>();

        //Let them fall

        Bricks = Bricks.OrderBy(b => b.brick[0].z).ToList();

        for (int i = 0; i < Bricks.Count; i++)
        {
            bool isFalling = true;
            while (isFalling)
            {
                isFalling = false;
                if (Bricks[i].brick[0].z <= 1)
                    continue;


                if (IsBrickUnderneath(Bricks[i].brick, Bricks))
                {
                    for (int j = 0; j < Bricks[i].brick.Length; j++)
                    {
                        foreach (((int x, int y, int z)[] brick, List<(int x, int y, int z)[]> under) b in Bricks)
                        {
                            if (b.brick.Last().z != Bricks[i].brick[j].z - 1)
                                continue;

                            if (Bricks[i].brick == b.brick)
                                continue;

                            if (b.brick.Contains((Bricks[i].brick[j].x, Bricks[i].brick[j].y, Bricks[i].brick[j].z - 1)))
                            {
                                if (!Bricks[i].under.Contains(b.brick))
                                    Bricks[i].under.Add(b.brick);
                                //continue;
                            }
                        }
                    }
                    continue;
                }

                (int x, int y, int z)[] nBrick = new (int x, int y, int z)[Bricks[i].brick.Length];
                for (int j = 0; j < Bricks[i].brick.Length; j++)
                    nBrick[j] = (Bricks[i].brick[j].x, Bricks[i].brick[j].y, Bricks[i].brick[j].z - 1);
                Bricks[i] = (nBrick, new List<(int x, int y, int z)[]>());
                isFalling = true;
            }
        }

        Bricks = Bricks.OrderBy(b => b.brick.Last().z).ToList();

        Console.WriteLine(Bricks.Count - 1);
        for (int i = Bricks.Count - 1; i >= 0; i--)
        {
            Console.WriteLine(Bricks[i].brick[0]);
            Console.WriteLine(Bricks[i].under.Count);
            if (Bricks[i].under.Count == 1)
                notToBeRemoved.Add(Bricks[i].under[0]);
            else if (Bricks[i].under.Count > 1)
            {
                for (int j = 0; j < Bricks[i].under.Count; j++)
                {
                    if (!notToBeRemoved.Contains(Bricks[i].under[j]))
                        toBeRemoved.Add(Bricks[i].under[j]);
                }
            }
            if (!notToBeRemoved.Contains(Bricks[i].brick))
                toBeRemoved.Add(Bricks[i].brick);
        }

        Console.WriteLine(toBeRemoved.Count);
    }


    static void Part2()
    {
       
    }

    //Part 1: 497--, 490-- goal 411
    //Part 2: 47671


    public static void Main(string[] args)
    {
        Input = ReadFile();
        Part1();
        Part2();
    }
}
