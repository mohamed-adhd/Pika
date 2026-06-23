using CommunityToolkit.Mvvm.Input;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using live_message_app.Services;
using live_message_app.Views;
namespace live_message_app.ViewModels;
using System.Text;
using System.Threading.Tasks;
public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty] private object currentpage;

    [ObservableProperty] private int id;
    [ObservableProperty] private bool connected = false;
    public Services.Network network=new();
    [ObservableProperty]private packet updates;
    [ObservableProperty]private bool news=false;
    
    public database Db { get; } = new();

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
                    var p = network.start_recieving();
                    News = true;
                    int s = Db.neword(Updates.From, Updates.To);
                    Db.addmsg(p.Text, p.From, p.To, s);
                    News = false;
                    Updates = p;
                    
                }

            });
        }
        

    }
}