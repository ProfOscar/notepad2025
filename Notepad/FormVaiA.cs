using System.Windows.Forms;

namespace Notepad
{
    public partial class FormVaiA : Form
    {
        private int linesCount;
        
        public int numRiga;

        public FormVaiA(int currentLine, int linesCount)
        {
            InitializeComponent();
            txtNumRiga.Text = currentLine.ToString();
            this.linesCount = linesCount > 0 ? linesCount : 1;
        }

        private void btnVaiA_Click(object sender, System.EventArgs e)
        {
            int.TryParse(txtNumRiga.Text, out numRiga);
            if (numRiga < 1 || numRiga > linesCount)
            {
                MessageBox.Show($"Il numero di riga deve essere compreso tra 1 e {linesCount}",
                    "Blocco note - Vai alla riga");
                txtNumRiga.Text = linesCount.ToString();
                txtNumRiga.Focus();
                txtNumRiga.SelectAll();
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void txtNumRiga_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !(e.KeyChar == '\b'))
            {
                errorProvider.SetError(txtNumRiga, "Carattere non ammesso");
                e.Handled = true;
            } 
            else
            {
                errorProvider.Clear();
            }
        }
    }
}
