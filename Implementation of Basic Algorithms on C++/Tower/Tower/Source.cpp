#include <iostream>
#include <math.h>
using namespace std;

void hanoi(int ring_count, char first_tower, char b, char c)
{
	if (ring_count == 1)
	{
		cout << "\nПеренос кольца " << ring_count << " с башни " << first_tower << " к башне " << b;
	}
	else
	{
		hanoi(ring_count - 1, first_tower, c, b);
		cout << "\nПеренос кольца " << ring_count << " с башни " << first_tower << " к башне " << b;
		hanoi(ring_count - 1, c, b, first_tower);
	}
}
int main() 
{
	setlocale(LC_ALL, "rus");
	int ring_count;
	cout << "Кол-во колец:\n";
	cin >> ring_count;
	int i = pow(2, ring_count) - 1;
	cout << ring_count << " колец = " << i << " опер." << '\n';
	hanoi(ring_count, 'A', 'C', 'B');
	cout << '\n';
	system("pause");
	return 0;
}