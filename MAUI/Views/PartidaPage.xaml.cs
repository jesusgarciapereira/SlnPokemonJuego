using DTO;
using MAUI.VM;
using System.Collections.ObjectModel;

namespace MAUI.Views;

public partial class PartidaPage : ContentPage
{
	public PartidaPage()
	{
		InitializeComponent();
	}

    public PartidaPage(List<clsPokemon> listadoPokemonPartida)
    {
        InitializeComponent();
        BindingContext = new clsPartidaVM(new ObservableCollection<clsPokemon>(listadoPokemonPartida));

    }
}