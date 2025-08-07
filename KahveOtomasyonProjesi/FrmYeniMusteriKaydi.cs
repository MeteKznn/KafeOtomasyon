using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KahveOtomasyonProjesi
{
    public partial class FrmYeniMusteriKaydi : Form
    {
        public FrmYeniMusteriKaydi()
        {
            InitializeComponent();
            
            // ESC tuşu ile form kapatma
            this.KeyPreview = true;
            this.KeyDown += FrmYeniMusteriKaydi_KeyDown;
        }

        private void FrmYeniMusteriKaydi_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC tuşuna basıldığında formu kapat
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                e.Handled = true;
            }
        }

        private void btniptal_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            // Zorunlu alan kontrolü
            if (string.IsNullOrWhiteSpace(txtAd.Text) || string.IsNullOrWhiteSpace(txtSoyad.Text) || string.IsNullOrWhiteSpace(txtTelefon.Text))
            {
                MessageBox.Show("Ad, Soyad ve Telefon alanları zorunludur.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kullanıcıya onay sor
            DialogResult result = MessageBox.Show("Müşteri kaydını kaydetmek istediğinizden emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
            {
                return;
            }

            using (var conn = new sqlbaglantisi().baglanti())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Musteriler (MusteriAd, MusteriSoyad, MusteriTelefon, MusteriAciklama, ToplamBorc) VALUES (@Ad, @Soyad, @Telefon, @Aciklama, 0)", conn);
                cmd.Parameters.AddWithValue("@Ad", txtAd.Text.Trim());
                cmd.Parameters.AddWithValue("@Soyad", txtSoyad.Text.Trim());
                cmd.Parameters.AddWithValue("@Telefon", txtTelefon.Text.Trim());
                cmd.Parameters.AddWithValue("@Aciklama", rchAciklama.Text.Trim());
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Yeni Müşteri başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
