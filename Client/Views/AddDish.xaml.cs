using Client.ViewModel;
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

namespace Client.Views
{
    /// <summary>
    /// Логика взаимодействия для AddDish.xaml
    /// </summary>
    public partial class AddDish : Window
    {
        public AddDish(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
<<<<<<< HEAD
        }
=======
>>>>>>> 85da2092a5d9599e2cb447969e3404b414b36043

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
