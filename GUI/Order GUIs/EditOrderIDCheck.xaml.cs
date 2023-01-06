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

namespace GUI.Order_GUIs
{
    /// <summary>
    /// Interaktionslogik für EditOrderIDCheck.xaml
    /// </summary>
    public partial class EditOrderIDCheck : Page
    {

        IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        public EditOrderIDCheck()
        {
            InitializeComponent();
        }

        private void IDCheck_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if(query.GetOrderDetails(Int32.Parse(IDCheck.Text))!=null)
                {
                    EditOrder editOrder = new EditOrder(Int32.Parse(IDCheck.Text));
                    NavigationService.Navigate(editOrder);
                } else
                {
                   WarningText.Text = "Die eingebene ID ist ungültig. Bitte geben Sie eine gültige ID ein.";
                }
            }
        }
    }
}
