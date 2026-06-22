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
                    updates = network.start_recieving();
                    news = true;
                    int s = Db.neword(Updates.From, Updates.To);
                    Db.addmsg(Updates.Text, Updates.From, Updates.To, s);
                    News = false;
                }

            });
        }
        

    }
}