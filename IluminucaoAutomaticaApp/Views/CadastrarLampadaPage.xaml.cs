using IluminucaoAutomaticaApp.Services;
using IluminucaoAutomaticaApp.ViewModels;

namespace IluminucaoAutomaticaApp.Views;

public partial class CadastrarLampadaPage : ContentPage
{
	public CadastrarLampadaPage()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = new CadastrarLampadaPageViewModel();
    }
    
    private async void OnCadastrarClicked(object sender, EventArgs e)
    {
        MensagemSucesso.IsVisible = false;
        MensagemErro.IsVisible = false;

        var nome = NomeEntry.Text?.Trim();
        var potenciaStr = PotenciaEntry.Text?.Trim();

        if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(potenciaStr))
        {
            MensagemErro.IsVisible = true;
            return;
        }

        if (!decimal.TryParse(potenciaStr, out decimal potencia) || potencia <= 0)
        {
            MensagemErro.IsVisible = true;
            return;
        }

        try
        {
            var vm = BindingContext as CadastrarLampadaPageViewModel;
            var sucesso = await vm.CadastrarLampada(nome, potencia);
            
            if (sucesso)
            {
                MensagemErro.IsVisible = false;
                MensagemSucesso.IsVisible = true;
                NomeEntry.Text = string.Empty;
                PotenciaEntry.Text = string.Empty;
            }
            else
            {
                MensagemSucesso.IsVisible = false;
                MensagemErro.Text = "Não foi possível cadastrar. Tente novamente.";
                MensagemErro.IsVisible = true;
            }
        }
        catch
        {
            MensagemSucesso.IsVisible = false;
            MensagemErro.IsVisible = true;
        }
    }
}