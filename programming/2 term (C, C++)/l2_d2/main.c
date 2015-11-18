#include <stdio.h>

int u (int* mass, int counter, int quantity)
{	
	int vol = 1;
	int middle;

	middle = (quantity / 2) + 1;
	//if (counter <= middle) {
		if ((mass[counter +1] - mass[counter]) == 1) {
			vol = 1;
		} else {
			vol = 0;
		}
	//} 
	/*if (counter > middle) {
		if ((mass[counter] - mass[counter-1]) == 1) {
			vol = 1;
		} else {
			vol = 0;
		}
	}*/

	return vol;
}

int main()
{
	int quantity;
	int length = 0;
	int max_length = 0;

	scanf ("%d", &quantity);

	int heights[quantity];

	for (int counter = 0; counter < quantity; ++counter) {
		scanf("%d", &heights[counter]);
	}

	for (int counter = 0; counter <= quantity ; ++counter) {
		if (u(heights, counter, quantity) == 1) {
			length++;
			if (length > max_length){ max_length = length; }
		} else {
			length = 0;
		}
	}

	printf ("%d", length);

	return 0;
}