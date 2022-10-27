using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Day9
{
    class Program
    {
        static void Main(string[] args)
        {
            var biginput = File.ReadLines(@"input.txt").ToList();

            var input = new List<string> { "2199943210", "3987894921", "9856789892", "8767896789", "9899965678" };

            Console.WriteLine($"Part 1: {CalculateRisk(input)}");
            Console.WriteLine($"Part 2: {FindLargestBasins(input)}");
        }

        public static int CalculateRisk(List<string> input)
        {
            var lowPoints = new List<int>();
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    // Compare left num
                    if (j != 0 && int.Parse(input[i][j].ToString()) >= int.Parse(input[i][j - 1].ToString())) continue;
                    // Compare right num
                    if (j < input[i].Length - 1 && int.Parse(input[i][j].ToString()) >= int.Parse(input[i][j + 1].ToString())) continue;
                    // Compare top num
                    if (i != 0 && int.Parse(input[i][j].ToString()) >= int.Parse(input[i - 1][j].ToString())) continue;
                    // Compare bottom num
                    if (i < input.Count - 1 && int.Parse(input[i][j].ToString()) >= int.Parse(input[i + 1][j].ToString())) continue;

                    lowPoints.Add(int.Parse(input[i][j].ToString()));
                }
            }
            var risk = lowPoints.Select(x => x + 1).ToList().Sum();
            return risk;
        }

        public static int FindLargestBasins(List<string> input)
        {
            var basins = new List<int>();
            int current_basin_size = 0;

            var charArrays = new List<char[]>();
            foreach (var s in input) charArrays.Add(s.ToCharArray());

            for (int row = 0; row < charArrays.Count; row++)
            {
                int index = 0;
                var row_offset = 0;
                while (index < charArrays[row].Count())
                {
                    if (charArrays[row][index] == 'x')
                    {
                        index++;
                        continue;
                    }
                    else if (charArrays[row][index] == '9')
                    {
                        if (current_basin_size > 0) basins.Add(current_basin_size);
                        current_basin_size = 0;
                    }
                    else
                    {
                        current_basin_size++;
                        charArrays[row][index] = 'x';
                        row_offset++;
                        while (row_offset + row < input.Count)
                        {
                            if (charArrays[row_offset + row][index] == '9') break;
                            else if (charArrays[row_offset + row][index] != 'x')
                            {
                                current_basin_size++;
                                charArrays[row_offset + row][index] = 'x';
                            }
                            row_offset++;
                        }
                    }
                    row_offset = 0;
                    index++;

                }
            }
            basins.Sort();
            return 0;
        }
    }
}
