using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public ExtractedDataModel()
        {
            Titles = new List<string>();
            Authors = new List<string>();
            Years = new List<int>();
            FullDate = new List<string>();
            Keywords = new List<string>();
            Doi = new List<string>();
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
            foreach(var x in jsonDataModel.title)
            {
                Titles.Add(x);
            }
            if(jsonDataModel.authors != string.Empty)
            {
                Authors.Add(jsonDataModel.authors);
            }
            foreach(var x in jsonDataModel.doi)
            {
                Doi.Add(x);
            }
            foreach(var x in jsonDataModel.keywords)
            {
                Keywords.Add(x);
            }
        }
    }
}
