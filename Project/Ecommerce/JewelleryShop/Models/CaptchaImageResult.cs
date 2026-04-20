using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

using System.Drawing;
using System.Diagnostics;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Drawing.Drawing2D;

namespace JewelleryShop.Models
{
    public class CaptchaImageResult : ActionResult
    {

        public override void ExecuteResult(ControllerContext context)
        {
            Bitmap oBitmap = new Bitmap(170, 41);

            Graphics oGraphic = Graphics.FromImage(oBitmap);

            //Next, we create two brushes, which will help us to draw our image. 
            //The first one, oBrush, will be used to draw the background of the image:

            SolidBrush oBrush = new SolidBrush(Color.BlanchedAlmond);

            //We're going to use the first brush to fill the image rectangle, 

            oGraphic.FillRectangle(oBrush, 0, 0, 170, 41);

            //So far, we've changed the background color of our image, 
            //but we haven't written any text. 

            string sText = GetRandomText();

            context.HttpContext.Session["captchastring"] = sText;

            Font oFont = new Font("Forte", 34);
            PointF oPoint1 = new PointF(0f, 0f);

            //we hardcode the brush color as Black.

            SolidBrush oBrushWrite = new SolidBrush(Color.BlueViolet);

            //so now we created instances of Font and PointF and now DrawString parameters are ready:

            oGraphic.DrawString(sText, oFont, oBrushWrite, oPoint1);

            //Now the last thing is we have to send the image to browser for that.
            //That's all we generated a Text image dynamically at run time.
            //The below image shows the sample output:
            HttpResponseBase response = context.HttpContext.Response;

            response.ContentType = "image/jpeg";

            oBitmap.Save(response.OutputStream, ImageFormat.Jpeg);


        }

        private string GetRandomText()
        {
            string uniqueID = Guid.NewGuid().ToString();
            //65608890676879898
            string randString = "";

            for (int j = 0; j <= 4; j++)
            {
                randString += uniqueID.ToCharArray()[j];
            }

            return randString;

        }

    }

}