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
        private static bool ReOpen;
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
    }
}
