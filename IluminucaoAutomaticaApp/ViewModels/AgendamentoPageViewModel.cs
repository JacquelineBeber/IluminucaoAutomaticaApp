using IluminucaoAutomaticaApp.Models;
using IluminucaoAutomaticaApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IluminucaoAutomaticaApp.ViewModels
{
    class AgendamentoPageViewModel : BaseViewModel
    {
        private readonly IAgendamentoService _agendamentoService;

        public AgendamentoPageViewModel()
        {
            _agendamentoService = new AgendamentoService();
        }

        public async Task<bool> CadastrarAgendamento(Agendamento agendamento)
        {
            try
            {
                var sucesso = await _agendamentoService.CadastrarAgendamentoAsync(agendamento);
                return sucesso;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
