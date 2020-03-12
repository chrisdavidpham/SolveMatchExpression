using System;
using System.Collections.Generic;
using System.Diagnostics;
using StopwatchExtensions;

namespace SolveMathExpression
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();
            //Console.WriteLine(Time(100000,20,100));
        }

        private static void Run()
        {
            //string[] expressions = RandomExpressions(1,30,100);

            string[] expressions = new string[] { "1 + 1 / 2" };
            
            foreach (string expression in expressions)
            {
                Console.WriteLine("Expression" + Environment.NewLine + expression + Environment.NewLine);
                BinaryTree tree = new BinaryTree(expression);

                Console.WriteLine("Tree");
                tree.Print();

                Fraction solution = tree.Evaluate();
                Console.WriteLine(Environment.NewLine + "Solution" + Environment.NewLine + solution);
                Console.WriteLine(solution.ToDouble() + Environment.NewLine);
            }
        }

        static double Time(int n, int tokens, long max)
        {
            Stopwatch stopwatch = new Stopwatch();

            string[] expressions = RandomExpressions(n, tokens, max);

            TimeSpan timeSpan = stopwatch.Time(() => Array.ForEach(expressions, e => new BinaryTree(e).TryEvaluate()));
            return timeSpan.TotalMilliseconds;
        }

        static string[] RandomExpressions(int n, int tokens, long max)
        {
            List<string> strings = new List<string>();

            MathExpression mathExpression = new MathExpression();

            while (n-- > 0)
                strings.Add(mathExpression.GenerateRandom(tokens, max));

            return strings.ToArray();
        }
    }
}