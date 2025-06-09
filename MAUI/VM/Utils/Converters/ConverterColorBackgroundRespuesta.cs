using MAUI.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.VM.Utils.Converters
{
    public class ConverterColorBackgroundRespuesta : IValueConverter
    {

        /// <summary>
        /// Convierte un valor de humedad en un color representando su nivel.
        /// </summary>
        /// <param name="value">Valor de humedad como entero (0–100).</param>
        /// <param name="targetType">Tipo de destino (no se usa).</param>
        /// <param name="parameter">Parámetro opcional (no se usa).</param>
        /// <param name="culture">Información de cultura (no se usa).</param>
        /// <returns>Un color: verde (≤33%), amarillo (34–66%), o rojo (>66%).</returns>
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            Color color = Colors.Transparent;
            clsPregunta pregunta;

            if (value != null)
            {
                pregunta = (clsPregunta)value;

                if (pregunta.EsCorrecto)
                {

                    color = Colors.LightGreen;
                }
                else
                {
                    color = Colors.IndianRed;
                }
            }

            return color;
        }

        /// <summary>
        /// Retorna el valor sin cambios (conversión inversa no implementada).
        /// </summary>
        /// <param name="value">Valor original.</param>
        /// <param name="targetType">Tipo de destino (no se usa).</param>
        /// <param name="parameter">Parámetro opcional (no se usa).</param>
        /// <param name="culture">Información de cultura (no se usa).</param>
        /// <returns>El mismo valor recibido.</returns>
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
