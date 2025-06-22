namespace IluminucaoAutomaticaApp.Views;

public partial class CadastrarLampadaPage : ContentPage
{
	public CadastrarLampadaPage()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
    }

    private void OnCadastrarClicked(object sender, EventArgs e)
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

        if (!double.TryParse(potenciaStr, out double potencia) || potencia <= 0)
        {
            MensagemErro.IsVisible = true;
            return;
        }

        try
        {
            // await LampadaService.CadastrarAsync(nome, potencia);
            MensagemErro.IsVisible = false;
            MensagemSucesso.IsVisible = true;
            NomeEntry.Text = string.Empty;
            PotenciaEntry.Text = string.Empty;
        }
        catch
        {
            MensagemSucesso.IsVisible = false;
            MensagemErro.IsVisible = true;
        }
    }
}