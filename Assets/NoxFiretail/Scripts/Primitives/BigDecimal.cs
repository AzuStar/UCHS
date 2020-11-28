using System;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;

namespace NoxFiretail.Scripts.Primitives
{
    /// <summary>
    /// Imperfect precision (Precision=<see cref="Precision">8</see>) infinite decimal number. <br />
    /// <i>Unrealistic hard cap of the number is <b>9223372036854775807E+2147483647</b></i>
    /// </summary>
    public struct BigDecimal : IComparable<BigDecimal>
    {
        #region Data
        public long Mantissa; // [-2^63 ~~ 2^63-1]E()
        public int Exponent; // ()E[-2^31 ~~ 2^31-1]
        #endregion
        #region Static info
        public const int Precision = 8; // (NumberOfDigits(Int64.MaxValue)-3)/2
        public static readonly string[] Notations = { "M", "B", "T", "q", "Q", "s", "S", "O", "D" }; // anythin beyond is too complicated
        public static readonly string[] LongNotations = { "Million", "Billion", "Trillion", "Quadrillion", "Quintillion", "Sextillion", "Septillion", "Octillion", "Nonillion", "Decillion" };
        #endregion
        #region Creation Of Numbers
        /// <summary>
        /// Creates basic bigdecimal
        /// </summary>
        /// <param name="man"></param>
        /// <param name="exp"></param>
        public BigDecimal(long man, int exp)
        {
            Mantissa = man;
            Exponent = exp;
            Truncate();
        }

        public BigDecimal(string stringNumber)
        {
            Regex reg = new Regex(@"(-?\d+)(\.(\d+))?(E([+-]?\d+))?");
            Match match = reg.Match(stringNumber);
            if (match.Success)
            {
                string man = match.Groups[1].Value + match.Groups[3].Value;
                Mantissa = Int64.Parse(man.Substring(0, Math.Min(man.Length, Precision)));
                string exp = match.Groups[5].Value;
                if (!String.IsNullOrEmpty(exp))
                    Exponent = Int32.Parse(exp);
                else Exponent = 0;
            }
            else
            {
                stringNumber = stringNumber.Replace(",", String.Empty);
                int len = stringNumber.Length;
                Mantissa = Int64.Parse(stringNumber.Substring(0, Math.Min(len, Precision)));
                if (len > Precision)
                    Exponent = stringNumber.Substring(Precision).Length;
                else Exponent = 0;
            }
        }
        #endregion
        #region Util Functions
        private int NumberOfDigitsMantissa()
        {
            return Mantissa == 0L ? 1 : 1 + (int)Math.Log10(Math.Abs((double)Mantissa));
        }
        /// <summary>
        /// Truncate all numbers above precision
        /// </summary>
        /// <param name="precision"></param>
        private BigDecimal Truncate(int precision = Precision) // store long to get around casting
        {
            int tmp = NumberOfDigitsMantissa() - precision;
            if (tmp > 0)
            {
                if (Mantissa < 0)
                    Exponent -= tmp;
                else
                    Exponent += tmp;
                Mantissa /= (long)Math.Pow(10, tmp);
            }
            return this;
        }
        private static long AlignMantissa(BigDecimal val, BigDecimal reference)
        {
            return val.Mantissa * (long)Math.Pow(10, val.Exponent - reference.Exponent);
        }
        #endregion

        #region Comparison
        public static bool operator ==(BigDecimal left, BigDecimal right)
        {
            return left.Exponent == right.Exponent && left.Mantissa == right.Mantissa;
        }

        public static bool operator !=(BigDecimal left, BigDecimal right)
        {
            return left.Exponent != right.Exponent || left.Mantissa != right.Mantissa;
        }
        public static bool operator <(BigDecimal left, BigDecimal right)
        {
            if (left.Exponent < right.Exponent - Precision) return true; // don't compare outside Precision
            if (left.Exponent - Precision > right.Exponent) return false;
            if (left.Exponent < right.Exponent) return AlignMantissa(right, left) > left.Mantissa;
            return AlignMantissa(left, right) < right.Mantissa;
        }
        public static bool operator >(BigDecimal left, BigDecimal right)
        {
            if (left.Exponent < right.Exponent - Precision) return false; // don't compare outside Precision
            if (left.Exponent - Precision > right.Exponent) return true;
            if (left.Exponent < right.Exponent) return AlignMantissa(right, left) < left.Mantissa;
            return AlignMantissa(left, right) > right.Mantissa;
        }

        public static bool operator <=(BigDecimal left, BigDecimal right)
        {
            if (left.Exponent < right.Exponent - Precision) return true; // don't compare outside Precision
            if (left.Exponent - Precision > right.Exponent) return false;
            if (left.Exponent < right.Exponent) return AlignMantissa(right, left) >= left.Mantissa;
            return AlignMantissa(left, right) <= right.Mantissa;
        }

        public static bool operator >=(BigDecimal left, BigDecimal right)
        {
            if (left.Exponent < right.Exponent - Precision) return false; // don't compare outside Precision
            if (left.Exponent - Precision > right.Exponent) return true;
            if (left.Exponent < right.Exponent) return AlignMantissa(right, left) <= left.Mantissa;
            return AlignMantissa(left, right) >= right.Mantissa;
        }
        #endregion
        #region Add, Sub Mult, Div Operations

        public static BigDecimal operator *(BigDecimal left, BigDecimal right)
        {
            return new BigDecimal(left.Mantissa * right.Mantissa, left.Exponent + right.Exponent).Truncate();
        }
        public static BigDecimal operator /(BigDecimal dividend, BigDecimal divisor)
        {
            var exponentChange = Precision - (dividend.NumberOfDigitsMantissa() - divisor.NumberOfDigitsMantissa());
            if (exponentChange < 0)
            {
                exponentChange = 0;
            }
            dividend.Mantissa *= (long)Math.Pow(10, exponentChange);
            return new BigDecimal(dividend.Mantissa / divisor.Mantissa, dividend.Exponent - divisor.Exponent - exponentChange).Truncate();
        }
        public static BigDecimal operator -(BigDecimal value)
        {
            value.Mantissa *= -1;
            return value;
        }
        public static BigDecimal operator ++(BigDecimal value)
        {
            return value + 1;
        }

        public static BigDecimal operator --(BigDecimal value)
        {
            return value - 1;
        }

        public static BigDecimal operator +(BigDecimal left, BigDecimal right)
        {
            if (left.Exponent < right.Exponent - Precision) return right; // don't do anything, just return highest
            if (left.Exponent - Precision > right.Exponent) return left;
            if (left.Exponent > right.Exponent) return new BigDecimal(AlignMantissa(left, right) + right.Mantissa, right.Exponent).Truncate();
            return new BigDecimal(AlignMantissa(right, left) + left.Mantissa, left.Exponent).Truncate();
        }

        public static BigDecimal operator -(BigDecimal left, BigDecimal right)
        {
            return left + -right;
            // if (left.Exponent < right.Exponent - Precision) return right; // don't do anything, just return highest
            // if (left.Exponent - Precision > right.Exponent) return left;
            // if (left.Exponent > right.Exponent) return new BigDecimal(AlignMantissa(right, left) - left.Mantissa, left.Exponent);
            // return new BigDecimal(AlignMantissa(left, right) - right.Mantissa, right.Exponent);
        }
        #endregion
        #region Extras
        public static BigDecimal Min(BigDecimal left, BigDecimal right)
        {
            if (left < right) return left;
            return right;
        }
        public static BigDecimal Max(BigDecimal left, BigDecimal right)
        {
            if (left < right) return right;
            return left;
        }
        #endregion

        #region String Output
        public override string ToString()
        {
            string str = Mantissa.ToString();
            if (Exponent < 0)
            {
                int local = NumberOfDigitsMantissa() + Exponent;
                if (local <= 0)
                    return "0." + new String('0', -local) + Mantissa;
                else
                    return str.Substring(0, local) + '.' + str.Substring(local);
            }
            else
                return str + new string('0', Exponent);
        }

        public string ToExponentString()
        {
            int localExp = Exponent + NumberOfDigitsMantissa() - 1;
            string s = String.Format("{0:#.###E+00}", Mantissa);
            return s.Substring(0, Math.Max(0, s.Length - 4)) + (localExp < 0 ? "E" : "E+") + localExp;
        }

        public string ToDecoratedString()
        {
            int localExp = Exponent + NumberOfDigitsMantissa();
            if ((Notations.Length + 1) * 3 < localExp) // figure if current exponent over available notations
                return ToExponentString();
            if (localExp > 6)
            {
                int bracket = (int)((localExp - 7) / 3f);
                string s = String.Empty;
                switch (localExp % 3)
                {
                    case 0:
                        s = String.Format("{0:###.##E+00}", Mantissa);
                        break;
                    case 1:
                        s = String.Format("{0:#.##E+00}", Mantissa);
                        break;
                    case 2:
                        s = String.Format("{0:##.##E+00}", Mantissa);
                        break;
                }
                return s.Substring(0, Math.Max(0, s.Length - 4)) + Notations[bracket];
            }
            string str = Mantissa + new string('0', Exponent);
            for (int i = str.Length - 3; i > 0; i -= 3)
                str = str.Insert(i, ",");
            return str;
        }
        #endregion
        #region Implicit Casts
        public static implicit operator BigDecimal(long i)
        {
            return new BigDecimal(i, 0);
        }
        public static implicit operator BigDecimal(int i)
        {
            return new BigDecimal(i, 0);
        }
        public static implicit operator BigDecimal(string s)
        {
            return new BigDecimal(s);
        }
        public static implicit operator string(BigDecimal bd)
        {
            return bd.ToString();
        }
        #endregion
        #region SortCompare, HashCode, Equals
        public override int GetHashCode()
        {
            unchecked
            {
                return (Mantissa.GetHashCode() * 123) ^ Exponent;
            }
        }
        public override bool Equals(object obj)
        {
            if (typeof(object) != typeof(BigDecimal)) throw new ArgumentException();
            return Mantissa == ((BigDecimal)obj).Mantissa && Exponent == ((BigDecimal)obj).Exponent;
        }
        public int CompareTo(BigDecimal other)
        {
            return this < other ? -1 : (this > other ? 1 : 0);
        }
        #endregion
    }
}