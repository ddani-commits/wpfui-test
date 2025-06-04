using System.Collections.ObjectModel;
using System.Diagnostics;
using UiDesktopApp1.Data;
using UiDesktopApp1.Models;
using Wpf.Ui;
using UiDesktopApp1.Controls;
using System.Windows.Controls;

namespace UiDesktopApp1.ViewModels.Pages
{
    public partial class CategoryViewModel : ViewModel
    {
        [ObservableProperty]
        private Models.Category? selectedCategory;

        [ObservableProperty]
        private string categoryName = string.Empty;

        [ObservableProperty]
        private string parentCategoryName = string.Empty;

        public ObservableCollection<Models.Category> CategoriesList { get; } = new();
        private readonly IContentDialogService _contentDialogService;
        public CategoryViewModel(IContentDialogService contentDialogService)
        {
            _contentDialogService = contentDialogService;
            LoadCategories();
        }
        private void LoadCategories()
        {
            using var db = new ApplicationDbContext();
            foreach (var category in db.Categories)
            {
                category.ViewModel = this;
                CategoriesList.Add(category);
            }
        }

        [RelayCommand]
        private async Task ShowSignInContentDialog()
        {
            if (_contentDialogService.GetDialogHost() is not null)
            {
                // Example of how to open a content dialog, a dialog must be created. examples are in Controls folder
                var newCategoryContentDialog = new NewCategoryContentDialog(_contentDialogService.GetDialogHost(), AddCategory);
                _ = await newCategoryContentDialog.ShowAsync();
            }
            Debug.WriteLine("Show SignIn Content Dialog Command Executed");
        }

        // Añadir
        [RelayCommand]
        public void AddCategory(Models.Category CurrentCategory)
        {
            using (var context = new ApplicationDbContext())
            {               
                context.Categories.Add(CurrentCategory);
                context.SaveChanges();
                CategoriesList.Add(CurrentCategory);
              
            }
        }
        // Guardar

        [RelayCommand]
        public void SaveCategory()
        {
            using var db = new ApplicationDbContext();
            foreach (var category in CategoriesList)
            {
                db.Categories.Update(category);
            }
            db.SaveChanges();
            Debug.WriteLine("Saved from ViewModel");
        }

        // Elliminar
        [RelayCommand]
        public void DeleteCategory(Models.Category category)
        {
            if (category is null) return;

            using var db = new ApplicationDbContext();
            var categoryToDelete = db.Categories.Find(category.CategoryId);

            if (categoryToDelete != null)
            {
                db.Categories.Remove(categoryToDelete);
                db.SaveChanges();
                CategoriesList.Remove(category);

                if (SelectedCategory?.CategoryId == category.CategoryId)
                    SelectedCategory = null;
            }
            else
            {
                Debug.WriteLine("Category not found.");
            }
        }
    
    }
}
