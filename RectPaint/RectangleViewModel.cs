using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RectPaint
{
    public class RectangleViewModel : INotifyPropertyChanged
    {
        private double _x;
        private double _y;
        private double _width;
        private double _height;
        private Brush _fill;
        private Brush _stroke;
        private double _strokeThickness;
        private bool _isSelected;
        private bool _isDrawing;
        private Point _startPoint;
        
        public double X
        {
            get => _x;
            set
            {
                _x = value;
                OnPropertyChanged(nameof(X));
            }
        }
        
        public double Y
        {
            get => _y;
            set
            {
                _y = value;
                OnPropertyChanged(nameof(Y));
            }
        }
        
        public double Width
        {
            get => _width;
            set
            {
                _width = value;
                OnPropertyChanged(nameof(Width));
            }
        }
        
        
public double Height
        {
            get => _height;
            set
            {
                _height = value;
                OnPropertyChanged(nameof(Height));
            }
        }

    public Brush Fill
        {
            get => _fill;
            set
            {
                _fill = value;
                OnPropertyChanged(nameof(Fill));
            }
        }
    
        public Brush Stroke
        {
            get => _stroke;
            set
            {
                _stroke = value;
                OnPropertyChanged(nameof(Stroke));
            }
        }
        
        public double StrokeThickness
        {
            get => _strokeThickness;
            set
            {
                _strokeThickness = value;
                OnPropertyChanged(nameof(StrokeThickness));
            }
        }
        
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }
        
        public bool IsDrawing
        {
            get => _isDrawing;
            set
            {
                _isDrawing = value;
                OnPropertyChanged(nameof(IsDrawing));
            }
        }
        
        private void HandleMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            IsSelected = true;
        }
        
        private void HandleMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            IsSelected = false;
        }
        
        private void HandleMouseMove(object sender, MouseEventArgs e)
        {
            if (IsDrawing)
            {
                var point = e.GetPosition((IInputElement) sender);
                Width = point.X - X;
                Height = point.Y - Y;
            }
        }
        
        private void HandleMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            IsDrawing = true;
            var point = e.GetPosition((IInputElement) sender);
            X = point.X;
            Y = point.Y;
        }

        private void HandleMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IsDrawing = false;
        }
        
        public Point StartPoint
        {
            get => _startPoint;
            set
            {
                _startPoint = value;
                OnPropertyChanged(nameof(StartPoint));
            }
        }
        
        
        public RectangleViewModel()
        {
            Fill = Brushes.Transparent;
            Stroke = Brushes.Black;
            StrokeThickness = 1;
            X = 0;
            Y = 0;
            Width = 0;
            Height = 0;
            IsSelected = false;
            IsDrawing = false;
        }



        public event PropertyChangedEventHandler PropertyChanged;
        
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}