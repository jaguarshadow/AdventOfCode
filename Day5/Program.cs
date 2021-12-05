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
                foreach (Vector v in GetPointsInLine(start, end, diagonal))
                {
                    if (intersects.ContainsKey(v.ToString())) intersects[v.ToString()] += 1;
                    else intersects.Add(v.ToString(), 1);
                }

            }
            int num_dangerous_points = intersects.Count(pair => pair.Value > 1);
            intersects.Clear();
            return num_dangerous_points;
        }

        static List<Vector> GetPointsInLine(Vector point1, Vector point2, bool diagonal)
        {
            var points_in_line = new List<Vector>();
            int diff;
            if (diagonal)
            {
                var x = Math.Min(point1.X, point2.X);
                var y1 = x == point1.X ? point1.Y : point2.Y;
                var y2 = y1 == point1.Y ? point2.Y : point1.Y;
                bool y_increasing = y1 < y2;
                diff = (int)Math.Abs(point1.X - point2.X);
                foreach (int i in Enumerable.Range((int)x, diff + 1))
                {
                    points_in_line.Add(new Vector(x, y1));
                    x++;
                    if (y_increasing) y1++;
                    else y1--;
                }
            }
            else
            {
                bool horizontal = point1.X != point2.X;
                var min = horizontal ? Math.Min(point1.X, point2.X) : Math.Min(point1.Y, point2.Y);
                diff = horizontal ? (int)Math.Abs(point1.X - point2.X) : (int)Math.Abs(point1.Y - point2.Y);
                foreach (int i in Enumerable.Range((int)min, (int)diff + 1))
                {
                    Vector v = horizontal ? new Vector(i, point1.Y) : new Vector(point1.X, i);
                    points_in_line.Add(v);
                }
            }
            return points_in_line;
        }
    }
}
