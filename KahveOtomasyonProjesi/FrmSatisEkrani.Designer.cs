namespace KahveOtomasyonProjesi
{
    partial class FrmSatisEkrani
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSatisEkrani));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBilardoMasalari = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnMasalar = new System.Windows.Forms.Button();
            this.btnDoluMasalar = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.doluMasaPanels = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panelKullanici = new System.Windows.Forms.Panel();
            this.labelKullaniciAdi = new System.Windows.Forms.Label();
            this.pictureBoxKullanici = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.masaPanels = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOnceki = new System.Windows.Forms.Button();
            this.btnSonraki = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panelKullanici.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxKullanici)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(23)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.btnBilardoMasalari);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnMasalar);
            this.panel1.Controls.Add(this.btnDoluMasalar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1170, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(180, 729);
            this.panel1.TabIndex = 0;
            // 
            // btnBilardoMasalari
            // 
            this.btnBilardoMasalari.FlatAppearance.BorderSize = 0;
            this.btnBilardoMasalari.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnBilardoMasalari.Location = new System.Drawing.Point(-3, 191);
            this.btnBilardoMasalari.Name = "btnBilardoMasalari";
            this.btnBilardoMasalari.Size = new System.Drawing.Size(183, 50);
            this.btnBilardoMasalari.TabIndex = 2;
            this.btnBilardoMasalari.Text = "Bilardo Masaları";
            this.btnBilardoMasalari.UseVisualStyleBackColor = true;
            this.btnBilardoMasalari.Click += new System.EventHandler(this.btnBilardoMasalari_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(231)))), ((int)(((byte)(239)))));
            this.label2.Location = new System.Drawing.Point(32, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "MASALAR";
            // 
            // btnMasalar
            // 
            this.btnMasalar.FlatAppearance.BorderSize = 0;
            this.btnMasalar.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnMasalar.Location = new System.Drawing.Point(-3, 135);
            this.btnMasalar.Name = "btnMasalar";
            this.btnMasalar.Size = new System.Drawing.Size(183, 50);
            this.btnMasalar.TabIndex = 1;
            this.btnMasalar.Text = "Masalar";
            this.btnMasalar.UseVisualStyleBackColor = true;
            this.btnMasalar.Click += new System.EventHandler(this.btnMasalar_Click);
            // 
            // btnDoluMasalar
            // 
            this.btnDoluMasalar.FlatAppearance.BorderSize = 0;
            this.btnDoluMasalar.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnDoluMasalar.Location = new System.Drawing.Point(-3, 79);
            this.btnDoluMasalar.Name = "btnDoluMasalar";
            this.btnDoluMasalar.Size = new System.Drawing.Size(183, 50);
            this.btnDoluMasalar.TabIndex = 0;
            this.btnDoluMasalar.Text = "Dolu Masalar";
            this.btnDoluMasalar.UseVisualStyleBackColor = true;
            this.btnDoluMasalar.Click += new System.EventHandler(this.btnDoluMasalar_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.doluMasaPanels);
            this.panel2.Controls.Add(this.panel7);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(395, 729);
            this.panel2.TabIndex = 1;
            // 
            // doluMasaPanels
            // 
            this.doluMasaPanels.AutoScroll = true;
            this.doluMasaPanels.AutoScrollMargin = new System.Drawing.Size(0, 10);
            this.doluMasaPanels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doluMasaPanels.Location = new System.Drawing.Point(0, 100);
            this.doluMasaPanels.Name = "doluMasaPanels";
            this.doluMasaPanels.Size = new System.Drawing.Size(395, 529);
            this.doluMasaPanels.TabIndex = 1;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.label7);
            this.panel7.Controls.Add(this.label6);
            this.panel7.Controls.Add(this.label5);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel7.Location = new System.Drawing.Point(0, 629);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(395, 100);
            this.panel7.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(185)))), ((int)(((byte)(188)))));
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(383, 23);
            this.label7.TabIndex = 2;
            this.label7.Text = "________________________________________________________________________";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(213, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(164, 45);
            this.label6.TabIndex = 1;
            this.label6.Text = "₺1.360,00";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(26, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 32);
            this.label5.TabIndex = 0;
            this.label5.Text = "TOPLAM";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panelKullanici);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(395, 100);
            this.panel5.TabIndex = 0;
            // 
            // panelKullanici
            // 
            this.panelKullanici.Controls.Add(this.labelKullaniciAdi);
            this.panelKullanici.Controls.Add(this.pictureBoxKullanici);
            this.panelKullanici.Location = new System.Drawing.Point(227, 15);
            this.panelKullanici.Name = "panelKullanici";
            this.panelKullanici.Size = new System.Drawing.Size(150, 40);
            this.panelKullanici.TabIndex = 3;
            // 
            // labelKullaniciAdi
            // 
            this.labelKullaniciAdi.AutoSize = true;
            this.labelKullaniciAdi.Location = new System.Drawing.Point(40, 10);
            this.labelKullaniciAdi.Name = "labelKullaniciAdi";
            this.labelKullaniciAdi.Size = new System.Drawing.Size(63, 21);
            this.labelKullaniciAdi.TabIndex = 2;
            this.labelKullaniciAdi.Text = "Kahveci";
            // 
            // pictureBoxKullanici
            // 
            this.pictureBoxKullanici.Location = new System.Drawing.Point(10, 8);
            this.pictureBoxKullanici.Name = "pictureBoxKullanici";
            this.pictureBoxKullanici.Size = new System.Drawing.Size(24, 24);
            this.pictureBoxKullanici.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxKullanici.TabIndex = 4;
            this.pictureBoxKullanici.TabStop = false;
            // 
            // label4
            // 
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(185)))), ((int)(((byte)(188)))));
            this.label4.Location = new System.Drawing.Point(12, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(383, 34);
            this.label4.TabIndex = 1;
            this.label4.Text = "________________________________________________________________________";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(12, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 32);
            this.label3.TabIndex = 0;
            this.label3.Text = "5 Adisyon";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(231)))), ((int)(((byte)(239)))));
            this.panel3.Controls.Add(this.masaPanels);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(395, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(775, 729);
            this.panel3.TabIndex = 2;
            // 
            // masaPanels
            // 
            this.masaPanels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.masaPanels.Location = new System.Drawing.Point(0, 55);
            this.masaPanels.Name = "masaPanels";
            this.masaPanels.Size = new System.Drawing.Size(775, 674);
            this.masaPanels.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnSonraki);
            this.panel4.Controls.Add(this.btnOnceki);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(775, 55);
            this.panel4.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(17, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Masalar";
            // 
            // btnOnceki
            // 
            this.btnOnceki.Image = ((System.Drawing.Image)(resources.GetObject("btnOnceki.Image")));
            this.btnOnceki.Location = new System.Drawing.Point(663, 3);
            this.btnOnceki.Name = "btnOnceki";
            this.btnOnceki.Size = new System.Drawing.Size(50, 50);
            this.btnOnceki.TabIndex = 1;
            this.btnOnceki.UseVisualStyleBackColor = true;
            this.btnOnceki.Click += new System.EventHandler(this.btnOnceki_Click);
            // 
            // btnSonraki
            // 
            this.btnSonraki.Image = ((System.Drawing.Image)(resources.GetObject("btnSonraki.Image")));
            this.btnSonraki.Location = new System.Drawing.Point(719, 3);
            this.btnSonraki.Name = "btnSonraki";
            this.btnSonraki.Size = new System.Drawing.Size(50, 50);
            this.btnSonraki.TabIndex = 1;
            this.btnSonraki.UseVisualStyleBackColor = true;
            this.btnSonraki.Click += new System.EventHandler(this.btnSonraki_Click);
            // 
            // FrmSatisEkrani
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmSatisEkrani";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmSatisEkrani";
            this.Load += new System.EventHandler(this.FrmSatisEkrani_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panelKullanici.ResumeLayout(false);
            this.panelKullanici.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxKullanici)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDoluMasalar;
        private System.Windows.Forms.Button btnBilardoMasalari;
        private System.Windows.Forms.Button btnMasalar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel masaPanels;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel doluMasaPanels;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelKullaniciAdi;
        private System.Windows.Forms.Panel panelKullanici;
        private System.Windows.Forms.PictureBox pictureBoxKullanici;
        private System.Windows.Forms.Button btnOnceki;
        private System.Windows.Forms.Button btnSonraki;
    }
}