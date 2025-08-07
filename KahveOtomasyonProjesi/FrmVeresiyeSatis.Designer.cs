namespace KahveOtomasyonProjesi
{
    partial class FrmVeresiyeSatis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVeresiyeSatis));
            this.label1 = new System.Windows.Forms.Label();
            this.addProductPanel = new System.Windows.Forms.Panel();
            this.btnYeniMusteri = new System.Windows.Forms.Button();
            this.cmbMusteriler = new System.Windows.Forms.ComboBox();
            this.btniptal = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.rchAciklama = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnKaydet = new System.Windows.Forms.Button();
            this.addProductPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Müşteri";
            // 
            // addProductPanel
            // 
            this.addProductPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(230)))), ((int)(((byte)(237)))));
            this.addProductPanel.Controls.Add(this.btnYeniMusteri);
            this.addProductPanel.Controls.Add(this.cmbMusteriler);
            this.addProductPanel.Controls.Add(this.btniptal);
            this.addProductPanel.Controls.Add(this.label5);
            this.addProductPanel.Controls.Add(this.rchAciklama);
            this.addProductPanel.Controls.Add(this.label4);
            this.addProductPanel.Controls.Add(this.btnKaydet);
            this.addProductPanel.Controls.Add(this.label1);
            this.addProductPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addProductPanel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.addProductPanel.Location = new System.Drawing.Point(0, 0);
            this.addProductPanel.Margin = new System.Windows.Forms.Padding(10);
            this.addProductPanel.Name = "addProductPanel";
            this.addProductPanel.Size = new System.Drawing.Size(356, 595);
            this.addProductPanel.TabIndex = 4;
            // 
            // btnYeniMusteri
            // 
            this.btnYeniMusteri.AutoSize = true;
            this.btnYeniMusteri.Image = ((System.Drawing.Image)(resources.GetObject("btnYeniMusteri.Image")));
            this.btnYeniMusteri.Location = new System.Drawing.Point(24, 173);
            this.btnYeniMusteri.Name = "btnYeniMusteri";
            this.btnYeniMusteri.Size = new System.Drawing.Size(299, 56);
            this.btnYeniMusteri.TabIndex = 14;
            this.btnYeniMusteri.Text = "Müşteri Ekle";
            this.btnYeniMusteri.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnYeniMusteri.UseVisualStyleBackColor = true;
            this.btnYeniMusteri.Click += new System.EventHandler(this.btnYeniMusteri_Click);
            // 
            // cmbMusteriler
            // 
            this.cmbMusteriler.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbMusteriler.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMusteriler.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.cmbMusteriler.FormattingEnabled = true;
            this.cmbMusteriler.IntegralHeight = false;
            this.cmbMusteriler.ItemHeight = 30;
            this.cmbMusteriler.Location = new System.Drawing.Point(24, 104);
            this.cmbMusteriler.Name = "cmbMusteriler";
            this.cmbMusteriler.Size = new System.Drawing.Size(299, 36);
            this.cmbMusteriler.TabIndex = 13;
            this.cmbMusteriler.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbMusteriler_DrawItem);
            this.cmbMusteriler.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.cmbMusteriler_MeasureItem);
            // 
            // btniptal
            // 
            this.btniptal.BackColor = System.Drawing.Color.Firebrick;
            this.btniptal.FlatAppearance.BorderSize = 0;
            this.btniptal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btniptal.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btniptal.ForeColor = System.Drawing.Color.White;
            this.btniptal.Location = new System.Drawing.Point(24, 488);
            this.btniptal.Name = "btniptal";
            this.btniptal.Size = new System.Drawing.Size(120, 60);
            this.btniptal.TabIndex = 12;
            this.btniptal.Text = "İptal";
            this.btniptal.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btniptal.UseVisualStyleBackColor = false;
            this.btniptal.Click += new System.EventHandler(this.btniptal_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(26, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(181, 25);
            this.label5.TabIndex = 5;
            this.label5.Text = "Veresiye Satış Kaydı";
            // 
            // rchAciklama
            // 
            this.rchAciklama.Location = new System.Drawing.Point(24, 260);
            this.rchAciklama.Name = "rchAciklama";
            this.rchAciklama.Size = new System.Drawing.Size(299, 134);
            this.rchAciklama.TabIndex = 11;
            this.rchAciklama.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 232);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 25);
            this.label4.TabIndex = 9;
            this.label4.Text = "Açıklama";
            // 
            // btnKaydet
            // 
            this.btnKaydet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnKaydet.FlatAppearance.BorderSize = 0;
            this.btnKaydet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKaydet.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnKaydet.ForeColor = System.Drawing.Color.White;
            this.btnKaydet.Location = new System.Drawing.Point(163, 488);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(160, 60);
            this.btnKaydet.TabIndex = 8;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnKaydet.UseVisualStyleBackColor = false;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            // 
            // FrmVeresiyeSatis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(356, 595);
            this.Controls.Add(this.addProductPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmVeresiyeSatis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmVeresiyeSatis";
            this.addProductPanel.ResumeLayout(false);
            this.addProductPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel addProductPanel;
        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox rchAciklama;
        private System.Windows.Forms.Button btniptal;
        private System.Windows.Forms.ComboBox cmbMusteriler;
        private System.Windows.Forms.Button btnYeniMusteri;
    }
}