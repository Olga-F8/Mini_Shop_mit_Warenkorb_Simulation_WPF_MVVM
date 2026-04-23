using Mini_Shop_mit_Warenkorb_Simulation_WPF.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_Shop_mit_Warenkorb_Simulation_WPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public BaseViewModel CurrentView { get; set; }

        public ProductListViewModel ProductListVM { get; set; }
        public CartViewModel CartVM { get; set; }

        public RelayCommand ShowProductsCommand { get; set; }
        public RelayCommand ShowCartCommand { get; set; }

        public MainViewModel()
        {
            ProductListVM = new ProductListViewModel(this);
            CartVM = new CartViewModel(this);

            CurrentView = ProductListVM;

            ShowProductsCommand = new RelayCommand(_ => CurrentView = ProductListVM);
            ShowCartCommand = new RelayCommand(_ => CurrentView = CartVM);
        }
    }
}
