using IluminucaoAutomaticaApp.Models;

namespace IluminucaoAutomaticaApp.Services
{
    interface IAgendamentoService
    {
        public Task<bool> CadastrarAgendamentoAsync(Agendamento agendamento);

        public Task<List<Agendamento>> BuscarAgendamentosAsync();
    }
}
