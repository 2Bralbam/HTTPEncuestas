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
        public event Action<string> CambioDeVista;
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
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
