using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadLines(@"input.txt").ToArray();
            var starting_school = input[0].Split(',').Select(n => double.Parse(n)).ToList();
            var smallinput = new List<double>() { 3, 4, 3, 1, 2 };
            Console.WriteLine($"Fish after 80 days: {CaclulateSpawn(starting_school, 80)}");
            Console.WriteLine($"Fish after 256 days: {CaclulateSpawn(starting_school, 256)}");
        }

        public static double CaclulateSpawn(List<double> school, int days)
        {
            var fish_map = new Dictionary<int, double>() { { 0, 0 }, { 1, 0 }, { 2, 0 }, { 3, 0 }, { 4, 0 },
                                                           { 5, 0 }, { 6, 0 }, {7, 0 }, { 8, 0 } };
            double fish_new = 0;
            double number_of_fish = 0;
            foreach (int fish in school) fish_map[fish]++;

            foreach (int day in Enumerable.Range(1, days)) { // For each Day
                for (int timer = 0; timer < 9; timer++) { // For each possible timer value
                    number_of_fish = fish_map[timer];
                    if (number_of_fish > 0)
                    {
                        if (timer == 0) {
                            fish_map[0] -= number_of_fish;
                            fish_new = number_of_fish;
                        } else {
                            fish_map[timer] -= number_of_fish;
                            fish_map[timer - 1] += number_of_fish;
                        }
                    }
                }
                fish_map[6] += fish_new;
                fish_map[8] += fish_new;
                fish_new = 0;
            }
            return fish_map.Sum(x => x.Value);
        }
    }
}