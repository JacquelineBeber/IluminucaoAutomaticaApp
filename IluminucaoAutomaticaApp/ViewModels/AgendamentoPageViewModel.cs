using IluminucaoAutomaticaApp.Models;
using IluminucaoAutomaticaApp.Services;

namespace IluminucaoAutomaticaApp.ViewModels
{
    class AgendamentoPageViewModel : BaseViewModel
    {
        private readonly IAgendamentoService _agendamentoService;

        public AgendamentoPageViewModel()
        {
            _agendamentoService = new AgendamentoService();
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

    }
}
