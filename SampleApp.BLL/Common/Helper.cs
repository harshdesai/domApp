using bomSampleApp.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Net.Mail;
using System.Web;
using EO.Pdf;
using System.Configuration;
using System.Drawing;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Web.Mvc;
using SendGrid;
using System.Net;

namespace bomSampleApp.BLL.Common
{
    public class Helper
    {
        public static String CombinePath(string directoryPath, string fileName)
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
            return Path.Combine(directoryPath, fileName);
        }

        public static string CombineURL(string str1, string str2)
        {
            str1 = str1.TrimEnd('/');
            str2 = str2.TrimStart('/');
            return str1 + "/" + str2;
        }

        public static string ApplicationRegistrationText
        {
            get
            {
                return "content/Application.html";
            }
        }

        public static string ApplicatinLogFile
        {
            get
            {
                return "log/";
            }
        }

        public static string getRegistrationFileName()
        {
            return "Appliaction.pdf";
        }

        public static string GetHtmlStringFromUrl(string urlAddress)
        {
            string data = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                data = readStream.ReadToEnd();
            }
            return data;
        }

        public static string getRegistrationHtml(Patient _patientDetail)
        {


            string html = GetHtmlStringFromUrl("http://domsample.azurewebsites.net/content/Application.html");
            //string html = "<div style='margin: 0 auto; width: 90%'><div width:'100%'><div style='width:250px;float:left;'><img src='http://domsample.azurewebsites.net/img/application.gif'></div>";
            //html = System.IO.File.ReadAllText(Helper.CombinePath(HttpContext.Current.Server.MapPath("~"), Helper.ApplicationRegistrationText));
            html = html.Replace("##FirstName##", _patientDetail.FirstName.ToString());
            html = html.Replace("##LastName##", _patientDetail.LastName);
            html = html.Replace("##wifeFirstName##", _patientDetail.WifeFirstName);
            html = html.Replace("##HusbandEmail##", _patientDetail.Email);
            html = html.Replace("##HomePh##", _patientDetail.PhoneNumber);
            html = html.Replace("##MaidenName##", _patientDetail.MaidenName);

            return html;
        }

        public static bool sendMail(string subject, string body, string to, MemoryStream contentStream, string fileName)
        {
            string from = ConfigurationManager.AppSettings["FromEmail"].ToString();
            string password = ConfigurationManager.AppSettings["FromPassword"].ToString();
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(to);
            mail.From = new MailAddress("testbom7@gmail.com", "Application Completed", System.Text.Encoding.UTF8);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            if (contentStream != null)
            {
                if (fileName == "")
                    fileName = "Application.pdf";
                contentStream.Position = 0;
                System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
                Attachment attach = new System.Net.Mail.Attachment(contentStream, ct);
                attach.ContentDisposition.FileName = fileName;

                mail.Attachments.Add(attach);

            }

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("testbom7@gmail.com", "Rewq4321");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            try
            {
                client.Send(mail);
                if (contentStream != null)
                    contentStream.Close();
                return true;
            }
            catch (Exception ex)
            {
                if (contentStream != null)
                    contentStream.Close();
                Exception ex2 = ex;
                string errorMessage = string.Empty;
                while (ex2 != null)
                {
                    errorMessage += ex2.ToString();
                    ex2 = ex2.InnerException;
                }
                return false;
            }
        }

        public static MemoryStream getPdf(Patient _patientDetail)
        {
            string html = Helper.getRegistrationHtml(_patientDetail);
            //string html = "";
            byte[] _MemoryStream = Helper.convertHTMLToPdf(html);

            return new MemoryStream(_MemoryStream);
        }

        public static byte[] convertHTMLToPdf(string html)
        {
            MemoryStream msOutput = new MemoryStream();
            // try
            // {
            //PdfDocument doc = new PdfDocument();
            //String htmlPdfLicense = "2czY6hbIiu246f/Mpev3Cuy8drOzBBTmp9j4Bh3kd4SOzdrrotrp/x7kd4SO" +
            //                  "dePt9BDtrNzCnrWfWZekzRfonNzyBBDInbW3x+Kvbqe0xeCwdabw+g7kp+rp" +
            //                  "2g+9RoGkscufdePt9BDtrNzpz+eupeDn9hnyntzCnrWfWZekzQzrpeb7z7iJ" +
            //                  "WZekscufWZfA8g/jWev9ARC8W7zTv/vjn5mkBxDxrODz/+ihbKW0s8uud4SO" +
            //                  "scufWbOz8hfrqO7CnrWfWZekzRrxndz22hnlqJfo8h/kdpm6wOKua6e1ws2f" +
            //                  "r9z2BBTup7SmxM2faLWRm8ufWZfABBTmp9j4Bh3kd9j3AvbFbA==";

            //EO.Pdf.Runtime.AddLicense(htmlPdfLicense);

            //HtmlToPdf.Options.PageSize = new SizeF(8.5F, 11.0F);

            //HtmlToPdf.Options.HeaderHtmlPosition = 0F;
            //HtmlToPdf.Options.ColumnGapWidth = 100;
            //HtmlToPdf.Options.OutputArea = new RectangleF(0.5F, 0.5F, 7.5F, 10.0F);
            //HtmlToPdfResult htmlToPdfResult = HtmlToPdf.ConvertHtml(html, msOutput);
            //  }
            //  catch (Exception ex)
            //  {
            //WriteToLogFile("Error in convertHTMLToPdf :" + ex.Message, ApplicatinLogFile);
            //  }

            byte[] result;
            using (MemoryStream ms = new MemoryStream())
            {
                Document pDoc = new Document(PageSize.A4,0, 0, 0, 0);
                PdfWriter writer = PdfWriter.GetInstance(pDoc, ms);
                pDoc.Open();

                StringReader sr = new StringReader(html);
                //XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, sr);
                HTMLWorker htmlparser = new HTMLWorker(pDoc);
                htmlparser.Parse(sr);

                //here you can create your own pdf.

                pDoc.Close();
                result = ms.GetBuffer();
            }

            return result;


            // return msOutput.ToArray();
        }

        public static void WriteToLogFile(string strMessage, string outputFile)
        {
            string line = DateTime.Now.ToString() + " | ";
            line += strMessage;
            System.IO.FileStream fs = new System.IO.FileStream(outputFile, FileMode.Append, FileAccess.Write, FileShare.None);
            StreamWriter swFromFileStream = new StreamWriter(fs);
            swFromFileStream.WriteLine(line);
            swFromFileStream.Flush();
            swFromFileStream.Close();
        }
    }
}
