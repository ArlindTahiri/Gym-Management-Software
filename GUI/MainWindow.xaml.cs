using loremipsum.Gym;
using loremipsum.Gym.Persistence;
using System.Windows;
using System.Windows.Navigation;


namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        private readonly IProductAdmin admin;
        private readonly IProductModule query;
        public MainWindow()
        {
            InitializeComponent();
            IGymPersistence persistence = new GymPersistenceEF();
            GymFactory factory = new GymFactory(persistence);
            admin = factory.GetProductAdmin();
            query = factory.GetProductModule();
            Application.Current.Properties.Add("IProductAdmin", admin);
            Application.Current.Properties.Add("IProductModule", query);
        }
    }
}
