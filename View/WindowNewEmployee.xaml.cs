using Lab_4_TokarevD.N._БПИ_23_01.Model;
using Lab_4_TokarevD.N._БПИ_23_01.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lab_4_TokarevD.N._БПИ_23_01.View
{
    public partial class WindowNewEmployee : Window
    {
        public WindowNewEmployee()
        {
            InitializeComponent();

            
            var roleVm = new RoleViewModel();
            CbRole.ItemsSource = roleVm.ListRole;

            
            if (DataContext is PersonDpo person)
            {
                CbRole.SelectedValue = person.RoleId;
            }
        }

        
        private void tbBirthday_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (tbBirthday.Visibility == Visibility.Hidden)
            {
                clBirthday.Visibility = Visibility.Visible;

                
                if (!string.IsNullOrEmpty(tbBirthday.Text))
                {
                    if (DateTime.TryParseExact(tbBirthday.Text, "dd.MM.yyyy", null,
                        System.Globalization.DateTimeStyles.None, out DateTime date))
                    {
                        clBirthday.SelectedDate = date;
                    }
                }
            }
            else
            {
                clBirthday.Visibility = Visibility.Hidden;
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is PersonDpo person)
            {
                
                if (CbRole.SelectedItem is Role selectedRole)
                {
                    person.RoleId = selectedRole.Id;
                    person.RoleName = selectedRole.NameRole;
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите должность.",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                
                if (string.IsNullOrWhiteSpace(person.Birthday))
                {
                    MessageBox.Show("Пожалуйста, укажите дату рождения.",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                
                if (clBirthday.SelectedDate.HasValue && clBirthday.Visibility == Visibility.Visible)
                {
                    person.Birthday = clBirthday.SelectedDate.Value.ToString("dd.MM.yyyy");
                }

                DialogResult = true;
            }
        }
    }
}
