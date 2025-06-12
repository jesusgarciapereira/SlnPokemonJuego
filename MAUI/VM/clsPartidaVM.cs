using DTO;
using MAUI.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Net;
using MAUI.Views;
using MAUI.VM.Utils;
using BLMAUI;

namespace MAUI.VM
{
    public class clsPartidaVM : INotifyPropertyChanged
    {
        #region Atributos
        private List<clsPokemon> listaPokemonGeneracion;
        private List<clsPregunta> listaPreguntas;

        private clsPregunta preguntaActual;

        private int numRonda;
        private int puntosTotales;
        private int cantPreguntas;
        private int contadorPreguntas;

        private string nickJugador;

        private bool partidaVisible;
        private bool formularioVisible;

        private DelegateCommand botonGuardar;

        private IDispatcherTimer temporizador;
        #endregion

        #region Propiedades
        public clsPregunta PreguntaActual
        {
            get { return preguntaActual; }
        }
        public clsPokemon PokemonSeleccionado // Intermediario
        {
            get { return preguntaActual.PokemonSeleccionado; }
            set
            {
                preguntaActual.PokemonSeleccionado = value;
                ComprobarRespuesta();
            }
        }
        public int NumRonda
        {
            get { return numRonda; }
        }
        public int PuntosTotales
        {
            get { return puntosTotales; }
        }
        public string NickJugador
        {
            get { return nickJugador; }
            set
            {
                nickJugador = value;
                botonGuardar.RaiseCanExecuteChanged();
            }
        }
        public bool PartidaVisible
        {
            get { return partidaVisible; }
        }
        public bool FormularioVisible
        {
            get { return formularioVisible; }
        }
        public DelegateCommand BotonGuardar
        {
            get { return botonGuardar; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructores
        public clsPartidaVM()
        {
        }
        public clsPartidaVM(List<clsPokemon> listaPokemonPartida)
        {
            this.listaPokemonGeneracion = listaPokemonPartida;
            this.preguntaActual = new clsPregunta();

            this.cantPreguntas = 20;
            this.contadorPreguntas = 0;

            this.botonGuardar = new DelegateCommand(guardarJugadorPartidaExecute, habilitarGuardar);

            this.temporizador = Application.Current.Dispatcher.CreateTimer();
            temporizador.Interval = TimeSpan.FromSeconds(1); // Intervalo de segundos
            temporizador.Tick += RestarContador; // Lo que hacemos en cada segundo, evento RestarContador

            temporizador.Start();

            IniciarPartida();
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
        /// Navega hacia la página indicada en el parámetro.
        /// </summary>
        private async void navegar(Page pagina)
        {
            await Application.Current.MainPage.Navigation.PushAsync(pagina);
        }

        /// <summary>
        /// Inicializa una nueva partida generando la lista de preguntas, estableciendo la primera pregunta
        /// como actual y actualizando las propiedades necesarias para reflejar el inicio de la ronda.
        /// </summary>
        private void IniciarPartida()
        {
            this.listaPreguntas = CreaPreguntas();

            this.partidaVisible = true;

            this.preguntaActual = new clsPregunta(listaPreguntas[contadorPreguntas].PokemonPreguntado, listaPreguntas[contadorPreguntas].Opciones);
            NotifyPropertyChanged(nameof(PreguntaActual));

            this.numRonda = contadorPreguntas + 1;

        }

        /// <summary>
        /// Crea una lista de elementos de tipo clsPregunta, seleccionando aleatoriamente
        /// un conjunto de opciones por pregunta y determinando un Pokémon correcto entre ellas.
        /// </summary>
        /// <param name="cantPreguntas">Cantidad total de preguntas a generar.</param>
        /// <param name="cantOpciones">Cantidad de opciones disponibles por cada pregunta.</param>
        /// <returns>Una lista de objetos <see cref="clsPregunta"/>, cada uno con un Pokémon preguntado y una lista de opciones.</returns>
        private List<clsPregunta> CreaPreguntas()
        {
            List<clsPokemon> listaOpcionesTotales = ObtenerPokemonAleatorios(this.listaPokemonGeneracion, this.cantPreguntas * preguntaActual.CantOpciones);

            List<clsPregunta> preguntas = new List<clsPregunta>();
            List<clsPokemon> opcionesPregunta;
            clsPokemon pokemonPreguntado;
            clsPregunta pregunta;

            for (int i = 0; i < cantPreguntas; i++)
            {
                opcionesPregunta = new List<clsPokemon>();

                for (int j = 0; j < this.preguntaActual.CantOpciones; j++)
                {
                    opcionesPregunta.Add(listaOpcionesTotales[j]);
                }

                listaOpcionesTotales.RemoveRange(0, this.preguntaActual.CantOpciones);

                pokemonPreguntado = ObtenerPokemonAleatorios(opcionesPregunta, 1)[0];

                // pregunta = new clsPregunta(pokemonPreguntado, new List<clsPokemon>(opcionesPregunta));
                pregunta = new clsPregunta(pokemonPreguntado, opcionesPregunta);

                preguntas.Add(pregunta);
            }

            return preguntas;
        }

        /// <summary>
        /// Devuelve una selección aleatoria de un listado de Pokémon del parámetro sin repetir.
        /// </summary>
        /// Pre: El listado original siempre debe estar llena y la cantidad debe ser menor o igual al tamannio de la lista 
        /// Post: Ninguna
        /// <param name="listadoOriginal">Lista original de Pokémon de donde seleccionar.</param>
        /// <param name="cantidad">Número de Pokémon a seleccionar aleatoriamente.</param>
        /// <returns>
        /// Una lista con la cantidad solicitada de Pokémon seleccionados aleatoriamente,
        /// sin repeticiones.
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
        /// Disminuye el contador de tiempo de la pregunta actual si aún queda tiempo
        /// y no se ha seleccionado un Pokémon.
        /// Si el tiempo se agota o ya se ha realizado una selección, comprueba la respuesta.
        /// </summary>
        /// <param name="sender">Objeto que generó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>

        private void RestarContador(object sender, EventArgs e)
        {
            if (this.preguntaActual.Tiempo > 0 && this.preguntaActual.PokemonSeleccionado == null)
            {
                this.preguntaActual.Tiempo--;

            }
            else
            {
                ComprobarRespuesta();
            }
        }

        /// <summary>
        /// Comprueba la respuesta del jugador actual.
        /// Asigna los puntos correspondientes y avanza a la siguiente pregunta.
        /// </summary>

        private void ComprobarRespuesta()
        {
            AsignaPuntos();

            SiguientePregunta();
        }

        /// <summary>
        /// Asigna los puntos totales de la partida cada vez que se responde una pregunta
        /// </summary>
        private void AsignaPuntos()
        {
            if (preguntaActual.PokemonSeleccionado != null)
            {
                if ((bool)preguntaActual.EsCorrecto)
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
        /// Avanza a la siguiente pregunta en la partida.
        /// Si quedan preguntas, actualiza la pregunta actual y el número de ronda.
        /// Si no quedan preguntas, detiene el temporizador y cambia la visibilidad de la interfaz
        /// para mostrar el formulario final.
        /// </summary>
        private void SiguientePregunta()
        {
            if (this.contadorPreguntas < this.cantPreguntas - 1)
            {
                this.contadorPreguntas++;

                this.numRonda = contadorPreguntas + 1;
                NotifyPropertyChanged(nameof(NumRonda));

                this.preguntaActual = this.listaPreguntas[contadorPreguntas];
                NotifyPropertyChanged(nameof(PreguntaActual));
            }
            else
            {
                temporizador.Stop();

                partidaVisible = false;
                NotifyPropertyChanged(nameof(PartidaVisible));

                formularioVisible = true;
                NotifyPropertyChanged(nameof(FormularioVisible));
            }
        }

        /// <summary>
        /// Guarda la información del jugador y la partida de forma asíncrona,
        /// y luego navega de vuelta al menú principal.
        /// Se asegura de que la ventana de navegación no se muestre antes de completar el guardado.
        /// </summary>
        private async Task GuardarVolviendo()
        {
            // Porque no quiero que la ventana se vea antes de navegar
            await GuardarJugadorPartida();

            navegar(new MenuPage());
        }

        /// <summary>
        /// Llama a la capa BL para guardar un jugador.
        /// Muestra un mensaje al usuario según el resultado de la operación:
        /// - Éxito: Puntuación guardada correctamente.
        /// - Error 400: Datos enviados no válidos.
        /// - Error 404: Recurso no encontrado.
        /// - Excepción: Error general de comunicación o sistema.
        /// </summary>
        private async Task GuardarJugadorPartida()
        {
            clsJugador jugador = new clsJugador(this.nickJugador, this.puntosTotales);
            try
            {
                HttpStatusCode estado = await clsJugadorServiceBL.GuardarJugadorServiceBL(jugador);

                if ((int)estado == 200)
                {
                    muestraMensaje("Éxito", $"Puntuacion de \"{this.nickJugador}\" = {this.puntosTotales} guardada correctamente", "Aceptar");
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
            catch (Exception e)
            {
                muestraMensaje("Error", "Ha habido un problema, vuelva a intentarlo más tarde", "OK");
            }
        }
        #endregion

        #region Comandos

        /// <summary>
        /// Método asociado al execute del comando botonGuardar que guarda la información del jugador ny navega a la pantalla del menú principal.
        /// </summary>
        private void guardarJugadorPartidaExecute()
        {
            GuardarVolviendo();
        }

        /// <summary>
        /// Método asociado al canExecute del comando botonGuardar que habilita o deshabilita el botón de Guardar.
        /// </summary>
        /// <returns>True si se ha seleccionado si se ha escrito algún nick, false en caso contrario.</returns>
        private bool habilitarGuardar()
        {
            bool habilitado = false;

            if (nickJugador != null && !nickJugador.Equals(""))
            {
                habilitado = true;
            }

            return habilitado;
        }
        #endregion
    }

}