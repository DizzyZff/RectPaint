using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace RectPaint
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private BitmapImage _imageSource;
        public BitmapImage ImageSource
        {
            get => _imageSource;
            set
            {
                _imageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }
        
        private ObservableCollection<RectangleViewModel> _rectangles = new ObservableCollection<RectangleViewModel>();
        public ObservableCollection<RectangleViewModel> Rectangles
        {
            get => _rectangles;
            set
            {
                _rectangles = value;
                OnPropertyChanged(nameof(Rectangles));
            }
        }
        
        
        private Stack<ObservableCollection<RectangleViewModel>> _undoStack = new Stack<ObservableCollection<RectangleViewModel>>();
        private Stack<ObservableCollection<RectangleViewModel>> _redoStack = new Stack<ObservableCollection<RectangleViewModel>>();
        
        public ICommand OpenCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand SaveAsCommand { get; }
        public ICommand ExitCommand { get; }
        public ICommand UndoCommand { get; }
        public ICommand RedoCommand { get; }
        public ICommand AboutCommand { get; }
        public ICommand DrawRectangleCommand { get; }

        public MainWindowViewModel()
        {
            OpenCommand = new RelayCommand(Open);
            SaveCommand = new RelayCommand(Save);
            SaveAsCommand = new RelayCommand(SaveAs);
            ExitCommand = new RelayCommand(Exit);
            UndoCommand = new RelayCommand(Undo, CanUndo);
            RedoCommand = new RelayCommand(Redo, CanRedo);
            AboutCommand = new RelayCommand(About);
            DrawRectangleCommand = new RelayCommand(DrawRectangle);
        }

        private void DrawRectangle(object obj)
        {
            var rectangle = new RectangleViewModel();
            if (obj is MouseButtonEventArgs mouseButtonEventArgs)
            {
                rectangle.IsDrawing = true;
                rectangle.StartPoint = mouseButtonEventArgs.GetPosition(null);
                rectangle.X = rectangle.StartPoint.X;
                rectangle.Y = rectangle.StartPoint.Y;
            }
        }

        private void Open(object parameter)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg;*.bmp)|*.png;*.jpeg;*.jpg;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(fileName);
                bitmapImage.EndInit();
                ImageSource = bitmapImage;
                if (ImageSource == null)
                {
                    throw new Exception("Image cannot be loaded.");
                }
            }
            Rectangles.Clear();
        }
        
        private void Save(object parameter)
        {
            var  saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Image files (*.png)|*.png|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                string fileName = saveFileDialog.FileName;
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(ImageSource));
                using (var fileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Create))
                {
                    encoder.Save(fileStream);
                }
            }
        }
        
        private void SaveAs(object parameter)
        {
            var  saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Image files (*.png)|*.png|Image files (*.jpeg)|*.jpeg|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                switch (saveFileDialog.FilterIndex)
                {
                    case 1:
                        SaveAsPng(saveFileDialog.FileName);
                        break;
                    case 2:
                        SaveAsJpeg(saveFileDialog.FileName);
                        break;
                    default:
                        throw new Exception("Unknown file format.");
                }
            }
        }
        
        private void SaveAsPng(string fileName)
        {
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(ImageSource));
            using (var fileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Create))
            {
                encoder.Save(fileStream);
            }
        }
        
        private void SaveAsJpeg(string fileName)
        {
            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(ImageSource));
            using (var fileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Create))
            {
                encoder.Save(fileStream);
            }
        }
        
        private void Exit(object parameter)
        {
            Environment.Exit(0);
        }
        
        private void Undo(object parameter)
        {
            _redoStack.Push(Rectangles);
            Rectangles = _undoStack.Pop();
        }
        
        private bool CanUndo(object parameter)
        {
            return _undoStack.Count > 0;
        }
        
        private void Redo(object parameter)
        {
            _undoStack.Push(Rectangles);
            Rectangles = _redoStack.Pop();
        }
        
        private bool CanRedo(object parameter)
        {
            return _redoStack.Count > 0;
        }
        
        private void Delete(object parameter)
        {
            foreach (var rectangle in Rectangles)
            {
                if (rectangle.IsSelected)
                {
                    _undoStack.Push(Rectangles);
                    Rectangles.Remove(rectangle);
                    break;
                }
            }
        }
        
        private bool CanDelete(object parameter)
        {
            return Rectangles.Count > 0;
        }
        
        private void DeleteAll(object parameter)
        {
            _undoStack.Push(Rectangles);
            Rectangles.Clear();
        }
        
        private bool CanDeleteAll(object parameter)
        {
            return Rectangles.Count > 0;
        }
        
        private void About(object parameter)
        {
            throw new NotImplementedException();
        }
        
        public double ImageWidth => ImageSource?.Width ?? 0;
        public double ImageHeight => ImageSource?.Height ?? 0;
        

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}