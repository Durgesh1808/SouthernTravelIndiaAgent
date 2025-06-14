﻿using SouthernTravelIndiaAgent.Common;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class JpegImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Create a CAPTCHA image using the text stored in the Session object.			
            this.Session["CaptchaImageText"] = GenerateRandomCode();

            CaptchaImage ci = new CaptchaImage(this.Session["CaptchaImageText"].ToString(), 125, 32, "Century Schoolbook");

            // Change the response headers to output a JPEG image.

            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";
            // Write the image to the response stream in JPEG format.
            ci.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);
            // Dispose of the CAPTCHA image object.
            ci.Dispose();
        }

        /// <summary>
        /// // Generates a random 6-digit code for CAPTCHA.
        /// </summary>
        /// <returns></returns>
        public string GenerateRandomCode()
        {
            string s = "";
            Random r = new Random();
            for (int i = 0; i < 6; i++)
                s = String.Concat(s, r.Next(10).ToString());
            return s;
        }
    }
}