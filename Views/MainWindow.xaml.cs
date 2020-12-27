#region

using Avalonia.Controls;
using Avalonia.Markup.Xaml;

#endregion

namespace MEM_GUI.Views
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}