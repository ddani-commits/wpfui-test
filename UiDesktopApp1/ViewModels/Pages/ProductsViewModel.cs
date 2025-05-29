using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UiDesktopApp1.Data;
using UiDesktopApp1.Models;
using UiDesktopApp1.Services;
using Wpf.Ui;

namespace UiDesktopApp1.ViewModels.Pages
{
    public partial class ProductsViewModel: ObservableObject
    {
        [ObservableProperty]
        private string _productName = "";
        [ObservableProperty]
        private bool _isActive = true;
        [ObservableProperty]
        private string _barcode = "";
        [ObservableProperty]
        private string _SKU = "";
        public ObservableCollection<Product> ProductsList { get; } = new();
        public ProductsViewModel(INavigationService navigation, IAuthenticationService authenticationService) : base(navigation, authenticationService)
        {
            LoadProducts();
        }

        private void LoadProducts()
        {
            using var db = new ApplicationDbContext();
            foreach (var product in db.Products)
            {
                ProductsList.Add(product);
            }
        }

        [RelayCommand]
        public void AddProduct()
        {
            using (var context = new ApplicationDbContext())
            {
                var CurrentProduct = new Product(ProductName, IsActive, Barcode, SKU);
                context.Products.Add(CurrentProduct);
                context.SaveChanges();
                ProductsList.Add(CurrentProduct);
            }
            Console.WriteLine("Add Product command executed.");
            ClearFields();
        }

        [RelayCommand]
        public void SaveProducts()
        {
            using (var context = new ApplicationDbContext())
            {
                foreach (var product in ProductsList)
                {
                    context.Products.Update(product);
                }
                context.SaveChanges();
            }
        }

        private void ClearFields()
        {
            ProductName = string.Empty;
            IsActive = true;
            Barcode = string.Empty;
            SKU = string.Empty;
        }
    }
}
