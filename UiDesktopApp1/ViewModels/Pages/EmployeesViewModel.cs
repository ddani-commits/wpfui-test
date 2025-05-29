using System.Collections.ObjectModel;
using System.Diagnostics;
using UiDesktopApp1.Data;
using UiDesktopApp1.Models;
using UiDesktopApp1.Services;
using Wpf.Ui;

namespace UiDesktopApp1.ViewModels.Pages
{
    public partial class EmployeesViewModel: ObservableObject
    {
        public ObservableCollection<Employee> EmployeesList { get; } = new();

        [ObservableProperty]
        private string firstName = "";

        [ObservableProperty]
        private string lastName = "";

        [ObservableProperty]
        private string email = "";

        [ObservableProperty]
        private string password = "";

        [ObservableProperty]
        private string confirmPassword = "";
        public EmployeesViewModel(INavigationService navigation, IAuthenticationService authenticationService) : base(navigation, authenticationService)
        {
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            using var db = new ApplicationDbContext();
            foreach (var employee in db.Employees)
            {
                EmployeesList.Add(employee);
            }
        }

        [RelayCommand]
        public void SaveEmployees()
        {
            using var db = new ApplicationDbContext();
            foreach (var employee in EmployeesList)
            {
                db.Employees.Update(employee);
            }
            db.SaveChanges();
            Debug.WriteLine("Saved from ViewModel");
        }

        [RelayCommand]
        public void AddEmployee()
        {
            using (var context = new ApplicationDbContext())
            {
                var CurrentEmployee = new Employee(FirstName, LastName, Email);
                context.Employees.Add(CurrentEmployee);
                context.SaveChanges();
                EmployeesList.Add(CurrentEmployee);
                ClearFields();
            }
        }

        public void ClearFields()
        {
            FirstName = "";
            LastName = "";
            Email = "";
            Password = "";
            ConfirmPassword = "";
        }
    }
}
