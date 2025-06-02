using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE.Conexion
{
    public class clsUriBaseJugador
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
        public clsUriBaseJugador()
        {
            this.uriBase = "https://apijugadores.azurewebsites.net";
        }

        public clsUriBaseJugador(string uriBase)
        {
            this.uriBase = uriBase;
        }
        #endregion

        #region Métodos
        public static string getUriBase()
        {
            return "https://apijugadores.azurewebsites.net/api";
        }
        #endregion
    }
}
