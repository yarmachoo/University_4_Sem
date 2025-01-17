﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Test
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> scopes = new Stack<int>();
            int maxDepthLevel = 0;
            int currentDepthLevel = 0;

            string code = @"
            #!/usr/bin/perl

                    use strict;
                    use warnings;

                    # Генерируем случайное число от 1 до 10
                    my $num = int(rand(10)) + 1;

                    # switch конструкция на Perl
                    switch ($num) {
                        case 1 {
                            print ""Число равно 1\n"";
                        }
                        case 2 {
                            print ""Число равно 2\n"";
                        }
                        case [3, 4, 5] {
                            print ""Число равно 3, 4 или 5\n"";
                        }
                        else {
                            print ""Число больше 5 или меньше 1\n"";
                        }
                    }

                    # Пример цикла for
                    for my $i (1..3) {
                        if ($i > 1) {
                            for my $j (1..2) {
                                print ""Внутренний цикл: $j\n"";
                                if ($j > 1) {
                                    for my $k (1..2) {
                                        print ""Внутренний цикл 2: $k\n"";
                                        if ($k > 1) {
                                            for my $l (1..2) {
                                                print ""Внутренний цикл 3: $l\n"";
                                                if ($l > 1) {
                                                    for my $m (1..2) {
                                                        print ""Внутренний цикл 4: $m\n"";
                                                        if ($m == 2) {
                                                            print ""Оператор if: \$m равно 2\n"";
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    # Добавление вложенных и составных условий
                    if ($num % 2 == 0) {
                        if ($num > 5) {
                            print ""Число больше 5 и четное\n"";
                            if ($num < 8) {
                                print ""Число больше 5, четное, и меньше 8\n"";
                            }
                            if ($num == 10) {
                                print ""Число больше 5, четное, и равно 10\n"";
                            }
                        } else {
                            print ""Число четное, но не больше 5\n"";
                        }
                    } else {
                        if ($num < 3) {
                            print ""Число меньше 3 и нечетное\n"";
                        } else {
                            print ""Число нечетное, но не меньше 3\n"";
                        }
                    }
                    ";

            string[] lines = code.Split('\n');

            foreach (string line in lines)
            {
                string word = line.Trim();

                if (word.StartsWith("if") || word.StartsWith("else") ||
                    word.StartsWith("for") || word.StartsWith("foreach") ||
                    word.StartsWith("do") || word.StartsWith("while"))
                {
                    scopes.Push(currentDepthLevel);
                    currentDepthLevel++;
                }

                if (word == "{")
                {
                    currentDepthLevel++;
                }

                if (word == "}")
                {
                    currentDepthLevel--;
                    maxDepthLevel = Math.Max(maxDepthLevel, currentDepthLevel);
                    if (scopes.Count > 0)
                    {
                        currentDepthLevel = scopes.Pop();
                    }
                }
            }

            Console.WriteLine("Максимальный уровень вложенности условного оператора CLI: " + maxDepthLevel);
            Console.ReadLine();
        }
    }
}
