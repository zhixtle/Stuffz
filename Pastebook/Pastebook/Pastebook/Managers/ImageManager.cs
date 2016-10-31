using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace Pastebook.Managers
{
    public class ImageManager
    {
        public byte[] imageToByteArray(HttpPostedFileBase file)
        {
            System.Drawing.Image picImage = System.Drawing.Image.FromStream(file.InputStream);
            MemoryStream ms = new MemoryStream();
            picImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        public bool IsImage(HttpPostedFileBase file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }

            string[] formats = new string[] { ".jpg", ".png", ".jpeg" };
            
            return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }

        public bool IsValidSize(HttpPostedFileBase file)
        {
            if(file.ContentLength > 4194304)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}