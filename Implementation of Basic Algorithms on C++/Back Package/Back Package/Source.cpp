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
	const int counter = 5; //количество предметов в рюкзаке
	const int criticalMass = 15; //максимальный вес в рюкзаке

	cout << "Количество предметов: " << counter << endl;
	cout << "Максимальный вес в рюкзаке: " << criticalMass << endl;
	cout << " " << endl;

								 //выбираем элементы, которые могут наполнять рюкзак.
	vector<backPackItem> backPackItems;
	for (int i = 0; i < counter; i++)
	{
		backPackItem newItem;
		cout << "Введите название предмета: ";
		cin >> newItem.name;
		cout << "Введите массу предмета: ";
		cin >> newItem.mass;
		cout << "Введите стоимость предмета: ";
		cin >> newItem.cost;
		cout << endl;
		backPackItems.push_back(newItem);
	}
	//сортируем элементы по стоимости
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



	//наполняем рюкзак
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
	//выводим результаты
	cout << "-----------------------------------------------------" << endl;
	cout << "В рюкзаке находится " << myBackPack.size()
		<< " предметов(a), с общей массой в " << massInBackPack
		<< " кг. и суммарной стоимостью в "
		<< costInBackPack << endl;
	cout << "-----------------------------------------------------" << endl;
	cout << "В рюкзаке находятся предметы: " << endl;
	for (int i = 0; i<myBackPack.size(); i++)
	{
		cout << myBackPack[i].name << "\t" << myBackPack[i].mass << "\t" << myBackPack[i].cost << endl;
	}

	system("pause");
	return 0;
}
