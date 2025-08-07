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
    public partial class AyarlarUrunUserControl : UserControl
    {
        public AyarlarUrunUserControl()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        private int? seciliUrunId = null;

        private void DataGridViewAyarla()
        {
            dataGridView1.RowTemplate.Height = 40;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 11);
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(173, 216, 230);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
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

            // Sil butonunu en sona ekle (varsa tekrar ekleme)
            if (dataGridView1.Columns.Contains("Sil"))
                dataGridView1.Columns.Remove("Sil");
            DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
            btnCol.Name = "Sil";
            btnCol.HeaderText = "Sil";
            btnCol.Text = "Sil";
            btnCol.UseColumnTextForButtonValue = true;
            btnCol.Width = 40;
            btnCol.DefaultCellStyle.BackColor = Color.White;
            btnCol.DefaultCellStyle.ForeColor = Color.Red;
            btnCol.DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dataGridView1.Columns.Add(btnCol);
        }

        private void AyarlarUrunUserControl_Load(object sender, EventArgs e)
        {
            KategorileriYukle();
            DataGridViewAyarla();
            UrunleriYukle();
            dataGridView1.ClearSelection();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            dataGridView1.CellPainting += dataGridView1_CellPainting;
            dataGridView1.CellMouseMove += dataGridView1_CellMouseMove;
        }

        private void KategorileriYukle()
        {
            cmbKategori.Items.Clear();
            using (SqlConnection conn = bgl.baglanti())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT KategoriID, KategoriAdi FROM Kategoriler", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbKategori.DataSource = dt;
                cmbKategori.DisplayMember = "KategoriAdi";
                cmbKategori.ValueMember = "KategoriID";
            }
        }

        private void UrunleriYukle()
        {
            using (SqlConnection conn = bgl.baglanti())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT UrunID, UrunAdi, Fiyat, Resim, KategoriID FROM Urunler WHERE Durum=1", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                if (dataGridView1.Columns.Contains("UrunID"))
                    dataGridView1.Columns["UrunID"].Visible = false;
                if (dataGridView1.Columns.Contains("KategoriID"))
                    dataGridView1.Columns["KategoriID"].Visible = false;
                DataGridViewAyarla();
                // Kolon başlıklarını ayarla
                if (dataGridView1.Columns.Contains("UrunAdi"))
                    dataGridView1.Columns["UrunAdi"].HeaderText = "Ürün Adı";
                if (dataGridView1.Columns.Contains("Fiyat"))
                    dataGridView1.Columns["Fiyat"].HeaderText = "Fiyat";
                if (dataGridView1.Columns.Contains("Sil"))
                    dataGridView1.Columns["Sil"].Width = 60;
                dataGridView1.ClearSelection();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var row = dataGridView1.SelectedRows[0];
                seciliUrunId = Convert.ToInt32(row.Cells["UrunID"].Value);
                txtUrunAdi.Text = row.Cells["UrunAdi"].Value.ToString();
                txtFiyat.Text = row.Cells["Fiyat"].Value.ToString();
                txtUrunGorsel.Text = row.Cells["Resim"].Value.ToString();
                // Kategori seçimi
                foreach (DataRowView item in cmbKategori.Items)
                {
                    if (Convert.ToInt32(item["KategoriID"]) == Convert.ToInt32(row.Cells["KategoriID"].Value))
                    {
                        cmbKategori.SelectedItem = item;
                        break;
                    }
                }
                btnYeniUrunKaydet.Text = "Güncelle";
            }
            else
            {
                seciliUrunId = null;
                txtUrunAdi.Text = "";
                txtFiyat.Text = "";
                txtUrunGorsel.Text = "";
                if (cmbKategori.Items.Count > 0) cmbKategori.SelectedIndex = 0;
                btnYeniUrunKaydet.Text = "Kaydet";
            }
        }

        private void btnYeniUrunKaydet_Click(object sender, EventArgs e)
        {
            string urunAdi = txtUrunAdi.Text.Trim();
            string fiyatStr = txtFiyat.Text.Trim();
            string gorsel = txtUrunGorsel.Text.Trim();
            if (string.IsNullOrEmpty(urunAdi) || string.IsNullOrEmpty(fiyatStr) || cmbKategori.SelectedValue == null)
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(fiyatStr, out decimal fiyat))
            {
                MessageBox.Show("Fiyat alanı geçerli bir sayı olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int kategoriId = Convert.ToInt32(cmbKategori.SelectedValue);
            if (seciliUrunId == null)
            {
                // Yeni ürün ekle
                using (SqlConnection conn = bgl.baglanti())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Urunler (UrunAdi, Fiyat, Resim, KategoriID) VALUES (@ad, @fiyat, @resim, @kategori)", conn);
                    cmd.Parameters.AddWithValue("@ad", urunAdi);
                    cmd.Parameters.AddWithValue("@fiyat", fiyat);
                    cmd.Parameters.AddWithValue("@resim", gorsel);
                    cmd.Parameters.AddWithValue("@kategori", kategoriId);
                    int sonuc = cmd.ExecuteNonQuery();
                    if (sonuc > 0)
                        MessageBox.Show("Ürün başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Ürün eklenemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Ürün güncelle
                using (SqlConnection conn = bgl.baglanti())
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Urunler SET UrunAdi=@ad, Fiyat=@fiyat, Resim=@resim, KategoriID=@kategori WHERE UrunID=@id", conn);
                    cmd.Parameters.AddWithValue("@ad", urunAdi);
                    cmd.Parameters.AddWithValue("@fiyat", fiyat);
                    cmd.Parameters.AddWithValue("@resim", gorsel);
                    cmd.Parameters.AddWithValue("@kategori", kategoriId);
                    cmd.Parameters.AddWithValue("@id", seciliUrunId.Value);
                    int sonuc = cmd.ExecuteNonQuery();
                    if (sonuc > 0)
                        MessageBox.Show("Ürün başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Ürün güncellenemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            UrunleriYukle();
            btnReset_Click(null, null);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            seciliUrunId = null;
            txtUrunAdi.Text = "";
            txtFiyat.Text = "";
            txtUrunGorsel.Text = "";
            if (cmbKategori.Items.Count > 0) cmbKategori.SelectedIndex = 0;
            btnYeniUrunKaydet.Text = "Kaydet";
            dataGridView1.ClearSelection();
        }

        private void btnDosyaSeç_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Ürün Görseli Seç";
                ofd.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string secilenDosya = ofd.FileName;
                    string dosyaAdi = System.IO.Path.GetFileName(secilenDosya);
                    string hedefKlasor = System.IO.Path.Combine(Application.StartupPath, "images");
                    if (!System.IO.Directory.Exists(hedefKlasor))
                        System.IO.Directory.CreateDirectory(hedefKlasor);
                    string hedefYol = System.IO.Path.Combine(hedefKlasor, dosyaAdi);
                    try
                    {
                        System.IO.File.Copy(secilenDosya, hedefYol, true);
                        txtUrunGorsel.Text = "images/" + dosyaAdi;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Görsel kopyalanırken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "Sil")
            {
                var row = dataGridView1.Rows[e.RowIndex];
                string urunAdi = row.Cells["UrunAdi"].Value.ToString();
                int urunId = Convert.ToInt32(row.Cells["UrunID"].Value);
                var result = MessageBox.Show($"'{urunAdi}' adlı ürünü silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = bgl.baglanti())
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE Urunler SET Durum=0 WHERE UrunID=@id", conn);
                        cmd.Parameters.AddWithValue("@id", urunId);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Ürün silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UrunleriYukle();
                    btnReset_Click(null, null);
                }
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Fiyat" && e.Value != null && decimal.TryParse(e.Value.ToString(), out decimal fiyat))
            {
                e.Value = $"₺{fiyat:N0}";
                e.FormattingApplied = true;
            }
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "Sil")
            {
                e.Handled = true;
                e.PaintBackground(e.ClipBounds, true);
                // Ortalanmış kırmızı kalın 'Sil' yazısı
                string text = "Sil";
                Color foreColor = Color.Red;
                Rectangle rect = e.CellBounds;
                rect.Inflate(-2, -2);
                TextRenderer.DrawText(
                    e.Graphics,
                    text,
                    new Font("Segoe UI", 11, FontStyle.Bold),
                    rect,
                    foreColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                );
                // Çerçeve
                e.Paint(e.ClipBounds, DataGridViewPaintParts.Border);
            }
        }

        private void dataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "Sil")
                dataGridView1.Cursor = Cursors.Hand;
            else
                dataGridView1.Cursor = Cursors.Default;
        }
    }
}
