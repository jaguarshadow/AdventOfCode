using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day3
{
    class Program
    {

        static void Main(string[] args)
        {
            List<string> binary_numbers = File.ReadLines(@"input.txt").ToList();

            Console.WriteLine($"gamma * epsilon = {GetPowerConsumption(binary_numbers)}");

            Console.WriteLine($"co2 * oxygen = {GetLifeSupportRating(binary_numbers)}");
        }

        static int GetPowerConsumption(List<string> binary_numbers)
        {
            string common_bits = GetCommonBits(binary_numbers);
            int gamma = Convert.ToInt32(common_bits, 2);
            int epsilon = Convert.ToInt32(string.Join("", common_bits.Select(c => c == '0' ? '1' : '0')), 2);
            return gamma * epsilon;
        }

        static int GetLifeSupportRating(List<string> input)
        {
            var o2_candidates = new List<string>(input);
            var co2_candidates = new List<string>(input);
            var non_matches = new List<string>();
            for (int i = 0; i < input[0].Length; i++)
            {
                // Handle Oxygen
                if (o2_candidates.Count > 1)
                {
                    var o2_common = GetCommonBits(o2_candidates, true);
                    non_matches = o2_candidates.Where(x => x[i] != o2_common[i]).ToList();
                    foreach (var x in non_matches) if (o2_candidates.Count > 1) o2_candidates.Remove(x);
                }
                // Handle CO2
                if (co2_candidates.Count > 1)
                {
                    var co2_common = GetCommonBits(co2_candidates, true, true);
                    non_matches = co2_candidates.Where(x => x[i] != co2_common[i]).ToList();
                    foreach (var x in non_matches) if (co2_candidates.Count > 1) co2_candidates.Remove(x);
                }
                if (o2_candidates.Count == 1 && co2_candidates.Count == 1) break;
            }
            int o2_gen_rate = Convert.ToInt32(o2_candidates[0], 2);
            int co2_scrub_rate = Convert.ToInt32(co2_candidates[0], 2);
            return o2_gen_rate * co2_scrub_rate;
        }
        static string GetCommonBits(List<string> input, bool detectEquals = false, bool flip = false)
        {
            char[] common = new char[input[0].Length];
            int[] zeroes = new int[input[0].Length];
            int[] ones = new int[input[0].Length];
            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[0].Length; j++)
                {
                    if (input[i][j] == '0') zeroes[j]++;
                    else ones[j]++;
                }
            }
            foreach (var bit in common.Select((Value, Index) => new { Value, Index }))
            {
                common[bit.Index] = zeroes[bit.Index] > ones[bit.Index] ? '0' : '1';
            }
            if (flip) return string.Join("", common.Select(c => c == '0' ? '1' : '0'));
            return string.Join("", common);
        }
    }
}
