using System;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using live_message_app.Services;
using live_message_app.Views;
namespace live_message_app.ViewModels;
using static Console;
public partial class LoginViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _main;
    public readonly database db;
    [ObservableProperty] public bool found=true;
    [ObservableProperty] private string username;
    [ObservableProperty] private string password;
    public LoginViewModel(MainWindowViewModel main)
    {
        db = new Services.database();
        _main = main;
    }
    
    
    [RelayCommand]
    private void GoToRegister()
    {
        _main.Currentpage = new RegisterViewModel(_main);
    }
    [RelayCommand]
    private void CheckLogin()
    {
        Console.WriteLine("DEBUG: button was clicked");
        _main.Id = db.check_login(Username, Password);
        if (_main.Id!=-1)
        {
            Found = true;
            _main.Currentpage = new MainMenuViewModel(_main);
        }
        else
        {
            Found = false;
        }
        
    }
}


