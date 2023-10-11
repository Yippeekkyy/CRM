using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
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
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using MyCRM.Requests;
using MyCRM.Responses;
using Newtonsoft.Json;

namespace Client.ViewModel;

public class MainViewModel : BaseViewModel
{
    private readonly BackendOptions _options;
    private readonly HttpClient _httpClient;

    public ObservableCollection<GetWaiterResponse> Waiters { get; set; } = new();
    
    public ObservableCollection<GetDishesResponse> Dishes { get; set; } = new();
    

    public AddWaiterRequest AddWaiterRequest { get; set; } = new ();
    public AddDishRequest AddDishRequest { get; set; } = new();
    
    public TriggerCommand SomeCommand { get; set; } 
    public TriggerCommand OpenAddWaiterFormCommand { get; set; }
    public TriggerCommand OpenAddDishFormCommand { get; set; }
    public TriggerCommand AddWaiterCommand { get; set; }
    public TriggerCommand AddDishCommand { get; set; }
    public TriggerCommand OpenEditWaiterFormCommand { get; set; }

    public TriggerCommand<object> DeleteWaiterCommand { get; set; }



    

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
        //OpenEditWaiterFormCommand = new TriggerCommand(HandleOpenEditWaiterForm);
        OpenAddWaiterFormCommand = new TriggerCommand(HandleOpenAddWaiterForm);
        OpenAddDishFormCommand = new TriggerCommand(HandleOpenAddDishForm);
        AddWaiterCommand = new TriggerCommand(HandleAddWaiter);
        AddDishCommand = new TriggerCommand(HandleAddDish);
        DeleteWaiterCommand = new TriggerCommand<object>(HandleDeleteWaiter);
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
    
    //Редактирование Официанта
    private async void HandleOpenEditWaiterForm(object waiter) // Todo Сделать метод
    {
        var DataContext = ((Button)waiter).DataContext;
        if (DataContext is GetWaiterResponse _waiter)
        {
            var win = new EditWaiter();
            win.Show();
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

}