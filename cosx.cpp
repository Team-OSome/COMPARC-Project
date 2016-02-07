#include <stdio.h>
#include <math.h>
/* Calculates cos(x) by using a Taylor approximation:
   cos(x) = x^0/(0!) - x^2/(2!) + x^4/(4!) - x^6/(6!) + x^8/(8!) - ... */

int main(void)
{    
    int   k;         // dummy variable k

    float x,         // parameter of cos(x), in radians
          epsilon,   // precision of cos(x) (cos = sum ± epsilon)
          sum,       // sum of the terms of the polynomial series
          term;      // variable that stores each term of the summation

    printf("Enter x: ");
				scanf("%f", &x);
				printf("Enter epsilon: ");
				scanf("%f", &epsilon);

    sum = term = 1, k = 0;

    while (fabs(term) >= epsilon)
    // while abs(term) is smaller than epsilon
    {
        k += 2;
        term *= -(x*x)/(k*(k-1));
        sum += term;
    }

    printf("cos(%f) = %f\n", x, sum);

    return 0;
}
