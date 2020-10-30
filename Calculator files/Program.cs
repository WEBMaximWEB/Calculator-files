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
            StreamReader f = new StreamReader("C:/Users/pmaxq/OneDrive/Desktop/TestF.txt");
            while (line == "" && counter < 20)
            {
                line += f.ReadLine();
                counter++;
            }
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
            Console.WriteLine("----->" + line);
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
                        Console.WriteLine("d1=" + digit1);
                        j++;
                    }
                    if (i >= 0)
                    {
                        j = i - 1;
                        while (j >= 0 && Char.IsDigit(text[j]))
                        {
                            //if (text[j] == '+' || text[j] == '-')
                              //  break;
                            digit2 += text[j];
                            Console.WriteLine("d2=" + digit2);
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
            return MainComputation(text, digit1, digit2, count);
        }

        static string MainComputation(string text, string digit1, string digit2, int i)
        {
            Console.WriteLine("попал в main");
            string str = "";
            if (text[i] == '*')
            {
                int num1 = Int32.Parse(digit1);
                int num2 = Int32.Parse(digit2);

                str = (num1 * num2).ToString();
            }
            else if (text[i] == '/')
            {
                int num1 = Int32.Parse(digit1), num2 = Int32.Parse(digit2);
                if (num2 == 0)
                {
                    Console.WriteLine("Нельзя делить на ноль!");
                    return "0";
                }
                str = (num2 / num1).ToString();
            }
            else if (text[i] == '+')
            {
                Console.WriteLine("Проверка на плюс");
                int num1 = Int32.Parse(digit1);
                int num2 = Int32.Parse(digit2);

                str = (num2 + num1).ToString();
            }
            else if (text[i] == '-')
            {
                Console.WriteLine("Проверка на минус");
                Console.WriteLine("Это digit1:" + digit1);
                int num1 = Int32.Parse(digit1);
                int num2 = Int32.Parse(digit2);

                str = (num2 - num1).ToString();
            }
            Console.WriteLine(text);
            int len = digit1.Length + digit2.Length + 1;
            int position = i - digit2.Length;
            Console.WriteLine("i=" + i + ", digit2L= " + digit2.Length + "textL=" + text.Length +
                "len = " + len +  " position = " + position + "digit2 =" + digit2 + 
                " digit1 = " + digit1 + "текст до удаления:" + text);
            text = text.Remove(position, len);
            Console.WriteLine("len ===" + len);
            Console.WriteLine("str + text= " + str + text + "на самом деле" + text.Insert(i - digit2.Length, str));
            return text.Insert(i - digit2.Length, str);
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
                        Console.WriteLine("*********--->" + str);
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
                        Console.WriteLine("до смола--->" + str);
                        str = Brackets(str, smallDictionary);
                        i = str.Length + 1;
                        flag = true;
                        Console.WriteLine("SmallDict*****->>>" + str);
                    }
                }
            }
            return str;
        }
    }
}
