using IluminucaoAutomaticaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IluminucaoAutomaticaApp.Services
{
    interface IAgendamentoService
    {
        Task<bool> CadastrarAgendamentoAsync(Agendamento agendamento);
    }
}
