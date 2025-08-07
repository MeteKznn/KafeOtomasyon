namespace KahveOtomasyonProjesi
{
    partial class FrmVeresiyeler
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chckBorcsuz = new System.Windows.Forms.CheckBox();
            this.chckBinUstu = new System.Windows.Forms.CheckBox();
            this.chckBinUstuVurgulama = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 126);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1350, 603);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chckBinUstuVurgulama);
            this.panel1.Controls.Add(this.chckBinUstu);
            this.panel1.Controls.Add(this.chckBorcsuz);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1350, 126);
            this.panel1.TabIndex = 1;
            // 
            // chckBorcsuz
            // 
            this.chckBorcsuz.AutoSize = true;
            this.chckBorcsuz.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.chckBorcsuz.Location = new System.Drawing.Point(12, 12);
            this.chckBorcsuz.Name = "chckBorcsuz";
            this.chckBorcsuz.Size = new System.Drawing.Size(338, 41);
            this.chckBorcsuz.TabIndex = 1;
            this.chckBorcsuz.Text = "Borcu Olmayanları Göster";
            this.chckBorcsuz.UseVisualStyleBackColor = true;
            // 
            // chckBinUstu
            // 
            this.chckBinUstu.AutoSize = true;
            this.chckBinUstu.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.chckBinUstu.Location = new System.Drawing.Point(12, 59);
            this.chckBinUstu.Name = "chckBinUstu";
            this.chckBinUstu.Size = new System.Drawing.Size(366, 41);
            this.chckBinUstu.TabIndex = 2;
            this.chckBinUstu.Text = "1000 TL üzeri olanları listele";
            this.chckBinUstu.UseVisualStyleBackColor = true;
            // 
            // chckBinUstuVurgulama
            // 
            this.chckBinUstuVurgulama.AutoSize = true;
            this.chckBinUstuVurgulama.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.chckBinUstuVurgulama.Location = new System.Drawing.Point(431, 12);
            this.chckBinUstuVurgulama.Name = "chckBinUstuVurgulama";
            this.chckBinUstuVurgulama.Size = new System.Drawing.Size(386, 41);
            this.chckBinUstuVurgulama.TabIndex = 3;
            this.chckBinUstuVurgulama.Text = "1000 TL üzeri olanları vurgula";
            this.chckBinUstuVurgulama.UseVisualStyleBackColor = true;
            // 
            // FrmVeresiyeler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmVeresiyeler";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmVeresiyeler";
            this.Load += new System.EventHandler(this.FrmVeresiyeler_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chckBorcsuz;
        private System.Windows.Forms.CheckBox chckBinUstu;
        private System.Windows.Forms.CheckBox chckBinUstuVurgulama;
    }
}