using Lab_4_TokarevD.N._БПИ_23_01.Helper;
using Lab_4_TokarevD.N._БПИ_23_01.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lab_4_TokarevD.N._БПИ_23_01.ViewModel
{
    public class MainViewModel
    {
        public ICommand OpenEmployeesCommand { get; }
        public ICommand OpenRolesCommand { get; }

        public MainViewModel()
        {
            OpenEmployeesCommand = new RelayCommand(_ => OpenEmployees());
            OpenRolesCommand = new RelayCommand(_ => OpenRoles());
        }

        private void OpenEmployees()
        {
            var window = new WindowEmployee();
            window.ShowDialog();
        }

        private void OpenRoles()
        {
            var window = new WindowRole();
            window.ShowDialog();
        }
    }
}
