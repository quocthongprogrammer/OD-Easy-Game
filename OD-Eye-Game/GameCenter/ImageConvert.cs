using System;
using System.Drawing;

namespace GameCenter
{
    public class ImageConvert
    {
        #region To Icon
        public static Icon BitmapToIcon(Bitmap bmp)
        {
            IntPtr Hicon = bmp.GetHicon();
            return Icon.FromHandle(Hicon);

        }
        public static Icon ImageToIcon(Image img)
        {
            return BitmapToIcon(ImageToBitmap(img));
        }
        #endregion
        //
        #region To Bitmap
        public static Bitmap ImageToBitmap(Image img)
        {
            return new Bitmap(img, new Size(img.Width, img.Height));
        }
        public static Bitmap IconToBitmap(Icon icon)
        {
            return icon.ToBitmap();
        }
        #endregion
        //
        #region To Image
        public static Image BitmapToImage(Bitmap bmp)
        {
            return Image.FromHbitmap(bmp.GetHbitmap());
        }
        public static Image IconToImage(Icon icon)
        {
            return Image.FromHbitmap(IconToBitmap(icon).GetHbitmap());
        }
        #endregion
    }
}
