using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Xml.Serialization;

class Day19{

    static public List<string> Input = new List<string>();
    static public List<Scanner> Scanners = new List<Scanner>();
    static public HashSet<Vector3> Beacons = new HashSet<Vector3>();

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

    static void FindScanners(){
        Scanner current = new Scanner();
        
        foreach (string s in Input){
            if (s.Contains("---")) current = new Scanner();
            else if (s == "") Scanners.Add(current);
            else{
                string[] cords = s.Split(',');
                Vector3 relPos = new Vector3(Convert.ToInt32(cords[0]), Convert.ToInt32(cords[1]), Convert.ToInt32(cords[2]));
                current.DetBeacons.Add(relPos);
            }
        }
        Scanners.Add(current);
    }

    static List<Vector3> GetPermutations(Vector3 src){
        List<Vector3> result = new List<Vector3>();
        int x = (int)src.X;
        int y = (int)src.Y;
        int z = (int)src.Z;

        result.Add(new Vector3(x, y, z));
        result.Add(new Vector3(x, z, y));
        result.Add(new Vector3(y, x, z));
        result.Add(new Vector3(y, z, x));
        result.Add(new Vector3(z, y, x));
        result.Add(new Vector3(z, x, y));

        return result;
    }

    static void CheckBeaconPositions(Scanner s){
        HashSet<Vector3> BeaconDifs = new HashSet<Vector3>();

        //If Beacons empty --> enter them as Scanner 0 Beacons
        if (Beacons.Count == 0){
            foreach (Vector3 v in s.DetBeacons) Beacons.Add(v);
            return;
        }
        
        foreach (Vector3 v1 in s.DetBeacons){
            foreach(Vector3 v2 in s.DetBeacons){
                if (v1 == v2) continue;

                int dX = (int)MathF.Abs(v1.X - v2.X);
                int dY = (int)MathF.Abs(v1.Y - v2.Y);
                int dZ = (int)MathF.Abs(v1.Z - v2.Z);

                foreach (Vector3 v3 in GetPermutations(new Vector3(dX, dY, dZ))){
                    if (!BeaconDifs.Add(v3))
                }

            }
        }
    }
    
    static void Part1(){
        FindScanners();
        foreach (Scanner s in Scanners){
            CheckBeaconPositions(s);
        } 
        Console.Write(Beacons.Count);
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

class Scanner{
    public Vector3 Position;
    public Vector3 Orientation;
    public List<Vector3> DetBeacons;

    public Scanner(){
        Position = Vector3.Zero;
        Orientation = Vector3.Zero;
        DetBeacons = new List<Vector3>();
    }
}
