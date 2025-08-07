using System;
using System.Drawing;
using System.Windows.Forms;

namespace KahveOtomasyonProjesi
{
    public partial class InputBox : Form
    {
        public string InputValue { get; private set; }
        public decimal Amount { get; private set; }

        public InputBox(string title, string prompt, decimal maxAmount)
        {
            InitializeComponent();
            this.Text = title;
            lblPrompt.Text = prompt;
            lblPrompt.AutoSize = false;
            lblPrompt.Size = new Size(260, 42);
            txtAmount.Text = maxAmount.ToString("F2");
            txtAmount.SelectAll();
            txtAmount.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                InputValue = txtAmount.Text;
                Amount = amount;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Lütfen geçerli bir miktar giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAmount.Focus();
                txtAmount.SelectAll();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Sadece rakam, virgül, nokta ve backspace'e izin ver
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void InputBox_Load(object sender, EventArgs e)
        {
            txtAmount.Focus();
            txtAmount.SelectAll();
        }
    }
}