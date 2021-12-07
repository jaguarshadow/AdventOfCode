using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadLines(@"input.txt").ToArray();
            var crabs1 = new List<int>() { 16, 1, 2, 0, 4, 2, 7, 1, 2, 14 };
            var crabs = input[0].Split(',').Select(n => int.Parse(n)).ToList();
            
            Console.WriteLine($"Part 1: {CalculateLeastFuel(crabs, true)}");
            Console.WriteLine($"Part 2: {CalculateLeastFuel(crabs, false)}");
        }
        public static int CalculateLeastFuel(List<int> positions, bool constant)
        {
            var min = positions.Min(c => c);
            var max = positions.Max(c => c);

            var least = int.MaxValue;
            for (int position = 0; position <= max; position++)
            {
                double fuel = 0;
                foreach (int crab in positions)
                {
                    double steps = Math.Abs(position - crab);
                    fuel += constant ? steps : Math.Floor((steps * (steps + 1))/2);
                }
                least = Math.Min(least, (int)fuel);
            }
            return least;
        }
    }
}
