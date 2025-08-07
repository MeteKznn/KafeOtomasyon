using System;

namespace KahveOtomasyonProjesi
{
    public static class KullaniciBilgileri
    {
        public static string KullaniciAdi { get; set; }
        public static int RolID { get; set; }
        public static int KullaniciID { get; set; }
        public static string Telefon { get; set; }
        public static string Mail { get; set; }
        public static string Sifre { get; set; }

        public static bool YetkiKontrol(int gerekliRol)
        {
            return RolID == gerekliRol;
        }
    }
}