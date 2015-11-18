#include <stdio.h>
#include <math.h>

int main() {
	long long  number;
	long int quantity = 0;
	int digit;
	int devisor = 10;
	long long buf;

	scanf ("%lld", &number);

	for (long long candidate = 1; candidate*candidate <= number; ++candidate) {
		if ((number % candidate) == 0) {
			buf = candidate;
			do  {
				digit = buf % devisor;
				buf /= devisor;
				if (digit == 4 || digit == 7){
					quantity++;
					break;
				}
			} while (buf != 0);
		}
	}

	printf("%ld\n", quantity);

	return 0;
}