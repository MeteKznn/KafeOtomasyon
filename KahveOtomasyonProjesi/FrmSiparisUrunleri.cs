using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KahveOtomasyonProjesi
{
    public partial class FrmSiparisUrunleri : Form
    {
        private sqlbaglantisi bgl = new sqlbaglantisi();
        private int siparisId;

        public FrmSiparisUrunleri(int siparisId)
        {
            InitializeComponent();
            this.siparisId = siparisId;
        }

        private void GridAyarlariniUygula()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.RowTemplate.Height = 40;

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "UrunAdi", HeaderText = "Ürün Adı", DataPropertyName = "UrunAdi", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "Adet", HeaderText = "Adet", DataPropertyName = "Adet", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "BirimFiyat", HeaderText = "Birim Fiyat", DataPropertyName = "BirimFiyat", DefaultCellStyle = { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleCenter }, AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { Name = "ToplamTutar", HeaderText = "Toplam Tutar", DataPropertyName = "ToplamTutar", DefaultCellStyle = { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleCenter }, AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells });

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
                            u.UrunAdi,
                            sd.Adet,
                            sd.Fiyat AS BirimFiyat,
                            (sd.Adet * sd.Fiyat) AS ToplamTutar
                        FROM SiparisDetay sd
                        JOIN Urunler u ON sd.UrunID = u.UrunID
                        WHERE sd.SiparisID = @p_siparisId
                        ORDER BY u.UrunAdi;
                    ");

                    SqlDataAdapter da = new SqlDataAdapter(query.ToString(), conn);
                    da.SelectCommand.Parameters.AddWithValue("@p_siparisId", this.siparisId);

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sipariş ürünleri yüklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmSiparisUrunleri_Load(object sender, EventArgs e)
        {
            GridAyarlariniUygula();
            VerileriListele();
            
            // ESC tuşu ile form kapatma
            this.KeyPreview = true;
            this.KeyDown += FrmSiparisUrunleri_KeyDown;
        }

        private void FrmSiparisUrunleri_KeyDown(object sender, KeyEventArgs e)
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