using BLMAUI;
using DTO;
using MAUI.Views;
using MAUI.VM.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.VM
{
    public class clsSeleccionarPartidaPageVM
    {
        #region Atributos
        private DelegateCommand<string> botonGeneracion1;
        private DelegateCommand<int> botonGeneracion2;
        private DelegateCommand<int> botonGeneracion3;
        private DelegateCommand<int> botonGeneracionTodas;
        #endregion

        #region Propiedades
        public DelegateCommand<string> BotonGeneracion1
        {
            get { return botonGeneracion1; }
        }
        public DelegateCommand<int> BotonGeneracion2
        {
            get { return botonGeneracion2; }
        }
        public DelegateCommand<int> BotonGeneracion3
        {
            get { return botonGeneracion3; }
        }
        public DelegateCommand<int> BotonGeneracionTodas
        {
            get { return botonGeneracionTodas; }
        }

        
        #endregion

        #region Constructores
        public clsSeleccionarPartidaPageVM()
        {
            botonGeneracion1 = new DelegateCommand<string>(generacion1Execute);
            // botonGeneracion2 = new DelegateCommand<int>(generacion2Execute);
            //botonGeneracion3 = new DelegateCommand(generacion3Execute);
            //botonGeneracionTodas = new DelegateCommand(generacionTodasExecute);
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
        /// Navega hacia la página de partida seleccionada pasando el objeto del parámetro.
        /// </summary>
        /// <param name="listadoPokemonPartida">Listado de Pokémon totales de una generación en concreto que recibirá la nueva página</param>
        private async void enviaDatosNavigation(List<clsPokemon> listadoPokemonPartida)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new PartidaPage(listadoPokemonPartida));
        }
        #endregion

        #region Comandos
        /// <summary>
        /// Método asociado al execute del comando botonGeneracion1 que navega a la página de Partida con la Primera Generación
        /// </summary>
        private async void generacion1Execute(string generacion)
        {
            List<clsPokemon> listadoPokemonPartida;

            if (await clsPokemonServiceBL.ObtenerListadoPokemonPorGeneracionBL(1) != null) // Para evitar NullArgumentException
            {
                listadoPokemonPartida = await clsPokemonServiceBL.ObtenerListadoPokemonPorGeneracionBL(1);

                if (listadoPokemonPartida.Count == 0)
                {
                    muestraMensaje("Sin datos", "No se encontraron datos, vuelva a intentarlo más tarde", "OK");
                }
                else
                {
                    enviaDatosNavigation(listadoPokemonPartida);
                }
            }
            else
            {
                muestraMensaje("Error", "Ha habido un problema en la Base de Datos, vuelva a intentarlo más tarde", "OK");
            }
        }

        /// <summary>
        /// Método asociado al execute del comando botonGeneracion2 que navega a la página de Partida con la Segunda Generación
        /// </summary>
        private async void generacion2Execute()
        {
            List<clsPokemon> listadoPokemonPartida;

            if (await clsPokemonServiceBL.ObtenerListadoPokemonPorGeneracionBL(2) != null) // Para evitar NullArgumentException
            {
                listadoPokemonPartida = await clsPokemonServiceBL.ObtenerListadoPokemonPorGeneracionBL(2);

                if (listadoPokemonPartida.Count == 0)
                {
                    muestraMensaje("Sin datos", "No se encontraron datos, vuelva a intentarlo más tarde", "OK");
                }
                else
                {
                    enviaDatosNavigation(listadoPokemonPartida);
                }
            }
            else
            {
                muestraMensaje("Error", "Ha habido un problema en la Base de Datos, vuelva a intentarlo más tarde", "OK");
            }
        }

        /// <summary>
        /// Método asociado al execute del comando botonGeneracion3 que navega a la página de Partida con la Tercera Generación
        /// </summary>
        private async void generacion3Execute()
        {
            List<clsPokemon> listadoPokemonPartida;

            if (await clsPokemonServiceBL.ObtenerListadoPokemonPorGeneracionBL(3) != null) // Para evitar NullArgumentException
            {
                listadoPokemonPartida = await clsPokemonServiceBL.ObtenerListadoPokemonPorGeneracionBL(3);

                if (listadoPokemonPartida.Count == 0)
                {
                    muestraMensaje("Sin datos", "No se encontraron datos, vuelva a intentarlo más tarde", "OK");
                }
                else
                {
                    enviaDatosNavigation(listadoPokemonPartida);
                }
            }
            else
            {
                muestraMensaje("Error", "Ha habido un problema en la Base de Datos, vuelva a intentarlo más tarde", "OK");
            }
        }

        /// <summary>
        /// Método asociado al execute del comando botonGeneracionTodas que navega a la página de Partida con todas las Generaciones
        /// </summary>
        private async void generacionTodasExecute()
        {
            List<clsPokemon> listadoPokemonPartida;

            if (await clsPokemonServiceBL.ObtenerListadoPokemonPorGeneracionBL(0) != null) // Para evitar NullArgumentException
            {
                listadoPokemonPartida = await clsPokemonServiceBL.ObtenerListadoPokemonPorGeneracionBL(0);

                if (listadoPokemonPartida.Count == 0)
                {
                    muestraMensaje("Sin datos", "No se encontraron datos, vuelva a intentarlo más tarde", "OK");
                }
                else
                {
                    enviaDatosNavigation(listadoPokemonPartida);
                }
            }
            else
            {
                muestraMensaje("Error", "Ha habido un problema en la Base de Datos, vuelva a intentarlo más tarde", "OK");
            }
        }
        #endregion
    }
}
