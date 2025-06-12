using DTO;
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
        /// Convierte el estado de una pregunta en un color visual.
        /// Devuelve LightGreen si la respuesta es correcta, IndianRed si es incorrecta, y White si no se ha seleccionado ninguna respuesta.
        /// </summary>
        /// <param name="value">Objeto que representa la pregunta (clsPregunta).</param>
        /// <param name="targetType">Tipo de destino (no se usa).</param>
        /// <param name="parameter">Parámetro opcional (no se usa).</param>
        /// <param name="culture">Información de cultura (no se usa).</param>
        /// <returns>Un color que representa el estado de la respuesta.</returns>
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            Color color = Colors.White;
            clsPregunta pregunta = (clsPregunta)value;

            if (pregunta.PokemonSeleccionado != null)
            {
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
