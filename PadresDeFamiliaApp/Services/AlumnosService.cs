using Newtonsoft.Json;
using PadresDeFamiliaApp.Models;
using PadresDeFamiliaApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadresDeFamiliaApp.Services
{
     public class AlumnosService
     {

        HttpClient client= new HttpClient
        { 
            BaseAddress=new Uri("https://padres.sistemas19.com/")
        };
        public event Action<string> error;

        void Error(string message)
        {
            error?.Invoke(message); 
        }
        void ErrorJson(string json)
        {
            string objeto=JsonConvert.DeserializeObject<string>(json);
            if (objeto !=null)
            {
                error?.Invoke(objeto);
            }
        }
        public async Task<List<AlumnoDTO>> GetAlumnos(int id)
        {
            List<AlumnoDTO> alumnoDTOs= new List<AlumnoDTO>();
            var response = await client.GetAsync("api/alumno/" + id);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                alumnoDTOs=JsonConvert.DeserializeObject<List<AlumnoDTO>>(json);    
            }
            if (alumnoDTOs !=null)
            {
                return alumnoDTOs;
            }
            else
            {
                return new List<AlumnoDTO>();
            }
        }
        public async Task<List<CalificacionDTO>>GetGrades(int id)
        {
            List<CalificacionDTO> Calificaciones= new List<CalificacionDTO>();
            var response = await client.GetAsync("api/alumno/" + id);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Calificaciones=JsonConvert.DeserializeObject<List<CalificacionDTO>>(json);  
            }
            if (Calificaciones !=null)
            {
                return Calificaciones;
            }
            else
            {
                return new List<CalificacionDTO>();
            }
        }
     }
}
