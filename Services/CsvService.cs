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
        
        private readonly string customerFilePath = "Data/customers.csv";
        public List<Product> LoadProducts()
        {
            var products = new List<Product>();

            var lines = File.ReadLines(filePath).Skip(1);

            foreach (var (line, idx) in lines.Select((l, i) => (l, i + 2)))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(';');
                if (parts.Length < 6) continue; // ❗ jetzt 6 Spalten

                if (!int.TryParse(parts[0].Trim(), out var id)) continue;

                if (!decimal.TryParse(parts[2].Trim(),
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out var price)) continue;

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
                    ImageUrl = imagePath,
                    Description = parts[5].Trim()
                });
            }

            return products;
        }

        public List<Customer> LoadCustomers()
        {
            var customers = new List<Customer>();

            var lines = File.ReadLines(customerFilePath).Skip(1);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(';');
                if (parts.Length < 4) continue;

                if (!int.TryParse(parts[0], out var id)) continue;

                customers.Add(new Customer
                {
                    Id = id,
                    FirstName = parts[1].Trim(),
                    LastName = parts[2].Trim(),
                    Email = parts[3].Trim()
                });
            }

            return customers;
        }

        public void AddCustomer(Customer customer)
        {
            // existierende Kunden laden
            var customers = LoadCustomers();

            // prüfen ob Kunde schon existiert (z. B. über Email)
            if (customers.Any(c => c.Email == customer.Email))
                return;

            // neue ID vergeben
            int newId = customers.Any() ? customers.Max(c => c.Id) + 1 : 1;
            customer.Id = newId;

            // Zeile anhängen
            var line = $"{customer.Id};{customer.FirstName};{customer.LastName};{customer.Email}";
            var fullPath = Path.Combine(
    AppDomain.CurrentDomain.BaseDirectory,
    "Data",
    "customers.csv"
);
            File.AppendAllText(fullPath, Environment.NewLine + line);
        }

        public void SaveProducts(List<Product> products)
        {
            var lines = new List<string>
            {
                "Id;Name;Price;Category;ImageUrl;Description" // ❗ Header erweitert
            };

            foreach (var p in products)
            {
                var relativePath = p.ImageUrl
                    .Replace(AppDomain.CurrentDomain.BaseDirectory, "")
                    .TrimStart('\\');

                lines.Add($"{p.Id};{p.Name};{p.Price};{p.Category};{relativePath};{p.Description}");
            }

            File.WriteAllLines(filePath, lines);
        }
    }
}