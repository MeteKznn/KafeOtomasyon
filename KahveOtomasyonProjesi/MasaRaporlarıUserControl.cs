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
    public partial class MasaRaporlarıUserControl : UserControl
    {
        private sqlbaglantisi bgl = new sqlbaglantisi();
        public MasaRaporlarıUserControl()
        {
            InitializeComponent();
            this.Load += MasaRaporlarıUserControl_Load;
            
            // Olay abonelikleri için en güvenli yer yapılandırıcıdır (constructor).
            // Birden fazla çağrılma riskini ortadan kaldırır.
            dataGridView1.CellPainting += DataGridView1_CellPainting;
            dataGridView1.CellClick += DataGridView1_CellClick;
            btnRapor.Click += btnRapor_Click;
            btnTemizle.Click += btnTemizle_Click;
        }

        private void btnRapor_Click(object sender, EventArgs e)
        {
            VerileriListele();
        }

        private void MasaRaporlarıUserControl_Load(object sender, EventArgs e)
        {
            dtpBaslangicTarihi.Value = DateTime.Today;
            dtpBitisTarihi.Value = DateTime.Today.AddDays(1).AddSeconds(-1); // Gün sonu olarak ayarla

            GridAyarlariniUygula();
            VerileriListele();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            dtpBaslangicTarihi.Value = DateTime.Today;
            dtpBitisTarihi.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
            VerileriListele();
        }

        private void GridAyarlariniUygula()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.RowTemplate.Height = 40;

            // Sütunları tanımla
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "MasaAdi", HeaderText = "Masa Adı", DataPropertyName = "MasaAdi", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "ToplamCiro", HeaderText = "Toplam Ciro", DataPropertyName = "ToplamCiro", DefaultCellStyle = { Format = "C2" }, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "AdisyonSayisi", HeaderText = "Müşteri Sayısı (Adisyon)", DataPropertyName = "AdisyonSayisi", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "OrtalamaAdisyonTutar", HeaderText = "Ort. Fiş Tutarı", DataPropertyName = "OrtalamaAdisyonTutar", DefaultCellStyle = { Format = "C2" }, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "EnPopulerUrun", HeaderText = "En Popüler Ürün", DataPropertyName = "EnPopulerUrun", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Detay", HeaderText = "Detay", Width=100 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "MasaID", HeaderText = "MasaID", DataPropertyName = "MasaID", Visible = false });

            // Genel grid ayarları
            //dataGridView1.RowTemplate.Height = 40;
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView1.MultiSelect = false;
            //dataGridView1.AllowUserToAddRows = false;
            //dataGridView1.ReadOnly = true;
            //dataGridView1.AllowUserToResizeColumns = false;
            //dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 11);
            //dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(36, 23, 64);
            //dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            //dataGridView1.DefaultCellStyle.BackColor = Color.White;
            //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);

            //dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //dataGridView1.ColumnHeadersHeight = 40;
            //dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            //dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(36, 23, 64);
            //dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            //dataGridView1.ColumnHeadersDefaultCellStyle.Padding = new Padding(5);

            //dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            //dataGridView1.GridColor = Color.LightGray;
            //dataGridView1.RowHeadersVisible = false;
            //dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            //dataGridView1.BorderStyle = BorderStyle.FixedSingle;

            //dataGridView1.EnableHeadersVisualStyles = false;

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
            try
            {
                using (var conn = bgl.baglanti())
                {
                    StringBuilder query = new StringBuilder();
                    //                    query.Append(@"
                    //WITH SiparisStats AS (
                    //    SELECT
                    //        MasaID,
                    //        SUM(ToplamFiyat) AS ToplamCiro,
                    //        COUNT(SiparisID) AS AdisyonSayisi,
                    //        AVG(ToplamFiyat) AS OrtalamaAdisyonTutar
                    //    FROM Siparisler
                    //    WHERE TarihSaat BETWEEN @p_baslangic AND @p_bitis
                    //    GROUP BY MasaID
                    //),
                    //MasaPopulerUrun AS (
                    //    SELECT
                    //        MasaID, UrunAdi,
                    //        ROW_NUMBER() OVER(PARTITION BY MasaID ORDER BY AdetSum DESC) AS rn
                    //    FROM (
                    //        SELECT s.MasaID, u.UrunAdi, SUM(sd.Adet) as AdetSum
                    //        FROM SiparisDetay sd
                    //        JOIN Urunler u ON sd.UrunID = u.UrunID
                    //        JOIN Siparisler s ON sd.SiparisID = s.SiparisID
                    //        WHERE s.TarihSaat BETWEEN @p_baslangic AND @p_bitis
                    //        GROUP BY s.MasaID, u.UrunAdi
                    //    ) AS UrunCounts
                    //)
                    //SELECT
                    //    m.MasaAdi,
                    //    m.MasaID,
                    //    ISNULL(ss.ToplamCiro, 0) AS ToplamCiro,
                    //    ISNULL(ss.AdisyonSayisi, 0) AS AdisyonSayisi,
                    //    ISNULL(ss.OrtalamaAdisyonTutar, 0) AS OrtalamaAdisyonTutar,
                    //    ISNULL(mpu.UrunAdi, 'Veri Yok') AS EnPopulerUrun
                    //FROM Masalar m
                    //LEFT JOIN SiparisStats ss ON m.MasaID = ss.MasaID
                    //LEFT JOIN MasaPopulerUrun mpu ON m.MasaID = mpu.MasaID AND mpu.rn = 1
                    //ORDER BY m.MasaID;
                    //                        ");
                    query.Append(@"
WITH MasaUrunSatis AS (
    SELECT 
        s.MasaID,
        u.UrunAdi,
        ROW_NUMBER() OVER(PARTITION BY s.MasaID ORDER BY SUM(sd.Adet) DESC) as rn
    FROM SiparisDetay sd
    JOIN Urunler u ON sd.UrunID = u.UrunID
    JOIN Siparisler s ON sd.SiparisID = s.SiparisID
    WHERE s.TarihSaat BETWEEN @p_baslangic AND @p_bitis
    GROUP BY s.MasaID, u.UrunAdi
)
SELECT 
    m.MasaID,
    m.MasaAdi,
    ISNULL(SUM(s.ToplamFiyat), 0) AS ToplamCiro,
    COUNT(s.SiparisID) AS AdisyonSayisi,
    ISNULL(AVG(s.ToplamFiyat), 0) AS OrtalamaAdisyonTutar,
    ISNULL(MAX(CASE WHEN mus.rn = 1 THEN mus.UrunAdi END), 'Veri Yok') AS EnPopulerUrun
FROM Masalar m
LEFT JOIN Siparisler s ON m.MasaID = s.MasaID AND s.TarihSaat BETWEEN @p_baslangic AND @p_bitis
LEFT JOIN MasaUrunSatis mus ON m.MasaID = mus.MasaID AND mus.rn = 1
GROUP BY m.MasaID, m.MasaAdi
ORDER BY ToplamCiro DESC;
");

                    SqlDataAdapter da = new SqlDataAdapter(query.ToString(), conn);
                    da.SelectCommand.Parameters.AddWithValue("@p_baslangic", dtpBaslangicTarihi.Value);
                    da.SelectCommand.Parameters.AddWithValue("@p_bitis", dtpBitisTarihi.Value);

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                    // Label'lara özet verileri yaz
                    // Label'lara özet verileri yaz
                    if (dt.Rows.Count > 0)
                    {
                        string enCokKazandiranMasa = dt.Rows[0]["MasaAdi"].ToString();
                        decimal toplamCiro = dt.AsEnumerable().Sum(r => r["ToplamCiro"] != DBNull.Value ? Convert.ToDecimal(r["ToplamCiro"]) : 0);
                        int toplamAdisyon = dt.AsEnumerable().Sum(r => r["AdisyonSayisi"] != DBNull.Value ? Convert.ToInt32(r["AdisyonSayisi"]) : 0);

                        lbl_encokKazandiranMasa.Text = enCokKazandiranMasa;
                        lbl_toplamCiro.Text = toplamCiro.ToString("C2");
                        lbl_toplamAdisyon.Text = toplamAdisyon.ToString();
                    }
                    else
                    {
                        lbl_encokKazandiranMasa.Text = "-";
                        lbl_toplamCiro.Text = "₺0,00";
                        lbl_toplamAdisyon.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Rapor verileri yüklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (columnName == "Detay")
            {
                e.Handled = true;
                e.PaintBackground(e.ClipBounds, true);
                string text = "İNCELE";
                bool isSelected = dataGridView1.Rows[e.RowIndex].Selected;
                Color foreColor = isSelected ? Color.White : Color.FromArgb(36, 23, 64);

                // Buton görünümü için bir dikdörtgen çizelim
                Rectangle rect = e.CellBounds;
                rect.Inflate(-4, -4);
                //ControlPaint.DrawBorder(e.Graphics, rect, SystemColors.ControlDark, ButtonBorderStyle.Solid);
                
                TextRenderer.DrawText(
                    e.Graphics,
                    text,
                    new Font("Segoe UI", 10, FontStyle.Bold),
                    e.CellBounds,
                    foreColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                );
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "Detay")
            {
                int masaId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["MasaID"].Value);
                DateTime baslangic = dtpBaslangicTarihi.Value;
                DateTime bitis = dtpBitisTarihi.Value;

                FrmMasaRaporuDetayi frm = new FrmMasaRaporuDetayi(masaId, baslangic, bitis);
                frm.ShowDialog();
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
        }
    }
}
