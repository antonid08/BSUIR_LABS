#include <stdio.h>

int quantity_validate()
{
	int element;

	while (scanf ("%d", &element) != 1 || element < 1 || element > 100) {
		printf("Введите корректные данные: ");
		while (getchar() != '\n');
	}
}

int main()
{
	int quantity_of_passwords = 0;

	char passwords[100][100];

	quantity_of_passwords = quantity_validate();

	for (int counter = 0; counter < quantity_of_passwords; ++counter) {
			scanf("%s", &passwords[counter][]);
		
	}	
	return 0;
}