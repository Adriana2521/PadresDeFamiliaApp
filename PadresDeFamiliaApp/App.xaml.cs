namespace PadresDeFamiliaApp;

using PadresDeFamiliaApp.Models;
using PadresDeFamiliaApp.Views;
public partial class App : Application
{
	public static Usuario usuario { get; set; }= new Usuario();
	public App()
	{
		InitializeComponent();

		MainPage = new LoginView();
	}
}
