using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SolveMathExpression
{
    class Program
    {
        private enum Operator { Multiply, Divide, Add, Subtract, OpenParathesis, CloseParanthesis };

        static void Main(string[] args)
        {
            Run();
        }

        private static void Run()
        {
            List<string> expressions = Read("filename");

            foreach (string expression in expressions)
            {
                List<Token> tokens = Tokenize(expression);
                tokens.ForEach(t => Console.WriteLine(t));
            }
        }

        private class Token
        {
            public long? Value { get; protected set; }
            public Operator? Operator { get; protected set; }

            public Token(long value)
            {
                Value = value;
                Operator = null;
            }

            public Token(Operator @operator)
            {
                Value = null;
                Operator = @operator;
            }

            public override string ToString()
            {
                return Operator?.ToString() ?? Value.ToString();
            }
        }

        private static List<Token> Tokenize(string expression)
        {
            CharEnumerator charEnumerator = expression.GetEnumerator();
            List<Token> tokens = new List<Token>();
            List<char> numerals = new List<char>();
            long? number = null;

            while (charEnumerator.MoveNext())
            {
                char symbol = charEnumerator.Current;

                // If numeral, continue to read all consecutive numerals
                if (char.IsNumber(symbol))
                {
                    int value = (int)Char.GetNumericValue(symbol);
                    number *= 10;
                    number = number + value ?? value;
                    continue;
                }

                if (number != null)
                {
                    tokens.Add(new Token((long)number));
                    number = null;
                }

                switch (symbol)
                {
                    case '*':
                        tokens.Add(new Token(Operator.Multiply));
                        break;
                    case '/':
                        tokens.Add(new Token(Operator.Divide));
                        break;
                    case '+':
                        tokens.Add(new Token(Operator.Add));
                        break;
                    case '-':
                        tokens.Add(new Token(Operator.Subtract));
                        break;
                    case '(':
                        tokens.Add(new Token(Operator.OpenParathesis));
                        break;
                    case ')':
                        tokens.Add(new Token(Operator.CloseParanthesis));
                        break;
                    case ' ':
                        break;
                    default:
                        throw new Exception($"Unknown symbol ${symbol}");
                }
            }

            if (number != null)
            {
                tokens.Add(new Token((long)number));
            }

            return tokens;
        }

        static List<string> Read(string file)
        {
            List<string> strings = new List<string>();
            strings.Add("(12 + 15 / (6 - 3)) * 5");
            return strings;
        }
    }
}