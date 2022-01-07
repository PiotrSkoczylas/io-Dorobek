using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace io_Dorobek.Model
{
    public static class FileContentParser
    {
        public static List<PublicationListItem> GetDocumentInfo(PdfDocument pdf) //not finished
        {
            List<PublicationListItem> result=new List<PublicationListItem>();
            var metadata = MetadataExtract(pdf);
            if(metadata!=null)
            {
                result.Add(metadata);
            }
            //add content parser result
            //var dataFromContent = ContentParserInfoExtract(pdf);
            return result;
        }

        private static PublicationListItem MetadataExtract(PdfDocument pdf) //not finished
        {
            if(pdf.Info.Title==null)
                return null;
            else
            {
                PublicationListItem result = new PublicationListItem()
                {
                    Title = pdf.Info.Title,
                    Author = pdf.Info.Author,
                    Year = pdf.Info.CreationDate.Year,
                    FullDate = pdf.Info.CreationDate.ToString(),
                    KeyWords = pdf.Info.Keywords
                };
                return result;
            }
        }

        private static PublicationListItem ContentParserInfoExtract(PdfDocument pdf)
        {
            throw new NotImplementedException();
        }

    }
}
