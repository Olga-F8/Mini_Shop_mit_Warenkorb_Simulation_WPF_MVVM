using Mini_Shop_mit_Warenkorb_Simulation_WPF.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_Shop_mit_Warenkorb_Simulation_WPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private BaseViewModel _currentView;

        public BaseViewModel CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public ProductListViewModel ProductListVM { get; set; }
        public CartViewModel CartVM { get; set; }
        public CheckoutViewModel CheckoutVM { get; set; }
        public RelayCommand ShowCheckoutCommand { get; set; }

        public RelayCommand ShowProductsCommand { get; set; }
        public RelayCommand ShowCartCommand { get; set; }

        public MainViewModel()
        {
            ProductListVM = new ProductListViewModel(this);
            CartVM = new CartViewModel(this);
            CheckoutVM = new CheckoutViewModel(this);

            ShowCheckoutCommand = new RelayCommand(_ =>
            {
                CurrentView = CheckoutVM;
            });

            CurrentView = ProductListVM;

            ShowProductsCommand = new RelayCommand(_ =>
            {
                CurrentView = ProductListVM;
            });

            ShowCartCommand = new RelayCommand(_ =>
            {
                CurrentView = CartVM;
            });
        }
    }
}
