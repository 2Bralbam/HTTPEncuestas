using HTTPEncuestas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HTTPEncuestas.Views
{
    /// <summary>
    /// Lógica de interacción para HomeConfig.xaml
    /// </summary>
    public partial class HomeConfig : Window
    {
        public HomeConfig()
        {
            InitializeComponent();
            VMMsg.CambioDeVista += VMMsg_CambioDeVista;
        }

        private void VMMsg_CambioDeVista(object? sender, string e)
        {
            view.Opacity = 0;
            DoubleAnimation fadeIn = new DoubleAnimation(0.0, 1.0, TimeSpan.FromSeconds(1));
            view.BeginAnimation(UserControl.OpacityProperty, fadeIn);
        }
    }
}
