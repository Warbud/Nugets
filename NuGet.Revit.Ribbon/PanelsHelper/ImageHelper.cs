using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace NuGet.Revit.Ribbon.PanelsHelper
{
    public static class ImageHelper
    {
        public static Bitmap TransformImage(this string path, int size)
        {
            var originalFile =  new Bitmap(path);
            return new Bitmap(originalFile, size, size);
        }
        
        public static Bitmap TransformImage(this Image image, int size)
        {
            return new Bitmap(image, size, size);
        }
        
        public static Bitmap ChangePngColor(this string path, Color color)
        {
            var bitmap = new Bitmap(path);
            return ChangePngColor(bitmap, color);
        }
        
        /// <summary>
        /// Change black pixels
        /// </summary>
        /// <param name="image"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Bitmap ChangePngColor(this Bitmap image, Color color)
        {
            if (color == Color.Black)
            {
                return image;
            }
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    var pixel = image.GetPixel(x, y).ToArgb();
                    
                    if (pixel == Color.Black.ToArgb())
                    {
                        image.SetPixel(x, y, color);
                    }
                }
            }
            return image;
        }
        
        public static BitmapImage ToBitmapImage(this Bitmap src)
        {
            var ms = new MemoryStream();
            src.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            var image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }
    }
}