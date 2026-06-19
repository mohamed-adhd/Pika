using System.Reflection.Metadata.Ecma335;
using live_message_app.Services;
namespace live_message_app.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
public partial class RegisterViewModel: ViewModelBase
{
    private readonly MainWindowViewModel _main;
    public readonly database db;
    [ObservableProperty] string username,name,passwrd,cpasswd,gmail,message;
    public RegisterViewModel(MainWindowViewModel main)
    {
        _main = main;
        db = new Services.database();
    }
    [RelayCommand]
    private void GoToLogin()
    {
        _main.Currentpage = new LoginViewModel(_main);
    }

    [RelayCommand]
    private bool register()
    {
        if (Cpasswd != Passwrd)
        {
            Message = "passwords arent matching!";
            return false;
        }else if (!Gmail.Contains("@gmail.com"))
        {
            Message = "not a valid gmail address, we only support gmail!";
            return false;
        }
        else if (Username == "" || Passwrd == "" || Cpasswd == "" || Gmail == "" || Name == "")
        {
            Message = "fill all fields!";
            return false;
        }
        else
        {
            if (db.add(Username, Name, Passwrd, Gmail))
            {
                Message = "successfully registered ! go back to login";
            }
            else
            {
                Message = "something went wrong , check your data";
            }
        }
        
    }
}