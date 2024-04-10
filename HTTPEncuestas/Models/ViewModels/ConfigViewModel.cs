using GalaSoft.MvvmLight.Command;
using HTTPEncuestas.Services;
using System;
using System.Collections.Generic;
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
        public string TituloEncuesta { get; set; }
        public ICommand IniciarEncuestaCommand { get; set; }
        public ICommand VerAnterioresEncuestasCommand { get; set; }
        public ConfigViewModel()
        {
            IniciarEncuestaCommand = new RelayCommand(IniciarEncuesta);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void IniciarEncuesta()
        {
            if(TituloEncuesta!= null)
            {
                _server.TituloEncuesta = TituloEncuesta;
                _server.StartServer();
            }
            else
            {
                MessageBox.Show("Por favor, coloca un título a la encuesta","Alerta");
            }
        }
    }
}
