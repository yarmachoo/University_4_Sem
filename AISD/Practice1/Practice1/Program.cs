for (int i = -5; i < 110; i++)
{
    Console.WriteLine($"{i}: M({i}) = {M(i)}");
}

Console.Write("m\\n");
for(int i = 0; i<10; i++) Console.Write($"\t{i}");

for (int m = 0; m < 10; m++)
{
    Console.Write($"\n{m}|");
    for (int n=0; n<10; n++)
    {
        Console.Write($"\t{A(m, n)}");
    }

}
static int M(int n)
{
    if (n > 100) return n - 10;
    else return M(M(n + 11));
}
//Функция Кирмана
static long A(long m, long n)
{
    if (m == 0)
        return n + 1;
    if (n == 0)
        return A(m - 1, 1);
    return A(m - 1, A(m, n - 1));
}