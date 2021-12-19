using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> valve_values = new List<int>();

            foreach (var line in File.ReadLines(@"input.txt"))
            {
                int index = line.IndexOf("| ");
                var valve_digits = line.Substring(index + 1).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                var segments = line.Substring(0, index + 1).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                var easy = DoEasySegments(segments);
                var map = easy.Concat(DoHardSegments(segments, easy)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                StringBuilder valve = new StringBuilder();
                foreach (var s in valve_digits) 
                    valve.Append(map.FirstOrDefault(x => SegmentMatch(x.Value, s) == true).Key);
                valve_values.Add(int.Parse(valve.ToString()));
            }

            Console.WriteLine(valve_values.Sum());
        }

        public static Dictionary<string, string> DoEasySegments(List<string> segments)
        {
            var map = new Dictionary<string, string>();
            foreach (var segment in segments)
            {
                switch (segment.Length)
                {
                    case 2:
                        map.Add("1", segment);
                        break;
                    case 3:
                        map.Add("7", segment);
                        break;
                    case 4:
                        map.Add("4", segment);
                        break;
                    case 7:
                        map.Add("8", segment);
                        break;
                }
            }
            return map;
        }

        public static Dictionary<string, string> DoHardSegments(List<string> segments, Dictionary<string, string> easy_segments)
        {
            var map = new Dictionary<string, string>();
            foreach (var segment in segments)
            {
                switch (segment.Length)
                {
                    case 5:
                        if (segment.Contains(easy_segments["1"][0]) && segment.Contains(easy_segments["1"][1])) map.Add("3", segment);
                        else if (CompareSegments(easy_segments["4"], segment) == 2) map.Add("2", segment);
                        else map.Add("5", segment);
                        break;
                    case 6:
                        if (segment.Contains(easy_segments["1"][0]) && segment.Contains(easy_segments["1"][1]))
                        {
                            if (CompareSegments(easy_segments["4"], segment) == 2) map.Add("9", segment);
                            else map.Add("0", segment);
                        }
                        else map.Add("6", segment);
                        break;
                }
            }
            return map;
        }

        public static int CompareSegments(string known_segment, string unknown_segment)
        {
            int count = 0;
            if (unknown_segment.Length == 5)
            {
                foreach (var c in known_segment) if (unknown_segment.Contains(c)) count++;
            }
            else if (unknown_segment.Length == 6)
            {
                string new_seg = unknown_segment;
                foreach (var c in known_segment) new_seg = new_seg.Replace(c.ToString(), string.Empty);
                count =  new_seg.Length;
            }
            return count;
        }

        public static bool SegmentMatch(string first, string second)
        {
            if (first.Length != second.Length) return false;
            int count = 0;
            foreach (var c in first) if (second.Contains(c)) count++;
            if (count == first.Length) return true;
            return false;
        }
    }
}
