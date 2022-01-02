using io_Dorobek.DAL.Repozytoria;
using io_Dorobek.Model;
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


        public class Publication //->PublicationListItem
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public uint Year { get; set; }
            public string DOI { get; set; }
        }

        private ListHandler listHandler { get; set; }

        public MainViewModel()
        {
            listHandler = new ListHandler();
             listHandler.publications;
            //Poniższe dane jeszcze do zmiany! Wpisane częściowo na podstawie fizycznych książek, a nie prac naukowych w formacie pdf
            //Pozycje.Add(new Publication() {Title = "Jakiś tytuł", Author = "Jakiś autor", Year = 1492, DOI = "123456789"});
            //Pozycje.Add(new Publication() {Title = "Rachunek różniczkowy\n i całkowy", Author = "G. M. Fichtenholz", Year = 1994, DOI = "-"});
            //Pozycje.Add(new Publication() {Title = "Matematyka\n dla studentów wyższych uczelni technicznych", Author = "Radosław Grzymkowski", Year = 2009, DOI = "-"});
        }



        //Zmienne których dot. binding w plikach xaml


        //Lista rzeczy do wyswietlenia w Combobox w okienku głównym aplikacji
        private List<string> path_of_combobox = new List<string>();
        public List<string> Path_of_combobox
        {
            get { return path_of_combobox; }
            private set
            {
                for (int i = 0; i < value.Count(); i++)
                { path_of_combobox[i] = value[i]; }
            }
        }


        #region Proporties
        //Zmienne występujące przy dodawaniu nowej pozycji (pola tekstowe uzupełniane przez aplikację, i potem ewentualnie modyfikowane przez użytkownika)
        private string wybranaŚcieżka = "";
        public string WybranaŚcieżka
        {
            get { return wybranaŚcieżka; }
            private set
            {
                    wybranaŚcieżka = value;
            }
        }

        private string w1_Title = "";
        public string W1_Title
        {
            get { return w1_Title; }
            private set
            {
                w1_Title = value;
            }
        }

        private string w1_Author = "";
        public string W1_Author
        {
            get { return w1_Author; }
            private set
            {
                w1_Author = value;
            }
        }

        private string w1_PublicationDate = "";
        public string W1_PublicationDate
{
            get { return w1_PublicationDate; }
            private set
            {
                w1_PublicationDate = value;
            }
        }

        private string w1_DOI_VM = "";
        public string W1_DOI_VM
        {
            get { return w1_DOI_VM; }
            private set
            {
                w1_DOI_VM = value;
            }
        }

        private string w1_IsbnOfPaper = "";
        public string W1_IsbnOfPaper
        {
            get { return w1_IsbnOfPaper; }
            private set
            {
                w1_IsbnOfPaper = value;
            }
        }

        private string w1_IssnOfPaper = "";
        public string W1_IssnOfPaper
        {
            get { return w1_IssnOfPaper; }
            private set
            {
                w1_IssnOfPaper = value;
            }
        }

        private string w1_ArticleName = "";
        public string W1_ArticleName
        {
            get { return w1_ArticleName; }
            private set
            {
                w1_ArticleName = value;
            }
        }

        private string w1_KeyWords = "";
        public string W1_KeyWords
        {
            get { return w1_KeyWords; }
            private set
            {
                w1_KeyWords = value;
            }
        }
        #endregion

        #region Commands

        #endregion



    }
}
