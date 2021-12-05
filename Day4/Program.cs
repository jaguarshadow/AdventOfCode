using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Day4
{
    class Program
    {
        public static class BingoHelper
        {
            public static List<int> CalledNumbers = new List<int>();
            public static List<Board> BoardsInPlay = new List<Board>();

            public static void GenerateBoards(List<string> input)
            {
                CalledNumbers.AddRange(input[0].Split(',').Select(n => int.Parse(n)));
                int row_count = 0;
                var current_numbers = new List<int>();
                for (int i = 1; i < input.Count; i++)
                {
                    if (string.IsNullOrWhiteSpace(input[i])) continue;
                    var row = input[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    foreach (string num in row) current_numbers.Add(int.Parse(num));
                    row_count++;
                    if (row_count == 5)
                    {
                        Board board = new Board(BoardsInPlay.Count, current_numbers);
                        BoardsInPlay.Add(board);
                        current_numbers.Clear();
                        row_count = 0;
                    }
                }
            }


            public static void CallNumbers(bool win = true)
            {
                var winners = new List<Board>();
                int index = 0;
                if (win)
                {
                    while (winners.Count == 0)
                    {
                        BoardsInPlay.ForEach(board => board.ProcessCalledNumber(CalledNumbers[index]));
                        winners = CheckForWinners();
                        if (index == CalledNumbers.Count - 1) break;
                        index++;
                    }
                    if (winners[0] != null) Console.WriteLine($"A winner was found!\nBoard Id: {winners[0].Id} \nScore: {Board.CalculateScore(winners[0], CalledNumbers[index - 1])}\n");
                    else Console.WriteLine("No winning boards found.");
                }
                else
                {
                    Board last_winner = null;
                    var inPlay = new List<Board>(BoardsInPlay);
                    while (index < CalledNumbers.Count && inPlay.Count > 1)
                    {
                        inPlay.ForEach(board => board.ProcessCalledNumber(CalledNumbers[index]));
                        winners = CheckForWinners();
                        if (winners.Count > 0) winners.ForEach(winner => inPlay.Remove(winner));
                        index++;
                        if (inPlay.Count == 1)
                        {
                            last_winner = inPlay[0];
                            while (!last_winner.Winner)
                            {
                                last_winner.ProcessCalledNumber(CalledNumbers[index]);
                                int current = CalledNumbers[index];
                                index++;
                            }
                        }
                    }
                    if (last_winner != null) Console.WriteLine($"Losing Board Id: {inPlay[0].Id} \nScore: {Board.CalculateScore(inPlay[0], CalledNumbers[index - 1])}\n");            
                }
                return;
            }
            public static List<Board> CheckForWinners(bool all = false)
            {
                return BoardsInPlay.Where(b=> b.Winner).ToList();
            }
        }

        public class Board
        {
            public int Id { get; set; }
            public bool Winner { get; set; }

            private int[,] Numbers = new int[5, 5];
            private bool[,] Called = new bool[5, 5];
            public Board(int id, List<int> nums)
            {
                for (int i = 0; i < nums.Count; i++)
                {
                    Id = id;
                    if (i < 5) Numbers[0, i] = nums[i];
                    else if (5 <= i && i < 10) Numbers[1, i-5] = nums[i];
                    else if (10 <= i && i < 15) Numbers[2, i-10] = nums[i];
                    else if (15 <= i && i < 20) Numbers[3, i-15] = nums[i];
                    else Numbers[4, i-20] = nums[i];
                }
            }
            public void ProcessCalledNumber(int number)
            {
                
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (Numbers[i, j] == number)
                        {
                            Called[i, j] = true;
                            break;
                        }
                    }

                    
                    if (Called[i, 0] && Called[i, 1] && Called[i, 2] && Called[i, 3] && Called[i, 4]
                        || Called[0, i] && Called[1, i] && Called[2, i] && Called[3, i] && Called[4, i]) Winner = true;
                }
            }
            public static int CalculateScore(Board b, int last_call)
            {
                int sum = 0;
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++) if (b.Called[i, j] == false) sum += b.Numbers[i, j];
                }
                return sum * last_call;
            }
        }


        static void Main(string[] args)
        {
            List<string> input = File.ReadLines(@"input.txt").ToList();
            BingoHelper.GenerateBoards(input);
            var boards = BingoHelper.BoardsInPlay;
            BingoHelper.CallNumbers();
            BingoHelper.CallNumbers(false);
        }
    }   
}
