﻿using DTO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SERVICE.Conexion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Diagnostics.Metrics;

namespace SERVICE
{
    public class clsPokemonService
    {
        /// <summary>
        /// Realiza una solicitud HTTP a la PokeAPI para obtener un listado de Pokémon con paginación mediante offset y limit según la generación del parámetro.
        /// Pre: Sólo se permiten como parámetros los números 1, 2, 3 y 0 (De momento) 
        /// Post: Ninguna
        /// </summary>
        /// <param name="generacion">Número correspondiente a la generación</param>
        /// <returns>
        /// Una lista de objetos clsPokemon:
        /// - Llena si la API devuelve datos (código 200).
        /// - Vacía si no hay datos disponibles (código 204).
        /// - Null si ocurre un error en la solicitud (código 400).
        /// </returns>
        public static async Task<List<clsPokemon>> ObtenerListadoPokemonPorGeneracionService(int generacion)
        {
            // Lista que contendrá los Pokémon recuperados
            List<clsPokemon> listadoPokemon;

            // Variables auxiliares del objeto de tipo clsPokemon
            clsPokemon pokemon;
            string urlPokemon;
            int idPokemon;
            string nombre;
            string imagen;

            // Variables para la Uri
            int offset;
            int limit;

            // La respuesta: El 404, el 200, etc…
            HttpResponseMessage miCodigoRespuesta;

            // El listado en futuro formato JSON, lo que viene del Postman
            string textoJsonRespuesta;

            // Instanciamos un httpClient porque nos comunicamos con protocolo http
            HttpClient mihttpClient = new HttpClient();

            // Pido la cadena de la Uri al método estático
            string miCadenaUrl = clsUriBasePokemon.getUriBase();

            // Asignación de variables de la Uri según el parámetro
            switch (generacion)
            {
                // Primera generación
                case 1:
                    offset = 0;
                    limit = 151;
                    break;

                // Segunda generación
                case 2:
                    offset = 151;
                    limit = 100;
                    break;

                // Tercera generación
                case 3:
                    offset = 251;
                    limit = 135;
                    break;

                // Las 3 generaciones
                case 0:
                    offset = 0;
                    limit = 386;
                    break;

                // NO DEBERÍA SUCEDER
                default:
                    offset = -1;
                    limit = -1;
                    break;
            }

            // Convierto el Uri en una cadena de conexion y le añado las variables de la Uri
            Uri miUri = new Uri($"{miCadenaUrl}?offset={offset}&limit={limit}");

            // Objeto JObject con el json completo recibido
            JObject jsonCompleto;
            // El array "results" del JSON
            JArray arrayResults;

            // await porque tengo que esperarlo, este Método es un Get
            miCodigoRespuesta = await mihttpClient.GetAsync(miUri);

            // Si me devuelve un 200…
            if ((int)miCodigoRespuesta.StatusCode == 200)
            {
                // Pedir la respuesta en formato string 
                textoJsonRespuesta = await mihttpClient.GetStringAsync(miUri);
                // Dejamos de comunicarnos cuando ya nos haya respondido
                mihttpClient.Dispose();

                // Instanciamos el listado que vamos a devolver
                listadoPokemon = new List<clsPokemon>();

                // Convertimos el string JSON a un objeto JObject
                jsonCompleto = JsonConvert.DeserializeObject<JObject>(textoJsonRespuesta);
                // Obtenemos el array "results" del JSON
                arrayResults = (JArray)jsonCompleto["results"];

                // Iteramos por cada resultado en el array
                foreach (JObject resultado in arrayResults)
                {
                    // Obtenemos la URL individual del Pokémon
                    urlPokemon = (string)resultado["url"];

                    // Extraemos el id del final de la urlPokemon
                    urlPokemon = urlPokemon.TrimEnd('/'); // Quitar la última barra
                    idPokemon = int.Parse(urlPokemon.Replace(miCadenaUrl + "/", "")); // Quedarse sólo con el número del id y transformarlo en int

                    // Obtenemos el nombre
                    nombre = (string)resultado["name"];

                    // Construimos la URL de la imagen oficial del Pokémon
                    imagen = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/{idPokemon}.png";

                    // Creamos el objeto Pokémon y lo añadimos a la lista
                    pokemon = new clsPokemon(idPokemon, nombre, imagen);
                    listadoPokemon.Add(pokemon);
                }

            }
            else if ((int)miCodigoRespuesta.StatusCode == 204)
            {
                // Una lista simplemente instanciada, es decir, vacía
                listadoPokemon = new List<clsPokemon>();
            }
            else
            {
                // Aquí queremos el DisplayAlert
                listadoPokemon = null;
            }

            return listadoPokemon;
        }

    }
}
