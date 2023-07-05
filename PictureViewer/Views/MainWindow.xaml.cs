using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PictureViewer.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
            Microsoft.Win32.OpenFileDialog img = new Microsoft.Win32.OpenFileDialog();
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
                rectangle1.Fill = null;
                imagebox.Source = bmpImage;
                
            }
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            rectangle1.Fill = null;
            imagebox.Source = null;
        }


        private void backgroundButton_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                imagebox.Source = null;

                // Convert "System.Drawing.Color" to  System.Windows.Media.Color 
                var mediaColor = System.Windows.Media.Color.FromArgb(cd.Color.A, cd.Color.R, cd.Color.G, cd.Color.B);
                
                SolidColorBrush myBrush = new SolidColorBrush(mediaColor);
                rectangle1.Fill = myBrush;  
            }
            
        }

    

        private void checkbox1_Checked(object sender, RoutedEventArgs e)
        {
            imagebox.Stretch = Stretch.Fill;
        }

        private void checkbox1_Unchecked(object sender, RoutedEventArgs e)
        {
            imagebox.Stretch = Stretch.Uniform;
        }
    }
}
