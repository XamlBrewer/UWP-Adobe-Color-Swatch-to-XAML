using Mvvm;
using Windows.UI.Xaml.Controls;
using XamlBrewer.Uwp.ColorSwatchReader.ViewModels;

namespace XamlBrewer.Uwp.ColorSwatchReader
{
    public sealed partial class MainPage : Page
    {
        // Splitview menu items.
        private MenuItem openItem;
        private MenuItem saveItem;

        public MainPage()
        {
            this.InitializeComponent();

            openItem = new MenuItem() { Glyph = Symbol.OpenFile, Text = "Open", Command = ViewModel.OpenCommand };
            ViewModel.Menu.Add(openItem);

            saveItem = new MenuItem() { Glyph = Symbol.Save, Text = "Save", Command = ViewModel.SaveCommand };
            ViewModel.Menu.Add(saveItem);
        }

        private MainPageViewModel ViewModel { get; } = new MainPageViewModel();
    }
}
