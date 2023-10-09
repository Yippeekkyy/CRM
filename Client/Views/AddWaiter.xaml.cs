using System.Windows;
using Client.ViewModel;

namespace Client.Views;

public partial class AddWaiter : Window
{
    public AddWaiter()
    {
        InitializeComponent();
        DataContext = MainViewModel.GetInstance();
    }
}