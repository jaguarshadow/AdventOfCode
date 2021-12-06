using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadLines(@"input.txt").ToArray();
            var starting_school = input[0].Split(',').Select(n => int.Parse(n)).ToList();
            var school = new List<int>(starting_school);
            Console.WriteLine($"Fish after 80 days: {CaclulateSpawn(school, 80)}");
            school = new List<int>(starting_school);
            Console.WriteLine($"Fish after 256 days: {CaclulateSpawn(school, 256)}");
        }

        public static int CaclulateSpawn(List<int> school, int days)
        {
            foreach (int day in Enumerable.Range(0, days))
            {
                int new_fish = 0;
                for (int i = 0; i < school.Count; i++)
                {
                    if (school[i] == 0)
                    {
                        new_fish++;
                        school[i] = 6;
                    }
                    else school[i]--;
                }
                for (int i = 0; i < new_fish; i++) school.Add(8);
                Console.WriteLine($"Fish after day {day}: {school.Count}");
            }
            return school.Count;
        }
    }
}
