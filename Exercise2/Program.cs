using System;
using System.IO;
using System.Linq;

namespace Exercise2
{
/*
Nice to get a repeat question from the first interview. This is based on what was the "correct" solution then.
*/
    internal class Program
    {
        public static void Main(string[] args)
        {
            if (!args.Any())
            {
                Console.WriteLine("Specify file path");
                args = new[] {Console.ReadLine()};
            }

            string filePath = args[0];
            Console.WriteLine("Initializing...");
            // ready file lazily and group by sort key
            var lookup = File.ReadLines(filePath).ToLookup(GetSortKey);
            
            // allow reuse of lookup after initial creation
            while (true)
            {
                Console.Write("Enter input: ");
                var input = Console.ReadLine();

                Console.WriteLine();
                Console.WriteLine($"Matches for {input}:");
                foreach (var match in lookup[GetSortKey(input)])
                {
                    Console.WriteLine(match);
                }
            }
        }

        // Sort the characters so that the ordering is irrelevant.
        // NOTE: It should be possible to make this a tiny bit faster by using some
        // other method to turn the sorted char array into a comparable key.
        static string GetSortKey(string str)
        {
            return new string(str.OrderBy(s => s).ToArray());
        }
    }
}