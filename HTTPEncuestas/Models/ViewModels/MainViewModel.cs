using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPEncuestas.Models.ViewModels
{
    public class MainViewModel:INotifyPropertyChanged
    {
        public event EventHandler<string> CambioDeVista;
        
        private string? NVista { get; set; }

        public string? NombreVista
        {
            get { return NVista; }
            set {
                if (NVista != value)
                {
                    NVista = value;
                    OnPropertyChanged("NombreVista");
                }
            }
        }
        public MainViewModel()
        {
            NombreVista = "ConfigView";
            VMMsg.CambioDeVista += VMMsg_CambioDeVista;
        }

        private void VMMsg_CambioDeVista(object? sender, string e)
        {
            if(!string.IsNullOrEmpty(e))
            {
                NombreVista = e;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
