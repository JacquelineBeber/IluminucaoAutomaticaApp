using IluminucaoAutomaticaApp.Models;

namespace IluminucaoAutomaticaApp.Services
{
    interface IConsumoService
    {
        Task<List<Consumo>> BuscarConsumoAsync();
        Task<MonitorarConsumo> BuscarConsumoDiarioAsync(DateTime data);
        Task<MonitorarConsumo> BuscarConsumoMensalAsync(int mes, int ano);
        Task<MonitorarConsumo> BuscarConsumoAnualAsync(int ano);
    }
}