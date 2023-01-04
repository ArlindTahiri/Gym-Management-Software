using GUI.MemberGUIs;
using loremipsum.Gym;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GUI.EmployeeGUIs
{
    /// <summary>
    /// Interaktionslogik für EditEmployeeIDCheck.xaml
    /// </summary>
    public partial class EditEmployeeIDCheck : Page
    {
        private IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        public EditEmployeeIDCheck()
        {
            InitializeComponent();
        }

        private void IDCheck_KeyDown(object sender, KeyEventArgs e)
        {
            string content = IDCheck.Text;

            if(e.Key == Key.Enter)
            {
                if(query.GetEmployeeDetails(Int32.Parse(content))!=null)
                {
                    
                    EditEmployee editEmployee = new EditEmployee(Int32.Parse(content));
                    NavigationService.Navigate(editEmployee);
                }
            }
        }

       
    }
}
