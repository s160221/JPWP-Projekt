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
    public partial class Zawody : Form
    {
        // slownik ifnromujacy, czy obrazy zostaly rozmieszczone w docelowych miejscach
        protected Dictionary<TableLayoutPanel, bool> wykorzystanyObrazek = new Dictionary<TableLayoutPanel, bool>();

        protected int liczbaKlikniec = 0;   // licznik klinkniec dla metody otwierania Menu
        FlowLayoutPanel flp = new FlowLayoutPanel();    // utworzenie panelu dla Menu

        public Zawody()
        {
            InitializeComponent();

            Form1.reOpen = false;       // powrot do stanu pierwotnego - wymazanie statusu ponownego otwarcia okna

            Dictionary<string, Image> dictOiW = new Dictionary<string, Image>();

            string path = @"Zawody";
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

            foreach (KeyValuePair<string, Image> obraz in dictOiW)
            {
                lab[i].Text = obraz.Key;    // uzycie klucza jako etykiety obrazu (angielskie slowo)
                pb[i].Image = obraz.Value;  // uzycie obrazu w picturebox'ie

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
            string zawod = "";      // zmienna do przechowania nazwy zawod

            foreach (Control c in tlp.Controls)
            {
                if (c is Label)     // wyszukiwanie ukrytej nazwy obrazka
                {
                    zawod = c.Text;   // pobranie nazwy obrazka
                }
            }

            if (zawod != "Baker")
            {
                e.Effect = DragDropEffects.None;
                MessageBox.Show("To nie jest piekarz!");
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

        private void panel2_DragDrop(object sender, DragEventArgs e)
        {
            var tlp = (TableLayoutPanel)e.Data.GetData(typeof(TableLayoutPanel));   // pobranie danych przeciaganego panelu
            string zawod = "";      // zmienna do przechowania nazwy zawodu

            foreach (Control c in tlp.Controls)
            {
                if (c is Label)     // wyszukiwanie ukrytej nazwy obrazka
                {
                    zawod = c.Text;   // pobranie nazwy obrazka
                }
            }

            if (zawod != "Teacher")
            {
                e.Effect = DragDropEffects.None;
                MessageBox.Show("To nie jest nauczyciel!");
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

        private void panel3_DragDrop(object sender, DragEventArgs e)
        {
            var tlp = (TableLayoutPanel)e.Data.GetData(typeof(TableLayoutPanel));   // pobranie danych przeciaganego panelu
            string zawod = "";      // zmienna do przechowania nazwy zawodu

            foreach (Control c in tlp.Controls)
            {
                if (c is Label)     // wyszukiwanie ukrytej nazwy obrazka
                {
                    zawod = c.Text;   // pobranie nazwy obrazka
                }
            }

            if (zawod != "Doctor")
            {
                e.Effect = DragDropEffects.None;
                MessageBox.Show("To nie jest lekarz!");
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

        private void panel4_DragDrop(object sender, DragEventArgs e)
        {
            var tlp = (TableLayoutPanel)e.Data.GetData(typeof(TableLayoutPanel));   // pobranie danych przeciaganego panelu
            string zawod = "";      // zmienna do przechowania nazwy zawodu

            foreach (Control c in tlp.Controls)
            {
                if (c is Label)     // wyszukiwanie ukrytej nazwy obrazka
                {
                    zawod = c.Text;   // pobranie nazwy obrazka
                }
            }

            if (zawod != "Taxi driver")
            {
                e.Effect = DragDropEffects.None;
                MessageBox.Show("To nie jest taksówkarz!");
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

        private void panel5_DragDrop(object sender, DragEventArgs e)
        {
            var tlp = (TableLayoutPanel)e.Data.GetData(typeof(TableLayoutPanel));   // pobranie danych przeciaganego panelu
            string zawod = "";      // zmienna do przechowania nazwy zawodu

            foreach (Control c in tlp.Controls)
            {
                if (c is Label)     // wyszukiwanie ukrytej nazwy obrazka
                {
                    zawod = c.Text;   // pobranie nazwy obrazka
                }
            }

            if (zawod != "Soldier")
            {
                e.Effect = DragDropEffects.None;
                MessageBox.Show("To nie jest żołnierz!");
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
            string zawod = "";      // zmienna do przechowania nazwy zawodu

            foreach (Control c in tlp.Controls)
            {
                if (c is Label)     // wyszukiwanie ukrytej nazwy obrazka
                {
                    zawod = c.Text;   // pobranie nazwy obrazka
                }
            }

            if (zawod != "Mechanic")
            {
                e.Effect = DragDropEffects.None;
                MessageBox.Show("To nie jest mechanik!");
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

        private void panel7_DragDrop(object sender, DragEventArgs e)
        {
            var tlp = (TableLayoutPanel)e.Data.GetData(typeof(TableLayoutPanel));   // pobranie danych przeciaganego panelu
            string zawod = "";      // zmienna do przechowania nazwy zawodu

            foreach (Control c in tlp.Controls)
            {
                if (c is Label)     // wyszukiwanie ukrytej nazwy obrazka
                {
                    zawod = c.Text;   // pobranie nazwy obrazka
                }
            }

            if (zawod != "Shopkeeper")
            {
                e.Effect = DragDropEffects.None;
                MessageBox.Show("To nie jest sprzedawca!");
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

        private void panel8_DragDrop(object sender, DragEventArgs e)
        {
            var tlp = (TableLayoutPanel)e.Data.GetData(typeof(TableLayoutPanel));   // pobranie danych przeciaganego panelu
            string zawod = "";      // zmienna do przechowania nazwy zawodu

            foreach (Control c in tlp.Controls)
            {
                if (c is Label)     // wyszukiwanie ukrytej nazwy obrazka
                {
                    zawod = c.Text;   // pobranie nazwy obrazka
                }
            }

            if (zawod != "Postman")
            {
                e.Effect = DragDropEffects.None;
                MessageBox.Show("To nie jest listonosz!");
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

        private void panel9_DragDrop(object sender, DragEventArgs e)
        {
            var tlp = (TableLayoutPanel)e.Data.GetData(typeof(TableLayoutPanel));   // pobranie danych przeciaganego panelu
            string zawod = "";      // zmienna do przechowania nazwy zawodu

            foreach (Control c in tlp.Controls)
            {
                if (c is Label)     // wyszukiwanie ukrytej nazwy obrazka
                {
                    zawod = c.Text;   // pobranie nazwy obrazka
                }
            }

            if (zawod != "Fireman")
            {
                e.Effect = DragDropEffects.None;
                MessageBox.Show("To nie jest strażak!");
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

        private void panel10_DragDrop(object sender, DragEventArgs e)
        {
            var tlp = (TableLayoutPanel)e.Data.GetData(typeof(TableLayoutPanel));   // pobranie danych przeciaganego panelu
            string zawod = "";      // zmienna do przechowania nazwy zawodu

            foreach (Control c in tlp.Controls)
            {
                if (c is Label)     // wyszukiwanie ukrytej nazwy obrazka
                {
                    zawod = c.Text;   // pobranie nazwy obrazka
                }
            }

            if (zawod != "Policeman")
            {
                e.Effect = DragDropEffects.None;
                MessageBox.Show("To nie jest policjant!");
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
        private void test_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;    // pobranie informacji o przycisku
            Form inf = (Form)btn.Parent;    // pobranie informacji o formie bedacej rodzicem przycisku

            Test.kategoria = "Zawody";   // zawarcie informacji o kategorii, z ktorej bedzie przeprowadzony test

            var testForm = new Test();
            inf.Hide();                     // ukrycie dialogboxa
            this.Hide();                    // ukrycie formy etapu nauki
            testForm.ShowDialog();
            inf.Close();
            this.Close();
        }

        // Metoda realizowana po wyborze przycisku powtorzenia etapu nauki
        private void powtorz_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;    // pobranie informacji o przycisku
            Form inf = (Form)btn.Parent;    // pobranie informacji o formie bedacej rodzicem przycisku

            Form1.reOpen = true;            // informacja o ponownym otwarciu child form

            inf.Close();
            this.Close();
        }

        // Metoda odpowiedzialna za utworzenie menu i wprowadzenie przyciskow funkcyjnych
        private void Menu_Click(object sender, EventArgs e)
        {
            liczbaKlikniec++;

            flp.Size = new Size(Menu.Width - 5, 170);
            flp.Location = new Point(1263 - Menu.Width, Menu.Height + 2);
            flp.BorderStyle = BorderStyle.FixedSingle;

            Button wyniki = new Button();
            wyniki.Text = "Wyniki";
            wyniki.Location = new Point(0, 0);
            wyniki.Size = new Size(Menu.Width - 15, 30);
            wyniki.Click += new EventHandler(wyniki_Click);

            Button pomoc = new Button();
            pomoc.Text = "Pomoc";
            pomoc.Location = new Point(0, wyniki.Height);
            pomoc.Size = new Size(Menu.Width - 15, 30);
            pomoc.Click += new EventHandler(pomoc_Click);

            Button powrot = new Button();
            powrot.Text = "Powrót do okna startowego";
            powrot.Location = new Point(0, wyniki.Height * 2);
            powrot.Size = new Size(Menu.Width - 15, 50);
            powrot.Click += new EventHandler(powrot_Click);

            Button wyjscie = new Button();
            wyjscie.Text = "Wyjście";
            wyjscie.Location = new Point(0, wyniki.Height * 3 + 20);
            wyjscie.Size = new Size(Menu.Width - 15, 30);
            wyjscie.Click += new EventHandler(wyjscie_Click);

            flp.Controls.Add(wyniki);
            flp.Controls.Add(pomoc);
            flp.Controls.Add(powrot);
            flp.Controls.Add(wyjscie);
            this.Controls.Add(flp);

            if (liczbaKlikniec % 2 != 0)      // w przpyadku nieparzystego klikniecia ikony - menu jest wyswietlane
            {
                flp.Visible = true;
                flp.BringToFront();
            }
            else
            {
                flp.Visible = false;        // w przypadku parzystego klikniecia ikony - menu jest ukrywane
            }
        }

        // Metoda realizowana po wyborze przycisku wyswietlenia wynikow
        private void wyniki_Click(object sender, EventArgs e)
        {
            Plik.Odczyt();
        }

        // Metoda realizowana po wyborze przycisku wyswietlenia pomocy
        private void pomoc_Click(object sender, EventArgs e)
        {
            Form pom = new Form();
            pom.Size = new Size(600, 400);
            pom.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            pom.MaximizeBox = false;
            pom.MinimizeBox = false;
            pom.Text = "Play with English";
            pom.FormBorderStyle = FormBorderStyle.FixedDialog;
            pom.ControlBox = false;      // usuniecie przycisku zamykania okna

            Label tekst = new Label();
            tekst.Text = "Celem wybranego etapu jest rozmieszczenie obrazków poprzez przeciąganie tak, aby dopasować je do "
                + "zawodów (nazwy podane w etykietach), z któtymi są one związane. W przypadku błędnego umieszczenia "
                + "zostanie wyświetlony komunikat, a obrazek wróci na swoje pierwotne miejsce. Po poprawnym rozmieszczeniu wszystkich "
                + "obrazków użytkownik może przejść do testu sprawdzającego nabytą wiedzę lub powtórzyć etap nauki. Powodzenia!";
            tekst.Font = new Font("Arial", 14);
            tekst.TextAlign = ContentAlignment.MiddleCenter;
            tekst.Location = new Point(10, 10);
            tekst.Size = new Size(550, 265);

            Button ok = new Button();
            ok.Text = "Ok";
            ok.Location = new Point(240, 300);
            ok.Size = new Size(100, 30);
            ok.Click += new EventHandler(ok_Click);

            pom.Controls.Add(tekst);
            pom.Controls.Add(ok);
            pom.ShowDialog();
        }

        // Metoda realizowana po wyborze przycisku Ok w oknie dialogowym pomocy
        private void ok_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;    // pobranie informacji o przycisku
            Form par = (Form)btn.Parent;    // pobranie informacji o formie bedacej rodzicem przycisku (oknie pomocy)

            par.Close();    // zamkniecie okna pomocy
        }

        // Metoda realizowana po wyborze przycisku powrotu
        private void powrot_Click(object sender, EventArgs e)
        {
            this.Close();   // zamkniecie okna etapu nauki
        }

        // Metoda realizowana po wyborze przycisku wyjscie
        private void wyjscie_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);     // zamkniecie aplikacji
        }
    }
}