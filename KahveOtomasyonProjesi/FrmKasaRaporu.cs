using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace KahveOtomasyonProjesi
{
    public partial class FrmKasaRaporu : Form
    {
        sqlbaglantisi bgl = new sqlbaglantisi();

        public FrmKasaRaporu()
        {
            InitializeComponent();
        }

        private void FrmKasaRaporu_Load(object sender, EventArgs e)
        {
            // DateTimePicker'ların başlangıç değerlerini ayarla
            dtpBaslangicTarihi.Value = DateTime.Today;
            dtpBitisTarihi.Value = DateTime.Today;
            
            // ComboBox'ların başlangıç değerlerini ayarla
            cmbislemTipi.SelectedItem = "Tüm İşlemler";
            
            // Kasiyer ComboBox'ını doldur
            LoadKasiyerler();
            
            // ESC tuşu ile form kapatma
            this.KeyPreview = true;
            this.KeyDown += FrmKasaRaporu_KeyDown;
        }

        private void FrmKasaRaporu_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC tuşuna basıldığında formu kapat
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                e.Handled = true;
            }
        }
        
        private void LoadKasiyerler()
        {
            using (SqlConnection conn = bgl.baglanti())
            {
                string sqlQuery = "SELECT KullaniciID, KullaniciAd FROM Kullanicilar ORDER BY KullaniciAd";
                SqlCommand komut = new SqlCommand(sqlQuery, conn);
                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // "Tüm Kasiyerler" seçeneğini ekle
                DataRow row = dt.NewRow();
                row["KullaniciID"] = 0;
                row["KullaniciAd"] = "Tüm Kasiyerler";
                dt.Rows.InsertAt(row, 0);

                cmbKasiyer.DisplayMember = "KullaniciAd";
                cmbKasiyer.ValueMember = "KullaniciID";
                cmbKasiyer.DataSource = dt;
                cmbKasiyer.SelectedIndex = 0;
            }
        }

        private void LoadKasaRaporu()
        {
            // Önce DataSource'u null yap
            dataGridView1.DataSource = null;
            
            // Sonra sütunları temizle
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.RowTemplate.Height = 40;

            // --- Sütunlar tanımlanıyor ---
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SiparisID",
                HeaderText = "Sipariş ID",
                DataPropertyName = "SiparisID",
                Visible = false
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "IslemTarihi",
                HeaderText = "İşlem Tarihi",
                DataPropertyName = "IslemTarihi",
                Width = 150,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "OdemeTipi",
                HeaderText = "Ödeme Tipi",
                DataPropertyName = "OdemeTipi",
                Width = 120,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Gelir",
                HeaderText = "Gelir",
                DataPropertyName = "Gelir",
                Width = 120,
                DefaultCellStyle = { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleCenter },
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AcikKalmaSuresi",
                HeaderText = "Açık Kalma Süresi",
                DataPropertyName = "AcikKalmaSuresi",
                Width = 150,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            // Son 3 sütun: Detay - Kasiyer - Sil
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DETAY",
                HeaderText = "DETAY",
                Width = 100,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter },
                HeaderCell = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } }
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Kasiyer",
                HeaderText = "Kasiyer",
                DataPropertyName = "Kasiyer",
                Width = 120,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SIL",
                HeaderText = "SİL",
                Width = 100,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter },
                HeaderCell = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } }
            });

            // Sütunları ekledikten sonra sıralamayı kesin olarak ayarla
            dataGridView1.Columns["IslemTarihi"].DisplayIndex = 0;
            dataGridView1.Columns["OdemeTipi"].DisplayIndex = 1;
            dataGridView1.Columns["Gelir"].DisplayIndex = 2;
            dataGridView1.Columns["AcikKalmaSuresi"].DisplayIndex = 3;
            dataGridView1.Columns["DETAY"].DisplayIndex = 4;
            dataGridView1.Columns["Kasiyer"].DisplayIndex = 5;
            dataGridView1.Columns["SIL"].DisplayIndex = 6;
            // AcilisZamani DataGridView'da görünmesin
            if (dataGridView1.Columns.Contains("AcilisZamani"))
                dataGridView1.Columns["AcilisZamani"].Visible = false;

            // --- Veri Çekiliyor ---
            using (SqlConnection conn = bgl.baglanti())
            {
                string sqlQuery = @"SELECT 
                                    S.SiparisID, 
                                    S.TarihSaat AS IslemTarihi, 
                                    S.OdemeTipi, 
                                    S.ToplamFiyat AS Gelir, 
                                    K.KullaniciAd AS Kasiyer,
                                    (SELECT MIN(sh.Tarih) FROM SatisHareket sh WHERE sh.SiparisID = S.SiparisID) AS AcilisZamani
                                    FROM Siparisler AS S
                                    JOIN Kullanicilar AS K ON S.KasiyerID = K.KullaniciID
                                    WHERE 1=1";

                // Ödeme tipi filtresi ekle
                if (cmbislemTipi.SelectedItem != null && cmbislemTipi.SelectedItem.ToString() != "Tüm İşlemler")
                {
                    sqlQuery += " AND S.OdemeTipi = @OdemeTipi";
                }

                // Kasiyer filtresi ekle
                if (cmbKasiyer.SelectedValue != null && Convert.ToInt32(cmbKasiyer.SelectedValue) != 0)
                {
                    sqlQuery += " AND S.KasiyerID = @KasiyerID";
                }

                // Tarih aralığı filtresi ekle
                if (dtpBaslangicTarihi.Value.Date <= dtpBitisTarihi.Value.Date)
                {
                    sqlQuery += " AND CAST(S.TarihSaat AS DATE) BETWEEN @BaslangicTarihi AND @BitisTarihi";
                }

                sqlQuery += " ORDER BY S.TarihSaat DESC";

                SqlCommand komut = new SqlCommand(sqlQuery, conn);

                // Ödeme tipi parametresi ekle
                if (cmbislemTipi.SelectedItem != null && cmbislemTipi.SelectedItem.ToString() != "Tüm İşlemler")
                {
                    komut.Parameters.AddWithValue("@OdemeTipi", cmbislemTipi.SelectedItem.ToString());
                }

                // Kasiyer parametresi ekle
                if (cmbKasiyer.SelectedValue != null && Convert.ToInt32(cmbKasiyer.SelectedValue) != 0)
                {
                    komut.Parameters.AddWithValue("@KasiyerID", Convert.ToInt32(cmbKasiyer.SelectedValue));
                }

                // Tarih parametrelerini ekle
                if (dtpBaslangicTarihi.Value.Date <= dtpBitisTarihi.Value.Date)
                {
                    komut.Parameters.AddWithValue("@BaslangicTarihi", dtpBaslangicTarihi.Value.Date);
                    komut.Parameters.AddWithValue("@BitisTarihi", dtpBitisTarihi.Value.Date);
                }

                SqlDataAdapter da = new SqlDataAdapter(komut);
                DataTable dt = new DataTable();
                da.Fill(dt);
                // Açık kalma süresi hesapla ve yeni sütun ekle
                if (!dt.Columns.Contains("AcikKalmaSuresi"))
                    dt.Columns.Add("AcikKalmaSuresi", typeof(string));
                foreach (DataRow row in dt.Rows)
                {
                    if (row["AcilisZamani"] != DBNull.Value && row["IslemTarihi"] != DBNull.Value)
                    {
                        DateTime acilis = Convert.ToDateTime(row["AcilisZamani"]);
                        DateTime kapanis = Convert.ToDateTime(row["IslemTarihi"]);
                        TimeSpan fark = kapanis - acilis;
                        row["AcikKalmaSuresi"] = string.Format("{0} saat {1} dk", (int)fark.TotalHours, fark.Minutes);
                    }
                    else
                    {
                        row["AcikKalmaSuresi"] = "-";
                    }
                }
                dataGridView1.DataSource = dt;

                // Sütun sıralamasını veri bağlandıktan sonra uygula
                dataGridView1.Columns["IslemTarihi"].DisplayIndex = 0;
                dataGridView1.Columns["OdemeTipi"].DisplayIndex = 1;
                dataGridView1.Columns["Gelir"].DisplayIndex = 2;
                dataGridView1.Columns["AcikKalmaSuresi"].DisplayIndex = 3;
                dataGridView1.Columns["DETAY"].DisplayIndex = 4;
                dataGridView1.Columns["Kasiyer"].DisplayIndex = 5;
                dataGridView1.Columns["SIL"].DisplayIndex = 6;
                if (dataGridView1.Columns.Contains("AcilisZamani"))
                    dataGridView1.Columns["AcilisZamani"].Visible = false;

                // Toplam tutarları hesapla
                decimal nakitToplam = 0;
                decimal posToplam = 0;
                decimal veresiyeToplam = 0;
                decimal toplamGelir = 0;

                foreach (DataRow row in dt.Rows)
                {
                    decimal tutar = Convert.ToDecimal(row["Gelir"]);
                    string odemeTipi = row["OdemeTipi"].ToString();
                    toplamGelir += tutar; // Toplam geliri hesapla

                    switch (odemeTipi.ToLower())
                    {
                        case "nakit":
                            nakitToplam += tutar;
                            break;
                        case "pos":
                            posToplam += tutar;
                            break;
                        case "veresiye":
                            veresiyeToplam += tutar;
                            break;
                    }
                }

                // Labellara toplam tutarları yazdır
                label6.Text = nakitToplam.ToString("C2");
                label7.Text = posToplam.ToString("C2");
                label9.Text = veresiyeToplam.ToString("C2");
                label13.Text = toplamGelir.ToString("C2"); // Toplam geliri yazdır

                // Devreden Kasa ve Kasa Toplamı hesapla
                HesaplaDevredenKasaVeToplam();
            }

            // --- Seçim ve görünüm ayarları ---
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

            // Header ayarları
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.ColumnHeadersHeight = 40; // Header yüksekliğini artır
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(36, 23, 64);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Padding = new Padding(5); // Header içeriğine padding ekle

            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            dataGridView1.GridColor = Color.LightGray;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView1.BorderStyle = BorderStyle.FixedSingle;

            dataGridView1.CellPainting -= DataGridView1_CellPainting;
            dataGridView1.CellPainting += DataGridView1_CellPainting;

            dataGridView1.CellClick -= DataGridView1_CellClick;
            dataGridView1.CellClick += DataGridView1_CellClick;

            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(36, 23, 64);
            dataGridView1.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;

            // İlk satırın otomatik seçilmesini engelle
            dataGridView1.CurrentCell = null;
        }

        private void DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (columnName == "DETAY" || columnName == "SIL")
            {
                e.Handled = true;
                e.PaintBackground(e.ClipBounds, true);

                string text = columnName == "DETAY" ? "DETAY" : "SİL";

                // Seçili satır kontrolü
                bool isSelected = dataGridView1.Rows[e.RowIndex].Selected;

                // Renkler
                Color foreColor = isSelected ? Color.White : (columnName == "DETAY" ? Color.Black : Color.Red);

                // Hücre alanı
                Rectangle rect = e.CellBounds;
                rect.Inflate(-2, -2);

                // Metni çiz
                TextRenderer.DrawText(
                    e.Graphics,
                    text,
                    new Font("Segoe UI", 10, FontStyle.Bold),
                    rect,
                    foreColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                );
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == dataGridView1.Columns["SIL"].Index)
                {
                    // Rol kontrolü
                    if (!KullaniciBilgileri.YetkiKontrol('1'))
                    {
                        MessageBox.Show("Bu işlem için yetkiniz bulunmamaktadır!", "Yetki Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["SiparisID"].Value);
                    if (MessageBox.Show("Bu kaydı silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using (SqlConnection conn = bgl.baglanti())
                        {
                            SqlTransaction transaction = conn.BeginTransaction();
                            try
                            {
                                // Önce SiparisDetay tablosundaki ilgili kayıtları sil
                                SqlCommand komutDetay = new SqlCommand("DELETE FROM SiparisDetay WHERE SiparisID = @p1", conn, transaction);
                                komutDetay.Parameters.AddWithValue("@p1", id);
                                komutDetay.ExecuteNonQuery();

                                // Sonra Siparisler tablosundaki kaydı sil
                                SqlCommand komutSiparis = new SqlCommand("DELETE FROM Siparisler WHERE SiparisID = @p1", conn, transaction);
                                komutSiparis.Parameters.AddWithValue("@p1", id);
                                komutSiparis.ExecuteNonQuery();

                                transaction.Commit();
                                MessageBox.Show("Kayıt başarıyla silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadKasaRaporu(); // Tabloyu yenile
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                MessageBox.Show("Silme işlemi sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                else if (e.ColumnIndex == dataGridView1.Columns["DETAY"].Index)
                {
                    int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["SiparisID"].Value);
                    FrmSiparisDetay detayForm = new FrmSiparisDetay(id);
                    detayForm.ShowDialog();
                }
            }
        }

        private void cmbislemTipi_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            LoadKasaRaporu();
        }

        private void btnRapor_Click(object sender, EventArgs e)
        {
            if (dtpBaslangicTarihi.Value.Date > dtpBitisTarihi.Value.Date)
            {
                MessageBox.Show("Başlangıç tarihi bitiş tarihinden büyük olamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoadKasaRaporu();
        }

        private void cmbKasiyer_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadKasaRaporu();
        }

        private void cmbKasiyer_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            LoadKasaRaporu();
        }

        private void HesaplaDevredenKasaVeToplam()
        {
            using (SqlConnection conn = bgl.baglanti())
            {
                // Devreden Kasa: Bugün hariç tüm veriler
                string devredenKasaQuery = @"SELECT ISNULL(SUM(ToplamFiyat), 0) 
                                            FROM Siparisler 
                                            WHERE CAST(TarihSaat AS DATE) < @BugunTarihi";
                
                SqlCommand devredenCmd = new SqlCommand(devredenKasaQuery, conn);
                devredenCmd.Parameters.AddWithValue("@BugunTarihi", DateTime.Today);
                decimal devredenKasa = Convert.ToDecimal(devredenCmd.ExecuteScalar());

                // Kasa Toplamı: Tüm veriler
                string kasaToplamQuery = @"SELECT ISNULL(SUM(ToplamFiyat), 0) 
                                          FROM Siparisler";
                
                SqlCommand toplamCmd = new SqlCommand(kasaToplamQuery, conn);
                decimal kasaToplami = Convert.ToDecimal(toplamCmd.ExecuteScalar());

                // Label'lara yazdır
                lbl_devredenKasa.Text = devredenKasa.ToString("C2");
                lbl_KasaToplami.Text = kasaToplami.ToString("C2");
            }
        }
    }
}
