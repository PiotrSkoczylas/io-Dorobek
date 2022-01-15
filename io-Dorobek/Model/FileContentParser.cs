using Newtonsoft.Json;
using PdfSharp.Pdf;
using System;
using System.IO;

namespace io_Dorobek.Model
{
    public static class FileContentParser
    {
        public static ExtractedDataModel GetDocumentInfo(PdfDocument pdf)
        {
            ExtractedDataModel result = new ExtractedDataModel();
            var metadata = MetadataExtract(pdf);
            if (metadata != null)
            {
                result.AppendFromDocumentInformation(metadata);
            }
            result.AppendFromJsonDataModel(ContentParserInfoExtract(pdf));
            return result;
        }

        private static PdfDocumentInformation MetadataExtract(PdfDocument pdf)
        {
            if (pdf.Info.Title == null)
                return null;
            return pdf.Info;
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
            using (var process = System.Diagnostics.Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    //string stderr = process.StandardError.ReadToEnd();
                    string result_string = reader.ReadToEnd();
                    if (result_string.Length > 0)
                    {
                        result_string = result_string.Substring(1, result_string.Length - 1);
                        result_string = result_string.Substring(0, result_string.Length - 1).Replace("\r", "");
                        result_string = result_string.Substring(0, result_string.Length - 1).Replace("'", "");
                        //System.Windows.MessageBox.Show(result_string);
                        try
                        {
                            //JsonConvert.DeserializeObject<List<JsonDataModel>>(result_string);
                            var tmp = JsonConvert.DeserializeObject<JsonDataModel>(result_string);
                            if (tmp != null)
                            {
                                result = tmp;
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    //System.Windows.MessageBox.Show(stderr); //for debugging only
                }
            }
            return result;
        }

    }
}
