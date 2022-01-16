using io_Dorobek.DAL.Encje;

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
        public PublicationListItem() { }
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
        public string GenerateBibTeX()
        {
            string result = $"@misc{{";
            if (Author != "" && Author != null)
            {
                result = $"{result}\n\tauthor=\"{Author}\",";
            }
            if (Title != "" && Title != null)
            {
                result = $"{result}\n\ttitle=\"{Title}\",";
            }
            if (Year != 0)
            {
                result = $"{result}\n\tyear=\"{Year}\",";
            }
            if (FullDate != "" && FullDate != null)
            {
                result = $"{result}\n\tfull_date=\"{FullDate}\",";
            }
            if (Doi != "" && Doi != null)
            {
                result = $"{result}\n\tdoi=\"{Doi}\",";
            }
            if (Issn != "" && Issn != null)
            {
                result = $"{result}\n\tissn=\"{Issn}\",";
            }
            if (Isbn != "" && Isbn != null)
            {
                result = $"{result}\n\tisbn=\"{Isbn}\",";
            }
            if (ArticleName != "" && ArticleName != null)
            {
                result = $"{result}\n\tarticle=\"{ArticleName}\",";
            }
            if (KeyWords != "" && KeyWords != null)
            {
                result = $"{result}\n\tkeywords=\"{KeyWords}\",";
            }
            result = result.Substring(0, result.Length - 1);
            result = $"{result}\n}}";

            return result;
        }
    }
}
