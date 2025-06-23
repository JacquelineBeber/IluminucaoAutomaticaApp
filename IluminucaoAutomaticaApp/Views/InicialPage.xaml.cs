using IluminucaoAutomaticaApp.Models;
using IluminucaoAutomaticaApp.ViewModels;

namespace IluminucaoAutomaticaApp.Views 
{
    public partial class InicialPage : ContentPage
    {
        public InicialPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new InicialPageViewModel();
        }

        private async void OnLampadaToggled(object sender, ToggledEventArgs e)
        {
            var switchControl = sender as Switch;
            var lampada = switchControl?.BindingContext as Lampada;

            if (e.Value) // Se foi ligado
            {
                var vm = BindingContext as InicialPageViewModel;
                if (vm != null)
                    await vm.AtivarLampadaAsync(lampada);
            }
        }
    }
}
