using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateLayout();
            Bitmap bmp = new Bitmap("D:/Programming/RiderProjects/WpfApp1/WpfApp1/fight.bmp");
            bool isFile = true;
            StreamReader streamReader = new StreamReader("D:/Programming/RiderProjects/WpfApp1/WpfApp1/text.txt");
            string text = streamReader.ReadToEnd();
            Bitmap encoded = Steganography.CodeText(text, bmp, isFile);
            encoded.Save("D:/Programming/RiderProjects/WpfApp1/WpfApp1/fight2.bmp", ImageFormat.Bmp);
            Bitmap bmpEncoded = new Bitmap("D:/Programming/RiderProjects/WpfApp1/WpfApp1/fight2.bmp");
            string text2 = Steganography.DecodeText(bmpEncoded);
            MessageBox.Show("Зашифрованный текст:\n" + text);
            MessageBox.Show("Расшифрованный текст:\n" + text2);
            Close();

        }
    }
}