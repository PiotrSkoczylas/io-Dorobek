using System.Collections.Generic;

namespace io_Dorobek.Model
{
    public class JsonDataModel
    {
        public List<string> title { get; set; }
        public string authors { get; set; }
        public List<string> doi { get; set; }
        public List<string> keywords { get; set; }
        public JsonDataModel()
        {
            title = new List<string>();
            authors = "";
            doi = new List<string>();
            keywords = new List<string>();
        }
    }
}
