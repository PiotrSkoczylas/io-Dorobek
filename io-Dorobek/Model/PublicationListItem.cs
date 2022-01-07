using io_Dorobek.DAL.Encje;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace io_Dorobek.Model
{
    public class PublicationListItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string FullDate { get; set; }
        public string Doi { get; set; }
        public string Issn { get; set; }
        public string Isbn { get; set; }
        public string ArticleName { get; set; }
        public string KeyWords { get; set; }
        public PublicationListItem() {}
        public PublicationListItem(Publication src)
        {
            Id = src.Id;
            Title = src.Title;
            Author = src.Author;
            Year = src.Year;
            FullDate = src.FullDate;
            Doi = src.Doi;
            Issn = src.Issn;
            Isbn = src.Isbn;
            ArticleName = src.ArticleName;
            KeyWords = src.KeyWords;
        }
    }
}
