using IluminucaoAutomaticaApp.Models;

namespace IluminucaoAutomaticaApp.Services
{
    interface IHistoricoService
    {
        Task<List<Historico>> BuscarHistoricoAsync();
    }
}
