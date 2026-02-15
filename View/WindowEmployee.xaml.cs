using Lab_4_TokarevD.N._БПИ_23_01.Helper;
using Lab_4_TokarevD.N._БПИ_23_01.Model;
using Lab_4_TokarevD.N._БПИ_23_01.ViewModel;
using Lab_4_TokarevD.N._БПИ_23_01.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab_4_TokarevD.N._БПИ_23_01.View
{

    public partial class WindowEmployee : Window
    {
        public WindowEmployee()
        {
            InitializeComponent();
            DataContext = new PersonViewModel();
        }
    }


}
