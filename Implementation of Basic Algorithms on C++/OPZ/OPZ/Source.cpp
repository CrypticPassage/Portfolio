#include <iostream>
#include <cctype>
#include <stdlib.h>
#include <conio.h>
#include <cstdio>
#include <stack>

using namespace std;

int prior(char v)
{
	switch (v)
	{
	case '(': return 1;
	case '+':
	case '-': return 2;
	case '*':
	case '/': return 3;
	}
}

bool is_op(char c)
{
	return c == '+' || c == '-' || c == '*' || c == '/';
}

bool is_digit(char c)
{
	return c >= '0' && c <= '9';
}

void OPN(char *a, char *out)
{
	stack <char> S;
	int i = 0, j = 0;
	char t;
	for (; a[i] != '\0'; ++i)
	{
		if (is_digit(a[i]))
		{
			out[j] = a[i];
			++j;
		}

		else
		{
			if (j != 0 && out[j - 1] >= '0' && out[j - 1] <= '9')
			{
				out[j] = '_';
				++j;
			}

			if (is_op(a[i]))
			{
				if (S.empty() || prior(S.top()) < prior(a[i]))
				{
					S.push(a[i]);
				}
				else
				{
					while (!S.empty() && (prior(S.top()) >= prior(a[i])))
					{
						out[j++] = S.top();
						S.pop();
					}
					S.push(a[i]);
				}
			}
			else
			{
				if (a[i] == '(')
				{
					S.push(a[i]);
				}
				else
				{
					if (a[i] == ')')
					{
						if (S.empty() || S.top() == '(')
						{
							cout << "Input error!";
							_getch();
							exit(1);
						}
						else
						{
							while (S.top() != '(')
							{
								out[j] = S.top();
								S.pop();
								j++;
							}
						}
						S.pop();
					}
				}
			}
		}
	}
	while (!S.empty())
	{
		if (S.top() == '(')
		{
			cout << "Input error!";
			_getch(); exit(1);
		}
		else
		{
			out[j] = S.top();
			S.pop();
			j++;
		}
	}
}

int Calc(char *out)
{
	int j = 0, c = 0, r1 = 0, r2 = 0;
	stack <double> S;
	stack <double> S1;
	char num[16];
	char* pEnd = nullptr;
	while (out[j] != '\0')
	{
		if (out[j] == '_') {
			++j;
			continue;
		}
		if (out[j] >= '0' && out[j] <= '9')
		{
			long iNum = strtol(&out[j], &pEnd, 10);
			S.push(iNum);
			j += pEnd - &out[j];
		}
		else
		{
			if (is_op(out[j]))
			{
				r1 = S.top(); S.pop();
				r2 = S.top(); S.pop();
				switch (out[j])
				{
				case '+': S.push(r2 + r1); break;
				case '-': S.push(r2 - r1); break;
				case '*': S.push(r2*r1); break;
				case '/': S.push(r2 / r1); break;
				}
			}
			++j;
		}
	}
	return (S.top());
}

int main()
{
	setlocale(0, "");
	char a[100] = { 0 };
	char out[100] = { 0 };
	stack <char> S;
	int i = 0;

	while (true)
	{
		cout << "Выражение: ";
		gets_s(a);
		OPN(a, out);
		cout << "ОПЗ: " << out << std::endl;
		cout << "Ответ: " << Calc(out) << "\n\n";
	}

	_getch();
	return 0;
}
