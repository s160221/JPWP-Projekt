using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace Play_with_English
{
    public class Plik
    {
        public static void Odczyt()
        {
            Wyniki wyniki = null;   // do przechowywania obiektow klasy Wynik

            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//Wyniki.xml"; // sciezka do pliku z danymi
            XmlSerializer serializer = new XmlSerializer(typeof(Wyniki));   // obiekt odpowiadajacy serializacje i deserializacje danych
            StreamReader reader = new StreamReader(path);   // wczytanie z pliku o zadanej sciezce

            wyniki = (Wyniki)serializer.Deserialize(reader);    // pobranie danych z drzewa xml do listy wynikow
            reader.Close();

            Form wyn = new Form();
            wyn.Size = new Size(600, 500);
            wyn.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            wyn.MaximizeBox = false;
            wyn.MinimizeBox = false;
            wyn.Text = "Play with English";
            wyn.FormBorderStyle = FormBorderStyle.FixedDialog;
            wyn.ControlBox = false;      // usuniecie przycisku zamykania okna

            Label tekst = new Label();

            foreach (Wynik w in wyniki.listaWynikow)
            {
                if (w.kategoria.ToString() == "OwoceIWarzywa")
                {
                    tekst.Text += "Wynik z kategorii Owoce i Warzywa wynosi: " + w.wynikTestu + " punktów (" + (w.wynikTestu * 10) + "%). \n";
                }
                if (w.kategoria.ToString() == "TestGlowny")
                {
                    tekst.Text += "Wynik z Testu Głównego wynosi: " + w.wynikTestu + " punktów (" + (w.wynikTestu * 10) + "%). \n";
                }
                if (w.kategoria.ToString() != "OwoceIWarzywa" && w.kategoria.ToString() != "TestGlowny")
                {
                    tekst.Text += "Wynik z kategorii " + w.kategoria.ToString() + " wynosi: " + w.wynikTestu + " punktów (" + (w.wynikTestu * 10) + "%). \n";
                }

            }

            tekst.Font = new Font("Arial", 14);
            tekst.TextAlign = ContentAlignment.MiddleCenter;
            tekst.Location = new Point(10, 10);
            tekst.Size = new Size(550, 365);

            Button ok = new Button();
            ok.Text = "Ok";
            ok.Location = new Point(240, 400);
            ok.Size = new Size(100, 30);
            ok.Click += new EventHandler(ok_Click);

            wyn.Controls.Add(tekst);
            wyn.Controls.Add(ok);
            wyn.ShowDialog();
        }

        // Metoda realizowana po wyborze przycisku Ok w oknie dialogowym pomocy
        public static void ok_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;    // pobranie informacji o przycisku
            Form par = (Form)btn.Parent;    // pobranie informacji o formie bedacej rodzicem przycisku (oknie pomocy)

            par.Close();    // zamkniecie okna pomocy
        }
    }
}
