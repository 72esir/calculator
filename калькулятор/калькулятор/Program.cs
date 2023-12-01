using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;

namespace UniversalCalculator
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Calculator";

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Доброго времени суток, дорогой пятиклассник!");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Ты запустил приложение, которое объяснит тебе, как работает сложение, вычитание и умножение целых чисел в какой - то из систем счисления с основанием от 1 до 50 включительно.");
            Console.WriteLine("Так же ты сможешь перевести числа до 5000 в римскую систему счисления и любые числа в любую систему счисления.");
            Console.ResetColor();

            GetHelp();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Введи команду!");
            Console.ResetColor();

        Begin:
            try
            {
                GetInput();              
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

        private static void GetHelp()
        {
            CreateBorder();
            Console.ForegroundColor= ConsoleColor.DarkCyan;  
            Console.WriteLine("Тыможешь выбрать одну из этих функций:");
            Console.ResetColor(); 
            
            CreateBorderTwo();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Функция 1: трансформация чисел в разные системы счсления.");
            Console.WriteLine("Функция 2: превращение обычных (любой системы счисления) чисел в римские.");
            Console.WriteLine("Функция 3: превращение римских чисел в обычные (любой системы счисления).");
            Console.WriteLine("Функция 4: сложение.");
            Console.WriteLine("Функция 5: вычитание");
            Console.WriteLine("Функция 6: умножение.");
            Console.WriteLine("Функция 7: вызвать список команд программы.");
            Console.ResetColor();
            CreateBorder();

        }

        private static void AnotherOne()
        {
            Console.WriteLine("Хочешь еще раз? Нажми любую кнопку клавиатуры!");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Доброго времени суток, дорогой пятиклассник!");
            Console.WriteLine("Ты запустил приложение, которое объяснит тебе, как работает сложение, вычитание, умножение и деление целых чисел в какой - то из систем счисления с основанием от 1 до 50 включительно.");
            Console.WriteLine("Так же ты сможешь перевести числа до 5000 в римскую систему счисления.");
            CreateBorder();
            GetHelp();
            Console.WriteLine("Введите команду!");
        }

        public static int GetInt(char c)
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
        private static int GetInput()
        {
            while (true)
            {
                string input = Console.ReadLine();
                
                bool testInt = int.TryParse(input, out int numOfFunction);
                
                if (testInt && (numOfFunction >=1 && numOfFunction < 8))
                {
                    switch (numOfFunction)
                    {
                        case 1:
                            FirstFunction();
                            AnotherOne();
                            break;
                        case 2:
                            SecondFunction();
                            AnotherOne();
                            break;
                        case 3:
                            ThirdFunction();
                            AnotherOne();
                            break;
                        case 4:
                            FourthFunction();
                            AnotherOne();
                            break;
                        case 5:
                            FifthFunction();
                            AnotherOne();
                            break;
                        case 6:
                            SixthFunction();
                            AnotherOne();
                            break;
                        case 7:
                            GetHelp();
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
                    GetHelp();
                    GetInput();
                }
            }
        }

        private static void CreateBorderTwo()
        {
            for (int i = 0; i < Console.WindowWidth / 3; i++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("─+─");
                Console.ResetColor();
            }
        }

        private static void CreateBorder()
        {
            for (int i = 0; i < Console.WindowWidth / 8; i++)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("──<••>──");
                Console.ResetColor();
            }
        }

        private static char ConvertNumberToSymbol(int modul)
        {
            if (modul >= 0 && modul <= 9) return (char)('0' + modul);
            if (modul >= 10 && modul <= 36) return (char)('A' + (modul - 10));
            if (modul >= 37 && modul <= 62) return (char)('a' + (modul - 36));

            throw new ArgumentException("Некорректный остаток от деления!");
        }

        private static int ConvertFromAnyToDec(string number, int numberBase)
        {
            Console.ForegroundColor= ConsoleColor.Yellow;
            if (numberBase > 50)
                throw new ArgumentException("Ты выбрал основание, системы счисления которой этот калькулятор не умеет считать :( \n выбери другое!");
            int result = 0;
            int digitsCount = number.Length;
            int num;

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
                if (res != number.Length) throw new ArgumentException("Некорректное число! Попробуйте еще раз");
                return res;
            }
            for (int i = 0; i < digitsCount; i++)
            {
                char symbol = number[i];

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

        private static string ConvertFromDecToAny(int number, int numberBase)
        {

            if (numberBase > 50)
            {
                Console.Clear();
                throw new ArgumentException("Основание некорректо! Оно должно быть в пределе от 1 до 50 включительно!");
            }
                StringBuilder builder = new StringBuilder();

            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write($"Теперь  переведем из 10-СС в "); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(number); Console.ResetColor();
            Console.Write(number); Console.ResetColor();
            Console.Write(" переведем из 10-СС в "); Console.ResetColor();
            Console.WriteLine(numberBase); Console.ResetColor();
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow; Console.Write($"Делим с остатком "); Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(number); Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow; Console.Write(" на "); Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(numberBase); Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine(". При этом остаток приписываем к числу-результату. "); Console.ResetColor();
                int mod = number % numberBase;
                char symbol = ConvertNumberToSymbol(mod);
                Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine($"{builder} + {mod}"); Console.ResetColor();
                builder.Append(symbol);
                number /= numberBase;

            } while (number >= numberBase);

            if (number != 0)
            {
                builder.Append(ConvertNumberToSymbol(number));
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

        private static int ConvertFromAnyToDecWithoutComments(string number, int numberBase)
        {
            if (numberBase > 50)
                throw new ArgumentException("Основание некорректо! Оно должно быть в пределе от 1 до 50 включительно!");
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
                if (res != number.Length) throw new ArgumentException("Некорректное число! Попробуйте еще раз");
                return res;
            }
            for (int i = 0; i < digitsCount; i++)
            {
                char symbol = number[i];

                if (symbol >= '0' && symbol <= '9') num = symbol - '0';

                else if (symbol >= 'A' && symbol <= 'Z') num = symbol - 'A' + 10;
                else if (symbol >= 'a' && symbol <= 'z') num = symbol - 'a' + (('Z' - 'A') + 1) + 10;
                else throw new ArgumentException("Некорректное число!");

                if (num >= numberBase) throw new ArgumentException("Исходная строка имеет некорректные символы в обозначении чисел.");
                result *= numberBase;
                result += num;
            }

            return result;

        }

        private static string ConvertFromDecToAnyWithoutComments(int number, int numberBase)
        {
            if (numberBase > 50)
                throw new ArgumentException("Основание некорректо! Оно должно быть в пределе от 1 до 50 включительно!");
            StringBuilder builder = new StringBuilder();

            do
            {
                int mod = number % numberBase;
                char symbol = ConvertNumberToSymbol(mod);

                builder.Append(symbol);
                number /= numberBase;

            } while (number >= numberBase);

            if (number != 0)
            {
                builder.Append(ConvertNumberToSymbol(number));
            }

            string result = string.Join("", builder.ToString().Reverse());

            return result;
        }
//xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        private static void FirstFunction()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("Введи число, которое хочешь преобразовать:"); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow; string originNumber = Console.ReadLine(); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("Введи систему счисления этого числа"); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow; int originNumberBase = int.Parse(Console.ReadLine()); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow; Console.WriteLine("Введи систему счисления, в которую хочешь преобразовать число"); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow; int toWhatBase = int.Parse(Console.ReadLine()); Console.ResetColor();
            Console.Clear();

            int toDec = ConvertFromAnyToDec(originNumber, originNumberBase);
            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("Твое новое число "); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow; Console.Write(ConvertFromDecToAny(toDec, toWhatBase)); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow; Console.Write(" в системе счисления "); Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow; Console.WriteLine(toWhatBase); Console.ResetColor();
            CreateBorder();
        }
//xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        private static void SecondFunction()
        {
            Console.Clear();
            int[] rim = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
            string[] arab = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

            Console.WriteLine("Введите число в диапазоне от 1 до 5000");
            string input = Console.ReadLine();
            Console.Clear();
            if (!int.TryParse(input, out int number) || !(number >= 1 && number <= 5000)) throw new ArgumentException("Некорректное число! Введите число от 1 до 5000");
            int i;
            i = 0;
            string output = "";
            var otigin = number;
            while (number > 0)
            {
                if (rim[i] <= number)
                {
                    Console.WriteLine($"{number} - {rim[i]} = {number - rim[i]}");
                    Console.WriteLine($"Число {arab[i]}, соответствующее {rim[i]} приписываем справа. И так до 0.");
                    number = number - rim[i];
                    output = output + arab[i];
                }
                else i++;

            }
            Console.WriteLine($"Получаем новое число {output} из исходного {otigin}");
            CreateBorder();
        }
//xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        private static void ThirdFunction()
        {
            Console.Clear();
            Console.WriteLine("Введите число в римской СС");
            string input = Console.ReadLine();
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

                if (!isCorrect) throw new ArgumentException("Некорректное число!");
            }

            Console.WriteLine($"Разбиваем число {input} на символы: {string.Join(" ", input.Split(""))}");

            int result = 0;
            var RomToArab = new Dictionary<char, int>
            {{ 'I', 1 },{ 'V', 5 },{ 'X', 10 },{ 'L', 50 },{ 'C', 100 },{ 'D', 500 },{ 'M', 1000 } };
            for (short i = 0; i < input.Length - 1; ++i)
            {
                if (RomToArab[input[i]] < RomToArab[input[i + 1]])
                {
                    Console.WriteLine($"Число слева {RomToArab[input[i]]} меньше числа справа {RomToArab[input[i + 1]]} , поэтому вычитаем из результирующега числа левое {RomToArab[input[i]]}");
                    result -= RomToArab[input[i]];
                }
                else if (RomToArab[input[i]] >= RomToArab[input[i + 1]])
                {
                    Console.WriteLine($"Число слева {RomToArab[input[i]]} больше, чем число справа {RomToArab[input[i + 1]]}, то прибавляем к результирующему числу левое {RomToArab[input[i]]}");
                    result += RomToArab[input[i]];
                }
                Console.WriteLine($"Получили текущее {result}");
            }
            result += RomToArab[input[^1]];
            Console.WriteLine($"Получили текущее {result}");
            Console.WriteLine($"Финальное число: {result}!");

            CreateBorder();
        }
//xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        private static void FourthFunction()
        {
            Console.Clear();
            Console.WriteLine("Введи систему счисления в которой хочешь произвести сложение: ");
            string ss = Console.ReadLine();
            bool testInt = int.TryParse(ss, out int based);

            TestSystem(testInt, based);

            Console.WriteLine("Введи первое слагаемое: ");
            string number1 = Console.ReadLine();

            Console.WriteLine("Введите второе слагаемое: ");
            string number2 = Console.ReadLine();

            Console.Clear();

            int n1 = ConvertFromAnyToDecWithoutComments(number1, based);
            int n2 = ConvertFromAnyToDecWithoutComments(number2, based);

            Console.WriteLine($"Начем сложение в {based}-СС");

            if (based == 1)
            {
                Console.WriteLine("Так как система счисления 1, то результатом суммы будет общее количество единиц обоих чисел");
                Console.WriteLine($"Тогда результат: {number1 + number2}");
                return;
            }

            List<int> num1 = new List<int>();
            List<int> num2 = new List<int>();

            List<int> sumResult = new List<int>();

            int maxLen = Math.Max(number1.Length, number2.Length);

            number1 = number1.PadLeft(maxLen, '0');
            number2 = number2.PadLeft(maxLen, '0');



            foreach (var i in number1)
                num1.Add(GetInt(i));
            foreach (var j in number2)
                num2.Add(GetInt(j));

            num1.Reverse();
            num2.Reverse();

            Console.WriteLine("Запишем выражение 'столбиком'");

            Console.WriteLine($" {number1}");
            Console.WriteLine($"+");
            Console.WriteLine($" {number2}");
            string border = "";
            Console.WriteLine($" {border.PadLeft(maxLen, '-')}");

            Console.WriteLine("Разряд числа — это место (позиция) цифры в записи данного числа. \nНапример, в числе '321' содержится три разряда, которые нумеруются справа налево: единицы (первый разряд), десятки (второй разряд), сотни (третий разряд). Разряды нумеруются по порядку: первый, второй, третий, четвёртый. ");

            Console.WriteLine("Складываем цифры из разрядов верхнего слагаемого с цифрами из разрядов нижнего. Такое сложение называется 'поразрядным'");

            int excess = 0;
            for (int i = 0; i < num1.Count; i++)
            {
                int result = num1[i] + num2[i] + excess;
                if (excess >= 1) excess -= 1;
                Console.WriteLine($"{num1[i]} + {num2[i]} = {result} в [{i + 1}] разряде  ");
                if (result >= based)
                {
                    Console.WriteLine("В результате поразрядного сложения получилось число, равное основанию системы счисления, или больше. Значит нужно будет прибавить 1 к следующему разряду");
                    Console.WriteLine($"Помимо этого записываем под {i + 1} разрядом {result} - {based} = {result - based} - это значение мы получили в результате вычитания из этого числа число, соответствущее номеру системы счисления.");
                    sumResult.Add(result - based);
                    excess += 1;
                }
                else
                {
                    Console.WriteLine($"Записываем под {i + 1} разрядом {result}");
                    sumResult.Add(result);
                }
            }

            sumResult.Reverse();

            StringBuilder sb = new StringBuilder();
            foreach (var item in sumResult)
            {
                sb.Append(item.ToString());
            }
            Console.WriteLine($" {number1}");
            Console.WriteLine($"+");
            Console.WriteLine($" {number2}");

            Console.WriteLine($" {border.PadLeft(maxLen, '-')}");

            Console.WriteLine($" {sb.ToString()}");

            Console.WriteLine($"Результат: {sb.ToString()}");
        }

        private static void TestSystem(bool testInt, int based)
        {
            if (!testInt || !(based >= 1 && based <= 50))
            {
                Console.Clear();
                throw new ArgumentException("Ты ввел систему счисления, с которой этот калькулятор не может работать :( \nВведи '7', чтобы вернуться к списку функций и начни сначала.");
                Console.ReadKey();
                Console.Clear();

                GetHelp();
            }
        }

        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        private static void FifthFunction()
        {
            Console.Clear();
            Console.WriteLine("Введи систему счисления в которой хочешь произвести вычитание:");
            string ss = Console.ReadLine();

            bool testInt = int.TryParse(ss, out int based);
            TestSystem(testInt, based);

            Console.WriteLine("Введи уменьшаемое:");
            string number1 = Console.ReadLine();
            Console.WriteLine("Введите вычитаемое:");
            string number2 = Console.ReadLine();
            Console.Clear();

            int numberCorrected = ConvertFromAnyToDecWithoutComments(number1, based);
            int vichitCorrected = ConvertFromAnyToDecWithoutComments(number2, based);
            
            List<int> numberList = new List<int>();
            List<int> vichitList = new List<int>();

            int maxLength = Math.Max(number1.Length, number2.Length);

            number2 = number2.PadLeft(maxLength, '0');
            number1 = number1.PadLeft(maxLength, '0');

            if (numberCorrected - vichitCorrected < 0)
            {

                Console.WriteLine($"Число {number1} меньше {number2}, поэтому разность будет отрицательной.");
                Console.WriteLine($"Поэтому просто вычтем из {number2} число {number1} и добавим минус спереди.");

                foreach (var i in number1)
                {
                    numberList.Add(GetInt(i));
                }
                foreach (var j in number2)
                {
                    vichitList.Add(GetInt(j));
                }
            }
            else
            {
                foreach (var i in number1)
                    numberList.Add(GetInt(i));
                foreach (var j in number2)
                    vichitList.Add(GetInt(j));

            }



            StringBuilder sb = new StringBuilder();

            Console.WriteLine("Разряд числа — это место (позиция) цифры в записи данного числа. \nНапример, в числе '321' содержится три разряда, которые нумеруются справа налево: единицы (первый разряд), десятки (второй разряд), сотни (третий разряд). \nРазряды нумеруются по порядку: первый, второй, третий, четвёртый. ");
            Console.WriteLine();
            if (numberCorrected - vichitCorrected >= 0)
            {
                for (int i = maxLength - 1; i >= 0; i--)
                {
                    Console.WriteLine($"Считаем разряд {i + 1}");

                    char resSub = SubtractionProcess(based, numberList, vichitList, i);

                    Console.WriteLine($"записываем под {i + 1} разрядом результат:  " + resSub);
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
                    Console.WriteLine($"Считаем разряд {i + 1}");

                    char resSub = SubtractionProcess(based, numberList, vichitList, i);

                    Console.WriteLine($"записываем под {i + 1} разрядом результат:  " + resSub);
                    Console.WriteLine();
                    sb.Append(resSub);


                }
            }

            CreateBorder();
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

            Console.WriteLine($"Ответ: {answer}");
            CreateBorder();
        }

        private static char SubtractionProcess(int based, List<int> numberList, List<int> vichitList, int i)
        {
            if (numberList[i] < vichitList[i])
            {
                if (numberList[i] >= 0)
                {
                    Console.WriteLine($"{numberList[i]} меньше {vichitList[i]}, занимаем у левого разряда");
                    Console.WriteLine($" {numberList[i]} + {based} вычитаем {vichitList[i]} и получаем {numberList[i] + based - vichitList[i]}");
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
            char resSub = ConvertNumberToSymbol(numberList[i] - vichitList[i]);
            return resSub;
        }
//xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        private static void SixthFunction()
        {
            Console.Clear();
            Console.WriteLine("Введи номер, соответствующий основанию нужной системы счисления:");
            string ss = Console.ReadLine();
            bool testInt = int.TryParse(ss, out int based);

            TestSystem(testInt, based);

            Console.WriteLine("Введи первый множитель:");
            string number1 = Console.ReadLine();

            Console.WriteLine("Введи второй множитель:");
            string number2 = Console.ReadLine();

            Console.Clear();

            int n1 = ConvertFromAnyToDecWithoutComments(number1, based);
            int n2 = ConvertFromAnyToDecWithoutComments(number2, based);

            List<int> num2 = new List<int>();
            List<int> num1 = new List<int>();

            string temp;
            string space = "";
            object x = num2;

            List<int> multResultsInDec = new List<int>();
            List<string> multResultsInAny = new List<string>();
            foreach (var i in number2)
                num2.Add(GetInt(i));

            foreach (var i in number1)
                num1.Add(GetInt(i));
                
            for(int i = 0; i < Math.Abs(num1.Count - num2.Count); i++)
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

                Console.WriteLine("Разряд числа — это место (позиция) цифры в записи данного числа. \nНапример, в числе '321' содержится три разряда, которые нумеруются справа налево: единицы (первый разряд), десятки (второй разряд), сотни (третий разряд). \nРазряды нумеруются по порядку: первый, второй, третий, четвёртый. ");

                for (int i = 0; i < num1.Count; i++)
                {
                    int currentRazryad = ConvertFromAnyToDecWithoutComments(number1, based) * num2[i];
                    string displayedRazryad = ConvertFromDecToAnyWithoutComments(currentRazryad, based);
                    Console.WriteLine($"{number1} * {ConvertNumberToSymbol(num1[i])} = {displayedRazryad}, где умножаем 1 число на число под [{i + 1}] разрядом.");
                    multResultsInDec.Add(currentRazryad);
                    multResultsInAny.Add(displayedRazryad);
                }
            }

            else
            {
                Column(number1, number2, space);

                num2.Reverse();

                Console.WriteLine("Разряд числа — это место (позиция) цифры в записи данного числа. \nНапример, в числе '321' содержится три разряда, которые нумеруются справа налево: единицы (первый разряд), десятки (второй разряд), сотни (третий разряд). \nРазряды нумеруются по порядку: первый, второй, третий, четвёртый. ");
               
                for (int i = 0; i < num2.Count; i++)
                {
                    int currentRazryad = ConvertFromAnyToDecWithoutComments(number1, based) * num2[i];
                    string displayedRazryad = ConvertFromDecToAnyWithoutComments(currentRazryad, based);
                    Console.WriteLine($"{number1} * {ConvertNumberToSymbol(num2[i])} = {displayedRazryad}, где умножаем верхнее число на цифру под [{i + 1}] разрядом нижнего числа.");
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
            Console.WriteLine("Получившиеся строки складываем поразрядно. Пример :");
            Console.WriteLine(number1.PadLeft(number1.Length + finalResults.Count, ' '));
            Console.WriteLine("*");
            Console.WriteLine( space + number2.PadLeft(number2.Length + finalResults.Count, ' '));
            Console.WriteLine(" " + "".PadLeft(finalResults[0].Length, '-'));
            foreach (var i in finalResults)
            {
                Console.WriteLine("+" + i);
            }

            Console.WriteLine(" " + "".PadLeft(finalResults[0].Length, '-'));

            Console.WriteLine(" " + ConvertFromDecToAnyWithoutComments(n1 * n2, based));



        }

        private static void Column(string number1, string number2, string space)
        {
            Console.WriteLine("Начинаем умножение");
            Console.WriteLine("Запишем выражение 'столбиком'. Если второе число оказалось больше первого, поменяем их местами  для удобства в записи.");
            Console.WriteLine(" " + number1);
            Console.WriteLine("*");
            Console.WriteLine(" " + space + number2);
        }
    }
}