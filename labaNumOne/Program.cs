
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Solution
{
    class ProgramDima
    {
        static string SaveBuffer(List<float> numbers, string buffer)
        {
            if (!string.IsNullOrEmpty(buffer))
            {
                numbers.Add(float.Parse(buffer));
            }

            return string.Empty;
        }

        static float CalculateExpression(List<float> numbers, List<char> operators)
        {
            while (operators.Count > 0)
            {
                int operatorIndex = -1;
                if (operators.Contains('*') || operators.Contains('/'))
                {
                    int multiplyIndex = operators.IndexOf('*');
                    int devideIndex = operators.IndexOf('/');

                    operatorIndex = devideIndex != -1 && multiplyIndex != -1
                        ? multiplyIndex > devideIndex
                            ? devideIndex
                            : multiplyIndex
                        : devideIndex == -1
                            ? multiplyIndex
                            : devideIndex;
                }

                if (operatorIndex == -1)
                {
                    operatorIndex = 0;
                }

                char op = operators[operatorIndex];

                float num1 = numbers[operatorIndex];
                float num2 = numbers[operatorIndex + 1];

                float result = Calculate(op, num1, num2);

                numbers.RemoveAt(operatorIndex + 1);
                numbers.RemoveAt(operatorIndex);
                operators.RemoveAt(operatorIndex);

                numbers.Insert(operatorIndex, result);
            }

            return numbers[0];
        }

        static float Calculate(char op, float num1, float num2)
        {
            switch (op)
            {
                case '+': return num1 + num2;
                case '-': return num1 - num2;
                case '*': return num1 * num2;
                case '/': return num1 / num2;
            }

            throw new Exception("Unknown operation");
            //Console.WriteLine("ERROR");
            //return 0;
        }

        static void Main(string[] args)
        {
            // 1 +12 *     32 / 2
            // int
            // + - * /

            // 1 12 32 2
            // + * /

            string input = "12,1 +    5 *11 / 4,2";
            input = input.Replace(" ", string.Empty);

            char[] availableOps = new[] { '+', '-', '*', '/' };

            List<float> numbers = new List<float>();
            List<char> operators = new List<char>();
            string buffer = string.Empty;
            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsDigit(input[i])
                    || input[i].ToString() == CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
                {
                    buffer += input[i];
                }
                else
                {
                    operators.Add(input[i]);
                    buffer = SaveBuffer(numbers, buffer);
                }
            }

            SaveBuffer(numbers, buffer);

            // output
            foreach (int number in numbers)
            {
                Console.Write($"{number} ");
            }
            Console.WriteLine();

            foreach (char op in operators)
            {
                Console.Write($"{op} ");
            }
            Console.WriteLine();

            float result = CalculateExpression(numbers, operators);
            Console.WriteLine(result);
        }
    }
}