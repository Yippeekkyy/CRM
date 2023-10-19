using System;
using System.Net.Http;
using Client.Base;
using Client.Commands;
using Client.Options;
using Common.HandleResponses;
using Microsoft.Extensions.Options;
using MyCRM.Authorize.Responses;
using MyCRM.Model;

namespace Client.ViewModel;

public class AuthorizeViewModel : BaseViewModel
{
    public event Action UpdateMainWindow;
    
    private readonly BackendOptions _options;
    private readonly HttpClient _httpClient;
    
    public LoginRequest LoginRequest { get; set; } = new();
    public TriggerCommand LoginCommand { get; set; }
    public TriggerCommand LogoutCommand { get; set; }
    
    
    
    public AuthorizeViewModel(IOptions<BackendOptions> options,IHttpClientFactory clientFactory)
    {
        _options = options.Value;
        _httpClient = clientFactory.CreateClient("HttpClient");
        InitializeCommands();
        
#if DEBUG
          
        SelectedUser = new User() {
            FirstName = "Guest",
            LastName = "Guest",
            Role = new UserRole(){Id = 1, Role = RoleType.Admin}};
                    
        Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiMSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwibmJmIjoxNjk3Njc2NzU4LCJleHAiOjIwMTMyOTU5NTh9.fXzK7w4KdS1poYiAPer_4Jmmx41i6CZF8fr2yN0PuHY";
         
#endif
        
    }
    private void InitializeCommands()
    {
        LoginCommand = new TriggerCommand(Login);
        LogoutCommand = new TriggerCommand(Logout);
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
    
    private void Logout()
    {
        SelectedUser = new User();
        Token = null;
        RaisePropertyChanged(nameof(SelectedUser));
        UpdateMainWindow.Invoke();
    }
}