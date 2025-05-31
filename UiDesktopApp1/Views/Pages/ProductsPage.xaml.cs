using UiDesktopApp1.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;

namespace UiDesktopApp1.Views.Pages
{
    public partial class ProductsPage : INavigableView<ProductsViewModel>
    {
        public ProductsViewModel ViewModel { get; }
        public ProductsPage(ProductsViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = ViewModel;
            InitializeComponent();
        }
    }
}
