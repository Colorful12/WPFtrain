using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PictureViewer.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double _imgh, _imgw;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void showButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog img = new OpenFileDialog();
            img.Title = "Select a picture file";
            img.Filter = "JPEG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|BMP Files (*.bmp)|*.bmp|All files (*.*)|*.*\r\n";
            if (img.ShowDialog() == true)
            {
                BitmapImage bmpImage = new BitmapImage();
                using (FileStream stream = File.OpenRead(img.FileName))
                {
                    bmpImage.BeginInit();
                    bmpImage.StreamSource = stream;
                    bmpImage.DecodePixelWidth = 500;
                    bmpImage.CacheOption = BitmapCacheOption.OnLoad;
                    bmpImage.CreateOptions = BitmapCreateOptions.None;
                    bmpImage.EndInit();
                    bmpImage.Freeze();
                }
                _imgh = bmpImage.Height;
                _imgw = bmpImage.Width;
                imagebox.Source = bmpImage;
                
            }
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            imagebox = null;
        }

        private void backgroundButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            imagebox.Stretch = Stretch.Fill;  
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            // Stretchをもどせない！次回ここから
            imagebox.Stretch = Stretch.None;
            imagebox.Height = _imgh;
            imagebox.Width = _imgw;
        }
    }
}
