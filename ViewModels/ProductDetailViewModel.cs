using Mini_Shop_mit_Warenkorb_Simulation_WPF.Helpers;
using Mini_Shop_mit_Warenkorb_Simulation_WPF.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Shop_mit_Warenkorb_Simulation_WPF.ViewModels
{
    public class ProductDetailViewModel : BaseViewModel
    {
        private MainViewModel _mainVM;

        public Product Product { get; set; }
       
        private int _quantity = 1;
        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity)); 
            }
        }
        public RelayCommand AddToCartCommand { get; set; }
        public RelayCommand BackCommand { get; set; }
        public RelayCommand IncreaseQuantityCommand { get; set; }
        public RelayCommand DecreaseQuantityCommand { get; set; }

        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        public ProductDetailViewModel(MainViewModel mainVM, Product product)
        {
            _mainVM = mainVM;
            Product = product;
            var window = System.Windows.Application.Current.MainWindow;
            window.Width = 900;
            window.Height = 600;
            AddToCartCommand = new RelayCommand(async _ =>
            {
                _mainVM.CartVM.AddToCart(Product, Quantity);

                Message = "Artikel wurde zum Warenkorb hinzugefügt!";

                await Task.Delay(1500);

                Message = "";
                Quantity = 1;
            });

            BackCommand = new RelayCommand(_ =>
            {
                _mainVM.CurrentView = _mainVM.ProductListVM;
                var window = System.Windows.Application.Current.MainWindow;
                window.Width = 900;
                window.Height = 800;
            });

            IncreaseQuantityCommand = new RelayCommand(_ =>
            {
                Quantity++;
            });

            DecreaseQuantityCommand = new RelayCommand(_ =>
            {
                if (Quantity > 1)
                    Quantity--;
            });
        }
    }
}
