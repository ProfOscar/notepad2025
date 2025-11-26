using System;
using System.Diagnostics;
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

        #region costruttori

        public FormMain()
        {
            InitializeComponent();
        }

        #endregion

        #region gestori_eventi

        private void FormMain_Load(object sender, System.EventArgs e)
        {
            Clipboard.Clear();
            Reset();
        }

        private void FormMain_Activated(object sender, EventArgs e)
        {
            incollaToolStripMenuItem.Enabled = Clipboard.ContainsText();
        }

        private void rtbMain_TextChanged(object sender, System.EventArgs e)
        {
            isEdited = rtbMain.Text != lastSavedContent;
            SetFormTitle();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (rtbMain.Text != lastSavedContent)
            {
                // c'è qualcosa da salvare
                // chiedo all'utente se vuole salvare
                DialogResult result = ChiediSeSalvare();
                if (result == DialogResult.Yes)
                {
                    SalvaFile(filePath == "", e);
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void rtbMain_SelectionChanged(object sender, EventArgs e)
        {
            copiaToolStripMenuItem.Enabled =
            tagliaToolStripMenuItem.Enabled =
            eliminaToolStripMenuItem.Enabled =
                rtbMain.SelectionLength > 0;
        }

        #endregion

        #region voci_di_menu

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
                DialogResult result = ChiediSeSalvare();
                if (result == DialogResult.Cancel) return;
                if (result == DialogResult.Yes) SalvaFile(filePath == ""); // fa "salva con nome"
                Reset();
            }
        }

        private void nuovaFinestraToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Process.Start(Application.ExecutablePath);
        }

        private void apriToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            if (rtbMain.Text != lastSavedContent)
            {
                // c'è contenuto da salvare
                // chiedo all'utente se vuole salvare
                DialogResult result = ChiediSeSalvare();
                if (result == DialogResult.Cancel) return;
                if (result == DialogResult.Yes) SalvaFile(filePath == ""); // fa "salva con nome"
            }
            // apro la dialog open file
            openFileDialogMain.FileName = "";
            if (openFileDialogMain.ShowDialog() == DialogResult.OK)
            {
                ApriFile(openFileDialogMain.FileName);
            }
        }

        private void salvaToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            SalvaFile(filePath == "");
        }

        private void salvaconnomeToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            SalvaFile(true);
        }

        private void esciToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void annullaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtbMain.CanRedo)
                rtbMain.Redo();
            else
                rtbMain.Undo();
        }

        private void tagliaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbMain.Cut();
            incollaToolStripMenuItem.Enabled = Clipboard.ContainsText();
        }

        private void copiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbMain.Copy();
            incollaToolStripMenuItem.Enabled = Clipboard.ContainsText();
        }

        private void incollaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbMain.Paste();
        }

        private void eliminaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbMain.SelectedText = "";
        }

        private void selezionatuttoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbMain.SelectAll();
        }

        private void oraDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbMain.SelectedText = DateTime.Now.ToString("HH:mm dd/MM/yyyy");
        }

        #endregion

        #region funzioni_helper

        private void Reset()
        {
            copiaToolStripMenuItem.Enabled =
            tagliaToolStripMenuItem.Enabled =
            eliminaToolStripMenuItem.Enabled =
                rtbMain.SelectionLength > 0;

            filePath = "";
            fileName = "Senza nome";
            lastSavedContent = "";
            isEdited = false;
            rtbMain.Text = "";
            SetFormTitle();
        }

        private void SetFormTitle()
        {
            string marker = isEdited ? EDITED_MARKER : "";
            Text = $"{marker}{fileName} - {PROGRAM_NAME}";
        }

        /// <summary>
        /// Salve il contenuto del RichTextBox su filePath se non è stringa vuote,
        /// altrimenti apre una SaveFileDialog
        /// </summary>
        /// <param name="filePath">Il percorso del file da salvare 
        /// (se vuoto apre saveFileDialog)</param>
        private void SalvaFile(bool isSalvaConNome = false, FormClosingEventArgs eventFormClosing = null)
        {
            try
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
            catch
            {
                MessageBox.Show("Problemi con il salvataggio del documento",
                    "ATTENZIONE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private DialogResult ChiediSeSalvare()
        {
            DialogResult result = MessageBox.Show(
                    $"Salvare le modifiche a {fileName}?",
                    PROGRAM_NAME,
                    MessageBoxButtons.YesNoCancel);
            return result;
        }

        private void ApriFile(string fp)
        {
            try
            {
                using (var reader = new StreamReader(fp))
                    rtbMain.Text = reader.ReadToEnd();
                filePath = fp;
                fileName = Path.GetFileName(filePath);
                lastSavedContent = rtbMain.Text;
                isEdited = false;
                SetFormTitle();
            }
            catch
            {
                MessageBox.Show("Problemi con l'apertura del documento",
                    "ATTENZIONE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

    }
}
