using io_Dorobek.DAL.Repozytoria;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace io_Dorobek.Model
{
    public class ListHandler
    {
        public ObservableCollection<PublicationListItem> publications { get; set; }
        private ObservableCollection<PublicationListItem> unfiltered { get; set; }
        public ListHandler()
        {
            //var repo = new PublicationRepo();//rozwazyc przerobienie klasy na statyczna
            publications = new ObservableCollection<PublicationListItem>();
            unfiltered = new ObservableCollection<PublicationListItem>();
            Update();
        }

        public void Filter(string text)
        {
            var repo = new PublicationRepo();
            publications.Clear();
            foreach (var y in unfiltered)
            {
                if (y.Title.ToUpper().Contains(text.ToUpper()) || y.Author.ToUpper().Contains(text.ToUpper()))
                {
                    publications.Add(y);
                }
            }
        }

        private void Update()
        {
            var repo = new PublicationRepo();
            publications.Clear();
            foreach (var y in repo.getAll())
            {
                publications.Add(new PublicationListItem(y));
                unfiltered.Add(new PublicationListItem(y));
            }
        }

        public void RemoveElements(List<PublicationListItem> items)
        {
            var repo = new PublicationRepo();
            repo.deleteById(items.Select(p => p.Id).ToList());
            Update();
        }

        public void AddElement(PdfDocument pdf, PublicationListItem item)
        {
            var repo = new PublicationRepo();
            using (var x = new MemoryStream())
            {
                pdf.Save(x);
                repo.addPublication(x.ToArray(), item);
            }
            Update();
        }

        public void EditElement(PublicationListItem item)
        {
            var repo = new PublicationRepo();
            repo.EditItem(item);
            Update();
        }
    }
}
