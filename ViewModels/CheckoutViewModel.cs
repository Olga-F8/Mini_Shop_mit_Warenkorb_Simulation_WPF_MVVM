using Microsoft.Win32;
using Mini_Shop_mit_Warenkorb_Simulation_WPF.Helpers;
using Mini_Shop_mit_Warenkorb_Simulation_WPF.Models;
using Mini_Shop_mit_Warenkorb_Simulation_WPF.Services;
using Mini_Shop_mit_Warenkorb_Simulation_WPF.ViewModels;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Mini_Shop_mit_Warenkorb_Simulation_WPF.ViewModels
{
    public class CheckoutViewModel : BaseViewModel
    {
        private MainViewModel _mainVM;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public decimal TotalPrice => _mainVM.CartVM.TotalPrice;

        public RelayCommand BackCommand { get; set; }
        public RelayCommand CreateInvoiceCommand { get; set; }

        public ObservableCollection<Customer> Customers { get; set; }
        public CheckoutViewModel(MainViewModel mainVM)
        {
            _mainVM = mainVM;

            var service = new CsvService();
            Customers = new ObservableCollection<Customer>(service.LoadCustomers());

            BackCommand = new RelayCommand(_ =>
            {
                _mainVM.CurrentView = _mainVM.CartVM;
                var window = System.Windows.Application.Current.MainWindow;
                window.Width = 900;
                window.Height = 600;
            });

            CreateInvoiceCommand = new RelayCommand(_ =>
            {
                CreateInvoice();
            });
        }
        

        private void CreateInvoice()
        {
            var service = new CsvService();

            var newCustomer = new Customer
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email
            };

            // Kunde speichern
            service.AddCustomer(newCustomer);

            var dialog = new SaveFileDialog
            {
                FileName = $"Rechnung_{FirstName}_{LastName}_{DateTime.Now:yyyyMMdd_HHmmss}.txt",
                Filter = "Textdatei (*.txt)|*.txt",
                DefaultExt = ".txt"
            };

            if (dialog.ShowDialog() == true)
            {
                var sb = new StringBuilder();

                sb.AppendLine("=== RECHNUNG ===");
                sb.AppendLine($"Name: {FirstName} {LastName}");
                sb.AppendLine($"Email: {Email}");
                sb.AppendLine("");

                foreach (var item in _mainVM.CartVM.Items)
                {
                    sb.AppendLine($"{item.Product.Name} x{item.Quantity} - {item.UnitPrice * item.Quantity} €");
                }

                sb.AppendLine("");
                sb.AppendLine($"Gesamt: {TotalPrice} €");

                // Datei speichern
                File.WriteAllText(dialog.FileName, sb.ToString());

                // Datei automatisch öffnen
                Process.Start(new ProcessStartInfo
                {
                    FileName = dialog.FileName,
                    UseShellExecute = true
                });
            }
        }
    }
}