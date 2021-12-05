using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using io_Dorobek.DAL.Repozytoria;
using PdfSharp.Pdf;
using System.IO;
using PdfSharp.Pdf.IO;

namespace io_Dorobek.Model
{
    static class FsHandler
    {
        static public PdfDocument FileLoader(string path)
        {
            PdfDocument pdfDocument = PdfReader.Open(path);
            if (pdfDocument == null)
                throw new Exception("Nie udało się otworzyć pliku");
            return pdfDocument;
        }

        static public void SavePdfFiles(List<int> idList, string path)
        {
            var repo = new PublicationRepo();
            foreach (int id in idList)
            {
                var a = repo.getPdf(id);
                a.Item1.Save(Path.Combine(path, a.Item2));
            }
        }

        static public void SaveToBibtex(List<PublicationListItem> items, string path)//1 plik wyjsciowy dla wielu wpisow - z rozszerzeniem bibtex
        {
            var repo = new PublicationRepo();
            string x = "";
            foreach(var item in items)
            {
                x = $"{x}, @misc{{\n\tauthor=\"{item.Author}\",\n\ttitle=\"{item.Title}\",\n\tyear={item.Year},\n\tdoi=\"{item.Doi}\"\n}}\n";
            }
            File.WriteAllText(path, x);
        }
    }
}
