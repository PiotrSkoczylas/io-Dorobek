using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace io_Dorobek.DAL.Encje
{
    public class Publication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public byte[]  PdfFile { get; set; }
    }
}