using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IluminucaoAutomaticaApp.Models
{
    class Historico
    {
        [JsonPropertyName("acao")]
        public string? Acao { get; set; }

        [JsonPropertyName("data_hora_acao")]
        public string? MomentoAcao { get; set; }

        [JsonPropertyName("responsavel_acao")]
        public string? OrigemAcao { get; set; }
    }
}
