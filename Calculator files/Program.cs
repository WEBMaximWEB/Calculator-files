using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Calculator_files
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = "";
            char[] dictionary = {'+', '-', '*', '/', '(', ')'};
            int counter = 0;

            // Пример:  1 +( 34 * 4 - 8/ 4+  3  )* 2   4 
            StreamReader f = new StreamReader(
                                       "C:/Users/pmaxq/OneDrive/Desktop/input.txt");
            while (line == "" && counter < 20)
            {
                line += f.ReadLine();
                counter++;
            }
            f.Close();

            //далее работа со стракой
            line = line.Replace(" ", "");
            Console.WriteLine(line);

            for (int i = 0; i < line.Length; i++)
            {
                if (!dictionary.Contains(line[i]) && !Char.IsDigit(line[i]))
                {
                    Console.WriteLine("Недопустимая операция!");
                    break;
                }
            }

            //работа со скобками
            string text = "";
            for (int i = line.Length - 1; i >= 0; i--)
            {
                if (line[i] == '(')
                {
                    line  = line.Remove(i, 1);
                    int j = i;
                    while (line[j] != ')' )
                    {
                        text += line[j];
                        j++;
                    }
                    line = line.Remove(j - text.Length, text.Length + 1);
                    line = line.Insert(i, Check(text));
                }
            }
            line = Check(line);
            Console.WriteLine("Готово.");

            StreamWriter sw = new StreamWriter("C:/Users/pmaxq/OneDrive/Desktop/output.txt");
            sw.WriteLine(line);
            sw.Close();

        }

        static string Brackets(string text, char[] xDictionary)
        {
            string digit1 = "", digit2 = "";
            int count = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (xDictionary.Contains(text[i]))
                {
                    int j = i + 1;
                    while (j < text.Length && Char.IsDigit(text[j]))
                    {
                        digit1 += text[j];
                        j++;
                    }
                    if (i >= 0)
                    {
                        j = i - 1;
                        while (j >= 0 && Char.IsDigit(text[j]))
                        {
                            digit2 += text[j];
                            j--;
                        }
                    }
                    count = i;
                    break;
                }
            }
            string s = "";
            for (int k = digit2.Length - 1; k >= 0; k--)
                s += digit2[k];
            digit2 = s;
            int num1 = Int32.Parse(digit1);
            int num2;
            if (digit2 != "")
                num2 = Int32.Parse(digit2);
            else
                num2 = 0;
            return MainComputation(text, num1, num2, count);
        }

        static string MainComputation(string text, int num1, int num2, int i)
        {
            string str = "";
            if (text[i] == '*')
            {
                str = (num1 * num2).ToString();
            }
            else if (text[i] == '/')
            {
                if (num2 == 0)
                {
                    Console.WriteLine("Нельзя делить на ноль!");
                    return "0";
                }
                str = (num2 / num1).ToString();
            }
            else if (text[i] == '+')
            {
                str = (num2 + num1).ToString();
            }
            else if (text[i] == '-')
            {
                if (num2 != 0)
                {
                    str = (num2 - num1).ToString();
                }
                else
                    str = "-" + num1.ToString();

            }
            int len = num1.ToString().Length + num2.ToString().Length + 1;
            int position = i - num2.ToString().Length;
            text = text.Remove(position, len);
            return text.Insert(i - num2.ToString().Length, str);
        }

        static string Check(string str)
        {
            char[] bigDictionary = { '*', '/' };
            char[] smallDictionary = { '+', '-' };
            bool flag = true;
            
            // поиск умножения и деления
            while (flag)
            {
                flag = false;
                for (int i = 0; i < str.Length; i++)
                {
                    if (bigDictionary.Contains(str[i]))
                    {
                        str = Brackets(str, bigDictionary);
                        i = str.Length + 1;
                        flag = true;
                    }
                }
            }
            // поиск сложения и вычитания
            flag = true;
            while (flag)
            {
                flag = false;
                for (int i = 0; i < str.Length; i++)
                {
                    if (smallDictionary.Contains(str[i]))
                    {
                        str = Brackets(str, smallDictionary);
                        i = str.Length + 1;
                        flag = true;
                    }
                }
            }
            return str;
        }
    }
}
