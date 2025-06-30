using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IluminucaoAutomaticaApp.Models
{
    class Consumo
    {
        [JsonPropertyName("momentoAcionamento")]
        public string? Acionamento { get; set; }

        [JsonPropertyName("momentoDesligamento")]
        public string? Desligamento { get; set; }

        [JsonPropertyName("energiaConsumida")]
        public decimal EnergiaConsumida { get; set; }

        public DateTime? AcionamentoDataHora
        {
            get
            {
                if (DateTime.TryParseExact(Acionamento, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var data))
                    return data;
                return null;
            }
        }
    }
}