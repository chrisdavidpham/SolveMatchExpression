using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

using LongExtensions;

namespace SolveMathExpression
{
    public class BinaryTree
    {
        private BinaryNode Root;

        public BinaryTree(string expression)
        {
            #region Initialization

            Root = new BinaryNode
            (
                value:  null,
                oper:   null,
                parent: null,
                left:   null,
                right:  null
            );
            char symbol = '\0';
            long? number = null;
            BinaryNode current = Root;

            BinaryNode subOperationRoot = null;
            CharEnumerator charEnumerator = expression.GetEnumerator();

            #endregion

            while (charEnumerator.MoveNext())
            {
                symbol = charEnumerator.Current;

                #region Whitespace

                if (symbol == ' ')
                {
                    // Skip white space
                    continue;
                }

                #endregion

                #region Numeral

                // If numeral, continue to read all consecutive numerals
                if (char.IsNumber(symbol))
                {
                    number = number.Append(symbol);
                    continue;
                }

                #endregion

                #region Not numeral

                // Symbol is not a numeral; add number to tokens
                if (number.HasValue)
                {
                    if (current.Value != null)
                        throw new MathExpressionException("Consecutive operands.");
                    current.Value = new Fraction((long)number);
                    number = null;
                }

                #endregion

                #region Sub-expression '('

                if (symbol == '(')
                {
                    // Read the sub-expression
                    StringBuilder stringBuilder = new StringBuilder();
                    Stack<char> stack = new Stack<char>();
                    while (charEnumerator.MoveNext())
                    {
                        char subSymbol = charEnumerator.Current;

                        if (subSymbol == '(')
                            stack.Push(')');
                        else if (subSymbol == ')')
                            if (stack.Any())
                                stack.Pop();
                            else
                                break;

                        stringBuilder.Append(subSymbol);
                    }
                    if (stack.Any())
                        throw new MathExpressionException("Unmatched paranthesis.");

                    BinaryTree subTree = new BinaryTree(stringBuilder.ToString());
                    current.Value = subTree.Root.Value;
                    current.Operator = subTree.Root.Operator;
                    current.Left = subTree.Root.Left;
                    current.Right = subTree.Root.Right;

                    continue;
                }

                #endregion

                #region Operator '+' '-'

                if (symbol == '+' || symbol == '-')
                {
                    BinaryNode newBinaryNode = new BinaryNode
                    (
                        value: null,
                        oper: new Operator(symbol),
                        parent: null,
                        left: Root,
                        right: null
                    );

                    newBinaryNode.Right = new BinaryNode
                    (
                        value: null,
                        oper: null,
                        parent: newBinaryNode,
                        left: null,
                        right: null
                    );

                    Root.Parent = newBinaryNode;
                    Root = newBinaryNode;
                    current = newBinaryNode.Right;

                    subOperationRoot = null;

                    continue;
                }

                #endregion

                #region Operator '*' '/'

                if (symbol == '*' || symbol == '/')
                {
                    BinaryNode newBinaryNode;
                    BinaryNode newBinaryNodeleft = subOperationRoot ?? current;

                    newBinaryNode = new BinaryNode
                    (
                        value:  null,
                        oper:   new Operator(symbol),
                        parent: newBinaryNodeleft.Parent,
                        left:   newBinaryNodeleft,
                        right:  null
                    );

                    if (newBinaryNodeleft == Root)
                        Root = newBinaryNode;
                    else
                        newBinaryNodeleft.Parent.Right = newBinaryNode;

                        
                    newBinaryNode.Right = new BinaryNode
                    (
                        value: null,
                        oper: null,
                        parent: newBinaryNode,
                        left: null,
                        right: null
                    );
                    current.Parent = newBinaryNode;
                    current = newBinaryNode.Right;

                    subOperationRoot = newBinaryNode;

                    continue;
                }

                #endregion

                throw new MathExpressionException("Unrecognized symbol.");
            }

            if (number is null && symbol != '(')
                throw new MathExpressionException("Missing opperand.");
                
            if (number != null)
                current.Value = new Fraction((long)number);
        }

        public Fraction Evaluate()
        {
            return Root.Evaluate();
        }
        
        public Fraction TryEvaluate()
        {
            try
            {
                return Root.Evaluate();
            }
            catch (DivideByZeroException)
            {
                return new Fraction(0);
            }
        }

        public void Print()
        {
            Root.Print();
        }
    }
}
