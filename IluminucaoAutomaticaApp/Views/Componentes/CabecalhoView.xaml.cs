using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace IluminucaoAutomaticaApp.Views.Componentes;

public partial class CabecalhoView : ContentView
{
	public CabecalhoView()
	{
        InitializeComponent();
    }
    private void OnMenuClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new MenuPage());
    }
    private void OnTituloClicked(object sender, EventArgs e)
    {
        // Se já estiver na InicialPage, apenas recarrega
        //if (Application.Current.MainPage is NavigationPage nav &&
        //    nav.CurrentPage is InicialPage)
        //{
        //    await nav.PopToRootAsync();
        //    nav.CurrentPage.BindingContext = new InicialPage();
        //}
        //else
        //{
            Application.Current.MainPage = new NavigationPage(new InicialPage());
        //}
    }
}
