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
    /// Interaktionslogik für PauseContract.xaml
    /// </summary>
    public partial class PauseContract : Page
    {

        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        private readonly IProductModule query = (IProductModule)Application.Current.Properties["IProductModule"];
        public PauseContract()
        {
            InitializeComponent();
        }

        private void PauseTime_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                // Vertrag Pausieren Methode muss noch gemacht werden

                GymHomepage gymHomepage = new GymHomepage();
                NavigationService.Navigate(gymHomepage);
            }
        }
    }
}
