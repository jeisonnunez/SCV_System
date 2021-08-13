using LiveCharts;
using LiveCharts.Wpf;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para PieChartExample.xaml
    /// </summary>
    public partial class PieChartExample : UserControl
    {
        public PieChartExample()
        {
            InitializeComponent();

            Func<ChartPoint, string> labelPoint = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            var bc = new BrushConverter();

            pieChart.Zoom = ZoomingOptions.Xy;           

            pieChart.Series = new SeriesCollection
            {
               
                new PieSeries
                {
                    Title = "Maria",
                    Values = new ChartValues<double> {3},                  
                    DataLabels = true,
                    LabelPoint = labelPoint,
                    Fill=(Brush)bc.ConvertFrom("#573366")
        },
                new PieSeries
                {
                    Title = "Charles",
                    Values = new ChartValues<double> {4},
                    DataLabels = true,
                    LabelPoint = labelPoint,
                     Fill=(Brush)bc.ConvertFrom("#cb231a")
                },
                new PieSeries
                {
                    Title = "Frida",
                    Values = new ChartValues<double> {6},
                    DataLabels = true,
                    LabelPoint = labelPoint,
                    Fill=(Brush)bc.ConvertFrom("#277256")
                },
                new PieSeries
                {
                    Title = "Frederic",
                    Values = new ChartValues<double> {2},
                    DataLabels = true,
                    LabelPoint = labelPoint,                   
                     Fill=(Brush)bc.ConvertFrom("#1b6c98")
                },
                new PieSeries
                {
                    Title = "Jeison",
                    Values = new ChartValues<double> {2},
                    DataLabels = true,
                    LabelPoint = labelPoint,
                     Fill=(Brush)bc.ConvertFrom("#e1aa05")
                }
            };

            pieChart.LegendLocation = LegendLocation.Bottom;
        }

        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
            {
                series.PushOut = 0;
            }
                

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }
    }
}
