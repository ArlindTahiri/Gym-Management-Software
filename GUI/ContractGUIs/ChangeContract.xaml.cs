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
    /// Interaktionslogik für ChangeContract.xaml
    /// </summary>
    public partial class ChangeContract : Page
    {

        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
       
        public ChangeContract()
        {
            InitializeComponent();
        }

        private void StandardMonthlyButton_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan duration = new TimeSpan(30, 0, 0, 0, 0);

            admin.UpdateContract(ContractCache.cacheID, "Standard monthly", 30);

            GymHomepage gymHomepage = new GymHomepage();
            NavigationService.Navigate(gymHomepage);
        }

        private void StandardYearlyButton_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan duration = new TimeSpan(365, 0, 0, 0, 0);

            admin.UpdateContract(ContractCache.cacheID, "Standard monthly", 20);

            GymHomepage gymHomepage = new GymHomepage();
            NavigationService.Navigate(gymHomepage);
        }

        private void PremiumMonthlyButton_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan duration = new TimeSpan(30, 0, 0, 0, 0);

            admin.UpdateContract(ContractCache.cacheID, "Standard monthly", 35);

            GymHomepage gymHomepage = new GymHomepage();
            NavigationService.Navigate(gymHomepage);
        }

        private void PremiumYearlyButton_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan duration = new TimeSpan(365, 0, 0, 0, 0);

            admin.UpdateContract(ContractCache.cacheID, "Standard monthly", 25);

            GymHomepage gymHomepage = new GymHomepage();
            NavigationService.Navigate(gymHomepage);
        }
    }
}
