using IluminucaoAutomaticaApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IluminucaoAutomaticaApp.ViewModels
{
    class CadastrarLampadaPageViewModel : BaseViewModel
    {
        private readonly ILampadaService _lampadaService;

        public CadastrarLampadaPageViewModel()
        {
            _lampadaService = new LampadaService();
        }

        public async Task<bool> CadastrarLampada(string nome, decimal potencia)
        {
            try
            {
                var sucesso = await _lampadaService.CadastrarLampadaAsync(nome, potencia);
                return sucesso;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
