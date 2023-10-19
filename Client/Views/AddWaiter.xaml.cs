using System;
using System.Windows;
using Client.ViewModel;

namespace Client.Views;

public partial class AddWaiter : Window
{
    public AddWaiter(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
    
    private void Close_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}