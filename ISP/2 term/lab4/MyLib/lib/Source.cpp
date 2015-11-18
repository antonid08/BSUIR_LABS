#include <stdio.h>


extern "C" 
{
	__declspec(dllexport) void HelloWorldFunction() {
		printf ("Hello World From DLL lib!\n");
	}

	__declspec(dllexport) int Add(int a, int b) {
		return a + b;
	}

	__declspec(dllexport) void swap(int* a, int* b){
		int temp;
		temp = *a;
		*a = *b;
		*b = temp;
	}
}