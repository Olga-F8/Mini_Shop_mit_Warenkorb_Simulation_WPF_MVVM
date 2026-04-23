using Mini_Shop_mit_Warenkorb_Simulation_WPF.Helpers;
using Mini_Shop_mit_Warenkorb_Simulation_WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Mini_Shop_mit_Warenkorb_Simulation_WPF.ViewModels
{
    public class ProductListViewModel : BaseViewModel
    {
        private MainViewModel _mainVM;

        public ObservableCollection<Product> Products { get; set; }

        public RelayCommand OpenDetailCommand { get; set; }

        public ProductListViewModel(MainViewModel mainVM)
        {
            _mainVM = mainVM;

            OpenDetailCommand = new RelayCommand(p =>
            {
                _mainVM.CurrentView = new ProductDetailViewModel(_mainVM, (Product)p);
            });
        }
    }
}
