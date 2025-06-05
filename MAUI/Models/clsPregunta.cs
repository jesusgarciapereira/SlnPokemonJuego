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
    public class clsPregunta
    {
        #region Atributos
        clsPokemon pokemonPreguntado;
        List<clsPokemon> opciones;
        clsPokemon pokemonSeleccionado;
        bool esCorrecto;
        int tiempo;
        int cantOpciones;
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
            set { pokemonSeleccionado = value; }
        }
        public bool EsCorrecto
        {
            get { return esCorrecto; }
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

            // CuentaAtras();

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

        private async Task CuentaAtras()
        {
            while (this.tiempo > 0)
            {
                await Task.Delay(1000);
                this.tiempo--;
                NotifyPropertyChanged(nameof(Tiempo));
            }
        }
    }
    #endregion


}

