using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace KahveOtomasyonProjesi
{
    public partial class CiroTakipUserControl : UserControl
    {
        private sqlbaglantisi bgl = new sqlbaglantisi();
        public CiroTakipUserControl()
        {
            InitializeComponent();
            this.Load += CiroTakipUserControl_Load;
        }

        private void CiroTakipUserControl_Load(object sender, EventArgs e)
        {
            // DateTimePicker'ların başlangıç değerlerini ayarla
            dtpBaslangicTarihi.Value = DateTime.Today;
            dtpBitisTarihi.Value = DateTime.Today;

            UrunleriYukle();
            KasiyerleriYukle();
            MasalariYukle();
            cmbOdemeTuru.SelectedIndex = 0;
            GridAyarlariniUygula();
            VerileriListele();
        }

        private void UrunleriYukle()
        {
            using (var conn = bgl.baglanti())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT UrunID, UrunAdi FROM Urunler ORDER BY UrunAdi", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataRow row = dt.NewRow();
                row["UrunID"] = 0;
                row["UrunAdi"] = "Tüm Ürünler";
                dt.Rows.InsertAt(row, 0);
                cmbUrunler.DataSource = dt;
                cmbUrunler.DisplayMember = "UrunAdi";
                cmbUrunler.ValueMember = "UrunID";
                cmbUrunler.SelectedIndex = 0;
            }
        }

        private void KasiyerleriYukle()
        {
            using (var conn = bgl.baglanti())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT KullaniciID, KullaniciAd FROM Kullanicilar ORDER BY KullaniciAd", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataRow row = dt.NewRow();
                row["KullaniciID"] = 0;
                row["KullaniciAd"] = "Tüm Kasiyerler";
                dt.Rows.InsertAt(row, 0);
                cmbKasiyer.DataSource = dt;
                cmbKasiyer.DisplayMember = "KullaniciAd";
                cmbKasiyer.ValueMember = "KullaniciID";
                cmbKasiyer.SelectedIndex = 0;
            }
        }

        private void MasalariYukle()
        {
            using (var conn = bgl.baglanti())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT MasaID, MasaAdi FROM Masalar ORDER BY MasaID", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DataRow row = dt.NewRow();
                row["MasaID"] = 0;
                row["MasaAdi"] = "Tüm Masalar";
                dt.Rows.InsertAt(row, 0);
                cmbMasalar.DataSource = dt;
                cmbMasalar.DisplayMember = "MasaAdi";
                cmbMasalar.ValueMember = "MasaID";
                cmbMasalar.SelectedIndex = 0;
            }
        }

        private void GridAyarlariniUygula()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.RowTemplate.Height = 40;

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SiparisID",
                HeaderText = "Sipariş ID",
                DataPropertyName = "SiparisID",
                Visible = false
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MasaAdi",
                HeaderText = "Masa",
                DataPropertyName = "MasaAdi",
                Width = 120,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ToplamFiyat",
                HeaderText = "Tutar",
                DataPropertyName = "ToplamFiyat",
                Width = 120,
                DefaultCellStyle = { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleCenter },
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TarihSaat",
                HeaderText = "Tarih",
                DataPropertyName = "TarihSaat",
                Width = 150,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "OdemeTipi",
                HeaderText = "Ödeme Türü",
                DataPropertyName = "OdemeTipi",
                Width = 120,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "KullaniciAd",
                HeaderText = "Kasiyer",
                DataPropertyName = "KullaniciAd",
                Width = 120,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DETAY",
                HeaderText = "DETAY",
                Width = 100,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter },
                HeaderCell = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } }
            });

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

        private void VerileriListele()
        {
            //GridAyarlariniUygula();
            
            using (var conn = bgl.baglanti())
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(@"SELECT S.SiparisID, M.MasaAdi, S.ToplamFiyat, S.TarihSaat, S.OdemeTipi, K.KullaniciAd
                            FROM Siparisler S
                            INNER JOIN Masalar M ON S.MasaID = M.MasaID
                            INNER JOIN Kullanicilar K ON S.KasiyerID = K.KullaniciID
                            WHERE 1=1");

                // Masa filtresi
                if (cmbMasalar.SelectedValue != null && Convert.ToInt32(cmbMasalar.SelectedValue) != 0)
                    sql.Append(" AND M.MasaID = @MasaID");
                // Ödeme türü filtresi
                if (cmbOdemeTuru.SelectedItem != null && cmbOdemeTuru.SelectedItem.ToString() != "Tüm Ödeme Türleri")
                    sql.Append(" AND S.OdemeTipi = @OdemeTipi");
                // Kasiyer filtresi
                if (cmbKasiyer.SelectedValue != null && Convert.ToInt32(cmbKasiyer.SelectedValue) != 0)
                    sql.Append(" AND K.KullaniciID = @KasiyerID");
                // Tarih aralığı filtresi
                DateTime baslangic = dtpBaslangicTarihi.Value.Date;
                DateTime bitis = dtpBitisTarihi.Value.Date;
                if (baslangic <= bitis)
                    sql.Append(" AND CAST(S.TarihSaat AS DATE) BETWEEN @BaslangicTarihi AND @BitisTarihi");
                // Ürün filtresi için alt sorgu
                if (cmbUrunler.SelectedValue != null && Convert.ToInt32(cmbUrunler.SelectedValue) != 0)
                {
                    sql.Append(" AND EXISTS (SELECT 1 FROM SiparisDetay SD WHERE SD.SiparisID = S.SiparisID AND SD.UrunID = @UrunID)");
                }
                //sql.Append(" ORDER BY M.MasaID ASC, S.TarihSaat DESC");
                sql.Append(" ORDER BY S.TarihSaat DESC");

                SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
                if (cmbMasalar.SelectedValue != null && Convert.ToInt32(cmbMasalar.SelectedValue) != 0)
                    cmd.Parameters.AddWithValue("@MasaID", cmbMasalar.SelectedValue);
                if (cmbOdemeTuru.SelectedItem != null && cmbOdemeTuru.SelectedItem.ToString() != "Tüm Ödeme Türleri")
                    cmd.Parameters.AddWithValue("@OdemeTipi", cmbOdemeTuru.SelectedItem.ToString());
                if (cmbKasiyer.SelectedValue != null && Convert.ToInt32(cmbKasiyer.SelectedValue) != 0)
                    cmd.Parameters.AddWithValue("@KasiyerID", cmbKasiyer.SelectedValue);
                if (baslangic <= bitis)
                {
                    cmd.Parameters.AddWithValue("@BaslangicTarihi", baslangic);
                    cmd.Parameters.AddWithValue("@BitisTarihi", bitis);
                }
                if (cmbUrunler.SelectedValue != null && Convert.ToInt32(cmbUrunler.SelectedValue) != 0)
                    cmd.Parameters.AddWithValue("@UrunID", cmbUrunler.SelectedValue);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;

                // Nakit, POS ve Veresiye toplamlarını hesapla
                decimal nakitToplam = 0;
                decimal posToplam = 0;
                decimal veresiyeToplam = 0;
                int toplamAdet = 0;

                foreach (DataRow row in dt.Rows)
                {
                    if (row["ToplamFiyat"] != DBNull.Value)
                    {
                        decimal tutar = Convert.ToDecimal(row["ToplamFiyat"]);
                        string odemeTipi = row["OdemeTipi"].ToString();

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
                    toplamAdet++;
                }

                // Label'lara toplamları yaz
                lbl_nakitToplami.Text = nakitToplam.ToString("C2");
                lbl_posToplami.Text = posToplam.ToString("C2");
                lbl_veresiyeToplami.Text = veresiyeToplam.ToString("C2");
            }
        }

        private void btnRapor_Click(object sender, EventArgs e)
        {
            if (dtpBaslangicTarihi.Value.Date > dtpBitisTarihi.Value.Date)
            {
                MessageBox.Show("Başlangıç tarihi bitiş tarihinden büyük olamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            VerileriListele();
        }

        private void DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (columnName == "DETAY")
            {
                e.Handled = true;
                e.PaintBackground(e.ClipBounds, true);
                string text = "DETAY";
                bool isSelected = dataGridView1.Rows[e.RowIndex].Selected;
                Color foreColor = isSelected ? Color.White : Color.Black;
                Rectangle rect = e.CellBounds;
                rect.Inflate(-2, -2);
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
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "DETAY")
            {
                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["SiparisID"].Value);
                FrmSiparisDetay detayForm = new FrmSiparisDetay(id);
                detayForm.ShowDialog();
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            // DateTimePicker'ların başlangıç değerlerini ayarla
            dtpBaslangicTarihi.Value = DateTime.Today;
            dtpBitisTarihi.Value = DateTime.Today;

            // ComboBox'ları sıfırla
            cmbUrunler.SelectedIndex = 0;
            cmbKasiyer.SelectedIndex = 0;
            cmbMasalar.SelectedIndex = 0;
            cmbOdemeTuru.SelectedIndex = 0;

            // Verileri yeniden listele
            VerileriListele();
        }

        private void cmbOdemeTuru_SelectedIndexChanged(object sender, EventArgs e)
        {
            VerileriListele();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
        }
    }
}
