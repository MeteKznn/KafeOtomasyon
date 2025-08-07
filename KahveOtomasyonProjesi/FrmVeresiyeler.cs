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
    public partial class FrmVeresiyeler : Form
    {
        public FrmVeresiyeler()
        {
            InitializeComponent();
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            chckBorcsuz.CheckedChanged += chckBorcsuz_CheckedChanged;
            chckBinUstu.CheckedChanged += chckBinUstu_CheckedChanged;
            chckBinUstuVurgulama.CheckedChanged += chckBinUstuVurgulama_CheckedChanged;
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        private void FrmVeresiyeler_Load(object sender, EventArgs e)
        {
            VeresiyeleriYukle();
            GridAyarla();
            
            // ESC tuşu ile form kapatma
            this.KeyPreview = true;
            this.KeyDown += FrmVeresiyeler_KeyDown;
        }

        private void FrmVeresiyeler_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC tuşuna basıldığında formu kapat
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                e.Handled = true;
            }
        }

        private void VeresiyeleriYukle()
        {
            using (var conn = bgl.baglanti())
            {
                string query;
                if (chckBorcsuz.Checked)
                {
                    query = @"
                    SELECT 
                        m.ID AS [MusteriID],
                        m.MusteriAd AS [Müşteri Adı],
                        m.MusteriSoyad AS [Müşteri Soyad],
                        m.MusteriTelefon AS [Müşteri Telefon],
                        m.ToplamBorc AS [Toplam Borç]
                    FROM Musteriler m
                    ORDER BY m.ToplamBorc DESC";
                }
                else if (chckBinUstu.Checked)
                {
                    query = @"
                    SELECT 
                        m.ID AS [MusteriID],
                        m.MusteriAd AS [Müşteri Adı],
                        m.MusteriSoyad AS [Müşteri Soyad],
                        m.MusteriTelefon AS [Müşteri Telefon],
                        m.ToplamBorc AS [Toplam Borç]
                    FROM Musteriler m
                    WHERE m.ToplamBorc >= 1000
                    ORDER BY m.ToplamBorc DESC";
                }
                else
                {
                    query = @"
                    SELECT 
                        m.ID AS [MusteriID],
                        m.MusteriAd AS [Müşteri Adı],
                        m.MusteriSoyad AS [Müşteri Soyad],
                        m.MusteriTelefon AS [Müşteri Telefon],
                        m.ToplamBorc AS [Toplam Borç]
                    FROM Musteriler m
                    WHERE m.ToplamBorc > 0
                    ORDER BY m.ToplamBorc DESC";
                }
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            if (chckBinUstuVurgulama.Checked)
                VurgulaBinUstuSatirlar();
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
            if (dataGridView1.Columns.Contains("MusteriID"))
                dataGridView1.Columns["MusteriID"].Visible = false;
            if (dataGridView1.Columns.Contains("Veresiye Sayısı"))
                dataGridView1.Columns["Veresiye Sayısı"].Visible = false;

            // Toplam Borç kolonunu para formatında göster
            if (dataGridView1.Columns.Contains("Toplam Borç"))
            {
                dataGridView1.Columns["Toplam Borç"].DefaultCellStyle.Format = "C2";
                dataGridView1.Columns["Toplam Borç"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            // Buton kolonlarını ekle
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Detay",
                HeaderText = "Detay",
                Width = 80,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter },
                HeaderCell = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } }
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "OdemeAl",
                HeaderText = "Ödeme Al",
                Width = 100,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter },
                HeaderCell = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } }
            });

            // GridAyarla metodunda buton kolonunu ekle
            if (!dataGridView1.Columns.Contains("Odemeler"))
            {
                var odemelerCol = new DataGridViewTextBoxColumn
                {
                    Name = "Odemeler",
                    HeaderText = "Ödemeler",
                    Width = 100,
                    DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter },
                    HeaderCell = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } }
                };
                dataGridView1.Columns.Add(odemelerCol);
            }

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

            if (columnName == "Detay")
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
            else if (columnName == "OdemeAl")
            {
                e.Handled = true;
                e.PaintBackground(e.ClipBounds, true);
                string text = "ÖDEME AL";
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
            // DataGridView1_CellPainting eventinde butonun görünümünü ayarla
            else if (columnName == "Odemeler")
            {
                e.Handled = true;
                e.PaintBackground(e.ClipBounds, true);
                string text = "ÖDEMELER";
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
                int musteriID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["MusteriID"].Value);

                if (columnName == "Detay")
                {
                    // Detay butonuna tıklandığında FrmVeresiyeBorclari formunu aç
                    FrmVeresiyeBorclari borclarForm = new FrmVeresiyeBorclari(musteriID);
                    borclarForm.ShowDialog();
                }
                else if (columnName == "OdemeAl")
                {
                    // Ödeme Al butonuna tıklandığında ödeme alma işlemi
                    MusteriOdemeAl(musteriID);
                }
                // DataGridView1_CellClick eventinde butona tıklanınca formu aç
                else if (columnName == "Odemeler")
                {
                    FrmMusteriVeresiyeHareketleri hareketForm = new FrmMusteriVeresiyeHareketleri();
                    hareketForm.MusteriID = musteriID;
                    hareketForm.ShowDialog();
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
            //dataGridView1.CurrentCell = null;
        }

        private void MusteriOdemeAl(int musteriID)
        {
            try
            {
                using (var conn = bgl.baglanti())
                {
                    // 1. Müşterinin toplam borcunu doğrudan Musteriler tablosundan al
                    string borcQuery = @"SELECT ToplamBorc FROM Musteriler WHERE ID = @MusteriID";
                    SqlCommand borcCmd = new SqlCommand(borcQuery, conn);
                    borcCmd.Parameters.AddWithValue("@MusteriID", musteriID);
                    decimal toplamBorc = Convert.ToDecimal(borcCmd.ExecuteScalar());

                    if (toplamBorc <= 0)
                    {
                        MessageBox.Show("Bu müşterinin borcu bulunmamaktadır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // 2. Müşteri adı
                    string musteriAdi = "";
                    using (SqlCommand musteriCmd = new SqlCommand("SELECT MusteriAd, MusteriSoyad FROM Musteriler WHERE ID = @MusteriID", conn))
                    {
                        musteriCmd.Parameters.AddWithValue("@MusteriID", musteriID);
                        using (SqlDataReader reader = musteriCmd.ExecuteReader())
                        {
                            if (reader.Read())
                                musteriAdi = $"{reader["MusteriAd"]} {reader["MusteriSoyad"]}";
                        }
                    }

                    // 3. Ödeme miktarını al
                    InputBox inputBox = new InputBox("Ödeme Miktarı", $"{musteriAdi}\nToplam Borç: {toplamBorc:C2}\nÖdenecek miktarı giriniz:", toplamBorc);
                    if (inputBox.ShowDialog() != DialogResult.OK)
                        return;

                    decimal odenenMiktar = inputBox.Amount;
                    if (odenenMiktar > toplamBorc)
                    {
                        MessageBox.Show("Ödenen miktar toplam borçtan fazla olamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // 4. Müşterinin en eski ödenmemiş veresiyelerini çek
                    string veresiyeQuery = @"
                        SELECT v.ID AS VeresiyeID, s.ToplamFiyat AS Borc, 
                            ISNULL((SELECT SUM(OdenenTutar) FROM VeresiyeOdemeHareketleri WHERE VeresiyeID = v.ID), 0) AS Odenen
                        FROM Veresiyeler v
                        INNER JOIN Siparisler s ON v.SiparisID = s.SiparisID
                        WHERE v.MusteriID = @MusteriID
                        ORDER BY s.TarihSaat ASC";
                    SqlCommand veresiyeCmd = new SqlCommand(veresiyeQuery, conn);
                    veresiyeCmd.Parameters.AddWithValue("@MusteriID", musteriID);

                    List<(int VeresiyeID, decimal KalanBorc)> veresiyeList = new List<(int, decimal)>();
                    using (SqlDataReader dr = veresiyeCmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            int veresiyeID = Convert.ToInt32(dr["VeresiyeID"]);
                            decimal borc = Convert.ToDecimal(dr["Borc"]);
                            decimal odenen = Convert.ToDecimal(dr["Odenen"]);
                            decimal kalan = borc - odenen;
                            if (kalan > 0)
                                veresiyeList.Add((veresiyeID, kalan));
                        }
                    }

                    decimal kalanOdeme = odenenMiktar;
                    foreach (var veresiye in veresiyeList)
                    {
                        if (kalanOdeme <= 0) break;

                        decimal odeme = Math.Min(veresiye.KalanBorc, kalanOdeme);

                        // 5. VeresiyeOdemeHareketleri tablosuna kayıt ekle
                        SqlCommand odemeCmd = new SqlCommand(
                            "INSERT INTO VeresiyeOdemeHareketleri (VeresiyeID, OdenenTutar, OdemeTarihi) VALUES (@VeresiyeID, @OdenenTutar, GETDATE())", conn);
                        odemeCmd.Parameters.AddWithValue("@VeresiyeID", veresiye.VeresiyeID);
                        odemeCmd.Parameters.AddWithValue("@OdenenTutar", odeme);
                        odemeCmd.ExecuteNonQuery();

                        // Eğer bu ödeme ile birlikte bu veresiye tamamen kapandıysa Durum = 1 yap
                        if (odeme == veresiye.KalanBorc && kalanOdeme >= odeme)
                        {
                            SqlCommand durumCmd = new SqlCommand(
                                "UPDATE Veresiyeler SET Durum = 1 WHERE ID = @VeresiyeID", conn);
                            durumCmd.Parameters.AddWithValue("@VeresiyeID", veresiye.VeresiyeID);
                            durumCmd.ExecuteNonQuery();
                        }
                        kalanOdeme -= odeme;
                    }

                    // 6. Müşterinin ToplamBorc alanını güncelle
                    SqlCommand musteriBorcCmd = new SqlCommand(
                        "UPDATE Musteriler SET ToplamBorc = ToplamBorc - @OdenenMiktar WHERE ID = @MusteriID", conn);
                    musteriBorcCmd.Parameters.AddWithValue("@OdenenMiktar", odenenMiktar);
                    musteriBorcCmd.Parameters.AddWithValue("@MusteriID", musteriID);
                    musteriBorcCmd.ExecuteNonQuery();

                    MessageBox.Show("Ödeme başarıyla kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    VeresiyeleriYukle();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ödeme alma işlemi sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chckBorcsuz_CheckedChanged(object sender, EventArgs e)
        {
            if (chckBorcsuz.Checked)
                chckBinUstu.Checked = false;
            VeresiyeleriYukle();
        }

        private void chckBinUstu_CheckedChanged(object sender, EventArgs e)
        {
            // Diğer filtrelerle çakışmaması için gerekirse diğer checkbox'ları kapatabilirsin
            if (chckBinUstu.Checked)
                chckBorcsuz.Checked = false;
            VeresiyeleriYukle();
        }

        private void chckBinUstuVurgulama_CheckedChanged(object sender, EventArgs e)
        {
            VurgulaBinUstuSatirlar();
        }

        private void VurgulaBinUstuSatirlar()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Toplam Borç"].Value != DBNull.Value &&
                    Convert.ToDecimal(row.Cells["Toplam Borç"].Value) >= 1000)
                {
                    if (chckBinUstuVurgulama.Checked)
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                    else
                        row.DefaultCellStyle.BackColor = Color.White;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }
            }
        }
    }
}
