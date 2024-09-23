using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Animation;

namespace NES_SNES_Movie_Maker
{
    /// <summary>
    /// Логика взаимодействия для Toolbar.xaml
    /// </summary>
    public partial class ProgressBar : Window
    {
        public ProgressBar()
        {
            InitializeComponent();
        }

        public void MaxValue(double maximum)
        {
            ProgressBar1.Maximum = maximum;
        }
        /// <summary>
        /// Инициализирует прогресс бар текущим значением и максимально допустимым
        /// </summary>
        /// <param name="current">текущее значение</param>
        /// <param name="maximum">максимально возможное значение</param>
        public void ChangeProgressBarValue(int current)
        {
            ProgressBar1.Value = (double) current;
            if (ProgressBar1.Value == ProgressBar1.Maximum)
                Close();
        }

        //private void ProgressBar1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    Duration duration = new Duration(TimeSpan.FromMilliseconds(500));
        //    DoubleAnimation doubleAnimation = new DoubleAnimation(ProgressBar1.Value + 1, duration);
        //    ProgressBar1.BeginAnimation(ProgressBar.DefaultStyleKeyProperty, doubleAnimation);
        //}
    }
}
