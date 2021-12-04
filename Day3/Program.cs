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

            List<string> test_numbers = File.ReadLines(@"smallinput.txt").ToList();

            Console.WriteLine($"gamma * epsilon = {GetPowerConsumption(binary_numbers)}");

            Console.WriteLine($"co2 * oxygen = {GetLifeSupportRating(binary_numbers)}");
        }

        static string GetCommonBits(List<string> input, bool detectEquals = true, bool flip = false)
        {
            int len = input[0].Length;
            char[] common = new char[len];

            int[] zeroes = new int[len];
            int[] ones = new int[len];

            foreach (string binary_str in input)
            {
                for (int i = 0; i < len; i++)
                {
                    if (binary_str[i] == '0') zeroes[i]++;
                    else ones[i]++;
                }
            }

            for (int i = 0; i < len; i++)
            {
                if (detectEquals && zeroes[i] == ones[i]) common[i] = '=';
                else if (zeroes[i] > ones[i]) common[i] = '0';
                else common[i] = '1';
            }

            if (flip) return string.Join("", common.Select(c => c == '0' ? '1' : '0'));

            return string.Join("", common);
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

            for (int i = 0; i < input[0].Length; i++)
            {
                // Handle Oxygen
                var o2_common = GetCommonBits(o2_candidates, true);

                for (int j = o2_candidates.Count - 1; j >= 0; j--)
                {
                    if (o2_candidates.Count > 1)
                    {
                        switch (o2_common[i])
                        {
                            case '=':
                                if (o2_candidates[j][i] != '1') o2_candidates.RemoveAt(j);
                                break;
                            default:
                                if (o2_candidates[j][i] != o2_common[i]) o2_candidates.RemoveAt(j);
                                break;
                        }
                    }
                    else break;
                }

                // Handle CO2
                var co2_common = GetCommonBits(co2_candidates, true, true);
                for (int j = co2_candidates.Count - 1; j >= 0; j--)
                {
                    if (co2_candidates.Count > 1)
                    {
                        switch (co2_common[i])
                        {
                            case '=':
                                if (co2_candidates[j][i] != '0') co2_candidates.RemoveAt(j);
                                break;
                            default:
                                if (co2_candidates[j][i] != co2_common[i]) co2_candidates.RemoveAt(j);
                                break;
                        }
                    }
                    else break;
                }
                if (o2_candidates.Count == 1 && co2_candidates.Count == 1) break;
            }

            int o2_gen_rate = Convert.ToInt32(o2_candidates[0], 2);
            int co2_scrub_rate = Convert.ToInt32(co2_candidates[0], 2);

            return o2_gen_rate * co2_scrub_rate;
        }


    }
}
