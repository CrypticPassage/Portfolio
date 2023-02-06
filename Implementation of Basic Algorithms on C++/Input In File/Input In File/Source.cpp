#include <iostream>
#include <fstream>
#include <ctime>

using namespace std;

int main()
{
	setlocale(LC_ALL, "Russian");
	fstream f;
	f.open("D://Massiv.txt", fstream::in | fstream::out | std::ofstream::trunc);
	int size;
	cout << "¬ведите размер массива, который будет забит рандомными значени€ми: " << endl;
	cin >> size;
	int *m = new int[size];
	srand(time(NULL));
	cout << "Ќаш массив: " << endl;
	for (int i = 0; i < size; ++i)
	{
		m[i] = rand();
		cout << m[i] << " | ";
		f << m[i] << " | ";
	}

	cout << " " << endl;

	f.close();
	system("pause");
	return 0;
}
