using IluminucaoAutomaticaApp.Models;
using IluminucaoAutomaticaApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IluminucaoAutomaticaApp.ViewModels
{
    class HistoricoPageViewModel : BaseViewModel
    {
        private readonly IHistoricoService _historicoService;
        public ObservableCollection<Historico> Historico { get; set; } = new();
        public HistoricoPageViewModel()
        {
            _historicoService = new HistoricoService();
            _ = CarregarHistoricoAsync();
        }
        private async Task CarregarHistoricoAsync()
        {
            var lista = await _historicoService.BuscarHistoricoAsync();
            if (lista != null)
            {
                var datasMomentoAcaoOrdenadas = lista
                    .Where(h => h.MomentoAcaoDataHora != null)
                    .OrderByDescending(h => h.MomentoAcaoDataHora)
                    .ToList();

                Historico.Clear();
                foreach (var item in datasMomentoAcaoOrdenadas)
                    Historico.Add(item);
            }
        }

    }
}
