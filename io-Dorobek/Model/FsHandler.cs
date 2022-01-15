using io_Dorobek.DAL.Repozytoria;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;

namespace io_Dorobek.Model
{
    static class FsHandler
    {
        static public PdfDocument FileLoader(string path)
        {
            if (PdfReader.TestPdfFile(path) != 0)
            {
                PdfDocument pdfDocument = PdfReader.Open(path);
                if (pdfDocument == null)
                    throw new Exception("Nie udało się otworzyć pliku");
                return pdfDocument;
            }
            return null;
        }

        static public void SavePdfFiles(List<int> idList, string path)
        {
            var repo = new PublicationRepo();
            foreach (int id in idList)
            {
                var a = repo.getPdf(id);
                a.Item1.Save(Path.Combine(path, string.Concat(a.Item2.Split(Path.GetInvalidFileNameChars()))));
            }
        }

        static public string CreateForPreview(int id)
        {
            var repo = new PublicationRepo();
            var a = repo.getPdf(id);
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tmp.pdf");
            a.Item1.Save(path);
            return path;
        }

        static public void SaveToBibtex(List<PublicationListItem> items, string path)
        {
            var repo = new PublicationRepo();
            string x = "";
            foreach (var item in items)
            {
                if (x.Length > 0)
                    x = $"{x},\n{item.GenerateBibTeX()}";
                else
                    x = item.GenerateBibTeX();
            }
            File.WriteAllText(path, x);
        }
    }
}
