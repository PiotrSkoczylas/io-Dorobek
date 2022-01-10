using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace io_Dorobek.Model
{
    public class JsonDataModel
    {
        public List<string> titles { get; set; }
        public string authors { get; set; }
        public List<string> doi { get; set; }
        public List<string> keywords { get; set; }
        public JsonDataModel()
        {
            titles = new List<string>();
            authors = "";
            doi = new List<string>();
            keywords = new List<string>();
        }
    }
}
