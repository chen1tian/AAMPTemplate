using Azure;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace AAMPTpl.ViewModels;

public class NavMenuItem
{
    public string Title { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
}

public partial class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        _currentPage = _page1;
        _selectedMenuItem = MenuItems[0];
    }

    #region 菜单
    [ObservableProperty]
    private ViewModelBase _currentPage;

    [ObservableProperty]
    private NavMenuItem _selectedMenuItem;

    [ObservableProperty]
    private bool _navDrawerSwitch;

    [ObservableProperty]
    private bool _isMenuCollapsed;

    partial void OnIsMenuCollapsedChanged(bool value)
    {        
        OnPropertyChanged(nameof(MenuWidth));
        OnPropertyChanged(nameof(IsMenuTextVisible));
        OnPropertyChanged(nameof(ToggleMenuIcon));
    }

    public double MenuWidth => IsMenuCollapsed ? 56 : 120;
    public bool IsMenuTextVisible => !IsMenuCollapsed;
    public string ToggleMenuIcon => IsMenuCollapsed
        ? "M8.59,16.58L13.17,12L8.59,7.41L10,6L16,12L10,18L8.59,16.58Z"
        : "M15.41,16.58L10.83,12L15.41,7.41L14,6L8,12L14,18L15.41,16.58Z";

    [RelayCommand]
    private void ToggleMenu() => IsMenuCollapsed = !IsMenuCollapsed;

    public ObservableCollection<NavMenuItem> MenuItems { get; } =
    [
        new NavMenuItem { Title = "菜单1", Icon = "M12,2A10,10 0 0,0 2,12A10,10 0 0,0 12,22A10,10 0 0,0 22,12A10,10 0 0,0 12,2M12,4A8,8 0 0,1 20,12A8,8 0 0,1 12,20A8,8 0 0,1 4,12A8,8 0 0,1 12,4M12,6A6,6 0 0,0 6,12A6,6 0 0,0 12,18A6,6 0 0,0 18,12A6,6 0 0,0 12,6M12,8A4,4 0 0,1 16,12A4,4 0 0,1 12,16A4,4 0 0,1 8,12A4,4 0 0,1 12,8Z" },
        new NavMenuItem { Title = "菜单2", Icon = "M22,21H2V3H4V19H6V10H10V19H12V6H16V19H18V14H22V21Z" },        
    ];

    partial void OnSelectedMenuItemChanged(NavMenuItem value)
    {
        SwitchPage(value);
    }

    private void SwitchPage(NavMenuItem menuItem)
    {
        CurrentPage = menuItem.Title switch
        {
            "菜单1" => _page1,
            "菜单2" => _page2,        
            _ => _page1
        };
    }

    private readonly Page1ViewModel _page1 = new();
    private readonly Page2ViewModel _page2 = new();
    #endregion 菜单
}
