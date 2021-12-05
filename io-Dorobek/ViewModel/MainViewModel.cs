using io_Dorobek.DAL.Repozytoria;
using io_Dorobek.Model;
using io_Dorobek.View;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace io_Dorobek.ViewModel
{

    class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<PublicationListItem> pozycje;
        public ObservableCollection<PublicationListItem> Pozycje
        {
            get { return pozycje; }
            private set 
            { 
                pozycje = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string name)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private ListHandler listHandler { get; set; }
        public MainViewModel()
        {
            listHandler = new ListHandler();
            Pozycje = listHandler.publications;

            //TestAddItem();
            //UpdateList();
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


        #region Proporties for Window1
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


        private ICommand SelectionChanged;
        public ICommand c_SelectionChanged
        {
            get
            {
                return SelectionChanged ?? (SelectionChanged = new RelayCommand(
                    (p) =>
                    {
                        System.Collections.IList items = (System.Collections.IList)p;
                        var collection = items.Cast<PublicationListItem>();
                        foreach(var item in collection)
                        {
                            var xa = Pozycje.Where(x => x.Id == item.Id).FirstOrDefault();
                            if (xa != null)
                            {
                                xa.ChangeCheck();
                            }
                        }
                    },
                    p => true)
                    );
            }
        }


        private ICommand DeleteItems;
        public ICommand c_DeleteItems
        {
            get
            {
                return DeleteItems ?? (DeleteItems = new RelayCommand(
                    (p) =>
                    {
                        listHandler.RemoveElements(
                            Pozycje.Where(x => x.Checked).ToList()
                            );
                    },
                    p => true)
                    );
            }
        }

        private ICommand SaveFiles;
        public ICommand c_SaveFiles
        {
            get
            {
                return SaveFiles ?? (SaveFiles = new RelayCommand(
                    (p) =>
                    {
                        using (FolderBrowserDialog x = new FolderBrowserDialog())
                        {
                            var path = x.ShowDialog();
                            if (path == DialogResult.OK && !string.IsNullOrWhiteSpace(x.SelectedPath))
                            {
                                FsHandler.SavePdfFiles(
                                    Pozycje.Where(z => z.Checked).Select(a => a.Id).ToList(), 
                                    path.ToString());
                            }
                        }
                    },
                    p => true)
                    );
            }
        }

        private ICommand SaveBibtex;
        public ICommand c_SaveBibtex
        {
            get
            {
                return SaveBibtex ?? (SaveBibtex = new RelayCommand(
                    (p) =>
                    {
                        using(SaveFileDialog x = new SaveFileDialog())
                        {
                            x.Filter = "bibtex file (*.bibtex)|*.bibtex"; 
                            if (x.ShowDialog() == DialogResult.OK)
                            {
                                Stream outStream;
                                if ((outStream = x.OpenFile()) != null)
                                {
                                    // Code to write the stream goes here.
                                    outStream.Close();
                                }
                            }
                            //FsHandler.SaveToBibtex(Pozycje.Where(x => x.Checked).ToList(), path);
                        }
                    },
                    p => true)
                    );
            }
        }

        private ICommand CallAddWindow;
        public ICommand c_CallAddWindow
        {
            get
            {
                return CallAddWindow ?? (CallAddWindow = new RelayCommand(
                    (p) =>
                    {
                        var window = new Window1();
                        window.Show();
                    },
                    p => true)
                    );
            }
        }

        private ICommand CallEditWindow;
        public ICommand c_CallEditWindow
        {
            get
            {
                return CallEditWindow ?? (CallEditWindow = new RelayCommand(
                    (p) =>
                    {
                        var window = new Window2();
                        window.Show();
                    },
                    p => true)
                    );
            }
        }

        private ICommand FilterResults;
        public ICommand c_FilterResults //wywołanie przy edycji paska wyszukiwania
        {
            get
            {
                return FilterResults ?? (FilterResults = new RelayCommand(
                    (p) =>
                    {
                        listHandler.Filter(p_searchBar);
                    },
                    p => true)
                    );
            }
        }

        //private ICommand ChangeSelectionOnItem;
        //public ICommand c_ChangeSelectionOnItem
        //{
        //    get
        //    {
        //        return ChangeSelectionOnItem ?? (ChangeSelectionOnItem = new RelayCommand(
        //            (p) =>
        //            {

        //            },
        //            p => true)
        //            );
        //    }
        //}


        #endregion

        #region Properties Main

        private string searchBar;
        public string p_searchBar
        {
            get { return searchBar; }
            set
            {
                searchBar = value;
                listHandler.Filter(searchBar);//tymczasowo tutaj
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(p_searchBar)));
            }
        }

        #endregion

    }
}
