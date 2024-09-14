#include <iostream> 
#include <cmath>
#include <fstream>
#include <chrono>
#define CL_TARGET_OPENCL_VERSION 300
#ifdef __APPLE__
#include <OpenCL/opencl.h>
#else
#include <CL/cl.h>
#endif
 
#define MAX_SOURCE_SIZE (0x100000)
std::string logFileDataName = "logAMDGPUData.txt";
std::string logFileName = "logAMDGPU.txt";

std::ofstream logFile(logFileName);
std::ofstream logFileData(logFileDataName);

double Y(double x)
{
    return (-std::log(std::abs(2.0*std::sin(x/2.0))));
}

void S(double x, double eps)
{
    double sum = 0;
    int i= 0;
    int n = 0;
    double yVal = Y(x);
    double *X = (double*)malloc(sizeof(double)*1);
    int *cycles = (int*)malloc(sizeof(int)*1);
    X[0] = x;
    cycles[0]=0;
    FILE *fp;
    char *source_str;
    size_t source_size;
    fp = fopen("lab2.cl", "r");
    if (!fp) {
        fprintf(stderr, "Failed to load kernel.\n");
        exit(1);
    }
    source_str = (char*)malloc(MAX_SOURCE_SIZE);
    source_size = fread( source_str, 1, MAX_SOURCE_SIZE, fp);
    fclose( fp );
 
    cl_platform_id platform_id = NULL;
    cl_device_id device_id = NULL;   
    cl_uint ret_num_devices;
    cl_uint ret_num_platforms;
    cl_int ret = clGetPlatformIDs(1, &platform_id, &ret_num_platforms);
    ret = clGetDeviceIDs( platform_id, CL_DEVICE_TYPE_DEFAULT, 1, 
            &device_id, &ret_num_devices);
 
    // Create an OpenCL context
    cl_context context = clCreateContext( NULL, 1, &device_id, NULL, NULL, &ret);
 
    // Create a command queue
    cl_command_queue command_queue = clCreateCommandQueue(context, device_id, 0, &ret);
 
    // Create memory buffers on the device for each vector 
    cl_mem X_mem_obj = clCreateBuffer(context, CL_MEM_READ_ONLY, 
             1 * sizeof(double), NULL, &ret);
    cl_mem cycles_mem_obj = clCreateBuffer(context, CL_MEM_READ_ONLY, 
            1 * sizeof(int), NULL, &ret);
    cl_mem results_mem_obj = clCreateBuffer(context, CL_MEM_WRITE_ONLY, 
            2048 * sizeof(double), NULL, &ret);
 
    // Copy the  X and cycles to their respective memory buffers
    ret = clEnqueueWriteBuffer(command_queue, X_mem_obj, CL_TRUE, 0,
            1 * sizeof(double), X, 0, NULL, NULL);
    ret = clEnqueueWriteBuffer(command_queue, cycles_mem_obj, CL_TRUE, 0,
           1 * sizeof(int), cycles, 0, NULL, NULL);
 
    // Create a program from the kernel source
    cl_program program = clCreateProgramWithSource(context, 1, 
            (const char **)&source_str, (const size_t *)&source_size, &ret);
 
    // Build the program
    ret = clBuildProgram(program, 1, &device_id, NULL, NULL, NULL);
 
    // Create the OpenCL kernel
    cl_kernel kernel = clCreateKernel(program, "lab2", &ret);
    double *results = new double[2048];
    auto start = std::chrono::high_resolution_clock::now();
    while(true){
            // Set the arguments of the kernel
            ret = clSetKernelArg(kernel, 0, sizeof(cl_mem), (void *)&X_mem_obj);
            ret = clSetKernelArg(kernel, 1, sizeof(cl_mem), (void *)&results_mem_obj);
            ret = clSetKernelArg(kernel, 2, sizeof(cl_mem), (void *)&cycles_mem_obj);

            // Copy the updated cycles to the cycles memory buffer
            ret = clEnqueueWriteBuffer(command_queue, cycles_mem_obj, CL_TRUE, 0,
                                   1 * sizeof(int), cycles, 0, NULL, NULL);

            // Execute the OpenCL kernel on the list
            size_t global_item_size = 2048; // Process the entire lists
            size_t local_item_size = 64; // Divide work items into groups of 64
            ret = clEnqueueNDRangeKernel(command_queue, kernel, 1, NULL, 
                    &global_item_size, &local_item_size, 0, NULL, NULL);
        
            ret = clEnqueueReadBuffer(command_queue, results_mem_obj, CL_TRUE, 0, 
                    2048 * sizeof(double), results, 0, NULL, NULL);

            while (i < 2048 && std::abs(sum - yVal) > eps)
                {
                    //std::cout << "results:"<< i<< " "<< results[i];
                    sum += results[i];
                    i++;
                }
            n +=i;

            if (std::abs(sum - yVal) <= eps)
            {
                std::cout << "Needed esp";
                break;
            }
    }
    auto end = std::chrono::high_resolution_clock::now();
    auto duration = std::chrono::duration_cast<std::chrono::nanoseconds>(end - start).count();

    // Clean up
    ret = clFlush(command_queue);
    ret = clFinish(command_queue);
    ret = clReleaseKernel(kernel);
    ret = clReleaseProgram(program);
    ret = clReleaseMemObject(results_mem_obj);
    ret = clReleaseMemObject(cycles_mem_obj);
    ret = clReleaseMemObject(X_mem_obj);
    ret = clReleaseCommandQueue(command_queue);
    ret = clReleaseContext(context);
    free(X);
    free(cycles);
    delete[] results;

    // Display the result to the screen
    std::cout << "x = " << x << ", Y(x) = " << yVal << ", S(x) = " << sum << ", n = " << n << ", time = " << duration << " ns" << std::endl;
    logFile << "x = " << x << ", Y(x) = " << yVal << ", S(x) = " << sum << ", n = " << n << ", time = " << duration << " ns" << std::endl;
    logFileData << n << " " << duration << std::endl;
}

int main()
{
    double a, b, h, eps;
    // std::cout << "Enter a, b, h, eps: ";
    // std::cin >> a >> b >> h >> eps;
    a = 0.1, b = 0.97, h = 0.001, eps = 0.001;

    for (double x = a; std::abs(x - b) > eps; x += h)
    {
        int n = 0;

        S(x, eps);
    }

    return 0;
}