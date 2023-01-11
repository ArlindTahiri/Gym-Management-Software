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
    }
}
