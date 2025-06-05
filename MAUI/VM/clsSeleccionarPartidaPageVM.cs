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
        private DelegateCommand<string> botonGeneracion;
        #endregion

        #region Propiedades
        public DelegateCommand<string> BotonGeneracion
        {
            get { return botonGeneracion; }
        }
        

     
        #endregion

        #region Constructores
        public clsSeleccionarPartidaPageVM()
        {
            botonGeneracion = new DelegateCommand<string>(generacionExecute);
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
        /// Método asociado al execute del comando botonGeneracion que navega a la página de Partida con la Generación escogida
        /// </summary>
        /// Pre: Sólo se permiten como parámetros los números 1, 2, 3 y 0 (De momento) en formato string
        /// Post: Ninguna
        /// <param name="generacion">Número correspondiente a la Generación, en formato string</param>
        private async void generacionExecute(string generacion)
        {
            List<clsPokemon> listadoPokemonPartida;

            if (await clsPokemonServiceBL.ObtenerListadoPokemonPorGeneracionBL(int.Parse(generacion)) != null) // Para evitar NullArgumentException
            {
                listadoPokemonPartida = await clsPokemonServiceBL.ObtenerListadoPokemonPorGeneracionBL(int.Parse(generacion));

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
