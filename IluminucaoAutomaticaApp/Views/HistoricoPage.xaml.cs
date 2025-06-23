using IluminucaoAutomaticaApp.ViewModels;

namespace IluminucaoAutomaticaApp.Views;

public partial class HistoricoPage : ContentPage
{
	public HistoricoPage()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = new HistoricoPageViewModel();
    }
}