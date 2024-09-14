using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab1
{
    public class ParseTools
    {
        public int Count { get; set; }
        //В Text хранится код на ЯП Perl
        public string Text { get; set; }
        public List<Operator> Operators { get; set; }
        public List<Operator> PairOperators { get; set; }
        public int n1 { get; set; }
        public int n2 { get; set; }
        public double N1 { get; set; }
        public double N2 { get; set; }
        public ParseTools()
        {
            Operators = new List<Operator>();
            PairOperators = new List<Operator>();
        }
        public List<Operator> InitializeListOfOperators()
        {
            //Этот метод вызывается из Parser.xaml.cs
            return GetOperators();
        }
        public List<Operator> InitializeListOfPairOperators()
        {
            //Этот метод вызывается из Parser.xaml.cs
            return GetPairOperators();
        }
        public List<Operator> GetOperators()
        {
            Operators.Clear();
            var operators = new Dictionary<string, int>();
            N1 = 0;
            n1 = 0;
            var operatorRegex = new Regex(
                "if|for|case"
            );

            string[] lines = Text.Split('\n');
            int lineNumber = 1;
            foreach (string line in lines)
            {
                if (line.Contains("#"))
                {
                    //continue;
                }

                var matches = operatorRegex.Matches(line);
                foreach (Match match in matches)
                {
                    var operatorString = match.Value;
                    var matchPosition = match.Index;

                    if (operatorString == "=")
                    {
                        // Check for the presence of the ~ character after the = operator
                        if (matchPosition + 1 < line.Length && line[matchPosition + 1] == '~')
                        {
                            operatorString = "=~";
                        }
                        else
                        {
                            operatorString = "=";
                        }
                    }

                    if (!JustText(line, matchPosition))
                    {
                        if (operators.ContainsKey(operatorString))
                        {
                            operators[operatorString]++;
                        }
                        else
                        {
                            operators[operatorString] = 1;
                        }
                    }
                }

                lineNumber++;
            }
            int Summ = 0;
            int i = 0;
            foreach (var pair in operators)
            {
                i++;
                if (pair.Key != "?")
                {
                    Operators.Add(new Operator(i, $"{pair.Key}", pair.Value));
                    //Console.WriteLine(pair.Key + " - " + pair.Value);
                }
                else
                {
                    Operators.Add(new Operator(i, $"? :", pair.Value));
                    //Console.WriteLine("? : - " + pair.Value);
                }
                Summ += pair.Value;
            }
            Initn1();
            InitN1();
            return Operators;
        }
        public List<Operator> GetPairOperators()
        {
            PairOperators.Clear();
            var operators = new Dictionary<string, int>();
            N2 = 0;
            n2 = 0;
            var operatorRegex = new Regex(
                "if|unless|while|until|for|foreach|last|next|push|pop|shift|unshift|splice|delete|print|use|my|our|case"
            );

            string[] lines = Text.Split('\n');
            int lineNumber = 1;
            foreach (string line in lines)
            {
                if (line.Contains("#"))
                {
                    //continue;
                }

                var matches = operatorRegex.Matches(line);
                foreach (Match match in matches)
                {
                    var operatorString = match.Value;
                    var matchPosition = match.Index;

                    if (operatorString == "=")
                    {
                        // Check for the presence of the ~ character after the = operator
                        if (matchPosition + 1 < line.Length && line[matchPosition + 1] == '~')
                        {
                            operatorString = "=~";
                        }
                        else
                        {
                            operatorString = "=";
                        }
                    }

                    if (!JustText(line, matchPosition))
                    {
                        if (operators.ContainsKey(operatorString))
                        {
                            operators[operatorString]++;
                        }
                        else
                        {
                            operators[operatorString] = 1;
                        }
                    }
                }

                lineNumber++;
            }
            int Summ = 0;
            int i = 0;
            foreach (var pair in operators)
            {
                i++;
                if (pair.Key != "?")
                {
                    PairOperators.Add(new Operator(i, $"{pair.Key}", pair.Value));
                    //Console.WriteLine(pair.Key + " - " + pair.Value);
                }
                else
                {
                    PairOperators.Add(new Operator(i, $"? :", pair.Value));
                    //Console.WriteLine("? : - " + pair.Value);
                }
                Summ += pair.Value;
            }
            Initn2();
            InitN2();
            return PairOperators;
        }
        static bool JustText(string line, int position)
        {
            bool singleQuotes = false;
            bool doubleQuotes = false;

            for (int i = 0; i < position; i++)
            {
                if (line[i] == '\'')
                {
                    if (!doubleQuotes)
                    {
                        singleQuotes = !singleQuotes;
                    }
                }
                else if (line[i] == '\"')
                {
                    if (!singleQuotes)
                    {
                        doubleQuotes = !doubleQuotes;
                    }
                }
            }

            return singleQuotes || doubleQuotes;
        }
        public void Initn1()
        {
            foreach (var op in Operators)
            {
                n1 += 1;
            }
        }
        public void InitN1()
        {
            foreach (var op in Operators)
            {
                N1 += op.F1j;
            }
        }

        public void Initn2()
        {
            foreach (var op in PairOperators)
            {
                n2 += 1;
            }
        }
        public void InitN2()
        {
            foreach (var op in PairOperators)
            {
                N2 += op.F1j;
            }
        }
    }
}
