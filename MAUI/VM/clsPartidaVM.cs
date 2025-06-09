using DTO;
using MAUI.Models;
using SERVICE;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net;
using MAUI.Views;

namespace MAUI.VM
{
    public class clsPartidaVM : INotifyPropertyChanged
    {
        #region Atributos
        // private List<clsPokemon> listaPokemonGeneracion; // Creo que no se Bindea
        // private List<clsPokemon> listaOpcionesTotales; // Creo que no se Bindea
        private List<clsPregunta> listaPreguntas;
        private clsPregunta preguntaActual;
        private int numRonda;
        private int puntosTotales;
        private int cantPreguntas;
        private clsJugador jugador;
        #endregion

        #region Propiedades
        //public List<clsPokemon> ListaPokemonGeneracion
        //{
        //    get { return listaPokemonGeneracion; }
        //}

        //public List<clsPokemon> ListaOpcionesTotales
        //{
        //    get { return listaOpcionesTotales; }
        //}

        //public List<clsPregunta> ListaPreguntas
        //{
        //    get { return listaPreguntas; }
        //}

        public clsPregunta PreguntaActual
        {
            get { return preguntaActual; }
        }

        public int NumRonda
        {
            get { return numRonda; }
        }

        public int PuntosTotales
        {
            get { return puntosTotales; }
        }

        public int CantPreguntas
        {
            get { return cantPreguntas; }
        }

        public clsJugador Jugador
        {
            get { return jugador; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // NotifyPropertyChanged(nameof(ListaLuchadoresConPuntuacionTotal));
        #endregion

        #region Constructores
        public clsPartidaVM()
        {
        }

        public clsPartidaVM(List<clsPokemon> listaPokemonPartida)
        {
            // this.listaPokemonGeneracion = listaPokemonGeneracion;
            this.preguntaActual = new clsPregunta();

            this.cantPreguntas = 20;
            this.listaPreguntas = CreaPreguntas(listaPokemonPartida, cantPreguntas, preguntaActual.CantOpciones);

            EjecutarPartida();

            
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Lanza el evento PropertyChanged para notificar a la vista que una propiedad ha cambiado.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad que cambió.</param>
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Navega hacia la página indicada en el parámetro.
        /// </summary>
        private async void navegar(Page pagina)
        {
            await Application.Current.MainPage.Navigation.PushAsync(pagina);
        }

        /// <summary>
        /// Muestra un mensaje emergente.
        /// </summary>
        /// <param name="titulo">Título del mensaje.</param>
        /// <param name="cuerpo">Cuerpo del mensaje.</param>
        /// <param name="boton">Texto del botón de cierre.</param>
        private async void muestraMensaje(string titulo, string cuerpo, string boton)
        {
            await Application.Current.MainPage.DisplayAlert(titulo, cuerpo, boton);
        }

        /// <summary>
        /// Devuelve una selección aleatoria de Pokémon sin repetir.
        /// </summary>
        /// Pre: El listado original siempre debe estar llena y la cantidad debe ser menor o igual al tamannio de la lista 
        /// Post: Ninguna
        /// <param name="listadoOriginal">Lista original de Pokémon de donde seleccionar.</param>
        /// <param name="cantidad">Número de Pokémon a seleccionar aleatoriamente.</param>
        /// <returns>
        /// Una lista con la cantidad solicitada de Pokémon seleccionados aleatoriamente,
        /// sin repeticiones. Si la cantidad es mayor que el número de elementos disponibles,
        /// se devuelven todos los Pokémon mezclados.
        /// </returns>
        private static List<clsPokemon> ObtenerPokemonAleatorios(List<clsPokemon> listadoOriginal, int cantidad)
        {
            // Copiamos la lista original para no modificarla
            List<clsPokemon> copiaListadoParametro = new List<clsPokemon>(listadoOriginal);
            List<clsPokemon> listadoSeleccionadoAleatorio = new List<clsPokemon>();

            Random random = new Random(); // Generador de números aleatorios

            int indiceAleatorio;
            clsPokemon pokemonSeleccionado;

            for (int i = 0; i < cantidad; i++)
            {
                indiceAleatorio = random.Next(copiaListadoParametro.Count);      // Índice aleatorio
                pokemonSeleccionado = copiaListadoParametro[indiceAleatorio];    // Seleccionamos el Pokémon
                listadoSeleccionadoAleatorio.Add(pokemonSeleccionado);           // Lo añadimos a la nueva lista
                copiaListadoParametro.RemoveAt(indiceAleatorio);                 // Lo eliminamos para evitar repeticiones
            }

            return listadoSeleccionadoAleatorio;
        }

        /// <summary>
        /// Crea una lista de preguntas para la partida, donde cada pregunta contiene un Pokémon preguntado
        /// y un conjunto de opciones de respuesta.
        /// </summary>
        /// Pre: La lista original de Pokémon debe contener suficientes elementos para generar 
        ///      la cantidad de preguntas multiplicada por la cantidad de opciones por pregunta.
        /// Post: Se devuelve una lista de preguntas con las opciones y el Pokémon preguntado asignados.
        /// <param name="listaPokemonPartida">Lista original de Pokémon disponibles para generar las preguntas.</param>
        /// <param name="cantPreguntas">Número de preguntas que se desean generar.</param>
        /// <param name="cantOpciones">Cantidad de opciones por pregunta.</param>
        /// <returns>
        /// Una lista de objetos clsPregunta, cada uno con un Pokémon preguntado y su lista de opciones.
        /// Las opciones no se repiten entre preguntas.
        /// </returns>
        private List<clsPregunta> CreaPreguntas(List<clsPokemon> listaPokemonPartida, int cantPreguntas, int cantOpciones)
        {
            List<clsPokemon> listaOpcionesTotales = ObtenerPokemonAleatorios(listaPokemonPartida, cantPreguntas * preguntaActual.CantOpciones);

            List<clsPregunta> preguntas = new List<clsPregunta>();
            List<clsPokemon> opcionesPregunta;
            clsPokemon pokemonPreguntado;
            clsPregunta pregunta;

            for (int i = 0; i < cantPreguntas; i++)
            {
                opcionesPregunta = new List<clsPokemon>();

                for (int j = 0; j < cantOpciones; j++)
                {
                    opcionesPregunta.Add(listaOpcionesTotales[j]);
                }

                listaOpcionesTotales.RemoveRange(0, cantOpciones);

                pokemonPreguntado = ObtenerPokemonAleatorios(opcionesPregunta, 1)[0];

                // pregunta = new clsPregunta(pokemonPreguntado, new List<clsPokemon>(opcionesPregunta));
                pregunta = new clsPregunta(pokemonPreguntado, opcionesPregunta);

                preguntas.Add(pregunta);
            }

            return preguntas;
        }

        /// <summary>
        /// Muestra cada pregunta de la lista durante un número determinado de segundos.
        /// </summary>
        /// Pre: La lista de preguntas debe estar previamente generada y contener elementos.
        /// Post: Se muestra una pregunta a la vez, actualizando la propiedad 'PreguntaActual' cada intervalo de tiempo.
        /// <param name="segundos">Cantidad de segundos que se muestra cada pregunta antes de pasar a la siguiente.</param>
        /// <returns>Una tarea asincrónica que gestiona la temporización entre preguntas.</returns>
        private async Task EjecutarPartida()
        {
            for (int i = 0; i < listaPreguntas.Count; i++)
            {
                this.preguntaActual = new clsPregunta(listaPreguntas[i].PokemonPreguntado, listaPreguntas[i].Opciones);

                NotifyPropertyChanged(nameof(PreguntaActual));

                this.numRonda = i + 1;

                NotifyPropertyChanged(nameof(NumRonda));

                while (preguntaActual.PokemonSeleccionado == null && preguntaActual.Tiempo > 0)
                {
                    await Task.Delay(100); // Espera 100ms antes de volver a comprobar
                }

                AsignaPuntos();

            }

            GuardarJugadorPartida();

            navegar(new MenuPage());
        }

        /// <summary>
        /// Asigna los puntos totales de la partida cada vez que se responde una pregunta
        /// </summary>
        private void AsignaPuntos()
        {
            if (preguntaActual.PokemonSeleccionado != null)
            {
                if (preguntaActual.EsCorrecto)
                {
                    this.puntosTotales += preguntaActual.Tiempo;
                }
                else
                {
                    this.puntosTotales -= 1;
                }

                NotifyPropertyChanged(nameof(PuntosTotales));
            }
        }

        /// <summary>
        /// Guarda la Partida del jugador llamando a la API
        /// </summary>
        /// <returns></returns>
        private async Task GuardarJugadorPartida()
        {

            this.jugador = new clsJugador("Jesús", this.puntosTotales);

            HttpStatusCode estado = await clsJugadorService.GuardarJugadorService(this.jugador);

            if ((int)estado == 200)
            {
                muestraMensaje("Éxito", $"Puntuacion de \"{this.jugador.Nick}\" = {this.jugador.Puntuacion} guardada correctamente", "Aceptar");
            }
            else if ((int)estado == 400)
            {
                muestraMensaje("Error de envío", "La información enviada no es válida. Por favor, revisa los campos e inténtalo de nuevo.", "Aceptar");
            }
            else if ((int)estado == 404)
            {
                muestraMensaje("No encontrado", "No se ha encontrado el recurso solicitado. Es posible que ya no exista o que haya un error en la dirección.", "Aceptar");
            }
        }


        //private void MostrarPreguntasPorSegundos()
        //{
        //    int indice = 0;

        //    this.preguntaActual = listaPreguntas[indice];
        //    NotifyPropertyChanged(nameof(PreguntaActual));

        //    if (preguntaActual.Tiempo == 0)
        //    {
        //        indice++;

        //        this.preguntaActual = listaPreguntas[indice];
        //        NotifyPropertyChanged(nameof(PreguntaActual));
        //    }

        //}

        #endregion
    }

}