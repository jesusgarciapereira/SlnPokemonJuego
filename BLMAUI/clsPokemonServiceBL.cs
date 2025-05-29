using DTO;
using SERVICE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLMAUI
{
    public class clsPokemonServiceBL
    {
        /// <summary>
        /// Realiza una solicitud HTTP a la PokeAPI para obtener un listado de Pokémon con paginación mediante offset y limit desde la capa SERVICE.
        /// </summary>
        /// <param name="offset">Número de Pokémon a omitir desde el inicio (inicio del bloque de resultados).</param>
        /// <param name="limit">Cantidad de Pokémon a obtener a partir del offset.</param>
        /// <returns>
        /// Una lista de objetos clsPokemon con las reglas de negocio aplicadas:
        /// - Llena si la API devuelve datos (código 200).
        /// - Vacía si no hay datos disponibles (código 204).
        /// - Null si ocurre un error en la solicitud (código 400).
        /// </returns>
        public static async Task<List<clsPokemon>> ObtenerListadoPokemonPartidaService(int offset, int limit)
        {
            return await clsPokemonService.ObtenerListadoPokemonPartidaService(offset, limit);
        }
    }
}
