#define YES 1
#define NO 0
#define DEVISOR 10

#include <stdio.h>

// Calculation of numbers in which figures increase.
void increase(int max_number)
{	
	int number;	
	int prev_digit;
	int digit;
	int current_number;
	int more = 0;

	for (current_number = 10; current_number <= max_number; ++current_number){
		number = current_number;		
    	digit = number % DEVISOR;
    	number = number / DEVISOR;
    	prev_digit = digit;

		while (number != 0) { 
    		digit = number % DEVISOR;
    		number = number / DEVISOR;
    		if (prev_digit > digit){
    			more = YES;
    			} else {
    			more = NO;
    			break;
    			}
    	prev_digit = digit;
 		}	

 		if (more == YES) {
 			printf("%d\n", current_number);
 		}
	}
	
}

//Calculation of numbers in which figures increase.
void decrease(int max_number)
{
	int number;
	int prev_digit;
	int digit;
	int current_number;
	int more = 0;

	for (current_number = DEVISOR; current_number <= max_number; ++current_number){
		number = current_number;		
    	digit = number % DEVISOR;
    	number = number / DEVISOR;
    	prev_digit = digit;

		while (number != 0) { 
    		digit = number % DEVISOR;
    		number = number / DEVISOR;
    		if (prev_digit < digit){
    			more = NO;
    			} else {
    			more = YES;
    			break;
    			}
    	prev_digit = digit;
 			}	
 		
 		if (more == NO) {
 			printf("%d\n", current_number);
 		}
	}
}

int main()
{
	int max_number; 

	scanf ("%d", &max_number);

	printf ("\nDigits increase in the following figures: \n");
	increase(max_number);

	printf ("\nDigits decrease in the following figures: \n");
	decrease(max_number);
	return 0;
}