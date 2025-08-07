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
    public partial class FrmGiris : Form
    {
        sqlbaglantisi bgl = new sqlbaglantisi();
        public FrmGiris()
        {
            InitializeComponent();
            
            // ESC tuşu ile form kapatma
            this.KeyPreview = true;
            this.KeyDown += FrmGiris_KeyDown;
        }

        private void FrmGiris_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC tuşuna basıldığında formu kapat
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKullaniciAdi.Text) || string.IsNullOrWhiteSpace(txtSifre.Text))
            {
                MessageBox.Show("Kullanıcı adı ve şifre boş olamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = bgl.baglanti())
            {
                SqlCommand komut = new SqlCommand("SELECT * FROM Kullanicilar WHERE Mail = @p1 AND Sifre = @p2", conn);
                komut.Parameters.AddWithValue("@p1", txtKullaniciAdi.Text);
                komut.Parameters.AddWithValue("@p2", txtSifre.Text);

                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    // Kullanıcı bilgilerini kaydet
                    KullaniciBilgileri.KullaniciAdi = dr["KullaniciAd"].ToString();
                    KullaniciBilgileri.RolID = Convert.ToInt32(dr["RolID"]);
                    KullaniciBilgileri.KullaniciID = Convert.ToInt32(dr["KullaniciID"]);
                    KullaniciBilgileri.Telefon = dr["Telefon"].ToString();
                    KullaniciBilgileri.Mail = dr["Mail"].ToString();
                    KullaniciBilgileri.Sifre = dr["Sifre"].ToString();

                    // Giriş başarılı
                    FrmSayfalaraGecisEkrani gecisForm = new FrmSayfalaraGecisEkrani();
                    this.Hide(); // Giriş formunu gizle
                    gecisForm.ShowDialog(); // Geçiş formunu göster
                    this.Close(); // Giriş formunu kapat
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
