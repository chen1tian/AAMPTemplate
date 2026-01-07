using CommunityToolkit.Mvvm.ComponentModel;

namespace AAMPTpl.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _greeting = "Welcome to Avalonia!";
}
