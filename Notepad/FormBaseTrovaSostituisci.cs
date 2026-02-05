using System;
using System.Windows.Forms;

namespace Notepad
{
    public partial class FormBaseTrovaSostituisci : Form
    {
        public FormBaseTrovaSostituisci()
        {
            InitializeComponent();
            btnTrova.Enabled = false;
        }

        private void FormBaseTrovaSostituisci_Load(object sender, EventArgs e)
        {
            txtTrova.Text = FindReplaceService.Parameters.TextToFind;
            chkMaiusMinus.Checked = FindReplaceService.Parameters.IsCaseSensive;
            chkTestoIntorno.Checked = FindReplaceService.Parameters.IsWholeWord;
        }

        private void txtTrova_TextChanged(object sender, EventArgs e)
        {
            btnTrova.Enabled = txtTrova.TextLength > 0;
        }

        private void btnTrova_Click(object sender, EventArgs e)
        {
            FindReplaceService.Parameters.TextToFind = txtTrova.Text;
            FindReplaceService.Parameters.IsCaseSensive = chkMaiusMinus.Checked;
            FindReplaceService.Parameters.IsWholeWord = chkTestoIntorno.Checked;
            if (FindReplaceService.Find() == -1)
                FindReplaceService.ShowNotFound();
        }

        private void btnAnnulla_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
