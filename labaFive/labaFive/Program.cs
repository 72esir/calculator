using System;
using System.Collections.Generic;
using System.Text;

public enum TokenType
{
    Number,
    Operator,
    LeftStaple,
    RightStaple
}

public class Token
{
    public TokenType Type;
    public double? Value;
    public char? Operator;
}

public class RpnCalculator
{
    public static List<Token> ConvertToRpn(List<Token> infixTokens)
    {
        List<Token> result = new List<Token>();
        Stack<Token> stack = new Stack<Token>();

        foreach (var token in infixTokens)
        {
            if (token.Type == TokenType.Number)
            {
                result.Add(token);
            }
            else if (token.Type == TokenType.Operator)
            {
                while (stack.Count > 0 && ShouldPopOperator(stack.Peek(), token))
                {
                    result.Add(stack.Pop());
                }
                stack.Push(token);
            }
            else if (token.Type == TokenType.LeftStaple)
            {
                stack.Push(token);
            }
            else if (token.Type == TokenType.RightStaple)
            {
                while (stack.Count > 0 && stack.Peek().Type != TokenType.LeftStaple)
                {
                    result.Add(stack.Pop());
                }

                if (stack.Count == 0 || stack.Peek().Type != TokenType.LeftStaple)
                {
                    Console.WriteLine("Ошибка: Несогласованные скобки");
                    return new List<Token>();
                }

                stack.Pop(); 
            }
        }

        while (stack.Count > 0)
        {
            if (stack.Peek().Type == TokenType.LeftStaple || stack.Peek().Type == TokenType.RightStaple)
            {
                Console.WriteLine("Ошибка: Несогласованные скобки");
                return new List<Token>();
            }
            result.Add(stack.Pop());
        }

        return result;
    }

    public static double CalculateRpn(List<Token> rpnTokens)
    {
        Stack<double> operandStack = new Stack<double>();

        foreach (var token in rpnTokens)
        {
            if (token.Type == TokenType.Number)
            {
                operandStack.Push(token.Value ?? throw new InvalidOperationException("Ошибка: Неправильный формат числа"));
            }
            else if (token.Type == TokenType.Operator)
            {
                if (operandStack.Count < 2)
                {
                    Console.WriteLine("Ошибка: Недостаточно операндов для операции");
                    return 0;
                }

                double num2 = operandStack.Pop();
                double num1 = operandStack.Pop();
                operandStack.Push(ApplyOperation(token.Operator ?? default, num1, num2));
            }
        }

        return operandStack.Count == 1 ? operandStack.Pop() : HandleError("Ошибка: Некорректное выражение");
    }

    private static double ApplyOperation(char operation, double op1, double op2)
    {
        return operation switch
        {
            '+' => op1 + op2,
            '-' => op1 - op2,
            '*' => op1 * op2,
            '/' => op1 / op2,
            _ => 0,
        };
    }

    private static bool ShouldPopOperator(Token op1, Token op2)
    {   
        return op1.Type == TokenType.Operator;
    }

    private static double HandleError(string message)
    {
        Console.WriteLine(message);
        return 0;
    }
}

class Program
{
    static void Main()
    {
        
        string inputExpression = Console.ReadLine().Replace(" ", "");
       
        List<Token> infixExpression = TokenizeExpression(inputExpression);
      
        var rpnExpression = RpnCalculator.ConvertToRpn(infixExpression);
     
        double result = RpnCalculator.CalculateRpn(rpnExpression);

        Console.WriteLine("Результат: " + result);
    }

    private static List<Token> TokenizeExpression(string inputExpression)
    {
        List<Token> tokens = new List<Token>();
        StringBuilder currentNumber = new StringBuilder();

        foreach (char c in inputExpression)
        {
            if (char.IsDigit(c) || c == '.')
            {
                currentNumber.Append(c);
            }
            else
            {
                if (currentNumber.Length > 0)
                {
                    tokens.Add(new Token { Type = TokenType.Number, Value = double.Parse(currentNumber.ToString()) });
                    currentNumber.Clear();
                }

                if (c == '+' || c == '-' || c == '*' || c == '/')
                {
                    tokens.Add(new Token { Type = TokenType.Operator, Operator = c });
                }
                else if (c == '(')
                {
                    tokens.Add(new Token { Type = TokenType.LeftStaple });
                }
                else if (c == ')')
                {
                    tokens.Add(new Token { Type = TokenType.RightStaple });
                }
            }
        }

        if (currentNumber.Length > 0)
        {
            tokens.Add(new Token { Type = TokenType.Number, Value = double.Parse(currentNumber.ToString()) });
        }

        return tokens;
    }
}