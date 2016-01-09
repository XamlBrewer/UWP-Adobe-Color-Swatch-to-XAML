using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;

namespace Mvvm.Services
{
    public class Theme
    {
        // Call this in App OnLaunched.
        // Requires reference to Windows Mobile Extension for the UWP.
        public static void ApplyToContainer()
        {
            //PC customization
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
            {
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                if (titleBar != null)
                {
                    titleBar.ButtonBackgroundColor = Color.FromArgb((byte)255,(byte)170, (byte)57, (byte)57);
                    titleBar.ButtonForegroundColor = Colors.White;
                    titleBar.BackgroundColor = Color.FromArgb((byte)255, (byte)170, (byte)57, (byte)57);
                    titleBar.ForegroundColor = Colors.White;
                }
            }

            //Mobile customization
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusBar = StatusBar.GetForCurrentView();
                if (statusBar != null)
                {
                    statusBar.BackgroundOpacity = 1;
                    statusBar.BackgroundColor = Color.FromArgb((byte)255, (byte)170, (byte)57, (byte)57);
                    statusBar.ForegroundColor = Colors.White;
                }
            }
        }
    }
}
