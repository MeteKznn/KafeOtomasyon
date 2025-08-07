using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace KahveOtomasyonProjesi
{
    public partial class FrmMasaRaporuDetayi : Form
    {
        private sqlbaglantisi bgl = new sqlbaglantisi();
        private int masaId;
        private DateTime baslangicTarihi;
        private DateTime bitisTarihi;

        public FrmMasaRaporuDetayi(int masaId, DateTime baslangic, DateTime bitis)
        {
            InitializeComponent();
            this.masaId = masaId;
            this.baslangicTarihi = baslangic;
            this.bitisTarihi = bitis;
            //this.Load += FrmMasaRaporuDetayi_Load;
        }

        private void GridAyarlariniUygula()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.RowTemplate.Height = 40;

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TarihSaat",
                HeaderText = "Sipariş Tarihi",
                DataPropertyName = "TarihSaat",
                DefaultCellStyle =
                {
                    Format = "g",
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "OdemeTipi",
                HeaderText = "Ödeme Türü",
                DataPropertyName = "OdemeTipi",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ToplamFiyat",
                HeaderText = "Toplam Fiyat",
                DataPropertyName = "ToplamFiyat",
                DefaultCellStyle = { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleCenter },
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AcikKalmaSuresi",
                HeaderText = "Açık Kalma Süresi",
                DataPropertyName = "AcikKalmaSuresi",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "KullaniciAd",
                HeaderText = "Kasiyer",
                DataPropertyName = "KullaniciAd",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SiparisID",
                HeaderText = "Sipariş ID",
                DataPropertyName = "SiparisID",
                Visible = false // Kullanıcıya gösterilmez
            });
            var btnCol = new DataGridViewButtonColumn
            {
                Name = "Urunler",
                HeaderText = "Ürünler",
                Text = "Ürünler",
                UseColumnTextForButtonValue = true,
                Width = 120
            };
            dataGridView1.Columns.Add(btnCol);

            // Grid stil ayarları (diğer sayfalardaki gibi)
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 11);
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(36, 23, 64);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView1.DefaultCellStyle.BackColor = Color.White;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);

            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.ColumnHeadersHeight = 40;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(36, 23, 64);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Padding = new Padding(5);

            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dataGridView1.GridColor = Color.LightGray;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView1.BorderStyle = BorderStyle.FixedSingle;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(36, 23, 64);
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            // İlk satırın otomatik seçilmesini engelle
            dataGridView1.CurrentCell = null;
        }

        private void VerileriListele()
        {
            try
            {
                using (var conn = bgl.baglanti())
                {
                    StringBuilder query = new StringBuilder();
                    query.Append(@"
                        SELECT 
                            s.SiparisID,
                            s.TarihSaat,
                            s.OdemeTipi,
                            s.ToplamFiyat,
                            k.KullaniciAd,
                            (
                                SELECT MIN(sh.Tarih) 
                                FROM SatisHareket sh 
                                WHERE sh.SiparisID = s.SiparisID
                            ) AS AcilisZamani
                        FROM Siparisler s
                        INNER JOIN Kullanicilar k ON s.KasiyerID = k.KullaniciID
                        WHERE s.MasaID = @p_masaId AND s.TarihSaat BETWEEN @p_baslangic AND @p_bitis
                        ORDER BY s.TarihSaat DESC;
                    ");

                    SqlDataAdapter da = new SqlDataAdapter(query.ToString(), conn);
                    da.SelectCommand.Parameters.AddWithValue("@p_masaId", this.masaId);
                    da.SelectCommand.Parameters.AddWithValue("@p_baslangic", this.baslangicTarihi);
                    da.SelectCommand.Parameters.AddWithValue("@p_bitis", this.bitisTarihi);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Açık kalma süresi hesapla ve yeni sütun ekle
                    if (!dt.Columns.Contains("AcikKalmaSuresi"))
                        dt.Columns.Add("AcikKalmaSuresi", typeof(string));

                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["AcilisZamani"] != DBNull.Value && row["TarihSaat"] != DBNull.Value)
                        {
                            DateTime acilis = Convert.ToDateTime(row["AcilisZamani"]);
                            DateTime kapanis = Convert.ToDateTime(row["TarihSaat"]);
                            TimeSpan fark = kapanis - acilis;
                            row["AcikKalmaSuresi"] = string.Format("{0} saat {1} dk", (int)fark.TotalHours, fark.Minutes);
                        }
                        else
                        {
                            row["AcikKalmaSuresi"] = "-";
                        }
                    }

                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Rapor detayları yüklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "Urunler")
            {
                int siparisId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["SiparisID"].Value);
                // Yeni formu aç
                FrmSiparisUrunleri frm = new FrmSiparisUrunleri(siparisId);
                frm.ShowDialog();
            }
        }

        private void FrmMasaRaporuDetayi_Load(object sender, EventArgs e)
        {
            GridAyarlariniUygula();
            VerileriListele();
            dataGridView1.CellContentClick += DataGridView1_CellContentClick;
            
            // ESC tuşu ile form kapatma
            this.KeyPreview = true;
            this.KeyDown += FrmMasaRaporuDetayi_KeyDown;
        }

        private void FrmMasaRaporuDetayi_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC tuşuna basıldığında formu kapat
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                e.Handled = true;
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
        }
    }
}
