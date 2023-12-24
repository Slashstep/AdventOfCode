class Day24{

    static public List<string> Input = new List<string>();
    static double MinBound = 200000000000000;
    static double MaxBound = 400000000000000;

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

    static List<((double x, double y, double z) stone, (double dx, double dy, double dz) dir)> CollectHailstones(){
        List<((double x, double y, double z) stone, (double dx, double dy, double dz) dir)> hailstones = new List<((double x, double y, double z) stone, (double dx, double dy, double dz) dir)>();

        foreach (string s in Input){
            string[] leftRight = s.Split(" @ ");
            string[] left = leftRight[0].Split(", ");
            string[] right = leftRight[1].Split(", ");

            (double, double, double) stone = (double.Parse(left[0]), double.Parse(left[1]), double.Parse(left[2]));
            (double, double, double) dir = (double.Parse(right[0]), double.Parse(right[1]), double.Parse(right[2]));
            hailstones.Add((stone, dir));
        }

        return hailstones;
    }

    static bool DoCollide(((double x, double y, double z) s, (double dx, double dy, double dz) d) hail1, 
                            ((double x, double y, double z) s, (double dx, double dy, double dz) d) hail2)
    {
        double ddx = hail1.d.dx / hail2.d.dx;
        double ddy = hail1.d.dy / hail2.d.dy;
        double ddz = hail1.d.dz / hail2.d.dz;

        if (ddx == ddy && ddx == ddz && ddy == ddz)
            return false;

        double num = hail2.s.x - hail1.s.x + (hail1.s.y - hail2.s.y) * hail2.d.dx / hail2.d.dy;
        double den = hail1.d.dx - hail1.d.dy * hail2.d.dx / hail2.d.dy;
        double lambda = num / den;

        if (lambda < 0)
            return false;

        double posX = hail1.s.x + lambda * hail1.d.dx;
        double posY = hail1.s.y + lambda * hail1.d.dy;

        double m1 = (posX - hail2.s.x) / hail2.d.dx;

        if (m1 < 0)
            return false;

        if (MinBound <= posX && posX <= MaxBound && MinBound <= posY && posY <= MaxBound)
            return true;

        return false;
    }

    static void Part1()
    {
        List<((double x, double y, double z) stone, (double dx, double dy, double dz) dir)> hailstones = CollectHailstones();

        double counter = 0;
        for (int i = 0; i < hailstones.Count - 1; i++)
        {
            for (int j = i + 1; j < hailstones.Count; j++){
                if (DoCollide(hailstones[i], hailstones[j]))
                    counter++;
            }
        }

        Console.WriteLine(counter);
    }


    static void Part2()
    {
       
    }

    //Part 1: 24627
    //Part 2: 527310134398217 goal


    public static void Main(string[] args)
    {
        Input = ReadFile();
        Part1();
        Part2();
    }
}
