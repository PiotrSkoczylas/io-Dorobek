﻿using io_Dorobek.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            W2_PublicationDate = item.Year.ToString();
            W2_Title = item.Title;
            W2_KeyWords = "";
            W2_IssnOfPaper = "";
            W2_IsbnOfPaper = "";
            W2_ArticleName = "";
            W2_Author = item.Author;
            W2_DOI_VM = item.Doi;
        }
        #endregion

        #region Commands

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
                            publicationListItem.Doi = W2_DOI_VM.Trim();
                            publicationListItem.Title = W2_Title.Trim();
                            publicationListItem.Author = W2_Author.Trim();
                            publicationListItem.Year = int.Parse(W2_PublicationDate.Trim());
                            listHandler.EditElement(publicationListItem);
                        }
                        catch (Exception ex)
                        {

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