using Newtonsoft.Json;
using PadresDeFamiliaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PadresDeFamiliaApp.Services
{
    public class PadresService
    {
        HttpClient client = new HttpClient();
        public PadresService()
        {
            client.BaseAddress = new Uri("https://padres.sistemas19.com/");
        }
        public event Action<string> OnError;    

        public async Task<Usuario> GetUser(string username,string password)
        {
            if (string.IsNullOrWhiteSpace(username)&& string.IsNullOrWhiteSpace(password))
            {
                OnError?.Invoke("Ingreso un User o Password Incorrecto");  
            }
            Usuario usuario = new Usuario() 
            { 
                NombreUsuario= username,
                Password= password  
            };
            var json =JsonConvert.SerializeObject(usuario);
            var response = client.PostAsync("api/usuario/login", new StringContent(json, Encoding.UTF8, "application/json"));
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                json = await response.Result.Content.ReadAsStringAsync();
                usuario=JsonConvert.DeserializeObject<Usuario>(json);
                return usuario;
            }
            else
            {
                var error= await response.Result.Content.ReadAsStringAsync();
                OnError?.Invoke(error);
                return null;
            }
        }







    }
}
