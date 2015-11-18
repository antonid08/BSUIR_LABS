/* 
*This program transfers the entered number
* from a decimal numeral system to the binary.
* If you want, it is possible to convert unlimited number of rooms.
* Numbers have to enter range from (-10^9) to (10^9)
*/

#include <stdio.h>

#define YES 'y'

int main() {
    int number;
    int tmp_buffer;
    int digits;

    char continue_or_no = YES;
	
    do {
        printf("Введите число: ");
        scanf("%d", &number);
        printf("Число в двоичной системе: ");

        if (number < 0) {
            number *= -1;
            number = ~(number) + 1;
        }

        digits = 0;
        tmp_buffer = number;
        while ((number / 2) != 0) { 
            ++digits;
            number /= 2;
        }
        number = tmp_buffer;
        
        for (int counter = digits+1; counter >= 0; --counter) { 
            printf("%d", (number >> counter) & 1 );
        }
        printf("\n");

        printf("Еще число? (y - да, остальное - нет)\n");
        continue_or_no = getchar();
    } while ((continue_or_no = getchar()) == YES);
	
    return 0;
}