class Day20{

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

    static List<Button> GetButtons()
    {
        List<Button> Buttons = new List<Button>();

        foreach (string s in Input)
        {
            string[] leftRight = s.Split(" -> ");
            string[] right = leftRight[1].Split(", ");

            Button b;
            if (leftRight[0] == "broadcaster")
            {
                b = new Broadcast("broadcaster");
                b.Connections = right.ToList();
            }
            else if (leftRight[0][0] == '%')
            {
                b = new FlipFlop(leftRight[0].Substring(1));
                b.Connections = right.ToList();
            }
            else
            {
                b = new Conjuction(leftRight[0].Substring(1));
                b.Connections = right.ToList();
            }

            Buttons.Add(b);
        }

        foreach (Conjuction c in Buttons.OfType<Conjuction>())
        {
            List<Button> ins = new List<Button>();

            foreach (Button b in Buttons)
            {
                if (b.Connections.Contains(c.Name))
                    ins.Add(b);
            }

            c.Inputs = ins;
        }

        return Buttons;
    }

    static void Part1()
    {
        List<Button> Buttons = GetButtons();

        long lCounter = 0;
        long hCounter = 0;
        for (int i = 0; i < 1000; i++)
        {
            Queue<(string, string)> pulseCheck = new Queue<(string, string)>();

            Button? start = Buttons.Find(b => b.Name == "broadcaster");

            if (start == null) return;

            pulseCheck.Enqueue(("low", start.Name));

            while (pulseCheck.Count > 0)
            {
                (string, string) nextAction = pulseCheck.Dequeue();

                if (nextAction.Item1 == "low") lCounter++;
                else hCounter++;

                Button? nButton = Buttons.Find(b => b.Name == nextAction.Item2);

                if (nButton == null)
                    continue;

                List<(string, string)> process = nButton.ProcessPulse(nextAction.Item1);

                if (process.Count == 0)
                    continue;

                foreach ((string, string) s in process)
                    pulseCheck.Enqueue(s);
            }
        }

        Console.WriteLine(lCounter * hCounter);
    }

    static void Part2()
    {
        List<Button> Buttons = GetButtons();
        List<long> cycles = new List<long>();

        long bCounter = 0;

        //Get max cycles length
        Button? t = Buttons.Find(g => g.Connections.Contains("rx"));
        if (t == null) return;

        int i = 0;
        foreach (Button b in Buttons)
        {
            if (b.Connections.Contains(t.Name))
                i++;
        }

        while (cycles.Count < i)
        {
            bCounter++;
            Queue<(string, string)> pulseCheck = new Queue<(string, string)>();

            Button? start = Buttons.Find(b => b.Name == "broadcaster");

            if (start == null) return;

            pulseCheck.Enqueue(("low", start.Name));

            while (pulseCheck.Count > 0)
            {
                (string, string) nextAction = pulseCheck.Dequeue();

                if (nextAction == ("high", t.Name))
                    cycles.Add(bCounter);

                Button? nButton = Buttons.Find(b => b.Name == nextAction.Item2);

                if (nButton == null)
                    continue;

                List<(string, string)> process = nButton.ProcessPulse(nextAction.Item1);

                if (process.Count == 0)
                    continue;

                foreach ((string, string) s in process)
                    pulseCheck.Enqueue(s);
            }
        }

        while (cycles.Count > 1)
        {
            cycles[0] = lcm(cycles[0], cycles[1]);
            cycles.RemoveAt(1);
        }

        Console.WriteLine(cycles[0]);
    }


    static long gcf(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    static long lcm(long a, long b)
    {
        return (a / gcf(a, b)) * b;
    }

    //Part 1: 841763884
    //Part 2: 246006621493687

    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}

class Button
{
    public string Name;
    public List<string> Connections;
    public string lastPulse = "low";

    public Button (string name)
    {
        Name = name;
        Connections = new List<string>();
    }

    public virtual List<(string, string)> ProcessPulse(string pulse)
    {
        List<(string, string)> nextAction = new List<(string, string)>();
        return nextAction;
    }
}

class FlipFlop : Button
{
    public bool isOn = false;

    public FlipFlop(string name) : base(name)
    {
    }

    public override List<(string, string)> ProcessPulse(string pulse)
    {
        if (pulse == "high")
            return base.ProcessPulse(pulse);

        if (!isOn)
        {
            isOn = true;
            lastPulse = "high";
        }
        else
        {
            isOn = false;
            lastPulse = "low";
        }

        List<(string, string)> nextAction = new List<(string, string)>();

        foreach (string b in Connections)
            nextAction.Add((lastPulse, b));

        return nextAction;
    }
}

class Conjuction : Button
{
    public List<Button> Inputs;

    public Conjuction(string name) : base(name)
    {
        Inputs = new List<Button>();
    }

    public override List<(string, string)> ProcessPulse(string pulse)
    {
        bool allHighs = true;
        foreach (Button b in Inputs)
        {
            if (b.lastPulse == "low")
            {
                allHighs = false;
                break;
            }
        }

        if (allHighs)
            lastPulse = "low";
        else
            lastPulse = "high";

        List<(string, string)> nextAction = new List<(string, string)>();

        foreach (string b in Connections)
            nextAction.Add((lastPulse, b));

        return nextAction;
    }
}

class Broadcast : Button
{
    public Broadcast(string name) : base(name)
    {
    }

    public override List<(string, string)> ProcessPulse(string pulse)
    {
        lastPulse = pulse;

        List<(string, string)> nextAction = new List<(string, string)>();

        foreach (string b in Connections)
            nextAction.Add((pulse, b));

        return nextAction;
    }
}
