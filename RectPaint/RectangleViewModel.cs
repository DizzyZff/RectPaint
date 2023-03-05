using System.ComponentModel;
using System.Windows.Media;

namespace RectPaint
{
    public class RectangleViewModel : INotifyPropertyChanged
    {
        
        private SolidColorBrush _fill = new SolidColorBrush(Colors.Red);
        public SolidColorBrush Fill
        {
            get => _fill;
            set
            {
                _fill = value;
                OnPropertyChanged(nameof(Fill));
            }
        }
        
        private SolidColorBrush _stroke = new SolidColorBrush(Colors.Black);
        public SolidColorBrush Stroke
        {
            get => _stroke;
            set
            {
                _stroke = value;
                OnPropertyChanged(nameof(Stroke));
            }
        }
        
        private double _strokeThickness = 1;
        public double StrokeThickness
        {
            get => _strokeThickness;
            set
            {
                _strokeThickness = value;
                OnPropertyChanged(nameof(StrokeThickness));
            }
        }
        
        private double _x = 0;
        public double X
        {
            get => _x;
            set
            {
                _x = value;
                OnPropertyChanged(nameof(X));
            }
        }
        
        private double _y = 0;
        public double Y
        {
            get => _y;
            set
            {
                _y = value;
                OnPropertyChanged(nameof(Y));
            }
        }
        
        private double _width = 0;
        public double Width
        {
            get => _width;
            set
            {
                _width = value;
                OnPropertyChanged(nameof(Width));
            }
        }
        
        private double _height = 0;
        public double Height
        {
            get => _height;
            set
            {
                _height = value;
                OnPropertyChanged(nameof(Height));
            }
        }
        
        private bool isSelected = false;
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}