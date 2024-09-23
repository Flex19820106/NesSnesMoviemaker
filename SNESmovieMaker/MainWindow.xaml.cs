using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace NES_SNES_Movie_Maker
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListBoxItem_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            MenuItem item = (MenuItem)e.Source;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;

            if (item.Header.ToString() == "Открыть movie файлы SNES")
            {
                ofd.Title = "Открытие файлов movie SNES";
                ofd.Filter = "SNES видео файлы|*.smv";
            }
            else
            {
                ofd.Title = "Открытие файлов movie NES";
                ofd.Filter = "NES видео файлы|*.fm2";
            }

            if ((bool)ofd.ShowDialog())
            {
                Program mm = new Program();
                mm.Main(ofd.FileNames);
            }
        }
    }
}
