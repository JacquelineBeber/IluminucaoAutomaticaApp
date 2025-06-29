using IluminucaoAutomaticaApp.ViewModels;

namespace IluminucaoAutomaticaApp.Views;

public partial class ConsumoPage : ContentPage
{
	public ConsumoPage()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = new ConsumoPageViewModel();
    }
}