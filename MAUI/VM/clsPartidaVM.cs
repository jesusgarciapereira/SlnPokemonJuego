using DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.VM
{
    public class clsPartidaVM
    {
        #region Atributos
        private ObservableCollection<clsPokemon> listaPokemonComprobar;

        #endregion

        #region Propiedades
        public ObservableCollection<clsPokemon> ListaPokemonComprobar
        {
            get { return listaPokemonComprobar; }
        }
        #endregion

        #region Constructors
        public clsPartidaVM()
        {
        }
        public clsPartidaVM(ObservableCollection<clsPokemon> listaPokemonComprobar)
        {
            this.listaPokemonComprobar = listaPokemonComprobar;
        }
        #endregion
    }
}
