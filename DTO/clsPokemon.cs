using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTO
{
    public class clsPokemon
    {
        #region Atributos
        private int idPokemon; // id
        private string nombre; // name
        private string imagen;
        #endregion

        #region Propiedades
        [JsonPropertyName("id")]
        public int IdPokemon
        {
            get { return idPokemon; }
        }

        [JsonPropertyName("name")]
        public string Nombre
        {
            get { return nombre; }
        }

        public string Imagen
        {
            get { return imagen; }
        }
        // ¿Ninguno tiene set porque todo está cogido de una API?
        #endregion

        #region Constructores
        public clsPokemon()
        {
        }

        // Quizás lo necesite, o no
        public clsPokemon(int idPokemon, string nombre)
        {
            this.idPokemon = idPokemon;
            this.nombre = nombre;
        }

        public clsPokemon(int idPokemon, string nombre, string imagen)
        {
            this.idPokemon = idPokemon;
            this.nombre = nombre;
            this.imagen = imagen;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Determina si el objeto especificado es igual al objeto actual según el id del Pokémon.
        /// </summary>
        /// <param name="obj">El objeto a comparar con el actual.</param>
        /// <returns>true si los objetos tienen el mismo id; en caso contrario, false.</returns>
        public override bool Equals(object obj)
        {
            bool resultado = false;

            clsPokemon parametroPokemon;   

            if (obj is clsPokemon)
            {
                parametroPokemon = (clsPokemon)obj;

                resultado = this.idPokemon == parametroPokemon.idPokemon;
            }

            return resultado;
        }

        /// <summary>
        /// Devuelve un código hash para el objeto actual, basado en su id.
        /// </summary>
        /// <returns>Código hash del id.</returns>
        public override int GetHashCode()
        {
            return idPokemon.GetHashCode();
        }

        #endregion
    }
}
