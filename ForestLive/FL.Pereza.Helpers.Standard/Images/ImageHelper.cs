using System;
using System.Drawing;
using System.IO;

namespace FL.Pereza.Helpers.Standard.Images
{
    public static class ImageHelper
    {
        public const string USER_PROFILE_IMAGE_EXTENSION = ".jpg";
        public const string PNG_EXTENSION = "png";
        public static string FromBase64PNGToBase64JPG(string base64PNG)
        {
            byte[] bytes = Convert.FromBase64String(base64PNG);
            using (MemoryStream msIn = new MemoryStream(bytes))
            {
                using (Image pic = Image.FromStream(msIn))
                {
                    using (MemoryStream msOut = new MemoryStream())
                    {
                        pic.Save(msOut, System.Drawing.Imaging.ImageFormat.Jpeg);
                        return Convert.ToBase64String(msOut.ToArray());
                    }
                }
            }
        }
    }
}
