using GalaSoft.MvvmLight.Command;
using HTTPEncuestas.Models.Entities;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace HTTPEncuestas.Models.ViewModels
{
    public class VerEncuestaLiveVIewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand DetenerEncuestaCommand { get; set; }
        private ObservableCollection<GraficaModel>? listbarras { get; set; } = new();
        public ObservableCollection<GraficaModel> ListaBarras
        {
            get
            {
                return new ObservableCollection<GraficaModel>(VMMsg.ListaDatos);

            }
        }
        public ObservableCollection<UltimaRespuesta> UltimasRespuestas { get; set; } = new();
        public VerEncuestaLiveVIewModel()
        {
            DetenerEncuestaCommand = new RelayCommand(DetenerEncuesta);
            VMMsg.UpdateGraficaView += UpdateBarras;
            VMMsg.UpdateHistorial += UpdateUltimaRespuesta;
        }

        private void UpdateUltimaRespuesta(object? sender, UltimaRespuesta e)
        {
            UltimasRespuestas.Insert(0, e);
            OnPropertyChanged(nameof(UltimasRespuestas));
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
            string pathdb = "EncuestasDB/db.txt";
            Directory.CreateDirectory(Path.GetDirectoryName(pathdb));
            string json = File.ReadAllText(pathdb);
            DB? DB = JsonConvert.DeserializeObject<DB>(json);
            dbModel datos = new();
            
                datos = new dbModel();
                datos.TituloEncuesta = VMMsg.TituloEncuesta;
                datos.UltimasRespuestas = new();
                datos.UltimasRespuestas.AddRange(UltimasRespuestas);
                datos.ListaBarras = new();
                datos.ListaBarras.AddRange(ListaBarras);
            if(DB!=null)
            {
                DB.Encuestas.Add(datos);
            }
            else
            {
                DB = new DB();
                DB.Encuestas.Add(datos);
            }
            string path = "EncuestasDB/db.txt";
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, JsonConvert.SerializeObject(DB));
            VMMsg.OnSetServerStatus(false);
            UltimasRespuestas.Clear();
            OnPropertyChanged(nameof(UltimasRespuestas));
            VMMsg.OnCambioDeVista("ConfigView");
        }
        public class dbModel
        {
            public string TituloEncuesta { get; set; } = null!;
            public List<UltimaRespuesta>? UltimasRespuestas { get; set; } = new();
            public List<GraficaModel>? ListaBarras { get; set; } = new();
            public float Promedio
            {
                get
                {
                    return ListaBarras?.Average(x => x.prom) ?? 0;
                }
            }
            public DateTime Fecha { get; set; } = DateTime.Now;
        }
        public class DB
        {
            public List<dbModel> Encuestas { get; set; } = new();
        }
    }
}
