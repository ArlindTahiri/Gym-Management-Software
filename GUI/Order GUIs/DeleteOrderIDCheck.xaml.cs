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
    /// Interaktionslogik für DeleteOrderIDCheck.xaml
    /// </summary>
    public partial class DeleteOrderIDCheck : Page
    {
        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        public DeleteOrderIDCheck()
        {
            InitializeComponent();
        }

        private void IDCheck_KeyDown(object sender, KeyEventArgs e)
        {
          ;
            if (e.Key == Key.Enter)
            { 
                if(query.GetOrderDetails(Int32.Parse(IDCheck.Text))!=null ) 
                { 
                
                    DeletePage deletePage = new DeletePage("DeleteOrder", Int32.Parse(IDCheck.Text));
                    NavigationService.Navigate(deletePage);
                       
                } 
                else
                {
                    WarningText.Text = "Die eingebene ID ist ungültig. Bitte geben Sie eine gültige ID ein.";
                }
            }
        }

        private void OrderData_Loaded(object sender, RoutedEventArgs e)
        {
            OrderData.ItemsSource = admin.ListOrders();
        }
    }
}
