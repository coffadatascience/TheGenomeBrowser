using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TheGenomeBrowser.Helpers
{
    /// <summary>
    /// specific helper class for parsing the text field from data files
    /// </summary>
    public static class TextParser
    {
        /// <summary>
        /// procedure that removes a character from a string (used to remove the quotes from a string)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="character"></param>
        /// <returns></returns>
        public static string Remove(this string text, char character)
        {
            var sb = new StringBuilder();
            foreach (char c in text)
            {
                if (c != character)
                    sb.Append(c);
            }
            return sb.ToString();
        }

        /// <summary>
        /// /procedure that checks if a string is a valid value to create a folder
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsValidFolderName(string name)
        {
            if (name == null)
            {
                return false;
            }

            if (name.Length == 0)
            {
                return false;
            }

            if (name.IndexOfAny(System.IO.Path.GetInvalidPathChars()) != -1)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// procedure that takes a string and removes all invalid characters for a file name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string RemoveInvalidFolderFileNameChars(string name)
        {
            if (name == null)
            {
                return "";
            }

            if (name.Length == 0)
            {
                return "";
            }

            //remove invalid chars
            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            {
                name = name.Replace(c.ToString(), "");
            }

            return name;
        }


        /// <summary>
        /// try parse a string to a GUID
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static System.Guid TryParseStringToGuid(string guid)
        {
            try
            {
                Guid ParsedGuid = Guid.Parse(guid);
                Console.WriteLine($"Converted {guid} to a Guid");
                return ParsedGuid;
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("The string to be parsed is null.");
            }
            catch (FormatException)
            {
                Console.WriteLine($"Bad format: {guid}");
            }

            return System.Guid.Empty;

        }

        /// <summary>
        /// procedures that attempts to convert a string to int, if the value is not correct then it will return -1 (since this is usually used to state that a samples is failed)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ParseStringToInt(string value)
        {
            int myInt = -1;

            if (int.TryParse(value, out myInt))
            {
                return myInt;
            }
            return myInt;
        }

        /// <summary>
        /// procedure that attempts to convert a string to double, if the value is not correct then it will return -1 (since this is usually used to state that a samples is failed)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ParseStringToDouble(string value)
        {
            double myDouble = -1;

            if (double.TryParse(value, out myDouble))
            {
                return myDouble;
            }
            return myDouble;
        }

        /// <summary>
        /// procedure that attempts to convert a string to bool, if the value is not correct hten it will return false (since this is usually used to state that a samples is failed)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool BoolTryParse(string value)
        {
            Boolean myBool = false;

            if (Boolean.TryParse(value, out myBool))
            {
                return myBool;
            }
            return myBool;
        }

        /// <summary>
        /// procedure to properly parse a string to a guid
        /// </summary>
        /// <param name="s"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool GuidTryParse(string s, out Guid result)
        {
            //check if string is not empty and matches the guid regex
            if (!String.IsNullOrEmpty(s) && guidRegEx.IsMatch(s))
            {

                //parse the string to a guid
                result = new Guid(s);

                //return true
                return true;
            }

            result = default(Guid);
            return false;
        }

        /// <summary>
        /// procedure that converts a string to a bool
        /// </summary>
        /// <param name="qualityPassedByCdm"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        internal static bool TryParseStringToBoolean(string qualityPassedByCdm)
        {
            //try to convert string to bool
            bool ParsedBool = false;
            if (bool.TryParse(qualityPassedByCdm, out ParsedBool))
            {
                return ParsedBool;
            }
            return ParsedBool;
        }

        /// <summary>
        /// try to parse a string to a color
        /// </summary>
        /// <param name="colorName"></param>
        /// <returns></returns>
        public static Color TryParseStringToColor(string colorName)
        {
            System.Drawing.Color systemColor = System.Drawing.Color.FromName(colorName);
            return systemColor;
        }

        /// <summary>
        /// procedure that attempts a conversion of a string to a double
        /// </summary>
        /// <param name="locationStart"></param>
        /// <returns></returns>
        public static double DoubleTryParse(string locationStart)
        {
            double ParsedDouble = -1;
            if (double.TryParse(locationStart, out ParsedDouble))
            {
                return ParsedDouble;
            }
            return ParsedDouble;
        }

        /// <summary>
        /// procedure that combine 
        /// </summary>
        /// <param name="chromosomeParsed"></param>
        /// <param name="locationStartParsed"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        internal static double CombineDouble(double chromosomeParsed, object locationStartParsed)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// local regex that is used to parse a string to a guid
        /// </summary>
        static Regex guidRegEx = new Regex("^[A-Fa-f0-9]{32}$|" +
                              "^({|\\()?[A-Fa-f0-9]{8}-([A-Fa-f0-9]{4}-){3}[A-Fa-f0-9]{12}(}|\\))?$|" +
                              "^({)?[0xA-Fa-f0-9]{3,10}(, {0,1}[0xA-Fa-f0-9]{3,6}){2}, {0,1}({)([0xA-Fa-f0-9]{3,4}, {0,1}){7}[0xA-Fa-f0-9]{3,4}(}})$", RegexOptions.Compiled);

    }
}
