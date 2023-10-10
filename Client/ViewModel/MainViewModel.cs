using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Client.Base;
using Client.Commands;
using Client.Views;
using MyCRM.Requests;
using MyCRM.Responses;
using Newtonsoft.Json;

namespace Client.ViewModel;

public class MainViewModel : BaseViewModel
{
    private List<GetWaitersResponse> waiters;
    

    public List<GetWaitersResponse> Waiters
    {
        get => waiters;
        set
        {
            waiters = value;
            RaisePropertyChanged(nameof(Waiters));
        }
    }
    private List<GetDishesResponse> dishes;

    public List<GetDishesResponse> Dishes
    {
        get => dishes;
        set
        {
            dishes = value;
            RaisePropertyChanged(nameof(Dishes));
        }
    }

    public AddWaiterRequest AddWaiterRequest { get; set; } = new ();
    public AddDishRequest AddDishRequest { get; set; } = new();
    
    public TriggerCommand SomeCommand { get; set; } 
    public TriggerCommand OpenAddWaiterFormCommand { get; set; }
    public TriggerCommand OpenAddDishFormCommand { get; set; }
    public TriggerCommand AddWaiterCommand { get; set; }
    public TriggerCommand AddDishCommand { get; set; }
    public TriggerCommand OpenEditWaiterFormCommand { get; set; }

    public TriggerCommand<object> DeleteWaiterCommand { get; set; }




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
        InitializeViewModelAsync();
    }

    private void InitializeCommands()
    {
        SomeCommand = new TriggerCommand(NewTableSomeCommand);
        //OpenEditWaiterFormCommand = new TriggerCommand(HandleOpenEditWaiterForm);
        OpenAddWaiterFormCommand = new TriggerCommand(HandleOpenAddWaiterForm);
        OpenAddDishFormCommand = new TriggerCommand(HandleOpenAddDishForm);
        AddWaiterCommand = new TriggerCommand(HandleAddWaiter);
        AddDishCommand = new TriggerCommand(HandleAddDish);
        DeleteWaiterCommand = new TriggerCommand<object>(HandleDeleteWaiter);
    }
    

    private async void InitializeViewModelAsync()
    {
        Waiters = await GetAllWaiters();
        Dishes = await GetAllDishes();

    }
    private void NewTableSomeCommand()
    {
        NewTableWindow newTableWindow = new NewTableWindow();
        newTableWindow.Show();
    }

    private void HandleOpenAddWaiterForm()
    {
        var win = new AddWaiter();
        win.Show();
    }
    private void HandleOpenAddDishForm()
    {
        var win = new AddDish();
        win.Show();
    }

    //Добавить официанта
    private async void HandleAddWaiter()
    {
        var httpClient = new HttpClient();
    
        await httpClient.PostAsJsonAsync("http://localhost:5000/api/Admin1/CreateWaiter", AddWaiterRequest); 
        Waiters = await GetAllWaiters();
    }

    private async void HandleAddDish()
    {
        var httpClient = new HttpClient();

        await httpClient.PostAsJsonAsync("http://localhost:5000/api/Admin1/CreateDish", AddDishRequest);
        Dishes = await GetAllDishes();
    }
    


    //Удалить Официанта
    private async void HandleDeleteWaiter(object waiter)
    {
        var Datacontext = ((Button)waiter).DataContext;
        if (Datacontext is GetWaitersResponse _waiter)
        {
            var httpClient = new HttpClient();
            await httpClient.DeleteAsync($"http://localhost:5000/api/Admin1/DeleteWaiter/{_waiter.Id}"); // ToDo: вынесли localhost в appsettigs
            Waiters = await GetAllWaiters();
        }
    }

    

    //Редактирование Официанта
    private async void HandleOpenEditWaiterForm(object waiter)
    {
        var DataContext = ((Button)waiter).DataContext;
        if (DataContext is GetWaitersResponse _waiter)
        {
            var win = new EditWaiter();
            win.Show();
        }
    }


    //Получить всех официантов
    private async Task<List<GetWaitersResponse>> GetAllWaiters()
    {
        var httpClient = new HttpClient();

        var response = await httpClient.GetAsync("http://localhost:5000/api/Admin1/GetWaiters");

        var responseJson = await response.Content.ReadAsStringAsync();

        var responseObj = JsonConvert.DeserializeObject<List<GetWaitersResponse>>(responseJson);
        
        return responseObj;
    }
    private async Task<List<GetDishesResponse>> GetAllDishes()
    {
        var httpClient = new HttpClient();

        var response = await httpClient.GetAsync("http://localhost:5000/api/Admin1/GetDishes");

        var responseJson = await response.Content.ReadAsStringAsync();

        var responseObj = JsonConvert.DeserializeObject<List<GetDishesResponse>>(responseJson);

        return responseObj;
    }

}