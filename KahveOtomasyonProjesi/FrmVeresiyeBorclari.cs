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

namespace KahveOtomasyonProjesi
{
    public partial class FrmVeresiyeBorclari : Form
    {
        private int _musteriID;
        sqlbaglantisi bgl = new sqlbaglantisi();

        public FrmVeresiyeBorclari(int musteriID)
        {
            InitializeComponent();
            _musteriID = musteriID;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
        }

        private void FrmVeresiyeBorclari_Load(object sender, EventArgs e)
        {
            VeresiyeBorclariniYukle();
            GridAyarla();
            
            // ESC tuşu ile form kapatma
            this.KeyPreview = true;
            this.KeyDown += FrmVeresiyeBorclari_KeyDown;
        }

        private void FrmVeresiyeBorclari_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC tuşuna basıldığında formu kapat
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                e.Handled = true;
            }
        }

        private void VeresiyeBorclariniYukle()
        {
            using (var conn = bgl.baglanti())
            {
                string query = @"
                    SELECT 
                        v.ID AS [VeresiyeID],
                        v.Aciklama AS [Açıklama],
                        s.SiparisID AS [SiparisID],
                        s.TarihSaat AS [TarihSaat],
                        s.ToplamFiyat AS [Borc],
                        ISNULL((SELECT SUM(OdenenTutar) FROM VeresiyeOdemeHareketleri WHERE VeresiyeID = v.ID), 0) AS [Odenen],
                        s.ToplamFiyat - ISNULL((SELECT SUM(OdenenTutar) FROM VeresiyeOdemeHareketleri WHERE VeresiyeID = v.ID), 0) AS [Kalan]
                    FROM Veresiyeler v
                    INNER JOIN Siparisler s ON v.SiparisID = s.SiparisID
                    WHERE v.MusteriID = @MusteriID
                    ORDER BY s.TarihSaat DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@MusteriID", _musteriID);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
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
            //if (dataGridView1.Columns.Contains("VeresiyeID"))
            //    dataGridView1.Columns["VeresiyeID"].Visible = false;
            if (dataGridView1.Columns.Contains("SiparisID"))
                dataGridView1.Columns["SiparisID"].Visible = false;

            // Fiyat kolonunu para formatında göster
            if (dataGridView1.Columns.Contains("Fiyat"))
            {
                dataGridView1.Columns["Fiyat"].DefaultCellStyle.Format = "C2";
                dataGridView1.Columns["Fiyat"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // Tarih kolonunu formatla
            if (dataGridView1.Columns.Contains("TarihSaat"))
            {
                dataGridView1.Columns["TarihSaat"].DefaultCellStyle.Format = "g";
            }

            // Buton kolonlarını ekle
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Detaylar",
                HeaderText = "Detaylar",
                Width = 100,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter },
                HeaderCell = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } }
            });

            // Event'leri bağla
            dataGridView1.CellPainting -= DataGridView1_CellPainting;
            dataGridView1.CellPainting += DataGridView1_CellPainting;
            dataGridView1.CellClick -= DataGridView1_CellClick;
            dataGridView1.CellClick += DataGridView1_CellClick;

            // Tüm satırların yüksekliğini 40 olarak ayarla
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = 40;
            }

            // İlk satırın otomatik seçilmesini engelle
            dataGridView1.CurrentCell = null;
        }

        private void DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
            
            if (columnName == "Detaylar")
            {
                e.Handled = true;
                e.PaintBackground(e.ClipBounds, true);
                string text = "DETAYLAR";
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
            if (e.RowIndex >= 0)
            {
                string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
                int siparisID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["SiparisID"].Value);

                if (columnName == "Detaylar")
                {
                    // Detaylar butonuna tıklandığında sipariş detaylarını göster
                    FrmSiparisDetay detayForm = new FrmSiparisDetay(siparisID);
                    detayForm.ShowDialog();
                }

            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
        }


    }
}
