using loremipsum.Gym;
using loremipsum.Gym.Entities;
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
    /// Interaktionslogik für AddContract.xaml
    /// </summary>
    public partial class AddContract : Page
    {

        private readonly IProductAdmin admin = (IProductAdmin)Application.Current.Properties["IProductAdmin"];
        public AddContract()
        {
            InitializeComponent();
        }

        private void AddContractButton_Click(object sender, RoutedEventArgs e)
        {

            Contract newContract = new Contract(ContractType.Text, Int32.Parse(Price.Text));
            admin.AddContract(newContract);

            GymHomepage gymHomepage = new GymHomepage();
            NavigationService.Navigate(gymHomepage);
        }
    }
}
