using CommunityToolkit.Mvvm.Input;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using live_message_app.Views;
namespace live_message_app.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _main;

    public LoginViewModel(MainWindowViewModel main)
    {
        _main = main;
    }
    
    
    [RelayCommand]
    private void GoToRegister()
    {
        _main.Currentpage = new RegisterViewModel(_main);
    }
    [RelayCommand]
    private void GoToMenu()
    {
        _main.Currentpage = new MainMenuViewModel(_main);
    }
}


