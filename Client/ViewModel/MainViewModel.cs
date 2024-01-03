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
using Azure;
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

    private GetWaiterResponse _selectedWaiter { get; set; }

   
    

    public ObservableCollection<GetWaiterResponse> Waiters { get; set; } = new();

    public ObservableCollection<GetDishResponse> Dishes { get; set; } = new();
    

    public EditWaiterRequest EditWaiterRequest { get; set; } = new ();
    public AddWaiterRequest AddWaiterRequest { get; set; } = new();
    public AddDishRequest AddDishRequest { get; set; } = new();
    
    
    
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
    

    public MainViewModel(IOptions<BackendOptions> options,IHttpClientFactory clientFactory) 
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
    }


    private async void InitializeData()
    {
        try
        {

            
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
        var win = new AddDish(this);
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
    private async void HandleAddDish() 
    {
        var response = await _httpClient.PostAsJsonAsync(_options.Host + "/api/Kitchen/Dish", AddDishRequest);
        if (response.IsSuccessStatusCode)
        {
            var responseObj = await ResponseHandler.DeserializeAsync<GetDishResponse>(response);

            Dishes.Add(responseObj);
        }
    }
    
    //Получить все блюда
    private async Task<ObservableCollection<GetDishResponse>> GetAllDishes()
    {
        var response = await _httpClient.GetAsync(_options.Host + "/api/Kitchen/Dishes");

        var responseObj = await ResponseHandler.DeserializeAsync<ObservableCollection<GetDishResponse>>(response);

        return responseObj;
        
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
}