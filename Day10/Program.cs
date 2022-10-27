using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Day10
{
    internal class SyntaxChecker
    {
        static void Main(string[] args)
        {
            var smallinput = File.ReadLines(@"smallinput.txt").ToList();
            var biginput = File.ReadLines(@"input.txt").ToList();

            // Part One
            var corruptScore = biginput.Select(x => LineCorruptScore(x)).Sum();
            Console.WriteLine($"Part 1 Corrupt Score: {corruptScore}");

            // Part Two
            var uncorrupted = biginput.Where(s => LineCorruptScore(s) == 0);
            var completeScores = uncorrupted.Select(s => CompleteAndScoreLine(s)).OrderBy(score => score).ToList();
            Console.WriteLine($"Part 2 Complete score: {completeScores[completeScores.Count / 2]}");
            
        }

        static List<char> openChars  = new List<char>() { '(', '[', '{', '<' };
        static List<char> closeChars = new List<char>() { ')', ']', '}', '>' };

        static Dictionary<char, char> brackets = new Dictionary<char, char>() {
            { '(', ')' },
            { '[', ']' },
            { '{', '}' },
            { '<', '>' }};

        static Dictionary<char, (int corrupt, int complete)> points = new Dictionary<char, (int corrupt, int complete)> () {
            { ')', (3, 1) },
            { ']', (57, 2) },
            { '}', (1197, 3) },
            { '>', (25137, 4) }};

        static long LineCorruptScore(string line) {
            var seenOpen = new List<char>();

            for (int i = 0; i < line.Length; i++) {
                var current = line[i];
                if (brackets.Keys.Contains(current)) seenOpen.Add(current);
                else {
                    if (current == brackets[seenOpen.Last()]) seenOpen.RemoveAt(seenOpen.Count - 1);
                    else return points[current].corrupt;
                }
            }
            return 0;
        }

        static long CompleteAndScoreLine(string line) {
            var seenOpen = new List<char>();
            long total = 0;
            for (int i = 0; i < line.Length; i++) {
                char current = line[i];
                if (brackets.Keys.Contains(current)) seenOpen.Add(current);
                else if (current == brackets[seenOpen.Last()]) seenOpen.RemoveAt(seenOpen.Count - 1);
            }
            for (int i = seenOpen.Count - 1; i >= 0; i--) { 
                total *= 5;
                total += points[brackets[seenOpen[i]]].complete;
            }
            return total;
        }


    }
}
