using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Mini_Shop_mit_Warenkorb_Simulation_WPF.Models
{
    public class OrderItem : INotifyPropertyChanged
    {
        private int _quantity;

        public Product Product { get; set; }
        public decimal UnitPrice { get; set; }

        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
                OnPropertyChanged(nameof(Total));
            }
        }

        public decimal Total => Quantity * UnitPrice;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
