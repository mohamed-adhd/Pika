namespace live_message_app.ViewModels;
using System.Collections.Generic;
using System.Net.Sockets;
using Tmds.DBus.Protocol;
using live_message_app.Services;
using static System.Console;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using live_message_app.Views;
using Avalonia.Threading;
using MailKit.Net.Smtp;
using MimeKit;
public partial class RecoverViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _main;

    [ObservableProperty] private string r_username = "";
    [ObservableProperty] private string info = "";

    public RecoverViewModel(MainWindowViewModel main)
    {
        _main = main;
    }

    [RelayCommand]
    private void SendRecovery()
    {
        string gmail = _main.Db.get_gmail(R_username);
        if (gmail == null)
        {
            Info = "user not found !";
            return;

        }

        var msg = new MimeMessage();
        msg.From.Add(new MailboxAddress("pika", "midouamdouni4@gmail.com"));
        msg.To.Add(new MailboxAddress("", gmail));
        msg.Subject = "Password Recovery";
        msg.Body = new TextPart("plain")
        {
            Text = "hello " + R_username + ", this is an automated email for password recovery, your password is " +
                   _main.Db.get_pass(R_username) +
                   ", if you didnt ask for this then ignore this email someone could ve entered your usernema by fault , sincerly Pika team(mohamed)"
        };
        using var client = new SmtpClient();
        client.Connect("smtp.gmail.com", 587, false);
        client.Authenticate("midouamdouni4@gmail.com", "jxjhveqftdssqpgh");
        client.Send(msg);
        client.Disconnect(true);
    }

    [RelayCommand]
    private void GoToLogin()
    {
        _main.Currentpage = new LoginViewModel(_main);
    }
}