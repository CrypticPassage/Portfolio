#include <iostream>
#include <time.h>

using namespace std;

int getMax(int arr[], int n)
{
	int mx = arr[0];
	for (int i = 1; i < n; i++)
		if (arr[i] > mx)
			mx = arr[i];
	return mx;
}
void countSort(int arr[], int n, int exp)
{
	int *output = new int[n];
	int i, count[10] = { 0 };

	for (i = 0; i < n; i++)
	{
		count[(arr[i] / exp) % 10]++;
	}

	for (i = 1; i < 10; i++)
	{
		count[i] += count[i - 1];
	}

	for (i = n - 1; i >= 0; i--)
	{
		output[count[(arr[i] / exp) % 10] - 1] = arr[i];
		count[(arr[i] / exp) % 10]--;
	}

	for (i = 0; i < n; i++)
	{
		arr[i] = output[i];
	}
}
void radixsort(int arr[], int n)
{
	int m = getMax(arr, n);
	for (int exp = 1; m / exp > 0; exp *= 10)
	{
		countSort(arr, n, exp);
	}
}
// поразрядный алгоритм

void bitsort(int *const array, int const n) 
{
	int *queue_0 = new int [n];
	int *queue_1 = new int [n];

	int const bits = sizeof(int) * 8;

	for (int i = 0, mask = 1; i < bits; ++i, mask <<= 1) 
	{
		int tail_0 = 0;
		int tail_1 = 0;

		for (int i = 0; i < n; ++i) 
		{
			if ((array[i] & mask) == 0) 
			{
				queue_0[tail_0] = array[i];
				++tail_0;
			}
			else 
			{
				queue_1[tail_1] = array[i];
				++tail_1;
			}
		}

		int m = 0;

		for (int j = 0; j < tail_0; ++m, ++j) 
		{
			array[m] = queue_0[j];
		}

		for (int j = 0; j < tail_1; ++m, ++j) 
		{
			array[m] = queue_1[j];
		}
	}
}
// битовая сортировка

    int main()
	{
		setlocale(LC_ALL, "rus");
		int choice;
		while (1)
		{
			cout << "Сравнительный анализ двух алгоритмов. Наш массив: " << endl;
			int massiv[15] = {};
			int n = sizeof(massiv) / sizeof(massiv[0]);
			srand(time(NULL));
			for (int i = 0; i < n; i++)
			{
				massiv[i] = rand();
				cout << massiv[i] << " ";
			}
			cout << " " << endl;
			cout << "Начинаем сортировку? " << endl;
			cout << "1 - да" << endl;
			cout << "2 - нет" << endl;
			cin >> choice;
			switch (choice)
			{
			case 1:
				clock_t time;
				time = clock();
				radixsort(massiv, n);
				
				for (int i = 0; i < n; i++)
				{
					cout << massiv[i] << " ";
				}
				time = clock() - time;
				cout << " " << endl;
				cout << "Коэффициент поразрядной сортировки: " << (double)time / CLOCKS_PER_SEC << endl;
				cout << " " << endl;

				clock_t time1;
				time1 = clock();
				bitsort(massiv, n);
				for (int i = 0; i < n; i++)
				{
					cout << massiv[i] << " ";
				}
				time1 = clock() - time1;
				cout << " " << endl;
				cout << "Коэффициент битовой сортировки: " << (double)time1 / CLOCKS_PER_SEC << endl;
				cout << " " << endl;
				break;
			case 2:
				exit(0);
			}
		}
		return 0;
}
