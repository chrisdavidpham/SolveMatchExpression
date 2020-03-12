using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LongExtensions;

namespace SolveMathExpression
{
    public readonly struct Fraction
    {
        private readonly long Numerator;
        private readonly long Denominator;

        public Fraction(long numerator, long denominator)
        {
            Numerator = numerator;
            Denominator = denominator;
        }
        public Fraction(long numerator)
        {
            Numerator = numerator;
            Denominator = 1L;
        }

        public static Fraction operator -(Fraction a) => new Fraction(-a.Numerator, a.Denominator);
        public static Fraction operator +(Fraction a, Fraction b) => new Fraction(checked(a.Numerator * b.Denominator + b.Numerator * a.Denominator), checked(a.Denominator * b.Denominator)).Simplify();
        public static Fraction operator -(Fraction a, Fraction b) => a + (-b);
        public static Fraction operator *(Fraction a, Fraction b) => new Fraction(checked(a.Numerator * b.Numerator), checked(a.Denominator * b.Denominator)).Simplify();
        public static Fraction operator /(Fraction a, Fraction b) => new Fraction(checked(a.Numerator * b.Denominator), checked(a.Denominator * b.Numerator)).Simplify();

        public Fraction Simplify()
        {
            long Gcd = GreatestCommonDivisor(Numerator, Denominator);
            long a   = Numerator / Gcd;
            long b   = Denominator / Gcd;
            return new Fraction(a, b);
        }

        public static long GreatestCommonDivisor(long a, long b)
        {
            a = a.Abs();
            b = b.Abs();
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }
            return a == 0 ? b : a;
        }

        public override string ToString()
        {
            return Denominator == 1 ? Numerator.ToString() : Numerator + "/" + Denominator;
        }

        public double ToDouble()
        {
            return (double)Numerator / (double)Denominator;
        }
    }
}
