using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Program
{
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

    static void Main()
    {
        // сюда считывать код
        string code = @"$hello = ""hi mom"";

print ""$hello, perl is cool! \n"";



my $local = ""I am a local variable"";
our $global = ""I am a global variable"";

@nums = (10, 20, 30);

@nums[1];  

%friends = ('Larry', 67, 'Ken', 79);
%friends{'Larry'};

if (5 > 10) {
    print ""hi"";
}

print ""hi"" if 5 > 10;

$result = (5 > 10)? ""hi"" : ""bye"";

sub PerlCanBeFun {
    print ""this is  function\n"";
    my ($n1, $n2) = @_;
    print $n1 + $n2;
}

PerlCanBeFun(2, 3);

if ($text =~ /cool/) {
    # do this
}

@counter = (1..10);
$counter[20++];
$len = @counter;
print $len;
";

        var operators = new Dictionary<string, int>();

        var operatorRegex = new Regex(
            "=|=~|\\+=|-=|\\*=|/=|%=|\\+|-|\\*|/|%|\\*\\*|==|!=|<|>|<=|>=|&&|\\|\\||!|" +
            "if|elsif|else|unless|while|until|for|foreach|last|next|push|pop|shift|unshift|splice|delete|print|use|my|our|" +
            ";|\\?|\\/"
        );

        string[] lines = code.Split('\n');
        int lineNumber = 1;
        foreach (string line in lines)
        {
            if (line.Contains("#"))
            {
                continue;
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
        foreach (var pair in operators)
        {
            if (pair.Key != "?")
            {
                Console.WriteLine(pair.Key + " - " + pair.Value);
            }
            else
            {
                Console.WriteLine("? : - " + pair.Value);
            }
            Summ += pair.Value;
        }

        Console.WriteLine("Total operators: " + Summ);
    }
}