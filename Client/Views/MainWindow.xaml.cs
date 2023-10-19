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
using Client.Views;
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
        private AuthorizeViewModel _authorizeViewModel;
      
        private AuthorizationControl AuthorizationControl { get; set; }
        private OrderControl OrderControl { get; set; }
        private AdminControl AdminControl { get; set; }
        private UserCabinetControl UserCabinet { get; set; }
        private AccessDenied AccessDenied { get; set; }

    
        public string currentTag { get; set; }
        
        public MainWindow(MainViewModel viewModel,AuthorizeViewModel authorizeViewModel)
        {
            _authorizeViewModel = authorizeViewModel;
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = new DataContexts(viewModel,authorizeViewModel);
            
            AuthorizationControl = new AuthorizationControl(_authorizeViewModel);
            OrderControl = new OrderControl();
            AdminControl = new AdminControl(_viewModel);
            UserCabinet = new UserCabinetControl(_authorizeViewModel);
            
            authorizeViewModel.UpdateMainWindow += Update;
            Update();
        }
       

        public void Update()
        {
            if (_authorizeViewModel.Token == null)
            {
                MainContentController.Content = new AuthorizationControl(_authorizeViewModel);
                 return;
            }
            
            switch (currentTag)
            {
                case ("Page1"):
                    if (_authorizeViewModel.SelectedUser.Role.Role == RoleType.Admin)
                    {
                        MainContentController.Content = AdminControl;
                        break;
                    }
                    MainContentController.Content = new AccessDenied();
                    break;
                
                case ("Page2"):
                    if (_authorizeViewModel.SelectedUser.Role.Role == RoleType.Admin || _authorizeViewModel.SelectedUser.Role.Role == RoleType.Waiter)
                    {
                        MainContentController.Content = OrderControl;
                        break;
                    }
                    MainContentController.Content = new AccessDenied();
                    break;
                
                case ("Page3"):
                        MainContentController.Content = new UserCabinetControl(_authorizeViewModel);
                    break;
                default:
                    MainContentController.Content = new UserCabinetControl(_authorizeViewModel);
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