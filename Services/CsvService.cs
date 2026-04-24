using System;
using System.Collections.Generic;
using System.Text;
using Mini_Shop_mit_Warenkorb_Simulation_WPF.Models;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Mini_Shop_mit_Warenkorb_Simulation_WPF.Services
{
   

    public class CsvService
    {
        private readonly string filePath = "Data/products.csv";

        public List<Product> LoadProducts()
        {
            var products = new List<Product>();

            // Streamen statt alles in den Speicher laden
            var lines = File.ReadLines(filePath).Skip(1); // Header überspringen

            foreach (var (line, idx) in lines.Select((l, i) => (l, i + 2))) // +2 = echte Dateizeilennummer (optional)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(';');
                if (parts.Length < 5) continue; // oder Logging/Fehlerbericht

                if (!int.TryParse(parts[0].Trim(), out var id)) continue; // oder Logging
                if (!decimal.TryParse(parts[2].Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out var price)) continue;

                products.Add(new Product
                {
                    Id = id,
                    Name = parts[1].Trim(),
                    Price = price,
                    Category = parts[3].Trim(),
                    ImageUrl = parts[4].Trim()
                });
            }

            return products;
        }

        public void SaveProducts(List<Product> products)
        {
            var lines = new List<string>
        {
            "Id;Name;Price;Category;ImageUrl"
        };

            foreach (var p in products)
            {
                lines.Add($"{p.Id};{p.Name};{p.Price};{p.Category};{p.ImageUrl}");
            }

            File.WriteAllLines(filePath, lines);
        }
    }
}
