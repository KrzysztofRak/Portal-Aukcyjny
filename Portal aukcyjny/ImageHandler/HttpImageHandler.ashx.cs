using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using Model.Repositories;

namespace ImageHandler
{
    public class HttpImageHandler : IHttpHandler
    {
        public static int TryIntParse(string source)
        {
            int val;
            return int.TryParse(source, out val) ? val : -1;
        }

        public byte[] GetImageBytes(int auctionId, int imageId)
        {
            ImagesRepository imagesRepo = new ImagesRepository();
            if(auctionId != -1)
                return imagesRepo.GetImageByAuctionId(auctionId);
            else
                return imagesRepo.GetImageById(imageId);
        }

        public void ProcessRequest(HttpContext context)
        {
            int auctionId = TryIntParse(context.Request.Params["auctionId"]);
            int imageId = TryIntParse(context.Request.Params["id"]);
            int width = TryIntParse(context.Request.Params["width"]);
            int height = TryIntParse(context.Request.Params["height"]);

            byte[] imgBytes = GetImageBytes(auctionId, imageId);
            if(imgBytes == null && auctionId != -1)
                imgBytes = File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/Images/defaultAuctionImg.jpg"));

            if (imgBytes != null && width > 0 && height > 0)
                imgBytes = GetResizedImage(imgBytes, width, height);

            context.Response.Clear();
            context.Response.ContentType = "image/jpeg";

            if(imgBytes != null)
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
                imgIn = new Bitmap(ms);

            double y = imgIn.Height;
            double x = imgIn.Width;
            double factor = 1;

            if (width > 0)
                factor = width / x;
            else if (height > 0)
                factor = height / y;

            MemoryStream outStream = new MemoryStream();
            Bitmap imgOut = new Bitmap((int)(x * factor), (int)(y * factor));
            Graphics g = Graphics.FromImage(imgOut);

            g.Clear(Color.White);
            g.DrawImage(imgIn, new Rectangle(0, 0, (int)(factor * x), (int)(factor * y)), new Rectangle(0, 0, (int)x, (int)y), GraphicsUnit.Pixel);

            imgOut.Save(outStream, ImageFormat.Jpeg);

            return outStream.ToArray();
        }
    }
}