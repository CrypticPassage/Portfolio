#include <iostream>
#include <iomanip>
#include <ctime>
#include <chrono>
using namespace std;

int linSearch(int arr[], int requiredKey, int size);
int Search_Binary(int arr[], int left, int right, int key);

int main()
{
	setlocale(LC_ALL, "rus");

	const int n = 5000;
	int massiv[n] = {};
	srand(time(NULL));
	cout << "��� ������: ";
	for (int i = 0; i < n; i++)
	{
		massiv[i] = i + 1;
		cout << massiv[i] << " | ";
	}
	cout << " " << endl;

	int nelement = 0;
	int required_key = 0;
	cout << "������������� ������ ���� ����������. ����� ����� ���������� ������?" << endl;
	cin >> required_key;
	cout << "�������� ����������������� ������ ��� ����� ������: " << endl;

	auto start1 = std::chrono::system_clock::now();
	nelement = linSearch(massiv, required_key, n);
	auto end1 = std::chrono::system_clock::now();
	auto elapsed1 = end1 - start1;

	if (nelement != -1)
	{
		cout << "�������� " << required_key << " ��������� � ������ � ��������: " << nelement << endl;
	}
	else
	{
		cout << "� ������� ��� ������ ��������" << endl;
	}

	cout << "����������� ���������� ����������������� ������: " << elapsed1.count() << endl;

	cout << "�������� ��������� ������ ��� ����� ������: " << endl;

	auto start = std::chrono::system_clock::now();
	nelement = Search_Binary(massiv, 0, n, required_key);
	auto end = std::chrono::system_clock::now();
	auto elapsed = end - start;

	if (nelement >= 0)
	{
		cout << "�������� " << required_key << " ��������� � ������ � ��������: " << nelement << endl;
	}

	else
	{
		cout << "� ������� ��� ������ ��������" << endl;
	}

	
	cout << "����������� ���������� ��������� ������: " << elapsed.count() << endl;

	system("pause");
	return 0;
}

int linSearch(int arr[], int requiredKey, int arrSize)
{
	for (int i = 0; i < arrSize; i++)
	{
		if (arr[i] == requiredKey)
			return i;
	}
	return -1;
}

int Search_Binary(int arr[], int left, int right, int key)
{
	int midd = 0;
	while (1)
	{
		midd = (left + right) / 2;

		if (key < arr[midd])
			right = midd - 1;
		else if (key > arr[midd])
			left = midd + 1;
		else
			return midd;

		if (left > right)
			return -1;
	}
}
