using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace TWord.ViewModel
{
    /// <summary>
    /// The view model for the cutom flat window
    /// </summary>
    class WindowViewModel : BaseViewModel
    {
        #region Private member
        /// <summary>
        /// private member
        /// </summary>
        private Window mWindow;
        /// <summary>
        /// Margin around the qindow to allo for drop shadow
        /// </summary>
        private int mOutMarginZise = 2;
        private int mWindowRadius = 5;
        /// <summary>
        /// The last known dock position
        /// </summary>
        private WindowDockPosition mDockPosition = WindowDockPosition.Undocked;

        #endregion

        /// <summary>
        /// Public Properties region is the redion is needed if you dont want any maximize button. The
        /// methods you need for such purpose are ResizeBorder, ResizeBorderThickness, OutMarginSize
        /// OutMarginSizeThickness, WindowRadius, WindowCornerRadius
        /// </summary>

        #region Public Properties


        /// <summary>
        /// Default minimum  dimension for a window
        /// </summary>
        public double WindowMinimumWidth { get; set; } = 400;
        public double WindowMinimumHeight { get; set; } = 400;

        /// <summary>
        /// True if the window should be borderless because it is docked or maximized
        /// </summary>
        public bool Borderless { get { return (mWindow.WindowState == WindowState.Maximized || mDockPosition != WindowDockPosition.Undocked); } }

        /// <summary>
        /// The size of the resize border around the window.
        /// </summary>
        public int ResizeBorder { get { return Borderless ? 0 : 6; } }

        /// <summary>
        /// The size of the resize border around the window, taking into account the outer margin
        /// </summary>
        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder + OutMarginSize); } }

        /// <summary>
        /// The  paddinf of the inner content of the main window
        /// </summary>
        /// Use following code to have padding around the window. if you donet want it use the second rwo with 
        /// thickness(0)
        //public Thickness InnerContentPadding { get { return new Thickness(ResizeBorder); } }
        public Thickness InnerContentPadding { get; set; } = new Thickness(0);
        /// <summary>
        /// The size of the resize border around the window.
        /// </summary>
        private int OutMarginSize
        {
            get
            {
                return Borderless ? 0 : mOutMarginZise;
            }
            set
            {
                mOutMarginZise = value;
            }
        }

        /// <summary>
        /// The size of the resize border thickness around the window.
        /// </summary>
        private Thickness OutMarginSizeThickness { get { return new Thickness(OutMarginSize); } }

        /// <summary>
        /// The size of the corner  border around the window.
        /// </summary>
        private int WindowRadius
        {
            get
            {
                return Borderless ? 0 : mWindowRadius;
            }
            set
            {
                mWindowRadius = value;
            }
        }

        /// <summary>
        /// Radius of the edge of the window
        /// </summary>
        private CornerRadius WindowCornerRadius { get { return new CornerRadius(WindowRadius); } }

        /// <summary>
        /// The height of the title bar/Caption of the window
        /// </summary>
        public int TitleHeight { get; set; } = 32;

        /// <summary>
        /// The height of the title bar /caption of the window
        /// </summary>
        public GridLength TitleHeightGridLength { get { return new GridLength(TitleHeight + ResizeBorder); } }

        /// <summary>
        /// Current page of the application
        /// </summary>

        public ApplicationPage CurrenPage { get; set; } = ApplicationPage.Pico;
        public MenuPage MenuPage { get; set; } = MenuPage.VerticalMenuLeft;

        #endregion

        #region Commands
        /// <summary>
        /// Command to minimize
        /// </summary>
        public ICommand MinimizeCommand { get; set; }

        /// <summary>
        /// Command to Maximize
        /// </summary>
        public ICommand MaximizeCommand { get; set; }

        /// <summary>
        /// Command to Close
        /// </summary>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// Command to Menu system
        /// </summary>
        public ICommand MenuCommand { get; set; }


        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public WindowViewModel(Window window)
        {
            mWindow = window;

            // listen out for the window resizing
            mWindow.StateChanged += (sender, e) =>
              {
                  // Fire off events for all properties that are effected by resize

                  WindowResized();
              };

            // Create command
            MinimizeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => mWindow.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => mWindow.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(mWindow, GetMousePosition()));

            // Fix window resize
            var resizer = new WindowResizer(mWindow);
            // Listen out for dock changes
            resizer.WindowDockChanged += (dock) =>
            {
                // Store last position
                mDockPosition = dock;

                // Fire off resize events
                WindowResized();
            };

        }

        private void WindowResized()
        {
            // Fire off events for all properties that are affected by a resize
            OnPropertyChanged(nameof(Borderless));
            OnPropertyChanged(nameof(ResizeBorderThickness));
            OnPropertyChanged(nameof(OutMarginSize));
            OnPropertyChanged(nameof(OutMarginSizeThickness));
            OnPropertyChanged(nameof(WindowRadius));
            OnPropertyChanged(nameof(WindowCornerRadius));
        }
        #endregion

        #region privateHelpers

        /// <summary>
        /// Gets the current mouse position on the screen
        /// </summary>
        /// <returns></returns>
        private Point GetMousePosition()
        {
            // Position of the mouse relative to the window
            var position = Mouse.GetPosition(mWindow);

            // Add the window position so its a "ToScreen"
            //return new Point(position.X + mWindow.Left, position.Y);
            return new Point(position.X + mWindow.Left, position.Y + mWindow.Top);
        }

        #endregion

    }
}
