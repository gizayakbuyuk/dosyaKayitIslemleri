using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; // dosyalar üzerinde işlem yapmak için gerekli olan kütüphane
namespace Dosya_Kayıt_İşlemleri
{
    public partial class Form1 : Form
    {
        string dosyaAdi = "kayitlar.ismek";
        string[] kayitlar = new string[1];
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonListele_Click(object sender, EventArgs e)
        {
            Array.Resize(ref kayitlar, File.ReadAllLines(dosyaAdi).Count());
            kayitlar = File.ReadAllLines(dosyaAdi);
            // form içerisindeki tüm nesneleri getiren bir döngü kur
            foreach (Control kontrol in this.Controls)
            {//her kontrol elemanını listbox mı diye kontrol et
                if (kontrol is ListBox)
                {//bulduğun listboxların tür dönüşümünü yaparak içerisini temizle
                    ((ListBox)kontrol).Items.Clear();
                }
            }

            foreach (string kayit  in kayitlar)
            {
                string[] alanlar = kayit.Split('#');
                if (alanlar.Length > 1)
                {
                    listBoxAd.Items.Add(alanlar[0]);
                    listBoxSoad.Items.Add(alanlar[1]);
                    listBoxTel.Items.Add(alanlar[2]);
                    listBoxEposta.Items.Add(alanlar[3]);
                }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists(dosyaAdi)==false)
            {
                File.WriteAllText(dosyaAdi, "");
            }
            buttonListele.PerformClick();

        }

        private void buttonKaydet_Click(object sender, EventArgs e)
        {
            Array.Resize(ref kayitlar, kayitlar.Length + 1);
            kayitlar[kayitlar.Length - 1] = textBoxAd.Text + "#" + textBoxSoyad.Text + "#" + textBoxTel.Text + "#" + textBoxEposta.Text; 
            File.WriteAllLines(dosyaAdi, kayitlar);
            buttonListele.PerformClick();

        }

        private void listBoxAd_SelectedIndexChanged(object sender, EventArgs e)
        {
            int yeniindex = ((ListBox)sender).SelectedIndex;
            listBoxAd.SelectedIndex = yeniindex;
            listBoxSoad.SelectedIndex = yeniindex;
            listBoxTel.SelectedIndex = yeniindex;
            listBoxEposta.SelectedIndex = yeniindex;
            textBoxAd.Text = listBoxAd.Text;
            textBoxSoyad.Text = listBoxSoad.Text;
            textBoxTel.Text = listBoxTel.Text;
            textBoxEposta.Text = listBoxEposta.Text;
            //foreach (Control kontrol in this.Controls)
            //    if (kontrol is ListBox)
            //        ((ListBox)kontrol).SelectedIndex = yeniindex;
                
          
        }

        private void buttonGuncelle_Click(object sender, EventArgs e)
        {
            int index = listBoxAd.SelectedIndex;
            if(index>=0)
            {
                kayitlar[index] = textBoxAd.Text + "#" + textBoxSoyad.Text + "#" + textBoxTel.Text + "#" + textBoxEposta.Text;
                File.WriteAllLines(dosyaAdi, kayitlar);
                buttonListele.PerformClick();
                listBoxAd.SelectedIndex = index;
            }

        }
        void sil(int index)
        {
            int syc = 0;
            string[] yedek = new string[kayitlar.Length - 1];
            for (int i = 0; i < kayitlar.Length; i++)
            {
                if (i!=index)
                {
                    yedek[syc] = kayitlar[i];
                    syc++;
                }
            }
            Array.Copy(yedek, kayitlar, yedek.Length);
        }
        private void buttonSil_Click(object sender, EventArgs e)
        {
            int index = listBoxAd.SelectedIndex;
            if (index >= 0)
            {
                sil(index);
                File.WriteAllLines(dosyaAdi, kayitlar);
                buttonListele.PerformClick();
            }
        }
    }
}
