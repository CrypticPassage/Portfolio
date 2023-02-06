#include<iostream>

using namespace std;
struct node // создание узла двусв€зного списка, в котором наход€тс€ данные и указали на следующий и предыдущий элементы
{
	int info;
	struct node *next;
	struct node *prev;
}*start;

class double_llist // создание самого списка в виде класса, в котором можно выполн€ть нижеприведЄнные функции
{
public:
	void create_list(int value);
	void add_begin(int value);
	void add_after(int value, int position);
	void delete_element(int value);
	void search_element(int value);
	void display_dlist();
	double_llist()
	{
		start = NULL; // список пустой, его начало = NULL
	}
};

int main()
{
	int choice, element, position;
	double_llist dl;
	while (1)
	{                                                          //главное меню выбора действий
		cout << "Operations on Doubly linked list:" << endl; 
		cout << "1.Create Node" << endl;
		cout << "2.Add at begining" << endl;
		cout << "3.Add after position" << endl;
		cout << "4.Delete" << endl;
		cout << "5.Display" << endl;
		cout << "6.Quit" << endl;
		cout << "Enter your choice : ";
		cin >> choice;
		switch (choice)
		{
		case 1:
			cout << "Enter the element: ";
			cin >> element;
			dl.create_list(element);
			cout << endl;
			break;
		case 2:
			cout << "Enter the element: ";
			cin >> element;
			dl.add_begin(element);
			cout << endl;
			break;
		case 3:
			cout << "Enter the element: ";
			cin >> element;
			cout << "Insert Element after position: ";
			cin >> position;
			dl.add_after(element, position);
			cout << endl;
			break;
		case 4:
			if (start == NULL)
			{
				cout << "List empty,nothing to delete" << endl;
				break;
			}
			cout << "Enter the element for deletion: ";
			cin >> element;
			dl.delete_element(element);
			cout << endl;
			break;
		case 5:
			dl.display_dlist();
			cout << endl;
			break;
		case 6:
			cout << "Goodbye!" << endl;
			exit(1);
		}
	}
	return 0;
}

void double_llist::create_list(int value) // функци€ создани€ листа, поочерЄдно добавл€€ элементы
{
	struct node *s, *temp;
	temp = new(struct node);
	temp->info = value;
	temp->next = NULL; 
	if (start == NULL) // если список пустой, то мы добавл€ем первый элемент в список и мен€ем указатели, позади нас теперь NULL, а start - это наш первый элемент 
	{
		temp->prev = NULL;
		start = temp;
	}
	else // если список не пустой, то мы добавл€ем второй элемент после первого 
	{
		s = start;
		while (s->next != NULL)
			s = s->next;
		s->next = temp;
		temp->prev = s;
	}
}

void double_llist::add_begin(int value)
{
	if (start == NULL) // проверка, есть ли хот€ бы один узел?
	{
		cout << "First Create the list." << endl;
		return;
	}
	struct node *temp; // создание нового узла, выставление ссылок
	temp = new(struct node);
	temp->prev = NULL;
	temp->info = value;
	temp->next = start;
	start->prev = temp;
	start = temp;
	cout << "Element Inserted" << endl;
}

void double_llist::add_after(int value, int pos)
{
	if (start == NULL) // проверка, есть ли хот€ бы один узел?
	{
		cout << "First Create the list." << endl; 
		return;
	}
	struct node *tmp, *q;
	int i;
	q = start;
	for (i = 0; i < pos - 1; i++)
	{
		q = q->next;
		if (q == NULL) // проверка, есть ли элемент в списке
		{
			cout << "There are less than ";
			cout << pos << " elements." << endl;
			return;
		}
	}
	tmp = new(struct node); // создание нового узла
	tmp->info = value;
	if (q->next == NULL) // если после элемента идЄт NULL, то выставл€ем указатели дл€ последующего элемента
	{
		q->next = tmp;
		tmp->next = NULL;
		tmp->prev = q;
	}
	else // если после элемента не идЄт NULL, то тоже выставл€ем соответсвующие указатели дл€ элемента
	{
		tmp->next = q->next;
		tmp->next->prev = tmp;
		q->next = tmp;
		tmp->prev = q;
	}
	cout << "Element Inserted" << endl;
}

void double_llist::delete_element(int value)
{
	struct node *tmp, *q;
	//удаление первого элемента
	if (start->info == value)
	{
		tmp = start;
		start = start->next;
		start->prev = NULL;
		cout << "Element Deleted" << endl;
		free(tmp);
		return;
	}
	q = start;
	while (q->next->next != NULL)
	{
		//удаление элемента между
		if (q->next->info == value)
		{
			tmp = q->next;
			q->next = tmp->next;
			tmp->next->prev = q;
			cout << "Element Deleted" << endl;
			free(tmp);
			return;
		}
		q = q->next;
	}
	// удаление последнего элемента
	if (q->next->info == value)
	{
		tmp = q->next;
		free(tmp);
		q->next = NULL;
		cout << "Element Deleted" << endl;
		return;
	}
	cout << "Element " << value << " not found" << endl;
}

void double_llist::display_dlist()
{
	struct node *q;
	if (start == NULL) //// проверка, есть ли хот€ бы один узел?
	{
		cout << "List empty,nothing to display" << endl;
		return;
	}
	q = start;
	cout << "The Doubly Link List is :" << endl;
	while (q != NULL)
	{
		cout << q->info << " <-> ";
		q = q->next;
	}
	cout << "NULL" << endl;
}
