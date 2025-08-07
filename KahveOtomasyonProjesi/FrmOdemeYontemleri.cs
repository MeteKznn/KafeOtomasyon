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
    public partial class FrmOdemeYontemleri : Form
    {
        private int _masaID;
        private FrmMasaDetay _parentMasaDetayForm;
        sqlbaglantisi bgl = new sqlbaglantisi();

        public FrmOdemeYontemleri(int masaID, FrmMasaDetay parentMasaDetayForm)
        {
            InitializeComponent();
            _masaID = masaID;
            _parentMasaDetayForm = parentMasaDetayForm;
        }

        private void btniptal_Click(object sender, EventArgs e)
        {
            this.Close();
            if (_parentMasaDetayForm != null)
            {
                _parentMasaDetayForm.BringToFront();
                _parentMasaDetayForm.Activate();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                btnNakit.PerformClick();
                return true;
            }
            else if (keyData == Keys.F2)
            {
                btnPos.PerformClick();
                return true;
            }
            else if (keyData == Keys.F3)
            {
                btnVeresiye.PerformClick();
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                btniptal.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnNakit_Click(object sender, EventArgs e)
        {
            if (!TotalAmount())
                return;
            if (MessageBox.Show("Nakit ödeme işlemini gerçekleştirmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            ProcessPayment("Nakit");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnPos_Click(object sender, EventArgs e)
        {
            if (!TotalAmount())
                return;
            if (MessageBox.Show("Pos ile ödeme işlemini gerçekleştirmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            ProcessPayment("Pos");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnVeresiye_Click(object sender, EventArgs e)
        {
            if (!TotalAmount())
                return;
            if (MessageBox.Show("Veresiye ödeme işlemini gerçekleştirmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            using (FrmVeresiyeSatis fr = new FrmVeresiyeSatis())
            {
                if (fr.ShowDialog() == DialogResult.OK)
                {
                    int siparisID = ProcessPayment("Veresiye");
                    if (siparisID > 0)
                    {
                        // Veresiyeler tablosundaki kaydı SiparisID ile güncelle
                        using (var conn = new sqlbaglantisi().baglanti())
                        {
                            SqlCommand cmd = new SqlCommand("UPDATE Veresiyeler SET SiparisID = @SiparisID WHERE ID = @VeresiyeID", conn);
                            cmd.Parameters.AddWithValue("@SiparisID", siparisID);
                            cmd.Parameters.AddWithValue("@VeresiyeID", fr.VeresiyeID);
                            cmd.ExecuteNonQuery();

                            // Müşterinin ToplamBorc alanını güncelle
                            decimal siparisTutari = GetSiparisTutari(siparisID);
                            if (siparisTutari > 0)
                            {
                                SqlCommand borcCmd = new SqlCommand("UPDATE Musteriler SET ToplamBorc = ToplamBorc + @eklenenBorc WHERE ID = @MusteriID", conn);
                                borcCmd.Parameters.AddWithValue("@eklenenBorc", siparisTutari);
                                borcCmd.Parameters.AddWithValue("@MusteriID", fr.MusteriID);
                                borcCmd.ExecuteNonQuery();
                            }
                        }
                        //MessageBox.Show("Veresiye satış başarıyla kaydedildi...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
        }

        private bool TotalAmount()
        {
            decimal totalAmount = 0;

            // Masanın Toplam Borcunu Hesapla
            SqlCommand getToplamKomut = new SqlCommand(@"SELECT ISNULL(SUM(Adet * BirimFiyat), 0) AS ToplamBorc 
                                                          FROM SatisHareket 
                                                          WHERE MasaID = @masaID AND Aktif = 1", bgl.baglanti());
            getToplamKomut.Parameters.AddWithValue("@masaID", _masaID);
            object result = getToplamKomut.ExecuteScalar();
            if (result != null) totalAmount = Convert.ToDecimal(result);
            bgl.baglanti().Close();

            // Bilardo masası kontrolü ve ek ücret hesaplama
            bool isBilardoMasa = false;
            try
            {
                SqlCommand masaKomut = new SqlCommand("SELECT MasaAdi FROM Masalar WHERE MasaID = @p1", bgl.baglanti());
                masaKomut.Parameters.AddWithValue("@p1", _masaID);
                SqlDataReader masaDr = masaKomut.ExecuteReader();
                if (masaDr.Read())
                {
                    string masaAdi = masaDr["MasaAdi"].ToString();
                    if (masaAdi.Contains("Bilardo"))
                    {
                        isBilardoMasa = true;
                    }
                }
                bgl.baglanti().Close();
            }
            catch (Exception ex)
            {
                // Hata durumunda Normal olarak devam et
            }

            // Bilardo masası ise ek ücreti hesapla
            if (isBilardoMasa)
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
                    totalAmount = sinirsizUcret;
                }
                else
                {
                    // Saatlik bilardo masası - normal ürünler + ek ücret
                    totalAmount += ekUcret + sinirsizUcret;
                }
            }

            if (totalAmount == 0)
            {
                MessageBox.Show("Masada ödeme yapılacak ürün bulunmamaktadır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                if (_parentMasaDetayForm != null)
                {
                    _parentMasaDetayForm.Close();
                }
                return false;
            }

            return true;
        }

        private int ProcessPayment(string odemeTipi)
        {
            if (!TotalAmount())
                return 0;

            decimal totalAmount = 0;
            int siparisID = 0;

            // TotalAmount metodundan toplam değeri al
            using (var conn = bgl.baglanti())
            {
                SqlCommand getToplamKomut = new SqlCommand(@"SELECT ISNULL(SUM(Adet * BirimFiyat), 0) AS ToplamBorc 
                                                              FROM SatisHareket 
                                                              WHERE MasaID = @masaID AND Aktif = 1", conn);
                getToplamKomut.Parameters.AddWithValue("@masaID", _masaID);
                object result = getToplamKomut.ExecuteScalar();
                if (result != null) totalAmount = Convert.ToDecimal(result);
            }

            // Bilardo masası kontrolü ve ek ücret hesaplama
            bool isBilardoMasa = false;
            try
            {
                SqlCommand masaKomut = new SqlCommand("SELECT MasaAdi FROM Masalar WHERE MasaID = @p1", bgl.baglanti());
                masaKomut.Parameters.AddWithValue("@p1", _masaID);
                SqlDataReader masaDr = masaKomut.ExecuteReader();
                if (masaDr.Read())
                {
                    string masaAdi = masaDr["MasaAdi"].ToString();
                    if (masaAdi.Contains("Bilardo"))
                    {
                        isBilardoMasa = true;
                    }
                }
                bgl.baglanti().Close();
            }
            catch (Exception ex)
            {
                // Hata durumunda Normal olarak devam et
            }

            // Bilardo masası ise ek ücreti hesapla
            if (isBilardoMasa)
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
                    totalAmount = sinirsizUcret;
                }
                else
                {
                    // Saatlik bilardo masası - normal ürünler + ek ücret
                    totalAmount += ekUcret + sinirsizUcret;
                }
            }

            try
            {
                //2 Siparisler Tablosuna Kayıt Ekle
                SqlCommand insertSiparisKomut = new SqlCommand("INSERT INTO Siparisler (MasaID, TarihSaat, Durum, ToplamFiyat, OdemeTipi, KasiyerID) VALUES (@masaID, @tarihSaat, @durum, @toplamFiyat, @odemeTipi, @kasiyerID); SELECT SCOPE_IDENTITY();", bgl.baglanti());
                insertSiparisKomut.Parameters.AddWithValue("@masaID", _masaID);
                insertSiparisKomut.Parameters.AddWithValue("@tarihSaat", DateTime.Now);
                insertSiparisKomut.Parameters.AddWithValue("@durum", true); // Sipariş tamamlandı
                insertSiparisKomut.Parameters.AddWithValue("@toplamFiyat", totalAmount);
                insertSiparisKomut.Parameters.AddWithValue("@odemeTipi", odemeTipi);
                insertSiparisKomut.Parameters.AddWithValue("@kasiyerID", KullaniciBilgileri.KullaniciID);
                siparisID = Convert.ToInt32(insertSiparisKomut.ExecuteScalar());
                bgl.baglanti().Close();

                // *** YENİ EKLENECEK KOD: SatisHarekette SiparisID'yi güncelle ***
                SqlCommand updateSatisHareketSiparisID = new SqlCommand(
                    "UPDATE SatisHareket SET SiparisID = @siparisID WHERE MasaID = @masaID AND Aktif = 1 AND (SiparisID IS NULL OR SiparisID = 0)", bgl.baglanti());
                updateSatisHareketSiparisID.Parameters.AddWithValue("@siparisID", siparisID);
                updateSatisHareketSiparisID.Parameters.AddWithValue("@masaID", _masaID);
                updateSatisHareketSiparisID.ExecuteNonQuery();
                bgl.baglanti().Close();

                // 3. SatisHareket'ten SiparisDetayaÜrünleri Taşı (her satırı tek tek aktar)
                SqlCommand getSatisHareketKomut = new SqlCommand(
                    "SELECT UrunID, Adet, BirimFiyat FROM SatisHareket WHERE MasaID = @masaID AND Aktif = 1", bgl.baglanti());
                getSatisHareketKomut.Parameters.AddWithValue("@masaID", _masaID);
                SqlDataReader dr = getSatisHareketKomut.ExecuteReader();

                while (dr.Read())
                {
                    SqlCommand insertDetayKomut = new SqlCommand(
                        "INSERT INTO SiparisDetay (SiparisID, UrunID, Adet, Fiyat) VALUES (@siparisID, @urunID, @adet, @fiyat)", bgl.baglanti());
                    insertDetayKomut.Parameters.AddWithValue("@siparisID", siparisID);
                    insertDetayKomut.Parameters.AddWithValue("@urunID", dr["UrunID"]);
                    insertDetayKomut.Parameters.AddWithValue("@adet", dr["Adet"]);
                    insertDetayKomut.Parameters.AddWithValue("@fiyat", dr["BirimFiyat"]);
                    insertDetayKomut.ExecuteNonQuery();
                    bgl.baglanti().Close();
                }
                bgl.baglanti().Close();

                // 4. SatisHareket Kayıtlarını Güncelle (Aktif = 0 yap)
                SqlCommand updateSatisHareketKomut = new SqlCommand("UPDATE SatisHareket SET Aktif = 0 WHERE MasaID = @masaID AND Aktif = 1", bgl.baglanti());
                updateSatisHareketKomut.Parameters.AddWithValue("@masaID", _masaID);
                updateSatisHareketKomut.ExecuteNonQuery();
                bgl.baglanti().Close();

                // 5 Masa Durumunu Güncelle (Boş yap)
                SqlCommand updateMasaDurumKomut = new SqlCommand("UPDATE Masalar SET Durum = 0 WHERE MasaID = @masaID", bgl.baglanti());
                updateMasaDurumKomut.Parameters.AddWithValue("@masaID", _masaID);
                updateMasaDurumKomut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show($"{odemeTipi} ile {totalAmount:N2} ₺ ödeme alınmıştır. Masa kapatıldı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return siparisID;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ödeme işlemi sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            finally
            {
                // Formları Kapatma ve Ana Ekranı Yenileme
                this.Close(); // Ödeme formunu kapat
                if (_parentMasaDetayForm != null)
                {
                    _parentMasaDetayForm.Close();
                }
            }
        }

        private decimal GetSiparisTutari(int siparisID)
        {
            try
            {
                using (var conn = bgl.baglanti())
                {
                    SqlCommand cmd = new SqlCommand("SELECT ToplamFiyat FROM Siparisler WHERE SiparisID = @SiparisID", conn);
                    cmd.Parameters.AddWithValue("@SiparisID", siparisID);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                        return Convert.ToDecimal(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sipariş tutarı alınırken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return 0;
        }

        /// <summary>
        /// Bilardo masasının ek ücretini hesaplar
        /// </summary>
        /// <returns>Ek ücret miktarı</returns>
        private decimal HesaplaBilardoEkUcreti()
        {
            decimal ekUcret = 0;
            
            try
            {
                // Masa açık değilse ek ücret hesaplama
                if (!MasaAcikMi())
                {
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
                        }
                        else
                        {
                            ekUcret = 0; // Süre içinde ek ücret yok
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
    }
}
