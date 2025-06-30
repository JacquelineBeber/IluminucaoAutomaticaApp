using IluminucaoAutomaticaApp.ViewModels;

namespace IluminucaoAutomaticaApp.Views;

public partial class MonitorarConsumoPage : ContentPage
{
	public MonitorarConsumoPage()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = new MonitorarConsumoPageViewModel();
    }
}