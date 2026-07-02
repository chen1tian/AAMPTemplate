using Avalonia.Controls;
using AAMPTpl.ViewModels;
using System.ComponentModel;
using System.Threading.Tasks;

namespace AAMPTpl.Views;

public partial class MainView : UserControl
{
    private MainViewModel? _viewModel;
    private ViewModelBase? _navigatedPage;

    public MainView()
    {
        InitializeComponent();
        DataContextChanged += OnDataContextChanged;
    }

    private void OnDataContextChanged(object? sender, System.EventArgs e)
    {
        if (_viewModel is not null)
        {
            _viewModel.PropertyChanged -= OnViewModelPropertyChanged;
        }

        _viewModel = DataContext as MainViewModel;

        if (_viewModel is null)
        {
            MainNavigation.Content = null;
            _navigatedPage = null;
            return;
        }

        _viewModel.PropertyChanged += OnViewModelPropertyChanged;
        NavigateToCurrentPage(replaceCurrentPage: false);
    }

    private async void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(MainViewModel.CurrentPage))
        {
            await NavigateToCurrentPageAsync(replaceCurrentPage: true);
        }
    }

    private void NavigateToCurrentPage(bool replaceCurrentPage)
    {
        _ = NavigateToCurrentPageAsync(replaceCurrentPage);
    }

    private async Task NavigateToCurrentPageAsync(bool replaceCurrentPage)
    {
        if (_viewModel is null || ReferenceEquals(_navigatedPage, _viewModel.CurrentPage))
        {
            return;
        }

        var page = CreateContentPage(_viewModel.CurrentPage, _viewModel.CurrentPageTitle);

        if (!replaceCurrentPage || MainNavigation.StackDepth == 0)
        {
            MainNavigation.Content = page;
        }
        else
        {
            await MainNavigation.ReplaceAsync(page);
        }

        _navigatedPage = _viewModel.CurrentPage;
    }

    private static ContentPage CreateContentPage(ViewModelBase pageViewModel, string header)
    {
        return new ContentPage
        {
            Header = header,
            Content = pageViewModel,
            HorizontalContentAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
            VerticalContentAlignment = Avalonia.Layout.VerticalAlignment.Stretch
        };
    }
}
