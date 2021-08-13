using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Vista.Clases_Basicas
{
    public class INotifyPropertyChangeObservable:INotifyPropertyChanged
    {
        ControladorDocumento cn = new ControladorDocumento();

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notify a property change
        /// </summary>
        /// <param name="propertyName">Name of property to update</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Notify a property change that uses CallerMemberName attribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="backingField">Backing field of property</param>
        /// <param name="value">Value to give backing field</param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected virtual bool OnPropertyChanged<T>(ref T backingField, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
                return false;

            backingField = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected static bool GetDecimalCount(decimal value)
        {
            bool valid;

            int count = BitConverter.GetBytes(decimal.GetBits(value)[3])[2];

            if (count <= 6)
            {
                valid = true;
            }
            else
            {
                valid = false;
            }

            return valid;
        }

        protected bool VerifyCuentasNoAsociadas(string acctCode)
        {
            var result = cn.VerifyCuentasNoAsociadas(acctCode);

            if (result.Item2 == null && result.Item1 == true)
            {
                return false;
            }
            else
            {
                return true;
            }


        }
        public static bool IsPhoneNumberValidator(string number)
        {

            return Regex.Match(number, @"^(?<countryCode>[\+][1-9]{1}[0-9]{0,2}\s)?(?<areaCode>0?[1-9]\d{0,4})(?<number>\s[1-9][\d]{5,12})(?<extension>\sx\d{0,4})?$").Success;

        }


        public static bool RifValidator(string value)
        {
            return Regex.Match(value, @"^((J|C|V){1}(-){1}[0-9]{8}(-){1}[0-9]{1})$").Success;


        }

        public static bool FaxValidator(string value)
        {
            return Regex.Match(value, @"^(\+?\d{1,}(\s?|\-?)\d*(\s?|\-?)\(?\d{2,}\)?(\s?|\-?)\d{3,}\s?\d{3,})$").Success;
        }

        public static bool IntegerValidator(string value)
        {
            return Regex.Match(value, @"^([0-9]+)$").Success;
        }

        public static bool NumericValidator(string value)
        {
            return Regex.Match(value, @"^[0-9]{1,19}([.,][0-9]{1,6})?$").Success;
        }


        public static bool EmailValidator(string emailAddress)
        {
            var regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            bool isValid = Regex.IsMatch(emailAddress, regex, RegexOptions.IgnoreCase);
            return isValid;
        }

        protected static bool GetNumberOfDigits(decimal value)
        {
            bool valid;

            decimal abs = Math.Abs(value);

            int count = abs < 1 ? 0 : (int)(Math.Log10(decimal.ToDouble(abs)) + 1);

            if (count <= 19)
            {
                valid = true;
            }
            else
            {
                valid = false;
            }

            return valid;
        }

        protected static bool ConvertToDecimal(object value)
        {
            bool valid;

            try
            {
                Convert.ToDecimal(value);

                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
            }

            return valid;

        }

        protected static bool ConvertToInteger(object value)
        {
            bool valid;

            try
            {
                Convert.ToInt64(value);

                valid = true;

            }
            catch (Exception ex)
            {
                valid = false;
            }

            return valid;

        }
    }
}
