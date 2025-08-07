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
    public partial class FrmMusteriVeresiyeHareketleri : Form
    {
        public FrmMusteriVeresiyeHareketleri()
        {
            InitializeComponent();
        }

        public int MusteriID { get; set; } // Form dışından atanacak

        private void FrmMusteriVeresiyeHareketleri_Load(object sender, EventArgs e)
        {
            if (MusteriID > 0)
                MusteriVeresiyeHareketleriniYukle(MusteriID);
            GridAyarla();
            
            // ESC tuşu ile form kapatma
            this.KeyPreview = true;
            this.KeyDown += FrmMusteriVeresiyeHareketleri_KeyDown;
        }

        private void FrmMusteriVeresiyeHareketleri_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC tuşuna basıldığında formu kapat
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                e.Handled = true;
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

            // Gizlenecek kolonlar
            if (dataGridView1.Columns.Contains("ID"))
                dataGridView1.Columns["ID"].Visible = false;

            // Tutar kolonunu para formatında göster
            if (dataGridView1.Columns.Contains("OdenenTutar"))
            {
                dataGridView1.Columns["OdenenTutar"].DefaultCellStyle.Format = "C2";
                dataGridView1.Columns["OdenenTutar"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // Tarih kolonunu formatla
            if (dataGridView1.Columns.Contains("OdemeTarihi"))
            {
                dataGridView1.Columns["OdemeTarihi"].DefaultCellStyle.Format = "g";
            }

            // Tüm satırların yüksekliğini 40 olarak ayarla
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = 40;
            }

            // İlk satırın otomatik seçilmesini engelle
            dataGridView1.CurrentCell = null;
        }

        private void MusteriVeresiyeHareketleriniYukle(int musteriID)
        {
            using (var conn = new sqlbaglantisi().baglanti())
            {
                string query = @"
            SELECT 
                voh.ID,
                voh.VeresiyeID,
                v.Aciklama AS Açıklama,
                voh.OdenenTutar,
                voh.OdemeTarihi
            FROM VeresiyeOdemeHareketleri voh
            INNER JOIN Veresiyeler v ON voh.VeresiyeID = v.ID
            WHERE v.MusteriID = @MusteriID
            ORDER BY voh.OdemeTarihi DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@MusteriID", musteriID);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }
    }
}
