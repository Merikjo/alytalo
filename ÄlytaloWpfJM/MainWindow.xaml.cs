using System;
using System.Speech.Synthesis;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Threading;

namespace ÄlytaloWpfJM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Lights OloHuone = new Lights(); //olio l. instanssi
        public Lights Keittiö = new Lights(); //olio l. instanssi
        public Sauna HouseSauna = new Sauna();
        public Thermostat HouseHeat = new Thermostat();
        public char Astemerkki;
        public SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
        public DateTime date1 = new DateTime(2015, 11, 15, 17, 12, 0);
        //public DispatcherTimer SaunaTimer = new System.Windows.Threading.

        private int j = 17;

        public DispatcherTimer asteTimer = new DispatcherTimer(); // Tick 
        public DispatcherTimer asteTimer2 = new DispatcherTimer();
        public DispatcherTimer asteTimer3 = new DispatcherTimer();

        public DispatcherTimer SaunaTimer = new DispatcherTimer();
        public DispatcherTimer SaunaSammutusTimer = new DispatcherTimer();

        DispatcherTimer dtClockTime = new DispatcherTimer();



        public MediaElement MediaElement = new MediaElement();

        public MainWindow()
        {
            InitializeComponent();

            OloHuone.Dimmer = "0";
            OloHuone.Switched = false;
            txtOlohuoneValo.Text = "OFF";

            Keittiö.Dimmer = "0";
            Keittiö.Switched = false;
            txtKeittiöValo.Text = "OFF";

            HouseSauna.SaunaPäällä(0);
            txtSaunaTila.Text = "";


            Astemerkki = Convert.ToChar(176);
            txtLämpötila.Text = "20" + Astemerkki;
            txtTavoitelämpötila.Text = "";

            #region saunan lämpötila tikit
            asteTimer.Tick += Lämpenee_Tick;
            asteTimer.Interval = new TimeSpan(0, 0, 1); //Tikki käy sekunnin välein

            asteTimer2.Tick += Lämpenee2_Tick;
            asteTimer2.Interval = new TimeSpan(0, 0, 1);

            asteTimer3.Tick += Lämpenee3_Tick;
            asteTimer.Interval = new TimeSpan(0, 0, 1);
            #endregion

            // Saunan Ajastin
            SaunaTimer.Tick += SaunanLampo_Tick;
            SaunaTimer.Interval = new TimeSpan(0, 0, 1);

            // Saunan ajastin sammutus
            SaunaSammutusTimer.Tick += SaunanSammutus_Tick;
            SaunaSammutusTimer.Interval = new TimeSpan(0, 0, 1);

            //Kellon asetus
            dtClockTime.Interval = new TimeSpan(0, 0, 1); //in Hour, Minutes, Second.
            dtClockTime.Tick += dtClockTime_Tick;

            dtClockTime.Start();

            //Video
            myMedia.Volume = 100;
            myMedia.Play();


        }
        /*void mediaPlay (object sender, EventArgs e)
        {
            saaTanaan.Play();
        }*/
        // timer eventhandler


        //

        //toiminnallisuuksien asettaminen

        #region Lights

        private void btnOlohuonePois_Click(object sender, RoutedEventArgs e)
        {
            OloHuone.SwitchOff();
            txtOlohuoneValo.Text = OloHuone.Dimmer;
            speechSynthesizer.Speak("LIVING ROOM " + txtOlohuoneValo.Text);
        }

        private void btnOlohuoneHämärä_Click(object sender, RoutedEventArgs e)
        {
            OloHuone.SwitchOn(33);
            txtOlohuoneValo.Text = "HÄMÄRÄ " + OloHuone.Dimmer + " %";
            txtOlohuoneValo.Background = Brushes.Lavender;
            speechSynthesizer.Speak("LIVING ROOM " + txtOlohuoneValo.Text);
        }

        private void btnOlohuonePuolivalo_Click(object sender, RoutedEventArgs e)
        {
            OloHuone.SwitchOn(66);
            txtOlohuoneValo.Text = "PUOLIVALO " + OloHuone.Dimmer + " %";
            txtOlohuoneValo.Background = Brushes.LightBlue;
            speechSynthesizer.Speak("LIVING ROOM " + txtOlohuoneValo.Text);
        }

        private void btnOlohuoneKirkas_Click(object sender, RoutedEventArgs e)
        {
            OloHuone.SwitchOn(100);
            txtOlohuoneValo.Text = "KIRKAS " + OloHuone.Dimmer + " %";
            txtOlohuoneValo.Background = Brushes.Azure;
            speechSynthesizer.Speak("LIVING ROOM " + txtOlohuoneValo.Text);

        }

        private void btnKeittiöPois_Click(object sender, RoutedEventArgs e)
        {
            Keittiö.SwitchOff();
            txtKeittiöValo.Text = Keittiö.Dimmer;
            speechSynthesizer.Speak("KITCHEN " + txtKeittiöValo.Text);
        }

        private void btnKeittiöHämärä_Click(object sender, RoutedEventArgs e)
        {
            Keittiö.SwitchOn(33);
            txtKeittiöValo.Text = "HÄMÄRÄ " + Keittiö.Dimmer + " %";
            txtKeittiöValo.Background = Brushes.Lavender;
            speechSynthesizer.Speak("KITCHEN " + txtKeittiöValo.Text);
        }

        private void btnKeittiöPuolivalo_Click(object sender, RoutedEventArgs e)
        {
            Keittiö.SwitchOn(66);
            txtKeittiöValo.Text = "PUOLIVALO " + Keittiö.Dimmer + " %";
            txtKeittiöValo.Background = Brushes.LightBlue;
            speechSynthesizer.Speak("KITCHEN " + txtKeittiöValo.Text);
        }

        private void btnKeittiöKirkas_Click(object sender, RoutedEventArgs e)
        {
            Keittiö.SwitchOn(100);
            txtKeittiöValo.Text = "KIRKAS " + Keittiö.Dimmer + " %";
            txtKeittiöValo.Background = Brushes.Azure;
            speechSynthesizer.Speak("KITCHEN " + txtKeittiöValo.Text);
        }
        #endregion

        #region Sauna
        private void btnSaunaTila_Click(object sender, RoutedEventArgs e)
        {
            if (HouseSauna.Switched)
            {
                HouseSauna.SaunaPäällä(0);
                SaunaTimer.Stop();
                SaunaSammutusTimer.Start();
                txtSaunaTila.Text = "SAUNA HEAT OFF";
                //lblLampotila
                txtSaunaTila.Background = Brushes.Silver;
                txtSaunaTila.Foreground = Brushes.BlueViolet;
                speechSynthesizer.Speak(txtSaunaTila.Text);
            }
            else
            {
                HouseSauna.SaunaPäällä(1);
                SaunaTimer.Start();
                txtSaunaTila.Text = "SAUNA HEAT ON";
                txtSaunaTila.Background = Brushes.MistyRose;
                txtSaunaTila.Foreground = Brushes.Red;
                speechSynthesizer.Speak(txtSaunaTila.Text);
            }
        }

        private void Lämpenee3_Tick(object sender, EventArgs e)
        {
            HouseSauna.Deg = HouseSauna.Deg + 1;
            Thread.Sleep(1000);
            lblSaunaHeat.Content = HouseSauna.Deg + Astemerkki;

            if (HouseSauna.Deg > 99)
            {
                txtSaunaTila.AppendText("SAUNA LÄMMIN");
                asteTimer3.Stop();
            }
        }
        private void Lämpenee2_Tick(object sender, EventArgs e)
        {
            HouseSauna.Deg = HouseSauna.Deg + 1;
            Thread.Sleep(1000);
            lblSaunaHeat.Content = HouseSauna.Deg + Astemerkki;

            if (HouseSauna.Deg > 79)
            {
                txtSaunaTila.Text = String.Empty;
                txtSaunaTila.AppendText("SAUNA LÄMMIN");
                asteTimer2.Stop();
            }
        }
        private void Lämpenee_Tick(object sender, EventArgs e)
        {
            //Kello on määritetty tikkaamaan sekunnin välein, joka on muunnettu sleepillä, että tikki kestää 2.0sek. Myös lämpötila celsius ilmoitettu.
            HouseSauna.Deg = HouseSauna.Deg + 1;
            Thread.Sleep(1000);
            lblSaunaHeat.Content = HouseSauna.Deg + Astemerkki;

            //Määrittää saunan lämpötilan pysähtymisen
            if (HouseSauna.Deg > 59)
            {
                txtSaunaTila.Text = String.Empty;
                txtSaunaTila.AppendText("SAUNA LÄMMIN");
                asteTimer.Stop();
            }
        }

        private void SaunanLampo_Tick(object sender, EventArgs e)
        {
            if (HouseSauna.SaunaTemperature > 60)
            {
                SaunaTimer.Stop();
            }
            HouseSauna.SaunaTemperature = HouseSauna.SaunaTemperature + 1.00;
            lblSaunaHeat.Content = HouseSauna.SaunaTemperature.ToString() + Astemerkki;
        }

        private void SaunanSammutus_Tick(object sender, EventArgs e)
        {
            if (HouseSauna.SaunaTemperature < 20)
            {
                SaunaSammutusTimer.Stop();
            }
            HouseSauna.SaunaTemperature = HouseSauna.SaunaTemperature - 0.50;
            lblSaunaHeat.Content = HouseSauna.SaunaTemperature.ToString() + Astemerkki;
        }

        #endregion

        #region Thermostat
        private void btnAsetaLämpötila_Click(object sender, RoutedEventArgs e)
        {
            int Tavoitelämpötila;
            try
            {
                Tavoitelämpötila = Int32.Parse(txtTavoitelämpötila.Text); //parse = merkkijonon muuttaminen kokonaisluvuksi

                if ((Tavoitelämpötila >= 15) && (Tavoitelämpötila < 40))

                    HouseHeat.SetTemp(Tavoitelämpötila);
                txtLämpötila.Text = HouseHeat.Temperature.ToString() + Astemerkki;
                txtTavoitelämpötila.Text = "";
                speechSynthesizer.Speak("NEW TEMPERATURE " + txtLämpötila.Text);

            }
            catch
            {
                txtTavoitelämpötila.Text = "ERROR " + "USE NUMBER VALUE";
                speechSynthesizer.Speak(txtTavoitelämpötila.Text);
            }
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            j--;
            string s = j.ToString();
            txtTavoitelämpötila.Text = (s);
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {


            j++;
            string s = j.ToString();
            txtTavoitelämpötila.Text = (s);

        }


        #endregion

        private void btnTyhjennäTeksti_Click(object sender, RoutedEventArgs e)
        {
            txtTavoitelämpötila.Clear();
        }




        private void dtClockTime_Tick(object sender, EventArgs e)
        {
            lblClockTime.Content = DateTime.Now.ToLongTimeString();
        }

        #region Video
        void mediaPlay(Object sender, EventArgs e)
        {
            myMedia.Play();
        }

        void mediaPause(Object sender, EventArgs e)
        {
            myMedia.Pause();
        }

        void mediaMute(Object sender, EventArgs e)
        {

            if (myMedia.Volume == 100)
            {
                myMedia.Volume = 0;
                muteButt.Content = "Listen";
            }
            else
            {
                myMedia.Volume = 100;
                muteButt.Content = "Mute";
            }
        }

        #endregion
    }
}













