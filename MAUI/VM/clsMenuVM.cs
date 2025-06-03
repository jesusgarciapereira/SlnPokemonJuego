using MAUI.Views;
using MAUI.VM.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.VM
{
    public class clsMenuVM
    {
        #region Atributos
        private DelegateCommand botonSeleccionarPartida;
        private DelegateCommand botonRanking;

        #endregion

        #region Propiedades
        public DelegateCommand BotonSeleccionarPartida
        {
            get { return botonSeleccionarPartida; }
        }
        public DelegateCommand BotonRanking
        {
            get { return botonRanking; }
        }
        #endregion

        #region Constructores
        public clsMenuVM() 
        {
            botonSeleccionarPartida = new DelegateCommand(seleccionarPartidaExecute);
            botonRanking = new DelegateCommand(rankingExecute);
        }
        #endregion

        #region Métodos
        // Creo que este primer Método no hará falta aquí 

        /// <summary>
        /// Muestra un mensaje emergente.
        /// </summary>
        /// <param name="titulo">Título del mensaje.</param>
        /// <param name="cuerpo">Cuerpo del mensaje.</param>
        /// <param name="boton">Texto del botón de cierre.</param>
        private async void muestraMensaje(string titulo, string cuerpo, string boton)
        {
            await Application.Current.MainPage.DisplayAlert(titulo, cuerpo, boton);
        }

        /// <summary>
        /// Navega hacia la página indicada en el parámetro.
        /// </summary>
        private async void navegar(Page pagina)
        {
            await Application.Current.MainPage.Navigation.PushAsync(pagina);
        }

        #endregion

        #region Comandos

        /// <summary>
        /// Método asociado al execute del comando botonRanking que navega a RankingPage
        /// </summary>
        private void rankingExecute()
        {
            navegar(new RankingPage());
        }

        /// <summary>
        /// Método asociado al execute del comando botonSeleccionarPartida que navega a SeleccionarPartidaPage
        /// </summary>
        private void seleccionarPartidaExecute()
        {
            navegar(new SeleccionarPartidaPage());
        }
        #endregion
    }
}
