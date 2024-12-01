using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Xml.Serialization;

class Node{
    public Node? Left {get; set;}
    public Node? Right {get; set;}
    public Node? Parent {get; set;}
    public int? Value {get; set;}

    public Node(Node? left, Node? right, Node? parent){
        Left = left;
        Right = right;
        Parent = parent;
    }

    public Node(int value){
        Value = value;
    }

    public void PrintNode(int depth)
    {
        if (this == null) return;

        // Indent based on the depth level
        Console.Write(new string(' ', depth * 2));

        if (Value != null)
        {
            // Print the regular number
            Console.WriteLine(Value);
        }
        else
        {
            // Print the pair
            Console.WriteLine("|");
            Left.PrintNode(depth + 1);
            Right.PrintNode(depth + 1);
            Console.WriteLine(new string(' ', depth * 2) + "/");
        }
    }
}

class Day18{

    static public List<string> Input = new List<string>();

    static List<string> ReadFile(){
        List<string> lines = new List<string>();

        try{
            if (File.Exists("input.txt"))
                lines.AddRange(File.ReadAllLines("input.txt"));
            else
                Console.WriteLine("The file does not exist.");
        }
        catch (Exception ex){
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        
        return lines;
    }

    static Node Input2Tree(string input){
        Stack<Node> nodeStack = new Stack<Node>();
        Node returnNode = null;

        for (int i = 0; i < input.Length; i++){
            char ch = input[i];

            if (ch == '['){
                //Start a new pair
                Node newNode = new Node(null, null, null);
                if (nodeStack.Count > 0){
                    if (nodeStack.Peek().Left == null) nodeStack.Peek().Left = newNode;
                    else nodeStack.Peek().Right = newNode;

                    newNode.Parent = nodeStack.Peek();
                }
                
                nodeStack.Push(newNode);
            }
            else if (char.IsDigit(ch)){
                int val = ch - '0';
                Node newNode = new Node(val);
                if (nodeStack.Count > 0){
                    if (nodeStack.Peek().Left == null) nodeStack.Peek().Left = newNode;
                    else nodeStack.Peek().Right = newNode;

                    newNode.Parent = nodeStack.Peek();
                }
            }
            else if (ch == ']'){
                if (nodeStack.Count == 1) returnNode = nodeStack.Pop();
                else nodeStack.Pop();
            }
        }

        return returnNode;
    }

    static Node Addition(Node left, Node right){
        Node newParent = new Node(left, right, null);
        left.Parent = newParent;
        right.Parent = newParent;

        return newParent;
    }

    static bool Explode(Node node, int depth){

        if (node == null) return false;

        if (depth == 4 && node.Left?.Value != null && node.Right?.Value != null){
            AddToLeftMost(node.Parent, node, (int)node.Left.Value);
            AddToRightMost(node.Parent, node, (int)node.Right.Value);
            node.Left = null;
            node.Right = null;
            node.Value = 0;
            Console.WriteLine("Explode");
            return true;
        }
        
        return Explode(node.Left, depth + 1) || Explode(node.Right, depth + 1);
    }

    static bool Split(Node node){
        if (node == null) return false;

        if (node.Value > 9){
            node.Left = new Node((int)node.Value / 2);
            node.Left.Parent = node;
            node.Right = new Node((int)(node.Value +1) /2);
            node.Right.Parent = node;
            node.Value = null;
            Console.WriteLine("Split");
            return true;
        }

        return Split(node.Left) || Split(node.Right);
    }

    static void AddToLeftMost(Node parent, Node currentNode, int value)
{
    Node temp = parent;
    
    // Traverse up to the parent to find the nearest left node
    while (temp != null && temp.Left == currentNode)
    {
        currentNode = temp;
        temp = temp.Parent;
    }

    if (temp == null) return; // No left neighbor found
    
    // Go to the rightmost child of the left subtree
    temp = temp.Left;
    while (temp.Right != null) temp = temp.Right;

    // Add the value to the nearest left regular number
    if (temp.Value.HasValue) temp.Value += value;
}

static void AddToRightMost(Node parent, Node currentNode, int value)
{
    Node temp = parent;

    // Traverse up to the parent to find the nearest right node
    while (temp != null && temp.Right == currentNode)
    {
        currentNode = temp;
        temp = temp.Parent;
    }

    if (temp == null) return; // No right neighbor found
    
    // Go to the leftmost child of the right subtree
    temp = temp.Right;
    while (temp.Left != null) temp = temp.Left;

    // Add the value to the nearest right regular number
    if (temp.Value.HasValue) temp.Value += value;
}

static void DeleteEmptyNodes(Node node)
{
    if (node == null) return;

    // Recursively check and delete empty nodes in the left subtree
    if (node.Left != null) DeleteEmptyNodes(node.Left);

    // Recursively check and delete empty nodes in the right subtree
    if (node.Right != null) DeleteEmptyNodes(node.Right);

    // If the current node is empty (no left, right, and no value), delete it
    if (node.Left == null && node.Right == null && node.Value == null)
    {
        if (node.Parent != null)
        {
            // Determine if this node is the left or right child and remove it
            if (node.Parent.Left == node)
            {
                node.Parent.Left = null;
            }
            else if (node.Parent.Right == node)
            {
                node.Parent.Right = null;
            }
        }
    }
}

    static void PrintNode(Node node)
    {
        if (node == null) return;
        if (node.Value != null)
        {
            // Print the regular number
            Console.Write(node.Value);
        }
        else
        {
            // Print the pair with brackets
            Console.Write("[");
            PrintNode(node.Left);
            Console.Write(",");
            PrintNode(node.Right);
            Console.Write("]");
        }
    }

    static void Part1(){
        Node current = new Node(null, null, null);
        for (int i = 0; i < Input.Count; i++){
            Node toAdd = Input2Tree(Input[i]);
            current = Addition(current, toAdd);

            bool change;
            do{
                change = Explode(current, 0) || Split(current);
            }
            while (change);

            DeleteEmptyNodes(current);
        }

        PrintNode(current);
    }

    static void Part2(){

    }

    //Part 1: 
    //Part 2: 
              
    public static void Main(string[] args){
        Input = ReadFile();
        Part1();
        Part2();
    }
}

