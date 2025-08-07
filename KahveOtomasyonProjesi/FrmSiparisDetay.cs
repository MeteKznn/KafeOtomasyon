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
    public partial class FrmSiparisDetay : Form
    {
        private int _siparisID;
        sqlbaglantisi bgl = new sqlbaglantisi();
        public FrmSiparisDetay(int siparisID)
        {
            InitializeComponent();
            _siparisID = siparisID;
            this.Load += FrmSiparisDetay_Load;
            dataGridView1.DataBindingComplete += dataGridView1_DataBindingComplete;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
        }

        private void FrmSiparisDetay_Load(object sender, EventArgs e)
        {
            SiparisDetaylariGetir();
            
            // ESC tuşu ile form kapatma
            this.KeyPreview = true;
            this.KeyDown += FrmSiparisDetay_KeyDown;
        }

        private void FrmSiparisDetay_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC tuşuna basıldığında formu kapat
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                e.Handled = true;
            }
        }

        void SiparisDetaylariGetir()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.RowTemplate.Height = 40;

            // Sütunlar
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Tarih",
                HeaderText = "Tarih",
                DataPropertyName = "TarihSaat",
                Width = 150,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UrunAdi",
                HeaderText = "Ürün Adı",
                DataPropertyName = "UrunAdi",
                Width = 180,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Adet",
                HeaderText = "Adet",
                DataPropertyName = "Adet",
                Width = 80,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter },
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "BirimFiyat",
                HeaderText = "Birim Fiyat",
                DataPropertyName = "BirimFiyat",
                Width = 100,
                DefaultCellStyle = { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleCenter },
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ToplamTutar",
                HeaderText = "Toplam Tutar",
                DataPropertyName = "ToplamTutar",
                Width = 120,
                DefaultCellStyle = { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleCenter },
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
                Name = "MasaAdi",
                HeaderText = "Masa",
                DataPropertyName = "MasaAdi",
                Width = 120,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "KullaniciAd",
                HeaderText = "Personel",
                DataPropertyName = "KullaniciAd",
                Width = 120,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            // SQL Sorgusu
            using (SqlConnection conn = bgl.baglanti())
            {
                string sql = @"SELECT 
                                S.TarihSaat,
                                U.UrunAdi,
                                SD.Adet,
                                SD.Fiyat AS BirimFiyat,
                                (SD.Adet * SD.Fiyat) AS ToplamTutar,
                                S.OdemeTipi,
                                M.MasaAdi,
                                K.KullaniciAd
                            FROM SiparisDetay SD
                            INNER JOIN Urunler U ON SD.UrunID = U.UrunID
                            INNER JOIN Siparisler S ON SD.SiparisID = S.SiparisID
                            INNER JOIN Masalar M ON S.MasaID = M.MasaID
                            INNER JOIN Kullanicilar K ON S.KasiyerID = K.KullaniciID
                            WHERE SD.SiparisID = @SiparisID
                            ORDER BY S.TarihSaat DESC";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@SiparisID", _siparisID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }

            // Grid özellikleri
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

            dataGridView1.CurrentCell = null;
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }
    }
}
