# UWP-Adobe-Color-Swatch-to-XAML
Reads an Adobe Color Swatch (.aco) file, displays the colors, and exports them to XAML brushes.

Contains 
* a PCL library to extract the colors out of the content of an Adobe Color Swatch (*.aco) file,
* a UWP app that opens an Adobe Color Swatch file, calls the PCL to extract the colors, displays the colors, and exports the colors to XAML SolidColorBrush definitions (e.g. to be used in a Resource Dictionary), and
* some sample *.aco files in the Assets/Samples folder.

PCL code is slightly adapted from http://cyotek.com/blog/reading-photoshop-color-swatch-aco-files-using-csharp.
