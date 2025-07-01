using IluminucaoAutomaticaApp.Models;

namespace IluminucaoAutomaticaApp.Services
{
    interface IAgendamentoService
    {
        Task<bool> CadastrarAgendamentoAsync(Agendamento agendamento);
    }
}
