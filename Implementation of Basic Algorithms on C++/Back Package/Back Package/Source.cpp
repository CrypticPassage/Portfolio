#include <iostream>
#include <vector>
#include <clocale>
using namespace std;

struct backPackItem
{
	char name[100];
	int cost;
	int mass;
};

int main()
{
	setlocale(LC_ALL, "Ru");
	const int counter = 5; //���������� ��������� � �������
	const int criticalMass = 15; //������������ ��� � �������

	cout << "���������� ���������: " << counter << endl;
	cout << "������������ ��� � �������: " << criticalMass << endl;
	cout << " " << endl;

								 //�������� ��������, ������� ����� ��������� ������.
	vector<backPackItem> backPackItems;
	for (int i = 0; i < counter; i++)
	{
		backPackItem newItem;
		cout << "������� �������� ��������: ";
		cin >> newItem.name;
		cout << "������� ����� ��������: ";
		cin >> newItem.mass;
		cout << "������� ��������� ��������: ";
		cin >> newItem.cost;
		cout << endl;
		backPackItems.push_back(newItem);
	}
	//��������� �������� �� ���������
	for (int i = counter - 1; i >= 0; i--)
	{
		int maxIndex = 0;

		for (int j = 0; j <= i; j++)
			if (backPackItems[j].cost > backPackItems[maxIndex].cost)
				maxIndex = j;

		if (maxIndex != i)
		{
			backPackItem temp = backPackItems[maxIndex];
			backPackItems[maxIndex] = backPackItems[i];
			backPackItems[i] = temp;
		}
	}



	//��������� ������
	vector<backPackItem> myBackPack;

	int massInBackPack = 0;
	int costInBackPack = 0;
	for (int i = counter - 1; i >= 0; i--)
	{
		if (massInBackPack + backPackItems[i].mass <= criticalMass)
		{
			massInBackPack += backPackItems[i].mass;
			costInBackPack += backPackItems[i].cost;
			myBackPack.push_back(backPackItems[i]);
		}
	}
	//������� ����������
	cout << "-----------------------------------------------------" << endl;
	cout << "� ������� ��������� " << myBackPack.size()
		<< " ���������(a), � ����� ������ � " << massInBackPack
		<< " ��. � ��������� ���������� � "
		<< costInBackPack << endl;
	cout << "-----------------------------------------------------" << endl;
	cout << "� ������� ��������� ��������: " << endl;
	for (int i = 0; i<myBackPack.size(); i++)
	{
		cout << myBackPack[i].name << "\t" << myBackPack[i].mass << "\t" << myBackPack[i].cost << endl;
	}

	system("pause");
	return 0;
}
