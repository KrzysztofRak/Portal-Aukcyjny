using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using Model.Repositories;

namespace ImageHandler
{
    /// <summary>
    /// Summary description for HttpImageHandler
    /// </summary>
    public class HttpImageHandler : IHttpHandler
    {
        public static int TryIntParse(string source)
        {
            int val;
            return int.TryParse(source, out val) ? val : 0;
        }

        public byte[] GetImageBytes(int auctionId)
        {
            ImagesRepository imagesRepo = new ImagesRepository();
            return imagesRepo.GetImage(auctionId);
        }

        public void ProcessRequest(HttpContext context)
        {

            int auctionId = TryIntParse(context.Request.Params["auctionId"]);
            int width = TryIntParse(context.Request.Params["width"]);
            int height = TryIntParse(context.Request.Params["height"]);

            byte[] imgBytes = GetImageBytes(auctionId);

            context.Response.Clear();
            context.Response.ContentType = "HttpImageHandler/jpeg";

            if (width > 0 && height > 0)
                imgBytes = GetResizedImage(imgBytes, width, height);

            context.Response.OutputStream.Write(imgBytes, 0, imgBytes.Length);
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        byte[] GetResizedImage(byte[] imgData, int width, int height)
        {
            Bitmap imgIn;

            using (var ms = new MemoryStream(imgData))
            {
                imgIn = new Bitmap(ms);
            }

            double y = imgIn.Height;
            double x = imgIn.Width;
            double factor = 1;
            if (width > 0)
            {
                factor = width / x;
            }
            else if (height > 0)
            {
                factor = height / y;
            }
            System.IO.MemoryStream outStream = new System.IO.MemoryStream();
            Bitmap imgOut = new Bitmap((int)(x * factor), (int)(y * factor));
            Graphics g = Graphics.FromImage(imgOut);
            g.Clear(Color.White);
            g.DrawImage(imgIn, new Rectangle(0, 0, (int)(factor * x), (int)(factor * y)), new Rectangle(0, 0, (int)x, (int)y), GraphicsUnit.Pixel);
            imgOut.Save(outStream, ImageFormat.Jpeg);
            return outStream.ToArray();
        }
    }
}