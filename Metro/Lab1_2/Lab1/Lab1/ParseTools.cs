using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
//using static ObjCRuntime.Dlfcn;

namespace Lab1;
public class ParseTools
{
    public int Count { get; set; }
    //В Text хранится код на ЯП Perl
    public  string Text { get; set; }
    public List<Operand> Operands {  get; set; }
    public List<Operator> Operators { get; set; }
    public int n1 { get; set; }
    public int n2 { get; set; }
    public int N1 { get; set; }
    public int N2 { get; set; }
    public ParseTools()
    {
        Operators = new List<Operator>();
        Operands = new List<Operand>();
    }
    public List<Operand> InitializeListOfOperands()
    {
         
        //Этот метод вызывается из Parser.xaml.cs
        return GetOperands();
    }
    public List<Operator> InitializeListOfOperators()
    {
        //Этот метод вызывается из Parser.xaml.cs
        return GetOperators();
    }
    public List<Operator> GetOperators()
    {
        Operators.Clear();
        var operators = new Dictionary<string, int>();
        N1 = 0;
        n1 = 0;
        var operatorRegex = new Regex(
            "if|elsif|unless|while|until|for|foreach|last|next|push|pop|shift|unshift|splice|delete|print|use|my|our|case"
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
    public List<Operand> GetOperands()
    {
        Operands.Clear();
        N2 = 0;
        n2 = 0;
        string regexPattern = @"\$[a-zA-Z_]\w*|\@[a-zA-Z_]\w*|\%[a-zA-Z_]\w*|'[^']*'|""[^""]*""|\b\d+(\.\d+)?\b";

        MatchCollection matches = Regex.Matches(Text, regexPattern);

        Console.WriteLine("Найденные операторы и количество их вхождений:");
        Dictionary<string, int> operatorsCount = new Dictionary<string, int>();

        foreach (Match match in matches)
        {
            if (operatorsCount.ContainsKey(match.Value))
            {
                for (int i = 0; i < operatorsCount.Count; i++)
                {
                    if (operatorsCount.ElementAt(i).Key == match.Value)
                    {
                        int n = operatorsCount.ElementAt(i).Value;
                        n++;
                        operatorsCount.Remove(match.Value);
                        operatorsCount.Add(match.Value, n);
                    }
                }
            }
            else
            {
                operatorsCount.Add(match.Value, 1);
            }
        }
        for (int i = 0; i < operatorsCount.Count; i++)
        {
            string regex = @"^[$@%]";
            //чтобы выводить переменные без символа @/$/%

            //if (Regex.IsMatch(operatorsCount.ElementAt(i).Key, regex))
            //{
            //    OperandList.Add(new Operand(i, operatorsCount.ElementAt(i).Key.Substring(1), operatorsCount.ElementAt(i).Value));
            //}
            //else
            //{
            //    OperandList.Add(new Operand(i, operatorsCount.ElementAt(i).Key, operatorsCount.ElementAt(i).Value));
            //}
            Operands.Add(new Operand(i+1, operatorsCount.ElementAt(i).Key, operatorsCount.ElementAt(i).Value));
        }
        Initn2();
        InitN2();
        return Operands;
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
        foreach (var op in Operands)
        {
            n2 += 1;
        }
    }
    public void InitN2()
    {
        foreach (var op in Operands)
        {
            N2 += op.F2i;
        }
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
}
