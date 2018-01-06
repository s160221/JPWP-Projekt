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
using System.Xml.Serialization;

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

        // sciezka do pliku z danymi
        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//Wyniki.xml";

        Wyniki wyniki = null;   // do przechowywania obiektow klasy Wynik

        protected int liczbaKlikniec = 0;   // licznik klinkniec dla metody otwierania Menu
        FlowLayoutPanel flp = new FlowLayoutPanel();    // utworzenie panelu dla Menu

        public Form1()
        {
            InitializeComponent();

            

            if (!File.Exists(path))     // sprawdzenie czy plik z wynikami zostal juz wczesniej utworzony
            {                           // jezeli nie, to jest on tworzony
                Wyniki wyniki = new Wyniki();
                wyniki.listaWynikow = new List<Wynik>();    // lista na obiekty klasy Wynik

                foreach (kategorie kat in Enum.GetValues(typeof(kategorie)))
                {   
                    // wprowadzenie informacji o kazdym tescie do listy
                    wyniki.listaWynikow.Add(new Wynik { kategoria = kat, wynikTestu = 0, odblokowane = false });
                }

                XmlSerializer serializer = new XmlSerializer(typeof(Wyniki));   // obiekt odpowiadajacy serializacje i deserializacje danych
                FileStream fs = File.Create(path);  // utworzenie pliku o zadanej sciezce
                serializer.Serialize(fs, wyniki);   // utworzenie drzewa xml z danymi obiektow klasy Wynik
                fs.Close();
            }
            else
            {
                Odczyt();              // metoda wczytujaca dane z pliku xml
                Odblokowywanie();      // metoda odblokowywujaca przyciski testow, ktore zostaly juz ukonczone
            }
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

            Odczyt();                       // metoda wczytujaca dane z pliku xml
            Odblokowywanie();               // metoda odblokowywujaca przyciski testow, ktore zostaly juz ukonczone
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

            Odczyt();              // metoda wczytujaca dane z pliku xml
            Odblokowywanie();      // metoda odblokowywujaca przyciski testow, ktore zostaly juz ukonczone
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

            Odczyt();              // metoda wczytujaca dane z pliku xml
            Odblokowywanie();      // metoda odblokowywujaca przyciski testow, ktore zostaly juz ukonczone
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

            Odczyt();              // metoda wczytujaca dane z pliku xml
            Odblokowywanie();      // metoda odblokowywujaca przyciski testow, ktore zostaly juz ukonczone
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

            Odczyt();              // metoda wczytujaca dane z pliku xml
            Odblokowywanie();      // metoda odblokowywujaca przyciski testow, ktore zostaly juz ukonczone
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

            Odczyt();              // metoda wczytujaca dane z pliku xml
            Odblokowywanie();      // metoda odblokowywujaca przyciski testow, ktore zostaly juz ukonczone
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

            Odczyt();              // metoda wczytujaca dane z pliku xml
            Odblokowywanie();      // metoda odblokowywujaca przyciski testow, ktore zostaly juz ukonczone
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

            Odczyt();              // metoda wczytujaca dane z pliku xml
            Odblokowywanie();      // metoda odblokowywujaca przyciski testow, ktore zostaly juz ukonczone
            this.Show();                    // powrot do pracy na glownej formie
        }

        private void Test_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;    // pobranie informacji o przycisku
            Test.kategoria = (string)btn.Tag;   // przypisanie odpowiedniej kategorii do testu, zaleznie od wybrangeo przycisku

            var testForm = new Test();
            this.Hide();                    // ukrycie glownej formy przed pokazaniem drugiej
            testForm.ShowDialog();          // wyswietlenie drugiej formy i przerwanie realizacji kodu dla pierwszej

            /*  while (reOpen == true)          // sprawdzenie, czy uruchomiono ponownie etap testu
              {
                  var reOpenForm = new Test();   // utworzenie nowej formy dla powtorzenia etapu testu
                  reOpenForm.ShowDialog();
              } */

            Odczyt();              // metoda wczytujaca dane z pliku xml
            Odblokowywanie();      // metoda odblokowywujaca przyciski testow, ktore zostaly juz ukonczone
            this.Show();                    // powrot do pracy na glownej formie
        }

        // Metoda do wczytywania pliku xml z danymi
        private void Odczyt()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Wyniki));   // obiekt odpowiadajacy serializacje i deserializacje danych
            StreamReader reader = new StreamReader(path);   // wczytanie z pliku o zadanej sciezce
            wyniki = (Wyniki)serializer.Deserialize(reader);    // pobranie danych z drzewa xml do listy wynikow
            reader.Close();
        }

        // Metoda odblokowywujaca przyciski testow, ktore zostaly juz ukonczone
        private void Odblokowywanie()
        {
            bool odblTestuGlownego = true;     // czy test glowny zostal odblokowany

            foreach (Wynik w in wyniki.listaWynikow)
            {
                if (w.kategoria != kategorie.TestGlowny)
                {
                    if (w.wynikTestu < 8)
                    {
                        odblTestuGlownego = false;  // jezeli ktorys z wynikow jest mniejszy od 8, to test glowny jest zablokowany
                    }
                }
                else
                {
                    if (odblTestuGlownego)
                    {
                        w.odblokowane = true;   // jezeli wszystkie wyniki byly na poziomie co najmniej 80%, to test glowny jest odblokowany
                    }
                }

                if (w.odblokowane)
                {

                    foreach (Control c in this.Controls.OfType<Button>())
                    {
                        if (w.kategoria.ToString() == (string)c.Tag)
                        {
                            c.Visible = true;
                            break;
                        }
                    }
                }
            }
        }

        // Metoda odpowiedzialna za utworzenie menu i wprowadzenie przyciskow funkcyjnych
        private void Menu_Click(object sender, EventArgs e)
        {
            liczbaKlikniec++;

            flp.Size = new Size(Menu.Width - 5, 110);
            flp.Location = new Point(1260 - Menu.Width, Menu.Height + 2);
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

            Button wyjscie = new Button();
            wyjscie.Text = "Wyjście";
            wyjscie.Location = new Point(0, wyniki.Height * 2);
            wyjscie.Size = new Size(Menu.Width - 15, 30);
            wyjscie.Click += new EventHandler(wyjscie_Click);

            flp.Controls.Add(wyniki);
            flp.Controls.Add(pomoc);
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
            tekst.Text = "Gra Play with English ma za zadanie nauczyć gracza podstawowych słów w języku angielskim z zakresu "
                + "przedstawionych na ekranie głównym kategorii. W każdej z kategorii na użytkownika czeka etap nauki, który kończy "
                + "się przeprowadzeniem testu. Po rozwiązaniu testu będzie można go rozwiązać w każdej chwili z pominięciem etapu nauki. "
                + "Rozwiązanie wszystkich testów z wynikiem na poziomie 80% lub większym odblokowuje test główny łączący wszystkie kategorie. "
                + "Udanej zabawy!";
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
            par.Dispose();
        }

        // Metoda realizowana po wyborze przycisku wyjscie
        private void wyjscie_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);     // zamkniecie aplikacji
        }
    }
}
