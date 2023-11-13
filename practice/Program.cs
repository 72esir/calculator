using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice
{
    internal class Program
    {
        static void Main()
        {
            Console.Write("введите число:   ");

            string num;
            bool testFloat, testInt;
            float doteNum;
            int integer;
            Parsing(out num, out testFloat, out doteNum, out testInt, out integer);

            List<double> preNumbers = new List<double>();
            preNumbers.Add(0.0);

            if (testInt)
            {
                char symbol = Convert.ToChar(integer);
                Console.Write("данное число соответствует символу:  ");
                Console.WriteLine(symbol);
                Environment.Exit(0);
            }

            else if (num == "q")
            {
                Console.WriteLine("программа остановлена");
                Environment.Exit(0);
            }
            else if (testFloat)
            {
                int count = preNumbers.Count;
                while (preNumbers[count - 1] != doteNum)
                {
                    preNumbers.Add(doteNum);
                    Console.WriteLine("предидущее число   " + doteNum);
                    
                    Parsing(out num, out testFloat, out doteNum, out testInt, out integer);

                    if (testInt)
                    {
                        Console.WriteLine("данное число соответствует символу:  " + Convert.ToChar(integer));
                        Environment.Exit(0);
                    }

                    else if (preNumbers[count - 1] != doteNum)
                    {
                        Console.WriteLine("программа остановлена, найдено число, равное предыдущему");
                        Environment.Exit(0);
                    }
                } 
            }   
        }

        private static void Parsing(out string num, out bool testFloat, out float doteNum, out bool testInt, out int integer)
        {
            num = Console.ReadLine();
            testFloat = float.TryParse(num, out doteNum);
            testInt = int.TryParse(num, out integer);
        }
    }
}