using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Client.Base;
using Client.Commands;

namespace Client.ViewModel;

public class MainViewModel : BaseViewModel,INotifyPropertyChanged
{
    public TriggerCommand SomeCommand { get; set; }

    private TriggerCommand _showCanvasCommand;

    public TriggerCommand ShowCanvasCommand
    {
        get => _showCanvasCommand;
        set => _showCanvasCommand = value;
    }

    private Visibility _canvasVisibility;
    
    public Visibility CanvasVisibility
    {
        get { return _canvasVisibility; }
        set
        {
            _canvasVisibility = value;
            OnPropertyChanged(nameof(CanvasVisibility));
        }
    }

    
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
        SomeCommand = new TriggerCommand(NewTableSomeCommand);
        ShowCanvasCommand = new TriggerCommand(ShowCanvas);
        CanvasVisibility = Visibility.Collapsed;
        
    }
    


    private void NewTableSomeCommand()
    {
        NewTableWindow newTableWindow = new NewTableWindow();
        newTableWindow.Show();
    }

    private void ShowCanvas()
    {
        
        CanvasVisibility = Visibility.Visible;
    }
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}