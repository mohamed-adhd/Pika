using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;
using Avalonia.Controls;
using Microsoft.Extensions.Logging;

namespace live_message_app.Services;

public class Notifahh
{

    public void notifi(string title, string obj,string path)
    {
        Process.Start(new ProcessStartInfo{
            FileName = "notify-send",
            ArgumentList = {  "-i", path, title, obj},
            UseShellExecute = false
    }
    );

}
}