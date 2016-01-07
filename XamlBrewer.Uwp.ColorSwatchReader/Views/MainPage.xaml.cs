using Mvvm.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using XamlBrewer.Pcl.ColorSwatchReader;

namespace XamlBrewer.Uwp.ColorSwatchReader
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            openPicker.FileTypeFilter.Add(".aco");
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                try
                {
                // User picked a file. 
                var stream = await file.OpenStreamForReadAsync();
                var reader = new AcoConverter();
                var result = reader.ReadPhotoShopSwatchFile(stream);
                var x = result;
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    Toast.ShowError("Oops, something went wrong.");
                }
            }
            else
            {
                // User cancelled.
                Toast.ShowWarning("Operation cancelled.");
            }
        }
    }
}
