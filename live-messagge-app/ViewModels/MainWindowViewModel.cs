using CommunityToolkit.Mvvm.Input;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using live_message_app.Views;
namespace live_message_app.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty] private object currentpage;

    [ObservableProperty] private int id;

    public MainWindowViewModel()
    {
        currentpage = new LoginViewModel(this);
    }
}