using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RectPaint
{
    public partial class ImageCanvas : UserControl
    {
        public ImageCanvas()
        {
            InitializeComponent();
        }
        
        public void SetImageSource(BitmapImage source)
        {
            image.Source = source;
            canvas.Width = source.PixelWidth;
            canvas.Height = source.PixelHeight;
        }
        
        public void DrawRectangle(Rect rect, Brush brush)
        {
            var rectShape = new System.Windows.Shapes.Rectangle
            {
                Width = rect.Width,
                Height = rect.Height,
                Fill = brush
            };
            Canvas.SetLeft(rectShape, rect.X);
            Canvas.SetTop(rectShape, rect.Y);
            canvas.Children.Add(rectShape);
        }
        
        public void Clear()
        {
            canvas.Children.Clear();
        }
    }
}