using Mini_Shop_mit_Warenkorb_Simulation_WPF.ViewModels;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mini_Shop_mit_Warenkorb_Simulation_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            //if (!File.Exists("Data/products.csv"))
            //{
            //    MessageBox.Show("Die Datei 'Data/products.csv' wurde nicht gefunden.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            //    Application.Current.Shutdown();
            //}
        }
    }
}