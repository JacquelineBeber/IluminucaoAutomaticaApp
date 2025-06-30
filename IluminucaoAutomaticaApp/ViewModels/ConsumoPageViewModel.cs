using IluminucaoAutomaticaApp.Models;
using IluminucaoAutomaticaApp.Services;
using System.Collections.ObjectModel;

namespace IluminucaoAutomaticaApp.ViewModels
{
    class ConsumoPageViewModel : BaseViewModel
    {
        private readonly IConsumoService _consumoService;
        public ObservableCollection<Consumo> Consumo { get; set; } = new();
        public ConsumoPageViewModel()
        {
            _consumoService = new ConsumoService();
            _ = CarregarConsumoAsync();
        }
        private async Task CarregarConsumoAsync()
        {
            var lista = await _consumoService.BuscarConsumoAsync();
            if (lista != null)
            {
                var datasAcionamentoOrdenadas = lista
                    .Where(c => c.AcionamentoDataHora != null)
                    .OrderByDescending(c => c.AcionamentoDataHora)
                    .ToList();
                Consumo.Clear();
                foreach (var item in datasAcionamentoOrdenadas)
                    Consumo.Add(item);
            }
        }
    }
}