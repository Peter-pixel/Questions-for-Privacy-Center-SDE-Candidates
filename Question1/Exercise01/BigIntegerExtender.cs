using System.Numerics;

namespace Exercise01
{
    public static class BigIntegerExtender
    {
        public static string EndPart(BigInteger value, BigInteger divider)
        {
            BigInteger modulus = value % divider;
            string endPart = modulus == 0 ? "" : $"{(divider == 100 ? "and " : "")}{Towards(modulus)}";
            return endPart;
        }

        public static string Towards(BigInteger value)
        {
            string[] uniqueDigits = new string[] {
                "zero", "one", "two", "three", "four",
                "five", "six", "seven", "eight", "nine",
                "ten", "eleven", "twelve",
                "thirteen",  "fourteen", "fifteen", "sixteen",
                "seventeen", "eighteen", "nineteen"
            };

            string[] tensMultiple = new string[] {
                "", "", "twenty",  "thirty", "forty",
                "fifty", "sixty", "seventy", "eighty", "ninety"
            };

            string[] tensPower = new string[] {
                "hundred", "thousand", "million", "billion", "trillion", "quadrillion", "quintillion"
            };
            if (value < 0)
            {
                value *= -1;
            }

            if (value < 20)
            {
                return uniqueDigits[(int)value];
            }

            if (value < 100)
            {
                int modulus = (int)value % 10;
                return $"{tensMultiple[(int)value / 10]} {(modulus == 0 ? "" : uniqueDigits[modulus])}";
            }

            // Hundred
            if (value < 1000)
            {
                int divider = 100;
                return $"{Towards(value / divider)} {tensPower[0]} {EndPart(value, divider)}";
            }

            // Thousand
            if (value < 1_000_000)
            {
                int divider = 1_000;
                return $"{Towards(value / divider)} {tensPower[1]} {EndPart(value, divider)}";
            }

            // Million
            if (value < 1_000_000_000)
            {
                int divider = 1_000_000;
                return $"{Towards(value / divider)} {tensPower[2]} {EndPart(value, divider)}";
            }

            // Billion
            if (value < 1_000_000_000_000)
            {
                int divider = 1_000_000_000;
                return $"{Towards(value / divider)} {tensPower[3]} {EndPart(value, divider)}";
            }

            // Trillion
            if (value < 1_000_000_000_000_000)
            {
                BigInteger divider = 1_000_000_000_000;
                return $"{Towards(value / divider)} {tensPower[4]} {EndPart(value, divider)}";
            }

            // Quadrillion
            if (value < 1_000_000_000_000_000_000)
            {
                BigInteger divider = 1_000_000_000_000_000;
                return $"{Towards(value / divider)} {tensPower[5]} {EndPart(value, divider)}";
            }

            // Quintillion
            if (value < 1_000_000_000_000_000_000)
            {
                BigInteger divider = 1_000_000_000_000_000_000;
                return $"{Towards(value / divider)} {tensPower[6]} {EndPart(value, divider)}";
            }

        
            return $"{value} is not supported";
        }
        public static string ToWords(this BigInteger bigInteger)
        {
            return Towards(bigInteger);
        }

        public static string ToWords(this int integer)
        {
            return Towards(integer);
        }
    }
}