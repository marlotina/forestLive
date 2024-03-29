﻿using System.Globalization;
using System.Text;

namespace FL.Pereza.Helpers.Standard.Extensions
{
    public static class StringExtensions
    {
        public static string NormalizeName(this string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }

            return sbReturn.ToString().ToLower();
        }
    }
}
