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

    public PartidaPage(List<clsPokemon> listaPokemonPartida)
    {
        InitializeComponent();
        BindingContext = new clsPartidaVM(listaPokemonPartida);

    }
}