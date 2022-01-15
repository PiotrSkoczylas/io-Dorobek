using io_Dorobek.Model;
using io_Dorobek.View;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;

namespace io_Dorobek.ViewModel
{

    class MainViewModel : INotifyPropertyChanged
    {
        private ListHandler listHandler { get; set; }

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
        public MainViewModel()
        {
            listHandler = new ListHandler();
            Pozycje = listHandler.publications;
        }


        #region Commands

        private ICommand DeleteItems;
        public ICommand c_DeleteItems
        {
            get
            {
                return DeleteItems ?? (DeleteItems = new RelayCommand(
                    (p) =>
                    {
                        System.Collections.IList items = (System.Collections.IList)p;
                        var collection = items.Cast<PublicationListItem>().ToList();
                        listHandler.RemoveElements(
                            collection
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
                                System.Collections.IList items = (System.Collections.IList)p;
                                var collection = items.Cast<PublicationListItem>().Select(a => a.Id).ToList();
                                FsHandler.SavePdfFiles(
                                    collection,
                                    x.SelectedPath);
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
                        using (SaveFileDialog x = new SaveFileDialog())
                        {
                            x.Filter = "bibtex file (*.bib)|*.bib";
                            if (x.ShowDialog() == DialogResult.OK)
                            {
                                System.Collections.IList items = (System.Collections.IList)p;
                                var collection = items.Cast<PublicationListItem>().ToList();
                                FsHandler.SaveToBibtex(collection, x.FileName);
                            }
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
                        var viewModel = new AddPublicationViewModel(listHandler);
                        var window = new AddPublicationView { DataContext = viewModel };
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
                        System.Collections.IList items = (System.Collections.IList)p;
                        var collection = items.Cast<PublicationListItem>().ToList();
                        if (collection.Count == 1)
                        {
                            var viewModel = new EditPublicationViewModel(listHandler, collection.First());
                            var window = new EditPublicationView { DataContext = viewModel };
                            window.Show();
                        }
                    },
                    p => true)
                    );
            }
        }

        #endregion

        #region Properties

        private ObservableCollection<PublicationListItem> pozycje;
        public ObservableCollection<PublicationListItem> Pozycje
        {
            get { return pozycje; }
            private set
            {
                pozycje = value;
            }
        }

        private string searchBar;
        public string p_searchBar
        {
            get { return searchBar; }
            set
            {
                searchBar = value;
                listHandler.Filter(searchBar);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(p_searchBar)));
            }
        }

        #endregion

    }
}
