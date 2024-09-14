string firstStr = Console.ReadLine();
string secondStr = Console.ReadLine();

int indexOfSpace = firstStr.IndexOf(' ');
int n = int.Parse(firstStr.Substring(startIndex: 0, length: indexOfSpace));
int s = int.Parse(firstStr.Substring(startIndex: indexOfSpace + 1));

int[] a = new int[n];
a = secondStr.Split(' ').Select(int.Parse).ToArray();

int res = 0;

Console.WriteLine(Substrings(n, s, a, 1, 0));
static int Substrings(int n, int s, int[] a, int lengthOfSubstr, int res)
{
    if(lengthOfSubstr==n)
        return res;
    else
    {
        int sum = 0;
        int countOfSubstringsInString = CountOfSubstringsWithFollowingLength(n, lengthOfSubstr);
        for (int i = 0; i < countOfSubstringsInString; i++ ) 
        {
            for(int j = 0; j < lengthOfSubstr; j++ )
            {
                sum += a[j + i];
            }
            if (sum <= s)
            {
                res++;
            }
            sum = 0;
        }
        lengthOfSubstr++;
        return Substrings(n, s, a, lengthOfSubstr, res);
    }
}
static int CountOfSubstringsWithFollowingLength(int n, int lengthOfSubstr)
{
    int count = 0;
    while(lengthOfSubstr<=n)
    {
        lengthOfSubstr++;
        count++;
    }
    return count;
}