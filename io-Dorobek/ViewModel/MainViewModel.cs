using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace io_Dorobek.ViewModel
{
    class MainViewModel
    {
        private string[] pozycje = { "blablabla blabla 123456789", "blablabla blabla 123456789 ", "blablabla blabla 123456789 ", "blablabla blabla 123456789 ", "blablabla blabla 123456789 ", "blablabla blabla 123456789 ", "blablabla blabla 123456789 ", "blablabla blabla 123456789 ", "blablabla blabla 123456789 " };
        public string[] Pozycje
        {
            get { return pozycje; }
            private set
            {
                for (int i = 0; i < 9; i++)
                { pozycje[i] = value[i]; }
            }
        }



    }
}
