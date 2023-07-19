using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace MatchingGame.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Random random = new Random();

        Label firstClicked = null;
        Label secondClicked = null;
        DispatcherTimer timer1 = new DispatcherTimer();

        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };

        private void AssignIconsToSquares()
        {
            foreach (UIElement element in this.Grid1.Children)
            {
                if (element is Control) { 
                    Label iconLabel = element as Label;
                    if (iconLabel != null)
                    {
                        int randomNumber = random.Next(icons.Count);
                        iconLabel.Content = icons[randomNumber];
                        iconLabel.Foreground = iconLabel.Background;
                        icons.RemoveAt(randomNumber);
                    }
                }
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = TimeSpan.FromMilliseconds(750); //750ms
            AssignIconsToSquares();
        }

        private void label1_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (timer1.IsEnabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                if (clickedLabel.Foreground == new SolidColorBrush(System.Windows.Media.Color.FromRgb(141, 253, 237)))
                    return;

                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(141, 253, 237));

                    return;
                }

            }

            secondClicked = clickedLabel;
            secondClicked.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(141, 253, 237));

            CheckForWinner();

            if (firstClicked.Content == secondClicked.Content)
            {
                firstClicked = null;
                secondClicked = null;
                return;
            }

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            firstClicked.Foreground = firstClicked.Background;
            secondClicked.Foreground = secondClicked.Background;

            firstClicked = null;
            secondClicked = null;
        }

        private void CheckForWinner()
        {

            foreach (UIElement element in this.Grid1.Children)
            {
                if (element is Control)
                {
                    Label iconLabel = element as Label;

                    if (iconLabel != null)
                    {
                        if (iconLabel.Foreground == iconLabel.Background)
                            return;
                    }
                }
            }

            MessageBox.Show("You matched all the icons!", "Congratulations");
            Close();
        }
    }
}
