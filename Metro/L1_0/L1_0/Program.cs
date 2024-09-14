using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

class HolsteadMetricsCalculator
{
    private int n1, n2, N1, N2;

    public HolsteadMetricsCalculator()
    {
        n1 = 0; // количество уникальных операторов в программе
        n2 = 0; // количество уникальных операндов в программе
        N1 = 0; // общее количество операторов в программе
        N2 = 0; // общее количество операндов в программе
    }

    public void CalculateMetrics(string filePath)
    {
        // Считывание содержимого файла
        string[] lines = File.ReadAllLines(filePath);

        // Регулярные выражения для поиска операторов и операндов
        Regex operatorRegex = new Regex(@"(\b[a-zA-Z]+\b)|(\b\W\b)");
        Regex operandRegex = new Regex(@"\b[a-zA-Z]+\b");

        HashSet<string> operators = new HashSet<string>();
        HashSet<string> operands = new HashSet<string>();

        // Подсчет уникальных операторов и операндов
        foreach (string line in lines)
        {
            MatchCollection operatorMatches = operatorRegex.Matches(line);
            foreach (Match match in operatorMatches)
            {
                operators.Add(match.Value);
            }

            MatchCollection operandMatches = operandRegex.Matches(line);
            foreach (Match match in operandMatches)
            {
                operands.Add(match.Value);
            }
        }

        // Вывод уникальных операторов и операндов
        Console.WriteLine("Unique Operators:");
        foreach (string op in operators)
        {
            Console.WriteLine(op);
        }
        Console.WriteLine();

        Console.WriteLine("Unique Operands:");
        foreach (string operand in operands)
        {
            Console.WriteLine(operand);
        }
        Console.WriteLine();

        // Обновление значений метрик
        n1 = operators.Count;
        n2 = operands.Count;

        // Подсчет общего количества операторов и операндов
        N1 = 0;
        foreach (string op in operators)
        {
            N1 += CountOccurrences(lines, op);
        }

        N2 = operands.Count;

        // Вывод результатов
        Console.WriteLine("n1 (Unique Operators): " + n1);
        Console.WriteLine("n2 (Unique Operands): " + n2);
        Console.WriteLine("N1 (Total Operators): " + N1);
        Console.WriteLine("N2 (Total Operands): " + N2);
    }

    private int CountOccurrences(string[] lines, string word)
    {
        int count = 0;
        foreach (string line in lines)
        {
            count += Regex.Matches(line, "\\b" + word + "\\b").Count;
        }
        return count;
    }
}

class Program
{
    static void Main(string[] args)
    {
        string filePath = "D:\\Uni\\4sem\\Metro\\L1_0\\L1_0\\TextFile1.txt"; // Укажите путь к файлу программы Perl
        HolsteadMetricsCalculator calculator = new HolsteadMetricsCalculator();
        calculator.CalculateMetrics(filePath);
    }
}
