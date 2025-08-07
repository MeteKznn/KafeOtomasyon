namespace KahveOtomasyonProjesi
{
    partial class Frmistatistikler
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnMasaRaporlari = new System.Windows.Forms.Button();
            this.btnUrunRaporlari = new System.Windows.Forms.Button();
            this.btnCiroTakibi = new System.Windows.Forms.Button();
            this.btnOzet = new System.Windows.Forms.Button();
            this.panelSayfalar = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(23)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnMasaRaporlari);
            this.panel1.Controls.Add(this.btnUrunRaporlari);
            this.panel1.Controls.Add(this.btnCiroTakibi);
            this.panel1.Controls.Add(this.btnOzet);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 729);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(231)))), ((int)(((byte)(239)))));
            this.label2.Location = new System.Drawing.Point(33, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 32);
            this.label2.TabIndex = 4;
            this.label2.Text = "RAPORLAR";
            // 
            // btnMasaRaporlari
            // 
            this.btnMasaRaporlari.Location = new System.Drawing.Point(20, 230);
            this.btnMasaRaporlari.Name = "btnMasaRaporlari";
            this.btnMasaRaporlari.Size = new System.Drawing.Size(183, 50);
            this.btnMasaRaporlari.TabIndex = 3;
            this.btnMasaRaporlari.Text = "Masa Raporları";
            this.btnMasaRaporlari.UseVisualStyleBackColor = true;
            this.btnMasaRaporlari.Click += new System.EventHandler(this.btnMasaRaporlari_Click);
            // 
            // btnUrunRaporlari
            // 
            this.btnUrunRaporlari.Location = new System.Drawing.Point(20, 286);
            this.btnUrunRaporlari.Name = "btnUrunRaporlari";
            this.btnUrunRaporlari.Size = new System.Drawing.Size(183, 50);
            this.btnUrunRaporlari.TabIndex = 2;
            this.btnUrunRaporlari.Text = "Ürün Raporları";
            this.btnUrunRaporlari.UseVisualStyleBackColor = true;
            this.btnUrunRaporlari.Click += new System.EventHandler(this.btnUrunRaporlari_Click);
            // 
            // btnCiroTakibi
            // 
            this.btnCiroTakibi.Location = new System.Drawing.Point(20, 174);
            this.btnCiroTakibi.Name = "btnCiroTakibi";
            this.btnCiroTakibi.Size = new System.Drawing.Size(183, 50);
            this.btnCiroTakibi.TabIndex = 1;
            this.btnCiroTakibi.Text = "Ciro Takibi";
            this.btnCiroTakibi.UseVisualStyleBackColor = true;
            this.btnCiroTakibi.Click += new System.EventHandler(this.btnCiroTakibi_Click);
            // 
            // btnOzet
            // 
            this.btnOzet.Location = new System.Drawing.Point(20, 118);
            this.btnOzet.Name = "btnOzet";
            this.btnOzet.Size = new System.Drawing.Size(183, 50);
            this.btnOzet.TabIndex = 0;
            this.btnOzet.Text = "Özet";
            this.btnOzet.UseVisualStyleBackColor = true;
            this.btnOzet.Click += new System.EventHandler(this.btnOzet_Click);
            // 
            // panelSayfalar
            // 
            this.panelSayfalar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSayfalar.Location = new System.Drawing.Point(200, 0);
            this.panelSayfalar.Name = "panelSayfalar";
            this.panelSayfalar.Size = new System.Drawing.Size(1150, 729);
            this.panelSayfalar.TabIndex = 1;
            // 
            // Frmistatistikler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.panelSayfalar);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Frmistatistikler";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frmistatistikler";
            this.Load += new System.EventHandler(this.Frmistatistikler_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnMasaRaporlari;
        private System.Windows.Forms.Button btnUrunRaporlari;
        private System.Windows.Forms.Button btnCiroTakibi;
        private System.Windows.Forms.Button btnOzet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panelSayfalar;
    }
}