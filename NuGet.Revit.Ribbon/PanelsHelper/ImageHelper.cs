using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace NuGet.Revit.Ribbon.PanelsHelper
{
    public static class ImageHelper
    {
        /// <summary>
        /// Resize image from path
        /// </summary>
        public static Bitmap ResizeImage(this string path, int size)
        {
            return new Bitmap(new Bitmap(path), size, size);
        }
        
        /// <summary>
        /// Resize image
        /// </summary>
        public static Bitmap ResizeImage(this Image image, int size)
        {
            return new Bitmap(image, size, size);
        }
        
        /// <summary>
        /// Change black pixels in image from path. If color parameter is Color.Black image will be returned unchanged.
        /// </summary>
        public static Bitmap ChangePngColor(this string path, Color color)
        {
            var bitmap = new Bitmap(path);
            return ChangePngColor(bitmap, color);
        }
        
        /// <summary>
        /// Change black pixels in image. If color parameter is Color.Black image will be returned unchanged.
        /// </summary>
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
        
        /// <summary>
        /// Convert Image to BitmapImage for WPF apps
        /// </summary>
        public static BitmapImage ToBitmapImage(this Image src)
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