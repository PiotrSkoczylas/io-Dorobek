using io_Dorobek.Model;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        #region Constructors
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
            PublicationDates = new ObservableCollection<string>();
            Titles = new ObservableCollection<string>();
            KeyWords = new ObservableCollection<string>();
            IssnOfPaper = new ObservableCollection<string>();
            IsbnOfPaper = new ObservableCollection<string>();
            ArticleNames = new ObservableCollection<string>();
            Authors = new ObservableCollection<string>();
            Dois = new ObservableCollection<string>();
            InsertDefaultsToComboboxes();
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
            PublicationDates = new ObservableCollection<string>();
            PublicationYears = new ObservableCollection<string>();
            Titles = new ObservableCollection<string>();
            KeyWords = new ObservableCollection<string>();
            IssnOfPaper = new ObservableCollection<string>();
            IsbnOfPaper = new ObservableCollection<string>();
            ArticleNames = new ObservableCollection<string>();
            Authors = new ObservableCollection<string>();
            Dois = new ObservableCollection<string>();
            InsertDefaultsToComboboxes();
        }
        #endregion

        #region Methods for data detection from file and comboboxes update
        private void InsertDefaultsToComboboxes()
        {
            PublicationDates.Add("");
            PublicationYears.Add("");
            Titles.Add("");
            KeyWords.Add("");
            IssnOfPaper.Add("");
            IsbnOfPaper.Add("");
            ArticleNames.Add("");
            Authors.Add("");
            Dois.Add("");
        }

        private void ClearComboboxes()
        {
            PublicationDates.Clear();
            PublicationYears.Clear();
            Titles.Clear();
            KeyWords.Clear();
            IssnOfPaper.Clear();
            IsbnOfPaper.Clear();
            ArticleNames.Clear();
            Authors.Clear();
            Dois.Clear();
            InsertDefaultsToComboboxes();
        }

        private void FetchW1FieldsFromFile()
        {
            var fileInfo = FileContentParser.GetDocumentInfo(FsHandler.FileLoader(WybranaŚcieżka));
            ClearComboboxes();
            if (fileInfo.Titles.Count() != 0)
            {
                W1_Title = fileInfo.Titles[0];
                foreach (var x in fileInfo.Titles)
                {
                    if (x.Trim() != string.Empty)
                        Titles.Add(x);
                }
            }
            if (fileInfo.Authors.Count() != 0)
            {
                W1_Author = fileInfo.Authors[0];
                foreach (var x in fileInfo.Authors)
                {
                    if (x.Trim() != string.Empty)
                        Authors.Add(x);
                }
            }
            if (fileInfo.Years.Count() != 0)
            {
                W1_PublicationYear = fileInfo.Years[0].ToString();
                foreach (var x in fileInfo.Years)
                {
                    if (x.ToString().Trim() != string.Empty)
                        PublicationYears.Add(x.ToString());
                }
            }
            if (fileInfo.Keywords.Count() != 0)
            {
                W1_KeyWords = fileInfo.Keywords[0];
                foreach (var x in fileInfo.Keywords)
                {
                    if (x.Trim() != string.Empty)
                        KeyWords.Add(x);
                }
            }
            if (fileInfo.Doi.Count() != 0)
            {
                W1_DOI_VM = fileInfo.Doi[0];
                foreach (var x in fileInfo.Doi)
                {
                    if (x.Trim() != string.Empty)
                        Dois.Add(x);
                }
            }
            if (fileInfo.FullDate.Count() != 0)
            {
                W1_PublicationDate = fileInfo.FullDate[0];
                foreach (var x in fileInfo.FullDate)
                {
                    if (x.Trim() != string.Empty)
                        PublicationDates.Add(x);
                }
            }
        }
        #endregion

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
                                    FullDate = W1_PublicationDate.Trim(),
                                    ArticleName = W1_ArticleName.Trim(),
                                    Isbn = W1_IsbnOfPaper.Trim(),
                                    Issn = W1_IssnOfPaper.Trim(),
                                    KeyWords = W1_KeyWords.Trim(),
                                    Year = int.Parse(W1_PublicationYear.Trim())
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

        #region ComboBox Selection Commands

        private ICommand TitleSelection;
        public ICommand c_TitleSelection
        {
            get
            {
                return TitleSelection ?? (TitleSelection = new RelayCommand(
                    (p) =>
                    {
                        if(p != null)
                        {
                            W1_Title=p.ToString();
                        }
                    },
                    p => true)
                    );
            }
        }

        private ICommand AuthorSelection;
        public ICommand c_AuthorSelection
        {
            get
            {
                return AuthorSelection ?? (AuthorSelection = new RelayCommand(
                    (p) =>
                    {
                        if (p != null)
                        {
                            W1_Author = p.ToString();
                        }
                    },
                    p => true)
                    );
            }
        }

        private ICommand DateSelection;
        public ICommand c_DateSelection
        {
            get
            {
                return DateSelection ?? (DateSelection = new RelayCommand(
                    (p) =>
                    {
                        if (p != null)
                        {
                            W1_PublicationDate = p.ToString();
                        }
                    },
                    p => true)
                    );
            }
        }

        private ICommand YearSelection;
        public ICommand c_YearSelection
        {
            get
            {
                return YearSelection ?? (YearSelection = new RelayCommand(
                    (p) =>
                    {
                        if (p != null)
                        {
                            W1_PublicationYear = p.ToString();
                        }
                    },
                    p => true)
                    );
            }
        }

        private ICommand KeywordsSelection;
        public ICommand c_KeywordsSelection
        {
            get
            {
                return KeywordsSelection ?? (KeywordsSelection = new RelayCommand(
                    (p) =>
                    {
                        if (p != null)
                        {
                            W1_KeyWords = p.ToString();
                        }
                    },
                    p => true)
                    );
            }
        }

        private ICommand IssnOfPaperSelection;
        public ICommand c_IssnOfPaperSelection
        {
            get
            {
                return IssnOfPaperSelection ?? (IssnOfPaperSelection = new RelayCommand(
                    (p) =>
                    {
                        if (p != null)
                        {
                            W1_IssnOfPaper = p.ToString();
                        }
                    },
                    p => true)
                    );
            }
        }

        private ICommand IsbnOfPaperSelection;
        public ICommand c_IsbnOfPaperSelection
        {
            get
            {
                return IsbnOfPaperSelection ?? (IsbnOfPaperSelection = new RelayCommand(
                    (p) =>
                    {
                        if (p != null)
                        {
                            W1_IsbnOfPaper = p.ToString();
                        }
                    },
                    p => true)
                    );
            }
        }

        private ICommand ArticleNameSelection;
        public ICommand c_ArticleNameSelection
        {
            get
            {
                return ArticleNameSelection ?? (ArticleNameSelection = new RelayCommand(
                    (p) =>
                    {
                        if (p != null)
                        {
                            W1_ArticleName = p.ToString();
                        }
                    },
                    p => true)
                    );
            }
        }

        private ICommand DoiSelection;
        public ICommand c_DoiSelection
        {
            get
            {
                return DoiSelection ?? (DoiSelection = new RelayCommand(
                    (p) =>
                    {
                        if (p != null)
                        {
                            W1_DOI_VM = p.ToString();
                        }
                    },
                    p => true)
                    );
            }
        }
        #endregion

        #region ComboBox Properties
        private ObservableCollection<string> publicationDates;
        public ObservableCollection<string> PublicationDates
        {
            get { return publicationDates; }
            set
            {
                publicationDates = value;
            }
        }
        private ObservableCollection<string> publicationYears;
        public ObservableCollection<string> PublicationYears
        {
            get { return publicationYears; }
            set
            {
                publicationYears = value;
            }
        }
        private ObservableCollection<string> titles;
        public ObservableCollection<string> Titles
        {
            get { return titles; }
            set
            {
                titles = value;
            }
        }
        private ObservableCollection<string> keyWords;
        public ObservableCollection<string> KeyWords
        {
            get { return keyWords; }
            set
            {
                keyWords = value;
            }
        }
        private ObservableCollection<string> issnOfPaper;
        public ObservableCollection<string> IssnOfPaper
        {
            get { return issnOfPaper; }
            set
            {
                issnOfPaper = value;
            }
        }
        private ObservableCollection<string> isbnOfPaper;
        public ObservableCollection<string> IsbnOfPaper
        {
            get { return isbnOfPaper; }
            set
            {
                isbnOfPaper = value;
            }
        }
        private ObservableCollection<string> articleNames;
        public ObservableCollection<string> ArticleNames
        {
            get { return articleNames; }
            set
            {
                articleNames = value;
            }
        }
        private ObservableCollection<string> authors;
        public ObservableCollection<string> Authors
        {
            get { return authors; }
            set
            {
                authors = value;
            }
        }
        private ObservableCollection<string> dois;
        public ObservableCollection<string> Dois
        {
            get { return dois; }
            set
            {
                dois = value;
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

        private string w1_PublicationYear;
        public string W1_PublicationYear
        {
            get { return w1_PublicationYear; }
            set
            {
                w1_PublicationYear = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(W1_PublicationYear)));
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
