using IluminucaoAutomaticaApp.Models;

namespace IluminucaoAutomaticaApp.Services
{
    interface ILampadaService
    {
        Task<List<Lampada>> BuscarLampadasAsync();
        Task<bool> LigarLampadaAsync();
        Task<bool> DesligarLampadaAsync();
        Task<bool> ExcluirLampadaAsync(string id);
        Task<bool> AtivarLampadaAsync(string id);
        Task<bool> CadastrarLampadaAsync(string nome, decimal potencia);
    }
}
