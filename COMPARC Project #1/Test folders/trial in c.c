#include<stdio.h>

int factorial(int ctr)
{
	int i, n = ctr, f = 1;
	for(i = 0; i < ctr; i++)
	{
		f = f * n;
		n--;
	}
	return f;
}

float exponent(float input, int exp)
{
	int i;
	float product = 1;
	
	for (i = 0 ; i < exp ; i ++)
	{
		product *= input;
	}
	return product;
}

int main()
{
	int i, n = 0, f, iterations; 
	float rad, formula = 0, ans = 0;

	
	printf("Enter value in radians: ");
	scanf("%f", &rad);
	printf("Enter number of iterations: ");
	scanf("%d", &iterations);
	
	for(i = 1 ; i <= iterations ; i ++)
	{
		n += 2;
		printf("\n");
		printf("Iteration #%d\n", i);
	
		if( i%2==0)
		{
			ans = ans + exponent(rad,n)/factorial(n);
		}
		else
		{
			ans = ans - exponent(rad,n)/factorial(n);
		}
			
		printf("cos(%f) = %f\n",rad,ans);
	}
	
	
	printf("\n\n");
	ans = 1 + ans;
	printf("FINAL ANSWER:\nCOS(%f) = %f",rad,ans);
	
}
