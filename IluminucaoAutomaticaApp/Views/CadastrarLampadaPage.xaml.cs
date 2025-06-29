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
        MensagemErroObrigatorio.IsVisible = false;
        MensagemErroCadastro.IsVisible = false;
        MensagemErroPotencia.IsVisible = false;

        var nome = NomeEntry.Text?.Trim();
        var potenciaStr = PotenciaEntry.Text?.Trim();

        if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(potenciaStr))
        {
            MensagemErroObrigatorio.IsVisible = true;
            MensagemErroCadastro.IsVisible = false;
            MensagemSucesso.IsVisible = false;
            MensagemErroPotencia.IsVisible = false;
            return;
        }

        if (!decimal.TryParse(potenciaStr, out decimal potencia) || potencia <= 0)
        {
            MensagemErroCadastro.IsVisible = false;
            MensagemErroObrigatorio.IsVisible = false;
            MensagemSucesso.IsVisible = false;
            
            MensagemErroPotencia.IsVisible = true;
            return;
        }

        try
        {
            var vm = BindingContext as CadastrarLampadaPageViewModel;
            var sucesso = await vm.CadastrarLampada(nome, potencia);
            
            if (sucesso)
            {
                MensagemErroCadastro.IsVisible = false;
                MensagemErroObrigatorio.IsVisible = false;
                MensagemErroPotencia.IsVisible = false;

                MensagemSucesso.IsVisible = true;
                NomeEntry.Text = string.Empty;
                PotenciaEntry.Text = string.Empty;
            }
            else
            {
                MensagemSucesso.IsVisible = false;
                MensagemErroObrigatorio.IsVisible = false;
                MensagemErroPotencia.IsVisible = false;

                MensagemErroCadastro.IsVisible = true;
            }
        }
        catch
        {
            MensagemSucesso.IsVisible = false;
            MensagemSucesso.IsVisible = false;
            MensagemErroObrigatorio.IsVisible = false;
            MensagemErroPotencia.IsVisible = false;

            MensagemErroCadastro.IsVisible = true;
        }
    }
}