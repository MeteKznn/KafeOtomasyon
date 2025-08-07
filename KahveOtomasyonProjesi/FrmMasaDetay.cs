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
    public partial class FrmMasaDetay : Form
    {
        private int _masaID;
        sqlbaglantisi bgl = new sqlbaglantisi();
        public FrmSatisEkrani _parentForm;
        private string _masaTuru;
        private Timer _sureTimer; // Süre güncellemesi için timer

        public FrmMasaDetay(int masaID, FrmSatisEkrani parentForm, string masaTuru)
        {
            InitializeComponent();
            this.KeyPreview = true;
            _masaID = masaID;
            _parentForm = parentForm;
            _masaTuru = masaTuru;
            
            // Timer'ı başlat (sadece Bilardo masaları için)
            if (_masaTuru == "Bilardo")
            {
                _sureTimer = new Timer();
                _sureTimer.Interval = 30000; // 30 saniyede bir güncelle
                _sureTimer.Tick += SureTimer_Tick;
                _sureTimer.Start();
            }
        }

        private void SureTimer_Tick(object sender, EventArgs e)
        {
            // Sadece açık masalarda süre bilgisini güncelle
            if (MasaAcikMi())
            {
                LoadBilardoSuresi();
            }
        }

        /// <summary>
        /// Masanın açık olup olmadığını kontrol eder
        /// </summary>
        /// <returns>Masa açıksa true, değilse false döner</returns>
        private bool MasaAcikMi()
        {
            try
            {
                SqlCommand masaDurumKomut = new SqlCommand("SELECT Durum FROM Masalar WHERE MasaID = @p1", bgl.baglanti());
                masaDurumKomut.Parameters.AddWithValue("@p1", _masaID);
                SqlDataReader masaDr = masaDurumKomut.ExecuteReader();
                
                bool masaAcik = false;
                if (masaDr.Read())
                {
                    masaAcik = Convert.ToBoolean(masaDr["Durum"]);
                }
                bgl.baglanti().Close();
                
                return masaAcik;
            }
            catch (Exception ex)
            {
                // Hata durumunda false döndür
                return false;
            }
        }

        /// <summary>
        /// Sınırsız bilardo masalarının ücretini hesaplar
        /// </summary>
        /// <returns>Sınırsız masa ücreti</returns>
        private decimal HesaplaSinirsizBilardoUcreti()
        {
            decimal sinirsizUcret = 0;
            
            try
            {
                // Masa açık değilse ücret hesaplama
                if (!MasaAcikMi())
                {
                    return 0;
                }

                SqlCommand komut = new SqlCommand(@"SELECT 
                                                    MIN(SH.Tarih) AS AcilmaSaati,
                                                    (SELECT TOP 1 Fiyat FROM Urunler WHERE UrunAdi = 'Bilardo' AND Durum = 1) AS SinirsizUcret
                                                    FROM SatisHareket AS SH
                                                    INNER JOIN Urunler AS U ON SH.UrunID = U.UrunID
                                                    WHERE SH.MasaID = @p1 AND SH.Aktif = 1 
                                                    AND U.UrunAdi = 'Bilardo'
                                                    GROUP BY SH.MasaID", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", _masaID);
                SqlDataReader dr = komut.ExecuteReader();

                if (dr.Read() && dr["AcilmaSaati"] != DBNull.Value)
                {
                    DateTime acilmaSaati = Convert.ToDateTime(dr["AcilmaSaati"]);
                    decimal saatlikUcret = dr["SinirsizUcret"] != DBNull.Value ? Convert.ToDecimal(dr["SinirsizUcret"]) : 0;
                    
                    if (saatlikUcret > 0)
                    {
                        TimeSpan gecenSure = DateTime.Now - acilmaSaati;
                        int toplamDakika = (int)gecenSure.TotalMinutes;
                        
                        // Dakika üzerinden ücret hesaplama
                        decimal dakikaUcreti = saatlikUcret / 60; // Saatlik ücretin dakika karşılığı
                        sinirsizUcret = toplamDakika * dakikaUcreti;
                    }
                }
                bgl.baglanti().Close();
            }
            catch (Exception ex)
            {
                // Hata durumunda 0 döndür
                sinirsizUcret = 0;
            }
            
            return sinirsizUcret;
        }

        /// <summary>
        /// Ek ücreti masa ürünlerinde item olarak gösterir
        /// </summary>
        /// <param name="ekUcret">Ek ücret miktarı</param>
        private void EkUcretItemEkle(decimal ekUcret)
        {
            // Debug için konsola yazdır
            Console.WriteLine($"EkUcretItemEkle çağrıldı: {ekUcret}");
            
            // Mevcut ek ücret item'ını kaldır (varsa)
            foreach (Control control in masaDetaylariPanels.Controls)
            {
                if (control is Panel panel && panel.Tag != null && panel.Tag.ToString() == "EkUcret")
                {
                    masaDetaylariPanels.Controls.Remove(panel);
                    panel.Dispose();
                    break;
                }
            }

            // Yeni ek ücret item'ı ekle
            int yPos = masaDetaylariPanels.Controls.Count > 0 ? 
                masaDetaylariPanels.Controls[masaDetaylariPanels.Controls.Count - 1].Bottom + 5 : 10;
            
            Panel ekUcretPanel = new Panel
            {
                Width = masaDetaylariPanels.Width - 20,
                Height = 50,
                Location = new Point(10, yPos),
                BackColor = Color.FromArgb(255, 235, 235), // Açık kırmızı arka plan
                Tag = "EkUcret"
            };

            // Ek ücret label'ı
            Label lblEkUcret = new Label
            {
                Text = "Ek Ücret",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(50, 15),
                ForeColor = Color.Red
            };
            ekUcretPanel.Controls.Add(lblEkUcret);

            // Ek ücret miktarı
            Label lblEkUcretMiktar = new Label
            {
                Text = "₺" + ekUcret.ToString("N2"),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(ekUcretPanel.Width - 100, 15),
                ForeColor = Color.Red
            };
            ekUcretPanel.Controls.Add(lblEkUcretMiktar);

            masaDetaylariPanels.Controls.Add(ekUcretPanel);
            
            // Debug için konsola yazdır
            Console.WriteLine($"Ek ücret panel eklendi: {ekUcretPanel.Width}x{ekUcretPanel.Height} at {ekUcretPanel.Location}");
        }

        private void FrmMasaDetay_Load(object sender, EventArgs e)
        {
            LoadMasaDetails();
            LoadUrunler();
            
            // Form seviyesinde key event handler'ları ekle
           
            this.KeyDown += FrmMasaDetay_KeyDown;
            
            // Ödeme Al butonunu varsayılan buton olarak ayarla
            this.AcceptButton = btnOdeme;
            
            // Form açıldığında Ödeme Al butonuna odaklan
            btnOdeme.Focus();
        }

        private void FrmMasaDetay_KeyDown(object sender, KeyEventArgs e)
        {
            MessageBox.Show("Metot içine girdi.");
            // Enter tuşuna basıldığında Ödeme Al butonunu tetikle
            if (e.KeyCode == Keys.Enter)
            {
                MessageBox.Show("Enter tuşuna basıldı.");
                // Eğer aktif kontrol bir TextBox, ComboBox veya başka bir input kontrolü ise, Enter'ı işleme
                if (this.ActiveControl is TextBox || this.ActiveControl is ComboBox || this.ActiveControl is RichTextBox)
                {
                    return; // Input kontrollerinde Enter'a izin ver
                }
                
                // Eğer aktif kontrol bir buton ise ve o buton btnOdeme değilse, işleme
                if (this.ActiveControl is Button && this.ActiveControl != btnOdeme)
                {
                    return; // Diğer butonlarda Enter'a izin ver
                }
                
                // Enter tuşunu yakala ve sadece Ödeme Al butonunu tetikle
                e.Handled = true; // Enter tuşunu yakala
                
                // Ödeme Al butonunu doğrudan tetikle
                if (btnOdeme != null && btnOdeme.Visible && btnOdeme.Enabled)
                {
                    btnOdeme.PerformClick();
                }
            }
            // ESC tuşuna basıldığında formu kapat
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                e.Handled = true;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                btnOdeme.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void LoadMasaDetails()
        {
            // Masa numarasını label3'e yazdır
            SqlCommand komut = new SqlCommand("SELECT MasaAdi FROM Masalar WHERE MasaID = @p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", _masaID);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                label3.Text = dr["MasaAdi"].ToString();
            }
            bgl.baglanti().Close();

            // Bilardo masası ise süre bilgisini panel4'e ekle
            if (_masaTuru == "Bilardo")
            {
                // Sadece açık masalarda süre bilgisi göster
                if (MasaAcikMi())
                {
                    LoadBilardoSuresi();
                }
            }

            // Masa detaylarını (ürünleri) masaDetaylariPanels'e listele
            // Bu kısım ListBox veya DataGridView ile yapılabilir. Şimdilik placeholder bırakalım.
            LoadMasaUrunleri();
            ApplySelectedButtonStyle(btnGenel);
        }

        private decimal HesaplaBilardoEkUcreti()
        {
            decimal ekUcret = 0;
            
            try
            {
                // Masa açık değilse ek ücret hesaplama
                if (!MasaAcikMi())
                {
                    Console.WriteLine("Masa açık değil, ek ücret hesaplanmıyor");
                    return 0;
                }

                SqlCommand komut = new SqlCommand(@"SELECT 
                                                    MIN(SH.Tarih) AS AcilmaSaati,
                                                    SUM(CASE WHEN U.UrunAdi LIKE '%Bilardo%' AND U.UrunAdi LIKE '%1 saat%' THEN SH.Adet ELSE 0 END) AS ToplamSaatlikAdet,
                                                    (SELECT TOP 1 Fiyat FROM Urunler WHERE UrunAdi LIKE '%Bilardo%' AND UrunAdi LIKE '%1 saat%' AND Durum = 1) AS SaatlikUcret,
                                                    (SELECT TOP 1 Fiyat FROM Urunler WHERE UrunAdi = 'Bilardo' AND Durum = 1) AS SinirsizUcret
                                                    FROM SatisHareket AS SH
                                                    INNER JOIN Urunler AS U ON SH.UrunID = U.UrunID
                                                    WHERE SH.MasaID = @p1 AND SH.Aktif = 1 
                                                    AND (U.UrunAdi LIKE '%Bilardo%' OR U.UrunAdi = 'Bilardo')
                                                    GROUP BY SH.MasaID", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", _masaID);
                SqlDataReader dr = komut.ExecuteReader();

                if (dr.Read() && dr["AcilmaSaati"] != DBNull.Value)
                {
                    DateTime acilmaSaati = Convert.ToDateTime(dr["AcilmaSaati"]);
                    decimal saatlikUcret = dr["SaatlikUcret"] != DBNull.Value ? Convert.ToDecimal(dr["SaatlikUcret"]) : 0;
                    decimal sinirsizUcret = dr["SinirsizUcret"] != DBNull.Value ? Convert.ToDecimal(dr["SinirsizUcret"]) : 0;
                    int toplamSaatlikAdet = dr["ToplamSaatlikAdet"] != DBNull.Value ? Convert.ToInt32(dr["ToplamSaatlikAdet"]) : 0;
                    
                    TimeSpan gecenSure = DateTime.Now - acilmaSaati;
                    int toplamDakika = (int)gecenSure.TotalMinutes;
                    
                    // Sınırsız masa kontrolü - sadece tam olarak "Bilardo" ürünü varsa
                    bool sinirsizMasa = sinirsizUcret > 0 && toplamSaatlikAdet == 0;
                    
                    if (sinirsizMasa)
                    {
                        // Sınırsız masalarda ek ücret yok
                        ekUcret = 0;
                    }
                    else
                    {
                        // Saatlik masa - sadece süre aştığında ek ücret
                        int toplamSaat = toplamSaatlikAdet * 60; // 1 saatlik adet sayısı * 60 dakika
                        
                        if (toplamDakika > toplamSaat && saatlikUcret > 0) // Süre geçmiş
                        {
                            int fazlaDakika = toplamDakika - toplamSaat;
                            ekUcret = (fazlaDakika * saatlikUcret) / 60;
                            Console.WriteLine($"Ek ücret hesaplandı: {fazlaDakika} dakika fazla, {ekUcret} TL");
                        }
                        else
                        {
                            ekUcret = 0; // Süre içinde ek ücret yok
                            Console.WriteLine("Süre içinde, ek ücret yok");
                        }
                    }
                }
                bgl.baglanti().Close();
            }
            catch (Exception ex)
            {
                // Hata durumunda 0 döndür
                ekUcret = 0;
            }
            
            return ekUcret;
        }

        private void LoadBilardoSuresi()
        {
            // Panel4'teki mevcut label1'i temizle ve yeni süre label'ı ekle
            panel4.Controls.Clear();

            // Menüler label'ı
            Label lblMenuler = new Label
            {
                Text = "Menüler",
                Font = new Font("Segoe UI Semibold", 18, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(17, 12),
                ForeColor = Color.Black
            };
            panel4.Controls.Add(lblMenuler);

            // Masa açık değilse süre bilgisi gösterme
            if (!MasaAcikMi())
            {
                return;
            }

            // Bilardo süre bilgisini al (1 saatlik ve sınırsız masalar için)
            SqlCommand komut = new SqlCommand(@"SELECT 
                                                MIN(SH.Tarih) AS AcilmaSaati,
                                                SUM(CASE WHEN U.UrunAdi LIKE '%Bilardo%' AND U.UrunAdi LIKE '%1 saat%' THEN SH.Adet ELSE 0 END) AS ToplamSaatlikAdet,
                                                (SELECT TOP 1 Fiyat FROM Urunler WHERE UrunAdi LIKE '%Bilardo%' AND UrunAdi LIKE '%1 saat%' AND Durum = 1) AS SaatlikUcret,
                                                (SELECT TOP 1 Fiyat FROM Urunler WHERE UrunAdi = 'Bilardo' AND Durum = 1) AS SinirsizUcret
                                                FROM SatisHareket AS SH
                                                INNER JOIN Urunler AS U ON SH.UrunID = U.UrunID
                                                WHERE SH.MasaID = @p1 AND SH.Aktif = 1 
                                                AND (U.UrunAdi LIKE '%Bilardo%' OR U.UrunAdi = 'Bilardo')
                                                GROUP BY SH.MasaID", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", _masaID);
            SqlDataReader dr = komut.ExecuteReader();

            if (dr.Read() && dr["AcilmaSaati"] != DBNull.Value)
            {
                DateTime acilmaSaati = Convert.ToDateTime(dr["AcilmaSaati"]);
                decimal saatlikUcret = dr["SaatlikUcret"] != DBNull.Value ? Convert.ToDecimal(dr["SaatlikUcret"]) : 0;
                decimal sinirsizUcret = dr["SinirsizUcret"] != DBNull.Value ? Convert.ToDecimal(dr["SinirsizUcret"]) : 0;
                int toplamSaatlikAdet = dr["ToplamSaatlikAdet"] != DBNull.Value ? Convert.ToInt32(dr["ToplamSaatlikAdet"]) : 0;
                
                TimeSpan gecenSure = DateTime.Now - acilmaSaati;
                int toplamDakika = (int)gecenSure.TotalMinutes;
                
                string sureText = "";
                decimal ekUcret = 0;
                
                // Sınırsız masa kontrolü - sadece tam olarak "Bilardo" ürünü varsa
                bool sinirsizMasa = sinirsizUcret > 0 && toplamSaatlikAdet == 0;
                
                Console.WriteLine($"Sınırsız ücret: {sinirsizUcret}, Saatlik adet: {toplamSaatlikAdet}, Toplam dakika: {toplamDakika}, Sınırsız masa: {sinirsizMasa}");
                
                if (sinirsizMasa)
                {
                    // Sınırsız masa - kalan süre göster (sınırsız olduğu için hep 0)
                    sureText = $"Kalan: Sınırsız";
                    ekUcret = 0; // Sınırsız masalarda ek ücret yok
                    Console.WriteLine("Sınırsız masa tespit edildi");
                }
                else
                {
                    // Saatlik masa - toplam süre hesaplama
                    int toplamSaat = toplamSaatlikAdet * 60; // 1 saatlik adet sayısı * 60 dakika
                    
                    Console.WriteLine($"Saatlik masa: Toplam {toplamSaat} dakika, Geçen {toplamDakika} dakika");
                    
                    if (toplamDakika <= toplamSaat) // Süre içinde
                    {
                        int kalanDakika = toplamSaat - toplamDakika;
                        sureText = $"Kalan: {kalanDakika} dk";
                        ekUcret = 0; // Süre içinde ek ücret yok
                        Console.WriteLine($"Süre içinde: {kalanDakika} dakika kaldı");
                    }
                    else // Süre geçmiş
                    {
                        int fazlaDakika = toplamDakika - toplamSaat;
                        sureText = $"Süre Bitti +{fazlaDakika} dk";
                        
                        // Fazla dakikaların ücretini hesapla (dakika başına saatlik ücretin 1/60'ı)
                        if (saatlikUcret > 0)
                        {
                            ekUcret = (fazlaDakika * saatlikUcret) / 60;
                        }
                        Console.WriteLine($"Süre aştı: {fazlaDakika} dakika fazla");
                    }
                }

                // Süre label'ı (sağ taraf)
                Label lblSure = new Label
                {
                    Text = sureText,
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    AutoSize = true,
                    Location = new Point(530, 15),
                    //548; 12
                    ForeColor = toplamDakika > 60 ? Color.Red : Color.Green
                };
                panel4.Controls.Add(lblSure);

                // Ek ücret varsa göster
                if (ekUcret > 0)
                {
                    lblSure.Location = new Point(400, 15);
                    Label lblEkUcret = new Label
                    {
                        Text = $"Ek Ücret: ₺{ekUcret:N2}",
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        AutoSize = true,
                        Location = new Point(550, 17),
                        ForeColor = Color.Red
                    };
                    panel4.Controls.Add(lblEkUcret);
                }
            }
            else
            {
                // Masa açık ama süre bilgisi yoksa (henüz ürün eklenmemiş)
                Label lblSure = new Label
                {
                    Text = "Masa henüz açılmadı",
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    AutoSize = true,
                    Location = new Point(20, 15),
                    ForeColor = Color.Gray
                };
                panel4.Controls.Add(lblSure);
            }
            bgl.baglanti().Close();
        }

        private void ResetButtonStyles()
        {
            Color defaultBackColor = ColorTranslator.FromHtml("#241740"); // Dark purple
            Color defaultForeColor = Color.White;
            int borderRadius = 0; // No rounding for unselected buttons

            btnGenel.BackColor = defaultBackColor;
            btnGenel.ForeColor = defaultForeColor;
            ControlStyles.ApplyBorderRadius(btnGenel, borderRadius, RoundedCorners.None);
            btnGenel.FlatStyle = FlatStyle.Flat;
            btnGenel.FlatAppearance.BorderSize = 0;

            btnicecekler.BackColor = defaultBackColor;
            btnicecekler.ForeColor = defaultForeColor;
            ControlStyles.ApplyBorderRadius(btnicecekler, borderRadius, RoundedCorners.None);
            btnicecekler.FlatStyle = FlatStyle.Flat;
            btnicecekler.FlatAppearance.BorderSize = 0;

            btnYiyecekler.BackColor = defaultBackColor;
            btnYiyecekler.ForeColor = defaultForeColor;
            ControlStyles.ApplyBorderRadius(btnYiyecekler, borderRadius, RoundedCorners.None);
            btnYiyecekler.FlatStyle = FlatStyle.Flat;
            btnYiyecekler.FlatAppearance.BorderSize = 0;

            // Add other buttons here as needed
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

        private void LoadMasaUrunleri()
        {
            masaDetaylariPanels.Controls.Clear();
            SqlCommand komut = new SqlCommand(@"SELECT
                                                SH.UrunID,
                                                U.UrunAdi,
                                                SUM(SH.Adet) AS ToplamAdet,
                                                SUM(SH.Adet * SH.BirimFiyat) AS ToplamFiyat
                                                FROM SatisHareket AS SH
                                                INNER JOIN Urunler AS U ON SH.UrunID = U.UrunID
                                                WHERE SH.MasaID = @p1 AND SH.Aktif = 1
                                                GROUP BY SH.UrunID, U.UrunAdi
                                                ORDER BY U.UrunAdi", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", _masaID);
            SqlDataReader dr = komut.ExecuteReader();

            int yPos = 10;
            int itemHeight = 50; // Yüksekliği artırıldı
            int margin = 5;
            while (dr.Read())
            {
                int urunID = Convert.ToInt32(dr["UrunID"]);
                int adet = Convert.ToInt32(dr["ToplamAdet"]);
                Panel urunItemPanel = new Panel
                {
                    Width = masaDetaylariPanels.Width - 20,
                    Height = itemHeight,
                    Location = new Point(10, yPos),
                    BackColor = Color.White,
                    Tag = urunID
                };

                // - Butonu
                Button btnAzalt = new Button
                {
                    Text = "-",
                    Font = new Font("Segoe UI", 16, FontStyle.Bold),
                    Size = new Size(40, 40),
                    Location = new Point(5, (itemHeight - 40) / 2),
                    BackColor = Color.FromArgb(230, 230, 230),
                    FlatStyle = FlatStyle.Flat,
                    Tag = urunID
                };
                btnAzalt.FlatAppearance.BorderSize = 0;
                btnAzalt.Click += (s, ev) =>
                {
                    if (adet > 1)
                        UrunAdetAzalt(_masaID, urunID);
                    else
                        UrunSilOnay(_masaID, urunID);
                };
                urunItemPanel.Controls.Add(btnAzalt);

                // Adet Label
                Label lblQuantity = new Label
                {
                    Text = adet + "x",
                    Font = new Font("Segoe UI", 12, FontStyle.Bold),
                    ForeColor = ColorTranslator.FromHtml("#f2255c"),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Size = new Size(45, 30), // Genişlik artırıldı
                    Location = new Point(btnAzalt.Right + 5, (itemHeight - 30) / 2),
                    Tag = urunID
                };
                ControlStyles.ApplyBorderRadius(lblQuantity, 15);
                urunItemPanel.Controls.Add(lblQuantity);

                // Çift tıklama ile adet düzenleme
                lblQuantity.DoubleClick += (s, ev) =>
                {
                    TextBox txtAdet = new TextBox
                    {
                        Text = adet.ToString(),
                        Font = lblQuantity.Font,
                        Size = lblQuantity.Size,
                        Location = lblQuantity.Location,
                        TextAlign = HorizontalAlignment.Center,
                        Tag = urunID
                    };
                    urunItemPanel.Controls.Add(txtAdet);
                    txtAdet.BringToFront();
                    txtAdet.Focus();
                    txtAdet.SelectAll();

                    bool textBoxRemoved = false;

                    void removeTextBox()
                    {
                        if (!textBoxRemoved)
                        {
                            textBoxRemoved = true;
                            if (urunItemPanel.Controls.Contains(txtAdet))
                            {
                                urunItemPanel.Controls.Remove(txtAdet);
                                txtAdet.Dispose();
                            }
                            // Panelde başka bir kontrol varsa ona, yoksa btnGeri'ye odak ver
                            if (urunItemPanel.Controls.Count > 0)
                                urunItemPanel.Controls[0].Focus();
                            else if (btnGeri != null && btnGeri.Visible && btnGeri.Enabled)
                                btnGeri.Focus();
                            else
                                this.ActiveControl = null;
                        }
                    }

                    void updateAdet()
                    {
                        try
                        {
                            if (int.TryParse(txtAdet.Text, out int yeniAdet) && yeniAdet > 0)
                            {
                                using (SqlConnection conn = bgl.baglanti())
                                {
                                    SqlCommand cmd = new SqlCommand("UPDATE SatisHareket SET Adet = @adet WHERE MasaID = @masaID AND UrunID = @urunID AND Aktif = 1", conn);
                                    cmd.Parameters.AddWithValue("@adet", yeniAdet);
                                    cmd.Parameters.AddWithValue("@masaID", _masaID);
                                    cmd.Parameters.AddWithValue("@urunID", urunID);
                                    cmd.ExecuteNonQuery();
                                }
                                removeTextBox(); // Önce TextBox'ı kaldır
                                LoadMasaUrunleri(); // Sonra paneli güncelle
                                if (_parentForm != null)
                                {
                                    _parentForm.MasalariGetir(false);
                                    _parentForm.DoluMasalar();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Lütfen pozitif bir sayı giriniz.", "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                removeTextBox();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Bir hata oluştu: " + ex.Message);
                            removeTextBox();
                        }
                    }

                    txtAdet.KeyDown += (snd, evt) =>
                    {
                        if (evt.KeyCode == Keys.Enter)
                        {
                            updateAdet();
                        }
                        else if (evt.KeyCode == Keys.Escape)
                        {
                            removeTextBox();
                        }
                    };
                    txtAdet.LostFocus += (snd, evt) =>
                    {
                        removeTextBox();
                    };
                };

                // + Butonu
                Button btnArttir = new Button
                {
                    Text = "+",
                    Font = new Font("Segoe UI", 16, FontStyle.Bold),
                    Size = new Size(40, 40),
                    Location = new Point(lblQuantity.Right + 5, (itemHeight - 40) / 2),
                    BackColor = Color.FromArgb(230, 230, 230),
                    FlatStyle = FlatStyle.Flat,
                    Tag = urunID
                };
                btnArttir.FlatAppearance.BorderSize = 0;
                btnArttir.Click += (s, ev) => UrunAdetArttir(_masaID, urunID);
                urunItemPanel.Controls.Add(btnArttir);

                // Ürün adı
                Label lblUrunAd = new Label
                {
                    Text = dr["UrunAdi"].ToString(),
                    Font = new Font("Segoe UI", 10, FontStyle.Regular),
                    AutoSize = true,
                    ForeColor = Color.Black,
                    Location = new Point(btnArttir.Right + 10, (itemHeight - 20) / 2)
                };
                urunItemPanel.Controls.Add(lblUrunAd);

                // Fiyat
                Label lblToplamFiyat = new Label
                {
                    Text = "₺" + Convert.ToDecimal(dr["ToplamFiyat"]).ToString("N2"),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    AutoSize = true,
                    ForeColor = Color.Black,
                    TextAlign = ContentAlignment.MiddleRight,
                };
                lblToplamFiyat.Location = new Point(urunItemPanel.Width - lblToplamFiyat.PreferredSize.Width - 30, (itemHeight - lblToplamFiyat.PreferredSize.Height) / 2);
                urunItemPanel.Controls.Add(lblToplamFiyat);

                // Çarpı (sil) butonu
                PictureBox pbSil = new PictureBox
                {
                    Image = Image.FromFile("images/Close.png"),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Width = 24,
                    Height = 24,
                    Cursor = Cursors.Hand,
                    BackColor = Color.Transparent,
                    Tag = urunID
                };
                pbSil.Location = new Point(urunItemPanel.Width - pbSil.Width - 8, (urunItemPanel.Height - pbSil.Height) / 2);
                pbSil.Click += (s, ev) => UrunSilOnay(_masaID, urunID);
                urunItemPanel.Controls.Add(pbSil);
                pbSil.BringToFront();

                masaDetaylariPanels.Controls.Add(urunItemPanel);
                yPos += itemHeight + margin;
            }
            bgl.baglanti().Close();

            // Toplam fiyatı label6'ya yazdır
            SqlCommand getTotalPriceKomut = new SqlCommand(@"SELECT ISNULL(SUM(Adet * BirimFiyat), 0) FROM SatisHareket WHERE MasaID = @masaID AND Aktif = 1", bgl.baglanti());
            getTotalPriceKomut.Parameters.AddWithValue("@masaID", _masaID);
            decimal currentTableTotalPrice = Convert.ToDecimal(getTotalPriceKomut.ExecuteScalar());
            bgl.baglanti().Close();

            // Bilardo masası ise ek ücreti hesapla ve toplam fiyata ekle
            if (_masaTuru == "Bilardo")
            {
                // Sadece açık masalarda ek ücret hesapla ve süre göster
                if (MasaAcikMi())
                {
                    decimal ekUcret = HesaplaBilardoEkUcreti();
                    decimal sinirsizUcret = HesaplaSinirsizBilardoUcreti();
                    
                    // Sınırsız bilardo masası kontrolü
                    bool sinirsizBilardoMasa = false;
                    try
                    {
                        SqlCommand sinirsizKontrol = new SqlCommand(@"SELECT COUNT(*) FROM SatisHareket AS SH 
                                                                      INNER JOIN Urunler AS U ON SH.UrunID = U.UrunID 
                                                                      WHERE SH.MasaID = @masaID AND SH.Aktif = 1 
                                                                      AND U.UrunAdi = 'Bilardo'", bgl.baglanti());
                        sinirsizKontrol.Parameters.AddWithValue("@masaID", _masaID);
                        int sinirsizCount = Convert.ToInt32(sinirsizKontrol.ExecuteScalar());
                        bgl.baglanti().Close();
                        sinirsizBilardoMasa = sinirsizCount > 0;
                    }
                    catch (Exception ex)
                    {
                        // Hata durumunda false olarak devam et
                    }
                    
                    if (sinirsizBilardoMasa)
                    {
                        // Sınırsız bilardo masası - sadece süre ücreti
                        currentTableTotalPrice = sinirsizUcret;
                    }
                    else
                    {
                        // Saatlik bilardo masası - normal ürünler + ek ücret
                        currentTableTotalPrice += ekUcret + sinirsizUcret;
                    }
                    
                    LoadBilardoSuresi();
                    
                    // Ek ücret varsa masa ürünlerine ek ücret item'ı ekle
                    if (ekUcret > 0)
                    {
                        EkUcretItemEkle(ekUcret);
                        // Debug için konsola yazdır
                        Console.WriteLine($"Ek ücret eklendi: {ekUcret}");
                    }
                    else
                    {
                        // Debug için konsola yazdır
                        Console.WriteLine($"Ek ücret yok: {ekUcret}");
                    }
                }
            }

            label6.Text = "₺" + currentTableTotalPrice.ToString("N2");
        }

        private void LoadUrunler(string kategori = "Genel")
        {
            urunPanels.Controls.Clear();

            string sql = "SELECT UrunID, UrunAdi, Fiyat, Resim FROM Urunler WHERE Durum=1";
            if (kategori == "Genel")
            {
                if (_masaTuru == "Normal")
                    sql += " AND (KategoriID = 1 OR KategoriID = 2)"; // 1: İçecekler, 2: Yiyecekler
                else if (_masaTuru == "Bilardo")
                    sql += " AND (KategoriID = 1 OR KategoriID = 2 OR KategoriID = 3)"; // 1: İçecekler, 2: Yiyecekler, 3: Masa Oyunları
            }
            else if (kategori == "İçecekler")
                sql += " AND KategoriID = 1"; // 1: İçecekler (veritabanınızda kategori id'si farklıysa değiştirin)
            else if (kategori == "Yiyecekler")
                sql += " AND KategoriID = 2"; // 2: Yiyecekler
            else if (kategori == "Masa Oyunları")
                sql += " AND KategoriID = 3"; // 3: Masa Oyunları

            SqlCommand komut = new SqlCommand(sql, bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();

            int x = 10;
            int y = 10;
            int cardWidth = 120;
            int cardHeight = 120;
            int priceLabelHeight = 24; // Biraz daha yüksek tutmak daha iyi olur
            int margin = 10;
            int cardsPerLine = 5;
            int cardCount = 0;

            while (dr.Read())
            {
                Panel urunCard = new Panel
                {
                    Width = cardWidth,
                    Height = cardHeight,
                    BackColor = Color.White,
                    Location = new Point(x, y),
                    Tag = dr["UrunID"],
                    Cursor = Cursors.Hand
                };

                ControlStyles.ApplyBorderRadius(urunCard, 10);

                bool resimYuklendi = false;
                PictureBox pbUrunResim = new PictureBox
                {
                    Width = cardWidth - 20,
                    Height = cardHeight / 2 - 10,
                    Location = new Point(10, 10),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BackColor = Color.Transparent,
                    Cursor = Cursors.Hand
                };

                if (dr["Resim"] != DBNull.Value && !string.IsNullOrEmpty(dr["Resim"].ToString()))
                {
                    try
                    {
                        pbUrunResim.Load(dr["Resim"].ToString());
                        resimYuklendi = true;
                    }
                    catch
                    {
                        resimYuklendi = false;
                    }
                }

                if (resimYuklendi)
                {
                    pbUrunResim.Click += (s, ev) => Urun_Click(urunCard, ev);
                    urunCard.Controls.Add(pbUrunResim);

                    // Ürün adı (resmin altında)
                    Label lblUrunAd = new Label
                    {
                        Text = dr["UrunAdi"].ToString(),
                        Font = new Font("Segoe UI", 9, FontStyle.Bold),
                        AutoSize = false,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Width = cardWidth,
                        Height = 20,
                        Location = new Point(0, (cardHeight / 2) + 5),
                        ForeColor = Color.Black,
                        Cursor = Cursors.Hand
                    };
                    lblUrunAd.Click += (s, ev) => Urun_Click(urunCard, ev);
                    urunCard.Controls.Add(lblUrunAd);

                    // Fiyat (en altta)
                    Label lblFiyat = new Label
                    {
                        Text = "₺" + Convert.ToDecimal(dr["Fiyat"]).ToString("N0"),
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        AutoSize = false,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Width = cardWidth,
                        Height = priceLabelHeight,
                        Location = new Point(0, cardHeight - priceLabelHeight - 5), // 5px yukarıdan boşluk bırak
                        ForeColor = Color.FromArgb(0, 150, 0),
                        Cursor = Cursors.Hand
                    };
                    lblFiyat.Click += (s, ev) => Urun_Click(urunCard, ev);
                    urunCard.Controls.Add(lblFiyat);
                }
                else
                {
                    // Ürün adı ve fiyatı birlikte ortala (kartın tam ortasında)
                    Label lblUrunAdResimYok = new Label
                    {
                        Text = dr["UrunAdi"].ToString(),
                        Font = new Font("Segoe UI", 14, FontStyle.Bold),
                        AutoSize = false,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Width = cardWidth - 20,
                        Height = cardHeight - 40,
                        Location = new Point(10, 10),
                        ForeColor = Color.Gray,
                        Cursor = Cursors.Hand
                    };
                    lblUrunAdResimYok.Click += (s, ev) => Urun_Click(urunCard, ev);
                    urunCard.Controls.Add(lblUrunAdResimYok);


                    // Fiyat (en altta)
                    Label lblFiyat = new Label
                    {
                        Text = "₺" + Convert.ToDecimal(dr["Fiyat"]).ToString("N0"),
                        Font = new Font("Segoe UI", 10, FontStyle.Bold),
                        AutoSize = false,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Width = cardWidth,
                        Height = priceLabelHeight,
                        Location = new Point(0, cardHeight - priceLabelHeight - 5), // 5px yukarıdan boşluk bırak
                        ForeColor = Color.FromArgb(0, 150, 0),
                        Cursor = Cursors.Hand
                    };
                    lblFiyat.Click += (s, ev) => Urun_Click(urunCard, ev);
                    urunCard.Controls.Add(lblFiyat);
                }

                urunCard.Click += Urun_Click;
                urunPanels.Controls.Add(urunCard);

                x += cardWidth + margin;
                cardCount++;

                if (cardCount % cardsPerLine == 0)
                {
                    x = 10;
                    y += cardHeight + margin;
                }
            }
            bgl.baglanti().Close();
        }

        private void Urun_Click(object sender, EventArgs e)
        {
            Panel clickedUrunCard = (Panel)sender;
            int urunID = Convert.ToInt32(clickedUrunCard.Tag);

            decimal birimFiyat = 0;
            // Ürünün fiyatını al
            SqlCommand getPriceKomut = new SqlCommand("SELECT Fiyat FROM Urunler WHERE UrunID = @p1 AND Durum=1", bgl.baglanti());
            getPriceKomut.Parameters.AddWithValue("@p1", urunID);
            SqlDataReader priceDr = getPriceKomut.ExecuteReader();
            if (priceDr.Read())
            {
                birimFiyat = Convert.ToDecimal(priceDr["Fiyat"]);
            }
            bgl.baglanti().Close();

            // Mevcut siparişte ürün var mı kontrol et
            SqlCommand checkKomut = new SqlCommand("SELECT Id, Adet FROM SatisHareket WHERE MasaID = @p1 AND UrunID = @p2 AND Aktif = 1", bgl.baglanti());
            checkKomut.Parameters.AddWithValue("@p1", _masaID);
            checkKomut.Parameters.AddWithValue("@p2", urunID);
            SqlDataReader checkDr = checkKomut.ExecuteReader();

            if (checkDr.Read()) // Ürün zaten varsa adeti artır
            {
                int satisHareketId = Convert.ToInt32(checkDr["Id"]);
                int mevcutAdet = Convert.ToInt32(checkDr["Adet"]);
                bgl.baglanti().Close();

                SqlCommand updateKomut = new SqlCommand("UPDATE SatisHareket SET Adet = @adet, Tarih = @tarih WHERE Id = @id", bgl.baglanti());
                updateKomut.Parameters.AddWithValue("@adet", mevcutAdet + 1);
                updateKomut.Parameters.AddWithValue("@tarih", DateTime.Now);
                updateKomut.Parameters.AddWithValue("@id", satisHareketId);
                updateKomut.ExecuteNonQuery();
                bgl.baglanti().Close();
            }
            else // Ürün yoksa yeni kayıt ekle
            {
                bgl.baglanti().Close();
                SqlCommand insertKomut = new SqlCommand("INSERT INTO SatisHareket (MasaID, UrunID, Adet, BirimFiyat, Tarih, Aktif) VALUES (@masaID, @urunID, @adet, @birimFiyat, @tarih, @aktif)", bgl.baglanti());
                insertKomut.Parameters.AddWithValue("@masaID", _masaID);
                insertKomut.Parameters.AddWithValue("@urunID", urunID);
                insertKomut.Parameters.AddWithValue("@adet", 1);
                insertKomut.Parameters.AddWithValue("@birimFiyat", birimFiyat);
                insertKomut.Parameters.AddWithValue("@tarih", DateTime.Now);
                insertKomut.Parameters.AddWithValue("@aktif", true);
                insertKomut.ExecuteNonQuery();
                bgl.baglanti().Close();
            }

            // Masanın durumunu güncelle (eğer boşsa doluya çevir ve açılış saatini ayarla)
            SqlCommand masaDurumKomut = new SqlCommand("SELECT Durum FROM Masalar WHERE MasaID = @p1", bgl.baglanti());
            masaDurumKomut.Parameters.AddWithValue("@p1", _masaID);
            SqlDataReader masaDr = masaDurumKomut.ExecuteReader();
            bool currentMasaDurum = false;
            if (masaDr.Read())
            {
                currentMasaDurum = Convert.ToBoolean(masaDr["Durum"]);
            }
            bgl.baglanti().Close();

            if (!currentMasaDurum) // Masa boşsa
            {
                SqlCommand updateMasaKomut = new SqlCommand("UPDATE Masalar SET Durum = @durum WHERE MasaID = @masaID", bgl.baglanti());
                updateMasaKomut.Parameters.AddWithValue("@durum", true);
                updateMasaKomut.Parameters.AddWithValue("@masaID", _masaID);
                updateMasaKomut.ExecuteNonQuery();
                bgl.baglanti().Close();
            }

            LoadMasaUrunleri(); // Ürün listesini yenile

            // FrmSatisEkrani'ndaki masaları ve dolu masalar listesini yenile
            if (_parentForm != null)
            {
                //_parentForm.MasaSayisiniKontrolEt();
                _parentForm.MasalariYenile();
            }
        }

        private void btnOdeme_Click(object sender, EventArgs e)
        {
            FrmOdemeYontemleri frOy = new FrmOdemeYontemleri(_masaID, this); // MasaID ve bu formun referansını ilet
            var result = frOy.ShowDialog(); // ShowDialog ile ödeme işlemi bitene kadar bu formu kilitler

            // Sadece ödeme işlemi başarılıysa SatisEkrani'ni öne getir
            if (result == DialogResult.OK && _parentForm != null)
            {
                //_parentForm.MasaSayisiniKontrolEt();
                _parentForm.MasalariYenile();
                _parentForm.Refresh();

                if (_parentForm.WindowState == FormWindowState.Minimized)
                {
                    _parentForm.WindowState = FormWindowState.Normal;
                }
                _parentForm.Visible = true;
                _parentForm.Show();
                _parentForm.BringToFront();
                _parentForm.Activate();
            }
        }

        private void btnGenel_Click(object sender, EventArgs e)
        {
            ApplySelectedButtonStyle(btnGenel);
            LoadUrunler("Genel");
        }

        private void btnicecekler_Click(object sender, EventArgs e)
        {
            ApplySelectedButtonStyle(btnicecekler);
            LoadUrunler("İçecekler");
        }

        private void btnYiyecekler_Click(object sender, EventArgs e)
        {
            ApplySelectedButtonStyle(btnYiyecekler);
            LoadUrunler("Yiyecekler");
        }

        private void UrunSil(int masaID, int urunID)
        {
            using (SqlConnection conn = bgl.baglanti())
            {
                SqlCommand cmdSil = new SqlCommand("DELETE FROM SatisHareket WHERE MasaID = @masaID AND UrunID = @urunID AND Aktif = 1", conn);
                cmdSil.Parameters.AddWithValue("@masaID", masaID);
                cmdSil.Parameters.AddWithValue("@urunID", urunID);
                cmdSil.ExecuteNonQuery();
            }
            // Masa detaylarını yenile
            LoadMasaUrunleri();

            // Ana satış ekranındaki kartları da yenile
            if (_parentForm != null)
            {
                //_parentForm.MasaSayisiniKontrolEt();
                _parentForm.MasalariYenile();
            }

            // Ürün silindikten sonra masada hiç ürün kalmadıysa masayı kapatmayı sor
            if (MasaUrunKalmadiMi())
            {
                if (MessageBox.Show("Masadaki tüm ürünler silindi. Masayı kapatmak istiyor musunuz?", "Masa Kapat", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MasaKapat();
                }
            }
        }

        // Masada hiç ürün kalmadıysa true döner
        private bool MasaUrunKalmadiMi()
        {
            using (SqlConnection conn = bgl.baglanti())
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM SatisHareket WHERE MasaID = @masaID AND Aktif = 1", conn);
                cmd.Parameters.AddWithValue("@masaID", _masaID);
                int kalan = Convert.ToInt32(cmd.ExecuteScalar());
                return kalan == 0;
            }
        }

        // Masayı kapatır (ürün yoksa)
        private void MasaKapat()
        {
            using (SqlConnection conn = bgl.baglanti())
            {
                SqlCommand cmdMasa = new SqlCommand("UPDATE Masalar SET Durum = 0 WHERE MasaID = @masaID", conn);
                cmdMasa.Parameters.AddWithValue("@masaID", _masaID);
                cmdMasa.ExecuteNonQuery();
            }
            if (_parentForm != null)
            {
                //_parentForm.MasaSayisiniKontrolEt();
                _parentForm.MasalariYenile();
            }
            MessageBox.Show("Masa kapatıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void UrunAdetArttir(int masaID, int urunID)
        {
            using (SqlConnection conn = bgl.baglanti())
            {
                SqlCommand cmd = new SqlCommand("UPDATE SatisHareket SET Adet = Adet + 1 WHERE MasaID = @masaID AND UrunID = @urunID AND Aktif = 1", conn);
                cmd.Parameters.AddWithValue("@masaID", masaID);
                cmd.Parameters.AddWithValue("@urunID", urunID);
                cmd.ExecuteNonQuery();
            }
            LoadMasaUrunleri();
            if (_parentForm != null)
            {
                //_parentForm.MasaSayisiniKontrolEt();
                _parentForm.MasalariYenile();
            }
        }

        private void UrunAdetAzalt(int masaID, int urunID)
        {
            using (SqlConnection conn = bgl.baglanti())
            {
                SqlCommand cmd = new SqlCommand("UPDATE SatisHareket SET Adet = Adet - 1 WHERE MasaID = @masaID AND UrunID = @urunID AND Aktif = 1 AND Adet > 1", conn);
                cmd.Parameters.AddWithValue("@masaID", masaID);
                cmd.Parameters.AddWithValue("@urunID", urunID);
                cmd.ExecuteNonQuery();
            }
            LoadMasaUrunleri();
            if (_parentForm != null)
            {
                //_parentForm.MasaSayisiniKontrolEt();
                _parentForm.MasalariYenile();
            }
        }

        private void UrunSilOnay(int masaID, int urunID)
        {
            if (MessageBox.Show("Bu ürünü silmek istediğinize emin misiniz?", "Ürün Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                UrunSil(masaID, urunID);
            }
        }

        private void btnTasi_Click(object sender, EventArgs e)
        {
            if (_parentForm != null)
            {
                _parentForm.MasaTasiModu = true;
                _parentForm.KaynakMasaID = _masaID;
                _parentForm.BringToFront();
                _parentForm.Activate();
                MessageBox.Show("Lütfen hesabı taşımak istediğiniz masaya tıklayın.", "Masa Taşıma", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void btnMasaiptal_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bu masanın hesabını iptal etmek istediğinize emin misiniz?", "İptal Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection conn = bgl.baglanti())
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM SatisHareket WHERE MasaID = @masaID AND Aktif = 1", conn);
                    cmd.Parameters.AddWithValue("@masaID", _masaID);
                    cmd.ExecuteNonQuery();

                    // Masa durumu boş yapılabilir:
                    SqlCommand cmdMasa = new SqlCommand("UPDATE Masalar SET Durum = 0 WHERE MasaID = @masaID", conn);
                    cmdMasa.Parameters.AddWithValue("@masaID", _masaID);
                    cmdMasa.ExecuteNonQuery();
                }
                MessageBox.Show("Masa hesabı iptal edildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Ana ekranı güncelle
                if (_parentForm != null)
                {
                    //_parentForm.MasaSayisiniKontrolEt();
                    _parentForm.MasalariYenile();
                }
                this.Close();
            }
        }

        // btnGeri butonuna tıklanınca MasaDetay formunu kapatıp SatisEkrani'ni öne al
        private void btnGeri_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

    }
}
