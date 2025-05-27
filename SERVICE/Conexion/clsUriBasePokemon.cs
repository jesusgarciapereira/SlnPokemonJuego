using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE.Conexion
{
    public class clsUriBasePokemon
    {
        #region Atributos
        private string uriBase;
        #endregion

        #region Propiedades
        public string UriBase
        {
            get { return uriBase; }
        }
        #endregion

        #region Constructores
        public clsUriBasePokemon()
        {
            this.uriBase = "https://pokeapi.co/api/v2/pokemon/";
        }

        public clsUriBasePokemon(string uriBase)
        {
            this.uriBase = uriBase;
        }
        #endregion

        #region Métodos
        public static string getUriBase()
        {
            return "https://pokeapi.co/api/v2/pokemon/";
        }
        #endregion
    }
}
