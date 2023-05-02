using System;

namespace RomanNumerals
{
    public class Program
    {

        #region Helpers

        /// <summary>
        /// Checks if a string has n repeated characters in a row
        /// </summary>
        /// <param name="str">The string to be checked</param>
        /// <param name="n">The length of the repeated characters to be found</param>
        /// <returns>True if the string has consecutive repeated characters of size n, otherwise returns false</returns>
        public static bool HasConsecutiveChars(string str, int n)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }

            if (str.Length == 1)
            {
                return false;
            }

            int cCount = 1;

            for (int i = 0; i < str.Length - 1; i++)
            {
                char c = str[i];
                if (c == str[i + 1])
                {
                    cCount++;

                    if (cCount >= n)
                    {
                        return true;
                    }
                }
                else
                {
                    cCount = 1;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if a given set of roman numerals are valid
        /// </summary>
        /// <param name="roman">The string of Roman numerals</param>
        /// <returns>True if the string of Roman numerals are valid, and false if it is not valid</returns>
        public static bool IsValid(string? roman)
        {

            char[] numerals = { 'I', 'V', 'X', 'L', 'C', 'D', 'M' };

            if (roman == null) return false;

            // check if all characters are valid numerals

            foreach (char c in roman)
            {
                if (!(numerals.Contains(c))) return false;
            }

            // check if numerals all appear <= 3 times in a row
            if (HasConsecutiveChars(roman, 4)) return false;

            // check if all sequences of two are correct
            string[] sequences = { "II", "IX", "IV", "VI", "XX", "XV", "XI", "XL", "XC", "LI", "LV", "LX", "CC", "CI", "CV", "CX", "CL", "CD", "CM", "DI", "DV", "DX", "DL", "DC", "MM", "MI", "MV", "MX", "ML", "MC", "MD"};
            for (int i = 0; i < roman.Length - 1; i++)
            {
                if (!sequences.Contains(roman[i].ToString() + roman[i + 1].ToString()))
                {
                    return false;
                }
            }

            // check if invalid sequences exist
            string[] invSeqs = { "IXI", "IVI", "XLX", "XCX", "CDC", "CMC", "IIX", "XXL", "XXC", "CCM", "IXX", "XCC", "CMM" };
            for (int i = 0; i < roman.Length - 2; i++)
            {
                if (invSeqs.Contains(roman[i].ToString() + roman[i + 1].ToString() + roman[i + 2].ToString()))
                {
                    return false;
                }
            }

            return true;

        }

        #endregion

        #region Converters

        /// <summary>
        /// Converts a Roman numeral string into numbers
        /// </summary>
        /// <param name="roman">The string of Roman numerals</param>
        /// <returns>The value of the string of Roman numerals, in base 10. If the Roman numerals are not valid, returns -1</returns>
        public static int RomanToDecimal(string? roman)
        {
            if (IsValid(roman))
            {
                roman = roman + "/";

                Dictionary<char, int> val = new Dictionary<char, int>()
                {
                    { 'I', 1 },
                    { 'V', 5 },
                    { 'X', 10 },
                    { 'L', 50 },
                    { 'C', 100 },
                    { 'D', 500 },
                    { 'M', 1000 },
                    { '/', 0 }
                };

                int sum = 0;

                for (int i = 0; i < roman.Length - 1; i++)
                {
                    if (val[roman[i]] < val[roman[i + 1]]) sum -= val[roman[i]];
                    else sum += val[roman[i]];
                }

                return sum;
            }
            return -1;
        }

        /// <summary>
        /// Converts a decimal number n (0 < n < 4000) into a string of Roman numerals
        /// </summary>
        /// <param name="n">The number to be converted.</param>
        /// <returns>The string of roman numerals, null if invalid number given.</returns>
        public static string? DecimalToRoman(int? n)
        {
            string roman = "";

            Dictionary<string, int> nval = new Dictionary<string, int>()
            {
                { "I", 1 },
                { "IV", 4 },
                { "V", 5 },
                { "IX", 9 },
                { "X", 10 },
                { "XL", 40 },
                { "L", 50 },
                { "XC", 90 },
                { "C", 100 },
                { "CD", 400 },
                { "D", 500 },
                { "CM", 900 },
                { "M", 1000 }
            };

            if (n == null) return null;

            if (n < 1 || n > 3999) return null;

            int lIndex = 0;
            string ntmp = "";
            while (n != 0)
            {
                // find the largest numeral value less than val
                for (int i = nval.Values.Count - 1; i >= 0; i--)
                {
                    if (nval.Values.ElementAt(i) <= n)
                    {
                        lIndex = i;
                        break;
                    }

                }
                ntmp = nval.ElementAt(lIndex).Key;
                roman += ntmp;
                n -= nval[ntmp];

            }


            return roman;
        }

        #endregion

        #region Prompts

        /// <summary>
        /// Prompts for converting roman numerals to a decimal value
        /// </summary>
        public static void PromptRomanToDecimal()
        {
            Console.Write("\nEnter roman numerals: ");

            string? roman = Console.ReadLine();
            roman = roman.ToUpper();

            int val = RomanToDecimal(roman);

            if (val == -1) Console.WriteLine("Invalid sequence entered.");
            else Console.WriteLine($"Value of {roman} is {val}");
        }

        /// <summary>
        /// Prompts for converting decimal values to roman numerals
        /// </summary>
        public static void PromptDecimalToRoman()
        {
            Console.Write("\nEnter decimal value: ");
            int? val;
            try
            {
                val = Int32.Parse(Console.ReadLine());
                string roman = DecimalToRoman(val) ?? "";
                if (roman == "" || roman == null)
                {
                    Console.WriteLine("Invalid input.");
                }
                else
                {
                    Console.WriteLine(roman);
                }
            }
            catch
            {
                Console.WriteLine("Invalid input.");
            };
        }

        #endregion

        public static void Main(string[] args)
        {

            Console.WriteLine("============================= Roman Numerals <=> Decimal Value =============================");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("1. Convert Roman Numerals to decimal value\n2. Convert decimal value to Roman Numerals\n3. Clear console");

            int count = 0;
            string? input;
            while (count++ >= 0)
            {
                Console.Write("\n>>> ");

                input = Console.ReadLine();

                if (input == "1") PromptRomanToDecimal();
                else if (input == "2") PromptDecimalToRoman();
                else if (input == "3") Console.Clear();
            }         

        }
    }
}
