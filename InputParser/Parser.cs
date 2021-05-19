using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Database;

namespace InputParser
{
    static class Parser
    {
        static IDictionary<string, Regex> regExPatterns = new Dictionary<string, Regex>() { };
        static Parser()
        {
            Parser.ReadInput();
        }
        static async void ReadInput()
        {
            while(true)
            {
                await Task.Run(() => Parser.Parse(Console.ReadLine()));
            }
        }
        static void Parse(string input)
        {
            string[] splittedInput = input.Split(" ");
            foreach(var kvp in Parser.regExPatterns)
            {
                foreach(var word in splittedInput)
                {
                    MatchCollection matches = kvp.Value.Matches(word);
                    if (matches.Count > 0)
                    {
                        foreach (Match match in matches)
                        {
                            Console.WriteLine(match.Value);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Not a valid input");
                    }
                }
            }
        }

    }
}
