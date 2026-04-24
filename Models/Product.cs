using System;
using System.Collections.Generic;
using System.Text;

namespace Mini_Shop_mit_Warenkorb_Simulation_WPF.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
    }
}
