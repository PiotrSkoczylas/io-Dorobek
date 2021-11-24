using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace io_Dorobek.DAL.Encje
{
    public class Publication
    {
        [Key]
        public uint Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public string Doi { get; set; }
        public byte[]  PdfFile { get; set; }
    }
}
