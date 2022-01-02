using io_Dorobek.Model;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace io_Dorobek.ViewModel
{
    public class AddPublicationViewModel : INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion
        private ListHandler listHandler { get; set; }

        public AddPublicationViewModel()
        {
            listHandler = new ListHandler();
            WybranaŚcieżka = "";
            W1_PublicationDate = "";
            W1_Title = "";
            W1_KeyWords = "";
            W1_IssnOfPaper = "";
            W1_IsbnOfPaper = "";
            W1_ArticleName = "";
            W1_Author = "";
            W1_DOI_VM = "";
        }
        public AddPublicationViewModel(ListHandler lst)
        {
            listHandler = lst;
            WybranaŚcieżka = "";
            W1_PublicationDate = "";
            W1_Title = "";
            W1_KeyWords = "";
            W1_IssnOfPaper = "";
            W1_IsbnOfPaper = "";
            W1_ArticleName = "";
            W1_Author = "";
            W1_DOI_VM = "";
        }

        private void FetchW1FieldsFromFile()
        {
            var fileInfo = FileContentParser.GetDocumentInfo(FsHandler.FileLoader(WybranaŚcieżka));
            if (fileInfo != null)
            {
                W1_Title = fileInfo.Title;
                W1_Author = fileInfo.Author;
                W1_PublicationDate = fileInfo.Year.ToString();
            }
        }

        #region Commands
        private ICommand AddPublicationToDb;
        public ICommand c_AddPublicationToDb
        {
            get
            {
                return AddPublicationToDb ?? (AddPublicationToDb = new RelayCommand(
                    (p) =>
                    {
                        try
                        {
                            PdfDocument doc = FsHandler.FileLoader(WybranaŚcieżka);
                            if (doc != null)
                            {
                                PublicationListItem item = new PublicationListItem()
                                {
                                    Title = W1_Title.Trim(),
                                    Author = W1_Author.Trim(),
                                    Doi = W1_DOI_VM.Trim(),
                                    Year = int.Parse(W1_PublicationDate)
                                };
                                listHandler.AddElement(doc, item);
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Windows.MessageBox.Show(ex.Message);
                        }
                    },
                    p => true)
                    );
            }
        }


        private ICommand BrowseForPdf;
        public ICommand c_BrowseForPdf
        {
            get
            {
                return BrowseForPdf ?? (BrowseForPdf = new RelayCommand(
                    (p) =>
                    {
                        using (var x = new OpenFileDialog())
                        {
                            x.Filter = "pdf file (*.pdf)|*.pdf";
                            x.Title = "Select pdf file";
                            if (x.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(x.FileName))
                            {
                                WybranaŚcieżka = x.FileName;
                                FetchW1FieldsFromFile();
                            }
                        }
                    },
                    p => true)
                    );
            }
        }


        #endregion

        #region Properties

        private string wybranaŚcieżka;
        public string WybranaŚcieżka
        {
            get { return wybranaŚcieżka; }
            private set
            {
                wybranaŚcieżka = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WybranaŚcieżka)));
            }
        }

        private string w1_Title;
        public string W1_Title
        {
            get { return w1_Title; }
            set
            {
                w1_Title = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(W1_Title)));
            }
        }

        private string w1_Author;
        public string W1_Author
        {
            get { return w1_Author; }
            set
            {
                w1_Author = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(W1_Author)));
            }
        }

        private string w1_PublicationDate;
        public string W1_PublicationDate
        {
            get { return w1_PublicationDate; }
            set
            {
                w1_PublicationDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(W1_PublicationDate)));
            }
        }

        private string w1_DOI_VM;
        public string W1_DOI_VM
        {
            get { return w1_DOI_VM; }
            set
            {
                w1_DOI_VM = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(W1_DOI_VM)));
            }
        }

        private string w1_IsbnOfPaper;
        public string W1_IsbnOfPaper
        {
            get { return w1_IsbnOfPaper; }
            set
            {
                w1_IsbnOfPaper = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(W1_IsbnOfPaper)));
            }
        }

        private string w1_IssnOfPaper;
        public string W1_IssnOfPaper
        {
            get { return w1_IssnOfPaper; }
            set
            {
                w1_IssnOfPaper = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(W1_IssnOfPaper)));
            }
        }

        private string w1_ArticleName;
        public string W1_ArticleName
        {
            get { return w1_ArticleName; }
            set
            {
                w1_ArticleName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(W1_ArticleName)));
            }
        }

        private string w1_KeyWords;
        public string W1_KeyWords
        {
            get { return w1_KeyWords; }
            set
            {
                w1_KeyWords = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(W1_KeyWords)));
            }
        }
        #endregion
    }
}
