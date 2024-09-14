using System.Collections.ObjectModel;

namespace Lab1;


public partial class Parser : ContentPage
{
    public ParseTools parseTools {  get; set; }
    public ObservableCollection<Operator> OperatorList { get; set; }
    public ObservableCollection<Operand> OperandList { get; set; }
    public Parser()
	{
        parseTools = new ParseTools();
        InitializeComponent();
        OperandList = new ObservableCollection<Operand>();
        OperatorList = new ObservableCollection<Operator>();
        BindingContext = this;
    }
    public void ClickedOnParse(object sender, EventArgs e)
    {
        //�������� ����� �� Editor
        parseTools.Text = CodeOnPerl.Text;


        //����������� ��������� ��������� 
        OperandList.Clear();
        foreach (var operand in parseTools.InitializeListOfOperands())
        {
            OperandList.Add(operand); // ��������� �������� � ���������
        }

        OperatorList.Clear();
        foreach (var op in parseTools.InitializeListOfOperators())
        {
            OperatorList.Add(op); // ��������� �������� � ���������
        }

        //���������� ������
        UpdateText();
    }
    public void UpdateText()
    {
        n1.Text = $" n1 = {parseTools.n1}";
        n2.Text = $" n2 = {parseTools.n2}";
        N1.Text = $" N1 = {parseTools.N1}";
        N2.Text = $" N2 = {parseTools.N2}";
        Dictionary.Text = $"�������: n = {parseTools.n1+ parseTools.n2}";
        Length.Text = $"�����: N = {parseTools.N1+parseTools.N2}";
        double volume = (parseTools.N1 + parseTools.N2)*Math.Log2(parseTools.n1 + parseTools.n2);
        Volume.Text = $"�����: V = {volume:F4}";
    }
}