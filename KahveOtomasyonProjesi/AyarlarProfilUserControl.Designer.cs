namespace KahveOtomasyonProjesi
{
    partial class AyarlarProfilUserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AyarlarProfilUserControl));
            this.grpKullaniciAyarlari = new System.Windows.Forms.GroupBox();
            this.btnBilgilerim = new System.Windows.Forms.Button();
            this.mskTel = new System.Windows.Forms.MaskedTextBox();
            this.btnYeniKullanici = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnKullaniciAyariKaydet = new System.Windows.Forms.Button();
            this.txtSifre = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMail = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKullaniciAdi = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpYeniRolEkleme = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnYeniRolKaydet = new System.Windows.Forms.Button();
            this.chckVeresiyeOdemeAl = new System.Windows.Forms.CheckBox();
            this.chckViewVeresiye = new System.Windows.Forms.CheckBox();
            this.chckViewKasaRaporu = new System.Windows.Forms.CheckBox();
            this.chckYeniUrunGirisi = new System.Windows.Forms.CheckBox();
            this.chckViewistatistik = new System.Windows.Forms.CheckBox();
            this.chckHizliSatis = new System.Windows.Forms.CheckBox();
            this.txtRolAdi = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.grpYeniKategori = new System.Windows.Forms.GroupBox();
            this.btnYeniKategoriKaydet = new System.Windows.Forms.Button();
            this.txtKategoriAdi = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.grpKasiyerRolGuncelleme = new System.Windows.Forms.GroupBox();
            this.dgrKasiyerRolleri = new System.Windows.Forms.DataGridView();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbGuncellenecekRol = new System.Windows.Forms.ComboBox();
            this.btnRolGuncelle = new System.Windows.Forms.Button();
            this.txtRol = new System.Windows.Forms.TextBox();
            this.grpKullaniciAyarlari.SuspendLayout();
            this.grpYeniRolEkleme.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.grpYeniKategori.SuspendLayout();
            this.grpKasiyerRolGuncelleme.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrKasiyerRolleri)).BeginInit();
            this.SuspendLayout();
            // 
            // grpKullaniciAyarlari
            // 
            this.grpKullaniciAyarlari.Controls.Add(this.txtRol);
            this.grpKullaniciAyarlari.Controls.Add(this.btnBilgilerim);
            this.grpKullaniciAyarlari.Controls.Add(this.mskTel);
            this.grpKullaniciAyarlari.Controls.Add(this.btnYeniKullanici);
            this.grpKullaniciAyarlari.Controls.Add(this.label5);
            this.grpKullaniciAyarlari.Controls.Add(this.btnKullaniciAyariKaydet);
            this.grpKullaniciAyarlari.Controls.Add(this.txtSifre);
            this.grpKullaniciAyarlari.Controls.Add(this.label4);
            this.grpKullaniciAyarlari.Controls.Add(this.txtMail);
            this.grpKullaniciAyarlari.Controls.Add(this.label3);
            this.grpKullaniciAyarlari.Controls.Add(this.label2);
            this.grpKullaniciAyarlari.Controls.Add(this.txtKullaniciAdi);
            this.grpKullaniciAyarlari.Controls.Add(this.label1);
            this.grpKullaniciAyarlari.Location = new System.Drawing.Point(30, 30);
            this.grpKullaniciAyarlari.Margin = new System.Windows.Forms.Padding(30, 30, 15, 30);
            this.grpKullaniciAyarlari.Name = "grpKullaniciAyarlari";
            this.grpKullaniciAyarlari.Size = new System.Drawing.Size(265, 564);
            this.grpKullaniciAyarlari.TabIndex = 0;
            this.grpKullaniciAyarlari.TabStop = false;
            this.grpKullaniciAyarlari.Text = "Kullanıcı Ayarları";
            // 
            // btnBilgilerim
            // 
            this.btnBilgilerim.Location = new System.Drawing.Point(132, 35);
            this.btnBilgilerim.Name = "btnBilgilerim";
            this.btnBilgilerim.Size = new System.Drawing.Size(120, 50);
            this.btnBilgilerim.TabIndex = 4;
            this.btnBilgilerim.Text = "Bilgilerim";
            this.btnBilgilerim.UseVisualStyleBackColor = true;
            this.btnBilgilerim.Click += new System.EventHandler(this.btnBilgilerim_Click);
            // 
            // mskTel
            // 
            this.mskTel.Location = new System.Drawing.Point(34, 206);
            this.mskTel.Mask = "(999) 000-0000";
            this.mskTel.Name = "mskTel";
            this.mskTel.Size = new System.Drawing.Size(199, 29);
            this.mskTel.TabIndex = 2;
            // 
            // btnYeniKullanici
            // 
            this.btnYeniKullanici.Location = new System.Drawing.Point(6, 35);
            this.btnYeniKullanici.Name = "btnYeniKullanici";
            this.btnYeniKullanici.Size = new System.Drawing.Size(120, 50);
            this.btnYeniKullanici.TabIndex = 3;
            this.btnYeniKullanici.Text = "Yeni Kullanici";
            this.btnYeniKullanici.UseVisualStyleBackColor = true;
            this.btnYeniKullanici.Click += new System.EventHandler(this.btnYeniKullanici_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 410);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 21);
            this.label5.TabIndex = 10;
            this.label5.Text = "Rol";
            // 
            // btnKullaniciAyariKaydet
            // 
            this.btnKullaniciAyariKaydet.BackColor = System.Drawing.Color.ForestGreen;
            this.btnKullaniciAyariKaydet.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnKullaniciAyariKaydet.ForeColor = System.Drawing.Color.White;
            this.btnKullaniciAyariKaydet.Location = new System.Drawing.Point(113, 488);
            this.btnKullaniciAyariKaydet.Name = "btnKullaniciAyariKaydet";
            this.btnKullaniciAyariKaydet.Size = new System.Drawing.Size(120, 60);
            this.btnKullaniciAyariKaydet.TabIndex = 8;
            this.btnKullaniciAyariKaydet.Text = "Kaydet";
            this.btnKullaniciAyariKaydet.UseVisualStyleBackColor = false;
            this.btnKullaniciAyariKaydet.Click += new System.EventHandler(this.btnKullaniciAyariKaydet_Click);
            // 
            // txtSifre
            // 
            this.txtSifre.Location = new System.Drawing.Point(34, 362);
            this.txtSifre.Name = "txtSifre";
            this.txtSifre.Size = new System.Drawing.Size(199, 29);
            this.txtSifre.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 338);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 21);
            this.label4.TabIndex = 6;
            this.label4.Text = "Şifre";
            // 
            // txtMail
            // 
            this.txtMail.Location = new System.Drawing.Point(34, 288);
            this.txtMail.Name = "txtMail";
            this.txtMail.Size = new System.Drawing.Size(199, 29);
            this.txtMail.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 264);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "Mail";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 182);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Telefon";
            // 
            // txtKullaniciAdi
            // 
            this.txtKullaniciAdi.Location = new System.Drawing.Point(34, 135);
            this.txtKullaniciAdi.Name = "txtKullaniciAdi";
            this.txtKullaniciAdi.Size = new System.Drawing.Size(199, 29);
            this.txtKullaniciAdi.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kullanıcı Adı";
            // 
            // grpYeniRolEkleme
            // 
            this.grpYeniRolEkleme.Controls.Add(this.button1);
            this.grpYeniRolEkleme.Controls.Add(this.dataGridView1);
            this.grpYeniRolEkleme.Controls.Add(this.btnYeniRolKaydet);
            this.grpYeniRolEkleme.Controls.Add(this.chckVeresiyeOdemeAl);
            this.grpYeniRolEkleme.Controls.Add(this.chckViewVeresiye);
            this.grpYeniRolEkleme.Controls.Add(this.chckViewKasaRaporu);
            this.grpYeniRolEkleme.Controls.Add(this.chckYeniUrunGirisi);
            this.grpYeniRolEkleme.Controls.Add(this.chckViewistatistik);
            this.grpYeniRolEkleme.Controls.Add(this.chckHizliSatis);
            this.grpYeniRolEkleme.Controls.Add(this.txtRolAdi);
            this.grpYeniRolEkleme.Controls.Add(this.label7);
            this.grpYeniRolEkleme.Controls.Add(this.label6);
            this.grpYeniRolEkleme.Location = new System.Drawing.Point(325, 30);
            this.grpYeniRolEkleme.Margin = new System.Windows.Forms.Padding(15, 30, 15, 30);
            this.grpYeniRolEkleme.Name = "grpYeniRolEkleme";
            this.grpYeniRolEkleme.Size = new System.Drawing.Size(498, 451);
            this.grpYeniRolEkleme.TabIndex = 1;
            this.grpYeniRolEkleme.TabStop = false;
            this.grpYeniRolEkleme.Text = "Yeni Rol Ekleme";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DodgerBlue;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(292, 373);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 60);
            this.button1.TabIndex = 11;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 73);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(214, 274);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // btnYeniRolKaydet
            // 
            this.btnYeniRolKaydet.BackColor = System.Drawing.Color.ForestGreen;
            this.btnYeniRolKaydet.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnYeniRolKaydet.ForeColor = System.Drawing.Color.White;
            this.btnYeniRolKaydet.Location = new System.Drawing.Point(358, 373);
            this.btnYeniRolKaydet.Name = "btnYeniRolKaydet";
            this.btnYeniRolKaydet.Size = new System.Drawing.Size(120, 60);
            this.btnYeniRolKaydet.TabIndex = 9;
            this.btnYeniRolKaydet.Text = "Kaydet";
            this.btnYeniRolKaydet.UseVisualStyleBackColor = false;
            this.btnYeniRolKaydet.Click += new System.EventHandler(this.btnYeniRolKaydet_Click);
            // 
            // chckVeresiyeOdemeAl
            // 
            this.chckVeresiyeOdemeAl.AutoSize = true;
            this.chckVeresiyeOdemeAl.Location = new System.Drawing.Point(230, 322);
            this.chckVeresiyeOdemeAl.Name = "chckVeresiyeOdemeAl";
            this.chckVeresiyeOdemeAl.Size = new System.Drawing.Size(217, 25);
            this.chckVeresiyeOdemeAl.TabIndex = 6;
            this.chckVeresiyeOdemeAl.Text = "Veresiyelerde Ödeme Alma";
            this.chckVeresiyeOdemeAl.UseVisualStyleBackColor = true;
            // 
            // chckViewVeresiye
            // 
            this.chckViewVeresiye.AutoSize = true;
            this.chckViewVeresiye.Location = new System.Drawing.Point(230, 291);
            this.chckViewVeresiye.Name = "chckViewVeresiye";
            this.chckViewVeresiye.Size = new System.Drawing.Size(201, 25);
            this.chckViewVeresiye.TabIndex = 6;
            this.chckViewVeresiye.Text = "Veresiyeler Görüntüleme";
            this.chckViewVeresiye.UseVisualStyleBackColor = true;
            // 
            // chckViewKasaRaporu
            // 
            this.chckViewKasaRaporu.AutoSize = true;
            this.chckViewKasaRaporu.Location = new System.Drawing.Point(230, 260);
            this.chckViewKasaRaporu.Name = "chckViewKasaRaporu";
            this.chckViewKasaRaporu.Size = new System.Drawing.Size(212, 25);
            this.chckViewKasaRaporu.TabIndex = 6;
            this.chckViewKasaRaporu.Text = "Kasa Raporu Görüntüleme";
            this.chckViewKasaRaporu.UseVisualStyleBackColor = true;
            // 
            // chckYeniUrunGirisi
            // 
            this.chckYeniUrunGirisi.AutoSize = true;
            this.chckYeniUrunGirisi.Location = new System.Drawing.Point(230, 229);
            this.chckYeniUrunGirisi.Name = "chckYeniUrunGirisi";
            this.chckYeniUrunGirisi.Size = new System.Drawing.Size(137, 25);
            this.chckYeniUrunGirisi.TabIndex = 6;
            this.chckYeniUrunGirisi.Text = "Yeni Ürün Girişi";
            this.chckYeniUrunGirisi.UseVisualStyleBackColor = true;
            // 
            // chckViewistatistik
            // 
            this.chckViewistatistik.AutoSize = true;
            this.chckViewistatistik.Location = new System.Drawing.Point(230, 201);
            this.chckViewistatistik.Name = "chckViewistatistik";
            this.chckViewistatistik.Size = new System.Drawing.Size(248, 25);
            this.chckViewistatistik.TabIndex = 5;
            this.chckViewistatistik.Text = "İstatistik Sayfasını Görüntüleme";
            this.chckViewistatistik.UseVisualStyleBackColor = true;
            // 
            // chckHizliSatis
            // 
            this.chckHizliSatis.AutoSize = true;
            this.chckHizliSatis.Location = new System.Drawing.Point(230, 170);
            this.chckHizliSatis.Name = "chckHizliSatis";
            this.chckHizliSatis.Size = new System.Drawing.Size(160, 25);
            this.chckHizliSatis.TabIndex = 4;
            this.chckHizliSatis.Text = "Hızlı Satış Yapabilir";
            this.chckHizliSatis.UseVisualStyleBackColor = true;
            // 
            // txtRolAdi
            // 
            this.txtRolAdi.Location = new System.Drawing.Point(230, 74);
            this.txtRolAdi.Name = "txtRolAdi";
            this.txtRolAdi.Size = new System.Drawing.Size(199, 29);
            this.txtRolAdi.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(226, 130);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(133, 21);
            this.label7.TabIndex = 2;
            this.label7.Text = "Rol Yetkilerini Seç";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(226, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 21);
            this.label6.TabIndex = 2;
            this.label6.Text = "Rol Adı";
            // 
            // grpYeniKategori
            // 
            this.grpYeniKategori.Controls.Add(this.btnYeniKategoriKaydet);
            this.grpYeniKategori.Controls.Add(this.txtKategoriAdi);
            this.grpYeniKategori.Controls.Add(this.label8);
            this.grpYeniKategori.Location = new System.Drawing.Point(853, 30);
            this.grpYeniKategori.Margin = new System.Windows.Forms.Padding(15, 30, 30, 15);
            this.grpYeniKategori.Name = "grpYeniKategori";
            this.grpYeniKategori.Size = new System.Drawing.Size(267, 206);
            this.grpYeniKategori.TabIndex = 2;
            this.grpYeniKategori.TabStop = false;
            this.grpYeniKategori.Text = "Yeni Kategori Ekleme";
            // 
            // btnYeniKategoriKaydet
            // 
            this.btnYeniKategoriKaydet.BackColor = System.Drawing.Color.ForestGreen;
            this.btnYeniKategoriKaydet.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnYeniKategoriKaydet.ForeColor = System.Drawing.Color.White;
            this.btnYeniKategoriKaydet.Location = new System.Drawing.Point(113, 121);
            this.btnYeniKategoriKaydet.Name = "btnYeniKategoriKaydet";
            this.btnYeniKategoriKaydet.Size = new System.Drawing.Size(120, 60);
            this.btnYeniKategoriKaydet.TabIndex = 10;
            this.btnYeniKategoriKaydet.Text = "Kaydet";
            this.btnYeniKategoriKaydet.UseVisualStyleBackColor = false;
            this.btnYeniKategoriKaydet.Click += new System.EventHandler(this.btnYeniKategoriKaydet_Click);
            // 
            // txtKategoriAdi
            // 
            this.txtKategoriAdi.Location = new System.Drawing.Point(34, 74);
            this.txtKategoriAdi.Name = "txtKategoriAdi";
            this.txtKategoriAdi.Size = new System.Drawing.Size(199, 29);
            this.txtKategoriAdi.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(30, 50);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 21);
            this.label8.TabIndex = 4;
            this.label8.Text = "Kategori Adı";
            // 
            // grpKasiyerRolGuncelleme
            // 
            this.grpKasiyerRolGuncelleme.Controls.Add(this.label9);
            this.grpKasiyerRolGuncelleme.Controls.Add(this.cmbGuncellenecekRol);
            this.grpKasiyerRolGuncelleme.Controls.Add(this.btnRolGuncelle);
            this.grpKasiyerRolGuncelleme.Controls.Add(this.dgrKasiyerRolleri);
            this.grpKasiyerRolGuncelleme.Location = new System.Drawing.Point(853, 266);
            this.grpKasiyerRolGuncelleme.Margin = new System.Windows.Forms.Padding(15, 15, 30, 30);
            this.grpKasiyerRolGuncelleme.Name = "grpKasiyerRolGuncelleme";
            this.grpKasiyerRolGuncelleme.Size = new System.Drawing.Size(267, 328);
            this.grpKasiyerRolGuncelleme.TabIndex = 3;
            this.grpKasiyerRolGuncelleme.TabStop = false;
            this.grpKasiyerRolGuncelleme.Text = "Kasiyer Rol Güncelleme";
            // 
            // dgrKasiyerRolleri
            // 
            this.dgrKasiyerRolleri.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrKasiyerRolleri.Location = new System.Drawing.Point(6, 28);
            this.dgrKasiyerRolleri.Name = "dgrKasiyerRolleri";
            this.dgrKasiyerRolleri.Size = new System.Drawing.Size(255, 162);
            this.dgrKasiyerRolleri.TabIndex = 11;
            this.dgrKasiyerRolleri.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgrKasiyerRolleri_DataBindingComplete);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(30, 193);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 21);
            this.label9.TabIndex = 14;
            this.label9.Text = "Rol";
            // 
            // cmbGuncellenecekRol
            // 
            this.cmbGuncellenecekRol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGuncellenecekRol.FormattingEnabled = true;
            this.cmbGuncellenecekRol.Location = new System.Drawing.Point(34, 217);
            this.cmbGuncellenecekRol.Name = "cmbGuncellenecekRol";
            this.cmbGuncellenecekRol.Size = new System.Drawing.Size(199, 29);
            this.cmbGuncellenecekRol.TabIndex = 13;
            // 
            // btnRolGuncelle
            // 
            this.btnRolGuncelle.BackColor = System.Drawing.Color.ForestGreen;
            this.btnRolGuncelle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnRolGuncelle.ForeColor = System.Drawing.Color.White;
            this.btnRolGuncelle.Location = new System.Drawing.Point(113, 252);
            this.btnRolGuncelle.Name = "btnRolGuncelle";
            this.btnRolGuncelle.Size = new System.Drawing.Size(120, 60);
            this.btnRolGuncelle.TabIndex = 12;
            this.btnRolGuncelle.Text = "Güncelle";
            this.btnRolGuncelle.UseVisualStyleBackColor = false;
            this.btnRolGuncelle.Click += new System.EventHandler(this.btnRolGuncelle_Click);
            // 
            // txtRol
            // 
            this.txtRol.Location = new System.Drawing.Point(34, 434);
            this.txtRol.Name = "txtRol";
            this.txtRol.ReadOnly = true;
            this.txtRol.Size = new System.Drawing.Size(199, 29);
            this.txtRol.TabIndex = 4;
            // 
            // AyarlarProfilUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpKasiyerRolGuncelleme);
            this.Controls.Add(this.grpYeniKategori);
            this.Controls.Add(this.grpYeniRolEkleme);
            this.Controls.Add(this.grpKullaniciAyarlari);
            this.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AyarlarProfilUserControl";
            this.Size = new System.Drawing.Size(1150, 729);
            this.Load += new System.EventHandler(this.AyarlarProfilUserControl_Load);
            this.grpKullaniciAyarlari.ResumeLayout(false);
            this.grpKullaniciAyarlari.PerformLayout();
            this.grpYeniRolEkleme.ResumeLayout(false);
            this.grpYeniRolEkleme.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.grpYeniKategori.ResumeLayout(false);
            this.grpYeniKategori.PerformLayout();
            this.grpKasiyerRolGuncelleme.ResumeLayout(false);
            this.grpKasiyerRolGuncelleme.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrKasiyerRolleri)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpKullaniciAyarlari;
        private System.Windows.Forms.GroupBox grpYeniRolEkleme;
        private System.Windows.Forms.TextBox txtSifre;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtKullaniciAdi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnKullaniciAyariKaydet;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MaskedTextBox mskTel;
        private System.Windows.Forms.TextBox txtRolAdi;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chckViewistatistik;
        private System.Windows.Forms.CheckBox chckHizliSatis;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chckViewVeresiye;
        private System.Windows.Forms.CheckBox chckViewKasaRaporu;
        private System.Windows.Forms.CheckBox chckYeniUrunGirisi;
        private System.Windows.Forms.CheckBox chckVeresiyeOdemeAl;
        private System.Windows.Forms.Button btnYeniRolKaydet;
        private System.Windows.Forms.GroupBox grpYeniKategori;
        private System.Windows.Forms.TextBox txtKategoriAdi;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnYeniKategoriKaydet;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnYeniKullanici;
        private System.Windows.Forms.Button btnBilgilerim;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.GroupBox grpKasiyerRolGuncelleme;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbGuncellenecekRol;
        private System.Windows.Forms.Button btnRolGuncelle;
        private System.Windows.Forms.DataGridView dgrKasiyerRolleri;
        private System.Windows.Forms.TextBox txtRol;
    }
}
