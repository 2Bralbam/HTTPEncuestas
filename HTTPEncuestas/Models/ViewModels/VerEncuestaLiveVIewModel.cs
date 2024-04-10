using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HTTPEncuestas.Models.ViewModels
{
    public class VerEncuestaLiveVIewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand DetenerEncuestaCommand { get; set; }
        private ObservableCollection<GraficaModel>? listbarras { get; set; } = new();
        public ObservableCollection<GraficaModel> ListaBarras { 
            get { 
                    return new ObservableCollection<GraficaModel>(VMMsg.ListaDatos); 
                
            }
        }
        public VerEncuestaLiveVIewModel()
        {
            DetenerEncuestaCommand = new RelayCommand(DetenerEncuesta);
            VMMsg.UpdateGraficaView += UpdateBarras;
        }

        private void UpdateBarras()
        {
            OnPropertyChanged(nameof(ListaBarras));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void DetenerEncuesta()
        {
            VMMsg.OnSetServerStatus(false);
            VMMsg.OnCambioDeVista("ConfigView");
        }
    }
}
