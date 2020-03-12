using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveMathExpression
{
    public class Operator
    {
        private enum OperatorName { Multiply, Divide, Add, Subtract };
        private OperatorName? Name;
        private Func<Fraction, Fraction, Fraction> Function;

        public Operator(char symbol)
        {
            bool success = true;
            success &= TrySetName(symbol);
            success &= TrySetFunction(symbol);
            if (!success)
                throw new OperatorException("Unrecognized symbol.");
        }

        public Fraction Operate(Fraction x, Fraction y)
        {
            return Function(x, y);
        }

        public override string ToString()
        {
            return Name.ToString();
        }

        public static bool IsOperator(char symbol)
        {
            return new char[] { '+', '-', '*', '/' }.Contains(symbol);
        }

        private bool TrySetName(char symbol)
        {
            Name = symbol == '+' ? OperatorName.Add
                 : symbol == '-' ? OperatorName.Subtract
                 : symbol == '*' ? OperatorName.Multiply
                 : symbol == '/' ? OperatorName.Divide
                 : (OperatorName?)null;
            return Name != null;
        }

        private bool TrySetFunction(char symbol)
        {
            Function = symbol == '+' ? (x, y) => x + y
                     : symbol == '-' ? (x, y) => x - y
                     : symbol == '*' ? (x, y) => x * y
                     : symbol == '/' ? (x, y) => x / y
                     : (Func<Fraction, Fraction, Fraction>)null;
            return Function != null;
        }
    }
    public class OperatorException : Exception
    {
        private static readonly string DefaultMessage = "Operator error.";
        public OperatorException() : base(DefaultMessage) { }
        public OperatorException(string message) : base(message) { }
        public OperatorException(string message, Exception inner) : base(message, inner) { }
    }

}

