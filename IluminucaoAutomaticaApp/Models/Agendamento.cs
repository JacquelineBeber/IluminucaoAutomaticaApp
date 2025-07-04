using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IluminucaoAutomaticaApp.Models
{
    class Agendamento
    {
        [JsonPropertyName("nome")]
        public string? Nome { get; set; }

        [JsonPropertyName("momento_acionamento")]
        public string? MomentoAcionamento { get; set; }

        [JsonPropertyName("acao")]
        public string? Acao { get; set; }
    }
}
