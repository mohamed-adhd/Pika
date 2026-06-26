using System;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using live_message_app.Services;
using live_message_app.Views;
namespace live_message_app.ViewModels;
using Tmds.DBus.Protocol;
using System.Text;
using System.Threading.Tasks;
public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty] private object currentpage;
    [ObservableProperty] private int id;
    [ObservableProperty] private bool connected = false;
    public Services.Network network=new();
    [ObservableProperty]private packet updates;
    [ObservableProperty]private packet new_invites;
    
    [ObservableProperty]private bool news=false;
    [ObservableProperty]private int accepted;
    Random random = new Random();
    public database Db { get; } = new();
    public Notifahh notifs = new();
    [ObservableProperty]private bool deleted=false;
    public MainWindowViewModel()
    {
        connected=network.Connect("127.0.0.1", 8000);
        
        currentpage = new LoginViewModel(this);
        if (Connected)
        {
            Task.Run(() =>
            {
                while (Connected)
                {
                    packet p=new();
                    try
                    {
                        p = network.start_recieving();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                    
                    if (p.Type == "message")
                    {
                        News = true;
                        int s = Db.neword(p.From, p.To);
                        Db.addmsg(p.Text, p.From, p.To, s);
                        News = false;
                        Updates = p;
                        string title = "new mesage from " + Db.get_username(p.From);
                        notifs.notifi(title,p.Text,"/home/bro/my-creations/temp/live-messagge-app/live-messagge-app/Assets/logo.jpg");
                    }
                    else if (p.Type=="request")
                    {
                        Db.add_invite(p.From, p.To);
                        New_invites = p;
                        string title = "new friend request";
                        string body = Db.get_username(p.From) + "sent you a friend request";
                        notifs.notifi(title,body,"/home/bro/my-creations/temp/live-messagge-app/live-messagge-app/Assets/logo.jpg");

                    }else if (p.Type=="add_user")
                    {
                        int s = p.Text.IndexOf("/");
                        string username = p.Text.Substring(0,s);
                        p.Text = p.Text.Substring(s+1);
                        s = p.Text.IndexOf("/");
                        string name = p.Text.Substring(0,s);
                        p.Text = p.Text.Substring(s+1);
                        s = p.Text.IndexOf("/");
                        string pass = p.Text.Substring(0,s);
                        p.Text = p.Text.Substring(s+1);
                        s = p.Text.IndexOf("/");
                        string gmail = p.Text.Substring(0,s);
                        p.Text = p.Text.Substring(s+1);
                        Db.add(username, name, pass, gmail);
                    }else if (p.Type == "accepted")
                    {
                        Accepted = random.Next();
                        Db.add_friends(p.From, p.To);
                    }
                }
            });
        }
        

    }
}