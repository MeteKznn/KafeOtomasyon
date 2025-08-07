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
    public partial class FrmSatisEkrani : Form
    {
        sqlbaglantisi bgl = new sqlbaglantisi();
        public bool MasaTasiModu = false;
        public int KaynakMasaID = 0;
        
        // Sayfa yönetimi için değişkenler
        private int mevcutSayfa = 1;
        private int masalarSayfaBasina = 25;
        private int toplamMasaSayisi = 0;
        private List<DataRow> tumMasalar = new List<DataRow>();

        public FrmSatisEkrani()
        {
            InitializeComponent();
            this.FormClosing += FrmSatisEkrani_FormClosing;
        }

        private void FrmSatisEkrani_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = false; // Formu kapatmaya izin ver
            }
        }

        private void FrmSatisEkrani_Load(object sender, EventArgs e)
        {
            // Sayfa butonlarının click eventlerini ekle
            btnOnceki.Click += btnOnceki_Click;
            btnSonraki.Click += btnSonraki_Click;
            
            // Masaları göster
            MasalariGetirBaslangic();
            DoluMasalarSadece(); // Sadece dolu masaları listele
            this.Refresh();
            ResetButtonStyles();
            ApplySelectedButtonStyle(btnMasalar); // Set Masalar as default selected
            GetKullaniciAdi();
            
            // Label1'i ve sayfa bilgisini doğru şekilde ayarla
            SayfaButonlariniGuncelle();
            
            // ESC tuşu ile form kapatma
            this.KeyPreview = true;
            this.KeyDown += FrmSatisEkrani_KeyDown;
        }

        private void FrmSatisEkrani_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC tuşuna basıldığında formu kapat
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                e.Handled = true;
            }
        }

        private void btnOnceki_Click(object sender, EventArgs e)
        {
            if (mevcutSayfa > 1)
            {
                mevcutSayfa--;
                if (btnBilardoMasalari.BackColor == ColorTranslator.FromHtml("#eae7ef"))
                {
                    BilardoMasalariniSayfadaGoster();
                }
                else
                {
                    MasalariSayfadaGoster();
                }
                SayfaButonlariniGuncelle();
            }
        }

        private void btnSonraki_Click(object sender, EventArgs e)
        {
            int toplamSayfaSayisi = (int)Math.Ceiling((double)toplamMasaSayisi / masalarSayfaBasina);
            if (mevcutSayfa < toplamSayfaSayisi)
            {
                mevcutSayfa++;
                if (btnBilardoMasalari.BackColor == ColorTranslator.FromHtml("#eae7ef"))
                {
                    BilardoMasalariniSayfadaGoster();
                }
                else
                {
                    MasalariSayfadaGoster();
                }
                SayfaButonlariniGuncelle();
            }
        }

        private void SayfaButonlariniGuncelle()
        {
            int toplamSayfaSayisi = (int)Math.Ceiling((double)toplamMasaSayisi / masalarSayfaBasina);
            
            // Önceki butonu kontrolü
            btnOnceki.Enabled = (mevcutSayfa > 1);
            
            // Sonraki butonu kontrolü
            btnSonraki.Enabled = (mevcutSayfa < toplamSayfaSayisi);
            
            // Sayfa bilgisini label1'de göster - Buton seçili durumlarını kontrol et
            bool masalarSecili = (btnMasalar.BackColor == ColorTranslator.FromHtml("#eae7ef"));
            bool bilardoMasalariSecili = (btnBilardoMasalari.BackColor == ColorTranslator.FromHtml("#eae7ef"));
            
            if (toplamSayfaSayisi > 1)
            {
                if (masalarSecili)
                {
                    label1.Text = $"Masalar - Sayfa {mevcutSayfa}/{toplamSayfaSayisi}";
                }
                else if (bilardoMasalariSecili)
                {
                    label1.Text = $"Bilardo Masaları - Sayfa {mevcutSayfa}/{toplamSayfaSayisi}";
                }
                else
                {
                    // Varsayılan olarak Masalar
                    label1.Text = $"Masalar - Sayfa {mevcutSayfa}/{toplamSayfaSayisi}";
                }
            }
            else
            {
                if (masalarSecili)
                {
                    label1.Text = "Masalar";
                }
                else if (bilardoMasalariSecili)
                {
                    label1.Text = "Bilardo Masaları";
                }
                else
                {
                    // Varsayılan olarak Masalar
                    label1.Text = "Masalar";
                }
            }
        }

        void GetKullaniciAdi()
        {
            using (SqlConnection conn = bgl.baglanti())
            {
                SqlCommand komut = new SqlCommand("SELECT KullaniciAd FROM Kullanicilar WHERE KullaniciID = @KullaniciID", conn);
                komut.Parameters.AddWithValue("@KullaniciID", KullaniciBilgileri.KullaniciID);
                
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    labelKullaniciAdi.Text = dr["KullaniciAd"].ToString();
                }
                dr.Close();
            }

            // Panel arka plan rengi
            panelKullanici.BackColor = Color.FromArgb(255, 94, 58, 163); // Dilerseniz başka bir renk
            ControlStyles.ApplyBorderRadius(panelKullanici, 8, RoundedCorners.All);

            // PictureBox ayarları
            pictureBoxKullanici.Image = Image.FromFile("images/Person.png"); // Proje dizininde olduğundan emin olun
            pictureBoxKullanici.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxKullanici.Size = new Size(24, 24);
            pictureBoxKullanici.Location = new Point(10, 8);

            // Label ayarları
            labelKullaniciAdi.ForeColor = Color.White;
            labelKullaniciAdi.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            labelKullaniciAdi.Location = new Point(40, 10);
            labelKullaniciAdi.AutoSize = true;
        }

        private void ResetButtonStyles()
        {
            Color defaultBackColor = ColorTranslator.FromHtml("#241740"); // Dark purple
            Color defaultForeColor = Color.White;
            int borderRadius = 0; // No rounding for unselected buttons

            btnMasalar.BackColor = defaultBackColor;
            btnMasalar.ForeColor = defaultForeColor;
            ControlStyles.ApplyBorderRadius(btnMasalar, borderRadius, RoundedCorners.None);
            btnMasalar.FlatStyle = FlatStyle.Flat;
            btnMasalar.FlatAppearance.BorderSize = 0;

            btnDoluMasalar.BackColor = defaultBackColor;
            btnDoluMasalar.ForeColor = defaultForeColor;
            ControlStyles.ApplyBorderRadius(btnDoluMasalar, borderRadius, RoundedCorners.None);
            btnDoluMasalar.FlatStyle = FlatStyle.Flat;
            btnDoluMasalar.FlatAppearance.BorderSize = 0;

            btnBilardoMasalari.BackColor = defaultBackColor;
            btnBilardoMasalari.ForeColor = defaultForeColor;
            ControlStyles.ApplyBorderRadius(btnBilardoMasalari, borderRadius, RoundedCorners.None);
            btnBilardoMasalari.FlatStyle = FlatStyle.Flat;
            btnBilardoMasalari.FlatAppearance.BorderSize = 0;
        }

        private void ApplySelectedButtonStyle(Button button)
        {
            ResetButtonStyles(); // Reset all buttons first
            
            button.BackColor = ColorTranslator.FromHtml("#eae7ef");
            button.ForeColor = Color.Black;
            ControlStyles.ApplyBorderRadius(button, 20, RoundedCorners.RightSide); // Apply right-side rounding
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
        }

        public void MasalariGetirBaslangic()
        {
            // Normal masaları veritabanından çek (Bilardo ile başlamayanlar)
            string query = @"SELECT 
                                                M.MasaID, 
                                                M.MasaAdi, 
                                                M.Durum, 
                                                ISNULL(SUM(SH.Adet * SH.BirimFiyat), 0) AS ToplamTutar, 
                                                MIN(SH.Tarih) AS AcilmaSaati
                                                FROM Masalar AS M
                                                LEFT JOIN SatisHareket AS SH ON M.MasaID = SH.MasaID AND SH.Aktif = 1
                                                WHERE UPPER(M.MasaAdi) NOT LIKE 'BİLARDO%' AND UPPER(M.MasaAdi) NOT LIKE 'BILARDO%'
                                                GROUP BY M.MasaID, M.MasaAdi, M.Durum ORDER BY M.MasaID";

            // Normal masaları DataTable'a çek
            DataTable dt = new DataTable();
            using (SqlConnection conn = bgl.baglanti())
            {
                SqlCommand komut = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(komut);
                da.Fill(dt);
            }

            // Normal masaları listeye aktar
            tumMasalar.Clear();
            foreach (DataRow row in dt.Rows)
            {
                tumMasalar.Add(row);
            }
            
            // Toplam masa sayısını güncelle
            toplamMasaSayisi = tumMasalar.Count;
            
            // İlk sayfayı göster
            mevcutSayfa = 1;
            MasalariSayfadaGoster();
            
            // Sayfa bilgisini güncelle
            SayfaButonlariniGuncelle();
        }

        public void MasalariGetir(bool showOnlyOccupied)
        {
            // Normal masaları veritabanından çek (Bilardo ile başlamayanlar)
            string query = @"SELECT 
                                                M.MasaID, 
                                                M.MasaAdi, 
                                                M.Durum, 
                                                ISNULL(SUM(SH.Adet * SH.BirimFiyat), 0) AS ToplamTutar, 
                                                MIN(SH.Tarih) AS AcilmaSaati
                                                FROM Masalar AS M
                                                LEFT JOIN SatisHareket AS SH ON M.MasaID = SH.MasaID AND SH.Aktif = 1
                                                WHERE UPPER(M.MasaAdi) NOT LIKE 'BİLARDO%' AND UPPER(M.MasaAdi) NOT LIKE 'BILARDO%'";
            
            if (showOnlyOccupied)
            {
                query += " AND M.Durum = 1"; // Filter for occupied tables if requested
            }

            query += " GROUP BY M.MasaID, M.MasaAdi, M.Durum ORDER BY M.MasaID";

            // Normal masaları DataTable'a çek
            DataTable dt = new DataTable();
            using (SqlConnection conn = bgl.baglanti())
            {
                SqlCommand komut = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(komut);
                da.Fill(dt);
            }

            // Normal masaları listeye aktar
            tumMasalar.Clear();
            foreach (DataRow row in dt.Rows)
            {
                tumMasalar.Add(row);
            }

            // Toplam masa sayısını güncelle
            toplamMasaSayisi = tumMasalar.Count;
            
            // İlk sayfayı göster
            mevcutSayfa = 1;
            MasalariSayfadaGoster();
            
            // Sayfa bilgisini güncelle
            SayfaButonlariniGuncelle();
        }

        private void MasalariSayfadaGoster()
        {
            masaPanels.Controls.Clear();

            int x = 10;
            int y = 10;
            int butonGenislik = 120;
            int butonYukseklik = 120;
            int margin = 10;
            int butonSayisi = 0;
            int butonlarBirSatirda = 5;

            // Mevcut sayfa için masaları hesapla
            int baslangicIndex = (mevcutSayfa - 1) * masalarSayfaBasina;
            int bitisIndex = Math.Min(baslangicIndex + masalarSayfaBasina, toplamMasaSayisi);

            for (int i = baslangicIndex; i < bitisIndex; i++)
            {
                DataRow masa = tumMasalar[i];

                Panel masaCard = new Panel
                {
                    Width = butonGenislik,
                    Height = butonYukseklik,
                    BackColor = Convert.ToBoolean(masa["Durum"]) ? ColorTranslator.FromHtml("#f2255c") : Color.White,
                    Location = new Point(x, y),
                    Tag = masa["MasaID"],
                    Cursor = Cursors.Hand
                };

                ControlStyles.ApplyBorderRadius(masaCard, 10);

                bool isMasaDolu = Convert.ToBoolean(masa["Durum"]);

                Label lblMasaAdi = new Label
                {
                    Text = masa["MasaAdi"].ToString(),
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    AutoSize = true,
                    ForeColor = isMasaDolu ? Color.White : Color.Black,
                    Cursor = Cursors.Hand
                };
                masaCard.Controls.Add(lblMasaAdi);
                lblMasaAdi.Click += (s, ev) => Masa_Click(masaCard, ev);

                Label lblDurumTutar = new Label
                {
                    Text = isMasaDolu ? "₺" + Convert.ToDecimal(masa["ToplamTutar"]).ToString("N2") : "Boş",
                    Font = new Font("Segoe UI", 10, FontStyle.Regular),
                    AutoSize = true,
                    ForeColor = isMasaDolu ? Color.White : Color.DarkGreen,
                    Cursor = Cursors.Hand
                };
                masaCard.Controls.Add(lblDurumTutar);
                lblDurumTutar.Click += (s, ev) => Masa_Click(masaCard, ev);

                int verticalSpacing = 5;
                int contentHeight = lblMasaAdi.PreferredSize.Height + verticalSpacing + lblDurumTutar.PreferredSize.Height;
                int startY = (masaCard.Height - contentHeight) / 2;

                lblMasaAdi.Location = new Point((masaCard.Width - lblMasaAdi.PreferredSize.Width) / 2, startY);
                lblDurumTutar.Location = new Point((masaCard.Width - lblDurumTutar.PreferredSize.Width) / 2, lblMasaAdi.Bottom + verticalSpacing);

                if (isMasaDolu)
                {
                    Label lblAcilmaSaati = new Label
                    {
                        Text = masa["AcilmaSaati"] == DBNull.Value ? "" : Convert.ToDateTime(masa["AcilmaSaati"]).ToString("HH:mm"),
                        Font = new Font("Segoe UI", 9, FontStyle.Italic),
                        AutoSize = true,
                        Padding = new Padding(0, 0, 5, 5),
                        ForeColor = Color.White,
                        Cursor = Cursors.Hand
                    };
                    masaCard.Controls.Add(lblAcilmaSaati);
                    lblAcilmaSaati.Click += (s, ev) => Masa_Click(masaCard, ev);
                    lblAcilmaSaati.Location = new Point(masaCard.Width - lblAcilmaSaati.PreferredSize.Width - lblAcilmaSaati.Padding.Right,
                                                         masaCard.Height - lblAcilmaSaati.PreferredSize.Height - lblAcilmaSaati.Padding.Bottom);
                }

                masaCard.Click += Masa_Click;

                masaPanels.Controls.Add(masaCard);

                x += butonGenislik + margin;
                butonSayisi++;

                if (butonSayisi % butonlarBirSatirda == 0)
                {
                    x = 10;
                    y += butonYukseklik + margin;
                }
            }
        }

        private void Masa_Click(object sender, EventArgs e)
        {
            Panel clickedCard = (Panel)sender;
            int hedefMasaID = Convert.ToInt32(clickedCard.Tag);

            if (MasaTasiModu && KaynakMasaID > 0 && hedefMasaID != KaynakMasaID)
            {
                if (MessageBox.Show($"Masa {KaynakMasaID} hesabı Masa {hedefMasaID} hesabına aktarılacak. Onaylıyor musunuz?", "Masa Taşı", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    decimal eskiFiyat = GetMasaToplamFiyat(hedefMasaID);
                    MasaHesapAktar(KaynakMasaID, hedefMasaID);
                    decimal yeniFiyat = GetMasaToplamFiyat(hedefMasaID);

                    MessageBox.Show($"Aktarım başarılı!\nEski Fiyat: ₺{eskiFiyat:N2}\nYeni Fiyat: ₺{yeniFiyat:N2}", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.MasaTasiModu = false;
                    this.KaynakMasaID = 0;
                    MasalariYenile();
                }
                return;
            }

            // Masa türünü belirle
            string masaTuru = "Normal";
            try
            {
                SqlCommand masaKomut = new SqlCommand("SELECT MasaAdi FROM Masalar WHERE MasaID = @p1", bgl.baglanti());
                masaKomut.Parameters.AddWithValue("@p1", hedefMasaID);
                SqlDataReader masaDr = masaKomut.ExecuteReader();
                if (masaDr.Read())
                {
                    string masaAdi = masaDr["MasaAdi"].ToString();
                    if (masaAdi.Contains("Bilardo"))
                    {
                        masaTuru = "Bilardo";
                    }
                }
                bgl.baglanti().Close();
            }
            catch (Exception ex)
            {
                // Hata durumunda Normal olarak devam et
            }
            
            FrmMasaDetay masaDetayForm = new FrmMasaDetay(hedefMasaID, this, masaTuru);
            masaDetayForm.Show();
        }

        public void DoluMasalar()
        {
            doluMasaPanels.Controls.Clear(); // Clear existing controls
            DoluMasalarSadece();
        }

        public void DoluMasalarSadece()
        {
            doluMasaPanels.Controls.Clear(); // Clear existing controls

            SqlCommand komut = new SqlCommand(@"SELECT 
                                                M.MasaID, 
                                                M.MasaAdi, 
                                                M.Durum, 
                                                ISNULL(SUM(SH.Adet * SH.BirimFiyat), 0) AS ToplamTutar, 
                                                MIN(SH.Tarih) AS AcilmaSaati
                                                FROM Masalar AS M
                                                LEFT JOIN SatisHareket AS SH ON M.MasaID = SH.MasaID AND SH.Aktif = 1
                                                WHERE M.Durum = 1 -- Sadece dolu masaları getir
                                                GROUP BY M.MasaID, M.MasaAdi, M.Durum
                                                ORDER BY MIN(SH.Tarih) DESC", bgl.baglanti()); // En son açılanlar üstte olsun
            SqlDataReader dr = komut.ExecuteReader();

            int yPos = 10; // Başlangıç Y konumu
            int itemHeight = 50; // Her bir liste öğesinin yüksekliği
            int itemMargin = 10; // Öğeler arası boşluk
            int counter = 1; // Sıra numarası için sayaç
            int totalOccupiedTables = 0;
            decimal totalSalesAmount = 0;

            while (dr.Read())
            {
                totalOccupiedTables++;
                
                // Masa türünü kontrol et ve fiyatı hesapla
                int masaID = Convert.ToInt32(dr["MasaID"]);
                string masaAdi = dr["MasaAdi"].ToString();
                bool isBilardoMasa = masaAdi.Contains("Bilardo");
                
                decimal masaFiyati;
                if (isBilardoMasa)
                {
                    masaFiyati = GetBilardoMasaToplamFiyat(masaID);
                }
                else
                {
                    masaFiyati = Convert.ToDecimal(dr["ToplamTutar"]);
                }
                
                totalSalesAmount += masaFiyati;

                Panel listItemPanel = new Panel
                {
                    Width = doluMasaPanels.Width - 30, // Panel genişliğini kısalt
                    Height = itemHeight,
                    BackColor = Color.White, // Arka plan rengini beyaz yap
                    Location = new Point(10, yPos),
                    Tag = dr["MasaID"],
                    Cursor = Cursors.Hand
                };

                ControlStyles.ApplyBorderRadius(listItemPanel, 8); // Hafif yuvarlatılmış köşeler

                // Sıra Numarası (Örnek: 5)
                Label lblCounter = new Label
                {
                    Text = counter.ToString(),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = Color.White,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(30, 30),
                    Location = new Point(5, (itemHeight - 30) / 2)
                };
                ControlStyles.ApplyBorderRadius(lblCounter, 15); // Yuvarlak görünüm için
                lblCounter.BackColor = ColorTranslator.FromHtml("#f2255c"); // Koyu kırmızı daire rengi (pembe)
                lblCounter.Click += (s, ev) => Masa_Click(listItemPanel, ev); // Click eventini panele yönlendir
                listItemPanel.Controls.Add(lblCounter);

                // Masa Adı
                Label lblMasaAdi = new Label
                {
                    Text = dr["MasaAdi"].ToString(),
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    ForeColor = Color.Black, // Masa adı rengini siyah yap
                    AutoSize = true,
                    Location = new Point(lblCounter.Right + 10, (itemHeight - 20) / 2) // Yanına konumlandır
                };
                lblMasaAdi.Click += (s, ev) => Masa_Click(listItemPanel, ev); // Click eventini panele yönlendir
                listItemPanel.Controls.Add(lblMasaAdi);

                // Açılma Saati
                Label lblAcilmaSaati = new Label
                {
                    Text = dr["AcilmaSaati"] == DBNull.Value ? "" : Convert.ToDateTime(dr["AcilmaSaati"]).ToString("HH:mm"),
                    Font = new Font("Segoe UI", 10, FontStyle.Regular),
                    ForeColor = Color.Black, // Açılma saati rengini siyah yap
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleRight,
                };
                lblAcilmaSaati.Click += (s, ev) => Masa_Click(listItemPanel, ev); // Click eventini panele yönlendir
                listItemPanel.Controls.Add(lblAcilmaSaati);

                // Toplam fiyatı hesapla (masaID, masaAdi, isBilardoMasa zaten tanımlı)
                decimal toplamFiyat;
                if (isBilardoMasa)
                {
                    toplamFiyat = GetBilardoMasaToplamFiyat(masaID);
                }
                else
                {
                    toplamFiyat = Convert.ToDecimal(dr["ToplamTutar"]);
                }
                
                // Toplam Tutar
                Label lblToplamTutar = new Label
                {
                    Text = "₺" + toplamFiyat.ToString("N2"),
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    ForeColor = Color.Black, // Toplam tutar rengini siyah yap
                    AutoSize = true,
                    TextAlign = ContentAlignment.MiddleRight,
                };
                lblToplamTutar.Click += (s, ev) => Masa_Click(listItemPanel, ev); // Click eventini panele yönlendir
                listItemPanel.Controls.Add(lblToplamTutar);

                // Konumlandırma - Sağ Hizalama (saati sola kaydır)
                lblToplamTutar.Location = new Point(listItemPanel.Width - lblToplamTutar.PreferredSize.Width - 10, (itemHeight - lblToplamTutar.PreferredSize.Height) / 2);
                lblAcilmaSaati.Location = new Point(lblToplamTutar.Left - lblAcilmaSaati.PreferredSize.Width - 5, (itemHeight - lblAcilmaSaati.PreferredSize.Height) / 2);

                listItemPanel.Click += Masa_Click;
                doluMasaPanels.Controls.Add(listItemPanel);

                yPos += itemHeight + itemMargin;
                counter++;
            }

            bgl.baglanti().Close();

            label3.Text = totalOccupiedTables + " Adisyon";
            label6.Text = "₺" + totalSalesAmount.ToString("N2");
        }

        private void btnDoluMasalar_Click(object sender, EventArgs e)
        {
            // Dolu masalar için özel işlem - sadece dolu masaları göster
            MasalariGetir(true); // Sadece dolu masaları getir
            DoluMasalar();
            ApplySelectedButtonStyle(btnDoluMasalar);
            SayfaButonlariniGuncelle(); // Label1'i güncelle
        }

        private void btnMasalar_Click(object sender, EventArgs e)
        {
            MasalariGetirBaslangic();
            ApplySelectedButtonStyle(btnMasalar);
            SayfaButonlariniGuncelle(); // Label1'i güncelle
        }

        private void btnBilardoMasalari_Click(object sender, EventArgs e)
        {
            // Bilardo masalarını getir ve göster
            BilardoMasalariniGetir();
            ApplySelectedButtonStyle(btnBilardoMasalari);
            SayfaButonlariniGuncelle(); // Label1'i güncelle
        }

        private decimal GetMasaToplamFiyat(int masaID)
        {
            using (SqlConnection conn = bgl.baglanti())
            {
                SqlCommand cmd = new SqlCommand("SELECT ISNULL(SUM(Adet * BirimFiyat), 0) FROM SatisHareket WHERE MasaID = @masaID AND Aktif = 1", conn);
                cmd.Parameters.AddWithValue("@masaID", masaID);
                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }

        private void MasaHesapAktar(int kaynakMasaID, int hedefMasaID)
        {
            using (SqlConnection conn = bgl.baglanti())
            {
                // Hedef masanın mevcut ürünlerini kontrol et
                SqlCommand cmdCheck = new SqlCommand(@"
                    SELECT SH1.UrunID, SH1.Adet as KaynakAdet, SH2.Adet as HedefAdet, SH2.Id as HedefId
                    FROM SatisHareket SH1
                    LEFT JOIN SatisHareket SH2 ON SH1.UrunID = SH2.UrunID AND SH2.MasaID = @hedefMasaID AND SH2.Aktif = 1
                    WHERE SH1.MasaID = @kaynakMasaID AND SH1.Aktif = 1", conn);
                
                cmdCheck.Parameters.AddWithValue("@kaynakMasaID", kaynakMasaID);
                cmdCheck.Parameters.AddWithValue("@hedefMasaID", hedefMasaID);
                
                SqlDataReader dr = cmdCheck.ExecuteReader();
                List<Tuple<int, int, int, int>> urunler = new List<Tuple<int, int, int, int>>();
                
                while (dr.Read())
                {
                    int urunID = Convert.ToInt32(dr["UrunID"]);
                    int kaynakAdet = Convert.ToInt32(dr["KaynakAdet"]);
                    int hedefAdet = dr["HedefAdet"] == DBNull.Value ? 0 : Convert.ToInt32(dr["HedefAdet"]);
                    int hedefId = dr["HedefId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["HedefId"]);
                    urunler.Add(new Tuple<int, int, int, int>(urunID, kaynakAdet, hedefAdet, hedefId));
                }
                dr.Close();

                // Her ürün için işlem yap
                foreach (var urun in urunler)
                {
                    if (urun.Item3 > 0) // Hedef masada aynı üründen varsa
                    {
                        // Hedef masadaki ürünün adetini güncelle
                        SqlCommand cmdUpdate = new SqlCommand("UPDATE SatisHareket SET Adet = @adet WHERE Id = @id", conn);
                        cmdUpdate.Parameters.AddWithValue("@adet", urun.Item2 + urun.Item3);
                        cmdUpdate.Parameters.AddWithValue("@id", urun.Item4);
                        cmdUpdate.ExecuteNonQuery();
                    }
                    else // Hedef masada yoksa yeni kayıt ekle
                    {
                        SqlCommand cmdInsert = new SqlCommand(@"
                            INSERT INTO SatisHareket (MasaID, UrunID, Adet, BirimFiyat, Tarih, Aktif)
                            SELECT @hedefMasaID, UrunID, Adet, BirimFiyat, GETDATE(), 1
                            FROM SatisHareket
                            WHERE MasaID = @kaynakMasaID AND UrunID = @urunID AND Aktif = 1", conn);
                        
                        cmdInsert.Parameters.AddWithValue("@hedefMasaID", hedefMasaID);
                        cmdInsert.Parameters.AddWithValue("@kaynakMasaID", kaynakMasaID);
                        cmdInsert.Parameters.AddWithValue("@urunID", urun.Item1);
                        cmdInsert.ExecuteNonQuery();
                    }
                }

                // Kaynak masadaki tüm ürünleri sil
                SqlCommand cmdDelete = new SqlCommand("DELETE FROM SatisHareket WHERE MasaID = @kaynakMasaID AND Aktif = 1", conn);
                cmdDelete.Parameters.AddWithValue("@kaynakMasaID", kaynakMasaID);
                cmdDelete.ExecuteNonQuery();

                // Kaynak masayı boşalt
                SqlCommand cmdUpdateMasa = new SqlCommand("UPDATE Masalar SET Durum = 0 WHERE MasaID = @kaynakMasaID", conn);
                cmdUpdateMasa.Parameters.AddWithValue("@kaynakMasaID", kaynakMasaID);
                cmdUpdateMasa.ExecuteNonQuery();

                // Hedef masayı dolu yap
                SqlCommand cmdUpdateHedefMasa = new SqlCommand("UPDATE Masalar SET Durum = 1 WHERE MasaID = @hedefMasaID", conn);
                cmdUpdateHedefMasa.Parameters.AddWithValue("@hedefMasaID", hedefMasaID);
                cmdUpdateHedefMasa.ExecuteNonQuery();
            }
        }

        // Bilardo Masaları İşlemleri
        public void BilardoMasalariniGetir()
        {
            // Masa adının başında "Bilardo" olan masaları getir (büyük/küçük harf duyarsız)
            string query = @"SELECT 
                                                M.MasaID, 
                                                M.MasaAdi, 
                                                M.Durum, 
                                                ISNULL(SUM(SH.Adet * SH.BirimFiyat), 0) AS ToplamTutar, 
                                                MIN(SH.Tarih) AS AcilmaSaati
                                                FROM Masalar AS M
                                                LEFT JOIN SatisHareket AS SH ON M.MasaID = SH.MasaID AND SH.Aktif = 1
                                                WHERE UPPER(M.MasaAdi) LIKE 'BİLARDO%' OR UPPER(M.MasaAdi) LIKE 'BILARDO%'
                                                GROUP BY M.MasaID, M.MasaAdi, M.Durum ORDER BY M.MasaID";

            // Bilardo masalarını DataTable'a çek
            DataTable dt = new DataTable();
            using (SqlConnection conn = bgl.baglanti())
            {
                SqlCommand komut = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(komut);
                da.Fill(dt);
            }

            // Bilardo masalarını listeye aktar
            tumMasalar.Clear();
            foreach (DataRow row in dt.Rows)
            {
                tumMasalar.Add(row);
            }

            // Toplam bilardo masa sayısını güncelle
            toplamMasaSayisi = tumMasalar.Count;
            
            // İlk sayfayı göster
            mevcutSayfa = 1;
            BilardoMasalariniSayfadaGoster();
            
            // Sayfa bilgisini güncelle
            SayfaButonlariniGuncelle();
        }

        private void BilardoMasalariniSayfadaGoster()
        {
            masaPanels.Controls.Clear();

            int x = 10;
            int y = 10;
            int butonGenislik = 120;
            int butonYukseklik = 120;
            int margin = 10;
            int butonSayisi = 0;
            int butonlarBirSatirda = 5;

            // Mevcut sayfa için bilardo masalarını hesapla
            int baslangicIndex = (mevcutSayfa - 1) * masalarSayfaBasina;
            int bitisIndex = Math.Min(baslangicIndex + masalarSayfaBasina, toplamMasaSayisi);

            for (int i = baslangicIndex; i < bitisIndex; i++)
            {
                DataRow bilardoMasa = tumMasalar[i];
                int masaID = Convert.ToInt32(bilardoMasa["MasaID"]);
                bool isBilardoMasaDolu = Convert.ToBoolean(bilardoMasa["Durum"]);
                bool sureAsildi = isBilardoMasaDolu && BilardoMasaSuresiAsildiMi(masaID);

                // Panelin rengi eğer masa saatini aştıysa farklı bir renkte göstersin
                Color masaRengi;
                if (!isBilardoMasaDolu)
                {
                    masaRengi = Color.White; // Boş masa
                }
                else if (sureAsildi)
                {
                    masaRengi = ColorTranslator.FromHtml("#FF6B35"); // Süre aşımı - turuncu
                }
                else
                {
                    masaRengi = ColorTranslator.FromHtml("#f2255c"); // Normal dolu masa
                }

                Panel bilardoMasaCard = new Panel
                {
                    Width = butonGenislik,
                    Height = butonYukseklik,
                    BackColor = masaRengi,
                    Location = new Point(x, y),
                    Tag = masaID,
                    Cursor = Cursors.Hand
                };

                ControlStyles.ApplyBorderRadius(bilardoMasaCard, 10);

                Label lblBilardoMasaAdi = new Label
                {
                    Text = bilardoMasa["MasaAdi"].ToString(),
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    AutoSize = true,
                    ForeColor = isBilardoMasaDolu ? Color.White : Color.Black,
                    Cursor = Cursors.Hand
                };
                bilardoMasaCard.Controls.Add(lblBilardoMasaAdi);
                lblBilardoMasaAdi.Click += (s, ev) => BilardoMasa_Click(bilardoMasaCard, ev);

                // Bilardo masaları için toplam fiyatı hesapla (ek ücret dahil)
                decimal toplamFiyat = isBilardoMasaDolu ? GetBilardoMasaToplamFiyat(masaID) : 0;
                
                Label lblDurumTutar = new Label
                {
                    Text = isBilardoMasaDolu ? "₺" + toplamFiyat.ToString("N2") : "Boş",
                    Font = new Font("Segoe UI", 10, FontStyle.Regular),
                    AutoSize = true,
                    ForeColor = isBilardoMasaDolu ? Color.White : Color.DarkGreen,
                    Cursor = Cursors.Hand
                };
                bilardoMasaCard.Controls.Add(lblDurumTutar);
                lblDurumTutar.Click += (s, ev) => BilardoMasa_Click(bilardoMasaCard, ev);

                int verticalSpacing = 5;
                int contentHeight = lblBilardoMasaAdi.PreferredSize.Height + verticalSpacing + lblDurumTutar.PreferredSize.Height;
                int startY = (bilardoMasaCard.Height - contentHeight) / 2;

                lblBilardoMasaAdi.Location = new Point((bilardoMasaCard.Width - lblBilardoMasaAdi.PreferredSize.Width) / 2, startY);
                lblDurumTutar.Location = new Point((bilardoMasaCard.Width - lblDurumTutar.PreferredSize.Width) / 2, lblBilardoMasaAdi.Bottom + verticalSpacing);

                if (isBilardoMasaDolu)
                {
                    Label lblAcilmaSaati = new Label
                    {
                        Text = bilardoMasa["AcilmaSaati"] == DBNull.Value ? "" : Convert.ToDateTime(bilardoMasa["AcilmaSaati"]).ToString("HH:mm"),
                        Font = new Font("Segoe UI", 9, FontStyle.Italic),
                        AutoSize = true,
                        Padding = new Padding(0, 0, 5, 5),
                        ForeColor = Color.White,
                        Cursor = Cursors.Hand
                    };
                    bilardoMasaCard.Controls.Add(lblAcilmaSaati);
                    lblAcilmaSaati.Click += (s, ev) => BilardoMasa_Click(bilardoMasaCard, ev);
                    lblAcilmaSaati.Location = new Point(bilardoMasaCard.Width - lblAcilmaSaati.PreferredSize.Width - lblAcilmaSaati.Padding.Right,
                                                         bilardoMasaCard.Height - lblAcilmaSaati.PreferredSize.Height - lblAcilmaSaati.Padding.Bottom);
                }

                bilardoMasaCard.Click += BilardoMasa_Click;

                masaPanels.Controls.Add(bilardoMasaCard);

                x += butonGenislik + margin;
                butonSayisi++;

                if (butonSayisi % butonlarBirSatirda == 0)
                {
                    x = 10;
                    y += butonYukseklik + margin;
                }
            }
        }

        private void BilardoMasa_Click(object sender, EventArgs e)
        {
            Panel clickedCard = (Panel)sender;
            int hedefBilardoMasaID = Convert.ToInt32(clickedCard.Tag);

            // Bilardo masası için özel işlemler burada yapılabilir

            FrmMasaDetay masaDetayForm = new FrmMasaDetay(hedefBilardoMasaID, this, "Bilardo");
            masaDetayForm.Show();
        }

        /// <summary>
        /// Bilardo masasının süresinin aşılıp aşılmadığını kontrol eder
        /// </summary>
        /// <param name="masaID">Masa ID</param>
        /// <returns>Süre aşıldıysa true, aşılmadıysa false</returns>
        private bool BilardoMasaSuresiAsildiMi(int masaID)
        {
            try
            {
                using (SqlConnection conn = bgl.baglanti())
                {
                    SqlCommand komut = new SqlCommand(@"SELECT 
                                                        MIN(SH.Tarih) AS AcilmaSaati,
                                                        (SELECT TOP 1 Fiyat FROM Urunler WHERE UrunAdi LIKE '%Bilardo%' AND UrunAdi LIKE '%1 saat%' AND Durum = 1) AS SaatlikUcret
                                                        FROM SatisHareket AS SH
                                                        WHERE SH.MasaID = @p1 AND SH.Aktif = 1", conn);
                    komut.Parameters.AddWithValue("@p1", masaID);
                    SqlDataReader dr = komut.ExecuteReader();

                    if (dr.Read() && dr["AcilmaSaati"] != DBNull.Value)
                    {
                        DateTime acilmaSaati = Convert.ToDateTime(dr["AcilmaSaati"]);
                        TimeSpan gecenSure = DateTime.Now - acilmaSaati;
                        int toplamDakika = (int)gecenSure.TotalMinutes;
                        
                        return toplamDakika > 60; // 1 saat geçmişse true
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda false döndür
            }
            
            return false;
        }

        /// <summary>
        /// Bilardo masasının toplam fiyatını hesaplar (ek ücret dahil)
        /// </summary>
        /// <param name="masaID">Masa ID</param>
        /// <returns>Toplam fiyat</returns>
        private decimal GetBilardoMasaToplamFiyat(int masaID)
        {
            decimal toplamFiyat = 0;
            
            try
            {
                using (SqlConnection conn = bgl.baglanti())
                {
                    // Temel ürün fiyatları
                    SqlCommand temelKomut = new SqlCommand(@"SELECT ISNULL(SUM(Adet * BirimFiyat), 0) 
                                                            FROM SatisHareket 
                                                            WHERE MasaID = @masaID AND Aktif = 1", conn);
                    temelKomut.Parameters.AddWithValue("@masaID", masaID);
                    toplamFiyat = Convert.ToDecimal(temelKomut.ExecuteScalar());

                    // Bilardo ek ücreti hesapla
                    SqlCommand bilardoKomut = new SqlCommand(@"SELECT 
                                                                MIN(SH.Tarih) AS AcilmaSaati,
                                                                SUM(CASE WHEN U.UrunAdi LIKE '%Bilardo%' AND U.UrunAdi LIKE '%1 saat%' THEN SH.Adet ELSE 0 END) AS ToplamSaatlikAdet,
                                                                (SELECT TOP 1 Fiyat FROM Urunler WHERE UrunAdi LIKE '%Bilardo%' AND UrunAdi LIKE '%1 saat%' AND Durum = 1) AS SaatlikUcret,
                                                                (SELECT TOP 1 Fiyat FROM Urunler WHERE UrunAdi = 'Bilardo' AND Durum = 1) AS SinirsizUcret
                                                                FROM SatisHareket AS SH
                                                                INNER JOIN Urunler AS U ON SH.UrunID = U.UrunID
                                                                WHERE SH.MasaID = @masaID AND SH.Aktif = 1 
                                                                AND (U.UrunAdi LIKE '%Bilardo%' OR U.UrunAdi = 'Bilardo')
                                                                GROUP BY SH.MasaID", conn);
                    bilardoKomut.Parameters.AddWithValue("@masaID", masaID);
                    SqlDataReader dr = bilardoKomut.ExecuteReader();

                    if (dr.Read() && dr["AcilmaSaati"] != DBNull.Value)
                    {
                        DateTime acilmaSaati = Convert.ToDateTime(dr["AcilmaSaati"]);
                        decimal saatlikUcret = dr["SaatlikUcret"] != DBNull.Value ? Convert.ToDecimal(dr["SaatlikUcret"]) : 0;
                        decimal sinirsizUcret = dr["SinirsizUcret"] != DBNull.Value ? Convert.ToDecimal(dr["SinirsizUcret"]) : 0;
                        int toplamSaatlikAdet = dr["ToplamSaatlikAdet"] != DBNull.Value ? Convert.ToInt32(dr["ToplamSaatlikAdet"]) : 0;
                        
                        TimeSpan gecenSure = DateTime.Now - acilmaSaati;
                        int toplamDakika = (int)gecenSure.TotalMinutes;
                        
                        // Sınırsız masa kontrolü
                        bool sinirsizMasa = sinirsizUcret > 0 && toplamSaatlikAdet == 0;
                        
                        if (sinirsizMasa)
                        {
                            // Sınırsız masa - sadece oynanan süreye göre ücret (başlangıç ücreti yok)
                            decimal dakikaUcreti = sinirsizUcret / 60;
                            toplamFiyat = toplamDakika * dakikaUcreti; // Toplam fiyatı sıfırla ve sadece süre ücretini hesapla
                        }
                        else
                        {
                            // Saatlik masa - süre aştığında ek ücret
                            int toplamSaat = toplamSaatlikAdet * 60;
                            
                            if (toplamDakika > toplamSaat && saatlikUcret > 0)
                            {
                                int fazlaDakika = toplamDakika - toplamSaat;
                                decimal ekUcret = (fazlaDakika * saatlikUcret) / 60;
                                toplamFiyat += ekUcret;
                            }
                        }
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda temel fiyatı döndür
            }
            
            return toplamFiyat;
        }

        // Masaları yenilemek için yardımcı metod
        public void MasalariYenile()
        {
            if (btnMasalar.BackColor == ColorTranslator.FromHtml("#eae7ef"))
            {
                MasalariGetirBaslangic();
            }
            else if (btnDoluMasalar.BackColor == ColorTranslator.FromHtml("#eae7ef"))
            {
                MasalariGetir(true);
            }
            else if (btnBilardoMasalari.BackColor == ColorTranslator.FromHtml("#eae7ef"))
            {
                BilardoMasalariniGetir();
            }
            DoluMasalarSadece();
        }
    }
}
