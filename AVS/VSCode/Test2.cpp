#include <iostream>
#include <cmath>
#include <chrono>
#include <fstream>
float Y(float x)
{
  const float one = 1.0f;
  const float two = 2.0f;
  const float four = 4.0f;
  float Y = 0.0f;
  __asm__(
    "fld %2;" //2
    "fld %1;" //1
    "fdivp;" // 1/2

    "fldln2;" //ln2
    "fld %3;" //x
    "fyl2x;" //log2(x)*ln(2) = ln(x)
    "fmulp;" // 1/2*(ln(x))      
    "fstp %0;"

    : "=m" (Y)
    : "m" (one), "m" (two), "m" (x)
  );
  return Y;
}

float S(float x, float eps, float& n)
{
  const float one = 1.0f;
  const float two = 2.0f;
  float sum = 0.0f;
  float res;
  const float pi = 3.14159265358979323846f;
  const float four = 4.0f;
  n = 1.0f;
  float S = 0.0f;
  float totalS = 0.0f;

  while (std::abs(totalS - Y(x)) > eps)
  {
    //std::cout << "Before " << n << std::endl;
    float tempS1 = 0.0f;
    float tempS2 = 0.0f;
    //1/(2*k+1)
    asm
    (
      "fld %2;" //2
      "fld %3;" //k
      "fmulp;"  //2*k
      "fld %1;" //1
      "faddp;"  //2*k+1
      "fld %1;" //1
      "fdivp;"  //1/(2*k+1)            
      "fstp %0;"
                     
      : "=m" (tempS1)
      : "m" (one), "m" (two), "m" (n), "m" (x)
    );
    //std::cout << "x = " << x << "k = " << n << std::endl;
   // std::cout << "tempS1 = " << tempS1 << std::endl;
    //(x-1)/(x+1)
    asm
    (
      "fld %3;" //x
      "fld %1;" //1
      "fadd;"   //x+1

      "fld %1;" //1
      "fld %3;" //x
      "fsub;"   //x-1

      "fdivp;" //(x-1)/(x+1)
            
      "fstp %0;"
            
      : "=m" (tempS2)
      : "m" (one), "m" (n), "m" (x)
    );
    //std::cout << "tempS2 = " << tempS2 << std::endl;
    float tempS2mul = tempS2;
    //std::cout<<"n = "<<n<<"\n";
    for(int i = 1; i < (2*n + 1); i++)
    {
      tempS2 *= tempS2mul;
      //std::cout<<"Up = "<<tempS2<<"\n";
    }
    //std::cout << "tempS2* = " << tempS2 << std::endl;
    asm
    (
      "fld %2;" //1/(2*k+1)
      "fld %1;" //((x-1)/(x+1))^(2*n + 1)         
      "fmulp;"  //S
      "fld %0;"
      "faddp;"
            
      "fstp %0;"
            
      : "=m" (S)
      : "m" (tempS2), "m" (tempS1)
    );
    totalS+=S;
    //std::cout<< "S = " << totalS << std::endl;
    //0.sum=S;1
    if (n>1000) break;
    n+=1.0f;
    //std::cout<< "totalS =  " <<totalS<< "Y(x) = " << Y(x) << " std::abs(totalS - Y(x))" << std::abs(totalS - Y(x)) << std::endl;
  }
  //return sum;
  return totalS;
}
int main()
{
  float a, b, h, eps;
  float k=1.0f;
  std::cout << "Enter a, b, h, eps: ";
  std::cin >> a >> b >> h >> eps;

  for (double x = a; x < b; x += h)
  {
    float n=1.0f;
    auto start = std::chrono::high_resolution_clock::now();
    float y = Y(x);
    float s = S(x, eps, n);
    auto end = std::chrono::high_resolution_clock::now();
    auto duration = std::chrono::duration_cast<std::chrono::nanoseconds>(end - start).count();
    //std::cout << "x = " << x << ", Y(x) = " << y << ", S(x) = " << s << ", n = " << n << ", time = " << duration << " ns" <<
    //std::endl;
    std::cout<<n<<" "<<duration<<std::endl;
  }
  return 0;
}