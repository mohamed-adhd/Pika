using System.Collections.Generic;
using System.Net.Sockets;
using Tmds.DBus.Protocol;
using live_message_app.Services;
using static System.Console;
namespace live_message_app.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using live_message_app.Views;
using Avalonia.Threading;
public partial class MainMenuViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _main;
    public Views.MainMenu _men;
    [ObservableProperty]
    private bool is_invite_open = false,is_setting_open = false;

    [ObservableProperty] private List<invites> pendingInvites;
    [ObservableProperty]private List<Messagestruct> messageslist = new();
    [ObservableProperty]private List<user> flist = new();
    [ObservableProperty] private user? selectedUser;
    [ObservableProperty] private List<Messagestruct> selectedMessages=new();
    [ObservableProperty] private string messageToSend="";
    [ObservableProperty] private string inviteUsername;
    
    partial void OnSelectedUserChanged(user? value)
    {
        WriteLine($"Selected user: {value?.username}");
        if (value == null)
            return;
        
        SelectedMessages = _main.Db.selecmsg(_main.Id,SelectedUser.id);
        WriteLine($"Messages loaded: {SelectedMessages.Count}");        
        MessageToSend = "";

        
    }

    public MainMenuViewModel(MainWindowViewModel main)
    {
        _main = main;
        PendingInvites = _main.Db.fetch_invites(_main.Id);
        Messageslist = _main.Db.Fetchmessages(_main.Id);
        Flist = _main.Db.Fetchfriends(Messageslist, _main.Id);
        _main.PropertyChanged += Main_PropertyChanged;
        
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

    [RelayCommand]
    private void SendInvite()
    {
        packet temps=new();
        temps.Text = InviteUsername;
        temps.Type = "request";
        temps.From = _main.Id;
        temps.To = SelectedUser.id;
        
        _main.network.sendpacket(temps);
        user temp = _main.Db.search_by_username(InviteUsername);
        _main.Db.add_invite(_main.Id, temp.id);
    }
    [RelayCommand]
    private void SendMessage()
    {
        int ord = _main.Db.neword(_main.Id, SelectedUser.id);
        _main.Db.addmsg(MessageToSend,_main.Id,SelectedUser.id,ord);
        packet temp=new();
        temp.Text = MessageToSend;
        temp.Type = "message";
        temp.From = _main.Id;
        temp.To = SelectedUser.id;
        
        _main.network.sendpacket(temp);
        
        SelectedMessages = _main.Db.selecmsg(_main.Id,SelectedUser.id);
        WriteLine($"Messages loaded: {SelectedMessages.Count}");        
        MessageToSend = "";
        
    }
    private void Main_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(MainWindowViewModel.Updates) && 
            e.PropertyName != nameof(MainWindowViewModel.New_invites))
            return;

        Dispatcher.UIThread.Post(() =>
        {
            if (e.PropertyName == nameof(MainWindowViewModel.Updates))
            {
                if (SelectedUser == null) return;
                var p = _main.Updates;
                bool belongsToOpenChat =
                    (p.From == _main.Id && p.To == SelectedUser.id) ||
                    (p.From == SelectedUser.id && p.To == _main.Id);
                if (belongsToOpenChat)
                    SelectedMessages = _main.Db.selecmsg(_main.Id, SelectedUser.id);
                Messageslist = _main.Db.Fetchmessages(_main.Id);
                Flist = _main.Db.Fetchfriends(Messageslist, _main.Id);
            }
            else if (e.PropertyName == nameof(MainWindowViewModel.New_invites) && _main.New_invites.To==_main.Id)
            {
                _main.Db.add_invite(_main.New_invites.From,_main.New_invites.To);
                PendingInvites = _main.Db.fetch_invites(_main.Id);
            }
        });
    }
    

    [RelayCommand]
    public void acceptInvite(invites i)
    {
        _main.Db.add_friends(i.from_id,i.to_id);
        _main.Db.delete_invite(i);
        pendingInvites.Remove(i);
        Messageslist = _main.Db.Fetchmessages(_main.Id);
        Flist = _main.Db.Fetchfriends(Messageslist, _main.Id);
        PendingInvites = _main.Db.fetch_invites(_main.Id);
    }
    [RelayCommand]
    public void refuseInvite(invites i)
    {
        _main.Db.delete_invite(i);
        pendingInvites.Remove(i);
        PendingInvites = _main.Db.fetch_invites(_main.Id);
    }

    
    
}


