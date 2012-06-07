using System;

namespace AthleticsDatabase
{
    /// <summary>Contains various functions that can come useful</summary>
    public static class VariousFunctions
    {
        /// <summary>Checkif a string input is numeric</summary>
        /// <param name="value">The string input</param>
        /// <returns>Returns true if Is Numeric</returns>
        public static bool IsNumeric(string value)
        {
            try
            {
                // ReSharper disable UnusedVariable
                var int32 = Convert.ToInt32(value);
                // ReSharper restore UnusedVariable
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>Validate a date in the format of DD/MM/YYYY</summary>
        /// <param name="value">The string date value</param>
        /// <returns>Returns true if the string is a valid date</returns>
        public static bool ValidateDate(string value)
        {
            try
            {
                //DD/MM/YYYY
                if (value.Length > 10) return false;
                if (!IsNumeric(value.Substring(0,2))) return false;
                if (value.Substring(2, 1) != "/") return false;
                if (!IsNumeric(value.Substring(3, 2))) return false;
                if (value.Substring(5, 1) != "/") return false;
                //if year is not a number or year less than 2000
                return IsNumeric(value.Substring(6, 4)) && Int32.Parse(value.Substring(6, 4)) >= 2000;
            }
            catch
            {
                return false;
            }
        }
    }
}
