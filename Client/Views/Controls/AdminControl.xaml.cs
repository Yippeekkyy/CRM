using System.Windows;
using System.Windows.Controls;
using Client.ViewModel;

namespace Client.Views.Controls;

public partial class AdminControl : UserControl
{
    private MainViewModel _viewModel;
    public AdminControl(MainViewModel viewModel)
    {
        _viewModel = viewModel;
        InitializeComponent();
        DataContext = viewModel;
    }
    
    private void OpenAddWaiter(object sender, RoutedEventArgs e)
    {
        var win = new AddWaiter(_viewModel);
        win.Show();
    }

<<<<<<< HEAD
    private void OpenAddDish(object sender, RoutedEventArgs e) 
=======
    private void OpenAddDish(object sender, RoutedEventArgs e)
>>>>>>> 85da2092a5d9599e2cb447969e3404b414b36043
    {
        var win = new AddDish(_viewModel);
        win.Show();
    }
}