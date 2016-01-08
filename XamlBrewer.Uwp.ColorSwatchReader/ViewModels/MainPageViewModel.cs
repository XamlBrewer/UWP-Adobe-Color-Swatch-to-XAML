using Mvvm;
using Mvvm.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using Windows.Storage;
using Windows.Storage.Pickers;
using XamlBrewer.Pcl.ColorSwatchReader;
using XamlBrewer.Uwp.ColorSwatchReader.Models;
using System.Linq;

namespace XamlBrewer.Uwp.ColorSwatchReader.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
        private DelegateCommand openCommand;
        private DelegateCommand saveCommand;

        public MainPageViewModel()
        {
            openCommand = new DelegateCommand(Open_Executed);
            saveCommand = new DelegateCommand(Save_Executed, Save_CanExecute);
        }

        public ObservableCollection<NamedColor> Palette { get; } = new ObservableCollection<NamedColor>();

        public ICommand OpenCommand
        {
            get { return openCommand; }
        }

        public ICommand SaveCommand
        {
            get { return saveCommand; }
        }

        private async void Open_Executed()
        {
            var openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            openPicker.FileTypeFilter.Add(".aco");
            StorageFile file = await openPicker.PickSingleFileAsync();
            if (null != file)
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
        private async void Save_Executed()
        {
            var savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
            savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
            savePicker.DefaultFileExtension = ".txt";
            savePicker.SuggestedFileName = "ColorSwatchBrushes";
            StorageFile file = await savePicker.PickSaveFileAsync();
            if (null != file)
            {
                try
                {
                    await FileIO.WriteLinesAsync(file, Palette.Select(color => color.AsXamlResource()));
                    Toast.ShowInfo("File saved");
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    Toast.ShowError("Oops, something went wrong.");
                }
            }
            else
            {
                Toast.ShowWarning("Operation cancelled.");
            }
        }

        private bool Save_CanExecute()
        {
            return true;
        }
    }
}
