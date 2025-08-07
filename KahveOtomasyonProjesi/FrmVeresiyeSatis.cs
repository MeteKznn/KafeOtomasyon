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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KahveOtomasyonProjesi
{
    public partial class FrmVeresiyeSatis : Form
    {
        public int SiparisID { get; set; } // Satış ekranından atanacak
        public int VeresiyeID { get; set; } // Oluşturulan veresiye kaydının ID'si
        public int MusteriID { get; set; } // Seçilen müşterinin ID'si

        public FrmVeresiyeSatis()
        {
            InitializeComponent();
            this.Load += FrmVeresiyeSatis_Load;
            // ComboBox ayarları
            cmbMusteriler.DrawMode = DrawMode.OwnerDrawFixed;
            cmbMusteriler.ItemHeight = 40;
            cmbMusteriler.DrawItem += cmbMusteriler_DrawItem;
            cmbMusteriler.MeasureItem += cmbMusteriler_MeasureItem;
        }

        private void FrmVeresiyeSatis_Load(object sender, EventArgs e)
        {
            MusterileriYukle();
            
            // ESC tuşu ile form kapatma
            this.KeyPreview = true;
            this.KeyDown += FrmVeresiyeSatis_KeyDown;
        }

        private void FrmVeresiyeSatis_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC tuşuna basıldığında formu kapat
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                e.Handled = true;
            }
        }

        private void MusterileriYukle()
        {
            using (var conn = new sqlbaglantisi().baglanti())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT ID, MusteriAd + ' ' + MusteriSoyad AS MusteriAdSoyad FROM Musteriler ORDER BY MusteriAd, MusteriSoyad", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbMusteriler.DataSource = null;
                cmbMusteriler.DisplayMember = "MusteriAdSoyad";
                cmbMusteriler.ValueMember = "ID";
                cmbMusteriler.DataSource = dt;
                if (cmbMusteriler.Items.Count > 0)
                    cmbMusteriler.SelectedIndex = 0;
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            // Zorunlu alan kontrolü
            if (cmbMusteriler.SelectedValue == null || cmbMusteriler.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen müşteri seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var conn = new sqlbaglantisi().baglanti())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Veresiyeler (MusteriID, Aciklama, SiparisID, TarihSaat, Durum) VALUES (@MusteriID, @Aciklama, NULL, @TarihSaat, 0); SELECT SCOPE_IDENTITY();", conn);
                cmd.Parameters.AddWithValue("@MusteriID", Convert.ToInt32(cmbMusteriler.SelectedValue));
                cmd.Parameters.AddWithValue("@Aciklama", rchAciklama.Text.Trim());
                cmd.Parameters.AddWithValue("@TarihSaat", DateTime.Today);
                VeresiyeID = Convert.ToInt32(cmd.ExecuteScalar());
                
                // Seçilen müşteri ID'sini kaydet
                MusteriID = Convert.ToInt32(cmbMusteriler.SelectedValue);
            }

            MessageBox.Show("Veresiye satış başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btniptal_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnYeniMusteri_Click(object sender, EventArgs e)
        {
            using (FrmYeniMusteriKaydi frm = new FrmYeniMusteriKaydi())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    MusterileriYukle(); // Yeni müşteri eklendikten sonra listeyi yenile
                }
            }
        }

        private void cmbMusteriler_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            e.DrawBackground();
            string text = "";
            if (cmbMusteriler.Items[e.Index] is DataRowView row)
                text = row["MusteriAdSoyad"].ToString();
            else
                text = cmbMusteriler.Items[e.Index].ToString();
            using (Brush brush = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(text, e.Font, brush, e.Bounds);
            }
            e.DrawFocusRectangle();
        }

        private void cmbMusteriler_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 40; // Yüksekliği artır
        }
    }
}
