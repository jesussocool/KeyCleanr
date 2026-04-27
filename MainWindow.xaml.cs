using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Windows.Graphics;

namespace KeyCleanr
{
    public sealed partial class MainWindow : Window
    {
        private readonly KeyboardHook _hook = new();
        private bool _firstActivation = true;

        public MainWindow()
        {
            InitializeComponent();

            SystemBackdrop = new MicaBackdrop();
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(TitleBarText);

            ((FrameworkElement)this.Content).Loaded += (_, _) =>
            {
                var dpi = AppWindow.ClientSize.Width > 0
                    ? (double)AppWindow.ClientSize.Width / 640
                    : 1.0;

                var scale = GetScaleFactor();
                AppWindow.Resize(new SizeInt32(
                    (int)(640 * scale),
                    (int)(340 * scale)
                ));

                if (AppWindow.Presenter is OverlappedPresenter p)
                {
                    p.IsResizable = false;
                    p.IsMaximizable = false;
                }
            };

            Activated += OnFirstActivation;
            Closed += (_, _) => _hook.Dispose();
        }

        private double GetScaleFactor()
        {
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var dpi = Windows.Win32.PInvoke.GetDpiForWindow((Windows.Win32.Foundation.HWND)hwnd);
            return dpi / 96.0;
        }

        private void OnFirstActivation(object sender, WindowActivatedEventArgs e)
        {
            if (!_firstActivation) return;
            _firstActivation = false;

            if (AutoStartCheckBox.IsChecked == true)
                StartCleaning();
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (_hook.IsLocked) StopCleaning();
            else StartCleaning();
        }

        private void StartCleaning()
        {
            _hook.Lock();
            ToggleButtonText.Text = "Click to stop cleaning mode / unlock the keyboard!";
        }

        private void StopCleaning()
        {
            _hook.Unlock();
            ToggleButtonText.Text = "Click to start cleaning mode / lock the keyboard!";
        }
    }
}