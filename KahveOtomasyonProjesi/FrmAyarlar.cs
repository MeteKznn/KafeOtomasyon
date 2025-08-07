using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KahveOtomasyonProjesi
{
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }

        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            LoadUserControl(new AyarlarProfilUserControl());
            
            // ESC tuşu ile form kapatma
            this.KeyPreview = true;
            this.KeyDown += FrmAyarlar_KeyDown;
        }

        private void FrmAyarlar_KeyDown(object sender, KeyEventArgs e)
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
            ayarlarPanel.Controls.Clear();  // Önceki UserControl'ü kaldır
            userControl.Dock = DockStyle.Fill;
            ayarlarPanel.Controls.Add(userControl); // Yeni UserControl ekle
        }

        private void btnProfil_Click(object sender, EventArgs e)
        {
            LoadUserControl(new AyarlarProfilUserControl());
        }

        private void btnUrun_Click(object sender, EventArgs e)
        {
            LoadUserControl(new AyarlarUrunUserControl());
        }

        private void btnMasa_Click(object sender, EventArgs e)
        {
            LoadUserControl(new AyarlarMasaUserControl());
        }

        private void btnVeresiye_Click(object sender, EventArgs e)
        {
            LoadUserControl(new AyarlarVeresiyeUserControl());
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
