using System.Windows.Controls;
using Client.ViewModel;

namespace Client.Views.Controls;

public partial class AuthorizationControl : UserControl
{
    private AuthorizeViewModel _viewModel;
    public AuthorizationControl(AuthorizeViewModel viewModel)
    {
        _viewModel = viewModel;
        InitializeComponent();
        DataContext = _viewModel;
    }
}