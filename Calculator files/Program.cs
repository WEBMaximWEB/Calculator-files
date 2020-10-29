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
                    line = line.Insert(i, Brackets(text));
                }
            }
            Console.WriteLine("----->" + line);
        }

        static string Brackets(string text)
        {
            char[] bigDictionary = { '*', '/' };
            string str = "", digit1 = "", digit2 = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (bigDictionary.Contains(text[i]))
                {
                    int j = i + 1;
                    while (j < text.Length && Char.IsDigit(text[j]))
                    {
                        digit1 += text[j];
                        j++;
                    }
                    if (i > 0)
                    {
                        j = i - 1;
                        while (j >= 0 && Char.IsDigit(text[j]))
                        {
                            digit2 += text[j];
                            j--;
                        }
                        string s = "";
                        for (int k = digit2.Length - 1; k >= 0; k--)
                            s += digit2[k];
                        digit2 = s;
                    }
                }
                if (text[i] == '*')
                {
                    int num1 = Int32.Parse(digit1);
                    int num2 = Int32.Parse(digit2);

                    text = text.Remove(i, 1);
                    str = (num1 * num2).ToString();
                    Console.WriteLine(digit2);
                    Console.WriteLine("->" + text);
                }
                else if (text[i] == '/')
                {
                    int num1 = Int32.Parse(digit1), num2 = Int32.Parse(digit2);
                    
                    text = text.Remove(i, 1);
                    if (num2 == 0)
                    {
                        Console.WriteLine("Нельзя делить на ноль!");
                        return "0";
                    }
                    str = (num1 / num2).ToString();
                }
                text = text.Remove(i - digit2.Length, digit1.Length + digit2.Length + 1);
            }
            Console.WriteLine("===" + str);
            return str + "(" +text + ")";
        }
    }
}
