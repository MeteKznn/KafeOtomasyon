using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KahveOtomasyonProjesi
{
    public partial class OzetUserControl : UserControl
    {

        private string imagesPath = Path.Combine(Application.StartupPath, "images");
        private sqlbaglantisi bgl = new sqlbaglantisi();
        private ToolTip toolTip = new ToolTip();
        public OzetUserControl()
        {
            InitializeComponent();
            this.Load += OzetUserControl_Load;
        }

        private void OzetUserControl_Load(object sender, EventArgs e)
        {
            PanelleriGuzellestir();
            VerileriDbdenYukleVeGuncelle();
            PictureBoxToolTipAyarla();
        }

        private void PanelleriGuzellestir()
        {
            List<Panel> panels = new List<Panel> { panel2, panel3, panel4, panel5, panel6, panel7, panel8, panel9, panel10 };
            foreach (var pnl in panels)
            {
                //pnl.BackColor = Color.FromArgb(245, 245, 250);
                pnl.BackColor = ColorTranslator.FromHtml("#fefefe");
                pnl.BorderStyle = BorderStyle.None;
                ControlStyles.ApplyBorderRadius(pnl, 18, RoundedCorners.All);
                pnl.Padding = new Padding(8);
                //pnl.ShadowDecoration(); // Eğer özel bir shadow fonksiyonun yoksa bu satırı kaldırabilirsin
            }
        }

        private void VerileriDbdenYukleVeGuncelle()
        {
            // Default olarak uptrend.png ata
            SetDefaultTrendIcon(pictureBox2);
            SetDefaultTrendIcon(pictureBox3);
            SetDefaultTrendIcon(pictureBox4);
            SetDefaultTrendIcon(pictureBox5);
            SetDefaultTrendIcon(pictureBox6);
            SetDefaultTrendIcon(pictureBox7);
            SetDefaultTrendIcon(pictureBox8);
            SetDefaultTrendIcon(pictureBox9);
            SetDefaultTrendIcon(pictureBox10);

            try
            {
                using (SqlConnection conn = bgl.baglanti())
                {
                    // 1. Günlük Satış Adedi (Siparişler tablosundan)
                    object bugunSatisObj = new SqlCommand("SELECT COUNT(*) FROM Siparisler WHERE CAST(TarihSaat AS DATE) = CAST(GETDATE() AS DATE)", conn).ExecuteScalar();
                    object dunSatisObj = new SqlCommand("SELECT COUNT(*) FROM Siparisler WHERE CAST(TarihSaat AS DATE) = CAST(DATEADD(day, -1, GETDATE()) AS DATE)", conn).ExecuteScalar();
                    int bugunSatis = (bugunSatisObj == DBNull.Value || bugunSatisObj == null) ? 0 : Convert.ToInt32(bugunSatisObj);
                    int dunSatis = (dunSatisObj == DBNull.Value || dunSatisObj == null) ? 0 : Convert.ToInt32(dunSatisObj);
                    lbl_gunluksatisadedi.Text = bugunSatis.ToString();
                    SetTrendIconWithTooltip(pictureBox2, dunSatis, bugunSatis, "Günlük satış adedi (sipariş sayısı)");

                    // 2. 08:00 - 18:00 arası toplam ciro (Siparişler tablosundan)
                    object gunduzCiroObj = new SqlCommand("SELECT ISNULL(SUM(ToplamFiyat), 0) FROM Siparisler WHERE CAST(TarihSaat AS DATE) = CAST(GETDATE() AS DATE) AND DATEPART(HOUR, TarihSaat) >= 8 AND DATEPART(HOUR, TarihSaat) < 18", conn).ExecuteScalar();
                    object dunGunduzCiroObj = new SqlCommand("SELECT ISNULL(SUM(ToplamFiyat), 0) FROM Siparisler WHERE CAST(TarihSaat AS DATE) = CAST(DATEADD(day, -1, GETDATE()) AS DATE) AND DATEPART(HOUR, TarihSaat) >= 8 AND DATEPART(HOUR, TarihSaat) < 18", conn).ExecuteScalar();
                    decimal gunduzCiro = (gunduzCiroObj == DBNull.Value || gunduzCiroObj == null) ? 0 : Convert.ToDecimal(gunduzCiroObj);
                    decimal dunGunduzCiro = (dunGunduzCiroObj == DBNull.Value || dunGunduzCiroObj == null) ? 0 : Convert.ToDecimal(dunGunduzCiroObj);
                    lbl_gunduzCirosu.Text = "₺" + gunduzCiro.ToString("N2");
                    SetTrendIconWithTooltip(pictureBox3, dunGunduzCiro, gunduzCiro, "08:00-18:00 arası ciro");

                    // 3. Masa Başına Ortalama Kazanç (Siparişler tablosundan)
                    object bugunOrtalamaKazancObj = new SqlCommand("SELECT AVG(ToplamFiyat) FROM Siparisler WHERE CAST(TarihSaat AS DATE) = CAST(GETDATE() AS DATE)", conn).ExecuteScalar();
                    object dunOrtalamaKazancObj = new SqlCommand("SELECT AVG(ToplamFiyat) FROM Siparisler WHERE CAST(TarihSaat AS DATE) = CAST(DATEADD(day, -1, GETDATE()) AS DATE)", conn).ExecuteScalar();
                    decimal bugunOrtalamaKazanc = (bugunOrtalamaKazancObj == DBNull.Value || bugunOrtalamaKazancObj == null) ? 0 : Convert.ToDecimal(bugunOrtalamaKazancObj);
                    decimal dunOrtalamaKazanc = (dunOrtalamaKazancObj == DBNull.Value || dunOrtalamaKazancObj == null) ? 0 : Convert.ToDecimal(dunOrtalamaKazancObj);
                    lbl_MasaBasınaOrtalamaKazanc.Text = bugunOrtalamaKazanc.ToString("N2");
                    SetTrendIconWithTooltip(pictureBox4, dunOrtalamaKazanc, bugunOrtalamaKazanc, "Masa başına ortalama kazanç");

                    // 4. Bugün açılan adisyon (Siparişler tablosundan)
                    object bugunAdisyonObj = new SqlCommand("SELECT COUNT(*) FROM Siparisler WHERE CAST(TarihSaat AS DATE) = CAST(GETDATE() AS DATE)", conn).ExecuteScalar();
                    object dunAdisyonObj = new SqlCommand("SELECT COUNT(*) FROM Siparisler WHERE CAST(TarihSaat AS DATE) = CAST(DATEADD(day, -1, GETDATE()) AS DATE)", conn).ExecuteScalar();
                    int bugunAdisyon = (bugunAdisyonObj == DBNull.Value || bugunAdisyonObj == null) ? 0 : Convert.ToInt32(bugunAdisyonObj);
                    int dunAdisyon = (dunAdisyonObj == DBNull.Value || dunAdisyonObj == null) ? 0 : Convert.ToInt32(dunAdisyonObj);
                    lbl_bugunAcilanToplamAdisyon.Text = bugunAdisyon.ToString();
                    SetTrendIconWithTooltip(pictureBox5, dunAdisyon, bugunAdisyon, "Bugün açılan adisyon sayısı");

                    // 5. Bugünkü Nakit vs Kart Dağılımı (Siparisler tablosundan)
                    object bugunNakitObj = new SqlCommand("SELECT ISNULL(SUM(ToplamFiyat),0) FROM Siparisler WHERE OdemeTipi = 'Nakit' AND CAST(TarihSaat AS DATE) = CAST(GETDATE() AS DATE)", conn).ExecuteScalar();
                    object bugunKartObj = new SqlCommand("SELECT ISNULL(SUM(ToplamFiyat),0) FROM Siparisler WHERE OdemeTipi = 'Pos' AND CAST(TarihSaat AS DATE) = CAST(GETDATE() AS DATE)", conn).ExecuteScalar();
                    object dunNakitObj = new SqlCommand("SELECT ISNULL(SUM(ToplamFiyat),0) FROM Siparisler WHERE OdemeTipi = 'Nakit' AND CAST(TarihSaat AS DATE) = CAST(DATEADD(day, -1, GETDATE()) AS DATE)", conn).ExecuteScalar();
                    object dunKartObj = new SqlCommand("SELECT ISNULL(SUM(ToplamFiyat),0) FROM Siparisler WHERE OdemeTipi = 'Pos' AND CAST(TarihSaat AS DATE) = CAST(DATEADD(day, -1, GETDATE()) AS DATE)", conn).ExecuteScalar();
                    decimal bugunNakit = (bugunNakitObj == DBNull.Value || bugunNakitObj == null) ? 0 : Convert.ToDecimal(bugunNakitObj);
                    decimal bugunKart = (bugunKartObj == DBNull.Value || bugunKartObj == null) ? 0 : Convert.ToDecimal(bugunKartObj);
                    decimal dunNakit = (dunNakitObj == DBNull.Value || dunNakitObj == null) ? 0 : Convert.ToDecimal(dunNakitObj);
                    decimal dunKart = (dunKartObj == DBNull.Value || dunKartObj == null) ? 0 : Convert.ToDecimal(dunKartObj);
                    decimal toplam = bugunNakit + bugunKart;
                    decimal nakitYuzde = toplam == 0 ? 0 : (bugunNakit / toplam) * 100;
                    decimal kartYuzde = toplam == 0 ? 0 : (bugunKart / toplam) * 100;
                    lbl_bugunkuNakitvsKartDagilimi.Text = $"Nakit: %{nakitYuzde:N0} \nKart: %{kartYuzde:N0}";
                    SetTrendIconWithTooltip(pictureBox6, dunNakit + dunKart, bugunNakit + bugunKart, "Bugünkü nakit/kart dağılımı");

                    // 6. Günlük Ortalama Sipariş Tutarı (Siparişler tablosundan)
                    object bugunOrtalamaSiparisObj = new SqlCommand("SELECT AVG(ToplamFiyat) FROM Siparisler WHERE CAST(TarihSaat AS DATE) = CAST(GETDATE() AS DATE)", conn).ExecuteScalar();
                    object dunOrtalamaSiparisObj = new SqlCommand("SELECT AVG(ToplamFiyat) FROM Siparisler WHERE CAST(TarihSaat AS DATE) = CAST(DATEADD(day, -1, GETDATE()) AS DATE)", conn).ExecuteScalar();
                    decimal bugunOrtalamaSiparis = (bugunOrtalamaSiparisObj == DBNull.Value || bugunOrtalamaSiparisObj == null) ? 0 : Convert.ToDecimal(bugunOrtalamaSiparisObj);
                    decimal dunOrtalamaSiparis = (dunOrtalamaSiparisObj == DBNull.Value || dunOrtalamaSiparisObj == null) ? 0 : Convert.ToDecimal(dunOrtalamaSiparisObj);
                    lbl_gunlukOrtalamaSiparisTutari.Text = bugunOrtalamaSiparis.ToString("N2");
                    SetTrendIconWithTooltip(pictureBox7, dunOrtalamaSiparis, bugunOrtalamaSiparis, "Günlük ortalama sipariş tutarı");

                    // 7. 18:00 - 00:00 arası toplam ciro (Siparişler tablosundan)
                    object geceCiroObj = new SqlCommand("SELECT ISNULL(SUM(ToplamFiyat), 0) FROM Siparisler WHERE CAST(TarihSaat AS DATE) = CAST(GETDATE() AS DATE) AND DATEPART(HOUR, TarihSaat) >= 18 AND DATEPART(HOUR, TarihSaat) < 24", conn).ExecuteScalar();
                    object dunGeceCiroObj = new SqlCommand("SELECT ISNULL(SUM(ToplamFiyat), 0) FROM Siparisler WHERE CAST(TarihSaat AS DATE) = CAST(DATEADD(day, -1, GETDATE()) AS DATE) AND DATEPART(HOUR, TarihSaat) >= 18 AND DATEPART(HOUR, TarihSaat) < 24", conn).ExecuteScalar();
                    decimal geceCiro = (geceCiroObj == DBNull.Value || geceCiroObj == null) ? 0 : Convert.ToDecimal(geceCiroObj);
                    decimal dunGeceCiro = (dunGeceCiroObj == DBNull.Value || dunGeceCiroObj == null) ? 0 : Convert.ToDecimal(dunGeceCiroObj);
                    lbl_geceCirosu.Text = "₺" + geceCiro.ToString("N2");
                    SetTrendIconWithTooltip(pictureBox8, dunGeceCiro, geceCiro, "18:00-00:00 arası ciro");

                    // 8. Aylık Ciro (geçen ay ile karşılaştır) (Siparişler tablosundan)
                    object buAyCiroObj = new SqlCommand("SELECT ISNULL(SUM(ToplamFiyat), 0) FROM Siparisler WHERE YEAR(TarihSaat) = YEAR(GETDATE()) AND MONTH(TarihSaat) = MONTH(GETDATE())", conn).ExecuteScalar();
                    object gecenAyCiroObj = new SqlCommand("SELECT ISNULL(SUM(ToplamFiyat), 0) FROM Siparisler WHERE YEAR(TarihSaat) = YEAR(DATEADD(month, -1, GETDATE())) AND MONTH(TarihSaat) = MONTH(DATEADD(month, -1, GETDATE()))", conn).ExecuteScalar();
                    decimal buAyCiro = (buAyCiroObj == DBNull.Value || buAyCiroObj == null) ? 0 : Convert.ToDecimal(buAyCiroObj);
                    decimal gecenAyCiro = (gecenAyCiroObj == DBNull.Value || gecenAyCiroObj == null) ? 0 : Convert.ToDecimal(gecenAyCiroObj);
                    lbl_aylikCiro.Text = "₺" + buAyCiro.ToString("N2");
                    SetTrendIconWithTooltip(pictureBox9, gecenAyCiro, buAyCiro, "Aylık ciro");

                    // 9. Günlük Ciro (dün ile karşılaştır) (Siparişler tablosundan)
                    object bugunCiroObj = new SqlCommand("SELECT ISNULL(SUM(ToplamFiyat), 0) FROM Siparisler WHERE CAST(TarihSaat AS DATE) = CAST(GETDATE() AS DATE)", conn).ExecuteScalar();
                    object dunCiroObj = new SqlCommand("SELECT ISNULL(SUM(ToplamFiyat), 0) FROM Siparisler WHERE CAST(TarihSaat AS DATE) = CAST(DATEADD(day, -1, GETDATE()) AS DATE)", conn).ExecuteScalar();
                    decimal bugunCiro = (bugunCiroObj == DBNull.Value || bugunCiroObj == null) ? 0 : Convert.ToDecimal(bugunCiroObj);
                    decimal dunCiro = (dunCiroObj == DBNull.Value || dunCiroObj == null) ? 0 : Convert.ToDecimal(dunCiroObj);
                    lbl_gunlukCiro.Text = "₺" + bugunCiro.ToString("N2");
                    SetTrendIconWithTooltip(pictureBox10, dunCiro, bugunCiro, "Günlük ciro");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veriler yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void SetDefaultTrendIcon(PictureBox pb)
        {
            try { pb.Image = Image.FromFile(Path.Combine(imagesPath, "default_line.png")); } catch { pb.Image = null; }
        }

        private void SetTrendIconWithTooltip(PictureBox pb, decimal previous, decimal current, string baslik)
        {
            string trendText = "";
            double oran = 0;
            if (previous > 0)
                oran = ((double)(current - previous) / (double)previous) * 100.0;
            if (current > previous)
            {
                pb.Image = Image.FromFile(Path.Combine(imagesPath, "uptrend.png"));
                trendText = $"{baslik} düne göre %{Math.Abs(oran):N1} arttı.";
            }
            else if (current < previous)
            {
                pb.Image = Image.FromFile(Path.Combine(imagesPath, "downtrend.png"));
                trendText = $"{baslik} düne göre %{Math.Abs(oran):N1} azaldı.";
            }
            else
            {
                pb.Image = Image.FromFile(Path.Combine(imagesPath, "default_line.png"));
                trendText = $"{baslik} düne göre değişmedi.";
            }
            pb.Tag = trendText;
            toolTip.SetToolTip(pb, trendText);
        }

        private void PictureBoxToolTipAyarla()
        {
            pictureBox2.MouseHover += (s, e) => ShowToolTip((PictureBox)s);
            pictureBox3.MouseHover += (s, e) => ShowToolTip((PictureBox)s);
            pictureBox4.MouseHover += (s, e) => ShowToolTip((PictureBox)s);
            pictureBox5.MouseHover += (s, e) => ShowToolTip((PictureBox)s);
            pictureBox6.MouseHover += (s, e) => ShowToolTip((PictureBox)s);
            pictureBox7.MouseHover += (s, e) => ShowToolTip((PictureBox)s);
            pictureBox8.MouseHover += (s, e) => ShowToolTip((PictureBox)s);
            pictureBox9.MouseHover += (s, e) => ShowToolTip((PictureBox)s);
            pictureBox10.MouseHover += (s, e) => ShowToolTip((PictureBox)s);
        }
        private void ShowToolTip(PictureBox pb)
        {
            if (pb.Tag != null)
                toolTip.SetToolTip(pb, pb.Tag.ToString());
        }
    }
}
