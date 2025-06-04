using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        public int CantOpciones
        {
            get { return cantOpciones; }
        }
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
        }
        #endregion


    }
}
