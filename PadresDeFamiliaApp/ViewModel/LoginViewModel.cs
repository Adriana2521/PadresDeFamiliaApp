using PadresDeFamiliaApp.Models;
using PadresDeFamiliaApp.Services;
using PadresDeFamiliaApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PadresDeFamiliaApp.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public Usuario usuario { get; set; }
        PadresService loginservice;
        public ICommand loginCommand { get; set; }

        public LoginViewModel()
        {
            loginCommand = new Command(login);
            usuario = new();
            loginservice= new PadresService();
            loginservice.OnError += Loginservice_OnError;
        }

        [Obsolete]
        private async void Loginservice_OnError(string error)
        {
            await Device.InvokeOnMainThreadAsync(async () =>
            {
                await App.Current.MainPage.DisplayAlert("Error", error, "OK");
            });
        }

        private async void login()
        {
            try
            {
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {
                    usuario = await loginservice.GetUser(NombreUsuario, Password);
                    if (usuario != null)
                    {
                        App.usuario= usuario;   
                        App.Current.MainPage = new NavigationPage(new PaginaInicialView() { BindingContext = new AlumnosViewModel(usuario) });


                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("error", "No tienes Conexion a Internet", "OK");
                }

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
