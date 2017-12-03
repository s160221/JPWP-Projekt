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

                i++;
            }

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;     // obsluga zdarzenia dla PictureBoxa
            TableLayoutPanel tlp = (TableLayoutPanel)pb.Parent;
            tlp.Select();                           // przekierowanie obslugi na rodzica - TableLayoutPanel
            tlp.DoDragDrop(tlp, DragDropEffects.Move);  // ustawienie przemieszczania elementu dla realizacji Drag&Drop
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            TableLayoutPanel tlp = (TableLayoutPanel)pb.Parent;
            tlp.Select();
            tlp.DoDragDrop(tlp, DragDropEffects.Move);
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            TableLayoutPanel tlp = (TableLayoutPanel)pb.Parent;
            tlp.Select();
            tlp.DoDragDrop(tlp, DragDropEffects.Move);
        }

        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            TableLayoutPanel tlp = (TableLayoutPanel)pb.Parent;
            tlp.Select();
            tlp.DoDragDrop(tlp, DragDropEffects.Move);
        }

        private void pictureBox5_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            TableLayoutPanel tlp = (TableLayoutPanel)pb.Parent;
            tlp.Select();
            tlp.DoDragDrop(tlp, DragDropEffects.Move);
        }

        private void pictureBox6_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            TableLayoutPanel tlp = (TableLayoutPanel)pb.Parent;
            tlp.Select();
            tlp.DoDragDrop(tlp, DragDropEffects.Move);
        }

        private void pictureBox7_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            TableLayoutPanel tlp = (TableLayoutPanel)pb.Parent;
            tlp.Select();
            tlp.DoDragDrop(tlp, DragDropEffects.Move);
        }

        private void pictureBox8_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            TableLayoutPanel tlp = (TableLayoutPanel)pb.Parent;
            tlp.Select();
            tlp.DoDragDrop(tlp, DragDropEffects.Move);
        }

        private void pictureBox9_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            TableLayoutPanel tlp = (TableLayoutPanel)pb.Parent;
            tlp.Select();
            tlp.DoDragDrop(tlp, DragDropEffects.Move);
        }

        private void pictureBox10_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            TableLayoutPanel tlp = (TableLayoutPanel)pb.Parent;
            tlp.Select();
            tlp.DoDragDrop(tlp, DragDropEffects.Move);
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)   // metoda wywolana podczas przeciagania elementu nad docelowym obszarem
        {
            if (e.Data.GetDataPresent(typeof(TableLayoutPanel))) e.Effect = DragDropEffects.Move;   // ustawienie efektu dla przeciagania panelu w dozwolone miejsce
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)    // metoda wywolana po upuszczeniu przeciaganego elementu
        {
            var tlp = (TableLayoutPanel)e.Data.GetData(typeof(TableLayoutPanel));   // pobranie danych przeciaganego panelu
            string typ = "";

            foreach (Control c in tlp.Controls)
            {
                if (c is Label)     // wyszukiwanie ukrytej nazwy obrazka
                {
                    typ = c.Text;   // przypisanie ukrytej nazwy do zmiennej
                }
            }

            if (typ != "Apple" && typ != "Banana" && typ != "Cherry" && typ != "Orange" && typ != "Pineapple")
            {
                e.Effect = DragDropEffects.None;
                MessageBox.Show("To nie jest owoc!");
            }
            else
            {
                //var tlp = (TableLayoutPanel)e.Data.GetData(typeof(TableLayoutPanel));   // pobranie danych przeciaganego panelu
                tlp.Location = new Point((panel1.Width - tlp.Width) / 2, (panel1.Height - tlp.Height) / 2); // wysrodkowanie panelu w obszarze docelowym
                tlp.Parent = panel1;    // ustawienie obszaru docelowego jako nowego rodzica przeciagnietego panelu
                tlp.BringToFront();

                foreach (Control c in tlp.Controls)
                {
                    if (c is Label)     // wyszukiwanie ukrytej nazwy obrazka
                    {
                        c.Visible = true;   // wyswietlanie ukrytej nazwy po umieszczeniu obrazka w miejscu docelowym
                    }
                }
            }
        }

        private void panel2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TableLayoutPanel))) e.Effect = DragDropEffects.Move;
        }

        private void panel2_DragDrop(object sender, DragEventArgs e)
        {
            var tlp = (TableLayoutPanel)e.Data.GetData(typeof(TableLayoutPanel));   // pobranie danych przeciaganego panelu
            tlp.Location = new Point((panel2.Width - tlp.Width) / 2, (panel2.Height - tlp.Height) / 2); // wysrodkowanie panelu w obszarze docelowym
            tlp.Parent = panel2;    // ustawienie obszaru docelowego jako nowego rodzica przeciagnietego panelu
            tlp.BringToFront();

            foreach (Control c in tlp.Controls)
            {
                if (c is Label)     // wyszukiwanie ukrytej nazwy obrazka
                {
                    c.Visible = true;   // wyswietlanie ukrytej nazwy po umieszczeniu obrazka w miejscu docelowym
                }
            }
        }

        private void panel3_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TableLayoutPanel))) e.Effect = DragDropEffects.Move;
        }

        private void panel3_DragDrop(object sender, DragEventArgs e)
        {
            var tlp = (TableLayoutPanel)e.Data.GetData(typeof(TableLayoutPanel));   // pobranie danych przeciaganego panelu
            tlp.Location = new Point((panel3.Width - tlp.Width) / 2, (panel3.Height - tlp.Height) / 2); // wysrodkowanie panelu w obszarze docelowym
            tlp.Parent = panel3;    // ustawienie obszaru docelowego jako nowego rodzica przeciagnietego panelu
            tlp.BringToFront();

            foreach (Control c in tlp.Controls)
            {
                if (c is Label)     // wyszukiwanie ukrytej nazwy obrazka
                {
                    c.Visible = true;   // wyswietlanie ukrytej nazwy po umieszczeniu obrazka w miejscu docelowym
                }
            }
        }

        private void panel4_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TableLayoutPanel))) e.Effect = DragDropEffects.Move;
        }

        private void panel4_DragDrop(object sender, DragEventArgs e)
        {
            var tlp = (TableLayoutPanel)e.Data.GetData(typeof(TableLayoutPanel));   // pobranie danych przeciaganego panelu
            tlp.Location = new Point((panel4.Width - tlp.Width) / 2, (panel4.Height - tlp.Height) / 2); // wysrodkowanie panelu w obszarze docelowym
            tlp.Parent = panel4;    // ustawienie obszaru docelowego jako nowego rodzica przeciagnietego panelu
            tlp.BringToFront();

            foreach (Control c in tlp.Controls)
            {
                if (c is Label)     // wyszukiwanie ukrytej nazwy obrazka
                {
                    c.Visible = true;   // wyswietlanie ukrytej nazwy po umieszczeniu obrazka w miejscu docelowym
                }
            }
        }

        private void panel5_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TableLayoutPanel))) e.Effect = DragDropEffects.Move;
        }

        private void panel5_DragDrop(object sender, DragEventArgs e)
        {
            var tlp = (TableLayoutPanel)e.Data.GetData(typeof(TableLayoutPanel));   // pobranie danych przeciaganego panelu
            tlp.Location = new Point((panel5.Width - tlp.Width) / 2, (panel5.Height - tlp.Height) / 2); // wysrodkowanie panelu w obszarze docelowym
            tlp.Parent = panel5;    // ustawienie obszaru docelowego jako nowego rodzica przeciagnietego panelu
            tlp.BringToFront();

            foreach (Control c in tlp.Controls)
            {
                if (c is Label)     // wyszukiwanie ukrytej nazwy obrazka
                {
                    c.Visible = true;   // wyswietlanie ukrytej nazwy po umieszczeniu obrazka w miejscu docelowym
                }
            }
        }

        private void panel6_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TableLayoutPanel))) e.Effect = DragDropEffects.Move;
        }

        private void panel6_DragDrop(object sender, DragEventArgs e)
        {
            var tlp = (TableLayoutPanel)e.Data.GetData(typeof(TableLayoutPanel));   // pobranie danych przeciaganego panelu
            tlp.Location = new Point((panel6.Width - tlp.Width) / 2, (panel6.Height - tlp.Height) / 2); // wysrodkowanie panelu w obszarze docelowym
            tlp.Parent = panel6;    // ustawienie obszaru docelowego jako nowego rodzica przeciagnietego panelu
            tlp.BringToFront();

            foreach (Control c in tlp.Controls)
            {
                if (c is Label)     // wyszukiwanie ukrytej nazwy obrazka
                {
                    c.Visible = true;   // wyswietlanie ukrytej nazwy po umieszczeniu obrazka w miejscu docelowym
                }
            }
        }

        private void panel7_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TableLayoutPanel))) e.Effect = DragDropEffects.Move;
        }

        private void panel7_DragDrop(object sender, DragEventArgs e)
        {
            var tlp = (TableLayoutPanel)e.Data.GetData(typeof(TableLayoutPanel));   // pobranie danych przeciaganego panelu
            tlp.Location = new Point((panel7.Width - tlp.Width) / 2, (panel7.Height - tlp.Height) / 2); // wysrodkowanie panelu w obszarze docelowym
            tlp.Parent = panel7;    // ustawienie obszaru docelowego jako nowego rodzica przeciagnietego panelu
            tlp.BringToFront();

            foreach (Control c in tlp.Controls)
            {
                if (c is Label)     // wyszukiwanie ukrytej nazwy obrazka
                {
                    c.Visible = true;   // wyswietlanie ukrytej nazwy po umieszczeniu obrazka w miejscu docelowym
                }
            }
        }

        private void panel8_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TableLayoutPanel))) e.Effect = DragDropEffects.Move;
        }

        private void panel8_DragDrop(object sender, DragEventArgs e)
        {
            var tlp = (TableLayoutPanel)e.Data.GetData(typeof(TableLayoutPanel));   // pobranie danych przeciaganego panelu
            tlp.Location = new Point((panel8.Width - tlp.Width) / 2, (panel8.Height - tlp.Height) / 2); // wysrodkowanie panelu w obszarze docelowym
            tlp.Parent = panel8;    // ustawienie obszaru docelowego jako nowego rodzica przeciagnietego panelu
            tlp.BringToFront();

            foreach (Control c in tlp.Controls)
            {
                if (c is Label)     // wyszukiwanie ukrytej nazwy obrazka
                {
                    c.Visible = true;   // wyswietlanie ukrytej nazwy po umieszczeniu obrazka w miejscu docelowym
                }
            }
        }

        private void panel9_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TableLayoutPanel))) e.Effect = DragDropEffects.Move;
        }

        private void panel9_DragDrop(object sender, DragEventArgs e)
        {
            var tlp = (TableLayoutPanel)e.Data.GetData(typeof(TableLayoutPanel));   // pobranie danych przeciaganego panelu
            tlp.Location = new Point((panel9.Width - tlp.Width) / 2, (panel9.Height - tlp.Height) / 2); // wysrodkowanie panelu w obszarze docelowym
            tlp.Parent = panel9;    // ustawienie obszaru docelowego jako nowego rodzica przeciagnietego panelu
            tlp.BringToFront();

            foreach (Control c in tlp.Controls)
            {
                if (c is Label)     // wyszukiwanie ukrytej nazwy obrazka
                {
                    c.Visible = true;   // wyswietlanie ukrytej nazwy po umieszczeniu obrazka w miejscu docelowym
                }
            }
        }

        private void panel10_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TableLayoutPanel))) e.Effect = DragDropEffects.Move;
        }

        private void panel10_DragDrop(object sender, DragEventArgs e)
        {
            var tlp = (TableLayoutPanel)e.Data.GetData(typeof(TableLayoutPanel));   // pobranie danych przeciaganego panelu
            tlp.Location = new Point((panel10.Width - tlp.Width) / 2, (panel10.Height - tlp.Height) / 2); // wysrodkowanie panelu w obszarze docelowym
            tlp.Parent = panel10;    // ustawienie obszaru docelowego jako nowego rodzica przeciagnietego panelu
            tlp.BringToFront();

            foreach (Control c in tlp.Controls)
            {
                if (c is Label)     // wyszukiwanie ukrytej nazwy obrazka
                {
                    c.Visible = true;   // wyswietlanie ukrytej nazwy po umieszczeniu obrazka w miejscu docelowym
                }
            }
        }

        private void panel1_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {
            
        }
    }
}
