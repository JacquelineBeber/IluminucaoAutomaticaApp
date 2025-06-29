using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IluminucaoAutomaticaApp.Converters
{
    internal class EstadoLampadaToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string estado)
            {
                if (estado.Equals("Ligada", StringComparison.OrdinalIgnoreCase))
                    return Color.FromArgb("#b4f5b4"); // Verde claro
                else
                    return Color.FromArgb("#f5b4b4"); // Vermelho claro
            }
            return Color.FromArgb("#f5b4b4");
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}