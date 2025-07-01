using IluminucaoAutomaticaApp.Services;

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
