#include <iostream>
#include <cstdlib>
#include <algorithm>
#include <ctime>

using namespace std;

void standartswap(int array[], int n)
{
	int i;
	for (i = 0; i < n; i++) 
	{
		for (int j(i + 1); j < n; j++)
		{
			if (array[i] > array[j])
			{
				swap(array[i], array[j]);
			}
		}
	}

	
}
// сортировка пузырька стандартным свапом

void swapwithdirectembaddingtemporarystorage(int array[], int n)
{
	int i;
	for (int i(0); i < n; i++)
	{
		for (int j(i + 1); j < n; j++)
		{
			if (array[i] > array[j])
			{
				int tmp = array[i];
				array[i] = array[j];
				array[j] = tmp;
			}
		}
	}
}
// сортировка пузырька прямым встраиванием с временным хранилищем

void swapwithoutdirectembaddingtemporarystorage(int array[], int n)
{
	int i;
	for (i = 0; i < n; i++)
	{
		for (int j(i + 1); j < n; j++)
		{
			if (array[i] > array[j])
			{
				array[i] ^= array[j];
				array[j] ^= array[i];
				array[i] ^= array[j];
			}
		}
	}


}
// сортировка пузырька прямым встраиванием без временного хранилища

void swapbubblewithtemp(int *xp, int *yp)
{
	int temp = *xp;
	*xp = *yp;
	*yp = temp;
}
void swapwithtemporarystorage(int array[], int n)
{
	int i;
	for (int i(0); i < n; i++)
	{
		for (int j(i + 1); j < n; j++)
		{
			if (array[i] > array[j])
			{
				swapbubblewithtemp(&array[i], &array[j]);
			}
		}
	}
}
// сортировка пузырька через swap с временным хранилищем

void swapbubblewithouttemp(int *xp, int *yp)
{
	*xp ^= *yp;
	*yp ^= *xp;
	*xp ^= *yp;
}
void swapwithouttemporarystorage(int array[], int n)
{
	int i;
	for (i = 0; i < n; i++)
	{
		for (int j(i + 1); j < n; j++)
		{
			if (array[i] > array[j])
			{
				swapbubblewithouttemp(&array[i], &array[j]);
			}
		}
	}
}
// сортировка пузырька через swap без временного хранилища

void shaker_sort_with_standart_swap(int array[], int n)
{
	int m = n;
	int i, j, k;
	for (i = 0; i < n;)
	{
		for (j = i + 1; j < n; j++)
		{
			if (array[j] < array[j - 1])
				swap(array[j], array[j - 1]);
		}
		
		m--;

		for (k = n - 1; k > i; k--)
		{
			if (array[k] < array[k - 1])
				swap(array[k], array[k - 1]);
		}
		i++;
	}


}
// сортировка шейкером стандартным свапом

void shaker_sort_direct_with_temporary(int array[], int n)
{
	int i, j, k;
	int m = n;
	for (i = 0; i < n;)
	{
		for (j = i + 1; j < n; j++)
		{
			if (array[j] < array[j - 1])
			{
				int tmp = array[j];
				array[j] = array[j-1];
				array[j-1] = tmp;
			}
		}

		m--;

		for (k = n - 1; k > i; k--)
		{
			if (array[k] < array[k - 1])
			{

				int tmp = array[k];
				array[k] = array[k-1];
				array[k-1] = tmp;
			}
		}
		i++;
	}


}
// сортировка шейкером прямым встраиванием с временным хранилищем

void shaker_sort_direct_without_temporary(int array[], int n)
{
	int m = n;
	int i, j, k;
	for (i = 0; i < n;)
	{
		for (j = i + 1; j < n; j++)
		{
			if (array[j] < array[j - 1])
			{
				array[j] ^= array[j-1];
				array[j-1] ^= array[j];
				array[j] ^= array[j-1];
			}
		}

		m--;

		for (k = n - 1; k > i; k--)
		{
			if (array[k] < array[k - 1])
			{
				array[k] ^= array[k - 1];
				array[k - 1] ^= array[k];
				array[k] ^= array[k - 1];
			}
		}
		i++;
	}

	
}
// сортировка шейкером прямым встраиванием без временного хранилища

void shaker_sort_swap_With_temporary(int *xp, int *yp)
{
	int temp = *xp;
	*xp = *yp;
	*yp = temp;
}
void shaker_sort_with_temporary(int array[], int n)
{
	int m = n;
	int i, j, k;
	for (i = 0; i < n;)
	{
		for (j = i + 1; j < n; j++)
		{
			if (array[j] < array[j - 1])
				shaker_sort_swap_With_temporary(&array[j], &array[j - 1]);
		}

		m--;

		for (k = n - 1; k > i; k--)
		{
			if (array[k] < array[k - 1])
				shaker_sort_swap_With_temporary(&array[k], &array[k - 1]);
		}
		i++;
	}


}
// cортировка шейкером свапом c временным хранилищем

void shaker_sort_swap_without_temporary(int *xp, int *yp)
{
	*xp ^= *yp;
	*yp ^= *xp;
	*xp ^= *yp;
}
void shaker_sort_without_temporary(int array[], int n)
{
	int m = n;
	int i, j, k;
	for (i = 0; i < n;)
	{
		for (j = i + 1; j < n; j++)
		{
			if (array[j] < array[j - 1])
				shaker_sort_swap_without_temporary(&array[j], &array[j - 1]);
		}

		m--;

		for (k = n - 1; k > i; k--)
		{
			if (array[k] < array[k - 1])
				shaker_sort_swap_without_temporary(&array[k], &array[k - 1]);
		}
		i++;
	}

}
// сортировка шейкером свапом без временного хранилища

void sort_shaker_with_break(int array[], int n)
{
	int m = n;
	bool swapped = true;
	int i, j, k;

	while (swapped)
	{ 

		swapped = false;

		for (i = 0; i < n;)
		{
			for (j = i + 1; j < n; j++)
			{
				if (array[j] < array[j - 1])
					swap(array[j], array[j - 1]);
				swapped = true;
			}
			if (!swapped)
				break;

			swapped = false;

			m--;

			for (k = n - 1; k > i; k--)
			{
				if (array[k] < array[k - 1])
					swap(array[k], array[k - 1]);
				swapped = true;
			}
			i++;
		}
	}


}
// сортировка шейкером стандартным свапом с остановкой по готовности

void break_shaker_direct_without_temporary(int array[], int n)
{
	int m = n;
	bool swapped = true;
	int i, j, k;

	while (swapped)
	{

		swapped = false;

		for (i = 0; i < n;)
		{
			for (j = i + 1; j < n; j++)
			{
				if (array[j] < array[j - 1])
				{
					array[j] ^= array[j - 1];
					array[j - 1] ^= array[j];
					array[j] ^= array[j - 1];
					swapped = true;
				}
			}
			if (!swapped)
				break;

			swapped = false;

			m--;

			for (k = n - 1; k > i; k--)
			{
				if (array[k] < array[k - 1])
				{
					array[k] ^= array[k - 1];
					array[k - 1] ^= array[k];
					array[k] ^= array[k - 1];
					swapped = true;
				}
			}
			i++;
		}
	}

}
// сортировка шейкером с остановкой по готовности прямым встраиванием без временного хранилища

void break_shaker_direct_with_temporary(int array[], int n)
{
	int m = n;
	bool swapped = true;
	int i, j, k;

	while (swapped)
	{

		swapped = false;

		for (i = 0; i < n;)
		{
			for (j = i + 1; j < n; j++)
			{
				if (array[j] < array[j - 1])
				{
					int tmp = array[j];
					array[j] = array[j - 1];
					array[j - 1] = tmp;
					swapped = true;
				}
			}
			if (!swapped)
				break;

			swapped = false;

			m--;

			for (k = n - 1; k > i; k--)
			{
				if (array[k] < array[k - 1])
				{
					int tmp = array[k];
					array[k] = array[k - 1];
					array[k - 1] = tmp;
					swapped = true;
				}
			}
			i++;
		}
	}


}
// сортировка шейкером с остановкой по готовности прямым встраиванием с временным хранилищем

void break_shaker_swap_with_temporary(int *xp, int *yp)
{
	int temp = *xp;
	*xp = *yp;
	*yp = temp;
}
void break_shaker_with_temporary(int array[], int n)
{
	int m = n;
	bool swapped = true;
	int i, j, k;

	while (swapped)
	{

		swapped = false;

		for (i = 0; i < n;)
		{
			for (j = i + 1; j < n; j++)
			{
				if (array[j] < array[j - 1])
					break_shaker_swap_with_temporary(&array[j], &array[j - 1]);
				swapped = true;
			}
			if (!swapped)
				break;

			swapped = false;

			m--;

			for (k = n - 1; k > i; k--)
			{
				if (array[k] < array[k - 1])
					break_shaker_swap_with_temporary(&array[k], &array[k - 1]);
				swapped = true;
			}
			i++;
		}
	}


}
// сортировка шейкером с остановкой по готовности через свап с временным хранилищем

void  break_shaker_swap_without_temporary(int *xp, int *yp)
{
	*xp ^= *yp;
	*yp ^= *xp;
	*xp ^= *yp;
}
void break_shaker_without_temporary(int array[], int n)
{
	int m = n;
	bool swapped = true;
	int i, j, k;

	while (swapped)
	{

		swapped = false;

		for (i = 0; i < n;)
		{
			for (j = i + 1; j < n; j++)
			{
				if (array[j] < array[j - 1])
					break_shaker_swap_without_temporary(&array[j], &array[j - 1]);
				swapped = true;
			}
			if (!swapped)
				break;

			swapped = false;

			m--;

			for (k = n - 1; k > i; k--)
			{
				if (array[k] < array[k - 1])
					break_shaker_swap_without_temporary(&array[k], &array[k - 1]);
				swapped = true;
			}
			i++;
		}
	}


}
// сортировка шейкером с остановкой по готовности через свап без временного хранилища

int main()
{	
	setlocale(LC_ALL, "Russian");

	int massiv[5000] = {};
	int n = sizeof(massiv) / sizeof(massiv[0]);
	srand(time(NULL));
	for (int i = 0; i < n; i++)
	{
		massiv[i] = rand();
	}

	clock_t time;
	time = clock();
	standartswap(massiv, n);
	time = clock() - time;
	cout << "Коэффициент: " << (double)time / CLOCKS_PER_SEC << endl;

	clock_t time1;
	time1 = clock();
	swapwithdirectembaddingtemporarystorage(massiv, n);
	time1 = clock() - time1;
	cout << "Коэффициент: " << (double)time1 / CLOCKS_PER_SEC << endl;

	clock_t time2;
	time2 = clock();
	swapwithoutdirectembaddingtemporarystorage(massiv, n);
	time2 = clock() - time2;
	cout << "Коэффициент: " << (double)time2 / CLOCKS_PER_SEC << endl;

	clock_t time3;
	time3 = clock();
	swapwithtemporarystorage(massiv, n);
	time3 = clock() - time3;
	cout << "Коэффициент: " << (double)time3 / CLOCKS_PER_SEC << endl;

	clock_t time4;
	time4 = clock();
	swapwithouttemporarystorage(massiv, n);
	time4 = clock() - time4;
	cout << "Коэффициент: " << (double)time4 / CLOCKS_PER_SEC << endl;

	clock_t time5;
	time5 = clock();
	shaker_sort_with_standart_swap(massiv, n);
	time5 = clock() - time5;
	cout << "Коэффициент: " << (double)time5 / CLOCKS_PER_SEC << endl;

	clock_t time8;
	time8 = clock();
	shaker_sort_direct_with_temporary(massiv, n);
	time8 = clock() - time8;
	cout << "Коэффициент: " << (double)time8 / CLOCKS_PER_SEC << endl;

	clock_t time7;
	time7 = clock();
	shaker_sort_direct_without_temporary(massiv, n);
	time7 = clock() - time7;
	cout << "Коэффициент: " << (double)time7 / CLOCKS_PER_SEC << endl;

	clock_t time9;
	time9 = clock();
	shaker_sort_with_temporary(massiv, n);
	time9 = clock() - time9;
	cout << "Коэффициент: " << (double)time9 / CLOCKS_PER_SEC << endl;

	clock_t time10;
	time10 = clock();
	shaker_sort_without_temporary(massiv, n);
	time10 = clock() - time10;
	cout << "Коэффициент: " << (double)time10 / CLOCKS_PER_SEC << endl;

	clock_t time6;
	time6 = clock();
	sort_shaker_with_break(massiv, n);
	time6 = clock() - time6;
	cout << "Коэффициент: " << (double)time6 / CLOCKS_PER_SEC << endl;

	clock_t time11;
	time11 = clock();
	break_shaker_direct_with_temporary(massiv, n);
	time11 = clock() - time11;
	cout << "Коэффициент: " << (double)time11 / CLOCKS_PER_SEC << endl;

	clock_t time12;
	time12 = clock();
	break_shaker_direct_without_temporary(massiv, n);
	time12 = clock() - time12;
	cout << "Коэффициент: " << (double)time12 / CLOCKS_PER_SEC << endl;

	clock_t time13;
	time13 = clock();
	break_shaker_with_temporary(massiv, n);
	time13 = clock() - time13;
	cout << "Коэффициент: " << (double)time13 / CLOCKS_PER_SEC << endl;

	clock_t time14;
	time14 = clock();
	break_shaker_without_temporary(massiv, n);
	time14 = clock() - time14;
	cout << "Коэффициент: " << (double)time14 / CLOCKS_PER_SEC << endl;

	system("pause");
	return 0;
}
