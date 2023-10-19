using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Client.Base;
using Client.Commands;
using Client.Options;
using Client.Views;
using Common;
using Common.HandleResponses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Options;
using MyCRM.Authorize.Responses;
using MyCRM.Model;
using MyCRM.Requests;
using MyCRM.Responses;
using Newtonsoft.Json;

namespace Client.ViewModel;

public class MainViewModel : BaseViewModel
{
    private readonly BackendOptions _options;
    private readonly HttpClient _httpClient;


    public string Token { get; set; } 
    
    public event Action UpdateMainWindow;

    private GetWaiterResponse _selectedWaiter { get; set; }

   
    public User SelectedUser { get; set; } = new();

    public ObservableCollection<GetWaiterResponse> Waiters { get; set; } = new();

    public ObservableCollection<GetDishesResponse> Dishes { get; set; } = new();
    

    public EditWaiterRequest EditWaiterRequest { get; set; } = new ();
    public AddWaiterRequest AddWaiterRequest { get; set; } = new();
    public AddDishRequest AddDishRequest { get; set; } = new();
    public LoginRequest LoginRequest { get; set; } = new();
    
    
    public TriggerCommand SomeCommand { get; set; } 
    public TriggerCommand OpenAddWaiterFormCommand { get; set; }
    public TriggerCommand OpenAddDishFormCommand { get; set; }
    public TriggerCommand AddWaiterCommand { get; set; }
    public TriggerCommand AddDishCommand { get; set; }
    public TriggerCommand<object> OpenEditWaiterFormCommand { get; set; }

    public TriggerCommand<object> DeleteWaiterCommand { get; set; }

    
    public TriggerCommand LoginCommand { get; set; }
    public TriggerCommand LogoutCommand { get; set; }
    public TriggerCommand EditWaiterCommand { get; set; }



    private MainWindow _mainWindow;
    

    public MainViewModel(IOptions<BackendOptions> options, IHttpClientFactory clientFactory)
    {
        _options = options.Value;
        _httpClient = clientFactory.CreateClient("HttpClient");
        InitializeCommands();
        InitializeData();
    }

    private void InitializeCommands()
    {
        SomeCommand = new TriggerCommand(NewTableSomeCommand);
        OpenEditWaiterFormCommand = new TriggerCommand<object>(HandleOpenEditWaiterForm);
        OpenAddWaiterFormCommand = new TriggerCommand(HandleOpenAddWaiterForm);
        OpenAddDishFormCommand = new TriggerCommand(HandleOpenAddDishForm);
        AddWaiterCommand = new TriggerCommand(HandleAddWaiter);
        AddDishCommand = new TriggerCommand(HandleAddDish);
        DeleteWaiterCommand = new TriggerCommand<object>(HandleDeleteWaiter);

        EditWaiterCommand = new TriggerCommand(HandleEditWaiterCommand);
        LoginCommand = new TriggerCommand(Login);
        LogoutCommand = new TriggerCommand(Logout);
    }


    private async void InitializeData()
    {
        try
        {
#if DEBUG
            LoginRequest = new LoginRequest() { UserId = "1", Password = "1111" };
            SelectedUser = new User() {
                    FirstName = "Guest",
                    LastName = "Guest",
                    Role = new UserRole(){Id = 1, Role = RoleType.Admin}};
                    
            Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiMSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwibmJmIjoxNjk3Njc2NzU4LCJleHAiOjIwMTMyOTU5NTh9.fXzK7w4KdS1poYiAPer_4Jmmx41i6CZF8fr2yN0PuHY";
         
#endif
            
            Waiters = await GetAllWaiters();
            Dishes = await GetAllDishes();
            RaisePropertyChanged(nameof(Waiters));
            RaisePropertyChanged(nameof(Dishes));
        }
        catch (Exception e)
        {
            MessageBox.Show("Ошибка соединения с Сервером");
        }
    }
    private void NewTableSomeCommand()
    {
        NewTableWindow newTableWindow = new NewTableWindow();
        newTableWindow.Show();
    }

    private void HandleOpenAddWaiterForm()
    {
        var win = new AddWaiter(this);
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
        var response = await _httpClient.PostAsJsonAsync(_options.Host + "/api/Admin/Waiter", AddWaiterRequest);

        if (response.IsSuccessStatusCode)
        {
            var responseObj = await ResponseHandler.DeserializeAsync<GetWaiterResponse>(response);
            
            Waiters.Add(responseObj);
        }
    }

    //Удалить Официанта
    private async void HandleDeleteWaiter(object waiter)
    {
        var Datacontext = ((Button)waiter).DataContext;
        if (Datacontext is GetWaiterResponse _waiter)
        {
            var response = await _httpClient.DeleteAsync(_options.Host + $"/api/Admin/Waiter/{_waiter.Id}"); 
            if (response.IsSuccessStatusCode)
            {
                Waiters.Remove(_waiter);
            }
        }
    }
    




    //Получить всех официантов
    private async Task<ObservableCollection<GetWaiterResponse>> GetAllWaiters()
    {
        var response = await _httpClient.GetAsync(_options.Host + "/api/Admin/Waiters");

        var responseObj = await ResponseHandler.DeserializeAsync<ObservableCollection<GetWaiterResponse>>(response);
   
        return responseObj;
    }
    
    
    
    
    //Добавить блюдо
    private async void HandleAddDish() // Todo переписать по образцу с официантами
    {
        await _httpClient.PostAsJsonAsync(_options.Host + "/api/Admin/Dish", AddDishRequest); 
        Dishes = await GetAllDishes();
    }
    
    //Получить все блюда
    private async Task<ObservableCollection<GetDishesResponse>> GetAllDishes()
    {
        var response = await _httpClient.GetAsync(_options.Host + "/api/Admin/Dishes");

        var responseObj = await ResponseHandler.DeserializeAsync<ObservableCollection<GetDishesResponse>>(response);

        return responseObj;
    }

    private async void Login()
    {
        var response = await _httpClient.GetAsync(_options.Host + $"/api/Authorization/Login?UserId={LoginRequest.UserId}&password={LoginRequest.Password}");

        if (response.IsSuccessStatusCode)
        {
            var responseObj = await ResponseHandler.DeserializeAsync<LoginResponse>(response);
            Token = responseObj.Token;
            
            SelectedUser = responseObj.User;
            LoginRequest = new LoginRequest();
            RaisePropertyChanged(nameof(SelectedUser));
            UpdateMainWindow.Invoke();
        }
        
    }
    //Редактирование Официанта
    private  void HandleOpenEditWaiterForm(object waiter) // Todo Сделать метод
    {
        var Datacontext = ((Button)waiter).DataContext;
        if(Datacontext is GetWaiterResponse _waiter) // rework
        {

            EditWaiterRequest.Id = _waiter.Id;
            EditWaiterRequest.FirstName = _waiter.FirstName;
            EditWaiterRequest.LastName = _waiter.LastName;
            EditWaiterRequest.Patronymic = _waiter.Patronymic;
            EditWaiterRequest.Phone = _waiter.Phone;
        }

       
        var win = new EditWaiter(this);
        win.Show();  
    }
    private async void HandleEditWaiterCommand()
    {
        var response = await _httpClient.PutAsJsonAsync(_options.Host + $"/api/Admin/Waiter/{EditWaiterRequest.Id}", EditWaiterRequest);

        if (response.IsSuccessStatusCode)
        {
            var responseObj = await ResponseHandler.DeserializeAsync<GetWaiterResponse>(response);
            
            var objToEdit = Waiters.FirstOrDefault(i => i.Id == responseObj.Id);
            
            if (objToEdit != null)
            {
                int i = Waiters.IndexOf(objToEdit);
                Waiters[i] = responseObj;
            }
        }
    }
    
    private void Logout()
    {
        SelectedUser = new User();
        Token = null;
        RaisePropertyChanged(nameof(SelectedUser));
        UpdateMainWindow.Invoke();
    }
}