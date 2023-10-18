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
using Common;
using MyCRM.Model;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;
      
        private AuthorizationControl AuthorizationControl { get; set; }
        private OrderControl OrderControl { get; set; }
        private AdminControl AdminControl { get; set; }
        private UserCabinetControl UserCabinet { get; set; }
        private AccessDenied AccessDenied { get; set; }

        public string currentTag { get; set; }
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = viewModel;
            
            AuthorizationControl = new AuthorizationControl(_viewModel);
            OrderControl = new OrderControl();
            AdminControl = new AdminControl();
            UserCabinet = new UserCabinetControl(_viewModel);
            
            viewModel.UpdateMainWindow += Update;
            Update();
        }
       

        public void Update()
        {
            if (_viewModel.Token == null)
            {
                MainContentController.Content = new AuthorizationControl(_viewModel);
                 return;
            }
            
            switch (currentTag)
            {
                case ("Page1"):
                    if (_viewModel.SelectedUser.Role.Role == RoleType.Admin)
                    {
                        MainContentController.Content = AdminControl;
                        break;
                    }
                    MainContentController.Content = new AccessDenied();
                    break;
                
                case ("Page2"):
                    if (_viewModel.SelectedUser.Role.Role == RoleType.Admin || _viewModel.SelectedUser.Role.Role == RoleType.Waiter)
                    {
                        MainContentController.Content = OrderControl;
                        break;
                    }
                    MainContentController.Content = new AccessDenied();
                    break;
                
                case ("Page3"):
                    if (_viewModel.Token == null)
                    {
                        MainContentController.Content = new AuthorizationControl(_viewModel);
                    }
                    else
                    {
                        MainContentController.Content = new UserCabinetControl(_viewModel);
                    }
                    break;
                default:
                    MainContentController.Content = new UserCabinetControl(_viewModel);
                    break;
            }
            UpdateLayout();
        }
        private void MenuClick(object sender, RoutedEventArgs e)
        {
            currentTag = (sender as Button).Tag.ToString();
            Update();
        }
    }
}