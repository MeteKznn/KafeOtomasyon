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
    public partial class AyarlarProfilUserControl : UserControl
    {
        public AyarlarProfilUserControl()
        {
            InitializeComponent();
            dataGridView1.DataBindingComplete += dataGridView1_DataBindingComplete;
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        // Aktif kullanıcıyı temsil eden örnek (uygulamanızda farklıysa uyarlayın)
        // AktifKullanici class'ı veya KullaniciBilgileri class'ı tekrar tanımlanmasın, bu satırı kaldırıyorum.

        private void AyarlarProfilUserControl_Load(object sender, EventArgs e)
        {
            RolListesiniYukle();
            DataGridViewAyarla();
            dataGridView1.ClearSelection(); // Sayfa yüklenince hiçbir satır seçili olmasın (garanti)
            SetAktifButon(btnBilgilerim);
            RolleriComboBoxaYukle();
            KullaniciBilgileriniYukle();
            KullaniciYetkilendirme();
            KasiyerRolleriniYukle();
            dgrKasiyerRolleri.SelectionChanged += dgrKasiyerRolleri_SelectionChanged;
        }


        private void RolListesiniYukle()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select RolID, RolAdi From Roller", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            if (dataGridView1.Columns.Contains("RolID"))
                dataGridView1.Columns["RolID"].Visible = false;
            DataGridViewAyarla(); // Grid ayarlarını veri bağladıktan sonra uygula
            dataGridView1.ClearSelection(); // Liste yüklendikten sonra da seçimi temizle
        }

        private void RolleriComboBoxaYukle()
        {
            cmbGuncellenecekRol.Items.Clear();
            using (SqlConnection conn = bgl.baglanti())
            {
                SqlCommand cmd = new SqlCommand("SELECT RolID, RolAdi FROM Roller", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    cmbGuncellenecekRol.Items.Add(new ComboBoxItem { Text = dr["RolAdi"].ToString(), Value = dr["RolID"] });
                }
            }
        }

        private class ComboBoxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }
            public override string ToString() => Text;
        }

        private void KullaniciBilgileriniYukle()
        {
            txtKullaniciAdi.Text = KullaniciBilgileri.KullaniciAdi;
            mskTel.Text = KullaniciBilgileri.Telefon;
            txtMail.Text = KullaniciBilgileri.Mail;
            txtSifre.Text = KullaniciBilgileri.Sifre;
            // Rol adını txtRol'a yaz
            using (SqlConnection conn = bgl.baglanti())
            {
                SqlCommand cmd = new SqlCommand("SELECT RolAdi FROM Roller WHERE RolID=@rolId", conn);
                cmd.Parameters.AddWithValue("@rolId", KullaniciBilgileri.RolID);
                var rolAdi = cmd.ExecuteScalar();
                txtRol.Text = rolAdi != null ? rolAdi.ToString() : "";
            }
        }

        private void SetAktifButon(Button aktifBtn)
        {
            Color aktifRenk = Color.FromArgb(36, 23, 64); // DataGrid başlık rengi
            Color pasifRenk = Color.White;
            Color aktifYazi = Color.White;
            Color pasifYazi = Color.Black;

            btnBilgilerim.BackColor = pasifRenk;
            btnBilgilerim.ForeColor = pasifYazi;
            btnYeniKullanici.BackColor = pasifRenk;
            btnYeniKullanici.ForeColor = pasifYazi;

            aktifBtn.BackColor = aktifRenk;
            aktifBtn.ForeColor = aktifYazi;
        }

        // Seçili rolü takip etmek için alan ekle
        private int? seciliRolId = null;

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                int rolId = Convert.ToInt32(selectedRow.Cells["RolID"].Value);
                seciliRolId = rolId;
                // Rol adını yazdır
                txtRolAdi.Text = selectedRow.Cells["RolAdi"].Value.ToString();
                // Tüm checkbox'ları önce temizle
                chckHizliSatis.Checked = false;
                chckViewistatistik.Checked = false;
                chckYeniUrunGirisi.Checked = false;
                chckViewKasaRaporu.Checked = false;
                chckViewVeresiye.Checked = false;
                chckVeresiyeOdemeAl.Checked = false;
                // Rolün yetkilerini veritabanından çek
                using (SqlConnection conn = bgl.baglanti())
                {
                    SqlCommand cmd = new SqlCommand(
                        "SELECT Y.YetkiAdi FROM RolYetkiler RY INNER JOIN Yetkiler Y ON RY.YetkiID = Y.YetkiID WHERE RY.RolID = @rolId", conn);
                    cmd.Parameters.AddWithValue("@rolId", rolId);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        string yetki = dr["YetkiAdi"].ToString();
                        if (yetki == "Hızlı Satış Yapabilir") chckHizliSatis.Checked = true;
                        else if (yetki == "İstatistik Sayfasını Görüntüleme") chckViewistatistik.Checked = true;
                        else if (yetki == "Yeni Ürün Girişi") chckYeniUrunGirisi.Checked = true;
                        else if (yetki == "Kasa Raporu Görüntüleme") chckViewKasaRaporu.Checked = true;
                        else if (yetki == "Veresiyeler Görüntüleme") chckViewVeresiye.Checked = true;
                        else if (yetki == "Veresiyelerde Ödeme Alma") chckVeresiyeOdemeAl.Checked = true;
                    }
                }
                btnYeniRolKaydet.Text = "Güncelle";
            }
            else
            {
                // Hiçbir satır seçili değilse, alanları temizle
                txtRolAdi.Text = "";
                chckHizliSatis.Checked = false;
                chckViewistatistik.Checked = false;
                chckYeniUrunGirisi.Checked = false;
                chckViewKasaRaporu.Checked = false;
                chckViewVeresiye.Checked = false;
                chckVeresiyeOdemeAl.Checked = false;
                seciliRolId = null;
                btnYeniRolKaydet.Text = "Kaydet";
            }
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.ClearSelection();
        }

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
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(173, 216, 230); // Açık mavi
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
        }

        private void btnYeniRolKaydet_Click(object sender, EventArgs e)
        {
            string rolAdi = txtRolAdi.Text.Trim();
            if (string.IsNullOrEmpty(rolAdi))
            {
                MessageBox.Show("Rol adı boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Yetkileri topla
            List<int> yetkiIdList = new List<int>();
            if (chckHizliSatis.Checked) yetkiIdList.Add(1);
            if (chckViewistatistik.Checked) yetkiIdList.Add(2);
            if (chckYeniUrunGirisi.Checked) yetkiIdList.Add(3);
            if (chckViewKasaRaporu.Checked) yetkiIdList.Add(4);
            if (chckViewVeresiye.Checked) yetkiIdList.Add(5);
            if (chckVeresiyeOdemeAl.Checked) yetkiIdList.Add(6);
            if (seciliRolId == null)
            {
                // YENİ ROL EKLEME
                if (RolAdiVarMi(rolAdi))
                {
                    MessageBox.Show("Bu isimde bir rol zaten var!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 1. Roller tablosuna ekle
                int yeniRolId;
                using (SqlConnection conn = bgl.baglanti())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Roller (RolAdi) OUTPUT INSERTED.RolID VALUES (@rolAdi)", conn);
                    cmd.Parameters.AddWithValue("@rolAdi", rolAdi);
                    yeniRolId = (int)cmd.ExecuteScalar();
                }

                // 2. RolYetkiler tablosuna yetkileri ekle
                using (SqlConnection conn = bgl.baglanti())
                {
                    foreach (int yetkiId in yetkiIdList)
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO RolYetkiler (RolID, YetkiID) VALUES (@rolId, @yetkiId)", conn);
                        cmd.Parameters.AddWithValue("@rolId", yeniRolId);
                        cmd.Parameters.AddWithValue("@yetkiId", yetkiId);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Yeni rol başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // GÜNCELLEME
                int rolId = seciliRolId.Value;
                if (RolAdiVarMi(rolAdi, rolId))
                {
                    MessageBox.Show("Bu isimde başka bir rol zaten var!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 1. Rol adını güncelle
                using (SqlConnection conn = bgl.baglanti())
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Roller SET RolAdi = @rolAdi WHERE RolID = @rolId", conn);
                    cmd.Parameters.AddWithValue("@rolAdi", rolAdi);
                    cmd.Parameters.AddWithValue("@rolId", rolId);
                    cmd.ExecuteNonQuery();
                }

                // 2. Eski yetkileri sil, yeni yetkileri ekle
                using (SqlConnection conn = bgl.baglanti())
                {
                    SqlCommand silCmd = new SqlCommand("DELETE FROM RolYetkiler WHERE RolID = @rolId", conn);
                    silCmd.Parameters.AddWithValue("@rolId", rolId);
                    silCmd.ExecuteNonQuery();
                    foreach (int yetkiId in yetkiIdList)
                    {
                        SqlCommand ekleCmd = new SqlCommand("INSERT INTO RolYetkiler (RolID, YetkiID) VALUES (@rolId, @yetkiId)", conn);
                        ekleCmd.Parameters.AddWithValue("@rolId", rolId);
                        ekleCmd.Parameters.AddWithValue("@yetkiId", yetkiId);
                        ekleCmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Rol başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // Son olarak grid ve alanları yenile ve formu sıfırla
            RolListesiniYukle();
            txtRolAdi.Text = "";
            chckHizliSatis.Checked = false;
            chckViewistatistik.Checked = false;
            chckYeniUrunGirisi.Checked = false;
            chckViewKasaRaporu.Checked = false;
            chckViewVeresiye.Checked = false;
            chckVeresiyeOdemeAl.Checked = false;
            seciliRolId = null;
            btnYeniRolKaydet.Text = "Kaydet";
        }

        private bool RolAdiVarMi(string rolAdi, int? hariçRolId = null)
        {
            using (SqlConnection conn = bgl.baglanti())
            {
                string query = "SELECT COUNT(*) FROM Roller WHERE RolAdi = @rolAdi";
                if (hariçRolId.HasValue)
                    query += " AND RolID <> @rolId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@rolAdi", rolAdi);
                if (hariçRolId.HasValue)
                    cmd.Parameters.AddWithValue("@rolId", hariçRolId.Value);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        private void btnKullaniciAyariKaydet_Click(object sender, EventArgs e)
        {
            // Eğer bilgilerim panelindeysek (örneğin aktif buton btnBilgilerim ise)
            if (btnBilgilerim.BackColor == Color.FromArgb(36, 23, 64)) // aktif renk
            {
                // Sadece kendi bilgilerini güncelle
                if (txtKullaniciAdi.Text == KullaniciBilgileri.KullaniciAdi &&
                    mskTel.Text == KullaniciBilgileri.Telefon &&
                    txtMail.Text == KullaniciBilgileri.Mail &&
                    txtSifre.Text == KullaniciBilgileri.Sifre)
                {
                    MessageBox.Show("Herhangi bir değişiklik yapılmadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (SqlConnection conn = bgl.baglanti())
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Kullanicilar SET KullaniciAd=@ad, Telefon=@tel, Mail=@mail, Sifre=@sifre, RolID=@rol WHERE KullaniciID=@id", conn);
                    cmd.Parameters.AddWithValue("@ad", txtKullaniciAdi.Text);
                    cmd.Parameters.AddWithValue("@tel", mskTel.Text);
                    cmd.Parameters.AddWithValue("@mail", txtMail.Text);
                    cmd.Parameters.AddWithValue("@sifre", txtSifre.Text);
                    cmd.Parameters.AddWithValue("@rol", KullaniciBilgileri.RolID); // txtRol.Text yerine KullaniciBilgileri.RolID kullanılacak
                    cmd.Parameters.AddWithValue("@id", KullaniciBilgileri.KullaniciID);
                    cmd.ExecuteNonQuery();
                }

                // Sadece kendi bilgilerini güncelle
                KullaniciBilgileri.KullaniciAdi = txtKullaniciAdi.Text;
                KullaniciBilgileri.Telefon = mskTel.Text;
                KullaniciBilgileri.Mail = txtMail.Text;
                KullaniciBilgileri.Sifre = txtSifre.Text;
                KullaniciBilgileri.RolID = KullaniciBilgileri.RolID; // txtRol.Text yerine KullaniciBilgileri.RolID kullanılacak

                MessageBox.Show("Bilgileriniz güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Yeni kullanıcı ekleme veya başka kullanıcı işlemleri
                // Sadece veritabanına ekle/güncelle, KullaniciBilgileri'ne dokunma!
                string kullaniciAdi = txtKullaniciAdi.Text.Trim();
                string telefon = mskTel.Text;
                string mail = txtMail.Text.Trim();
                string sifre = txtSifre.Text;
                int rolId = KullaniciBilgileri.RolID; // txtRol.Text yerine KullaniciBilgileri.RolID kullanılacak

                if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(sifre) || txtRol.Text == "") // txtRol.Text yerine KullaniciBilgileri.RolID kullanılacak
                {
                    MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (KullaniciAdiVarMi(kullaniciAdi, KullaniciBilgileri.KullaniciID))
                {
                    MessageBox.Show("Bu kullanıcı adı zaten kullanılıyor.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection conn = bgl.baglanti())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Kullanicilar (KullaniciAd, Telefon, Mail, Sifre, RolID) VALUES (@kullaniciAdi, @telefon, @mail, @sifre, @rolId)", conn);
                    cmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
                    cmd.Parameters.AddWithValue("@telefon", telefon);
                    cmd.Parameters.AddWithValue("@mail", mail);
                    cmd.Parameters.AddWithValue("@sifre", sifre);
                    cmd.Parameters.AddWithValue("@rolId", rolId);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Yeni kullanıcı başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool KullaniciAdiVarMi(string kullaniciAdi, int? hariçKullaniciId = null)
        {
            using (SqlConnection conn = bgl.baglanti())
            {
                string query = "SELECT COUNT(*) FROM Kullanicilar WHERE KullaniciAd = @kullaniciAdi";
                if (hariçKullaniciId.HasValue)
                    query += " AND KullaniciID <> @kullaniciId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
                if (hariçKullaniciId.HasValue)
                    cmd.Parameters.AddWithValue("@kullaniciId", hariçKullaniciId.Value);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        private void btnYeniKullanici_Click(object sender, EventArgs e)
        {
            //Eğer giriş yapan kullanicinin rolü 1 değilse buraya giremez kullanamaz
            if (!KullaniciBilgileri.YetkiKontrol(1))
            {
                MessageBox.Show("Bu işlem için yetkiniz bulunmamaktadır!", "Yetki Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SetAktifButon(btnYeniKullanici);
            // Tüm alanları temizle
            txtKullaniciAdi.Text = "";
            mskTel.Text = "";
            txtMail.Text = "";
            txtSifre.Text = "";
            txtRol.Text = "Komi";
            // ... diğer alanlar
        }

        private void btnBilgilerim_Click(object sender, EventArgs e)
        {
            SetAktifButon(btnBilgilerim);
            KullaniciBilgileriniYukle();
            // Gerekirse diğer panelleri gizle/göster
        }


        private void KullaniciYetkilendirme()
        {

            if (!KullaniciBilgileri.YetkiKontrol(1))
            {
                grpYeniKategori.Visible = false;
                grpYeniRolEkleme.Visible = false;
                grpKasiyerRolGuncelleme.Visible = false;
            }
        }

        private void btnYeniKategoriKaydet_Click(object sender, EventArgs e)
        {
            string kategoriAdi = txtKategoriAdi.Text.Trim();

            if (string.IsNullOrEmpty(kategoriAdi))
            {
                MessageBox.Show("Kategori adı boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Aynı isimde kategori var mı kontrol et
            if (KategoriAdiVarMi(kategoriAdi))
            {
                MessageBox.Show("Bu isimde bir kategori zaten var!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kategoriyi veritabanına ekle
            using (SqlConnection conn = bgl.baglanti())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Kategoriler (KategoriAdi) VALUES (@kategoriAdi)", conn);
                cmd.Parameters.AddWithValue("@kategoriAdi", kategoriAdi);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Kategori başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Kategori adı alanını temizle
            txtKategoriAdi.Text = "";
        }

        private bool KategoriAdiVarMi(string kategoriAdi)
        {
            using (SqlConnection conn = bgl.baglanti())
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Kategoriler WHERE KategoriAdi = @kategoriAdi", conn);
                cmd.Parameters.AddWithValue("@kategoriAdi", kategoriAdi);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Alanları sıfırla
            txtRolAdi.Text = "";
            chckHizliSatis.Checked = false;
            chckViewistatistik.Checked = false;
            chckYeniUrunGirisi.Checked = false;
            chckViewKasaRaporu.Checked = false;
            chckViewVeresiye.Checked = false;
            chckVeresiyeOdemeAl.Checked = false;
            seciliRolId = null;
            btnYeniRolKaydet.Text = "Kaydet";
            // DataGrid'de hiçbir satır seçili olmasın
            dataGridView1.ClearSelection();
        }

        private void KasiyerRolleriniYukle()
        {
            using (SqlConnection conn = bgl.baglanti())
            {
                SqlDataAdapter da = new SqlDataAdapter(@"SELECT K.KullaniciID, K.KullaniciAd, R.RolID, R.RolAdi FROM Kullanicilar K INNER JOIN Roller R ON K.RolID = R.RolID", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgrKasiyerRolleri.DataSource = dt;
                if (dgrKasiyerRolleri.Columns.Contains("KullaniciID"))
                    dgrKasiyerRolleri.Columns["KullaniciID"].Visible = false;
                if (dgrKasiyerRolleri.Columns.Contains("RolID"))
                    dgrKasiyerRolleri.Columns["RolID"].Visible = false;
                dgrKasiyerRolleri.Columns["KullaniciAd"].HeaderText = "Kullanıcı Adı";
                dgrKasiyerRolleri.Columns["RolAdi"].HeaderText = "Rol";
                dgrKasiyerRolleri.RowTemplate.Height = 32;
                dgrKasiyerRolleri.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgrKasiyerRolleri.MultiSelect = false;
                dgrKasiyerRolleri.AllowUserToAddRows = false;
                dgrKasiyerRolleri.ReadOnly = true;
                dgrKasiyerRolleri.AllowUserToResizeColumns = false;
                dgrKasiyerRolleri.DefaultCellStyle.Font = new Font("Segoe UI", 11);
                dgrKasiyerRolleri.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgrKasiyerRolleri.DefaultCellStyle.SelectionBackColor = Color.FromArgb(173, 216, 230);
                dgrKasiyerRolleri.DefaultCellStyle.SelectionForeColor = Color.Black;
                dgrKasiyerRolleri.DefaultCellStyle.BackColor = Color.White;
                dgrKasiyerRolleri.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
                dgrKasiyerRolleri.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dgrKasiyerRolleri.ColumnHeadersHeight = 36;
                dgrKasiyerRolleri.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                dgrKasiyerRolleri.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgrKasiyerRolleri.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(36, 23, 64);
                dgrKasiyerRolleri.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgrKasiyerRolleri.ColumnHeadersDefaultCellStyle.Padding = new Padding(5);
                dgrKasiyerRolleri.CellBorderStyle = DataGridViewCellBorderStyle.Single;
                dgrKasiyerRolleri.GridColor = Color.LightGray;
                dgrKasiyerRolleri.RowHeadersVisible = false;
                dgrKasiyerRolleri.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                dgrKasiyerRolleri.BorderStyle = BorderStyle.FixedSingle;
                dgrKasiyerRolleri.EnableHeadersVisualStyles = false;
                dgrKasiyerRolleri.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(36, 23, 64);
                dgrKasiyerRolleri.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
                dgrKasiyerRolleri.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            dgrKasiyerRolleri.ClearSelection();
        }

        private void dgrKasiyerRolleri_SelectionChanged(object sender, EventArgs e)
        {
            if (dgrKasiyerRolleri.SelectedRows.Count > 0)
            {
                var row = dgrKasiyerRolleri.SelectedRows[0];
                int rolId = Convert.ToInt32(row.Cells["RolID"].Value);
                // Rol combobox'unda ilgili rolü seçili yap
                bool bulundu = false;
                for (int i = 0; i < cmbGuncellenecekRol.Items.Count; i++)
                {
                    var item = cmbGuncellenecekRol.Items[i];
                    if (item is ComboBoxItem cbi && Convert.ToInt32(cbi.Value) == rolId)
                    {
                        cmbGuncellenecekRol.SelectedIndex = i;
                        bulundu = true;
                        break;
                    }
                }
                if (!bulundu)
                    cmbGuncellenecekRol.SelectedIndex = -1;
            }
            else
            {
                cmbGuncellenecekRol.SelectedIndex = -1;
            }
        }

        private void btnRolGuncelle_Click(object sender, EventArgs e)
        {
            if (dgrKasiyerRolleri.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir kasiyer seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var row = dgrKasiyerRolleri.SelectedRows[0];
            int kullaniciId = Convert.ToInt32(row.Cells["KullaniciID"].Value);
            int eskiRolId = Convert.ToInt32(row.Cells["RolID"].Value);
            if (!(cmbGuncellenecekRol.SelectedItem is ComboBoxItem seciliRol))
            {
                MessageBox.Show("Lütfen bir rol seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int yeniRolId = Convert.ToInt32(seciliRol.Value);
            if (eskiRolId == yeniRolId)
            {
                MessageBox.Show("Zaten bu kasiyer bu role sahip.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            using (SqlConnection conn = bgl.baglanti())
            {
                SqlCommand cmd = new SqlCommand("UPDATE Kullanicilar SET RolID=@rol WHERE KullaniciID=@id", conn);
                cmd.Parameters.AddWithValue("@rol", yeniRolId);
                cmd.Parameters.AddWithValue("@id", kullaniciId);
                cmd.ExecuteNonQuery();
            }
            MessageBox.Show("Kasiyerin rolü güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            KasiyerRolleriniYukle();
        }

        private void dgrKasiyerRolleri_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgrKasiyerRolleri.ClearSelection();
        }
    }
}
