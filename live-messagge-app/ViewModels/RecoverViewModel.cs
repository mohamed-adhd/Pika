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
        msg.Body = new TextPart("html")
        {
            Text = $"""
                    <div style="font-family: Arial, sans-serif; max-width: 480px; margin: auto; padding: 32px; background: #f9f9f9; border-radius: 12px;">
                        
                        <h2 style="color: #6366F1;">🔑 Password Recovery</h2>
                        
                        <p style="color: #333;">wsupp <b>{R_username}</b>,</p>
                        
                        <p style="color: #333;">Here is yo password:</p>
                        
                        <div style="background: #1a1a2e; color: #a5b4fc; font-size: 20px; font-weight: bold;
                                    padding: 16px; border-radius: 8px; text-align: center; letter-spacing: 2px;">
                            {_main.Db.get_pass(R_username)}
                        </div>
                        
                        <p style="color: #666; font-size: 13px; margin-top: 24px;">
                            didn't request this? don't worry someone might've entered your username by mistake. 
                            You can safely ignore this email.
                        </p>
                        
                        <hr style="border: none; border-top: 1px solid #ddd; margin: 24px 0;"/>
                        
                        <p style="color: #999; font-size: 12px;">
                            Sincerely,<br/>
                            <b style="color: #6366F1;">Pika Team</b> — Mohamed
                        </p>

                    </div>
                    """
        };
        using var client = new SmtpClient();
        client.Connect("smtp.gmail.com", 587, false);
        client.Authenticate("midouamdouni4@gmail.com", "jxjhveqftdssqpgh");
        client.Send(msg);
        client.Disconnect(true);
        Info = "email sent! check your inbox";
    }

    [RelayCommand]
    private void GoToLogin()
    {
        _main.Currentpage = new LoginViewModel(_main);
    }
}