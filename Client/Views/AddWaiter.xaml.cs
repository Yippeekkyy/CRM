using System.Windows;
using Client.ViewModel;

namespace Client.Views;

public partial class AddWaiter : Window
{
    private MainViewModel _viewModel;
    public AddWaiter(MainViewModel viewModel)
    {
        _viewModel = viewModel;
        InitializeComponent();
        DataContext = viewModel;
    }
}