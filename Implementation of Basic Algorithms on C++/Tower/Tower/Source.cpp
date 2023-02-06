#include <iostream>
#include <math.h>
using namespace std;

void hanoi(int ring_count, char first_tower, char b, char c)
{
	if (ring_count == 1)
	{
		cout << "\n������� ������ " << ring_count << " � ����� " << first_tower << " � ����� " << b;
	}
	else
	{
		hanoi(ring_count - 1, first_tower, c, b);
		cout << "\n������� ������ " << ring_count << " � ����� " << first_tower << " � ����� " << b;
		hanoi(ring_count - 1, c, b, first_tower);
	}
}
int main() 
{
	setlocale(LC_ALL, "rus");
	int ring_count;
	cout << "���-�� �����:\n";
	cin >> ring_count;
	int i = pow(2, ring_count) - 1;
	cout << ring_count << " ����� = " << i << " ����." << '\n';
	hanoi(ring_count, 'A', 'C', 'B');
	cout << '\n';
	system("pause");
	return 0;
}