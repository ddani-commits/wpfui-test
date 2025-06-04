using System.Diagnostics;
using System.Windows.Controls;
using Wpf.Ui.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace UiDesktopApp1.Controls
{
    /// <summary>
    /// Lógica de interacción para NewSupplierContentDialog.xaml
    /// </summary>
    public partial class NewSupplierContentDialog : ContentDialog
    {
        private string _name = string.Empty;
        private string _contactName = string.Empty;
        private string _address = string.Empty;
        private string _email = string.Empty;
        private string _phone = string.Empty;
        
        public string NameText
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }
        public string ContactNameText
        {
            get => _contactName;
            set { _contactName = value; OnPropertyChanged(); }
        }
        public string AddressText
        {
            get => _address;
            set { _address = value; OnPropertyChanged(); }
        }
        public string EmailText
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }
        public string PhoneText
        {
            get => _phone;
            set { _phone = value; OnPropertyChanged(); }
        }
        private readonly Action<Models.Supplier>? _saveSupplier;
        public NewSupplierContentDialog(ContentPresenter? contentPresenter, Action<Models.Supplier>? saveSuppliers = null)
        {
            InitializeComponent();
            _saveSupplier = saveSuppliers;
            DataContext = this;
        }

        protected override void OnButtonClick(ContentDialogButton button)
        {
            if (button == ContentDialogButton.Primary)
            {
                var supplier = new Models.Supplier(NameText, ContactNameText, AddressText, EmailText, PhoneText);
                _saveSupplier?.Invoke(supplier);
                base.OnButtonClick(button);
                Debug.WriteLine("Primary button clicked");
            }
            else if (button == ContentDialogButton.Close)
            {
                base.OnButtonClick(button);
                Debug.WriteLine("Close button clicked");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
}
