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
}