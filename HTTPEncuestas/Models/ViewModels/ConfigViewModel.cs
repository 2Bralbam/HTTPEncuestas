using GalaSoft.MvvmLight.Command;
using HTTPEncuestas.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HTTPEncuestas.Models.ViewModels
{
    public class ConfigViewModel:INotifyPropertyChanged
    {
        HTTPServer _server = new();
        public string TituloEncuesta { get; set; } = "";
        //public List<string> Preguntas { get { return preguntas; } set { preguntas = value; OnPropertyChanged(nameof(Preguntas));  } }
        //private List<string> preguntas { get; set; }
        public ObservableCollection<string> Preguntas { get; set; }
        public string SelectedPregunta { get; set; }
        public ICommand IniciarEncuestaCommand { get; set; }
        public ICommand VerAnterioresEncuestasCommand { get; set; }
        public ICommand AgregarPreguntaCommand { get; set; }
        public ICommand QuitarPreguntaCommand { get; set; }
        private bool FirstEncuesta = true;
        public string Pregunta { get; set; } = "";
        public ConfigViewModel()
        {
            IniciarEncuestaCommand = new RelayCommand(IniciarEncuesta);
            AgregarPreguntaCommand = new RelayCommand(AgregarPregunta);
            QuitarPreguntaCommand = new RelayCommand(() => { 
                if(Preguntas==null)
                {
                    MessageBox.Show("No hay preguntas para quitar", "Alerta");
                    return;
                }
                if(SelectedPregunta!=null)
                Preguntas.Remove(SelectedPregunta);
            });
            VerAnterioresEncuestasCommand = new RelayCommand(() => { VMMsg.OnCambioDeVista("EncuestasView"); });
            Preguntas = new ObservableCollection<string>();
        }

        private void AgregarPregunta()
        {
            if (Pregunta == null)
            {
                MessageBox.Show("Por favor, coloca una pregunta", "Alerta");
                return;
            }
            Preguntas.Add(Pregunta);
            Pregunta = "";
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void IniciarEncuesta()
        {
            FirstEncuesta = false;
            List<GraficaModel> PreguntasParaGrafica = new();
            Parallel.ForEach(Preguntas,P => 
            {
                GraficaModel m = new() 
                {
                    Pregunta = P,
                    Promedio = 50,
                    Tamaño = 100
                };
                PreguntasParaGrafica.Add(m);
            });
            VMMsg.OnUpdateGraficasLive(PreguntasParaGrafica);
            if (!FirstEncuesta)
            {
                VMMsg.UpdateGraficas();
            }
            if(!string.IsNullOrEmpty(TituloEncuesta))
            {
                if(Preguntas.Count == 0)
                {
                    MessageBox.Show("Por favor, agrega al menos una pregunta", "Alerta");
                    return;
                }
                _server.TituloEncuesta = TituloEncuesta;
                VMMsg.OnCambioDeVista("VerEncuestaLiveView");
                _server.StartServer();
            }
            else
            {
                MessageBox.Show("Por favor, coloca un título a la encuesta","Alerta");
            }
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
