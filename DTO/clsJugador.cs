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
            set { nick = value; } // ?
        }

        public int Puntuacion
        {
            get { return puntuacion; }
            set { puntuacion = value; } // ¿Es un campo calculado, no debería tener set. Pero debo ponérselo para hacer los Post?
        }
        // ¿Ninguno tiene set porque todo está cogido de una API?
        #endregion

        #region Constructores
        public clsJugador() 
        { 
        }

        public clsJugador(int id) // Seguramente este Constructor es innecesario aquí
        {
            this.id = id;
        }
        public clsJugador(string nick, int puntuacion)
        {
            this.nick = nick;
            this.puntuacion = puntuacion;
        }

        public clsJugador(int id, string nick, int puntuacion)
        {
            this.id = id;
            this.nick = nick;
            this.puntuacion = puntuacion;
        }
        #endregion
    }
}
