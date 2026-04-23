using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_Shop_mit_Warenkorb_Simulation_WPF.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
