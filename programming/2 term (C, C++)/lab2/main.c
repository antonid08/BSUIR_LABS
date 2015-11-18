#define TRUE 1
#define FALSE 0

#define METERS_IN_KILOMETER 1000
#define CENTIMETERS_IN_METER 100
#define MILLIMIYETRA_IN_CENTIMETER 10

#define MILLIMIYETRA_IN_VERSHOK 44.45
#define VERSHOKS_IN_ARSHIN 16
#define ARSHINS_IN_SAZHEN 3
#define SAZHENS_IN_VERST 500

#define MILLIMIYETRA_IN_INCH 25.4
#define INCHES_IN_FOOT 12
#define FOOTS_IN_YARD 3
#define YARDS_IN_MILE 1760



#include <stdio.h>
#include <string.h>
#include <stdlib.h>

struct length 
{
	double kilometers;
	double meters;
	double centimeters;
	long int millimiyetra;
	double versts;
	double arshins;
	double sazhens;
	double vershoks;
	double miles;
	double yards;
	double foots;
	double inches;
};

enum choise 
{
	C_IN_KILOMETERS = 1,
	C_IN_METERS = 2,
	C_IN_CENTIMETERS = 3,
	C_IN_MILLIMYETRYA = 4,
	C_EXIT_FROM_FIRST_MENU = 5,
	C_TRANSTALE_TO_ENGLISH = 1,
	C_TRANSLATE_TO_RUSSIAN = 2,
	C_REPORT = 3,
	C_NEW_LENTH = 4,
	C_EXIT_FROM_SEC_MENU = 5
};

void to_millimiyetra(struct length* p_length, int choise)
{	
	switch (choise) {
		case C_IN_KILOMETERS:
		p_length->millimiyetra = p_length->kilometers * MILLIMIYETRA_IN_CENTIMETER * CENTIMETERS_IN_METER *
		METERS_IN_KILOMETER;
		break;

		case C_IN_METERS:
		p_length->millimiyetra = p_length->meters * MILLIMIYETRA_IN_CENTIMETER * CENTIMETERS_IN_METER;
		break;

		case C_IN_CENTIMETERS: 
		p_length->millimiyetra = p_length->centimeters * MILLIMIYETRA_IN_CENTIMETER;
		break;

		default :

		break;
	}	
}

void  to_english(struct length* p_length)
{
	p_length->inches = p_length->millimiyetra / MILLIMIYETRA_IN_INCH;
	p_length->foots = p_length->inches / INCHES_IN_FOOT;
	p_length->yards = p_length->foots / FOOTS_IN_YARD;
	p_length->miles = p_length->yards / YARDS_IN_MILE;

	printf ("\nАнглийские традиционные еденицы: \n");
	printf ("Мили: %lf\n", p_length->miles);
	printf ("Ярды: %lf\n", p_length->yards);
	printf ("Футы: %lf\n", p_length->foots);
	printf ("Дюймы: %lf\n", p_length->inches);
}

void to_russian(struct length* p_length)
{
	p_length->vershoks = p_length->millimiyetra / MILLIMIYETRA_IN_VERSHOK;
	p_length->arshins = p_length->vershoks / VERSHOKS_IN_ARSHIN;
	p_length->sazhens = p_length->arshins / ARSHINS_IN_SAZHEN;
	p_length->versts = p_length->sazhens / SAZHENS_IN_VERST;


	printf ("\nРусские традиционные еденицы: \n");
	printf ("Версты: %lf\n", p_length->versts);
	printf ("Сажени: %lf\n", p_length->sazhens);
	printf ("Аршины: %lf\n", p_length->arshins);
	printf ("Вершки: %lf\n", p_length->vershoks);
}

void to_default(struct length* p_length)
{
	p_length->centimeters = p_length->millimiyetra / MILLIMIYETRA_IN_CENTIMETER;
	p_length->meters = p_length->centimeters / CENTIMETERS_IN_METER;
	p_length->kilometers = p_length->meters / METERS_IN_KILOMETER;

	printf ("\n\nОтчет: \n");
	printf ("Километры: %lf\n", p_length->kilometers);
	printf ("Метры: %lf\n", p_length->meters);
	printf ("Сантиметры: %lf\n", p_length->centimeters);
	printf ("Миллиметры: %ld\n", p_length->millimiyetra);
}

 i = volidate_first_menu();
int volidate_first_menu() {

	int element;

	while (scanf ("%d", &element) != 1  || element < 1 || element > 5) {
		printf("Введите корректные данные: ");
		while (getchar() != '\n');
	}

	return element;
}

int volidate_second_menu() {

	int element;

	while (scanf ("%d", &element) != 1  || element < 1 || element > 5) {
		printf("Введите корректные данные: ");
		while (getchar() != '\n');
	}	

	return element;
}

double length_input_volidate() 
{
	double element;

	while (scanf ("%lf", &element) != 1 || element < 0) {
		printf("Введите корректные данные: ");
		while (getchar() != '\n');
	}

	return element;
}

int main()
{	
	struct length mylength;

	int choise = 0;
	int number_of_menu;
	
	do { 
		printf ("1 - ввод длины в километрах.\n");
		printf ("2 - ввод длины в метрах.\n");
		printf ("3 - ввод длины в сантиметрах.\n");
		printf ("4 - ввод длины в миллиметрах.\n");
		printf ("5 - выход из программы.\n");
		printf("Выбор: ");

		number_of_menu = 1;
		choise = volidate_first_menu();

		mylength.kilometers = 0;
		mylength.meters = 0;
		mylength.centimeters = 0;
		mylength.millimiyetra = 0;

		switch (choise) {
			case C_IN_KILOMETERS :
			printf("Ввод (км): ");
			mylength.kilometers = length_input_volidate();
			break;

			case C_IN_METERS :
			printf("Ввод (м): ");
			mylength.meters = length_input_volidate();
			break;

			case C_IN_CENTIMETERS :
			printf("Ввод (см): ");
			mylength.centimeters = length_input_volidate();
			break;

			case C_IN_MILLIMYETRYA :
			printf("Ввод (мм): ");
			mylength.millimiyetra = length_input_volidate();
			break;

			case C_EXIT_FROM_FIRST_MENU :
			printf("Программа завершила работу!\n");
			exit(0);
			break;

			default :

			break;
		}

		to_millimiyetra(&mylength, choise);

		do {
			printf ("\n1 - перевод в английские традиционные еденицы.\n");
			printf ("2 - перевод в русские традиционные еденицы.\n");
			printf ("3 - вывод отчета в км, м, см, мм.\n");
			printf ("4 - задать новую длину.\n");
			printf ("5 - выход из программы.\n");
			printf ("Выбор: ");

			number_of_menu = 2;
			choise = volidate_second_menu();

			switch (choise) {
				case C_TRANSTALE_TO_ENGLISH:
				to_english(&mylength);
				break;

				case C_TRANSLATE_TO_RUSSIAN:
				to_russian(&mylength);
				break;

				case C_REPORT: 
				to_default(&mylength);
				break;

				case C_NEW_LENTH:
				printf("\n");
				break;

				case C_EXIT_FROM_SEC_MENU:
				printf("Программа завершила работу!\n");
				exit(0);
				break;

				default :
				//;
				break;
			}

		} while (choise != C_NEW_LENTH);

	}	while(1);

	return 0;
}