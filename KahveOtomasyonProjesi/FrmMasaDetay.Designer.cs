namespace KahveOtomasyonProjesi
{
    partial class FrmMasaDetay
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
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
                
                // Timer'ı temizle
                if (_sureTimer != null)
                {
                    _sureTimer.Stop();
                    _sureTimer.Dispose();
                }
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMasaDetay));
            this.label7 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnOdeme = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.masaDetaylariPanels = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.urunPanels = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnYiyecekler = new System.Windows.Forms.Button();
            this.btnicecekler = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnGenel = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnGeri = new System.Windows.Forms.Button();
            this.btnTasi = new System.Windows.Forms.Button();
            this.btnMasaiptal = new System.Windows.Forms.Button();
            this.panel7.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
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
            // panel7
            // 
            this.panel7.Controls.Add(this.btnOdeme);
            this.panel7.Controls.Add(this.label7);
            this.panel7.Controls.Add(this.label6);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel7.Location = new System.Drawing.Point(0, 629);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(395, 100);
            this.panel7.TabIndex = 2;
            // 
            // btnOdeme
            // 
            this.btnOdeme.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(152)))), ((int)(((byte)(88)))));
            this.btnOdeme.FlatAppearance.BorderSize = 0;
            this.btnOdeme.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnOdeme.ForeColor = System.Drawing.Color.White;
            this.btnOdeme.Location = new System.Drawing.Point(18, 28);
            this.btnOdeme.Name = "btnOdeme";
            this.btnOdeme.Size = new System.Drawing.Size(150, 60);
            this.btnOdeme.TabIndex = 0;
            this.btnOdeme.Text = "ÖDEME AL";
            this.btnOdeme.UseVisualStyleBackColor = false;
            this.btnOdeme.Click += new System.EventHandler(this.btnOdeme_Click);
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
            // masaDetaylariPanels
            // 
            this.masaDetaylariPanels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.masaDetaylariPanels.Location = new System.Drawing.Point(0, 100);
            this.masaDetaylariPanels.Name = "masaDetaylariPanels";
            this.masaDetaylariPanels.Size = new System.Drawing.Size(395, 629);
            this.masaDetaylariPanels.TabIndex = 1;
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
            this.label3.Size = new System.Drawing.Size(92, 32);
            this.label3.TabIndex = 0;
            this.label3.Text = "Masa 8";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.label3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(395, 100);
            this.panel5.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(17, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Menüler";
            // 
            // urunPanels
            // 
            this.urunPanels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.urunPanels.Location = new System.Drawing.Point(0, 55);
            this.urunPanels.Name = "urunPanels";
            this.urunPanels.Size = new System.Drawing.Size(695, 674);
            this.urunPanels.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(231)))), ((int)(((byte)(239)))));
            this.panel3.Controls.Add(this.urunPanels);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(475, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(695, 729);
            this.panel3.TabIndex = 5;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(695, 55);
            this.panel4.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel7);
            this.panel2.Controls.Add(this.masaDetaylariPanels);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(80, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(395, 729);
            this.panel2.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(32, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "MENÜLER";
            // 
            // btnYiyecekler
            // 
            this.btnYiyecekler.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnYiyecekler.Location = new System.Drawing.Point(-3, 191);
            this.btnYiyecekler.Name = "btnYiyecekler";
            this.btnYiyecekler.Size = new System.Drawing.Size(183, 50);
            this.btnYiyecekler.TabIndex = 11;
            this.btnYiyecekler.Text = "YİYECEKLER";
            this.btnYiyecekler.UseVisualStyleBackColor = true;
            this.btnYiyecekler.Click += new System.EventHandler(this.btnYiyecekler_Click);
            // 
            // btnicecekler
            // 
            this.btnicecekler.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnicecekler.Location = new System.Drawing.Point(-3, 135);
            this.btnicecekler.Name = "btnicecekler";
            this.btnicecekler.Size = new System.Drawing.Size(183, 50);
            this.btnicecekler.TabIndex = 10;
            this.btnicecekler.Text = "İÇECEKLER";
            this.btnicecekler.UseVisualStyleBackColor = true;
            this.btnicecekler.Click += new System.EventHandler(this.btnicecekler_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(23)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.btnGenel);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnYiyecekler);
            this.panel1.Controls.Add(this.btnicecekler);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1170, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(180, 729);
            this.panel1.TabIndex = 3;
            // 
            // btnGenel
            // 
            this.btnGenel.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnGenel.Location = new System.Drawing.Point(-3, 79);
            this.btnGenel.Name = "btnGenel";
            this.btnGenel.Size = new System.Drawing.Size(183, 50);
            this.btnGenel.TabIndex = 12;
            this.btnGenel.Text = "GENEL";
            this.btnGenel.UseVisualStyleBackColor = true;
            this.btnGenel.Click += new System.EventHandler(this.btnGenel_Click);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(23)))), ((int)(((byte)(64)))));
            this.panel6.Controls.Add(this.btnGeri);
            this.panel6.Controls.Add(this.btnTasi);
            this.panel6.Controls.Add(this.btnMasaiptal);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(80, 729);
            this.panel6.TabIndex = 6;
            // 
            // btnGeri
            // 
            this.btnGeri.BackColor = System.Drawing.Color.Transparent;
            this.btnGeri.FlatAppearance.BorderSize = 0;
            this.btnGeri.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnGeri.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnGeri.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGeri.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnGeri.ForeColor = System.Drawing.SystemColors.Control;
            this.btnGeri.Image = ((System.Drawing.Image)(resources.GetObject("btnGeri.Image")));
            this.btnGeri.Location = new System.Drawing.Point(0, 5);
            this.btnGeri.Name = "btnGeri";
            this.btnGeri.Size = new System.Drawing.Size(80, 80);
            this.btnGeri.TabIndex = 2;
            this.btnGeri.Text = "Geri";
            this.btnGeri.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnGeri.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnGeri.UseVisualStyleBackColor = false;
            this.btnGeri.Click += new System.EventHandler(this.btnGeri_Click);
            // 
            // btnTasi
            // 
            this.btnTasi.BackColor = System.Drawing.Color.Transparent;
            this.btnTasi.FlatAppearance.BorderSize = 0;
            this.btnTasi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnTasi.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnTasi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTasi.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnTasi.ForeColor = System.Drawing.SystemColors.Control;
            this.btnTasi.Image = ((System.Drawing.Image)(resources.GetObject("btnTasi.Image")));
            this.btnTasi.Location = new System.Drawing.Point(0, 551);
            this.btnTasi.Name = "btnTasi";
            this.btnTasi.Size = new System.Drawing.Size(80, 80);
            this.btnTasi.TabIndex = 1;
            this.btnTasi.Text = "Taşı";
            this.btnTasi.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnTasi.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnTasi.UseVisualStyleBackColor = false;
            this.btnTasi.Click += new System.EventHandler(this.btnTasi_Click);
            // 
            // btnMasaiptal
            // 
            this.btnMasaiptal.BackColor = System.Drawing.Color.Transparent;
            this.btnMasaiptal.FlatAppearance.BorderSize = 0;
            this.btnMasaiptal.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnMasaiptal.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnMasaiptal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMasaiptal.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnMasaiptal.ForeColor = System.Drawing.SystemColors.Control;
            this.btnMasaiptal.Image = ((System.Drawing.Image)(resources.GetObject("btnMasaiptal.Image")));
            this.btnMasaiptal.Location = new System.Drawing.Point(0, 637);
            this.btnMasaiptal.Name = "btnMasaiptal";
            this.btnMasaiptal.Size = new System.Drawing.Size(80, 80);
            this.btnMasaiptal.TabIndex = 0;
            this.btnMasaiptal.Text = "İptal";
            this.btnMasaiptal.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMasaiptal.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnMasaiptal.UseVisualStyleBackColor = false;
            this.btnMasaiptal.Click += new System.EventHandler(this.btnMasaiptal_Click);
            // 
            // FrmMasaDetay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel6);
            this.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmMasaDetay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmMasaDetay";
            this.Load += new System.EventHandler(this.FrmMasaDetay_Load);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel masaDetaylariPanels;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel urunPanels;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnYiyecekler;
        private System.Windows.Forms.Button btnicecekler;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOdeme;
        private System.Windows.Forms.Button btnGenel;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btnMasaiptal;
        private System.Windows.Forms.Button btnTasi;
        private System.Windows.Forms.Button btnGeri;
    }
}