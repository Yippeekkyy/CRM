using System.Windows.Controls;
using Client.ViewModel;

namespace Client.Views.Controls;

public partial class AuthorizationControl : UserControl
{
    private MainViewModel _viewModel;
    public AuthorizationControl(MainViewModel viewModel)
    {
        _viewModel = viewModel;
        InitializeComponent();
        DataContext = _viewModel;
    }
}