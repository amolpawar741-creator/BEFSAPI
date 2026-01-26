using System.Drawing;
using System.Drawing.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace BEFS.Utility
{
    public class CaptchaGenerator
    {
        public static byte[] GenerateCaptchaImage(string captchaText)
        {
            using var bitmap = new Bitmap(120, 40);
            using var graphics = Graphics.FromImage(bitmap);
            using var font = new System.Drawing.Font("Arial", 20, FontStyle.Bold);
            using var ms = new MemoryStream();

            graphics.Clear(Color.White);
            graphics.DrawString(captchaText, font, Brushes.Black, 10, 5);

            // Noise
            for (int i = 0; i < 10; i++)
            {
                graphics.DrawLine(Pens.Gray,
                    new Random().Next(0, 120), new Random().Next(0, 40),
                    new Random().Next(0, 120), new Random().Next(0, 40));
            }

            bitmap.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }
    }
}
