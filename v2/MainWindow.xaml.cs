using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using mThink.Agents;

namespace mThink
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            View view = new View((int)MapSizeSlider.Value, (int)SignalRadiusSlider.Value, (int)VisionRadiusSlider.Value, (int)TreesSlider.Value, (int)MonkeysSlider.Value, (int)EaglesSlider.Value, (int)TigersSlider.Value);
        }

        private void MapSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        }
    }
}
