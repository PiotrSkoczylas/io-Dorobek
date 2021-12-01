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
        public bool Checked { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string Doi { get; set; }

        public PublicationListItem()
        {
            Checked = false;
        }
        public PublicationListItem(Publication src)
        {
            Id = src.Id;
            Title = src.Title;
            Author = src.Author;
            Year = src.Year;
            Doi = src.Doi;
            Checked = false;
        }
    }
}
