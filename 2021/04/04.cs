var inputFile = File.ReadAllLines("./input.txt");
var input = new List<string>(inputFile);

foreach (int i in TextToInt(input)){
    Console.WriteLine(i.ToString());
}

public List<int> TextToInt(string input){
    List<int> inputInt = new List<int>();

    int curInt = 0;
    foreach (char c in input){
        if (c ==","){
            inputInt.Add(curInt);
            curInt = 0;
        }
        
        curInt = curInt*10 + Int32.Parse(c);
    }

    return inputInt;
}


