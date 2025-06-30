using IluminucaoAutomaticaApp.ViewModels;
using System.ComponentModel;

namespace IluminucaoAutomaticaApp.Views;

public partial class ConsumoPage : ContentPage
{
    private readonly ConsumoPageViewModel _viewModel;
    public ConsumoPage()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        //BindingContext = new ConsumoPageViewModel();

        _viewModel = new ConsumoPageViewModel();
        BindingContext = _viewModel;
        _viewModel.PropertyChanged += ViewModel_PropertyChanged;
    }

    private async void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(_viewModel.SemConsumo))
        {
            if(_viewModel.SemConsumo)
            {
                LabelSemConsumo.IsVisible = true;
                await LabelSemConsumo.FadeTo(1, 500); // Fade in
            }
            else
            {
                await LabelSemConsumo.FadeTo(0, 300); // Fade out
                LabelSemConsumo.IsVisible = false;
            }
        }
    }
}