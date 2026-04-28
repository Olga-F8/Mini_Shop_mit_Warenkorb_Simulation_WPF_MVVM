using Mini_Shop_mit_Warenkorb_Simulation_WPF.Helpers;
using Mini_Shop_mit_Warenkorb_Simulation_WPF.ViewModels;
using System.IO;
using System.Text;
using Microsoft.Win32;
using System.Diagnostics;

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

        public CheckoutViewModel(MainViewModel mainVM)
        {
            _mainVM = mainVM;

            BackCommand = new RelayCommand(_ =>
            {
                _mainVM.CurrentView = _mainVM.CartVM;
            });

            CreateInvoiceCommand = new RelayCommand(_ =>
            {
                CreateInvoice();
            });
        }

        private void CreateInvoice()
        {
            var dialog = new SaveFileDialog
            {
                FileName = "Rechnung.txt",
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