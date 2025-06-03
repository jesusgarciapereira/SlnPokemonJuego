using BLMAUI;
using DTO;
using MAUI.VM.Utils;
using SERVICE;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.VM
{
    public class clsRankingVM :INotifyPropertyChanged
    {
        #region Atributos
        private ObservableCollection<clsJugador> listaJugadoresVisible;
        private DelegateCommand botonActualizar;
        private bool cargando;
        private bool listaVacia;
        #endregion

        #region Propiedades
        public ObservableCollection<clsJugador> ListaJugadoresVisible
        {
            get { return listaJugadoresVisible; }
        }

        public DelegateCommand BotonActualizar
        {
            get { return botonActualizar; }
        }

        public bool Cargando
        {
            get { return cargando; }
        }

        public bool ListaVacia
        {
            get { return listaVacia; }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructores
        public clsRankingVM()
        {
            // Se ejecuta también al inicio
            actualizarExecute();

            botonActualizar = new DelegateCommand(actualizarExecute); // Podríamos poner un CanExecute() para que no se pulse tan consecutivamente
        }
        #endregion

        #region Funciones
        /// <summary>
        /// Lanza el evento PropertyChanged para notificar a la vista que una propiedad ha cambiado.
        /// </summary>
        /// <param name="propertyName">Nombre de la propiedad que cambió.</param>
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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
        #endregion

        #region Comandos
        /// <summary>
        /// Método asociado al execute del comando botonMostrar que asigna a la lista visible la lista obtenida de la API
        /// </summary>
        private async void actualizarExecute()
        {
            // Para que el label de lista vacía sea invisible
            listaVacia = false;
            NotifyPropertyChanged(nameof(ListaVacia));

            // Para activar el ActivityIndicator
            cargando = true;
            NotifyPropertyChanged(nameof(Cargando));

            if (await clsJugadorServiceBL.ObtenerListadoJugadoresServiceBL() != null) // Para evitar NullArgumentException
            {

                listaJugadoresVisible = new ObservableCollection<clsJugador>(await clsJugadorServiceBL.ObtenerListadoJugadoresServiceBL());
                NotifyPropertyChanged(nameof(ListaJugadoresVisible));


                if (listaJugadoresVisible.Count == 0)
                {
                    // Cambia la visibilidad del label
                    listaVacia = true;
                    NotifyPropertyChanged(nameof(ListaVacia));
                }
            }
            else
            {
                muestraMensaje("Error", "Ha habido un problema en la Base de Datos, vuelva a intentarlo más tarde", "OK");
            }

            // Al final siempre desactivamos el ActivityIndicator
            cargando = false;
            NotifyPropertyChanged(nameof(Cargando));

        }
        #endregion
    }
}
