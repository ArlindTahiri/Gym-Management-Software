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

namespace GUI.ContractGUIs
{
    /// <summary>
    /// Interaktionslogik für ContractIDCheck.xaml
    /// </summary>
    public partial class ContractIDCheck : Page
    {

        IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        public ContractIDCheck()
        {
            InitializeComponent();
        }

        private void IDCheck_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
               if(query.GetContractDetails(Int32.Parse(IDCheck.Text))!=null)
                {
                    DeleteContract deleteContract = new DeleteContract(int.Parse(IDCheck.Text));   
                    NavigationService.Navigate(deleteContract);
                } 
            }
        }
    }
}
