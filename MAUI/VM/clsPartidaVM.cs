using DTO;
using MAUI.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.VM
{
    public class clsPartidaVM : INotifyPropertyChanged
    {
        #region Atributos
        // private List<clsPokemon> listaPokemonGeneracion; // Creo que no se Bindea
        // private List<clsPokemon> listaOpcionesTotales; // Creo que no se Bindea
        private List<clsPregunta> listaPreguntas;
        private clsPregunta preguntaActual;
        private int cantPreguntas;
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

        public List<clsPregunta> ListaPreguntas
        {
            get { return listaPreguntas; }
        }

        public int CantPreguntas
        {
            get { return cantPreguntas; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // NotifyPropertyChanged(nameof(ListaLuchadoresConPuntuacionTotal));
        #endregion

        #region Constructores
        public clsPartidaVM()
        {
        }

        public clsPartidaVM(List<clsPokemon> listaPokemonGeneracion)
        {
            // this.listaPokemonGeneracion = listaPokemonGeneracion;
            this.cantPreguntas = 20;
            this.listaPreguntas = GeneraPreguntas(listaPokemonGeneracion, cantPreguntas, new clsPregunta().CantOpciones);

            for (int i = 0; listaPreguntas.Count > 0; i++) 
            {
                this.preguntaActual = listaPreguntas[0];
                // Notificar y esperar 5 segundos
            }
            
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
        /// Genera una lista de preguntas para la partida, donde cada pregunta contiene un Pokémon preguntado
        /// y un conjunto de opciones de respuesta.
        /// </summary>
        /// Pre: La lista original de Pokémon debe contener suficientes elementos para generar 
        ///      la cantidad de preguntas multiplicada por la cantidad de opciones por pregunta.
        /// Post: Se devuelve una lista de preguntas con las opciones y el Pokémon preguntado asignados.
        /// <param name="listaPokemonGeneracion">Lista original de Pokémon disponibles para generar las preguntas.</param>
        /// <param name="cantPreguntas">Número de preguntas que se desean generar.</param>
        /// <param name="cantOpciones">Cantidad de opciones por pregunta.</param>
        /// <returns>
        /// Una lista de objetos clsPregunta, cada uno con un Pokémon preguntado y su lista de opciones.
        /// Las opciones no se repiten entre preguntas.
        /// </returns>
        private List<clsPregunta> GeneraPreguntas(List<clsPokemon> listaPokemonGeneracion, int cantPreguntas, int cantOpciones)
        {
            List<clsPokemon> listaOpcionesTotales = ObtenerPokemonAleatorios(listaPokemonGeneracion, cantPreguntas * (new clsPregunta().CantOpciones));

            List<clsPregunta> preguntas = new List<clsPregunta>();
            List<clsPokemon> opcionesPregunta = new List<clsPokemon>();
            clsPokemon pokemonPreguntado;

            for (int i = 0; i < cantPreguntas; i++)
            {
                for (int j = 0; j < cantOpciones; j++)
                {
                    opcionesPregunta.Add(listaOpcionesTotales[j]);
                }

                listaOpcionesTotales.RemoveRange(0, cantOpciones);

                pokemonPreguntado = ObtenerPokemonAleatorios(opcionesPregunta, 1)[0];

                preguntas.Add(new clsPregunta(pokemonPreguntado, new List<clsPokemon>(opcionesPregunta)));

                opcionesPregunta.Clear();
            }

            return preguntas;
        }


        #endregion
    }

}