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

            var lines = File.ReadLines(filePath).Skip(1);

            foreach (var (line, idx) in lines.Select((l, i) => (l, i + 2)))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(';');
                if (parts.Length < 5) continue;

                if (!int.TryParse(parts[0].Trim(), out var id)) continue;

                if (!decimal.TryParse(parts[2].Trim(),
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out var price)) continue;

                // ImagePath absolut setzen
                var imagePath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    parts[4].Trim()
                );

                products.Add(new Product
                {
                    Id = id,
                    Name = parts[1].Trim(),
                    Price = price,
                    Category = parts[3].Trim(),
                    ImageUrl = imagePath
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
                // Speichere wieder RELATIVEN Pfad in CSV
                var relativePath = p.ImageUrl
                    .Replace(AppDomain.CurrentDomain.BaseDirectory, "")
                    .TrimStart('\\');

                lines.Add($"{p.Id};{p.Name};{p.Price};{p.Category};{relativePath}");
            }

            File.WriteAllLines(filePath, lines);
        }
    }
}