using PdfSharp.Pdf;
using System.Collections.Generic;

namespace io_Dorobek.Model
{
    public class ExtractedDataModel
    {
        public List<string> Titles { get; set; }
        public List<string> Authors { get; set; }
        public List<int> Years { get; set; }
        public List<string> FullDate { get; set; }
        public List<string> Keywords { get; set; }
        public List<string> Doi { get; set; }
        public List<string> Isbn { get; set; }
        public List<string> Issn { get; set; }
        public List<string> Article { get; set; }
        public ExtractedDataModel()
        {
            Titles = new List<string>();
            Authors = new List<string>();
            Years = new List<int>();
            FullDate = new List<string>();
            Keywords = new List<string>();
            Doi = new List<string>();
            Isbn = new List<string>();
            Issn = new List<string>();
            Article = new List<string>();
        }

        public void AppendFromDocumentInformation(PdfDocumentInformation info)
        {
            Titles.Add(info.Title);
            Authors.Add(info.Author);
            Years.Add(info.CreationDate.Year);
            FullDate.Add(info.CreationDate.ToString());
            Keywords.Add(info.Keywords);
        }

        public void AppendFromJsonDataModel(JsonDataModel jsonDataModel)
        {
            foreach (var x in jsonDataModel.title)
            {
                Titles.Add(x);
            }
            if (jsonDataModel.authors != string.Empty)
            {
                Authors.Add(jsonDataModel.authors);
            }
            foreach (var x in jsonDataModel.doi)
            {
                Doi.Add(x);
            }
            foreach (var x in jsonDataModel.keywords)
            {
                Keywords.Add(x);
            }
        }

        public void AppendFromBibTeXLine(string line)
        {
            var x = line.Split('=');
            if(x.Length>1)
            {
                var y = x[1].Trim().Replace("\"","");
                if (y[y.Length - 1] == ',')
                {
                    y = y.Substring(0, y.Length - 1);
                }
                switch (x[0].Trim())
                {
                    case "author": Authors.Add(y); break;
                    case "title": Titles.Add(y); break;
                    case "year": Years.Add(int.Parse(y)); break;
                    case "full_date": FullDate.Add(y); break;
                    case "keywords": Keywords.Add(y); break;
                    case "article": Article.Add(y); break;
                    case "isbn": Isbn.Add(y); break;
                    case "issn": Issn.Add(y); break;
                    case "doi": Doi.Add(y); break;
                    default: break;
                }
            }
        }
    }
}
