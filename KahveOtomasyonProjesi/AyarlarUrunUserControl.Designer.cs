namespace KahveOtomasyonProjesi
{
    partial class AyarlarUrunUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AyarlarUrunUserControl));
            this.grpUrunAyarlari = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUrunAdi = new System.Windows.Forms.TextBox();
            this.txtFiyat = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbKategori = new System.Windows.Forms.ComboBox();
            this.btnDosyaSeç = new System.Windows.Forms.Button();
            this.txtUrunGorsel = new System.Windows.Forms.TextBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnYeniUrunKaydet = new System.Windows.Forms.Button();
            this.grpUrunAyarlari.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // grpUrunAyarlari
            // 
            this.grpUrunAyarlari.Controls.Add(this.btnYeniUrunKaydet);
            this.grpUrunAyarlari.Controls.Add(this.btnReset);
            this.grpUrunAyarlari.Controls.Add(this.btnDosyaSeç);
            this.grpUrunAyarlari.Controls.Add(this.txtUrunGorsel);
            this.grpUrunAyarlari.Controls.Add(this.cmbKategori);
            this.grpUrunAyarlari.Controls.Add(this.label4);
            this.grpUrunAyarlari.Controls.Add(this.label3);
            this.grpUrunAyarlari.Controls.Add(this.txtFiyat);
            this.grpUrunAyarlari.Controls.Add(this.label2);
            this.grpUrunAyarlari.Controls.Add(this.txtUrunAdi);
            this.grpUrunAyarlari.Controls.Add(this.label1);
            this.grpUrunAyarlari.Controls.Add(this.dataGridView1);
            this.grpUrunAyarlari.Location = new System.Drawing.Point(30, 30);
            this.grpUrunAyarlari.Margin = new System.Windows.Forms.Padding(30, 30, 15, 30);
            this.grpUrunAyarlari.Name = "grpUrunAyarlari";
            this.grpUrunAyarlari.Size = new System.Drawing.Size(577, 529);
            this.grpUrunAyarlari.TabIndex = 0;
            this.grpUrunAyarlari.TabStop = false;
            this.grpUrunAyarlari.Text = "Ürün Ayarları";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 28);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(565, 230);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 285);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ürün Adı";
            // 
            // txtUrunAdi
            // 
            this.txtUrunAdi.Location = new System.Drawing.Point(15, 309);
            this.txtUrunAdi.Name = "txtUrunAdi";
            this.txtUrunAdi.Size = new System.Drawing.Size(199, 29);
            this.txtUrunAdi.TabIndex = 2;
            // 
            // txtFiyat
            // 
            this.txtFiyat.Location = new System.Drawing.Point(253, 309);
            this.txtFiyat.Name = "txtFiyat";
            this.txtFiyat.Size = new System.Drawing.Size(199, 29);
            this.txtFiyat.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(249, 285);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "Fiyat";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(249, 357);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "Görsel";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 357);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 21);
            this.label4.TabIndex = 7;
            this.label4.Text = "Kategori";
            // 
            // cmbKategori
            // 
            this.cmbKategori.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKategori.FormattingEnabled = true;
            this.cmbKategori.Location = new System.Drawing.Point(15, 381);
            this.cmbKategori.Name = "cmbKategori";
            this.cmbKategori.Size = new System.Drawing.Size(199, 29);
            this.cmbKategori.TabIndex = 8;
            // 
            // btnDosyaSeç
            // 
            this.btnDosyaSeç.Location = new System.Drawing.Point(452, 377);
            this.btnDosyaSeç.Margin = new System.Windows.Forms.Padding(0);
            this.btnDosyaSeç.Name = "btnDosyaSeç";
            this.btnDosyaSeç.Size = new System.Drawing.Size(122, 35);
            this.btnDosyaSeç.TabIndex = 12;
            this.btnDosyaSeç.Text = "Dosya Seç";
            this.btnDosyaSeç.UseVisualStyleBackColor = true;
            this.btnDosyaSeç.Click += new System.EventHandler(this.btnDosyaSeç_Click);
            // 
            // txtUrunGorsel
            // 
            this.txtUrunGorsel.Location = new System.Drawing.Point(253, 381);
            this.txtUrunGorsel.Margin = new System.Windows.Forms.Padding(10, 10, 0, 10);
            this.txtUrunGorsel.Name = "txtUrunGorsel";
            this.txtUrunGorsel.ReadOnly = true;
            this.txtUrunGorsel.Size = new System.Drawing.Size(199, 29);
            this.txtUrunGorsel.TabIndex = 11;
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnReset.Image = ((System.Drawing.Image)(resources.GetObject("btnReset.Image")));
            this.btnReset.Location = new System.Drawing.Point(266, 423);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(60, 60);
            this.btnReset.TabIndex = 14;
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnYeniUrunKaydet
            // 
            this.btnYeniUrunKaydet.BackColor = System.Drawing.Color.ForestGreen;
            this.btnYeniUrunKaydet.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnYeniUrunKaydet.ForeColor = System.Drawing.Color.White;
            this.btnYeniUrunKaydet.Location = new System.Drawing.Point(332, 423);
            this.btnYeniUrunKaydet.Name = "btnYeniUrunKaydet";
            this.btnYeniUrunKaydet.Size = new System.Drawing.Size(120, 60);
            this.btnYeniUrunKaydet.TabIndex = 15;
            this.btnYeniUrunKaydet.Text = "Kaydet";
            this.btnYeniUrunKaydet.UseVisualStyleBackColor = false;
            this.btnYeniUrunKaydet.Click += new System.EventHandler(this.btnYeniUrunKaydet_Click);
            // 
            // AyarlarUrunUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpUrunAyarlari);
            this.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AyarlarUrunUserControl";
            this.Size = new System.Drawing.Size(1150, 729);
            this.Load += new System.EventHandler(this.AyarlarUrunUserControl_Load);
            this.grpUrunAyarlari.ResumeLayout(false);
            this.grpUrunAyarlari.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpUrunAyarlari;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtUrunAdi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFiyat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbKategori;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDosyaSeç;
        private System.Windows.Forms.TextBox txtUrunGorsel;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnYeniUrunKaydet;
    }
}
