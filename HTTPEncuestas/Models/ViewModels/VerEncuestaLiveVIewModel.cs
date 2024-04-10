using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HTTPEncuestas.Models.ViewModels
{
    public class VerEncuestaLiveVIewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand DetenerEncuestaCommand { get; set; }
        public VerEncuestaLiveVIewModel()
        {
            DetenerEncuestaCommand = new RelayCommand(DetenerEncuesta);
        }

        private void DetenerEncuesta()
        {
            VMMsg.OnSetServerStatus(false);
            VMMsg.OnCambioDeVista("ConfigView");
        }
    }
}
