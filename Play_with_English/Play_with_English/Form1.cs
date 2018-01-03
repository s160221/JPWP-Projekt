using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Play_with_English
{
    public partial class Form1 : Form
    {
        private static bool ReOpen;     // zmienna przechowujaca informacje o ponownym uruchomieniu etapu nauki
        public static bool reOpen
        {
            get { return ReOpen; }
            set { ReOpen = value; }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var button1Form = new OwoceIWarzywa();
            this.Hide();                    // ukrycie glownej formy przed pokazaniem drugiej
            button1Form.ShowDialog();       // wyswietlenie drugiej formy i przerwanie realizacji kodu dla pierwszej

            while (reOpen == true)          // sprawdzenie, czy uruchomiono ponownie etap nauki
            {
                var reOpenForm = new OwoceIWarzywa();   // utworzenie nowej formy dla powtorzenia etapu nauki
                reOpenForm.ShowDialog();
            }
            
            this.Show();                    // powrot do pracy na glownej formie
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var button2Form = new Sport();
            this.Hide();                    // ukrycie glownej formy przed pokazaniem drugiej
            button2Form.ShowDialog();       // wyswietlenie drugiej formy i przerwanie realizacji kodu dla pierwszej

            while (reOpen == true)          // sprawdzenie, czy uruchomiono ponownie etap nauki
            {
                var reOpenForm = new Sport();   // utworzenie nowej formy dla powtorzenia etapu nauki
                reOpenForm.ShowDialog();
            }

            this.Show();                    // powrot do pracy na glownej formie
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var button3Form = new Rodzina();
            this.Hide();                    // ukrycie glownej formy przed pokazaniem drugiej
            button3Form.ShowDialog();       // wyswietlenie drugiej formy i przerwanie realizacji kodu dla pierwszej

            while (reOpen == true)          // sprawdzenie, czy uruchomiono ponownie etap nauki
            {
                var reOpenForm = new Rodzina();   // utworzenie nowej formy dla powtorzenia etapu nauki
                reOpenForm.ShowDialog();
            }

            this.Show();                    // powrot do pracy na glownej formie
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var button4Form = new Zwierzeta();
            this.Hide();                    // ukrycie glownej formy przed pokazaniem drugiej
            button4Form.ShowDialog();       // wyswietlenie drugiej formy i przerwanie realizacji kodu dla pierwszej

            while (reOpen == true)          // sprawdzenie, czy uruchomiono ponownie etap nauki
            {
                var reOpenForm = new Zwierzeta();   // utworzenie nowej formy dla powtorzenia etapu nauki
                reOpenForm.ShowDialog();
            }

            this.Show();                    // powrot do pracy na glownej formie
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var button5Form = new Ubrania();
            this.Hide();                    // ukrycie glownej formy przed pokazaniem drugiej
            button5Form.ShowDialog();       // wyswietlenie drugiej formy i przerwanie realizacji kodu dla pierwszej

            while (reOpen == true)          // sprawdzenie, czy uruchomiono ponownie etap nauki
            {
                var reOpenForm = new Ubrania();   // utworzenie nowej formy dla powtorzenia etapu nauki
                reOpenForm.ShowDialog();
            }

            this.Show();                    // powrot do pracy na glownej formie
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var button6Form = new Edukacja();
            this.Hide();                    // ukrycie glownej formy przed pokazaniem drugiej
            button6Form.ShowDialog();       // wyswietlenie drugiej formy i przerwanie realizacji kodu dla pierwszej

            while (reOpen == true)          // sprawdzenie, czy uruchomiono ponownie etap nauki
            {
                var reOpenForm = new Edukacja();   // utworzenie nowej formy dla powtorzenia etapu nauki
                reOpenForm.ShowDialog();
            }

            this.Show();                    // powrot do pracy na glownej formie
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var button7Form = new Zawody();
            this.Hide();                    // ukrycie glownej formy przed pokazaniem drugiej
            button7Form.ShowDialog();       // wyswietlenie drugiej formy i przerwanie realizacji kodu dla pierwszej

            while (reOpen == true)          // sprawdzenie, czy uruchomiono ponownie etap nauki
            {
                var reOpenForm = new Zawody();   // utworzenie nowej formy dla powtorzenia etapu nauki
                reOpenForm.ShowDialog();
            }

            this.Show();                    // powrot do pracy na glownej formie
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var button8Form = new Dom();
            this.Hide();                    // ukrycie glownej formy przed pokazaniem drugiej
            button8Form.ShowDialog();       // wyswietlenie drugiej formy i przerwanie realizacji kodu dla pierwszej

            while (reOpen == true)          // sprawdzenie, czy uruchomiono ponownie etap nauki
            {
                var reOpenForm = new Dom();   // utworzenie nowej formy dla powtorzenia etapu nauki
                reOpenForm.ShowDialog();
            }

            this.Show();                    // powrot do pracy na glownej formie
        }
    }
}
