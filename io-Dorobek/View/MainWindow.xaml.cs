
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//na potrzeby testu
using io_Dorobek.View;


namespace io_Dorobek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        //Na potrzeby testów
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            //Window window = new Window(Window1);
            Window1 x = new Window1();
            x.Show();
            this.Close();
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            Window2 x = new Window2();
            x.Show();
            this.Close();
        }
    }
}
