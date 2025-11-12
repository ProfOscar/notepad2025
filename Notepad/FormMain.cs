using System.IO;
using System.Windows.Forms;

namespace Notepad
{
    public partial class FormMain : Form
    {
        const string EDITED_MARKER = "*";
        const string PROGRAM_NAME = "Blocco note di Windows";

        string filePath = "";
        string fileName = "Senza nome";
        string lastSavedContent = "";
        bool isEdited = false;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, System.EventArgs e)
        {
            SetFormTitle();
        }

        private void rtbMain_TextChanged(object sender, System.EventArgs e)
        {
            isEdited = rtbMain.Text != lastSavedContent;
            SetFormTitle();
        }

        private void nuovoToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (rtbMain.Text == lastSavedContent)
            {
                // non ci sono modifiche da salvare
                rtbMain.Text = "";
            }
            else
            {
                // chiedo all'utente se vuole salvare
                DialogResult result = MessageBox.Show(
                    $"Salvare le modifiche a {fileName}?",
                    PROGRAM_NAME,
                    MessageBoxButtons.YesNoCancel);
                switch ( result )
                {
                    case DialogResult.Yes:
                        // salvo
                        MessageBox.Show("Salvo");
                        rtbMain.Text = "";
                        SetFormTitle();
                        break;
                    case DialogResult.No:
                        // non salvo
                        MessageBox.Show("Non salvo");
                        rtbMain.Text = "";
                        SetFormTitle();
                        break;
                }
            }
        }

        private void SetFormTitle()
        {
            string marker = isEdited ? EDITED_MARKER : "";
            Text = $"{marker}{fileName} - {PROGRAM_NAME}";
        }

        private void salvaconnomeToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if ( saveFileDialogMain.ShowDialog() == DialogResult.OK )
            {
                filePath = saveFileDialogMain.FileName;
                fileName = Path.GetFileName(filePath);
                using ( var writer = new StreamWriter(filePath) )
                {
                    writer.Write(rtbMain.Text);
                }
                lastSavedContent = rtbMain.Text;
                isEdited = false;
                SetFormTitle();
            }
        }
    }
}
