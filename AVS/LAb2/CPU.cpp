#include <iostream>
#include <cmath>
#include <chrono>
#include <fstream>
#include <string>
#include <future>
#include <vector>
#include <mutex>

double Y(double x)
{
    return (-std::log(std::abs(2.0*std::sin(x/2.0))));
}

double S(double x, double eps, int& n)
{
    double sum = 0;
    double yVal = Y(x);

    n = 1;

    std::vector<std::future<double>> futures;
    std::mutex mtx;

    bool stop = false;

    double sumsMade = 0;

    while (!stop)
    {
        int currentN = n;
        futures.push_back(std::async(std::launch::async, [currentN, x]
        {
            return std::cos(currentN * x)/currentN;
        }));

        n++;

        // Wait for 4 iterations
        if (n % 4 == 0)
        { 
            for (auto& future : futures)
            {
                mtx.lock();
                double val = future.get();
                if (std::abs(sum - yVal) <= eps)
                {
                    mtx.unlock();
                    stop = true;
                    n = sumsMade;
                    std::cout << "Latest n: " << n << std::endl;
                    break;
                }

                sum += val;
                sumsMade++;
                mtx.unlock();
            }

            futures.clear();
        }
    }

    // Get the remaining results
    for (auto& future : futures)
    {
        mtx.lock();

        if (std::abs(sum - yVal) <= eps)
        {
            mtx.unlock();
            n = sumsMade;
            std::cout << "Latest n: " << n << std::endl;
            break;
        }

        sum += future.get();
        sumsMade++;
        mtx.unlock();
    }

    return sum;
}

int main()
{
    bool mode;
    std::cout << "Enter 1 for SMT On, 0 otherwise: " << std::endl;
    std::cin >> mode;

    std::string logFileDataName;
    std::string logFileName;

    if (mode)
    {
        logFileName = "logSMTOn.txt";
        logFileDataName = "logSMTOnData.txt";
    } else
    {
        logFileName = "logSMTOff.txt";
        logFileDataName = "logSMTOffData.txt";
    }

    double a, b, h, eps;
    // std::cout << "Enter a, b, h, eps: ";
    // std::cin >> a >> b >> h >> eps;
    a = 0.1, b = 0.97, h = 0.001, eps = 0.001;

    std::ofstream logFile(logFileName);
    std::ofstream logFileData(logFileDataName);

    for (double x = a; std::abs(x - b) > eps; x += h)
    {
        int n=1;

        auto start = std::chrono::high_resolution_clock::now();

        double y = Y(x);
        double s = S(x, eps, n);

        auto end = std::chrono::high_resolution_clock::now();
        auto duration = std::chrono::duration_cast<std::chrono::nanoseconds>(end - start).count();

        std::cout << "x = " << x << ", Y(x) = " << y << ", S(x) = " << s << ", n = " << n << ", time = " << duration << " ns" << std::endl;
        logFile << "x = " << x << ", Y(x) = " << y << ", S(x) = " << s << ", n = " << n << ", time = " << duration << " ns" << std::endl;
        logFileData << n << " " << duration << std::endl;
    }

    return 0;
}