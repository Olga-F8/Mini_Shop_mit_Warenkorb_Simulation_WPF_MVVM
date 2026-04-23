using System;
using System.Collections.Generic;
using System.Text;
using Mini_Shop_mit_Warenkorb_Simulation_WPF.Helpers;
using Mini_Shop_mit_Warenkorb_Simulation_WPF.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace Mini_Shop_mit_Warenkorb_Simulation_WPF.ViewModels
{


    public class CartViewModel : BaseViewModel
    {
        private MainViewModel _mainVM;

        public ObservableCollection<OrderItem> Items { get; set; } = new();

        public decimal TotalPrice => Items.Sum(i => i.Quantity * i.UnitPrice);

        public RelayCommand IncreaseCommand { get; set; }
        public RelayCommand DecreaseCommand { get; set; }
        public RelayCommand RemoveCommand { get; set; }
        public RelayCommand CheckoutCommand { get; set; }

        public CartViewModel(MainViewModel mainVM)
        {
            _mainVM = mainVM;

            IncreaseCommand = new RelayCommand(i =>
            {
                ((OrderItem)i).Quantity++;
                OnPropertyChanged(nameof(TotalPrice));
            });

            DecreaseCommand = new RelayCommand(i =>
            {
                var item = (OrderItem)i;
                if (item.Quantity > 1)
                    item.Quantity--;
                OnPropertyChanged(nameof(TotalPrice));
            });

            RemoveCommand = new RelayCommand(i =>
            {
                Items.Remove((OrderItem)i);
                OnPropertyChanged(nameof(TotalPrice));
            });
        }

        public void AddToCart(Product product, int quantity)
        {
            Items.Add(new OrderItem
            {
                Product = product,
                Quantity = quantity,
                UnitPrice = product.Price
            });

            OnPropertyChanged(nameof(TotalPrice));
        }
    }
}
