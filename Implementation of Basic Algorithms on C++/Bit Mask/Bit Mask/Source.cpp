#include <iostream>
#include <chrono>
#include<list>

const int NumOfBits = 16;

using namespace std;

void bit(int);

int main()
{
	setlocale(LC_ALL, "Russian");

	while (1)
	{
		int n;
		int n1, n2, n3, n4, n5;
		cout << "Программа отщепления цифры от числа двумя способами! Введите пятизначное число: " << endl;
		cin >> n;
		int m = n;
		if (n > 99999 || n < 10000)
		{
			cout << "Число не входит в диапазон!" << endl;
		}

		else
		{
			cout << "Число в двоичной системе: " << endl;
			bit(n);
			cout << " " << endl;
			cout << " " << endl;
			cout << "Десятичное отщепление цифры: " << endl;
			auto start = std::chrono::system_clock::now();
			n1 = n / 10000;
			n2 = (n - n1 * 10000) / 1000;
			n3 = (n - n1 * 10000 - n2 * 1000) / 100;
			n4 = (n - n1 * 10000 - n2 * 1000 - n3 * 100) / 10;
			n5 = (n - n1 * 10000 - n2 * 1000 - n3 * 100 - n4 * 10);
			auto end = std::chrono::system_clock::now();
			auto elapsed = end - start;
			cout << n1 << "_" << n2 << "_" << n3 << "_" << n4 << "_" << n5 << endl;
			cout << "Время выполнения: " << elapsed.count() << endl;

			cout << " " << endl;
			cout << " " << endl;

			const unsigned int lowMask = 0xFF;
			cout << "Применение сдвига и битовой маски: " << endl;
			auto start1 = std::chrono::system_clock::now();
			n &= lowMask;
			m = m >> 8;
			auto end1 = std::chrono::system_clock::now();
			auto elapsed1 = end1 - start1;
			cout << "Выделение младших разрядов: " << endl;
			bit(n);
			cout << " " << endl;
			cout << "Число, которое получается из младших разрядов: " << n << endl;
			cout << "Выделение старших разрядов: " << endl;
			bit(m);
			cout << " " << endl;
			cout << "Число, которое получается из старших разрядов: " << m << endl;
			cout << "Время выполнения: " << elapsed1.count() << endl;
			cout << " " << endl;
		}
	}
		system("pause");
		return 0;
	
}

void bit(int number)
{
	int i = 1;
	list<int> bitsList;
	if (number)
	{
		for (; number != 0; number /= 2)
		{
			int bit = number % 2;
			bitsList.push_front(bit);
		}
	}
	else
		bitsList.push_front(0);

	if (bitsList.size() < NumOfBits)
	{
		for (int i = NumOfBits - bitsList.size(); i != 0; i--)
			bitsList.push_front(0);
	}
	for (list<int>::const_iterator ci = bitsList.begin(); ci != bitsList.end(); ci++, i++)
	{
		if (!(i % 4))
			cout << *ci << " ";
		else
			cout << *ci;
	}
}
