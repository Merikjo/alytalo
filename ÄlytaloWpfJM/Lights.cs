using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ÄlytaloWpfJM
{
    public class Lights
    {
        //
        public bool Switched { get; set; }
        public string Dimmer { get; set; } //string = merkkijono

        public void SwitchOff() //metodi l. toiminto
            //mikä arvo on himmentimellä
        {
            Switched = false;
            Dimmer = "OFF";
        }
        public void SwitchOn(int DimmerValue) //metodi
        {
            if (DimmerValue != 0) //parametri ! = not l. kielto eli ei ole yhtäsuuri
            {
                Dimmer = DimmerValue.ToString();  //
            }
            else Dimmer = "ERROR";  //virhetilanne
        }
    }
}
