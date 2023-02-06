#include <iostream>

using namespace std;

int nod(int a, int b) 
{
	return (a == 0) ? b : nod(b % a, a);
}

int nok(int t, int q) 
{
	return q * t / nod(t, q);
}


int main() 
{

	setlocale(LC_ALL, "Russian");

	int choice;
	int n, m = 0;
	int o = 0;
	int p = 1;

		cout << "Программа нахождения НОД и НОК! Что вы хотите сделать? " << endl;
		cout << "1 - Найти НОД; 2 - Найти НОК" << endl;
		cin >> choice;
		if (choice == 1)
		{
			cout << "Введите количество чисел, для которых надо найти НОД: " << endl;
			cin >> n;
			cout << "Вводим числа: " << endl;
			for (int i = 0; i < n; i++)
			{
				int a;
				cin >> a;
				m = nod(m, a);
			}

			cout << "Нод = " << m << endl;
		}

		else if (choice == 2)
		{
			cout << "Введите количество чисел, для которых надо найти НОК: " << endl;
			cin >> o;
			cout << "Вводим числа: " << endl;
			for (int i = 0; i < o; i++)
			{
				int b;
				cin >> b;
				p = nok(p, b);
			}

			cout << "НОК = " << p << endl;
		}


		system("pause");
		return 0;
}