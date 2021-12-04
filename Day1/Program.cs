using System;
using System.IO;
using System.Linq;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] nums = File.ReadAllLines(@"input.txt").Select(x => int.Parse(x)).ToArray();

            Console.WriteLine($"\n  Evaluating {nums.Length} depth measurements...");

            Console.WriteLine($"\n    Part 1: {Part1(nums)}");

            Console.WriteLine($"\n    Part 2: {Part2(nums)}\n");
        }

        static int Part1(int[] nums)
        {
            int count = 0;
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] > nums[i - 1]) count++;
            }
            return count;
        }

        static int Part2(int[] nums)
        {
            int count = 0;
            for (int i = 3; i < nums.Length; i++)
            {
                if (nums[i] > nums[i - 3]) count++;
            }
            return count;
        }
    }
}
