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
        // Ninguno tiene set porque todo está cogido de una API
        #endregion

        #region Constructores
        public clsPokemon()
        {
        }

        public clsPokemon(int idPokemon, string nombre, string imagen)
        {
            this.idPokemon = idPokemon;
            this.nombre = nombre;
            this.imagen = imagen;
        }
        #endregion
    }
}
