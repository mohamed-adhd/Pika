using System.Collections.Generic;
using Tmds.DBus.Protocol;
using live_message_app.Services;
using Message = Tmds.DBus.Protocol.Message;

namespace live_message_app.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
public partial class MainMenuViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _main;
    [ObservableProperty]
    private bool is_invite_open = false,is_setting_open = false;
    public readonly database db;

    [ObservableProperty]private List<Messagestruct> messageslist = new();
    [ObservableProperty]private List<user> flist = new();
    [ObservableProperty] private user selectedUser;
    [ObservableProperty] private user selectedChats;

    public MainMenuViewModel(MainWindowViewModel main)
    {
        db = new Services.database();
        _main = main;
        Messageslist = db.Fetchmessages(_main.Id);
        Flist = db.Fetchfriends(Messageslist, _main.Id);
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


