using Mini_Shop_mit_Warenkorb_Simulation_WPF.Helpers;
using Mini_Shop_mit_Warenkorb_Simulation_WPF.Models;
using Mini_Shop_mit_Warenkorb_Simulation_WPF.Services;
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
            
            var service = new CsvService();
            var products = service.LoadProducts();
            _mainVM = mainVM;

            Products = new ObservableCollection<Product>(products);

            OpenDetailCommand = new RelayCommand(p =>
            {
                var product = p as Product;
                if (product == null) return;

                _mainVM.CurrentView = new ProductDetailViewModel(_mainVM, product);
            });
        }
    }
}
