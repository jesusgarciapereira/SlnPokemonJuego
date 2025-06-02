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
        /// Realiza una solicitud HTTP a la PokeAPI para obtener un listado de Pokémon con paginación mediante offset y limit desde la capa SERVICE según la generación del parámetro.
        /// Pre: Sólo se permiten como parámetros los números 1, 2, 3 y 0 (De momento) 
        /// Post: Ninguna
        /// </summary>
        /// <param name="generacion">Número correspondiente a la generación</param>
        /// <returns>
        /// Una lista de objetos clsPokemon con las reglas de negocio aplicadas:
        /// - Llena si la API devuelve datos (código 200).
        /// - Vacía si no hay datos disponibles (código 204).
        /// - Null si ocurre un error en la solicitud (código 400).
        /// </returns>
        public static async Task<List<clsPokemon>> ObtenerListadoPokemonPorGeneracionBL(int generacion)
        {
            return await clsPokemonService.ObtenerListadoPokemonPorGeneracionService(generacion);
        }
    }
}
