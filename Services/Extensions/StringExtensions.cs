using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Services.Extensions
{
    public static class StringExtensions
    {
        public static string ToLowerNonAccentVN(this string str)
        {
            str = str.ToLower();
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = str.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static string ToAliasUrl(this string str)
        {
            return str.ToLowerNonAccentVN().Replace(" ", "-");
        }
    }
}
