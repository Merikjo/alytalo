using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ÄlytaloWpfJM
{
    public class Sauna
    {
        public bool Switched { get; set; }
        public string Color { get; set; }

        public double SaunaTemperature { get; set; }

        public int Deg { get; set; }
        //public double S

        public void SaunaPäällä(int tila)
        {
            if (tila == 0)
            {
                Switched = false;

            }
            else
            {
                Switched = true;

            }
        }
    }
}
