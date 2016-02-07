#include <stdio.h>
#include <math.h>
/* Calculates cos(x) by using a Taylor approximation:
   cos(x) = x^0/(0!) - x^2/(2!) + x^4/(4!) - x^6/(6!) + x^8/(8!) - ... */

int main(void)
{    
    int   k,         // dummy variable k
    						epsilon,			// number of iterations
    						n = 0;					// count of iterations for testing purposes

    float x,         // parameter of cos(x), in radians
          //epsilon,   // precision of cos(x) (cos = sum ± epsilon)
          sum,       // sum of the terms of the polynomial series
          term;      // variable that stores each term of the summation

    printf("Enter x: ");
				scanf("%f", &x);
				printf("Enter epsilon (number of iterations): ");
				//scanf("%f", &epsilon);
				scanf("%d", &epsilon);

    sum = term = 1, k = 0;

    //while (fabs(term) >= epsilon)
    while (epsilon > 0)
    // while absolute of term is smaller than epsilon
    {
        k += 2;
        term *= -(x*x)/(k*(k-1));
        sum += term;
        printf("K = %d \nx = %d\nsum = %f\nterm = %f\n", k*(k-1), n, sum, term);
        n++;
        epsilon--;
    }

    printf("cos(%f) = %f\n", x, sum);
    printf("Number of iterations: %d", n);

    return 0;
}
