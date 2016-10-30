﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace Pastebook.Managers
{
    public class ImageManager
    {
        //http://www.codeproject.com/Articles/15460/C-Image-to-Byte-Array-and-Byte-Array-to-Image-Conv

        public byte[] imageToByteArray(HttpPostedFileBase file)
        {
            System.Drawing.Image picImage = System.Drawing.Image.FromStream(file.InputStream);
            MemoryStream ms = new MemoryStream();
            picImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        //http://stackoverflow.com/questions/12120135/mvc3-how-to-check-if-httppostedfilebase-is-an-image

        public bool IsImage(HttpPostedFileBase file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }

            string[] formats = new string[] { ".jpg", ".png", ".jpeg" };
            
            return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }
    }
}