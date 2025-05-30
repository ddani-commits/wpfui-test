﻿using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Xml.Linq;
using UiDesktopApp1.Data;
using UiDesktopApp1.Services;
using Wpf.Ui;

namespace UiDesktopApp1.ViewModels.Pages
{
    public partial class SuppliersViewModel: ObservableObject
    {
        [ObservableProperty]
        private Models.Supplier? selectedSupplier;
        [ObservableProperty]
        private string name = string.Empty;
        [ObservableProperty]
        private string contactName = string.Empty;
        [ObservableProperty]
        private string address = string.Empty;
        [ObservableProperty]
        private string email = string.Empty;
        [ObservableProperty]
        private string phone = string.Empty;
        public ObservableCollection<Models.Supplier> SuppliersList { get; } = new();
        public SuppliersViewModel()
        {
            LoadSuppliers();
        }
        public void LoadSuppliers()
        {
            using var db = new ApplicationDbContext();
            foreach (var supplier in db.Suppliers)
            {
                supplier.ViewModel = this; // Esto permite acceder al comando desde XAML
                SuppliersList.Add(supplier);

            }
        }

        //Añadir
        [RelayCommand]
        public void AddSupplier()
        {
            using (var context = new ApplicationDbContext())
            {
                var currentSupplier = new Models.Supplier(Name, ContactName, Address, Email, Phone);
                context.Suppliers.Add(currentSupplier);
                context.SaveChanges();
                SuppliersList.Add(currentSupplier);
                ClearFields();
            }
        }

        // Guardar
        [RelayCommand]
        public void SaveSupplier()
        {
            using var db = new ApplicationDbContext();
            foreach (var supplier in SuppliersList)
            {
                db.Suppliers.Update(supplier);
            }
            db.SaveChanges();
            Debug.WriteLine("Saved from ViewModel");
        }

        // Eliminar
        [RelayCommand]
        public void DeleteSupplier(Models.Supplier supplier)
        {
            if (supplier is null) return;

            using var db = new ApplicationDbContext();
            var supplierToDelete = db.Suppliers.Find(supplier.SupplierId);

            if (supplierToDelete != null)
            {
                db.Suppliers.Remove(supplierToDelete);
                db.SaveChanges();
                SuppliersList.Remove(supplier);

                if (SelectedSupplier?.SupplierId == supplier.SupplierId)
                    SelectedSupplier = null;
            }
            else
            {
                Debug.WriteLine("Supplier not found in database.");
            }
        }
        public void ClearFields()
        {
            Name = string.Empty;
            ContactName = string.Empty;
            Address = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;

        }
    }
}