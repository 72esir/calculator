using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UniversalCalculator
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Калькулятор для пятиклассника";

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("                             Доброго времени суток, дорогой пятиклассник!");
            Console.WriteLine();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Ты запустил приложение, которое объяснит тебе, как работает сложение, вычитание и умножение целых чисел в какой - то из систем счисления с основанием от 1 до 50 включительно.");
            Console.WriteLine("Так же ты сможешь перевести числа до 5000 в римскую систему счисления и любые числа в любую систему счисления.");
            Console.ResetColor();

            Preview();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Введи команду!");
            Console.ResetColor();

        Begin:
            try
            {
                Input();
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(ArgumentException))
                {
                    Console.WriteLine(ex.Message);
                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Обнаружена ошибка! Повторите ввод еще раз, скорее всего данные некорректны!");
                    Console.ResetColor();
                }
                goto Begin;
            }

        }

        private static void Preview()
        {
            Line();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                              Ты можешь выбрать одну из этих функций:");
            Console.ResetColor();

            LineTwo();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("                            | 1: трансформация чисел в разные системы счсления.");
            Console.WriteLine("                            | 2: превращени обычных (любой системы счисления) чисел в римские.");
            Console.WriteLine("                            | 3: превращение римских чисел в обычные (любой системы счисления).");
            Console.WriteLine("                            | 4: сложение.");
            Console.WriteLine("                            | 5: вычитание.");
            Console.WriteLine("                            | 6: умножение.");
            Console.WriteLine("                            | 7: вызвать список команд программы.");
            Console.ResetColor();
            Line();

        }

        private static void NewTry()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Хочешь еще раз? Нажми любую кнопку клавиатуры!");
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Доброго времени суток, дорогой пятиклассник!");
            Console.WriteLine();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Ты запустил приложение, которое объяснит тебе, как работает сложение, вычитание, умножение и деление целых чисел в какой - то из систем счисления с основанием от 1 до 50 включительно.");
            Console.WriteLine("Так же ты сможешь перевести числа до 5000 в римскую систему счисления.");
            Console.ResetColor();

            Preview();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Введите команду!");
            Console.ResetColor();
        }

        public static int Int(char c)
        {
            Dictionary<int, char> alphabet = new Dictionary<int, char>();
            for (int i = 0; i < 62; i++)
            {
                if (i >= 0 && i <= 9)
                    alphabet.Add(i, (char)('0' + i));
                if (i >= 10 && i <= 35)
                    alphabet.Add(i, (char)('A' + i - 10));
                if (i >= 36 && i <= 62)
                    alphabet.Add(i, (char)('a' + i - 36));
            }

            for (int i = 0; i < 62; i++)
            {
                if (alphabet[i] == c)
                {
                    return i;
                }
            }

            throw new ArgumentException("Число невозможно получить из остатка. Попробуйте еще раз в следующий раз!");

        }
        private static int Input()
        {
            while (true)
            {
                string input = Console.ReadLine();

                bool testInt = int.TryParse(input, out int numOfFunction);

                if (testInt && (numOfFunction >= 1 && numOfFunction < 8))
                {
                    switch (numOfFunction)
                    {
                        case 1:
                            TransformationNum();
                            NewTry();
                            break;
                        case 2:
                            IntoRom();
                            NewTry();
                            break;
                        case 3:
                            FromRom();
                            NewTry();
                            break;
                        case 4:
                            Sum();
                            NewTry();
                            break;
                        case 5:
                            Subtraction();
                            NewTry();
                            break;
                        case 6:
                            Multiplication();
                            NewTry();
                            break;
                        case 7:
                            Preview();
                            break;
                        default:
                            Console.WriteLine("Ошибка!");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Такой комманды не существует! Нажмите на любую клавишу клавиатуры, чтобы вернуться к списку комманд.");
                    Console.ReadKey();
                    Console.Clear();
                    Preview();
                    Input();
                }
            }
        }

        private static void LineTwo()
        {
            for (int i = 0; i < Console.WindowWidth / 3; i++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("─+─");
                Console.ResetColor();
            }
        }

        private static void Line()
        {
            for (int i = 0; i < Console.WindowWidth / 8; i++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("──<••>──");
                Console.ResetColor();
            }
        }

        private static char NumToSym(int modul)
        {
            if (modul >= 0 && modul <= 9) return (char)('0' + modul);
            if (modul >= 10 && modul <= 36) return (char)('A' + (modul - 10));
            if (modul >= 37 && modul <= 62) return (char)('a' + (modul - 36));
            else
                throw new ArgumentException("Некорректный остаток от деления!");
        }

        private static int AnyToDecOutside(string number, int numberBase)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (numberBase > 50)
                throw new ArgumentException("Ты выбрал основание, системы счисления которой этот калькулятор не умеет считать :( \nВыбери другое!");
            Console.ResetColor();
            int result = 0;
            int digitsCount = number.Length;
            int num;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Разбиваем число на отдельные символы.");
            var builder = new StringBuilder();
            builder.Append("Символы:");
            foreach (char c in number.ToCharArray())
            {
                builder.Append($" {c}");
            }
            Console.WriteLine(builder.ToString());

            Console.WriteLine("Теперь начинаем перевод в десятичную систему счисления.");
            Console.WriteLine("Изначально результат вычисления 0.");

            if (numberBase == 1)
            {
                Console.WriteLine("Чтобы число из 1-СС перевести в 10-СС нужно просто подсчитать, сколько 1 в этом числе. Полученное число и будет искомым число в 10-СС");
                int res = 0;
                char[] chars = number.ToCharArray();
                for (int i = 0; i < number.Length; i++)
                {
                    char lol = chars[i];
                    res += int.Parse(lol.ToString());
                }
                Console.WriteLine(res);

                Console.ForegroundColor = ConsoleColor.Red;
                if (res != number.Length)
                {
                    throw new ArgumentException("Некорректное число! Попробуйте еще раз");
                }
                Console.ResetColor();
                return res;

            }
            for (int i = 0; i < digitsCount; i++)
            {
                char symbol = number[i];
                Console.ForegroundColor = ConsoleColor.Red;
                if (symbol >= '0' && symbol <= '9') num = symbol - '0';

                else if (symbol >= 'A' && symbol <= 'Z') num = symbol - 'A' + 10;
                else if (symbol >= 'a' && symbol <= 'z') num = symbol - 'a' + (('Z' - 'A') + 1) + 10;

                else
                {
                    Console.Clear();
                    throw new ArgumentException("Некорректное число!");
                }
                if (num >= numberBase)
                {
                    string phrase = "Это число не существует в данной системем счисления. \nВведи '7', чтобы вернуться к списку функций и начни заново или выбери другую функцию!";
                    Console.Clear();
                    throw new ArgumentException(phrase);
                }
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("Умножаем результат на основание СС: "); Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(numberBase); Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow; Console.Write(", затем прибавляем число: "); Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(num); Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow; Console.Write(", соответствующее "); Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(i + 1); Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine(" элементу числа."); Console.ResetColor();

                result *= numberBase;
                result += num;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"({result / numberBase} * {numberBase}) + {num} = {result}");
                Console.ResetColor();
            }
            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("В ходе манипуляций получаем новое число:  "); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(result);
            Console.ResetColor();
            Console.WriteLine();
            return result;
        }

        private static string DecToAnyOutside(int number, int numberBase)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (numberBase > 50)
            {
                Console.Clear();
                throw new ArgumentException("Основание некорректо! Оно должно быть в пределе от 1 до 50 включительно!");
            }
            Console.ResetColor();
            StringBuilder builder = new StringBuilder();

            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write($"Теперь  переведем из 10-СС в "); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine(numberBase); Console.ResetColor();

            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow; Console.Write($"Делим с остатком "); Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(number); Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow; Console.Write(" на "); Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(numberBase); Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine(". При этом остаток приписываем к числу-результату. "); Console.ResetColor();
                int mod = number % numberBase;
                char symbol = NumToSym(mod);
                Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine($"{builder} + {mod}"); Console.ResetColor();
                builder.Append(symbol);
                number /= numberBase;

            } while (number >= numberBase);

            if (number != 0)
            {
                builder.Append(NumToSym(number));
                Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("Делим с остатком "); Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(number); Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine(" на 10. При этом остаток приписываем к числу-результату. "); Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("Получаем число "); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(builder.ToString()); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write(". Но это еще не результат. Чтобы получить корректное нужно его записать наоборот: "); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine(string.Join("", builder.ToString().Reverse())); Console.ResetColor();
            string result = string.Join("", builder.ToString().Reverse());

            return result;
        }

        private static int AnyToDec(string number, int numberBase)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (numberBase > 50)
                throw new ArgumentException("Основание некорректо! Оно должно быть в пределе от 1 до 50 включительно!");
            Console.ResetColor();
            int result = 0;
            int digitsCount = number.Length;
            int num;

            var builder = new StringBuilder();
            builder.Append("Символы:");
            foreach (char c in number.ToCharArray())
            {
                builder.Append($" {c}");
            }


            if (numberBase == 1)
            {
                int res = 0;
                for (int i = 0; i < number.Length; i++)
                    res++;
                Console.ForegroundColor = ConsoleColor.Red;
                if (res != number.Length) throw new ArgumentException("Некорректное число! Попробуйте еще раз");
                Console.ResetColor();
                return res;
            }
            for (int i = 0; i < digitsCount; i++)
            {
                char symbol = number[i];

                Console.ForegroundColor = ConsoleColor.Red;
                if (symbol >= '0' && symbol <= '9') num = symbol - '0';

                else if (symbol >= 'A' && symbol <= 'Z') num = symbol - 'A' + 10;
                else if (symbol >= 'a' && symbol <= 'z') num = symbol - 'a' + (('Z' - 'A') + 1) + 10;
                else throw new ArgumentException("Некорректное число!");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Red;
                if (num >= numberBase) throw new ArgumentException("Исходная строка имеет некорректные символы в обозначении чисел.");
                Console.ResetColor();
                result *= numberBase;
                result += num;
            }

            return result;

        }

        private static string DecToAny(int number, int numberBase)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (numberBase > 50)
                throw new ArgumentException("Основание некорректо! Оно должно быть в пределе от 1 до 50 включительно!");
            Console.ResetColor();
            StringBuilder builder = new StringBuilder();

            do
            {
                int mod = number % numberBase;
                char symbol = NumToSym(mod);

                builder.Append(symbol);
                number /= numberBase;

            } while (number >= numberBase);

            if (number != 0)
            {
                builder.Append(NumToSym(number));
            }

            string result = string.Join("", builder.ToString().Reverse());

            return result;
        }
        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        private static void TransformationNum()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("Введи число, которое хочешь преобразовать:"); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow; string originNumber = Console.ReadLine(); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("Введи систему счисления этого числа"); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow; int originNumberBase = int.Parse(Console.ReadLine()); Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            foreach (char digit in originNumber)
            {
                if ((int)Char.GetNumericValue(digit) >= originNumberBase) throw new ArgumentException("Ты выбрал число, которое не существует в выбранной системе счисленияю.");
            }
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("Введи систему счисления, в которую хочешь преобразовать число"); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow; int toWhatBase = int.Parse(Console.ReadLine()); Console.ResetColor();
            Console.Clear();

            int toDec = AnyToDecOutside(originNumber, originNumberBase);
            string x = DecToAnyOutside(toDec, toWhatBase);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Твое новое число ");
            Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(x); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write(" в системе счисления "); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine(toWhatBase);
            Console.ResetColor();
            Line();
            Console.WriteLine();
            Console.WriteLine();
            DrawHeart();
        }
        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        private static void IntoRom()
        {
            Console.Clear();
            int[] rim = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
            string[] arab = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("Введите число в диапазоне от 1 до 5000"); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow; string input = Console.ReadLine(); Console.ResetColor();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            if (!int.TryParse(input, out int number) || !(number >= 1 && number <= 5000)) throw new ArgumentException("Некорректное число! Введите число от 1 до 5000");
            Console.ResetColor();
            int i;
            i = 0;
            string output = "";
            var otigin = number;
            while (number > 0)
            {
                if (rim[i] <= number)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine($"{number} - {rim[i]} = {number - rim[i]}"); Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("Число  приписываем справа. И так до 0. "); Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(arab[i]); Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow; Console.Write(", соответствующее "); Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(rim[i]); Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine(" приписываем справа. И так до 0."); Console.ResetColor();

                    number = number - rim[i];
                    output = output + arab[i];
                }
                else i++;

            }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Получаем новое число {output} из исходного {otigin}");
            Console.ResetColor();
            Line();
            Console.WriteLine();
            Console.WriteLine();
            DrawHeart();
        }
        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        private static void FromRom()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("Введите число в римской СС"); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow; string input = Console.ReadLine(); Console.ResetColor();
            Console.Clear();
            int[] rim = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
            string[] arab = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

            for (int i = 0; i < input.Length; i++)
            {
                bool isCorrect = false;
                for (int j = 0; j < arab.Length; j++)
                {
                    if (input[i].ToString() == arab[j])
                    {
                        isCorrect = true;
                        break;
                    }
                }
                Console.ForegroundColor = ConsoleColor.Red;
                if (!isCorrect) throw new ArgumentException("Некорректное число!");
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write($"Разбиваем число "); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(input); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write(" на символы: "); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine(string.Join(" ", input.Split(""))); Console.ResetColor();

            int result = 0;
            var RomToArab = new Dictionary<char, int>
            {{ 'I', 1 },{ 'V', 5 },{ 'X', 10 },{ 'L', 50 },{ 'C', 100 },{ 'D', 500 },{ 'M', 1000 } };
            for (short i = 0; i < input.Length - 1; ++i)
            {
                if (RomToArab[input[i]] < RomToArab[input[i + 1]])
                {
                    Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("Число слева "); Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(RomToArab[input[i]]); Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow; Console.Write(" меньше числа справа "); Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(RomToArab[input[i + 1]]); Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow; Console.Write(" , поэтому вычитаем из результирующега числа левое "); Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine(RomToArab[input[i]]); Console.ResetColor();
                    result -= RomToArab[input[i]];
                }
                else if (RomToArab[input[i]] >= RomToArab[input[i + 1]])
                {
                    Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("Если число слева "); Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(RomToArab[input[i]]); Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow; Console.Write(" больше, чем число справа "); Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(RomToArab[input[i + 1]]); Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow; Console.Write(", то прибавляем к результирующему числу левое "); Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine(RomToArab[input[i]]); Console.ResetColor();
                    result += RomToArab[input[i]];
                }
                Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($"Получили текущее {result}"); Console.ResetColor();
            }
            result += RomToArab[input[^1]];
            Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine($"Получили текущее {result}");
            Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine($"Финальное число: {result}!");

            Line();
            Console.WriteLine();
            Console.WriteLine();
            DrawHeart();
        }
        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        private static void Sum()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("Введи систему счисления в которой хочешь произвести сложение: "); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow; string ss = Console.ReadLine(); Console.ResetColor();
            bool testInt = int.TryParse(ss, out int based);

            TestSystem(testInt, based);

            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("Введи первое слагаемое: "); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow; string number1 = Console.ReadLine(); Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("Введите второе слагаемое: "); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow; string number2 = Console.ReadLine(); Console.ResetColor();

            Console.Clear();

            int n1 = AnyToDec(number1, based);
            int n2 = AnyToDec(number2, based);

            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write($"Начем сложение в "); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(based); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("-СС"); Console.ResetColor();

            if (based == 1)
            {
                Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("Так как система счисления "); Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write("1"); Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine(", то результатом суммы будет общее количество единиц обоих чисел"); Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine($"Тогда результат: {number1 + number2}"); Console.ResetColor();
                return;
            }

            List<int> num1 = new List<int>();
            List<int> num2 = new List<int>();

            List<int> sumResult = new List<int>();

            int maxLen = Math.Max(number1.Length, number2.Length);

            number1 = number1.PadLeft(maxLen, '0');
            number2 = number2.PadLeft(maxLen, '0');



            foreach (var i in number1)
                num1.Add(Int(i));
            foreach (var j in number2)
                num2.Add(Int(j));

            num1.Reverse();
            num2.Reverse();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Запишем выражение 'столбиком'");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($" {number1}");
            Console.WriteLine($"+");
            Console.WriteLine($" {number2}");
            string border = "";
            Console.WriteLine($" {border.PadLeft(maxLen, '-')}");
            Console.ResetColor();

            Line();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Разряд числа — это место (позиция) цифры в записи данного числа. \nНапример, в числе '321' содержится три разряда, которые нумеруются справа налево: единицы (первый разряд), десятки (второй разряд), сотни (третий разряд). Разряды нумеруются по порядку: \nпервый, второй, третий, четвёртый. ");
            Console.WriteLine();
            Console.WriteLine("Складываем цифры из разрядов верхнего слагаемого с цифрами из разрядов нижнего. Такое сложение называется 'поразрядным'");
            Console.ResetColor();
            Line();


            int excess = 0;
            for (int i = 0; i < num1.Count; i++)
            {
                int result = num1[i] + num2[i] + excess;
                if (excess >= 1) excess -= 1;
                Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine($"{num1[i]} + {num2[i]} = {result} в [{i + 1}] разряде  "); Console.ResetColor();
                if (result >= based)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("В результате поразрядного сложения получилось число, равное основанию системы счисления, или больше. Значит нужно будет прибавить 1 к следующему разряду");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("Помимо этого записываем под "); Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(i + 1); Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow; Console.Write(" разрядом "); Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write($"{result} - {based} = {result - based}"); Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("- это значение мы получили в результате вычитания из этого числа число, соответствущее номеру системы счисления."); Console.ResetColor();

                    sumResult.Add(result - based);
                    excess += 1;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine($"Записываем под {i + 1} разрядом {result}"); Console.WriteLine(); Console.ResetColor();
                    sumResult.Add(result);
                }
            }

            sumResult.Reverse();

            StringBuilder sb = new StringBuilder();
            foreach (var item in sumResult)
            {
                sb.Append(item.ToString());
            }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($" {number1}");
            Console.WriteLine($"+");
            Console.WriteLine($" {number2}");

            Console.WriteLine($" {border.PadLeft(maxLen, '-')}");

            Console.WriteLine($" {sb.ToString()}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Результат: {sb.ToString()}");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
            DrawHeart();
        }

        private static void TestSystem(bool testInt, int based)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (!testInt || !(based >= 1 && based <= 50))
            {
                Console.Clear();
                throw new ArgumentException("Ты ввел систему счисления, с которой этот калькулятор не может работать :( \nВведи '7', чтобы вернуться к списку функций и начни сначала.");
                Console.ReadKey();
                Console.Clear();

                Preview();
            }
            Console.ResetColor();
        }

        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        private static void Subtraction()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("Введи систему счисления в которой хочешь произвести вычитание:"); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow; string ss = Console.ReadLine(); Console.ResetColor();

            bool testInt = int.TryParse(ss, out int based);
            TestSystem(testInt, based);

            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("Введи уменьшаемое:"); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow; string number1 = Console.ReadLine(); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("Введите вычитаемое:"); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow; string number2 = Console.ReadLine(); Console.ResetColor();
            Console.Clear();

            int numberCorrected = AnyToDec(number1, based);
            int vichitCorrected = AnyToDec(number2, based);

            List<int> numberList = new List<int>();
            List<int> vichitList = new List<int>();

            int maxLength = Math.Max(number1.Length, number2.Length);

            number2 = number2.PadLeft(maxLength, '0');
            number1 = number1.PadLeft(maxLength, '0');

            if (numberCorrected - vichitCorrected < 0)
            {

                Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("Число  меньше "); Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(number1); Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow; Console.Write(" меньше "); Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(number2); Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine(", поэтому разность будет отрицательной."); Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow; Console.Write($"Поэтому просто вычтем из "); Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(number2); Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow; Console.Write(" число "); Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(number1); Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine(" и добавим минус спереди."); Console.Clear();

                foreach (var i in number1)
                {
                    numberList.Add(Int(i));
                }
                foreach (var j in number2)
                {
                    vichitList.Add(Int(j));
                }
            }
            else
            {
                foreach (var i in number1)
                    numberList.Add(Int(i));
                foreach (var j in number2)
                    vichitList.Add(Int(j));

            }



            StringBuilder sb = new StringBuilder();

            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("Разряд числа — это место (позиция) цифры в записи данного числа. \nНапример, в числе '321' содержится три разряда, которые нумеруются справа налево: единицы (первый разряд), десятки (второй разряд), сотни (третий разряд). \nРазряды нумеруются по порядку: первый, второй, третий, четвёртый. "); Console.ResetColor();
            Console.WriteLine();
            if (numberCorrected - vichitCorrected >= 0)
            {
                for (int i = maxLength - 1; i >= 0; i--)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow; Console.Write($"Считаем разряд "); Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine(i + 1); Console.ResetColor();

                    char resSub = SubtractionProcess(based, numberList, vichitList, i);

                    Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("записываем под  разрядом результат:  "); Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(i + 1); Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow; Console.Write(" разрядом результат:  "); Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine(resSub); Console.ResetColor();
                    Console.WriteLine();
                    sb.Append(resSub);
                }
            }
            else
            {
                var temp = number1;
                number1 = number2;
                number2 = temp;

                var temp2 = numberList;
                numberList = vichitList;
                vichitList = temp2;


                for (int i = maxLength - 1; i >= 0; i--)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow; Console.Write($"Считаем разряд "); Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine(i + 1); Console.ResetColor();

                    char resSub = SubtractionProcess(based, numberList, vichitList, i);

                    Console.ForegroundColor = ConsoleColor.Yellow; Console.Write($"записываем под " + resSub); Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(i + 1); Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow; Console.Write(" разрядом результат:  "); Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine(resSub); Console.ResetColor();
                    Console.WriteLine();
                    sb.Append(resSub);


                }
            }

            Line();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            if (numberCorrected - vichitCorrected < 0)
            {
                Console.WriteLine();
                Console.WriteLine(" " + number2);
                Console.WriteLine("-");
                Console.WriteLine(" " + number1.PadLeft(maxLength, '0'));
            }

            else
            {
                Console.WriteLine();
                Console.WriteLine(" " + number1);
                Console.WriteLine("-");
                Console.WriteLine(" " + number2.PadLeft(maxLength, '0'));
            }

            for (int i = 0; i <= maxLength; i++)
            {
                Console.Write("-");

            }

            string answer;
            if (numberCorrected - vichitCorrected < 0)
            {

                answer = "-" + new string(sb.ToString().Reverse().ToArray());
                Console.WriteLine($"\n{answer}");
            }
            else
            {

                answer = new string(sb.ToString().Reverse().ToArray());
                Console.WriteLine($"\n {answer}");

            }
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Ответ: {answer}"); Console.ResetColor();
            Line();
            Console.WriteLine();
            Console.WriteLine();
            DrawHeart();
        }

        private static char SubtractionProcess(int based, List<int> numberList, List<int> vichitList, int i)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            if (numberList[i] < vichitList[i])
            {
                if (numberList[i] >= 0)
                {
                    Console.WriteLine($"{numberList[i]} меньше {vichitList[i]}, занимаем у левого разряда");
                    Console.WriteLine($"{numberList[i]} + {based} вычитаем {vichitList[i]} и получаем {numberList[i] + based - vichitList[i]}");
                }
                else
                {
                    Console.WriteLine("Так как этот разряд первого числа равен нулю(ранее из него занимали), то занимаем у левого разряда");
                    Console.WriteLine($"{based - 1} - {vichitList[i]} = {based - 1 - vichitList[i]}");
                }

                numberList[i - 1] = numberList[i - 1] - 1;
                numberList[i] += based;
            }
            else
            {
                Console.WriteLine($"Из {numberList[i]} - {vichitList[i]} = {numberList[i] - vichitList[i]}");
            }
            Console.ResetColor();
            char resSub = NumToSym(numberList[i] - vichitList[i]);
            return resSub;
        }
        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        private static void Multiplication()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("Введи номер, соответствующий основанию нужной системы счисления:"); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow; string ss = Console.ReadLine(); Console.ResetColor();

            bool testInt = int.TryParse(ss, out int based);
            TestSystem(testInt, based);

            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("Введи первый множитель:"); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow; string number1 = Console.ReadLine(); Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("Введи второй множитель:"); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow; string number2 = Console.ReadLine(); Console.ResetColor();

            Console.Clear();

            int n1 = AnyToDec(number1, based);
            int n2 = AnyToDec(number2, based);

            List<int> num2 = new List<int>();
            List<int> num1 = new List<int>();

            string temp;
            string space = "";
            object x = num2;

            List<int> multResultsInDec = new List<int>();
            List<string> multResultsInAny = new List<string>();
            foreach (var i in number2)
                num2.Add(Int(i));

            foreach (var i in number1)
                num1.Add(Int(i));

            for (int i = 0; i < Math.Abs(num1.Count - num2.Count); i++)
            {
                space += " ";
            }

            if (num2.Count > num1.Count)
            {
                temp = number1;
                number1 = number2;
                number2 = temp;

                Column(number1, number2, space);

                num1.Reverse();

                Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("Разряд числа — это место (позиция) цифры в записи данного числа. \nНапример, в числе '321' содержится три разряда, которые нумеруются справа налево: единицы (первый разряд), десятки (второй разряд), сотни (третий разряд). \nРазряды нумеруются по порядку: первый, второй, третий, четвёртый. "); Console.ResetColor();

                for (int i = 0; i < num1.Count; i++)
                {
                    int currentRazryad = AnyToDec(number1, based) * num2[i];
                    string displayedRazryad = DecToAny(currentRazryad, based);
                    Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine($"{number1} * {NumToSym(num1[i])} = {displayedRazryad}, где умножаем 1 число на число под [{i + 1}] разрядом."); Console.ResetColor();
                    multResultsInDec.Add(currentRazryad);
                    multResultsInAny.Add(displayedRazryad);
                }
            }

            else
            {
                Column(number1, number2, space);

                num2.Reverse();

                Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("Разряд числа — это место (позиция) цифры в записи данного числа. \nНапример, в числе '321' содержится три разряда, которые нумеруются справа налево: единицы (первый разряд), десятки (второй разряд), сотни (третий разряд). \nРазряды нумеруются по порядку: первый, второй, третий, четвёртый. "); Console.ResetColor();

                for (int i = 0; i < num2.Count; i++)
                {
                    int currentRazryad = AnyToDec(number1, based) * num2[i];
                    string displayedRazryad = DecToAny(currentRazryad, based);
                    Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine($"{number1} * {NumToSym(num2[i])} = {displayedRazryad}, где умножаем верхнее число на цифру под [{i + 1}] разрядом нижнего числа."); Console.ResetColor();
                    multResultsInDec.Add(currentRazryad);
                    multResultsInAny.Add(displayedRazryad);
                }
            }
            List<string> finalResults = new List<string>();
            finalResults.Add(multResultsInAny[0].PadLeft(multResultsInAny[0].Length + multResultsInAny.Count - 1, '0'));
            for (int i = 1; i < multResultsInAny.Count; i++)
            {

                var result = multResultsInAny[i].PadLeft(finalResults[0].Length - i, '0');
                result = result.PadRight(finalResults[0].Length, '0');

                finalResults.Add(result);

            }
            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("Получившиеся строки складываем поразрядно. Пример :"); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(number1.PadLeft(number1.Length + finalResults.Count, ' '));
            Console.WriteLine("*");
            Console.WriteLine(space + number2.PadLeft(number2.Length + finalResults.Count, ' '));
            Console.WriteLine(" " + "".PadLeft(finalResults[0].Length, '-'));
            foreach (var i in finalResults)
            {
                Console.WriteLine("+" + i);
            }

            Console.WriteLine(" " + "".PadLeft(finalResults[0].Length, '-'));

            Console.Write(" ");
            Console.ResetColor();
            string y = DecToAny(n1 * n2, based);
            Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine(y);
            Console.WriteLine();
            Console.WriteLine("Результат умножения равен:   " + y);
            Console.ResetColor();

            Line();
            Console.WriteLine();
            Console.WriteLine();
            DrawHeart();

        }

        private static void Column(string number1, string number2, string space)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Начинаем умножение");
            Console.WriteLine("Запишем выражение 'столбиком'. Если второе число оказалось больше первого, поменяем их местами  для удобства в записи.");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(" " + number1);
            Console.WriteLine("*");
            Console.WriteLine(" " + space + number2);
            Console.ResetColor();
        }
        static void DrawHeart()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("                                                    .....    ....");
            Console.WriteLine("                             надеюсь,              ................");
            Console.WriteLine("                                ты                 ................");
            Console.WriteLine("                               все                   .............");
            Console.WriteLine("                             понял!!!                  .........");
            Console.WriteLine("                                                        ......");
            Console.WriteLine("                                                         ...");
            Console.WriteLine("                                                          .");
        }
    }
}