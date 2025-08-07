using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KahveOtomasyonProjesi
{
    public partial class FrmSayfalaraGecisEkrani : Form
    {
        public FrmSayfalaraGecisEkrani()
        {
            InitializeComponent();
            this.FormClosing += FrmSayfalaraGecisEkrani_FormClosing;
            
            // ESC tuşu ile form kapatma
            this.KeyPreview = true;
            this.KeyDown += FrmSayfalaraGecisEkrani_KeyDown;
        }

        private void FrmSayfalaraGecisEkrani_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void FrmSayfalaraGecisEkrani_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC tuşuna basıldığında formu kapat
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                e.Handled = true;
            }
        }

        private void btnHizliSatis_Click(object sender, EventArgs e)
        {
            using (FrmSatisEkrani fr = new FrmSatisEkrani())
            {
                this.Hide();
                fr.ShowDialog();
                this.Show();
            }
        }

        private void btnUrunGiris_Click(object sender, EventArgs e)
        {
            // Ürün giriş formu
            using (FrmUrunGiris fr = new FrmUrunGiris())
            {
                this.Hide();
                fr.ShowDialog();
                this.Show();
            }
        }

        private void btnistatistikler_Click(object sender, EventArgs e)
        {
            
            // İstatistikler formu
            using (Frmistatistikler fr = new Frmistatistikler())
            {
                this.Hide();
                fr.ShowDialog();
                this.Show();
            }
        }

        private void btnKasaRaporu_Click(object sender, EventArgs e)
        {
            using (FrmKasaRaporu fr = new FrmKasaRaporu())
            {
                this.Hide();
                fr.ShowDialog();
                this.Show();
            }
        }

        private void btnAyarlar_Click(object sender, EventArgs e)
        {
            // Ayarlar formu
            using (FrmAyarlar fr = new FrmAyarlar())
            {
                this.Hide();
                fr.ShowDialog();
                this.Show();
            }
        }

        private void btnVeresiyeler_Click(object sender, EventArgs e)
        {
            //Veresiyeler sayfası
            using (FrmVeresiyeler fr = new FrmVeresiyeler())
            {
                this.Hide();
                fr.ShowDialog();
                this.Show();
            }
        }
    }
}
