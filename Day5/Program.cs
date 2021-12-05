using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> input = File.ReadLines(@"input.txt").ToList();
            Console.WriteLine($"Number of dangerous points in Part1: {GetDangerousPoints(input)}");
            Console.WriteLine($"Number of dangerous points in Part2: {GetDangerousPoints(input, false)}");
        }

        static int GetDangerousPoints(List<string> input, bool skip_diagonals = true)
        {
            var intersects = new Dictionary<string, int>();
            foreach (string line in input)
            {
                var segment = (from s in line.Split(", ->".ToArray(), StringSplitOptions.RemoveEmptyEntries)
                               select int.Parse(s)).ToList();
                Vector start = new Vector(segment[0], segment[1]);
                Vector end = new Vector(segment[2], segment[3]);
                bool diagonal = start.X != end.X && start.Y != end.Y;
                if (skip_diagonals && start.X != end.X && start.Y != end.Y) continue;
                foreach (Vector v in GetPointsInLine(start, end))
                {
                    if (intersects.ContainsKey(v.ToString())) intersects[v.ToString()] += 1;
                    else intersects.Add(v.ToString(), 1);
                }
            }
            int num_dangerous_points = intersects.Count(pair => pair.Value > 1);
            intersects.Clear();
            return num_dangerous_points;
        }

        static List<Vector> GetPointsInLine(Vector start, Vector end)
        {
            var points = new List<Vector>();
            var displacement = new Vector(end.X - start.X, end.Y - start.Y);
            int length = 1 + (int)Math.Max(Math.Abs(displacement.X), Math.Abs(displacement.Y));
            var dir = new Vector(Math.Sign(displacement.X), Math.Sign(displacement.Y));
            foreach (int i in Enumerable.Range(0, length))
                points.Add(new Vector(start.X + (i * dir.X), start.Y + (i * dir.Y)));
            return points;
        }
    }
}
