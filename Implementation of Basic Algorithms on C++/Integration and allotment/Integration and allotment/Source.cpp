#include <iostream>
#include <conio.h>
#include <time.h>

using namespace std;

#define maxn 100

int a[maxn];

void merge(int l, int r) 
{
	if (r == l)
		return;
	if (r - l == 1) 
	{
		if (a[r] < a[l])
			swap(a[r], a[l]);
		return;
	}
	int m = (r + l) / 2;
	merge(l, m);
	merge(m + 1, r);
	int buf[maxn];
	int xl = l;
	int xr = m + 1;
	int cur = 0;
	while (r - l + 1 != cur) 
	{
		if (xl > m)
			buf[cur++] = a[xr++];
		else if (xr > r)
			buf[cur++] = a[xl++];
		else if (a[xl] > a[xr])
			buf[cur++] = a[xr++];
		else buf[cur++] = a[xl++];

	}
	for (int i = 0; i < cur; i++)
		a[i + l] = buf[i];
}
// алгоритм слияния

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

int main() 
{
	int choice;
	int massiv[15] = {};
	int n = sizeof(massiv) / sizeof(massiv[0]);
	srand(time(NULL));
	cout << "Our massive is: ";
	for (int i = 0; i < n; i++)
	{
		massiv[i] = rand();
		cout << massiv[i] << " ";
	}
	cout << " " << endl;

	while (1)
	{
		cout << "What do you want to do?" << endl;
		cout << "1 - Integration algorithm" << endl;
		cout << "2 - Radix algorithm" << endl;
		cin >> choice;
		switch (choice)
		{
		case 1:	
			merge(0, n - 1);
		case 2:
			radixsort(massiv, n);
			for (int i = 0; i < n; i++)
			{
				cout << massiv[i] << " ";
			}
			cout << " " << endl;
		}
	}
	return 0;
}
