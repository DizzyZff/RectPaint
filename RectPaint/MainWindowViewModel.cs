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
        
        public MainWindowViewModel()
        {
            var openCommand = new RelayCommand(Open);
            var saveCommand = new RelayCommand(Save);
            var saveAsCommand = new RelayCommand(SaveAs);
            var exitCommand = new RelayCommand(Exit);
            var undoCommand = new RelayCommand(Undo, CanUndo);
            var redoCommand = new RelayCommand(Redo);
            var aboutCommand = new RelayCommand(About);
            var DrawRectangleCommand = new RelayCommand(DrawRectangle);
        }

        private void DrawRectangle(object obj)
        {
            throw new NotImplementedException();
        }

        private void Open(object parameter)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg;*.bmp)|*.png;*.jpeg;*.jpg;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                ImageSource = new BitmapImage(new Uri(fileName));
            }
            Rectangles.Clear();
        }
        
        private void Save(object parameter)
        {
            throw new NotImplementedException();
        }
        
        private void SaveAs(object parameter)
        {
            throw new NotImplementedException();
        }
        
        private void Exit(object parameter)
        {
            throw new NotImplementedException();
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
        

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}