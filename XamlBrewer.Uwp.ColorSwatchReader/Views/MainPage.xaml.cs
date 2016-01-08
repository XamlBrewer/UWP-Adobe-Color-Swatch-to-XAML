using Mvvm;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using XamlBrewer.Uwp.ColorSwatchReader.ViewModels;

namespace XamlBrewer.Uwp.ColorSwatchReader
{
    public sealed partial class MainPage : Page
    {
        private MenuItem openItem;

        public MainPage()
        {
            this.InitializeComponent();
            openItem = new MenuItem() { Glyph = Symbol.OpenFile, Text = "Open", Command = ViewModel.OpenCommand };
            ViewModel.Menu.Add(openItem);
        }

        private MainPageViewModel ViewModel { get; } = new MainPageViewModel();

        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{

        //    base.OnNavigatedTo(e);
        //}

        //protected override void OnNavigatedFrom(NavigationEventArgs e)
        //{
        //    ViewModel.Menu.Remove(openItem);
        //    base.OnNavigatedFrom(e);
        //}
    }
}
