using System.Windows.Controls;
using Client.ViewModel;

namespace Client.Views.Controls;

public partial class UserCabinetControl : UserControl
{
    public UserCabinetControl(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}