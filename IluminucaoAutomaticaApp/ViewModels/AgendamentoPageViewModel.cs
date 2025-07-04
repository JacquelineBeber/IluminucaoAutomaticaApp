using IluminucaoAutomaticaApp.Models;
using IluminucaoAutomaticaApp.Services;
using System.Collections.ObjectModel;

namespace IluminucaoAutomaticaApp.ViewModels
{
    class AgendamentoPageViewModel : BaseViewModel
    {
        private readonly IAgendamentoService _agendamentoService;
        public ObservableCollection<Agendamento> Agendamentos { get; set; } = new();

        private bool _semAgendamento;
        public bool SemAgendamento
        {
            get => _semAgendamento;
            set => SetProperty(ref _semAgendamento, value);
        }
        public AgendamentoPageViewModel()
        {
            _agendamentoService = new AgendamentoService();
            _ = BuscarAgendamentos();
        }

        public async Task<bool> CadastrarAgendamento(Agendamento agendamento)
        {
            try
            {
                var sucesso = await _agendamentoService.CadastrarAgendamentoAsync(agendamento);
                return sucesso;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task BuscarAgendamentos()
        {
            Carregando = true;
            var lista = await _agendamentoService.BuscarAgendamentosAsync();

            if (lista != null && lista.Any())
            {
                Agendamentos.Clear();
                foreach (var item in lista)
                    Agendamentos.Add(item);
            }

            SemAgendamento = !Agendamentos.Any();
            Carregando = false;
        }

    }
}
