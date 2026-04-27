using System;
using System.Collections.Generic;
using System.Text;
using Mini_Shop_mit_Warenkorb_Simulation_WPF.Helpers;
using Mini_Shop_mit_Warenkorb_Simulation_WPF.Models;
using System.Collections.ObjectModel;
using System.Linq;
using Mini_Shop_mit_Warenkorb_Simulation_WPF.Views;

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
        public CartViewModel(MainViewModel mainVM)
        {
            _mainVM = mainVM;

            IncreaseCommand = new RelayCommand(async i =>
            {
                ((OrderItem)i).Quantity++;
                Message = "Menge aktualisiert!";
                OnPropertyChanged(nameof(TotalPrice));
                await Task.Delay(1500);
                Message = "";
            });

            DecreaseCommand = new RelayCommand(async i =>
            {
                var item = (OrderItem)i;
                if (item.Quantity > 1)
                    item.Quantity--;
                
                OnPropertyChanged(nameof(TotalPrice));
                Message = "Menge aktualisiert!";
                await Task.Delay(1500);

                Message = "";
            });

            RemoveCommand = new RelayCommand(async i =>
            {
                Items.Remove((OrderItem)i);
                Message = "Artikel entfernt!";
                OnPropertyChanged(nameof(TotalPrice));
                await Task.Delay(1500);
                Message = "";
            });
        }

        public void AddToCart(Product product, int quantity)
        {
            var existingItem = Items.FirstOrDefault(i => i.Product.Id == product.Id);

            if (existingItem != null)
            {
                // Artikel existiert → Menge erhöhen
                existingItem.Quantity += quantity;

                // UI aktualisieren
                OnPropertyChanged(nameof(Items));
            }
            else
            {
                // Neuer Artikel
                Items.Add(new OrderItem
                {
                    Product = product,
                    Quantity = quantity,
                    UnitPrice = product.Price
                });
            }

            OnPropertyChanged(nameof(TotalPrice));
        }
    }
}
