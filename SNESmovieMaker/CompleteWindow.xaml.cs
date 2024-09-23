using System.Windows;

namespace NES_SNES_Movie_Maker
{
    /// <summary>
    /// Логика взаимодействия для CompleteWindow.xaml
    /// </summary>
    public partial class CompleteWindow : Window
    {
        public CompleteWindow()
        {
            InitializeComponent();
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
