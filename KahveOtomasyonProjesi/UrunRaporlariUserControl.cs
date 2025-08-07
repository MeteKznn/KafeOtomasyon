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
    public partial class UrunRaporlariUserControl : UserControl
    {
        public UrunRaporlariUserControl()
        {
            InitializeComponent();
            this.Load += UrunRaporlariUserControl_Load;
        }

        private sqlbaglantisi bgl = new sqlbaglantisi();

        private void UrunRaporlariUserControl_Load(object sender, EventArgs e)
        {
            // DateTimePicker'ların başlangıç değerlerini ayarla
            dtpBaslangicTarihi.Value = DateTime.Today;
            dtpBitisTarihi.Value = DateTime.Today;

            UrunleriYukle();
            KasiyerleriYukle();
            SatislariYukle();
            // İlk yüklemede bugünün satışları gelmesin, sadece filtreler ayarlansın
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

        private void SatislariYukle()
        {
            int urunId = Convert.ToInt32(cmbUrunler.SelectedValue);
            int kasiyerId = Convert.ToInt32(cmbKasiyer.SelectedValue);
            DateTime baslangic = dtpBaslangicTarihi.Value.Date;
            DateTime bitis = dtpBitisTarihi.Value.Date.AddDays(1).AddSeconds(-1);

            using (var conn = bgl.baglanti())
            {
                StringBuilder query = new StringBuilder();
                query.Append(@"
            SELECT 
                sh.Id AS [Satış No],
                u.UrunAdi AS [Ürün Adı],
                k.KullaniciAd AS [Kasiyer],
                m.MasaAdi AS [Masa],
                sh.Adet,
                sh.BirimFiyat,
                (sh.Adet * sh.BirimFiyat) AS [Tutar],
                sh.Tarih
            FROM SatisHareket sh
            LEFT JOIN Urunler u ON sh.UrunID = u.UrunID
            LEFT JOIN Siparisler s ON sh.SiparisID = s.SiparisID
            LEFT JOIN Kullanicilar k ON s.KasiyerID = k.KullaniciID
            LEFT JOIN Masalar m ON sh.MasaID = m.MasaID
            WHERE sh.Tarih BETWEEN @Baslangic AND @Bitis
        ");

                if (urunId != 0)
                    query.Append(" AND sh.UrunID = @UrunID ");
                if (kasiyerId != 0)
                    query.Append(" AND s.KasiyerID = @KullaniciID ");

                // Tarihe göre azalan sıralama
                query.Append(" ORDER BY sh.Tarih DESC ");

                SqlCommand cmd = new SqlCommand(query.ToString(), conn);
                cmd.Parameters.AddWithValue("@Baslangic", baslangic);
                cmd.Parameters.AddWithValue("@Bitis", bitis);
                if (urunId != 0)
                    cmd.Parameters.AddWithValue("@UrunID", urunId);
                if (kasiyerId != 0)
                    cmd.Parameters.AddWithValue("@KullaniciID", kasiyerId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                GridAyarla();

                // Toplam ciro ve adet hesapla
                decimal toplamCiro = 0;
                int toplamAdet = 0;
                foreach (DataRow row in dt.Rows)
                {
                    toplamCiro += row["Tutar"] != DBNull.Value ? Convert.ToDecimal(row["Tutar"]) : 0;
                    toplamAdet += row["Adet"] != DBNull.Value ? Convert.ToInt32(row["Adet"]) : 0;
                }
                lbl_toplamCiro.Text = toplamCiro.ToString("C2");
                lbl_toplamAdet.Text = toplamAdet.ToString();
            }
        }

        private void GridAyarla()
        {
            dataGridView1.RowTemplate.Height = 40;

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
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Kolon formatlarını ayarla
            if (dataGridView1.Columns.Contains("BirimFiyat"))
                dataGridView1.Columns["BirimFiyat"].DefaultCellStyle.Format = "C2";
            if (dataGridView1.Columns.Contains("Tutar"))
                dataGridView1.Columns["Tutar"].DefaultCellStyle.Format = "C2";
            if (dataGridView1.Columns.Contains("Tarih"))
                dataGridView1.Columns["Tarih"].DefaultCellStyle.Format = "g";

            // Tüm satırların yüksekliğini 40 olarak ayarla
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = 40;
            }

            // İlk satırın otomatik seçilmesini engelle
            dataGridView1.CurrentCell = null;
        }

        private void btnBugun_Click(object sender, EventArgs e)
        {
            dtpBaslangicTarihi.Value = DateTime.Today;
            dtpBitisTarihi.Value = DateTime.Today;
            SatislariYukle();
        }

        private void btnHaftalik_Click(object sender, EventArgs e)
        {
            dtpBaslangicTarihi.Value = DateTime.Today.AddDays(-7);
            dtpBitisTarihi.Value = DateTime.Today;
            SatislariYukle();
        }

        private void btnAylik_Click(object sender, EventArgs e)
        {
            dtpBaslangicTarihi.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpBitisTarihi.Value = DateTime.Today;
            SatislariYukle();
        }

        private void btnRapor_Click(object sender, EventArgs e)
        {
            SatislariYukle();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            dtpBaslangicTarihi.Value = DateTime.Today;
            dtpBitisTarihi.Value = DateTime.Today;
            if (cmbUrunler.Items.Count > 0) cmbUrunler.SelectedIndex = 0;
            if (cmbKasiyer.Items.Count > 0) cmbKasiyer.SelectedIndex = 0;
            SatislariYukle();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
        }
    }
}
