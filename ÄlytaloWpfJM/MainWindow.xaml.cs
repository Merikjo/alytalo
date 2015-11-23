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
        public Timer t = new Timer();

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

            //timer interval
            t.Interval = 1000;

            t.Tick += new EventHandler(this.t_Tick);

            //start timer when form loads
            t.Start(); //this will use t_Tick() method

        }
        // timer eventhandler
        private void t_Tick(object sender, EventArgs e)
        {
            //get current time
            int hh = DateTime.Now.Hour;
            int mm = DateTime.Now.Minute;
            int ss = DateTime.Now.Second;

            //time
            string time = "";

            //padding leading zero
            if (hh < 10)
            {
                time += "0" + hh;
            }
            else
            {
                time += hh;
            }
            time += ":";

            if (mm < 10)
            {
                time += "0" + mm;
            }
            else
            {
                time += mm;
            }
            time += ":";

            if (ss < 10)
            {
                time += "0" + ss;
            }
            else
            {
                time += ss;
            }

            //update txtTime
            txtTime.Text = time;
        }



        //toiminnallisuuksien asettaminen
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

        private void btnSaunaTila_Click(object sender, RoutedEventArgs e)
        {
            if (HouseSauna.Switched)
            {
                HouseSauna.SaunaPäällä(0);
                txtSaunaTila.Text = "SAUNA HEAT OFF";
                //Sauna.Timer.Stop();
                //lblLampotila
                txtSaunaTila.Background = Brushes.Silver;
                txtSaunaTila.Foreground = Brushes.BlueViolet;
                speechSynthesizer.Speak(txtSaunaTila.Text);
            }
            else
            {
                HouseSauna.SaunaPäällä(1);
                txtSaunaTila.Text = "SAUNA HEAT ON";
                txtSaunaTila.Background = Brushes.MistyRose;
                txtSaunaTila.Foreground = Brushes.Red;
                speechSynthesizer.Speak(txtSaunaTila.Text);
            }
        }

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
        private void btnTyhjennäTeksti_Click(object sender, RoutedEventArgs e)
            {
                txtTavoitelämpötila.Clear();
            }

        private void txtAika_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtAika.Text = date1.ToString();
        }

        private void t_Tick(object sender, ContextMenuEventArgs e)
        {

        }
    }
}

      

  





        
    

