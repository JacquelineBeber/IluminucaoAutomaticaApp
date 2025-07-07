namespace IluminucaoAutomaticaApp.Views.Componentes;

public partial class CabecalhoView : ContentView
{
	public CabecalhoView()
	{
        InitializeComponent();
    }
    private async void OnMenuClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MenuPage());
        //Application.Current.MainPage = new NavigationPage(new MenuPage());
    }
    private void OnTituloClicked(object sender, EventArgs e)
    {
         Application.Current.MainPage = new NavigationPage(new InicialPage());
    }
}
