namespace Client.ViewModel;

public class DataContexts
{
    public MainViewModel MainViewModel { get; set; }
    public AuthorizeViewModel AuthorizeViewModel { get; set; }

    public DataContexts(MainViewModel mainViewModel, AuthorizeViewModel authorizeViewModel)
    {
        MainViewModel = mainViewModel;
        AuthorizeViewModel = authorizeViewModel;
    }
}