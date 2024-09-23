using System;
using System.Windows;

namespace NES_SNES_Movie_Maker
{
    /// <summary>
    /// Логика взаимодействия для ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        private Exception ex;
        public ErrorWindow()
        {
            InitializeComponent();
        }

        public void Exception_catch(Exception exc)
        {
            ex = exc;
            textBlockException.Text = ex.Message;
        }

        private void Button_Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
