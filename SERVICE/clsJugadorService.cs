using ENT;
using Newtonsoft.Json;
using SERVICE.Conexion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
//JsonConvert necesita using Newtonsoft.Json;
//Es el paquete Nuget de Newtonsoft

namespace SERVICE
{
    public class clsJugadorService
    {
        #region Métodos
        /// <summary>
        /// Realiza una solicitud HTTP a la API para obtener el listado completo de jugadores.
        /// </summary>
        /// Pre: Ninguno.
        /// Post: Devuelve una lista de objetos de tipo clsJugador que puede estar llena, vacía o ser null si ocurre un error.
        /// <returns>Una lista de clsJugador: llena si hay datos, vacía si no hay registros, o null si hay un error de conexión o respuesta inesperada.</returns>
        public static async Task<List<clsJugador>> ObtenerListadoJugadoresService()
        {
            // Instanciamos el listado que devolverá la función
            List<clsJugador> listadoJugadores;

            // La respuesta: El 404, el 200, etc…
            HttpResponseMessage miCodigoRespuesta;

            // El listado en futuro formato JSON, lo que viene del Postman
            string textoJsonRespuesta;

            // Instanciamos un httpClient porque nos comunicamos con protocolo http
            HttpClient mihttpClient = new HttpClient();

            // Pido la cadena de la Uri al método estático
            string miCadenaUrl = clsUriBaseJugador.getUriBase();

            // Convierto el Uri en una cadena de conexion y le añado /Jugador
            Uri miUri = new Uri($"{miCadenaUrl}Jugador");

            // await porque tengo que esperarlo, este Método es un Get
            miCodigoRespuesta = await mihttpClient.GetAsync(miUri);
            // En este caso no hay ni cabeceras ni cuerpo

            // Si me devuelve un 200… (int)miCodigoRespuesta.StatusCode == 200 
            // miCodigoRespuesta.IsSuccessStatusCode
            if ((int)miCodigoRespuesta.StatusCode == 200)
            {
                // Pedir la respuesta en formato string 
                textoJsonRespuesta = await mihttpClient.GetStringAsync(miUri);
                // Dejamos de comunicarnos cuando ya nos haya respondido
                mihttpClient.Dispose();

                // Cambiar el string por un listadoJugadores (desearializar) o un sólo Jugador en caso de Get por id
                listadoJugadores = JsonConvert.DeserializeObject<List<clsJugador>>(textoJsonRespuesta);
            }
            else if ((int)miCodigoRespuesta.StatusCode == 204)
            {
                // Una lista simplemente instanciada, es decir, vacía
                listadoJugadores = new List<clsJugador>();
            }

            else
            {
                // Aquí queremos el DisplayAlert
                listadoJugadores = null;
            }

            return listadoJugadores;

        }

        public async Task<HttpStatusCode> GuardarJugadorService(clsJugador jugador)
        {
            // Instanciamos un httpClient porque nos comunicamos con protocolo http
            HttpClient mihttpClient = new HttpClient();

            string datos; // El Body
            HttpContent contenido; // El Header

            // Pido la cadena de la Uri al método estático
            string miCadenaUrl = clsUriBaseJugador.getUriBase();

            // Convierto el Uri en una cadena de conexion y le añado /Jugador
            Uri miUri = new Uri($"{miCadenaUrl}Jugador");

            //Usaremos el Status de la respuesta para comprobar si ha borrado
            HttpResponseMessage miRespuesta = new HttpResponseMessage();
            try
            {
                datos = JsonConvert.SerializeObject(jugador);

                contenido = new StringContent(datos, System.Text.Encoding.UTF8, "application/json");
                miRespuesta = await mihttpClient.PostAsync(miUri, contenido);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return miRespuesta.StatusCode;
        }

        #endregion
    }
}
