using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace WEB.Auction
{
    /// <summary>
    /// CheckCode 的摘要说明
    /// </summary>
    public class CheckCode : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string g = GenerateCheckCode();
            CreateCheckCodeImage(g);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private string GenerateCheckCode()
        {
            int number;
            char code;
            string checkCode = String.Empty;

            System.Random random = new Random();

            for (int i = 0; i < 5; i++)
            {
                number = random.Next();

                if (number % 2 == 0)
                    //生成'0'-'9'字符
                    code = (char)('0' + (char)(number % 10));
                else
                    //生成'A'-'Z'字符
                    code = (char)('A' + (char)(number % 26));

                checkCode += code.ToString();
            }

            HttpContext.Current.Response.Cookies.Add(new HttpCookie("checkcode", checkCode));

            return checkCode;
            //两个字符相加等于=asicc码加
        }


        private void CreateCheckCodeImage(string checkCode)
        {
            if (checkCode == null || checkCode.Trim() == String.Empty)
                return;
            //(int)Math.Ceiling((checkCode.Length * 12.5)),22
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(80, 30);
            Graphics g = Graphics.FromImage(image);

            try
            {
                //生成随机生成器
                Random random = new Random();

                //清空图片背景色
                g.Clear(Color.White);

                //画图片的背景噪音线
                for (int i = 0; i < 5; i++)
                {
                    int x1 = random.Next(image.Width + 10);
                    int x2 = random.Next(image.Width + 10);
                    int y1 = random.Next(image.Height + 10);
                    int y2 = random.Next(image.Height + 10);

                    Pen p = new Pen(Color.Gray, 4);
                    g.DrawLine(p, x1, y1, x2, y2);
                }

                Font font = new System.Drawing.Font("Arial", 15, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic));
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, System.Drawing.Color.DarkRed, 1.2f, true);
                g.DrawString(checkCode, font, brush, 5, 5);

                //画图片的前景噪音点
                for (int i = 0; i < 200; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);

                    image.SetPixel(x, y, System.Drawing.Color.FromArgb(random.Next()));
                }

                //画图片的边框线
                g.DrawRectangle(new System.Drawing.Pen(System.Drawing.Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);

                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ContentType = "image/jpg";
                HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                //g.Dispose();
                //image.Dispose();
            }
        }
    }
}