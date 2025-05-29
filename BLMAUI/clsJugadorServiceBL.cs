using DTO;
using SERVICE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLMAUI
{
    public class clsJugadorServiceBL
    {
        /// <summary>
        /// Realiza una solicitud HTTP a la API desde la capa SERVICE.
        /// Pre: Ninguna
        /// Post: Devuelve una lista de objetos de tipo clsJugador que puede estar llena, vacía o ser null si ocurre un error.
        /// </summary>
        /// <returns>Una lista de objetos de tipo clsJugador con las reglas de negocio aplicadas.</returns>
        public static async Task<List<clsJugador>> ObtenerListadoJugadoresServiceBL() 
        {
            return await clsJugadorService.ObtenerListadoJugadoresService();
        }

        /// <summary>
        /// Envía un objeto clsJugador a la API para guardarlo mediante una solicitud HTTP POST desde la capa SERVICE.
        /// </summary>
        /// <param name="jugador">El objeto clsJugador que se desea enviar para ser guardado.</param>
        /// Pre: Ninguno.
        /// Post: Devuelve sólo 200, 404, o 400
        /// <returns>
        /// Un HttpStatusCode que indica el resultado de la solicitud con las reglas de negocio aplicadas:
        /// - 200 si el jugador fue guardado correctamente.
        /// - 404 si no se guardó ningún jugador.
        /// - 400 si ocurrió un error durante la solicitud.
        /// </returns>
        public static async Task<HttpStatusCode> GuardarJugadorServiceBL(clsJugador jugador) 
        {
            return await clsJugadorService.GuardarJugadorService(jugador);
        }
    }
}
