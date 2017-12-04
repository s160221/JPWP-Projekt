using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Play_with_English
{
    public partial class OwoceIWarzywa : Form
    {
        // slownik ifnromujacy, czy obrazy zostaly rozmieszczone w docelowych miejscach
        protected Dictionary<TableLayoutPanel, bool> wykorzystanyObrazek = new Dictionary<TableLayoutPanel, bool>();

        public OwoceIWarzywa()
        {
            InitializeComponent();

            Dictionary<string, Image> dictOiW = new Dictionary<string, Image>();

            //string path = "E:\\GitHub\\JPWP - Projekt\\Play_with_English\\Play_with_English\\OwoceIWarzywa";    // sciezka do katalogu z obrazami

            string path = @"OwoceIWarzywa";
            string fullPath = Path.GetFullPath(path);

            string[] zdjecia = Directory.GetFiles(fullPath);    // zapisanie sciezek do poszczegolnych obrazow
            string klucz;   // kontener na klucze slownika

            PictureBox[] pb =
            {
                pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9, pictureBox10
            };

            Label[] lab =
            {
                label1, label2, label3, label4, label5, label6, label7, label8, label9, label10
            };

            int i = 0;  // licznik tablic

            foreach (var nazwa in zdjecia)
            {
                Bitmap bmp = null;

                try
                {
                    bmp = new Bitmap(nazwa);    // utworzenie bitmapy na podstawie sciezki
                    klucz = nazwa.Split('.')[0];
                    klucz = klucz.Split('\\').Last();   // zebranie nazwy pliku jako klucza
                    klucz = char.ToUpper(klucz[0]) + klucz.Substring(1);    // zamiana pierwszej litery na wielka
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    continue;
                }

                dictOiW.Add(klucz, bmp);
            }

            Random losuj = new Random();
            dictOiW = dictOiW.OrderBy(x => losuj.Next()).ToDictionary(elem => elem.Key, elem => elem.Value);    // przetasowanie elementow w slowniku

            foreach (KeyValuePair<string,Image> obraz in dictOiW)
            {
                lab[i].Text = obraz.Key;    // uzycie klucza jako etykiety obrazu (angielskie slowo)
                pb[i].Image = obraz.Value;  // uzycie obrazu w picturebox'ie

                // okreslenie, czy dany obrazek to owoc czy warzywo
                if (lab[i].Text == "Apple" || lab[i].Text == "Banana" || lab[i].Text == "Cherry" || lab[i].Text == "Orange" || lab[i].Text == "Pineapple")
                {
                    lab[i].Parent.Tag = "Owoc";
                }
                else
                {
                    lab[i].Parent.Tag = "Warzywo";
                }

                wykorzystanyObrazek.Add((TableLayoutPanel)pb[i].Parent, false);     // dodanie informacji o tym, ze zaden obrazek nie zostal jeszcze rozmieszczony

                i++;
            }

        }

        // Metoda przemieszczania niewykorzystanych obrazkow - jednakowa dla wszystkich
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;     // obsluga zdarzenia dla PictureBoxa
            TableLayoutPanel tlp = (TableLayoutPanel)pb.Parent;
            tlp.Select();                           // przekierowanie obslugi na rodzica - TableLayoutPanel

            if (wykorzystanyObrazek[tlp] == false)
            {
                tlp.DoDragDrop(tlp, DragDropEffects.Move);  // ustawienie przemieszczania niewykorzystanego elementu dla realizacji Drag&Drop
            }
            else
            {
                tlp.DoDragDrop(tlp, DragDropEffects.None);  // wykorzystany element nie moze byc przemieszczany
            }
        }

        // metoda wywolana podczas przeciagania elementu nad docelowym obszarem - jednakowa dla wszystkich
        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TableLayoutPanel))) e.Effect = DragDropEffects.Move;   // ustawienie efektu dla przeciagania panelu w dozwolone miejsce
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)    // metoda wywolana po upuszczeniu przeciaganego elementu
        {
            var tlp = (TableLayoutPanel)e.Data.GetData(typeof(TableLayoutPanel));   // pobranie danych przeciaganego panelu

            if ((String)tlp.Tag == "Warzywo")
            {
                e.Effect = DragDropEffects.None;
                MessageBox.Show("To nie jest owoc!");
            }
            else
            {   
                tlp.Parent = (Panel)sender;     // ustawienie obszaru docelowego jako nowego rodzica przeciagnietego panelu
                tlp.Location = new Point((tlp.Parent.Width - tlp.Width) / 2, (tlp.Parent.Height - tlp.Height) / 2); // wysrodkowanie panelu w obszarze docelowym
                
                foreach (Control c in tlp.Controls)
                {
                    if (c is Label)     // wyszukiwanie ukrytej nazwy obrazka
                    {
                        c.Visible = true;   // wyswietlanie ukrytej nazwy po umieszczeniu obrazka w miejscu docelowym
                    }
                }

                tlp.Parent.AllowDrop = false;
                wykorzystanyObrazek[tlp] = true;    // zawarcie informacji o tym, ze obrazek zostal wykorzystany
            }
        }

        private void panel6_DragDrop(object sender, DragEventArgs e)
        {
            var tlp = (TableLayoutPanel)e.Data.GetData(typeof(TableLayoutPanel));   // pobranie danych przeciaganego panelu

            if ((String)tlp.Tag == "Owoc")
            {
                e.Effect = DragDropEffects.None;
                MessageBox.Show("To nie jest warzywo!");
            }
            else
            {
                tlp.Parent = (Panel)sender;     // ustawienie obszaru docelowego jako nowego rodzica przeciagnietego panelu
                tlp.Location = new Point((tlp.Parent.Width - tlp.Width) / 2, (tlp.Parent.Height - tlp.Height) / 2); // wysrodkowanie panelu w obszarze docelowym

                foreach (Control c in tlp.Controls)
                {
                    if (c is Label)     // wyszukiwanie ukrytej nazwy obrazka
                    {
                        c.Visible = true;   // wyswietlanie ukrytej nazwy po umieszczeniu obrazka w miejscu docelowym
                    }
                }

                tlp.Parent.AllowDrop = false;
                wykorzystanyObrazek[tlp] = true;    // zawarcie informacji o tym, ze obrazek zostal wykorzystany
            }
        }
    }
}
