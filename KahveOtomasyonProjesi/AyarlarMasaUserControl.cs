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
    public partial class AyarlarMasaUserControl : UserControl
    {
        public AyarlarMasaUserControl()
        {
            InitializeComponent();
            this.Load += AyarlarMasaUserControl_Load;
            btnMasaEkle.Click += btnMasaEkle_Click;
            btnMasaGuncelle.Click += btnMasaGuncelle_Click;
            dgvMasalar.CellContentClick += dgvMasalar_CellContentClick;
            dgvMasalar.SelectionChanged += dgvMasalar_SelectionChanged;
            
            // Bilardo masaları için event handler'lar
            btnBilardoEkle.Click += btnBilardoEkle_Click;
            btnBilardoGuncelle.Click += btnBilardoGuncelle_Click;
            dgvBilardoMasalar.CellContentClick += dgvBilardoMasalar_CellContentClick;
            dgvBilardoMasalar.SelectionChanged += dgvBilardoMasalar_SelectionChanged;
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        private void AyarlarMasaUserControl_Load(object sender, EventArgs e)
        {
            MasalariYukle();
            BilardoMasalariniYukle();
        }

        private void MasalariYukle()
        {
            using (SqlConnection conn = bgl.baglanti())
            {
                // Normal masaları getir (Bilardo ile başlamayanlar)
                SqlDataAdapter da = new SqlDataAdapter("SELECT MasaID, MasaAdi FROM Masalar WHERE UPPER(MasaAdi) NOT LIKE 'BİLARDO%' AND UPPER(MasaAdi) NOT LIKE 'BILARDO%'", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvMasalar.DataSource = dt;
                if (dgvMasalar.Columns.Contains("MasaID"))
                    dgvMasalar.Columns["MasaID"].Visible = false;
                if (dgvMasalar.Columns.Contains("Sil"))
                    dgvMasalar.Columns.Remove("Sil");
                DataGridViewButtonColumn silCol = new DataGridViewButtonColumn();
                silCol.Name = "Sil";
                silCol.HeaderText = "Sil";
                silCol.Text = "Sil";
                silCol.UseColumnTextForButtonValue = true;
                silCol.Width = 60;
                silCol.DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                silCol.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Bold);
                dgvMasalar.Columns.Add(silCol);
                // Görsel ayarlar
                dgvMasalar.RowTemplate.Height = 36;
                dgvMasalar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvMasalar.MultiSelect = false;
                dgvMasalar.AllowUserToAddRows = false;
                dgvMasalar.ReadOnly = true;
                dgvMasalar.AllowUserToResizeColumns = false;
                dgvMasalar.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 11);
                dgvMasalar.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvMasalar.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(173, 216, 230);
                dgvMasalar.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
                dgvMasalar.DefaultCellStyle.BackColor = System.Drawing.Color.White;
                dgvMasalar.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                dgvMasalar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dgvMasalar.ColumnHeadersHeight = 36;
                dgvMasalar.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold);
                dgvMasalar.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvMasalar.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(36, 23, 64);
                dgvMasalar.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
                dgvMasalar.ColumnHeadersDefaultCellStyle.Padding = new System.Windows.Forms.Padding(5);
                dgvMasalar.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                dgvMasalar.GridColor = System.Drawing.Color.LightGray;
                dgvMasalar.RowHeadersVisible = false;
                dgvMasalar.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                dgvMasalar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                dgvMasalar.EnableHeadersVisualStyles = false;
                dgvMasalar.ColumnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(36, 23, 64);
                dgvMasalar.ColumnHeadersDefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
                dgvMasalar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvMasalar.ClearSelection();
            }
        }

        private void btnMasaEkle_Click(object sender, EventArgs e)
        {
            string masaAdi = txtMasaAdi.Text.Trim();
            if (string.IsNullOrEmpty(masaAdi))
            {
                MessageBox.Show("Lütfen masa adını girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Masa adının zaten kayıtlı olup olmadığını kontrol et
            using (SqlConnection conn = bgl.baglanti())
            {
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Masalar WHERE MasaAdi = @ad", conn);
                checkCmd.Parameters.AddWithValue("@ad", masaAdi);
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                
                if (count > 0)
                {
                    MessageBox.Show($"'{masaAdi}' adlı masa zaten kayıtlıdır. Lütfen farklı bir masa adı girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Masa adı kayıtlı değilse ekle
                SqlCommand cmd = new SqlCommand("INSERT INTO Masalar (MasaAdi, Durum) VALUES (@ad, 0)", conn);
                cmd.Parameters.AddWithValue("@ad", masaAdi);
                cmd.ExecuteNonQuery();
            }
            
            txtMasaAdi.Text = "";
            MasalariYukle();
            MessageBox.Show($"'{masaAdi}' adlı masa başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnMasaGuncelle_Click(object sender, EventArgs e)
        {
            if (dgvMasalar.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen güncellenecek bir masa seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string yeniAd = txtMasaAdi.Text.Trim();
            if (string.IsNullOrEmpty(yeniAd))
            {
                MessageBox.Show("Lütfen yeni masa adını girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            int masaId = Convert.ToInt32(dgvMasalar.SelectedRows[0].Cells["MasaID"].Value);
            string eskiAd = dgvMasalar.SelectedRows[0].Cells["MasaAdi"].Value.ToString();
            
            // Eğer yeni ad eski adla aynıysa güncelleme yapmaya gerek yok
            if (yeniAd == eskiAd)
            {
                MessageBox.Show("Masa adı değişmedi. Güncelleme yapılmadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            // Yeni masa adının başka bir masada kullanılıp kullanılmadığını kontrol et
            using (SqlConnection conn = bgl.baglanti())
            {
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Masalar WHERE MasaAdi = @ad AND MasaID != @id", conn);
                checkCmd.Parameters.AddWithValue("@ad", yeniAd);
                checkCmd.Parameters.AddWithValue("@id", masaId);
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                
                if (count > 0)
                {
                    MessageBox.Show($"'{yeniAd}' adlı masa zaten kayıtlıdır. Lütfen farklı bir masa adı girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Masa adı kayıtlı değilse güncelle
                SqlCommand cmd = new SqlCommand("UPDATE Masalar SET MasaAdi=@ad WHERE MasaID=@id", conn);
                cmd.Parameters.AddWithValue("@ad", yeniAd);
                cmd.Parameters.AddWithValue("@id", masaId);
                cmd.ExecuteNonQuery();
            }
            
            txtMasaAdi.Text = "";
            MasalariYukle();
            MessageBox.Show($"'{eskiAd}' adlı masa başarıyla '{yeniAd}' olarak güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvMasalar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvMasalar.Columns[e.ColumnIndex].Name == "Sil")
            {
                var row = dgvMasalar.Rows[e.RowIndex];
                string masaAdi = row.Cells["MasaAdi"].Value.ToString();
                int masaId = Convert.ToInt32(row.Cells["MasaID"].Value);
                var result = MessageBox.Show($"'{masaAdi}' adlı masayı silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = bgl.baglanti())
                    {
                        SqlCommand cmd = new SqlCommand("DELETE FROM Masalar WHERE MasaID=@id", conn);
                        cmd.Parameters.AddWithValue("@id", masaId);
                        cmd.ExecuteNonQuery();
                    }
                    MasalariYukle();
                }
            }
        }

        private void dgvMasalar_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMasalar.SelectedRows.Count > 0)
            {
                txtMasaAdi.Text = dgvMasalar.SelectedRows[0].Cells["MasaAdi"].Value.ToString();
            }
        }

        // Bilardo Masaları İşlemleri
        private void BilardoMasalariniYukle()
        {
            using (SqlConnection conn = bgl.baglanti())
            {
                // Masa adının başında "Bilardo" olan masaları getir (büyük/küçük harf duyarsız)
                SqlDataAdapter da = new SqlDataAdapter("SELECT MasaID, MasaAdi FROM Masalar WHERE UPPER(MasaAdi) LIKE 'BİLARDO%' OR UPPER(MasaAdi) LIKE 'BILARDO%'", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvBilardoMasalar.DataSource = dt;
                if (dgvBilardoMasalar.Columns.Contains("MasaID"))
                    dgvBilardoMasalar.Columns["MasaID"].Visible = false;
                if (dgvBilardoMasalar.Columns.Contains("Sil"))
                    dgvBilardoMasalar.Columns.Remove("Sil");
                DataGridViewButtonColumn silCol = new DataGridViewButtonColumn();
                silCol.Name = "Sil";
                silCol.HeaderText = "Sil";
                silCol.Text = "Sil";
                silCol.UseColumnTextForButtonValue = true;
                silCol.Width = 60;
                silCol.DefaultCellStyle.ForeColor = System.Drawing.Color.Red;
                silCol.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 11, FontStyle.Bold);
                dgvBilardoMasalar.Columns.Add(silCol);
                
                // Görsel ayarlar
                dgvBilardoMasalar.RowTemplate.Height = 36;
                dgvBilardoMasalar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvBilardoMasalar.MultiSelect = false;
                dgvBilardoMasalar.AllowUserToAddRows = false;
                dgvBilardoMasalar.ReadOnly = true;
                dgvBilardoMasalar.AllowUserToResizeColumns = false;
                dgvBilardoMasalar.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 11);
                dgvBilardoMasalar.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvBilardoMasalar.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(173, 216, 230);
                dgvBilardoMasalar.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
                dgvBilardoMasalar.DefaultCellStyle.BackColor = System.Drawing.Color.White;
                dgvBilardoMasalar.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
                dgvBilardoMasalar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dgvBilardoMasalar.ColumnHeadersHeight = 36;
                dgvBilardoMasalar.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold);
                dgvBilardoMasalar.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvBilardoMasalar.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(36, 23, 64);
                dgvBilardoMasalar.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
                dgvBilardoMasalar.ColumnHeadersDefaultCellStyle.Padding = new System.Windows.Forms.Padding(5);
                dgvBilardoMasalar.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                dgvBilardoMasalar.GridColor = System.Drawing.Color.LightGray;
                dgvBilardoMasalar.RowHeadersVisible = false;
                dgvBilardoMasalar.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                dgvBilardoMasalar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                dgvBilardoMasalar.EnableHeadersVisualStyles = false;
                dgvBilardoMasalar.ColumnHeadersDefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(36, 23, 64);
                dgvBilardoMasalar.ColumnHeadersDefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
                dgvBilardoMasalar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvBilardoMasalar.ClearSelection();
            }
        }

        private void btnBilardoEkle_Click(object sender, EventArgs e)
        {
            string bilardoMasaAdi = txtBilardoMasaAdi.Text.Trim();
            if (string.IsNullOrEmpty(bilardoMasaAdi))
            {
                MessageBox.Show("Lütfen bilardo masa adını girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Bilardo masa adının zaten kayıtlı olup olmadığını kontrol et
            using (SqlConnection conn = bgl.baglanti())
            {
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Masalar WHERE MasaAdi = @ad", conn);
                checkCmd.Parameters.AddWithValue("@ad", bilardoMasaAdi);
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                
                if (count > 0)
                {
                    MessageBox.Show($"'{bilardoMasaAdi}' adlı bilardo masası zaten kayıtlıdır. Lütfen farklı bir masa adı girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Bilardo masa adı kayıtlı değilse ekle
                SqlCommand cmd = new SqlCommand("INSERT INTO Masalar (MasaAdi, Durum) VALUES (@ad, 0)", conn);
                cmd.Parameters.AddWithValue("@ad", bilardoMasaAdi);
                cmd.ExecuteNonQuery();
            }
            
            txtBilardoMasaAdi.Text = "";
            BilardoMasalariniYukle();
            MessageBox.Show($"'{bilardoMasaAdi}' adlı bilardo masası başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBilardoGuncelle_Click(object sender, EventArgs e)
        {
            if (dgvBilardoMasalar.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen güncellenecek bir bilardo masası seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string yeniAd = txtBilardoMasaAdi.Text.Trim();
            if (string.IsNullOrEmpty(yeniAd))
            {
                MessageBox.Show("Lütfen yeni bilardo masa adını girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            int masaId = Convert.ToInt32(dgvBilardoMasalar.SelectedRows[0].Cells["MasaID"].Value);
            string eskiAd = dgvBilardoMasalar.SelectedRows[0].Cells["MasaAdi"].Value.ToString();
            
            // Eğer yeni ad eski adla aynıysa güncelleme yapmaya gerek yok
            if (yeniAd == eskiAd)
            {
                MessageBox.Show("Bilardo masa adı değişmedi. Güncelleme yapılmadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            // Yeni bilardo masa adının başka bir masada kullanılıp kullanılmadığını kontrol et
            using (SqlConnection conn = bgl.baglanti())
            {
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Masalar WHERE MasaAdi = @ad AND MasaID != @id", conn);
                checkCmd.Parameters.AddWithValue("@ad", yeniAd);
                checkCmd.Parameters.AddWithValue("@id", masaId);
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                
                if (count > 0)
                {
                    MessageBox.Show($"'{yeniAd}' adlı bilardo masası zaten kayıtlıdır. Lütfen farklı bir masa adı girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Bilardo masa adı kayıtlı değilse güncelle
                SqlCommand cmd = new SqlCommand("UPDATE Masalar SET MasaAdi=@ad WHERE MasaID=@id", conn);
                cmd.Parameters.AddWithValue("@ad", yeniAd);
                cmd.Parameters.AddWithValue("@id", masaId);
                cmd.ExecuteNonQuery();
            }
            
            txtBilardoMasaAdi.Text = "";
            BilardoMasalariniYukle();
            MessageBox.Show($"'{eskiAd}' adlı bilardo masası başarıyla '{yeniAd}' olarak güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvBilardoMasalar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvBilardoMasalar.Columns[e.ColumnIndex].Name == "Sil")
            {
                var row = dgvBilardoMasalar.Rows[e.RowIndex];
                string masaAdi = row.Cells["MasaAdi"].Value.ToString();
                int masaId = Convert.ToInt32(row.Cells["MasaID"].Value);
                var result = MessageBox.Show($"'{masaAdi}' adlı bilardo masasını silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = bgl.baglanti())
                    {
                        SqlCommand cmd = new SqlCommand("DELETE FROM Masalar WHERE MasaID=@id", conn);
                        cmd.Parameters.AddWithValue("@id", masaId);
                        cmd.ExecuteNonQuery();
                    }
                    BilardoMasalariniYukle();
                    MessageBox.Show($"'{masaAdi}' adlı bilardo masası başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void dgvBilardoMasalar_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBilardoMasalar.SelectedRows.Count > 0)
            {
                txtBilardoMasaAdi.Text = dgvBilardoMasalar.SelectedRows[0].Cells["MasaAdi"].Value.ToString();
            }
        }
    }
}
