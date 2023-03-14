using PadresDeFamiliaApp.Models;
using PadresDeFamiliaApp.Services;
using PadresDeFamiliaApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PadresDeFamiliaApp.ViewModel
{
     public class AlumnosViewModel: INotifyPropertyChanged
     {
        public ObservableCollection<AlumnoDTO> AlumnoDTOs { get;set; }= new ObservableCollection<AlumnoDTO>();  
        public ObservableCollection<CalificacionDTO> calificacionDTOs { get;set; }=new ObservableCollection<CalificacionDTO>();
        public string errores { get; set; } 
        private AlumnosService alumnosService { get; set; } 
        public ICommand AlumnosCommand { get; set; }    
        public ICommand RegresarCommand { get; set; } 
        public ICommand VerAlumnosCommand { get; set; }
        public Usuario usuario { get; set; }
        public AlumnoDTO alumnosV { get; set; }

        public ICommand VerCalificacionesCommand { get; set; }

        CalificacionesView gradesView;


        public AlumnosViewModel(Usuario username)
        {
            usuario= username;
            VerAlumnosCommand = new Command(verAlumnos);
            alumnosService= new AlumnosService();
            AlumnosCommand = new Command(alumnos);
            RegresarCommand = new Command(regresar);
            VerCalificacionesCommand = new Command<AlumnoDTO>(Grades);
            alumnos();

        }

        private async void Grades(AlumnoDTO obj)
        {
            alumnosV = new AlumnoDTO();
            alumnosV = obj;
            actualizarDato(nameof(alumnosV));
            calificacionDTOs = new ObservableCollection<CalificacionDTO>();
            obj.Calificaciones.ForEach(x => calificacionDTOs.Add(x));
            actualizarDato(nameof(calificacionDTOs)); 
            gradesView= new CalificacionesView() { BindingContext= this };
            Application.Current.MainPage.Navigation.PushAsync(gradesView);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void regresar()
        {
            Application.Current.MainPage.Navigation.PopAsync(); 
        }

        private void alumnos()
        {
            verAlumnos();
            actualizarDato(nameof(AlumnoDTOs));
        }

        PaginaInicialView VerAlumnos;
        async void verAlumnos()
        {
            var info= await alumnosService.GetAlumnos(usuario.Id);
            info.ForEach(x => AlumnoDTOs.Add(x));
        }

        public void actualizarDato(string nombre)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
                
        }

    }
}
