using Avalonia.Controls;
using System.ComponentModel;
using Avalonia;
using Avalonia.Threading;
using live_message_app.ViewModels;
namespace live_message_app.Views;
public partial class MainMenu : UserControl
{
    public MainMenu()
    {
        InitializeComponent();
        DataContextChanged += (_, _) =>
        {
            if (DataContext is MainMenuViewModel vm)
            {
                vm.PropertyChanged += (_, e) =>
                {
                    if (e.PropertyName ==
                        nameof(MainMenuViewModel.SelectedMessages))
                    {
                        Dispatcher.UIThread.Post(() =>
                        {
                            Scrollshi.Offset = new Vector(
                                Scrollshi.Offset.X,
                                Scrollshi.Extent.Height
                            );
                        });
                    }
                };
            }
        };
    }
    
}
