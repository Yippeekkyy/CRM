using System.Windows;
using Client.Base;
using Client.Commands;

namespace Client.ViewModel;

public class MainViewModel : BaseViewModel
{
    public TriggerCommand SomeCommand { get; set; }    
    
    
    
    public static MainViewModel _instance;
    public static MainViewModel GetInstance()
    {
        if (_instance == null)
        {
            _instance = new MainViewModel();
        }
        return _instance;
    }

    public MainViewModel()
    {
        InitializeCommands();
    }

    private void InitializeCommands()
    {
        SomeCommand = new TriggerCommand(HandleSomeCommand);
        
        
        
    }
    


    private void HandleSomeCommand()
    {
        MessageBox.Show("Hello World");
    }
}