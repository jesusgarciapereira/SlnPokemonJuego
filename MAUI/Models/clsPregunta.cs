using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.Models
{
    public class clsPregunta : INotifyPropertyChanged
    {
        #region Atributos
        private clsPokemon pokemonPreguntado;
        private List<clsPokemon> opciones;
        private clsPokemon pokemonSeleccionado;
        private bool esCorrecto;
        private int tiempo;
        private int cantOpciones;
        private IDispatcherTimer temporizador;
        #endregion

        #region Propiedades
        public clsPokemon PokemonPreguntado
        {
            get { return pokemonPreguntado; }
        }
        public List<clsPokemon> Opciones
        {
            get { return opciones; }
        }
        public clsPokemon PokemonSeleccionado
        {
            get { return pokemonSeleccionado; }
            set
            {
                pokemonSeleccionado = value;
                NotifyPropertyChanged(nameof(PokemonSeleccionado));

                if (pokemonSeleccionado != null)
                {
                    temporizador.Stop();
                }
            }
        }
        public bool EsCorrecto
        {
            get
            {
                //this.esCorrecto = false;

                //if (pokemonPreguntado.Equals(pokemonSeleccionado))
                //{
                //    this.esCorrecto = true;
                //}

                //return esCorrecto;

                return pokemonPreguntado.Equals(pokemonSeleccionado);
            }
        }
        public int Tiempo
        {
            get { return tiempo; }
            // set { tiempo = value; }
        }

        public int CantOpciones
        {
            get { return cantOpciones; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructores
        public clsPregunta()
        {
            this.cantOpciones = 4;
            this.tiempo = 5;
        }

        public clsPregunta(clsPokemon pokemonPreguntado, List<clsPokemon> opciones) : this()
        {
            this.pokemonPreguntado = pokemonPreguntado;
            this.opciones = opciones;

            this.temporizador = Application.Current.Dispatcher.CreateTimer();
            temporizador.Interval = TimeSpan.FromSeconds(1); // Intervalo de segundos
            temporizador.Tick += RestarContador; // Lo que hacemos en cada segundo, evento RestarContador

            temporizador.Start();

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

        private void RestarContador(object sender, EventArgs e)
        {
            if (tiempo > 0)
            {
                tiempo--;
                NotifyPropertyChanged(nameof(Tiempo));
            }
            else
            {
                temporizador.Stop();

                // Esto creo que no hace falta
                //tiempo = 5;
                //NotifyPropertyChanged(nameof(Tiempo));

                //temporizador.Start();
            }
        }
        #endregion

    }



}

