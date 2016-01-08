using Mvvm;
using Mvvm.Services;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.Pickers;
using XamlBrewer.Pcl.ColorSwatchReader;
using XamlBrewer.Uwp.ColorSwatchReader.Models;

namespace XamlBrewer.Uwp.ColorSwatchReader.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
        private DelegateCommand openCommand;

        public MainPageViewModel()
        {
            openCommand = new DelegateCommand(Open_Executed);
        }

        public ObservableCollection<NamedColor> Palette { get; } = new ObservableCollection<NamedColor>();

        public ICommand OpenCommand
        {
            get { return openCommand; }
        }

        private async void Open_Executed()
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
                    var swatchColors = reader.ReadPhotoShopSwatchFile(stream);
                    Palette.Clear();
                    foreach (var color in swatchColors)
                    {
                        var pc = new NamedColor(color.Red, color.Green, color.Blue, color.Name);
                        Palette.Add(pc);
                    }
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
