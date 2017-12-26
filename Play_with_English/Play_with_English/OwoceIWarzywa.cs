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

            Form1.reOpen = false;       // powrot do stanu pierwotnego - wymazanie statusu ponownego otwarcia okna

            Dictionary<string, Image> dictOiW = new Dictionary<string, Image>();

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

        // Metoda wywolana podczas przeciagania elementu nad docelowym obszarem - jednakowa dla wszystkich
        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TableLayoutPanel)))
            {
                e.Effect = DragDropEffects.Move;   // ustawienie efektu dla przeciagania panelu w dozwolone miejsce
            }
                
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
                EndLearningPart();                  // wyswietlenie dialogboxa (o ile wszystkie obrazki zostaly rozmieszczone)
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
                EndLearningPart();                  // wyswietlenie dialogboxa (o ile wszystkie obrazki zostaly rozmieszczone)
            }
        }

        // Metoda realizowana po rozmieszczeniu wszystkich obrazkow
        private void EndLearningPart()
        {
            bool rozmieszczone = true;

            foreach (KeyValuePair<TableLayoutPanel, bool> wykorzystany in wykorzystanyObrazek)
            {
                if (wykorzystany.Value == false)    // sprawdzanie, czy wszystkie obrazki sa rozmieszczone
                {
                    rozmieszczone = false;
                }
            }

            if (rozmieszczone)      // tworzenie okna dialogowego w przypadku rozmieszczenia wszystkich obrazkow
            {
                Form informacja = new Form();
                informacja.Size = new Size(600, 350);
                informacja.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                informacja.MaximizeBox = false;
                informacja.MinimizeBox = false;
                informacja.Text = "Play with English";
                informacja.FormBorderStyle = FormBorderStyle.FixedDialog;
                informacja.ControlBox = false;      // usuniecie przycisku zamykania okna

                Label tekst = new Label();
                tekst.Text = "Rozmieszczono poprawnie wszystkie obrazki! Chcesz przejść do testu czy powtórzyć etap nauki?";
                tekst.Font = new Font("Arial", 20);
                tekst.TextAlign = ContentAlignment.MiddleCenter;
                tekst.Location = new Point(10, 25);
                tekst.Size = new Size(550, 100);

                Button test = new Button();
                test.Text = "Przejdź do testu";
                test.Location = new Point(115, 200);
                test.Size = new Size(150, 75);
                test.Click += new EventHandler(test_Click);

                Button powtorz = new Button();
                powtorz.Text = "Powtórz";
                powtorz.Location = new Point(325, 200);
                powtorz.Size = new Size(150, 75);
                powtorz.Click += new EventHandler(powtorz_Click);

                informacja.Controls.Add(tekst);
                informacja.Controls.Add(test);
                informacja.Controls.Add(powtorz);
                informacja.ShowDialog();
            }
        }

        // Metoda realizowana po wyborze przycisku przejscia do testu
        private void test_Click (object sender, EventArgs e)
        {
            Button btn = (Button)sender;    // pobranie informacji o przycisku
            Form inf = (Form)btn.Parent;    // pobranie informacji o formie bedacej rodzicem przycisku

            var testForm = new Test();
            inf.Hide();                     // ukrycie dialogboxa
            this.Hide();                    // ukrycie formy etapu nauki
            testForm.ShowDialog();
            inf.Close();
            this.Close();
        }

        // Metoda realizowana po wyborze przycisku powtorzenia etapu nauki
        private void powtorz_Click (object sender, EventArgs e)
        {
            Button btn = (Button)sender;    // pobranie informacji o przycisku
            Form inf = (Form)btn.Parent;    // pobranie informacji o formie bedacej rodzicem przycisku

            Form1.reOpen = true;            // informacja o ponownym otwarciu child form

            inf.Close();
            this.Close();
        }
    }
}
