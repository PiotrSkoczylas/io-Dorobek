using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace io_Dorobek.Model
{
    internal class Publication
    {
        string Title { get; set; }
        string Author { get; set; }
        uint Year { get; set; }
        string DOI { get; set; }
    }
}
