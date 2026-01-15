using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Notepad
{
    public partial class FormMain : Form
    {
        const string EDITED_MARKER = "*";
        const string PROGRAM_NAME = "Blocco note di Windows";

        const string WIN_LINE_ENDING = "Windows (CRLF)";
        const string MAC_LINE_ENDING = "Macintosh (CR)";
        const string UNIX_LINE_ENDING = "Unix (LF)";

        string filePath;
        string fileName;
        string lastSavedContent;
        bool isEdited;

        Encoding currentEncoding;

        #region costruttori

        public FormMain()
        {
            InitializeComponent();
        }

        #endregion

        #region gestori_eventi

        private void FormMain_Load(object sender, System.EventArgs e)
        {
            // Clipboard.Clear(); // opzionale: pulisce gli appunti all'avvio
            barraDistatoToolStripMenuItem.Checked = true;
            barraDistatoToolStripMenuItem.CheckOnClick = true;
            statusStripBottom.Visible = true;
            rtbMain.WordWrap = false;
            rtbMain.Font = new Font("Consolas", 11, FontStyle.Regular);
            rtbMain.MouseWheel += RtbMain_MouseWheel;
            Reset();
            ScriviZoomSuStatusBar();
        }

        private void FormMain_Activated(object sender, EventArgs e)
        {
            incollaToolStripMenuItem.Enabled = Clipboard.ContainsText();
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

        private void rtbMain_TextChanged(object sender, System.EventArgs e)
        {
            trovaToolStripMenuItem.Enabled =
            trovaPrecedenteToolStripMenuItem.Enabled =
            trovaSuccessivoToolStripMenuItem.Enabled =
            sostituisciToolStripMenuItem.Enabled =
                rtbMain.TextLength > 0;
            isEdited = rtbMain.Text != lastSavedContent;
            SetFormTitle();
        }

        private void rtbMain_SelectionChanged(object sender, EventArgs e)
        {
            copiaToolStripMenuItem.Enabled =
            tagliaToolStripMenuItem.Enabled =
            eliminaToolStripMenuItem.Enabled =
            cercaConBingToolStripMenuItem.Enabled =
                rtbMain.SelectionLength > 0;
            ScriviRigaColonnaSuStatusBar();
        }

        private void RtbMain_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ModifierKeys == Keys.Control)
                ScriviZoomSuStatusBar();
        }

        private int indexFirstCharOnPage;
        private void printDocumentMain_BeginPrint(object sender, PrintEventArgs e)
        {
            indexFirstCharOnPage = 0;
        }

        private void printDocumentMain_PrintPage(object sender, PrintPageEventArgs e)
        {
            indexFirstCharOnPage = rtbMain.FormatRange(false, e, indexFirstCharOnPage, rtbMain.TextLength);
            e.HasMorePages = indexFirstCharOnPage < rtbMain.TextLength;
        }

        private void printDocumentMain_EndPrint(object sender, PrintEventArgs e)
        {
            rtbMain.FormatRangeDone();
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

        private void impostaPaginaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pageSetupDialogMain.Document = printDocumentMain; // potrebbe essere fatto al load del form
            // Usando RichTextBoxEx NON BISOGNA applicare le impostazioni di stampa all'oggetto PrintDocument
            //if (pageSetupDialogMain.ShowDialog() == DialogResult.OK)
            //{
            //    printDocumentMain.PrinterSettings = pageSetupDialogMain.PrinterSettings;
            //}
            pageSetupDialogMain.ShowDialog();
        }

        private void stampaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDocumentMain.DocumentName = fileName;
            printDialogMain.Document = printDocumentMain; // potrebbe essere fatto al load del form
            if (printDialogMain.ShowDialog() == DialogResult.OK)
            {
                printDocumentMain.Print();
            }
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

        private void cercaConBingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string baseUrl = "https://www.bing.com/search?q=";
            string searchQuery = rtbMain.SelectedText.Trim();
            if (searchQuery.Length > 128)
                searchQuery = searchQuery.Substring(0, 128);
            Process.Start(baseUrl + Uri.EscapeDataString(searchQuery));
        }

        private void trovaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTrova formTrova = new FormTrova();
            formTrova.Location = new Point(70, 70);
            formTrova.ShowDialog();
        }

        private void trovaSuccessivoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void trovaPrecedenteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void sostituisciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSostituisci formSostituisci = new FormSostituisci();
            formSostituisci.Location = new Point(70, 70);
            formSostituisci.ShowDialog();
        }

        private void vaiAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int currentLine = rtbMain.GetLineFromCharIndex(rtbMain.SelectionStart) + 1;
            int linesCount = rtbMain.Lines.Length;
            FormVaiA f = new FormVaiA(currentLine, linesCount);
            if (f.ShowDialog() == DialogResult.OK)
            {
                int charIndex = rtbMain.GetFirstCharIndexFromLine(f.numRiga - 1);
                rtbMain.SelectionStart = charIndex;
            }
        }

        private void selezionatuttoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbMain.SelectAll();
        }

        private void oraDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbMain.SelectedText = DateTime.Now.ToString("HH:mm dd/MM/yyyy");
        }

        private void aCapoAutomaticoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbMain.WordWrap = aCapoAutomaticoToolStripMenuItem.Checked;
            vaiAToolStripMenuItem.Enabled = !rtbMain.WordWrap;
        }

        private void carattereToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialogMain.Font = rtbMain.Font;
            if (fontDialogMain.ShowDialog() == DialogResult.OK)
                rtbMain.Font = fontDialogMain.Font;
        }

        private void zoomAvantiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtbMain.ZoomFactor < 5)
            {
                rtbMain.ZoomFactor += (float)0.1;
                ScriviZoomSuStatusBar();
            }
        }

        private void zoomIndietroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtbMain.ZoomFactor >= 0.2) 
            {
                rtbMain.ZoomFactor -= 0.1f;
                ScriviZoomSuStatusBar();
            }
        }

        private void ripristinaZoomPredefinitoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtbMain.ZoomFactor = 1;
            ScriviZoomSuStatusBar();
        }

        private void barraDistatoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStripBottom.Visible = barraDistatoToolStripMenuItem.Checked;
        }

        private void guidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://go.microsoft.com/fwlink/?LinkId=834783");
        }

        private void inviaFeedbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/ProfOscar/notepad2025/issues");
        }

        private void informazionisuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        #endregion

        #region funzioni_helper

        private void Reset()
        {
            copiaToolStripMenuItem.Enabled =
            tagliaToolStripMenuItem.Enabled =
            eliminaToolStripMenuItem.Enabled =
            cercaConBingToolStripMenuItem.Enabled =
                rtbMain.SelectionLength > 0;

            trovaToolStripMenuItem.Enabled =
            trovaPrecedenteToolStripMenuItem.Enabled =
            trovaSuccessivoToolStripMenuItem.Enabled =
            sostituisciToolStripMenuItem.Enabled =
                rtbMain.TextLength > 0;

            filePath = "";
            fileName = "Senza nome";
            lastSavedContent = "";
            isEdited = false;
            rtbMain.Text = "";
            SetFormTitle();
            ScriviRigaColonnaSuStatusBar();
            toolStripStatusLabelLineEnding.Text = WIN_LINE_ENDING;
            currentEncoding = new UTF8Encoding(false);
            toolStripStatusLabelEncoding.Text = currentEncoding.BodyName.ToUpper();
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
                using (var writer = new StreamWriter(filePath, false, currentEncoding))
                {
                    string textToSave;
                    if (toolStripStatusLabelLineEnding.Text == WIN_LINE_ENDING)
                        textToSave = rtbMain.Text.Replace("\n", "\r\n");
                    else if (toolStripStatusLabelLineEnding.Text == MAC_LINE_ENDING)
                        textToSave = rtbMain.Text.Replace("\n", "\r");
                    else // UNIX_LINE_ENDING
                        textToSave = rtbMain.Text;
                    writer.Write(textToSave);
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
                {
                    // necessaria questa variable perchè il RichTextBox converte tutti gli "a capo" in LF ("\n)
                    string myText = reader.ReadToEnd();

                    if (myText.Contains("\r\n"))
                        toolStripStatusLabelLineEnding.Text = WIN_LINE_ENDING;
                    else if (myText.Contains("\r"))
                        toolStripStatusLabelLineEnding.Text = MAC_LINE_ENDING;
                    else if (myText.Contains("\n"))
                        toolStripStatusLabelLineEnding.Text = UNIX_LINE_ENDING;
                    else
                        toolStripStatusLabelLineEnding.Text = WIN_LINE_ENDING;

                    currentEncoding = reader.CurrentEncoding;
                    toolStripStatusLabelEncoding.Text = currentEncoding.BodyName.ToUpper();

                    rtbMain.Text = myText;
                }
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

        private void ScriviZoomSuStatusBar()
        {
            int zoom = (int)(rtbMain.ZoomFactor * 10);
            toolStripStatusLabelZoom.Text = $"{zoom}0%";
        }

        private void ScriviRigaColonnaSuStatusBar()
        {
            int nLine = 1, nCol = 1;
            if (rtbMain.Text.Length > 0)
            {
                string textBeforeCursor = rtbMain.Text.Substring(0, rtbMain.SelectionStart);
                nLine = Regex.Matches(textBeforeCursor, @"\n").Count + 1;
                nCol = rtbMain.SelectionStart - textBeforeCursor.LastIndexOf("\n");
            }
            toolStripStatusLabelLineColumn.Text = $"Linea {nLine}, colonna {nCol}";
        }

        #endregion
    }
}
