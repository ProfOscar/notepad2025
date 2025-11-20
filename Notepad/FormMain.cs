using System.IO;
using System.Windows.Forms;

namespace Notepad
{
    public partial class FormMain : Form
    {
        const string EDITED_MARKER = "*";
        const string PROGRAM_NAME = "Blocco note di Windows";

        string filePath;
        string fileName;
        string lastSavedContent;
        bool isEdited;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, System.EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            filePath = "";
            fileName = "Senza nome";
            lastSavedContent = "";
            isEdited = false;
            rtbMain.Text = "";
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
                Reset();
            }
            else
            {
                // chiedo all'utente se vuole salvare
                DialogResult result = MessageBox.Show(
                    $"Salvare le modifiche a {fileName}?",
                    PROGRAM_NAME,
                    MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Cancel) return;
                if (result == DialogResult.Yes) SalvaFile(filePath == ""); // fa "salva con nome"
                Reset();
            }
        }

        private void SetFormTitle()
        {
            string marker = isEdited ? EDITED_MARKER : "";
            Text = $"{marker}{fileName} - {PROGRAM_NAME}";
        }

        private void salvaconnomeToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            SalvaFile(true);
        }

        /// <summary>
        /// Salve il contenuto del RichTextBox su filePath se non è stringa vuote,
        /// altrimenti apre una SaveFileDialog
        /// </summary>
        /// <param name="filePath">Il percorso del file da salvare 
        /// (se vuoto apre saveFileDialog)</param>
        private void SalvaFile(bool isSalvaConNome = false, FormClosingEventArgs eventFormClosing = null)
        {
            if (isSalvaConNome)
            {
                DialogResult result = saveFileDialogMain.ShowDialog();
                if (result == DialogResult.Cancel && eventFormClosing != null) 
                    eventFormClosing.Cancel = true;
                if (result == DialogResult.OK)
                    filePath = saveFileDialogMain.FileName;
                else
                    return;
            }
            // salva il contenuto del richtextbox su filePath
            fileName = Path.GetFileName(filePath);
            using (var writer = new StreamWriter(filePath))
            {
                writer.Write(rtbMain.Text);
            }
            lastSavedContent = rtbMain.Text;
            isEdited = false;
            SetFormTitle();
        }

        private void salvaToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            SalvaFile(filePath == "");
        }

        private void esciToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (rtbMain.Text != lastSavedContent)
            {
                // c'è qualcosa da salvare
                // chiedo all'utente se vuole salvare
                DialogResult result = MessageBox.Show(
                    $"Salvare le modifiche a {fileName}?",
                    PROGRAM_NAME,
                    MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    SalvaFile(filePath == "", e);
                } else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
