namespace IluminucaoAutomaticaApp.Views;

public partial class MenuPage : ContentPage
{
    public MenuPage()
    {
        InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
    }

    private void OnFecharMenu(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new InicialPage());
    }

    private void OnTituloClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new InicialPage());
    }

    private void OnCadastrarLampadaClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new CadastrarLampadaPage());
    }

    private void OnHistoricoClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new HistoricoPage());
    }

    private void OnConsumoClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new ConsumoPage());
    }

    private void OnAgendamentoClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new AgendamentoPage());
    }
}
