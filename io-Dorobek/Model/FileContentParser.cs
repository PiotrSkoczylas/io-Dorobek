using Newtonsoft.Json;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace io_Dorobek.Model
{
    public static class FileContentParser
    {
        public static ExtractedDataModel GetDocumentInfo(PdfDocument pdf) //not finished
        {
            ExtractedDataModel result=new ExtractedDataModel();
            var metadata = MetadataExtract(pdf);
            if(metadata!=null)
            {
                result.AppendFromDocumentInformation(metadata);
            }
            result.AppendFromJsonDataModel(ContentParserInfoExtract(pdf));
            return result;
        }

        private static PdfDocumentInformation MetadataExtract(PdfDocument pdf) //not finished
        {
            if(pdf.Info.Title==null)
                return null;
            //else
           // {
                //PublicationListItem result = new PublicationListItem()
                //{
                //    Title = pdf.Info.Title,
                //    Author = pdf.Info.Author,
                //    Year = pdf.Info.CreationDate.Year,
                //    FullDate = pdf.Info.CreationDate.ToString(),
                //    KeyWords = pdf.Info.Keywords
                //};
            return pdf.Info;
            //}
        }

        private static JsonDataModel ContentParserInfoExtract(PdfDocument pdf)
        {
            JsonDataModel result = new JsonDataModel();
            System.Diagnostics.ProcessStartInfo start = new System.Diagnostics.ProcessStartInfo();
            start.FileName = @"python.exe";
            start.Arguments = String.Format("\"{0}\" \"{1}\"", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "read_pdf_data.py"), pdf.FullPath);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;
            start.CreateNoWindow = true;
            start.LoadUserProfile = true;
            using(var process = System.Diagnostics.Process.Start(start))
            {
                using(StreamReader reader = process.StandardOutput)
                {
                    //string stderr = process.StandardError.ReadToEnd();
                    string result_string = reader.ReadToEnd();
                    System.Windows.MessageBox.Show(result_string);
                    try
                    {
                        //JsonConvert.DeserializeObject<List<JsonDataModel>>(result_string);
                        var tmp = JsonConvert.DeserializeObject<JsonDataModel>(result_string);
                        if(tmp!=null)
                        {
                            result = tmp;
                        }
                    }
                    catch(Exception ex)
                    {

                    }
                    //System.Windows.MessageBox.Show(stderr); //for debugging only
                }
            }
            return result;
        }

    }
}
