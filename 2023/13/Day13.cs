class Day13{

    static public List<string> Input = ReadFile();
    static List<Pattern> Patterns = GetPatterns();

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

    static List<Pattern> GetPatterns(){
        List<Pattern> patterns = new List<Pattern>();
        List<string> lines = new List<string>();

        for (int i = 0; i < Input.Count; i++){
            if (Input[i] == ""){
                patterns.Add(new Pattern(new List<string>(lines)));
                lines.Clear();
            }
            else
            {
                lines.Add(Input[i]);
            }
        }
        patterns.Add(new Pattern(new List<string>(lines)));
        return patterns;
    }

    static void Part1(){
        int counter = 0;
        foreach (Pattern p in Patterns)
            counter += p.TotalValue;

        Console.WriteLine(counter);
    }

    static void Part2(){
        int counter = 0;
        foreach (Pattern p in Patterns)
            counter += p.SmudgeValue;

        Console.WriteLine(counter);
    }

    //Part 1: 42974
    //Part 2: 27587

    public static void Main(string[] args){
        Part1();
        Part2();
    }
}

class Pattern{
    public List<string> Lines = new List<string>();
    public List<string> LinesTurned = new List<string>();
    public int TotalValue;
    public int SmudgeValue;

    public Pattern(List<string> lines){
        Lines = lines;
        LinesTurned = TurnLines();

        int VerticalValue = CheckVertical();
        int HorizontalValue = CheckHorizontal();
        if (VerticalValue > HorizontalValue) TotalValue = VerticalValue;
        else TotalValue = 100 * HorizontalValue;

        int SmudgeVertical = CheckSmudgeVertical();
        int SmudgeHorizontal = CheckSmudgeHorizontal();
        if (SmudgeVertical > SmudgeHorizontal) SmudgeValue = SmudgeVertical;
        else SmudgeValue = 100 * SmudgeHorizontal;
    }

    List<string> TurnLines()
    {
        List<string> lt = new List<string>();

        for (int i = 0; i < Lines[0].Length; i++)
        {
            string s = "";
            for (int y = 0; y < Lines.Count; y++)
                s += Lines[y][i];

            lt.Add(s);
        }

        return lt;
    }

    int CheckVertical()
    {
        List<string> verticalLines = new List<string>();
        List<(int, int)> symmetries = new List<(int, int)>();

        for (int i = 0; i < LinesTurned.Count; i++)
        { 
            if (verticalLines.Count > 0 && verticalLines.Last() == LinesTurned[i])
            {
                for (int j = 0; j < LinesTurned.Count; j++)
                {
                    if (verticalLines.Count - 1 - j < 0 || i + j >= LinesTurned.Count)
                    {
                        symmetries.Add((i, j));
                        break;
                    }

                    if (verticalLines[verticalLines.Count - 1 - j] != LinesTurned[i + j])
                        break;
                }
            }

            verticalLines.Add(LinesTurned[i]);
        }

        symmetries = symmetries.OrderByDescending(t => t.Item2).ToList();

        if (symmetries.Count == 0) return 0;
        else return symmetries[0].Item1;
    }

    int CheckHorizontal()
    {
        List<string> horizontalLines = new List<string>();
        List<(int, int)> symmetries = new List<(int, int)>();

        for (int i = 0; i < Lines.Count; i++)
        {
            if (horizontalLines.Count > 0 && horizontalLines.Last() == Lines[i])
            {
                for (int j = 0; j < Lines.Count; j++)
                {
                    if (horizontalLines.Count - 1 - j < 0 || i + j >= Lines.Count)
                    {
                        symmetries.Add((i, j));
                        break;
                    }

                    if (horizontalLines[horizontalLines.Count - 1 - j] != Lines[i + j])
                        break;
                }
            }

            horizontalLines.Add(Lines[i]);
        }

        symmetries = symmetries.OrderByDescending(t => t.Item2).ToList();

        if (symmetries.Count == 0) return 0;
        else return symmetries[0].Item1;
    }

    int CheckSmudgeVertical()
    {
        List<string> verticalLines = new List<string>();
        List<(int, int)> symmetries = new List<(int, int)>();

        for (int i = 0; i < LinesTurned.Count; i++)
        {
            int smudgeCounter = 0;
            for (int j = 0; j < LinesTurned.Count; j++)
            {
                if (verticalLines.Count - 1 - j < 0 || i + j >= LinesTurned.Count)
                {
                    if (smudgeCounter <= 1)
                    {
                        symmetries.Add((i, j));
                        break;
                    }
                    else break;
                }

                if (verticalLines[verticalLines.Count - 1 - j] != LinesTurned[i + j])
                {
                    for (int k = 0; k < LinesTurned[i + j].Length; k++)
                    {
                        if (verticalLines[verticalLines.Count - 1 - j][k] != LinesTurned[i + j][k])
                            smudgeCounter++;
                    }

                    if (smudgeCounter > 1) break;
                }
            }
            verticalLines.Add(LinesTurned[i]);
        }

        symmetries = symmetries.OrderByDescending(t => t.Item2).ToList();

        if (symmetries.Count == 0) return 0;
        else
        {
            if (symmetries[0].Item1 == TotalValue)
            {
                if (symmetries.Count > 1) return symmetries[1].Item1;
                else return 0;
            }
            else return symmetries[0].Item1;
        }
    }

    int CheckSmudgeHorizontal()
    {
        List<string> horizontalLines = new List<string>();
        List<(int, int)> symmetries = new List<(int, int)>();

        for (int i = 0; i < Lines.Count; i++)
        {
            int smudgeCounter = 0;
            for (int j = 0; j < Lines.Count; j++)
            {
                if (horizontalLines.Count - 1 - j < 0 || i + j >= Lines.Count)
                {
                    if (smudgeCounter <= 1)
                    {
                        symmetries.Add((i, j));
                        break;
                    }
                    else break;
                }

                if (horizontalLines[horizontalLines.Count - 1 - j] != Lines[i + j])
                {
                    for (int k = 0; k < Lines[i + j].Length; k++)
                    {
                        if (horizontalLines[horizontalLines.Count - 1 - j][k] != Lines[i + j][k])
                            smudgeCounter++;
                    }

                    if (smudgeCounter > 1) break;
                }
            }
            horizontalLines.Add(Lines[i]);
        }

        symmetries = symmetries.OrderByDescending(t => t.Item2).ToList();

        if (symmetries.Count == 0) return 0;
        else
        {
            if (symmetries[0].Item1 * 100 == TotalValue)
            {
                if (symmetries.Count > 1) return symmetries[1].Item1;
                else return 0;
            }
            else return symmetries[0].Item1;
        }
    }
}
