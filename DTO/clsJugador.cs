using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DTO
{
    public class clsJugador
    {
        #region Atributos
        private int id;
        private string nick;
        private int puntuacion;
        #endregion

        #region Propiedades
        public int Id
        {
            get { return id; }
        }

        public string Nick
        {
            get { return nick; }
            set { nick = value; } // Es necesario
        }

        public int Puntuacion
        {
            get { return puntuacion; }
            set { puntuacion = value; } // Es necesario
        }
        
        #endregion

        #region Constructores
        public clsJugador() 
        { 
        }

        public clsJugador(string nick, int puntuacion)
        {
            this.nick = nick;
            this.puntuacion = puntuacion;
        }
        #endregion
    }
}
