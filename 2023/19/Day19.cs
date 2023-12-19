using System.Text.RegularExpressions;

class Day19{

    static public List<string> Input = new List<string>();
    static public List<Workflow> workflows = new List<Workflow>();
    static public List<Part> parts = new List<Part>();
    static public List<Part> accepted = new List<Part>();
    static public List<RangePart> rangeParts = new List<RangePart>();
    static public List<RangePart> rangeAccepted = new List<RangePart>();

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

    static void PrepareInput()
    {
        int index = 0;
        while (Input[index] != "")
        {
            string[] sep = Input[index].Split("{");
            workflows.Add(new Workflow(sep[0], sep[1].Substring(0, sep[1].Length - 1)));
            index++;
        }

        index++;
        for (int i = index; i < Input.Count; i++)
        {
            List<int> nums = new List<int>();
            string[] sep = Input[i].Substring(1, Input[i].Length - 2).Split(",");
            foreach (string s in sep)
            {
                string[] sepNum = s.Split("=");
                nums.Add(int.Parse(sepNum[1]));
            }
            parts.Add(new Part(nums[0], nums[1], nums[2], nums[3]));
        }
    }

    static void CheckPart(Workflow? cwf, Part cp)
    {
        if (cwf == null)
            return;

        string nextWorkflowName = cwf.Evaluation(cp);

        if (nextWorkflowName == "A")
        {
            accepted.Add(cp);
            return;
        }
        else if (nextWorkflowName == "R")
            return;

        Workflow? nextWorkflow = workflows.Find(w => w.Name == nextWorkflowName);
        CheckPart(nextWorkflow, cp);
    }


    static void Part1()
    {
        Workflow? start = workflows.Find(w => w.Name == "in");

        foreach (Part p in parts)
            CheckPart(start, p);

        long counter = 0;
        foreach (Part p in accepted)
            counter += p.x + p.m + p.a + p.s;

        Console.WriteLine(counter);
    }

    static void CheckRange(Workflow? cwf, RangePart rcp)
    {
        if (cwf == null)
            return;

        foreach ((string, RangePart) pair in cwf.CreateNewRanges(rcp))
        {
            if ((pair.Item1) == "A")
            {
                rangeAccepted.Add(pair.Item2);
                continue;
            }
            else if ((pair.Item1) == "R")
                continue;

            Workflow? nextWorkflow = workflows.Find(w => w.Name == pair.Item1);
            CheckRange(nextWorkflow, pair.Item2);
        }
    }

    static void Part2()
    {
        RangePart startRange = new RangePart((1, 4000), (1, 4000), (1, 4000), (1, 4000));
        Workflow? start = workflows.Find(w => w.Name == "in");

        CheckRange(start, startRange);

        long counter = 0;
        foreach (RangePart rp in rangeAccepted)
            counter += rp.Permutations();

        Console.WriteLine(counter);
    }

    //Part 1: 421983
    //Part 2: 129249871135292

    public static void Main(string[] args){
        Input = ReadFile();
        PrepareInput();
        Part1();
        Part2();
    }
}

class Workflow
{
    public string Name;
    public string Formula;

    public Workflow(string name, string formula)
    {
        Name = name;
        Formula = formula;
    }

    public List<(string, RangePart)> CreateNewRanges(RangePart input)
    {
        List<(string, RangePart)> newRanges = new List<(string, RangePart)>();

        string pattern = @"(\w+)([<>])(\d+):(\w+)";
        Regex r = new Regex(pattern);

        MatchCollection matches = r.Matches(Formula);
        RangePart cRange = input;

        foreach (Match m in matches)
        {
            string variable = m.Groups[1].Value;
            string comp = m.Groups[2].Value;
            int threshold = int.Parse(m.Groups[3].Value);
            string action = m.Groups[4].Value;

            (long, long) newRange;
            (long, long) leftRange;

            switch (variable)
            {
                case "x":
                    if (comp == ">")
                    {
                        if (cRange.x.Item2 <= threshold)
                            continue;

                        newRange = (Math.Max(cRange.x.Item1, threshold + 1), cRange.x.Item2);
                        leftRange = (Math.Min(cRange.x.Item1, threshold), Math.Min(cRange.x.Item2, threshold));
                    }
                    else
                    {
                        if (cRange.x.Item1 >= threshold)
                            continue;

                        newRange = (cRange.x.Item1, Math.Min(cRange.x.Item2, threshold - 1));
                        leftRange = (Math.Max(cRange.x.Item1, threshold), Math.Max(cRange.x.Item2, threshold));
                    }

                    newRanges.Add((action, new RangePart(newRange, cRange.m, cRange.a, cRange.s)));
                    cRange.x = leftRange;
                    break;
                case "m":
                    if (comp == ">")
                    {
                        if (cRange.m.Item2 <= threshold)
                            continue;

                        newRange = (Math.Max(cRange.m.Item1, threshold + 1), cRange.m.Item2);
                        leftRange = (Math.Min(cRange.m.Item1, threshold), Math.Min(cRange.m.Item2, threshold));
                    }
                    else
                    {
                        if (cRange.m.Item1 >= threshold)
                            continue;

                        newRange = (cRange.m.Item1, Math.Min(cRange.m.Item2, threshold - 1));
                        leftRange = (Math.Max(cRange.m.Item1, threshold), Math.Max(cRange.m.Item2, threshold));
                    }

                    newRanges.Add((action, new RangePart(cRange.x, newRange, cRange.a, cRange.s)));
                    cRange.m = leftRange;
                    break;
                case "a":
                    if (comp == ">")
                    {
                        if (cRange.a.Item2 <= threshold)
                            continue;

                        newRange = (Math.Max(cRange.a.Item1, threshold + 1), cRange.a.Item2);
                        leftRange = (Math.Min(cRange.a.Item1, threshold), Math.Min(cRange.a.Item2, threshold));
                    }
                    else
                    {
                        if (cRange.a.Item1 >= threshold)
                            continue;

                        newRange = (cRange.a.Item1, Math.Min(cRange.a.Item2, threshold - 1));
                        leftRange = (Math.Max(cRange.a.Item1, threshold), Math.Max(cRange.a.Item2, threshold));
                    }

                    newRanges.Add((action, new RangePart(cRange.x, cRange.m, newRange, cRange.s)));
                    cRange.a = leftRange;
                    break;
                case "s":
                    if (comp == ">")
                    {
                        if (cRange.s.Item2 <= threshold)
                            continue;

                        newRange = (Math.Max(cRange.s.Item1, threshold + 1), cRange.s.Item2);
                        leftRange = (Math.Min(cRange.s.Item1, threshold), Math.Min(cRange.s.Item2, threshold));
                    }
                    else
                    {
                        if (cRange.s.Item1 >= threshold)
                            continue;

                        newRange = (cRange.s.Item1, Math.Min(cRange.s.Item2, threshold - 1));
                        leftRange = (Math.Max(cRange.s.Item1, threshold), Math.Max(cRange.s.Item2, threshold));
                    }

                    newRanges.Add((action, new RangePart(cRange.x, cRange.m, cRange.a, newRange)));
                    cRange.s = leftRange;
                    break;
                default:
                    newRange = (0, 0);
                    break;
            }
        }

        newRanges.Add((Formula.Split(",").Last(), cRange));

        return newRanges;
    }

    public string Evaluation(Part input)
    {
        string pattern = @"(\w+)([<>])(\d+):(\w+)";
        Regex r = new Regex(pattern);

        MatchCollection matches = r.Matches(Formula);

        foreach (Match m in matches)
        {
            string variable = m.Groups[1].Value;
            string comp = m.Groups[2].Value;
            int threshold = int.Parse(m.Groups[3].Value);
            string action = m.Groups[4].Value;

            if (EvaluateCondition(variable, comp, threshold, input))
                return action;
        }

        return Formula.Split(",").Last();
    }

    bool EvaluateCondition(string variable, string comparison, int threshold, Part p)
    {
        switch (variable)
        {
            case "x":
                return comparison == ">" ? p.x > threshold : p.x < threshold;
            case "m":
                return comparison == ">" ? p.m > threshold : p.m < threshold;
            case "a":
                return comparison == ">" ? p.a > threshold : p.a < threshold;
            case "s":
                return comparison == ">" ? p.s > threshold : p.s < threshold;
            default:
                return false;
        }
    }
}

class Part
{
    public int x;
    public int m;
    public int a;
    public int s;

    public Part(int x, int m, int a, int s)
    {
        this.x = x;
        this.m = m;
        this.a = a;
        this.s = s;
    }
}

class RangePart
{
    public (long, long) x;
    public (long, long) m;
    public (long, long) a;
    public (long, long) s;

    public RangePart((long, long) x, (long, long) m, (long, long) a, (long, long) s)
    {
        this.x = x;
        this.m = m;
        this.a = a;
        this.s = s;
    }

    public void Print()
    {
        Console.WriteLine("{0}, {1}, {2}, {3}", x, m, a, s);
    }

    public long Permutations()
    {
        return (x.Item2 - x.Item1 + 1) * (m.Item2 - m.Item1 + 1) * (a.Item2 - a.Item1 + 1) * (s.Item2 - s.Item1 + 1);
    }
}
