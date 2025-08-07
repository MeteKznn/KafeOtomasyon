namespace KahveOtomasyonProjesi
{
    partial class FrmUrunGiris
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUrunGiris));
            this.label1 = new System.Windows.Forms.Label();
            this.txtUrunAd = new System.Windows.Forms.TextBox();
            this.addProductPanel = new System.Windows.Forms.Panel();
            this.cmbKategoriler = new System.Windows.Forms.ComboBox();
            this.btnUrunEkle = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUrunGorsel = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFiyat = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnDosyaSeç = new System.Windows.Forms.Button();
            this.addProductPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ürün Adı";
            // 
            // txtUrunAd
            // 
            this.txtUrunAd.Location = new System.Drawing.Point(10, 57);
            this.txtUrunAd.Margin = new System.Windows.Forms.Padding(10);
            this.txtUrunAd.Name = "txtUrunAd";
            this.txtUrunAd.Size = new System.Drawing.Size(406, 33);
            this.txtUrunAd.TabIndex = 1;
            // 
            // addProductPanel
            // 
            this.addProductPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(230)))), ((int)(((byte)(237)))));
            this.addProductPanel.Controls.Add(this.btnDosyaSeç);
            this.addProductPanel.Controls.Add(this.cmbKategoriler);
            this.addProductPanel.Controls.Add(this.btnUrunEkle);
            this.addProductPanel.Controls.Add(this.label4);
            this.addProductPanel.Controls.Add(this.txtUrunGorsel);
            this.addProductPanel.Controls.Add(this.label3);
            this.addProductPanel.Controls.Add(this.txtFiyat);
            this.addProductPanel.Controls.Add(this.label2);
            this.addProductPanel.Controls.Add(this.txtUrunAd);
            this.addProductPanel.Controls.Add(this.label1);
            this.addProductPanel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.addProductPanel.Location = new System.Drawing.Point(19, 87);
            this.addProductPanel.Margin = new System.Windows.Forms.Padding(10);
            this.addProductPanel.Name = "addProductPanel";
            this.addProductPanel.Size = new System.Drawing.Size(455, 370);
            this.addProductPanel.TabIndex = 2;
            // 
            // cmbKategoriler
            // 
            this.cmbKategoriler.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKategoriler.FormattingEnabled = true;
            this.cmbKategoriler.Location = new System.Drawing.Point(216, 135);
            this.cmbKategoriler.Margin = new System.Windows.Forms.Padding(10);
            this.cmbKategoriler.Name = "cmbKategoriler";
            this.cmbKategoriler.Size = new System.Drawing.Size(200, 33);
            this.cmbKategoriler.TabIndex = 9;
            // 
            // btnUrunEkle
            // 
            this.btnUrunEkle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnUrunEkle.FlatAppearance.BorderSize = 0;
            this.btnUrunEkle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUrunEkle.ForeColor = System.Drawing.Color.White;
            this.btnUrunEkle.Image = ((System.Drawing.Image)(resources.GetObject("btnUrunEkle.Image")));
            this.btnUrunEkle.Location = new System.Drawing.Point(237, 271);
            this.btnUrunEkle.Name = "btnUrunEkle";
            this.btnUrunEkle.Size = new System.Drawing.Size(180, 70);
            this.btnUrunEkle.TabIndex = 8;
            this.btnUrunEkle.Text = "Ürün Ekle";
            this.btnUrunEkle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUrunEkle.UseVisualStyleBackColor = false;
            this.btnUrunEkle.Click += new System.EventHandler(this.btnUrunEkle_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(211, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "Kategori";
            // 
            // txtUrunGorsel
            // 
            this.txtUrunGorsel.Location = new System.Drawing.Point(11, 213);
            this.txtUrunGorsel.Margin = new System.Windows.Forms.Padding(10, 10, 0, 10);
            this.txtUrunGorsel.Name = "txtUrunGorsel";
            this.txtUrunGorsel.ReadOnly = true;
            this.txtUrunGorsel.Size = new System.Drawing.Size(283, 33);
            this.txtUrunGorsel.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 178);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Ürün Görseli";
            // 
            // txtFiyat
            // 
            this.txtFiyat.Location = new System.Drawing.Point(10, 135);
            this.txtFiyat.Margin = new System.Windows.Forms.Padding(10);
            this.txtFiyat.Name = "txtFiyat";
            this.txtFiyat.Size = new System.Drawing.Size(200, 33);
            this.txtFiyat.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Fiyat";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(14, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 25);
            this.label5.TabIndex = 3;
            this.label5.Text = "Yeni Ürün Ekle";
            // 
            // btnDosyaSeç
            // 
            this.btnDosyaSeç.Location = new System.Drawing.Point(294, 212);
            this.btnDosyaSeç.Margin = new System.Windows.Forms.Padding(0);
            this.btnDosyaSeç.Name = "btnDosyaSeç";
            this.btnDosyaSeç.Size = new System.Drawing.Size(122, 35);
            this.btnDosyaSeç.TabIndex = 10;
            this.btnDosyaSeç.Text = "Dosya Seç";
            this.btnDosyaSeç.UseVisualStyleBackColor = true;
            this.btnDosyaSeç.Click += new System.EventHandler(this.btnDosyaSeç_Click);
            // 
            // FrmUrunGiris
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(502, 491);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.addProductPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmUrunGiris";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Yeni Ürün Girişi";
            this.Load += new System.EventHandler(this.FrmUrunGiris_Load);
            this.addProductPanel.ResumeLayout(false);
            this.addProductPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUrunAd;
        private System.Windows.Forms.Panel addProductPanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUrunGorsel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFiyat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnUrunEkle;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbKategoriler;
        private System.Windows.Forms.Button btnDosyaSeç;
    }
}