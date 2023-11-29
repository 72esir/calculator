using System;
using System.Collections.Generic;
using System.Linq;

namespace lab4
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string input = Console.ReadLine();
            input = input.Replace(" ", string.Empty);

            List<object> elems = elemsCreation(input);
            List<object> rpn = Transformation(elems);

            long result = CalculateRPN(rpn);
            //вывод ОПЗ
            for (int i = 0; i < rpn.Count; i++)
            {
                Console.Write(rpn[i] + " ");
            }

            Console.WriteLine();
            Console.WriteLine("Результат выражения равен: " + result);
        }

        static string SaveBuffer(List<object> tokens, string buffer)
        {
            if (!string.IsNullOrEmpty(buffer))
            {
                tokens.Add(int.Parse(buffer));
            }

            return string.Empty;
        }

        static List<object> elemsCreation(string input)
        {
            List<object> sybbols = new List<object>();
            string buffer = string.Empty;

            foreach (char e in input)
            {
                bool testChar = char.IsDigit(e);
                
                if (testChar)
                {
                    buffer += e;
                }
                else
                {
                    buffer = SaveBuffer(sybbols, buffer);
                    sybbols.Add(e);
                }
            }


            SaveBuffer(sybbols, buffer);

            return sybbols;
        }

        static List<object> Transformation(List<object> tokens)
        {
            List<object> result = new List<object>();
            Stack<object> stk = new Stack<object>();
            foreach (var token in tokens)
            {
                if (char.IsDigit(token.ToString()[0]))
                {
                    result.Add(token);
                }
                else if (stk.Count != 0 && IsOperation(token.ToString()[0]))
                {
                    object lastOperation = stk.Peek();
                    if (GetOperatonPriority(lastOperation) < GetOperatonPriority(token))
                    {
                        stk.Push(token);
                        continue;
                    }
                    else
                    {
                        result.Add(stk.Pop());
                        stk.Push(token);
                        continue;
                    }
                }
                else if (stk.Count() == 0)
                {
                    stk.Push(token);
                    continue;
                }

                if (token.ToString()[0] == '(')
                {
                    stk.Push(token);
                    continue;
                }
                else if (token.ToString()[0] == ')')
                {
                    while (stk.Peek().ToString()[0] != '(')
                    {
                        result.Add(stk.Pop());
                    }

                    stk.Pop();
                }
            }

            while (stk.Count() != 0)
            {
                result.Add(stk.Pop());
            }

            static int GetOperatonPriority(object operation)
            {
                switch (operation)
                {
                    case '+': return 1;
                    case '-': return 1;
                    case '*': return 2;
                    case '/': return 2;
                    default: return 0;
                }
            }

            static bool IsOperation(char c)
            {
                if (c == '+' || c == '-' || c == '*' || c == '/')
                    return true;
                else
                    return false;
            }

            return result;
        }

        static int CalculateRPN(List<object> rpnString)
        {
            Stack<int> operationStack = new Stack<int>();
            
            foreach (var token in rpnString)
            {

                if (char.IsDigit(token.ToString()[0]))
                {
                    operationStack.Push(Convert.ToInt32(token));
                    
                }
                else
                {
                    char operation = token.ToString()[0];
                    int num2 = operationStack.Pop();
                    int num1 = operationStack.Pop();
                    operationStack.Push(UseOperation(operation, num1, num2));
                    
                }

            }

            static int UseOperation(char operation, int op1, int op2)
            {
                switch (operation)
                {
                    case '+': return (op1 + op2);
                    case '-': return (op1 - op2);
                    case '*': return (op1 * op2);
                    case '/': return (op1 / op2);
                    default: return 0;
                }
            }

            static bool IsOperation(char c)
            {
                if (c == '+' ||
                    c == '-' ||
                    c == '*' ||
                    c == '/')
                    return true;
                else
                    return false;
            }

            return operationStack.Pop();
        }
    }
}