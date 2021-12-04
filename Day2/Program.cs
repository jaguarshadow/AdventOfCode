using System;
using System.IO;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = @"input.txt";

            Console.WriteLine($"Part 1: {Part1(file)}\n");

            Console.WriteLine($"Part 2: {Part2(file)}");

        }

        static double Part1(string file)
        {
            int horizontal = 0;
            int depth = 0;
            foreach (string line in File.ReadLines(file))
            {
                string[] command = line.Split(' ');
                char direction = command[0][0];
                int x = int.Parse(command[1]);

                if (direction == 'f') horizontal += x;
                else if (direction == 'd') depth += x;
                else if (direction == 'u') depth -= x;
            }
            return horizontal * depth;
        }

        static double Part2(string file)
        {
            int horizontal = 0;
            int depth = 0;
            int aim = 0;
            foreach (string line in File.ReadLines(file))
            {
                string[] command = line.Split(' ');
                char direction = command[0][0];
                int x = int.Parse(command[1]);

                if (direction == 'f')
                {
                    horizontal += x;
                    depth += aim * x;
                }
                else if (direction == 'd') aim += x;
                else if (direction == 'u') aim -= x;
            }
            return horizontal * depth;
        }


    }
}
