using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KahveOtomasyonProjesi
{
    public partial class MasaRaporlariUserControl : UserControl
    {
        private sqlbaglantisi bgl = new sqlbaglantisi();

        public MasaRaporlariUserControl()
        {
            InitializeComponent();
            this.Load += MasaRaporlariUserControl_Load;
        }

        private void MasaRaporlariUserControl_Load(object sender, EventArgs e)
        {
            dtpBaslangicTarihi.Value = DateTime.Today;
            dtpBitisTarihi.Value = DateTime.Today.AddDays(1).AddSeconds(-1); // Gün sonu olarak ayarla

            GridAyarlariniUygula();
            VerileriListele();

            btnRapor.Click += btnRapor_Click;
            btnTemizle.Click += btnTemizle_Click;
        }

        private void GridAyarlariniUygula()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();
            
            // Sütunları tanımla
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "MasaAdi", HeaderText = "Masa Adı", DataPropertyName = "MasaAdi", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "ToplamCiro", HeaderText = "Toplam Ciro", DataPropertyName = "ToplamCiro", DefaultCellStyle = { Format = "C2" }, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "AdisyonSayisi", HeaderText = "Adisyon Sayısı", DataPropertyName = "AdisyonSayisi", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "OrtalamaAdisyonTutar", HeaderText = "Ort. Adisyon Tutarı", DataPropertyName = "OrtalamaAdisyonTutar", DefaultCellStyle = { Format = "C2" }, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "EnPopulerUrun", HeaderText = "En Popüler Ürün", DataPropertyName = "EnPopulerUrun", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            
            // Genel grid ayarları
            dataGridView1.RowTemplate.Height = 40;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 11);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersHeight = 40;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(36, 23, 64);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void VerileriListele()
        {
            try
            {
                using (var conn = bgl.baglanti())
                {
                    StringBuilder query = new StringBuilder();
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
    m.MasaAdi,
    ISNULL(SUM(s.ToplamFiyat), 0) AS ToplamCiro,
    COUNT(s.SiparisID) AS AdisyonSayisi,
    ISNULL(AVG(s.ToplamFiyat), 0) AS OrtalamaAdisyonTutar,
    ISNULL(MAX(CASE WHEN mus.rn = 1 THEN mus.UrunAdi END), 'Veri Yok') AS EnPopulerUrun
FROM Masalar m
LEFT JOIN Siparisler s ON m.MasaID = s.MasaID AND s.TarihSaat BETWEEN @p_baslangic AND @p_bitis
LEFT JOIN MasaUrunSatis mus ON m.MasaID = mus.MasaID AND mus.rn = 1
GROUP BY m.MasaAdi
ORDER BY TRY_CAST(SUBSTRING(m.MasaAdi, 6, LEN(m.MasaAdi)) AS INT) ASC;
");

                    SqlDataAdapter da = new SqlDataAdapter(query.ToString(), conn);
                    da.SelectCommand.Parameters.AddWithValue("@p_baslangic", dtpBaslangicTarihi.Value);
                    da.SelectCommand.Parameters.AddWithValue("@p_bitis", dtpBitisTarihi.Value);

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                    // Label'lara özet verileri yaz
                    if (dt.Rows.Count > 0)
                    {
                        // En çok kazandıran masa (ToplamCiro'ya göre)
                        var maxRow = dt.AsEnumerable().OrderByDescending(r => r["ToplamCiro"] != DBNull.Value ? Convert.ToDecimal(r["ToplamCiro"]) : 0).First();
                        string enCokKazandiranMasa = maxRow["MasaAdi"].ToString();
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

        private void btnRapor_Click(object sender, EventArgs e)
        {
            VerileriListele();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            dtpBaslangicTarihi.Value = DateTime.Today;
            dtpBitisTarihi.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
            VerileriListele();
        }
    }
} 