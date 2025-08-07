using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace KahveOtomasyonProjesi
{
    public partial class Frmistatistikler : Form
    {
        // Önceki değerleri tutmak için örnek dictionary
        private Dictionary<string, decimal> previousValues = new Dictionary<string, decimal>();
        private Dictionary<string, decimal> currentValues = new Dictionary<string, decimal>();
        private string imagesPath = Path.Combine(Application.StartupPath, "images");
        private sqlbaglantisi bgl = new sqlbaglantisi();
        private ToolTip toolTip = new ToolTip();

        public Frmistatistikler()
        {
            InitializeComponent();
        }

        private void Frmistatistikler_Load(object sender, EventArgs e)
        {

            ApplySelectedButtonStyle(btnOzet);
            LoadUserControl(new OzetUserControl());
            
            // ESC tuşu ile form kapatma
            this.KeyPreview = true;
            this.KeyDown += Frmistatistikler_KeyDown;
        }

        private void Frmistatistikler_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC tuşuna basıldığında formu kapat
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                e.Handled = true;
            }
        }

        private void LoadUserControl(UserControl userControl)
        {
            panelSayfalar.Controls.Clear();  // Önceki UserControl'ü kaldır
            userControl.Dock = DockStyle.Fill;
            panelSayfalar.Controls.Add(userControl); // Yeni UserControl ekle
        }


        private void ResetButtonStyles()
        {
            Color defaultBackColor = ColorTranslator.FromHtml("#241740"); // Dark purple
            Color defaultForeColor = Color.White;
            int borderRadius = 0; // No rounding for unselected buttons

            btnOzet.BackColor = defaultBackColor;
            btnOzet.ForeColor = defaultForeColor;
            ControlStyles.ApplyBorderRadius(btnOzet, borderRadius, RoundedCorners.None);
            btnOzet.FlatStyle = FlatStyle.Flat;
            btnOzet.FlatAppearance.BorderSize = 0;

            btnCiroTakibi.BackColor = defaultBackColor;
            btnCiroTakibi.ForeColor = defaultForeColor;
            ControlStyles.ApplyBorderRadius(btnCiroTakibi, borderRadius, RoundedCorners.None);
            btnCiroTakibi.FlatStyle = FlatStyle.Flat;
            btnCiroTakibi.FlatAppearance.BorderSize = 0;

            btnMasaRaporlari.BackColor = defaultBackColor;
            btnMasaRaporlari.ForeColor = defaultForeColor;
            ControlStyles.ApplyBorderRadius(btnMasaRaporlari, borderRadius, RoundedCorners.None);
            btnMasaRaporlari.FlatStyle = FlatStyle.Flat;
            btnMasaRaporlari.FlatAppearance.BorderSize = 0;

            btnUrunRaporlari.BackColor = defaultBackColor;
            btnUrunRaporlari.ForeColor = defaultForeColor;
            ControlStyles.ApplyBorderRadius(btnUrunRaporlari, borderRadius, RoundedCorners.None);
            btnUrunRaporlari.FlatStyle = FlatStyle.Flat;
            btnUrunRaporlari.FlatAppearance.BorderSize = 0;

            // Add other buttons here as needed
        }

        private void ApplySelectedButtonStyle(Button button)
        {
            ResetButtonStyles(); // Reset all buttons first

            button.BackColor = ColorTranslator.FromHtml("#f5f5f5"); 
            button.ForeColor = Color.Black;
            ControlStyles.ApplyBorderRadius(button, 20, RoundedCorners.LeftSide); // Apply right-side rounding
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
        }

        private void btnOzet_Click(object sender, EventArgs e)
        {
            ApplySelectedButtonStyle(btnOzet);
            LoadUserControl(new OzetUserControl());
        }

        private void btnCiroTakibi_Click(object sender, EventArgs e)
        {
            ApplySelectedButtonStyle(btnCiroTakibi);
            LoadUserControl(new CiroTakipUserControl());
        }

        private void btnMasaRaporlari_Click(object sender, EventArgs e)
        {
            ApplySelectedButtonStyle(btnMasaRaporlari);
            LoadUserControl(new MasaRaporlarıUserControl());
        }

        private void btnUrunRaporlari_Click(object sender, EventArgs e)
        {
            ApplySelectedButtonStyle(btnUrunRaporlari);
            LoadUserControl(new UrunRaporlariUserControl());
        }

    }
}
