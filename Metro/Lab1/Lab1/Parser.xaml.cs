using System.Collections.ObjectModel;
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Lab1;


public partial class Parser : ContentPage
{
    private string code;
    public ParseTools parseTools { get; set; }
    public ObservableCollection<Operator> OperatorList { get; set; }
    public ObservableCollection<Operator> PairOperatorList { get; set; }

    public Parser()
    {
        parseTools = new ParseTools();
        InitializeComponent();
        PairOperatorList = new ObservableCollection<Operator>();
        OperatorList = new ObservableCollection<Operator>();
        BindingContext = this;
    }
    public void ClickedOnParse(object sender, EventArgs e)
    {
        parseTools.Text = CodeOnPerl.Text;
        code = CodeOnPerl.Text;
        //обновление метрик
        //Отображение элементов коллекции 
        PairOperatorList.Clear();
        foreach (var operand in parseTools.InitializeListOfPairOperators())
        {
            PairOperatorList.Add(operand); // Добавляем элементы в коллекцию
        }

        OperatorList.Clear();
        foreach (var op in parseTools.InitializeListOfOperators())
        {
            OperatorList.Add(op); // Добавляем элементы в коллекцию
        }

        //обновление метрик
        UpdateText();
    }
    public void UpdateText()
    {
        code = $"{CodeOnPerl.Text}";
        double maxDepthLevel = Parse();
        Volume.Text = $"Максимальный уровень вложенности CLI: {maxDepthLevel}";
        N1.Text = $"CL: N1 = {parseTools.N1}";
        N2.Text = $"cl: N2 = {parseTools.N1}/{parseTools.N2} = {(parseTools.N1)/(parseTools.N2)}";
    }
    private double Parse()
    {
        string text = code;
        List<string> keywords = new List<string>()
            {
                "case", "catch", "default:", "do", "else",
                "for", "foreach", "if", "switch", "while", "where"
            };
        List<string> separator = new List<string>() { ";", "{", "}", "\r", "\n", "\r\n" };


        List<double> levels = new List<double>();
        double depthLevel = -1.0;
        bool isCondition = false;
        double openScopes = 0.0;
        double closeScopes = 0.0;
        double countCasesDef = 0.0;

        StringBuilder Parse(string item)
        {
            StringBuilder str = new StringBuilder();

            if (keywords.Contains(item))
            {
                if (item == "if" || item == "for" || item == "foreach" || item == "do" || item == "while")
                {
                    isCondition = true;
                }
                if (item == "switch")
                {
                    depthLevel -= 3;
                    levels.Add(depthLevel);
                }
                if (item == "case")
                {
                    depthLevel += 1.0;
                    levels.Add(depthLevel);
                    countCasesDef += 1.0;
                }
                if (item == "default:")
                {
                    countCasesDef += 0.5;
                }

                str.Append("(keyword, " + item + ") ");
                return str;
            }

            if (separator.Contains(item))
            {
                if (isCondition)
                {
                    if (item == "{")
                    {
                        openScopes += 1.0;
                        depthLevel += 1.0;
                    }
                    if (item == "}")
                    {
                        levels.Add(depthLevel);
                        depthLevel -= 1.0;
                        closeScopes += 1.0;
                        if (openScopes == closeScopes)
                        {
                            openScopes = 0.0;
                            closeScopes = 0.0;
                            isCondition = false;
                        }
                    }
                }
                return str;
            }

            return str;
        }

        bool CheckDelimiter(string str)
        {
            return separator.Contains(str);
        }

        string GetNextLexicalAtom(ref string item)
        {
            StringBuilder token = new StringBuilder();

            for (int i = 0; i < item.Length; i++)
            {
                if (CheckDelimiter(item[i].ToString()))
                {
                    if (i + 1 < item.Length && CheckDelimiter(item.Substring(i, 2)))
                    {
                        token.Append(item.Substring(i, 2));
                        item = item.Remove(i, 2);
                        return Parse(token.ToString()).ToString();
                    }
                    else
                    {
                        token.Append(item[i]);
                        item = item.Remove(i, 1);
                        return Parse(token.ToString()).ToString();
                    }
                }
                else if (item[i + 1].ToString().Equals(" ") || CheckDelimiter(item[i + 1].ToString()))
                {
                    token.Append(item.Substring(0, i + 1));
                    item = item.Remove(0, i + 1);
                    return Parse(token.ToString()).ToString();
                }
            }
            return null;
        }

        while (text != null && text.Length > 0)
        {
            text = text.Trim(' ', '\t');
            string token = GetNextLexicalAtom(ref text);
            if (token == null)
                break;

        }


        Console.WriteLine($"Max level: {levels.Max()}");
        Console.WriteLine($"Count cases def: {countCasesDef}");
        return levels.Max();
    }
}