﻿using Windows.UI.Xaml.Controls;
using XamlBrewer.Uwp.ColorSwatchReader;

namespace Mvvm
{
    class ShellViewModel : ViewModelBase
    {
        public ShellViewModel()
        {
            // Build the menu
            // Symbol enumeration is here: https://msdn.microsoft.com/en-us/library/windows/apps/windows.ui.xaml.controls.symbol.aspx
            Menu.Add(new MenuItem() { Glyph = Symbol.OpenFile, Text = "Import", NavigationDestination = typeof(MainPage) });
        }
    }
}
