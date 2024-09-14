#include <iostream>
#include <cmath>
#include <chrono>
#include <fstream>
#include <string>

double Y(double x)
{
    return(-std::log(std::abs(2.0*std::sin(x/2.0))));
}

__device__ void atomicAddAndCheck(double* sum, double value, double yVal, double eps, int* metCondition, int idx)
{
    unsigned long long int* addressAsUll = (unsigned long long int*)sum;
    unsigned long long int old = *addressAsUll, assumed;

    do {
        assumed = old;
        double assumedAsDouble = __longlong_as_double(assumed);
        double newValue = assumedAsDouble + value;
        old = atomicCAS(addressAsUll, assumed, __double_as_longlong(newValue));
        if (abs(newValue - yVal) <= eps)
        {
            atomicExch(&metCondition[idx], 1);
        }
    } while (assumed != old);
}

__global__ void computeTerms(double x, double* output, double* sum, double yVal, double eps, int* metCondition,int cycles)
{
    int idx = (threadIdx.x + blockIdx.x * blockDim.x);
    output[idx] = cos(((idx+1)+512*(cycles)) * x)/((idx+1)+512*(cycles));
    atomicAddAndCheck(sum, output[idx], yVal, eps, metCondition, idx);
}

double S(double x, double eps, int& n)
{
    double sum = 0;
    double yVal = Y(x);

    int cycles = 0;

    int* metCondition = new int[512];

    while (true)
    {
        double* dOutput;
        cudaMalloc(&dOutput, 512 * sizeof(double));

        double* dSum;
        cudaMalloc(&dSum, sizeof(double));
        cudaMemcpy(dSum, &sum, sizeof(double), cudaMemcpyHostToDevice);

        int* dMetCondition;
        cudaMalloc(&dMetCondition, 512 * sizeof(int));

        computeTerms<<<1, 512>>>(x, dOutput, dSum, yVal, eps, dMetCondition, cycles);

        cudaMemcpy(&sum, dSum, sizeof(double), cudaMemcpyDeviceToHost);
        cudaMemcpy(metCondition, dMetCondition, 512 * sizeof(int), cudaMemcpyDeviceToHost);

        cudaFree(dOutput);
        cudaFree(dSum);
        cudaFree(dMetCondition);

        for (int i = 0; i < 512; i++)
        {
            if (metCondition[i] == 1)
            {
                n += i;
                break;
            }
        }

        cycles++;

        if (std::abs(sum - yVal) <= eps)
        {
            break;
        }
    }

    delete[] metCondition;

    std::cout << "Cycles: " << cycles << std::endl;

    return sum;
}

int main()
{
    std::string logFileDataName;
    std::string logFileName;

    logFileName = "logCUDA.txt";
    logFileDataName = "logCUDAData.txt";

    double a, b, h, eps;
    // std::cout << "Enter a, b, h, eps: ";
    // std::cin >> a >> b >> h >> eps;
    a = 0.1, b = 0.97, h = 0.001, eps = 0.001;

    std::ofstream logFile(logFileName);
    std::ofstream logFileData(logFileDataName);

    for (double x = a; std::abs(x - b) > eps; x += h)
    {
        int n = 1;

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
