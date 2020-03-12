using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolveMathExpression
{
    public class BinaryNode
    {
        public Fraction?  Value;
        public Operator   Operator;
        public BinaryNode Parent;
        public BinaryNode Left;
        public BinaryNode Right;

        public BinaryNode()
        {
            Value = null;
            Operator = null;
            Parent = null;
            Left = null;
            Right = null;
        }

        public BinaryNode(Fraction? value, Operator oper, BinaryNode parent, BinaryNode left, BinaryNode right)
        {
            Value = value;
            Operator = oper;
            Parent = parent;
            Left = left;
            Right = right;
        }

        public void Print(bool last = true, string indent = "")
        {
            Console.WriteLine(indent + "+- " + ToString());
            indent += last ? "   " : "|  ";
            if (Left != null)
                Left.Print(false, indent);
            if (Right != null)
                Right.Print(false, indent);
        }

        public override string ToString()
        {
            return Value?.ToString() ?? Operator?.ToString();
        }

        public Fraction Evaluate()
        {
            return Value ?? Operator.Operate(Left.Evaluate(), Right.Evaluate());
        }
    }
}
