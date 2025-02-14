﻿using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace BlackSpiritHelper.Core
{
    public static class StringUtils
    {
        /// <summary>
        /// Check if the string contains only letters and numbers.
        /// </summary>
        /// <param name="input">The string.</param>
        /// <param name="underscores">Are underscores allowed?</param>
        /// <param name="spaces">Are spaces allowed?</param>
        /// <param name="dashes">Are dashes allowed?</param>
        /// <param name="specials">Special additional character restriction.</param>
        /// <returns></returns>
        public static bool CheckAlphanumeric(this string input, bool underscores = false, bool spaces = false, bool dashes = false, string specials = "")
        {
            string regChars = "";

            regChars += specials;

            if (underscores)
                regChars += "_";

            if (spaces)
                regChars += " ";

            if (dashes)
                regChars += "-";

            return Regex.IsMatch(input, @"^[a-zA-Z0-9" + regChars + @"]+$");
        }

        /// <summary>
        /// Check if the string contains only digits.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool CheckNumeric(this string input)
        {
            return Regex.IsMatch(input, @"^[0-9]+$");
        }

        /// <summary>
        /// Simple URL check.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool CheckURL(this string input)
        {
            Uri uriResult;
            return Uri.TryCreate(input, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        /// <summary>
        /// Check if the string has appropriate form as HEX color.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="hasHashmark"></param>
        /// <returns></returns>
        public static bool CheckColorHEX(this string str, bool hasHashmark = false)
        {
            if (hasHashmark)
                return Regex.IsMatch(str, @"^#[A-Fa-f0-9]{6}$");

            return Regex.IsMatch(str, @"^[A-Fa-f0-9]{6}$");
        }

        /// <summary>
        /// Transfer color HEX string into HEX string without hashmark with standard 6 character format.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToHexStringWithoutHashmark(this string str)
        {
            string colorString;

            if (str.Length == 9)
                colorString = str.Substring(3);
            // Color has hashmark.
            else if (str.Length == 7)
                colorString = str.Substring(1);
            // Color is already in that format.
            else
                colorString = str;

            return colorString;
        }

        /// <summary>
        /// Remove N lines from the beginning of the string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveFirstLines(this string str, int nLines)
        {
            var lines = Regex.Split(str, "\r\n|\r|\n").Skip(nLines);
            return string.Join(Environment.NewLine, lines.ToArray());
        }

        /// <summary>
        /// Get N lines from the beginning of the string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetFirstLines(this string str, int nLines)
        {
            var lines = Regex.Split(str, "\r\n|\r|\n");
            var newLines = new string[nLines];

            for (int i = 0; i < lines.Length; i++)
            {
                if (i >= nLines)
                    break;

                newLines[i] = lines[i];
            }

            return string.Join(Environment.NewLine, newLines.ToArray());
        }
    }
}
