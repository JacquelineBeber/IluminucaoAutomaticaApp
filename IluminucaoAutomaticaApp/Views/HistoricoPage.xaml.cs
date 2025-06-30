using IluminucaoAutomaticaApp.ViewModels;
using System.ComponentModel;

namespace IluminucaoAutomaticaApp.Views;

public partial class HistoricoPage : ContentPage
{
    private readonly HistoricoPageViewModel _viewModel;
    public HistoricoPage()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        //BindingContext = new HistoricoPageViewModel();

        _viewModel = new HistoricoPageViewModel();
        BindingContext = _viewModel;

        _viewModel.PropertyChanged += ViewModel_PropertyChanged;
    }

    private async void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(_viewModel.SemHistorico))
        {
            if (_viewModel.SemHistorico)
            {
                LabelSemHistorico.IsVisible = true;
                await LabelSemHistorico.FadeTo(1, 500); // Fade in
            }
            else
            {
                await LabelSemHistorico.FadeTo(0, 300); // Fade out
                LabelSemHistorico.IsVisible = false;
            }
        }
    }
}