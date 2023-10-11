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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Client.ViewModel;
using Client.Views.Controls;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;
      
        private OrderControl OrderControl { get; set; }
        private AdminControl AdminControl { get; set; }
        private UserCabinet UserCabinet { get; set; }
        public MainWindow(MainViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
            DataContext = viewModel;
            Loaded += MainWindow_Loaded;

         
            OrderControl = new OrderControl();
            AdminControl = new AdminControl();
            UserCabinet = new UserCabinet();
            
            MainContentController.Content = AdminControl;
        }
        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
            
        }

        private void MenuClick(object sender, RoutedEventArgs e)
        {
            string currentTag = (sender as Button).Tag.ToString();
            switch (currentTag)
            {
                case ("Page1"):
                    MainContentController.Content = AdminControl;
                    break;
                case ("Page2"):
                    MainContentController.Content = OrderControl;
                    break;
                case ("Page3"):
                    MainContentController.Content = UserCabinet;
                    break;
            }
        }
    }
}