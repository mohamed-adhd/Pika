namespace live_message_app.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
public partial class MainMenuViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _main;

    public MainMenuViewModel(MainWindowViewModel main)
    {
        _main = main;
    }
    
    
    [RelayCommand]
    private void GoToLogin()
    {
        _main.Currentpage = new LoginViewModel(_main);
    }
    
}


