using Mini_Shop_mit_Warenkorb_Simulation_WPF.Helpers;
using Mini_Shop_mit_Warenkorb_Simulation_WPF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_Shop_mit_Warenkorb_Simulation_WPF.ViewModels
{
    public class ProductDetailViewModel : BaseViewModel
    {
        private MainViewModel _mainVM;

        public Product Product { get; set; }
        public int Quantity { get; set; } = 1;

        public RelayCommand AddToCartCommand { get; set; }
        public RelayCommand BackCommand { get; set; }

        public ProductDetailViewModel(MainViewModel mainVM, Product product)
        {
            _mainVM = mainVM;
            Product = product;

            AddToCartCommand = new RelayCommand(_ =>
            {
                _mainVM.CartVM.AddToCart(Product, Quantity);
            });

            BackCommand = new RelayCommand(_ =>
            {
                _mainVM.CurrentView = _mainVM.ProductListVM;
            });
        }
    }
}
