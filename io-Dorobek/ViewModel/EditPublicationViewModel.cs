using io_Dorobek.Model;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace io_Dorobek.ViewModel
{
    public class EditPublicationViewModel : INotifyPropertyChanged
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
        private PublicationListItem publicationListItem { get; set; }
        #region Constructors
        public EditPublicationViewModel()
        {
            listHandler = new ListHandler();
            W2_PublicationDate = "";
            W2_Title = "";
            W2_KeyWords = "";
            W2_IssnOfPaper = "";
            W2_IsbnOfPaper = "";
            W2_ArticleName = "";
            W2_Author = "";
            W2_DOI_VM = "";
        }
        public EditPublicationViewModel(ListHandler lst, PublicationListItem item)
        {
            listHandler = lst;
            publicationListItem = item;
            W2_PublicationYear = item.Year.ToString();
            W2_PublicationDate = item.FullDate;
            W2_Title = item.Title;
            W2_KeyWords = item.KeyWords;
            W2_IssnOfPaper = item.Issn;
            W2_IsbnOfPaper = item.Isbn;
            W2_ArticleName = item.ArticleName;
            W2_Author = item.Author;
            W2_DOI_VM = item.Doi;
        }
        #endregion

        #region Commands

        private ICommand PreviewPdf;
        public ICommand c_PreviewPdf
        {
            get
            {
                return PreviewPdf ?? (PreviewPdf = new RelayCommand(
                    (p) =>
                    {
                        string path = FsHandler.CreateForPreview(publicationListItem.Id);
                        Process.Start(path);
                    },
                    p => true)
                    );
            }
        }

        private ICommand Close;
        public ICommand c_Close
        {
            get
            {
                return Close ?? (Close = new RelayCommand(
                    (p) =>
                    {
                        ((Window)p).Close();
                    },
                    p => true)
                    );
            }
        }

        private ICommand EditItem;
        public ICommand c_EditItem
        {
            get
            {
                return EditItem ?? (EditItem = new RelayCommand(
                    (p) =>
                    {
                        try
                        {
                            if (W2_DOI_VM != null)
                                publicationListItem.Doi = W2_DOI_VM.Trim();
                            if (W2_Title != null)
                                publicationListItem.Title = W2_Title.Trim();
                            if (W2_Author != null)
                                publicationListItem.Author = W2_Author.Trim();
                            if (W2_PublicationYear != null)
                                publicationListItem.Year = int.Parse(W2_PublicationYear.Trim());
                            if (W2_PublicationDate != null)
                                publicationListItem.FullDate = W2_PublicationDate.Trim();
                            if (W2_KeyWords != null)
                                publicationListItem.KeyWords = W2_KeyWords.Trim();
                            if (W2_IsbnOfPaper != null)
                                publicationListItem.Isbn = W2_IsbnOfPaper.Trim();
                            if (W2_IssnOfPaper != null)
                                publicationListItem.Issn = W2_IssnOfPaper.Trim();
                            if (W2_ArticleName != null)
                                publicationListItem.ArticleName = W2_ArticleName.Trim();
                            listHandler.EditElement(publicationListItem);
                            ((Window)p).Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    },
                    p => true)
                    );
            }
        }

        #endregion

        #region Properties

        private string w2_Title;
        public string W2_Title
        {
            get { return w2_Title; }
            set
            {
                w2_Title = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(W2_Title)));
            }
        }

        private string w2_Author;
        public string W2_Author
        {
            get { return w2_Author; }
            set
            {
                w2_Author = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(W2_Author)));
            }
        }

        private string w2_PublicationDate;
        public string W2_PublicationDate
        {
            get { return w2_PublicationDate; }
            set
            {
                w2_PublicationDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(W2_PublicationDate)));
            }
        }

        private string w2_PublicationYear;
        public string W2_PublicationYear
        {
            get { return w2_PublicationYear; }
            set
            {
                w2_PublicationYear = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(W2_PublicationYear)));
            }
        }

        private string w2_DOI_VM;
        public string W2_DOI_VM
        {
            get { return w2_DOI_VM; }
            set
            {
                w2_DOI_VM = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(W2_DOI_VM)));
            }
        }

        private string w2_IsbnOfPaper;
        public string W2_IsbnOfPaper
        {
            get { return w2_IsbnOfPaper; }
            set
            {
                w2_IsbnOfPaper = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(W2_IsbnOfPaper)));
            }
        }

        private string w2_IssnOfPaper;
        public string W2_IssnOfPaper
        {
            get { return w2_IssnOfPaper; }
            set
            {
                w2_IssnOfPaper = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(W2_IssnOfPaper)));
            }
        }

        private string w2_ArticleName;
        public string W2_ArticleName
        {
            get { return w2_ArticleName; }
            set
            {
                w2_ArticleName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(W2_ArticleName)));
            }
        }

        private string w2_KeyWords;
        public string W2_KeyWords
        {
            get { return w2_KeyWords; }
            set
            {
                w2_KeyWords = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(W2_KeyWords)));
            }
        }
        #endregion
    }
}
