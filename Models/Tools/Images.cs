using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Tools
{
    public static class Images
    {
        public static Image Base64ToImage(string base64String)
        {
            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                Image image = Image.FromStream(ms, true);
                return image;
            }
        }

        public static string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to base 64 string
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        /// <summary>
        /// Изменение размера изображения, с сохранением пропорций. Если ширина была
        /// меньше width, она не изменится
        /// </summary>
        /// <param name="width">Ширина в пикселях, до которой сожмём изображение </param>
        /// <param name="imageInBytes">Картинка в байтовом виде</param>
        public static byte[] Resize(int width, byte[] imageInBytes)
        {
            throw new NotImplementedException();
        }
    }
}
