#include <iostream>
#include <math.h>
#include <ctime>
#include <chrono>
static int Collatz(int n)
{
    std::cout << n << " -> ";
    auto start_time = std::chrono::steady_clock::now();
    while (n != 1)
    {
        if (n % 2 == 0)
        {
            std::cout << n / 2<<" -> ";
            n /= 2;
        }
        else
        {
            std::cout << (3 * n + 2) / 2<<" => ";
            n = (3 * n + 2) / 2;
        }
    }
    auto end_time = std::chrono::steady_clock::now();
    auto duration = std::chrono::duration_cast<std::chrono::nanoseconds>(end_time - start_time);
    std::cout << "\n" << " Time: " << duration.count() << "\n";
    return 1;
}
static long A(long m, long n)
{
    if (m == 0)
        return n + 1;
    if (n == 0)
        return A(m - 1, 1);
    return A(m - 1, A(m, n - 1));
}
static long AkkermanNotRecurse(long m, long n)
{
    if (m == 0) return n + 1;
    else if (m == 1)return n + 2;
    else if (m == 2) return 2 * n + 3;
    else if (m == 3) return pow(2, n + 3) - 3;
    else return 0;
}

int main()
{
    std::cout<< "m\\n";
    for (int i = 0; i < 10; i++) std::cout << "\t"<<i;
    
    for (int m = 0; m < 4; m++)
    {
        std::cout<<"\n"<<m;
        for (int n = 0; n < 15; n++)
        {
            std::cout<<"\t"<< AkkermanNotRecurse(m, n);
        }
    }
    std::cout << "Collatz!\n\n";
    for (int i = 900; i < 1000; i++)
    {
        std::cout << "\n\nCollatz for "<<i<<"\n";
        Collatz(i);
    }
}