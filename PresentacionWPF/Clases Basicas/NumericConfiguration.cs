using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Vista
{

    public partial class NumericConfiguration:Window
    {
        public static readonly Regex regex = new Regex("[^0-9,.-]");

        public static readonly Regex regexString = new Regex("[^A-Za-z]");

        public static NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;       
        public static void SetNumericConfiguration()
        {
            nfi.NumberGroupSeparator = ".";
            nfi.NumberDecimalSeparator = ",";
        }

        public decimal ConvertDecimalTwoPlaces<T>(T number)
        {
            string str = null;

            decimal amount = 0;

            try
            {
                if (String.IsNullOrWhiteSpace(number.ToString()) == false)
                {
                    str = regex.Replace(number.ToString(), String.Empty);

                    amount = decimal.Parse(str.ToString(), nfi);

                    amount = Math.Round(amount, 2);
                }
                else
                {
                    amount = 0;
                }

                return amount;
            }
            catch(FormatException ex)
            {
                return amount;

            }

            catch (Exception ex)
            {
                return amount;
            }


        }

    }
}
