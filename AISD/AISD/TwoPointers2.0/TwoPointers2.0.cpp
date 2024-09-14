#include <iostream>
#include <vector>

int main()
{
    std::cout << "Hello World!\n";
    int n, s;
    std::cin >> n >> s;

    std::vector<int> a(n);
    for (int i = 0; i < n; i++) std::cin >> a[i];

    int segmentCount = 0;
    int curSum = 0;
    bool theLastElement = 0;

    for (int l = 0, r = 0; r < n && l < n; r++)
    {
        curSum += a[r];

        if (curSum > s)
        {
            l++;
            r = l;
            curSum = a[r];
        }
        if (curSum <= s)
        {
            segmentCount++;
        }

        std::cout << "sum = " << curSum << "\n";
        std::cout << "l = " << l << "\n";
        std::cout << "r = " << r << "\n";
        std::cout << "segmentCount = " << segmentCount << "\n";

        if (r == n - 1 && l < n)
        {
            std::cout << "last cycle: segmentCount=" << segmentCount<<"\n";
            curSum = a[l];
            l++;
            r = l;
            while (l < n)
            {
                std::cout << "sum = " << curSum << "\n";
                std::cout << "l = " << l << "\n";
                std::cout << "r = " << r << "\n";
                std::cout << "segmentCount = " << segmentCount << "\n";
                
                curSum += a[l];
                if (curSum > s)
                {
                    l++;
                    r = l;
                    curSum = a[r];
                }
                if (curSum <= s)
                {
                    segmentCount++;
                }
                l++;

            }
        }

    }
    std::cout << segmentCount;
}
