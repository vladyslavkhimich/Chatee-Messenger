using ChateeCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ChateeWPF
{
    public class ChromeWindowViewModel : BaseViewModel
    {
        #region Private Members
        private Window mWindow;
        private int mOuterMarginSize = 10;
        private int mWindowRadius = 8;
        private WindowDockPosition mDockPosition = WindowDockPosition.Undocked;
        #endregion
        #region Public Properties
        public double WindowMinimumWidth { get; set; } = 500;
        public double WindowMinimumHeight { get; set; } = 400;
        public bool Borderless { get { return (mWindow.WindowState == WindowState.Maximized || mDockPosition != WindowDockPosition.Undocked); } }
        public int ResizeBorder { get; set; } = 6;
        public int OuterMarginSize { get { return Borderless ? 0 : mOuterMarginSize; } set { mOuterMarginSize = value; } }
        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder + OuterMarginSize); } }
        public Thickness OuterMarginSizeThickness { get { return new Thickness(OuterMarginSize); } }
        public int WindowRadius { get { return Borderless ? 0 : mWindowRadius; } set { mWindowRadius = value; } }
        public CornerRadius WindowCornerRadius { get { return new CornerRadius(WindowRadius); } }
        public int TitleHeight { get; set; } = 40;
        public GridLength TitleHeightGridLength { get { return new GridLength(TitleHeight + ResizeBorder); } }
        public ApplicationPages CurrentPage { get; set; } = ApplicationPages.LoginPage;
        #endregion
        #region Commands
        public ICommand MinimizeCommand { get; set; }
        public ICommand MaximizeCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand MenuCommand { get; set; }
        #endregion
        #region Constructor
        public ChromeWindowViewModel(Window window)
        {
            mWindow = window;
            mWindow.StateChanged += (sender, e) => { WindowResized(); };
            MinimizeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => mWindow.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => mWindow.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(mWindow, GetMousePosition()));
        }
        #endregion
        #region Private Helpers
        private Point GetMousePosition()
        {
            var position = Mouse.GetPosition(mWindow);
            if (mWindow.WindowState == WindowState.Maximized)
                return new Point(position.X, position.Y);
            else
                return new Point(position.X + mWindow.Left, position.Y + mWindow.Top);
        }
        private void WindowResized()
        {
            OnPropertyChanged(nameof(Borderless));
            OnPropertyChanged(nameof(ResizeBorderThickness));
            OnPropertyChanged(nameof(OuterMarginSize));
            OnPropertyChanged(nameof(OuterMarginSizeThickness));
            OnPropertyChanged(nameof(WindowRadius));
            OnPropertyChanged(nameof(WindowCornerRadius));
        }
        #endregion
    }
}
