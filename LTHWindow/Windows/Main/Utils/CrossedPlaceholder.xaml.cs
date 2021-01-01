using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LTHWindow.Windows.Main.Utils
{
    public partial class CrossedPlaceholder : UserControl
    {
        public CrossedPlaceholder()
        {
            InitializeComponent();
            
            var drawingBrush = new DrawingBrush();
            
            var geometryDrawing = new GeometryDrawing
            {
                Brush = Brushes.LightGray,
                Pen= new Pen(new SolidColorBrush(Colors.DimGray), 1)
            };
            var cross = new GeometryGroup();
            cross.Children.Add(new LineGeometry(new Point(0,0), new Point(100, 100)));
            cross.Children.Add(new LineGeometry(new Point(0, 100), new Point(100, 0)));

            geometryDrawing.Geometry = cross;
            drawingBrush.Drawing = geometryDrawing;
            
            Main.Fill = drawingBrush;
        }
    }
}