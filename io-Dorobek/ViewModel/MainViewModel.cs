using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace io_Dorobek.ViewModel
{

    class MainViewModel
    {
        private List<Publication> pozycje = new List<Publication>();
        public List<Publication> Pozycje
        {
            get { return pozycje; }
            private set
            {
                for (int i = 0; i < value.Count(); i++)
                { pozycje[i] = value[i]; }
            }
        }


        public class Publication
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public uint Year { get; set; }
            public string DOI { get; set; }
        }


        public MainViewModel()
        {
            //Poniższe dane jeszcze do zmiany! Wpisane częściowo na podstawie fizycznych książek, a nie prac naukowych w formacie pdf
            Pozycje.Add(new Publication() {Title = "Jakiś tytuł", Author = "Jakiś autor", Year = 1492, DOI = "123456789"});
            Pozycje.Add(new Publication() {Title = "Rachunek różniczkowy\n i całkowy", Author = "G. M. Fichtenholz", Year = 1994, DOI = "-"});
            Pozycje.Add(new Publication() {Title = "Matematyka\n dla studentów wyższych uczelni technicznych", Author = "Radosław Grzymkowski", Year = 2009, DOI = "-"});
        }



    }
}
