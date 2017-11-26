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

        /*
        private void tableLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            TableLayoutPanel panel = (TableLayoutPanel)sender;
            panel.Select();
            panel.DoDragDrop(panel, DragDropEffects.Move);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pic1 = (PictureBox)sender;
            TableLayoutPanel panel = (TableLayoutPanel)pic1.Parent;
            panel.Select();
            panel.DoDragDrop(panel, DragDropEffects.Move);
        }

        private void tableLayoutPanel1_DragDrop(object sender, DragEventArgs e)
        {

        } */
    }
}
