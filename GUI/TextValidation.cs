using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUI
{
    class TextValidation
    {
        public static void CheckIsNumeric(TextCompositionEventArgs e)
        {
            int result;

            if (!int.TryParse(e.Text, out result))
            {
                e.Handled = true;
            }
        }

        public static bool CheckIsMail(string mail)
        {
            if(mail.Contains("@") && mail.Contains(".") && !mail.StartsWith("@") && !mail.StartsWith(".") && !mail.EndsWith("@") && !mail.EndsWith(".")){
                return true;
            } else
            {
                return false;
            }
        }

        public static bool CheckIsPostalCode (string postalCode)
        {
            postalCode= postalCode.Replace(" ", String.Empty);
            return (postalCode.Length == 5);
        }

        public static bool CheckIsDate(string date)
        {
            if (string.IsNullOrEmpty(date)) return false;
            return DateTime.TryParse(date, out DateTime dateTime);
        }

        public static bool CheckIsIban(string iban)
        {
            iban = iban.ToUpper().Replace(" ", String.Empty); //IN ORDER TO COPE WITH THE REGEX BELOW
            if (String.IsNullOrEmpty(iban))
                return false;
            else if (iban.Length != 22)
                return false;
            else if (System.Text.RegularExpressions.Regex.IsMatch(iban, "^[A-Z0-9]"))
            {
                string bank =
                iban.Substring(4, iban.Length - 4) + iban.Substring(0, 4);
                int asciiShift = 55;
                StringBuilder sb = new StringBuilder();
                foreach (char c in bank)
                {
                    int v;
                    if (Char.IsLetter(c)) v = c - asciiShift;
                    else v = int.Parse(c.ToString());
                    sb.Append(v);
                }
                string checkSumString = sb.ToString();
                int checksum = int.Parse(checkSumString.Substring(0, 1));
                for (int i = 1; i < checkSumString.Length; i++)
                {
                    int v = int.Parse(checkSumString.Substring(i, 1));
                    checksum *= 10;
                    checksum += v;
                    checksum %= 97;
                }
                return checksum == 1;
            }
            else
                return false;
        }
    }
}
