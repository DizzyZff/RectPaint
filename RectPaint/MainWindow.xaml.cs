using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Converters;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RectPaint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        private Point _startPoint;
        private RectangleViewModel _currentRectangle;

        private void Canvas_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _startPoint = e.GetPosition(canvas);
            _currentRectangle = new RectangleViewModel
            {
                X = _startPoint.X,
                Y = _startPoint.Y,
                Width = 0,
                Height = 0,
                Fill = Brushes.Transparent,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };
            var rectangle = new Rectangle();
            rectangle.SetBinding(Canvas.LeftProperty, new Binding(nameof(RectangleViewModel.X)));
            rectangle.SetBinding(Canvas.TopProperty, new Binding(nameof(RectangleViewModel.Y)));
            rectangle.SetBinding(FrameworkElement.WidthProperty, new Binding(nameof(RectangleViewModel.Width)));
            rectangle.SetBinding(FrameworkElement.HeightProperty, new Binding(nameof(RectangleViewModel.Height)));
            rectangle.SetBinding(Shape.FillProperty, new Binding(nameof(RectangleViewModel.Fill)));
            rectangle.SetBinding(Shape.StrokeProperty, new Binding(nameof(RectangleViewModel.Stroke)));
            rectangle.SetBinding(Shape.StrokeThicknessProperty,
                new Binding(nameof(RectangleViewModel.StrokeThickness)));
            rectangle.DataContext = _currentRectangle;
            //add to MainWindowViewModel
            ((MainWindowViewModel) DataContext).Rectangles.Add(_currentRectangle);
            canvas.Children.Add(rectangle);

        }

        private void Canvas_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _currentRectangle = null;
        }
        
        private void Canvas_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_currentRectangle != null)
            {
                var currentPoint = e.GetPosition(canvas);
                // can drag rectangle from any corner
                _currentRectangle.X = Math.Min(_startPoint.X, currentPoint.X);
                _currentRectangle.Y = Math.Min(_startPoint.Y, currentPoint.Y);
                _currentRectangle.Width = Math.Abs(_startPoint.X - currentPoint.X);
                _currentRectangle.Height = Math.Abs(_startPoint.Y - currentPoint.Y);
            }
        }

        
    }
}