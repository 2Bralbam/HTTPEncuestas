using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static HTTPEncuestas.Models.ViewModels.VerEncuestaLiveVIewModel;

namespace HTTPEncuestas.Models.ViewModels
{
    public class EncuestasAnterioresViewModel : INotifyPropertyChanged
    {
        public ICommand BackCommand { get; set; }
        public DB? BaseDeDatos
        {
            get
            {
                string pathdb = "EncuestasDB/db.txt";
                // No necesitas crear el directorio aquí
                // Directory.CreateDirectory(Path.GetDirectoryName(pathdb));
                // Usa la ruta del archivo completo, no solo el directorio
                string json = File.ReadAllText(pathdb);
                DB? DB = JsonConvert.DeserializeObject<DB>(json);
                return DB;
            }
        }
        public EncuestasAnterioresViewModel()
        {
            BackCommand = new RelayCommand(() => { VMMsg.OnCambioDeVista("ConfigView"); });
            VMMsg.UpdateGraficaView += VMMsg_UpdateGraficaView;
            VMMsg.UpdateHistorialView+= VMMsg_OnUpdateHistorialView;
        }

        private void VMMsg_OnUpdateHistorialView()
        {
            OnPropertyChanged(nameof(BaseDeDatos));
        }

        private void VMMsg_UpdateGraficaView()
        {
            //throw new NotImplementedException();
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
