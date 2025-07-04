using IluminucaoAutomaticaApp.ViewModels;
using System.ComponentModel;

namespace IluminucaoAutomaticaApp.Views;

public partial class AcompanharAgendamentosPage : ContentPage
{
    private readonly AgendamentoPageViewModel _viewModel;
    public AcompanharAgendamentosPage()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);

        _viewModel = new AgendamentoPageViewModel();
        BindingContext = _viewModel;

        _viewModel.PropertyChanged += ViewModel_PropertyChanged;
    }

    private async void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(_viewModel.SemAgendamento))
        {
            if (_viewModel.SemAgendamento)
            {
                LabelSemAgendamento.IsVisible = true;
                await LabelSemAgendamento.FadeTo(1, 500);
            }
            else
            {
                await LabelSemAgendamento.FadeTo(0, 300);
                LabelSemAgendamento.IsVisible = false;
            }
        }
    }
}