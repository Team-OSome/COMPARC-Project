#include <stdio.h>
#include <math.h>

/* Calculates cos(x) by using a Taylor approximation:
   cos(x) = x^0/(0!) - x^2/(2!) + x^4/(4!) - x^6/(6!) + x^8/(8!) - ... */

int main(void)
{    
    int   k = 0,         // dummy variable k
    						epsilon,							// number of iterations
    						n = 0;									// count of iterations for testing purposes

    float x,         				// parameter of cos(x), in radians
          sum = 1,       // sum of the terms of the polynomial series
          term = 1;      // variable that stores each term of the summation

    printf("Enter x in radians: ");
				scanf("%f", &x);
				printf("Enter epsilon (number of iterations): ");
				scanf("%d", &epsilon);

    while (epsilon > 0)   // while the summation is not yet done
    {
        k += 2;
        term *= -(x*x)/(k*(k-1));
        sum += term;
        epsilon--;
        //printf("K = %d \nx = %d\nsum = %f\nterm = %f\n", k*(k-1), n, sum, term);
        //n++;
    }

    printf("cos(%f) = %f\n", x, sum);
    //printf("Number of iterations: %d", n);

    return 0;
}
