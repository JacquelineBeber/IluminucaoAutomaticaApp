using IluminucaoAutomaticaApp.Models;
using IluminucaoAutomaticaApp.ViewModels;

namespace IluminucaoAutomaticaApp.Views;

public partial class AgendamentoPage : ContentPage
{
    private readonly AgendamentoPageViewModel _viewModel;
    public AgendamentoPage()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _viewModel = new AgendamentoPageViewModel();
    }
    private async void OnCadastrarAgendamentoClicked(object sender, EventArgs e)
    {
        MensagemSucesso.IsVisible = false;
        MensagemErroObrigatorio.IsVisible = false;
        MensagemErroCadastro.IsVisible = false;

        var nome = NomeAgendamentoEntry.Text?.Trim();
        var data = DataAgendamentoPicker.Date;
        var hora = HoraAgendamentoPicker.Time;
        var acao = AcaoPicker.SelectedItem?.ToString();

        if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(acao))
        {
            MensagemErroObrigatorio.IsVisible = true;
            MensagemErroCadastro.IsVisible = false;
            MensagemSucesso.IsVisible = false;
            return;
        }

        var dataHora = data + hora;

        var agendamento = new Agendamento
        {
            Nome = nome,
            Acao = acao,
            MomentoAcionamento = dataHora.ToString("dd/MM/yyyy HH:mm:ss")
        };

        var sucesso = await _viewModel.CadastrarAgendamento(agendamento);

        if (sucesso)
        {
            MensagemErroObrigatorio.IsVisible = false;
            MensagemErroCadastro.IsVisible = false;
            MensagemSucesso.IsVisible = true;
            NomeAgendamentoEntry.Text = string.Empty;
            AcaoPicker.SelectedIndex = -1;
        }
        else
        {
            MensagemSucesso.IsVisible = false;
            MensagemErroObrigatorio.IsVisible = false;
            MensagemErroCadastro.IsVisible = true;
        }
    }
}