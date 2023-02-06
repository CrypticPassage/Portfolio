#include <iostream>
#include <ctime>

using namespace std;

void standartswap(int array[], int n)
{
	int i;
	for (i = 0; i < n; i++)
	{
		for (int j(i + 1); j < n; j++)
		{
			if (array[i] < array[j])
			{
				swap(array[i], array[j]);
			}
		}
	}

	for (i = 0; i < n; i++)
	{
		cout << array[i] << " | ";
	}

	cout << " " << endl;

}

int main()
{
	setlocale(LC_ALL, "Russian");
	double sum;
	int size;
	cout << "Введите размер массива, который будет забит рандомными значениями: " << endl;
	cin >> size;
	int *m = new int[size];
	srand(time(NULL));
	cout << "Наш массив: " << endl;
	for (int i = 0; i < size; ++i)
	{
		m[i] = rand();
		cout << m[i] << " | ";
	}

	cout << " " << endl;

	cout << "Сортируем массив по убыванию: " << endl;
	standartswap(m, size);

	sum = 0;
	for (int i = 0; i < size; i++)
	{
		sum = sum + m[i];
	}

	cout << "Сумма равна " << sum << endl;

	system("pause");
	return 0;
}
