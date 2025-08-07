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
    public partial class FrmUrunGiris : Form
    {
        sqlbaglantisi bgl = new sqlbaglantisi();
        public FrmUrunGiris()
        {
            InitializeComponent();
        }

        private void FrmUrunGiris_Load(object sender, EventArgs e)
        {
            EditDesign();
            LoadCategorys();
            
            // ESC tuşu ile form kapatma
            this.KeyPreview = true;
            this.KeyDown += FrmUrunGiris_KeyDown;
        }

        private void FrmUrunGiris_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC tuşuna basıldığında formu kapat
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                e.Handled = true;
            }
        }


        void EditDesign()
        {
            ControlStyles.ApplyBorderRadius(addProductPanel, 10, RoundedCorners.All);
            ControlStyles.ApplyBorderRadius(btnUrunEkle, 8, RoundedCorners.All);
        }

        void LoadCategorys()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Kategoriler", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbKategoriler.DataSource = dt;
            cmbKategoriler.DisplayMember = "KategoriAdi";
            cmbKategoriler.ValueMember = "KategoriID";
        }

        private void btnUrunEkle_Click(object sender, EventArgs e)
        {
            // 1. Boşluk ve geçerlilik kontrolleri
            if (string.IsNullOrWhiteSpace(txtUrunAd.Text) ||
                string.IsNullOrWhiteSpace(txtFiyat.Text) ||
                cmbKategoriler.SelectedValue == null)
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtFiyat.Text, out decimal fiyat))
            {
                MessageBox.Show("Fiyat alanı geçerli bir sayı olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. SQL ile ekleme işlemi
            using (SqlConnection conn = bgl.baglanti())
            {
                string sql = "INSERT INTO Urunler (UrunAdi, Fiyat, Resim, KategoriID,Durum) VALUES (@ad, @fiyat, @resim, @kategori,@durum)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ad", txtUrunAd.Text.Trim());
                cmd.Parameters.AddWithValue("@fiyat", fiyat);
                cmd.Parameters.AddWithValue("@resim", txtUrunGorsel.Text.Trim());
                cmd.Parameters.AddWithValue("@kategori", cmbKategoriler.SelectedValue);
                cmd.Parameters.AddWithValue("@durum", true);

                try
                {
                    int sonuc = cmd.ExecuteNonQuery();
                    if (sonuc > 0)
                    {
                        MessageBox.Show("Ürün başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Alanları temizle
                        txtUrunAd.Clear();
                        txtFiyat.Clear();
                        txtUrunGorsel.Clear();
                        cmbKategoriler.SelectedIndex = 0;
                    }
                    else
                    {
                        MessageBox.Show("Ürün eklenemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDosyaSeç_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Ürün Görseli Seç";
                ofd.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string secilenDosya = ofd.FileName;
                    string dosyaAdi = System.IO.Path.GetFileName(secilenDosya);
                    string hedefKlasor = System.IO.Path.Combine(Application.StartupPath, "images");
                    if (!System.IO.Directory.Exists(hedefKlasor))
                        System.IO.Directory.CreateDirectory(hedefKlasor);
                    string hedefYol = System.IO.Path.Combine(hedefKlasor, dosyaAdi);

                    try
                    {
                        System.IO.File.Copy(secilenDosya, hedefYol, true);
                        txtUrunGorsel.Text = "images/" + dosyaAdi;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Görsel kopyalanırken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
