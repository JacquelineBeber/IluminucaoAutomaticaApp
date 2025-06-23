using IluminucaoAutomaticaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
