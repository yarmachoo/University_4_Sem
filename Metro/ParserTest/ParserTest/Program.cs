using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        string perlCode = @"$hello = ""hi mom"";
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

        // Регулярное выражение для поиска операторов
        string regexPattern = @"\$[a-zA-Z_]\w*|\@[a-zA-Z_]\w*|\%[a-zA-Z_]\w*|'[^']*'|""[^""]*""|\b\d+(\.\d+)?\b";

        MatchCollection matches = Regex.Matches(perlCode, regexPattern);


        Console.WriteLine("Найденные операторы и количество их вхождений:");
        Dictionary <string, int> operatorsCount = new Dictionary<string, int>();
        

        foreach (Match match in matches)
        {
            if (operatorsCount.ContainsKey(match.Value))
            {
                for(int i =0; i<operatorsCount.Count; i++)
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
        foreach(var op in operatorsCount)
        {
            Console.WriteLine($"key: {op.Key}, value: {op.Value}");
        }
    }
}
