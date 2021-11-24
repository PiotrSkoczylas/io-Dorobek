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

        public ListHandler()
        {
            var repo = new PublicationRepo();//rozwazyc przerobienie klasy na statyczna
            publications = new ObservableCollection<PublicationListItem>();
            foreach(var y in repo.getAll())
            {
                publications.Add(new PublicationListItem(y));
            }
        }

        public void Update()
        {
            var repo = new PublicationRepo();
            foreach (var y in repo.getAll())
            {
                if (publications.Contains(new PublicationListItem(y))) //potestowac
                    publications.Add(new PublicationListItem(y));
            }
        }

        public void RemoveElements(List<PublicationListItem> items)
        {
            var repo = new PublicationRepo();//
            repo.deleteById(items.Select(p => p.Id).ToList());
        }

        public void AddElement(PdfDocument pdf, PublicationListItem item)
        {
            var repo = new PublicationRepo();
            using (var x = new MemoryStream())
            {
                pdf.Save(x);
                repo.addPublication(x.ToArray(), item);
            }
        }

    }
}
