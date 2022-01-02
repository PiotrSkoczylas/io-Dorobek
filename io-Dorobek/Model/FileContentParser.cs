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
        public static PublicationListItem GetDocumentInfo(PdfDocument pdf) //not finished
        {
            var metadata = MetadataExtract(pdf);
            if(metadata!=null)
            {
                return metadata;
            }
            //add content parser result
            //var dataFromContent = ContentParserInfoExtract(pdf);
            return null;
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
                    Year = pdf.Info.CreationDate.Year
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
