using System;
using System.ComponentModel;
using System.Net.Http;
using Client.Options;
using Microsoft.Extensions.Options;
using MyCRM.Model;

namespace Client.Base;

public abstract class BaseViewModel : INotifyPropertyChanged
{
  //  public event Action UpdateMainWindow;
    
    public User SelectedUser { get; set; } = new();
    public string Token { get; set; } 
    
    public event PropertyChangedEventHandler PropertyChanged;
    public void RaisePropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
}