namespace live_message_app.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
public partial class MainMenuViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _main;
    [ObservableProperty]
    private bool is_invite_open = false,is_setting_open = false;

   

    public MainMenuViewModel(MainWindowViewModel main)
    {
        _main = main;
    }
    
    
    [RelayCommand]
    private void GoToLogin()
    {
        _main.Currentpage = new LoginViewModel(_main);
    }

    [RelayCommand]
    private void open_invite()
    {
        Is_invite_open = true;
        Is_setting_open = false;
    }
    [RelayCommand]
    private void close_invite()
    {
        Is_invite_open = false;
    }
    [RelayCommand]
    private void open_setting()
    {
        Is_setting_open = true;
        Is_invite_open = false;
    }
    [RelayCommand]
    private void close_setting()
    {
        Is_setting_open = false;
    }
}


