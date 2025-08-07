namespace KahveOtomasyonProjesi
{
    partial class AyarlarMasaUserControl
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
            this.lblMasaAdi = new System.Windows.Forms.Label();
            this.txtMasaAdi = new System.Windows.Forms.TextBox();
            this.btnMasaEkle = new System.Windows.Forms.Button();
            this.btnMasaGuncelle = new System.Windows.Forms.Button();
            this.dgvMasalar = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvBilardoMasalar = new System.Windows.Forms.DataGridView();
            this.txtBilardoMasaAdi = new System.Windows.Forms.TextBox();
            this.btnBilardoGuncelle = new System.Windows.Forms.Button();
            this.btnBilardoEkle = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMasalar)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBilardoMasalar)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMasaAdi
            // 
            this.lblMasaAdi.AutoSize = true;
            this.lblMasaAdi.Location = new System.Drawing.Point(6, 34);
            this.lblMasaAdi.Name = "lblMasaAdi";
            this.lblMasaAdi.Size = new System.Drawing.Size(74, 21);
            this.lblMasaAdi.TabIndex = 0;
            this.lblMasaAdi.Text = "Masa Adı";
            // 
            // txtMasaAdi
            // 
            this.txtMasaAdi.Location = new System.Drawing.Point(6, 59);
            this.txtMasaAdi.Name = "txtMasaAdi";
            this.txtMasaAdi.Size = new System.Drawing.Size(200, 29);
            this.txtMasaAdi.TabIndex = 1;
            // 
            // btnMasaEkle
            // 
            this.btnMasaEkle.BackColor = System.Drawing.Color.ForestGreen;
            this.btnMasaEkle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnMasaEkle.ForeColor = System.Drawing.Color.White;
            this.btnMasaEkle.Location = new System.Drawing.Point(216, 57);
            this.btnMasaEkle.Name = "btnMasaEkle";
            this.btnMasaEkle.Size = new System.Drawing.Size(80, 32);
            this.btnMasaEkle.TabIndex = 2;
            this.btnMasaEkle.Text = "Ekle";
            this.btnMasaEkle.UseVisualStyleBackColor = false;
            // 
            // btnMasaGuncelle
            // 
            this.btnMasaGuncelle.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnMasaGuncelle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnMasaGuncelle.ForeColor = System.Drawing.Color.White;
            this.btnMasaGuncelle.Location = new System.Drawing.Point(306, 57);
            this.btnMasaGuncelle.Name = "btnMasaGuncelle";
            this.btnMasaGuncelle.Size = new System.Drawing.Size(100, 32);
            this.btnMasaGuncelle.TabIndex = 3;
            this.btnMasaGuncelle.Text = "Güncelle";
            this.btnMasaGuncelle.UseVisualStyleBackColor = false;
            // 
            // dgvMasalar
            // 
            this.dgvMasalar.AllowUserToAddRows = false;
            this.dgvMasalar.AllowUserToResizeColumns = false;
            this.dgvMasalar.AllowUserToResizeRows = false;
            this.dgvMasalar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMasalar.BackgroundColor = System.Drawing.Color.White;
            this.dgvMasalar.ColumnHeadersHeight = 36;
            this.dgvMasalar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvMasalar.Location = new System.Drawing.Point(6, 104);
            this.dgvMasalar.MultiSelect = false;
            this.dgvMasalar.Name = "dgvMasalar";
            this.dgvMasalar.ReadOnly = true;
            this.dgvMasalar.RowHeadersVisible = false;
            this.dgvMasalar.RowTemplate.Height = 36;
            this.dgvMasalar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMasalar.Size = new System.Drawing.Size(400, 300);
            this.dgvMasalar.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblMasaAdi);
            this.groupBox1.Controls.Add(this.dgvMasalar);
            this.groupBox1.Controls.Add(this.txtMasaAdi);
            this.groupBox1.Controls.Add(this.btnMasaGuncelle);
            this.groupBox1.Controls.Add(this.btnMasaEkle);
            this.groupBox1.Location = new System.Drawing.Point(30, 30);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(415, 412);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Masa İşlemleri";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.dgvBilardoMasalar);
            this.groupBox2.Controls.Add(this.txtBilardoMasaAdi);
            this.groupBox2.Controls.Add(this.btnBilardoGuncelle);
            this.groupBox2.Controls.Add(this.btnBilardoEkle);
            this.groupBox2.Location = new System.Drawing.Point(505, 30);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(30);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(415, 412);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Bilardo Masa İşlemleri";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bilardo Masa Adı";
            // 
            // dgvBilardoMasalar
            // 
            this.dgvBilardoMasalar.AllowUserToAddRows = false;
            this.dgvBilardoMasalar.AllowUserToResizeColumns = false;
            this.dgvBilardoMasalar.AllowUserToResizeRows = false;
            this.dgvBilardoMasalar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBilardoMasalar.BackgroundColor = System.Drawing.Color.White;
            this.dgvBilardoMasalar.ColumnHeadersHeight = 36;
            this.dgvBilardoMasalar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvBilardoMasalar.Location = new System.Drawing.Point(6, 104);
            this.dgvBilardoMasalar.MultiSelect = false;
            this.dgvBilardoMasalar.Name = "dgvBilardoMasalar";
            this.dgvBilardoMasalar.ReadOnly = true;
            this.dgvBilardoMasalar.RowHeadersVisible = false;
            this.dgvBilardoMasalar.RowTemplate.Height = 36;
            this.dgvBilardoMasalar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBilardoMasalar.Size = new System.Drawing.Size(400, 300);
            this.dgvBilardoMasalar.TabIndex = 4;
            // 
            // txtBilardoMasaAdi
            // 
            this.txtBilardoMasaAdi.Location = new System.Drawing.Point(6, 59);
            this.txtBilardoMasaAdi.Name = "txtBilardoMasaAdi";
            this.txtBilardoMasaAdi.Size = new System.Drawing.Size(200, 29);
            this.txtBilardoMasaAdi.TabIndex = 1;
            // 
            // btnBilardoGuncelle
            // 
            this.btnBilardoGuncelle.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnBilardoGuncelle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnBilardoGuncelle.ForeColor = System.Drawing.Color.White;
            this.btnBilardoGuncelle.Location = new System.Drawing.Point(306, 57);
            this.btnBilardoGuncelle.Name = "btnBilardoGuncelle";
            this.btnBilardoGuncelle.Size = new System.Drawing.Size(100, 32);
            this.btnBilardoGuncelle.TabIndex = 3;
            this.btnBilardoGuncelle.Text = "Güncelle";
            this.btnBilardoGuncelle.UseVisualStyleBackColor = false;
            // 
            // btnBilardoEkle
            // 
            this.btnBilardoEkle.BackColor = System.Drawing.Color.ForestGreen;
            this.btnBilardoEkle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnBilardoEkle.ForeColor = System.Drawing.Color.White;
            this.btnBilardoEkle.Location = new System.Drawing.Point(216, 57);
            this.btnBilardoEkle.Name = "btnBilardoEkle";
            this.btnBilardoEkle.Size = new System.Drawing.Size(80, 32);
            this.btnBilardoEkle.TabIndex = 2;
            this.btnBilardoEkle.Text = "Ekle";
            this.btnBilardoEkle.UseVisualStyleBackColor = false;
            // 
            // AyarlarMasaUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AyarlarMasaUserControl";
            this.Size = new System.Drawing.Size(1181, 507);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMasalar)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBilardoMasalar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblMasaAdi;
        private System.Windows.Forms.TextBox txtMasaAdi;
        private System.Windows.Forms.Button btnMasaEkle;
        private System.Windows.Forms.Button btnMasaGuncelle;
        private System.Windows.Forms.DataGridView dgvMasalar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvBilardoMasalar;
        private System.Windows.Forms.TextBox txtBilardoMasaAdi;
        private System.Windows.Forms.Button btnBilardoGuncelle;
        private System.Windows.Forms.Button btnBilardoEkle;
    }
}
