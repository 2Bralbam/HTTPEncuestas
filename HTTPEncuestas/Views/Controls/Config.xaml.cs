﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HTTPEncuestas.Views.Controls
{
    /// <summary>
    /// Lógica de interacción para Config.xaml
    /// </summary>
    public partial class Config : UserControl
    {
        public Config()
        {
            InitializeComponent();
            this.Unloaded += Disappearing;
            this.Loaded += Appearing;
        }
        private void Appearing(object sender, RoutedEventArgs e)
        {
            DoubleAnimation fadeIn = new DoubleAnimation(0.0, 1.0, TimeSpan.FromSeconds(1));
            this.BeginAnimation(UserControl.OpacityProperty, fadeIn);
        }

        private void Disappearing(object sender, RoutedEventArgs e)
        {
            DoubleAnimation fadeOut = new DoubleAnimation(1.0, 0.0, TimeSpan.FromSeconds(1));
            this.BeginAnimation(UserControl.OpacityProperty, fadeOut);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            preg.Clear();
        }
    }
}
