namespace ÄlytaloWpfJM
{
    public class Thermostat
    {
        public bool Switched { get; set; }
       

        public int Temperature { get; set; }

        public void SetTemp(int tila)
        {
            if (tila == 0)
            {
                Switched = false;
                Temperature = 0;
            }
            else
            {
                Switched = true;
                Temperature = tila;
            }
        }
    }
}