using System.Windows.Forms;

namespace Notepad
{
    public partial class FormMain : Form
    {
        const string EDITED_MARKER = "*";
        const string PROGRAM_NAME = "Blocco note di Windows";

        string fileName = "Senza nome";
        string lastContentSaved = "";
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
            isEdited = rtbMain.Text != lastContentSaved;
            SetFormTitle();
        }

        private void SetFormTitle()
        {
            string marker = isEdited ? EDITED_MARKER : "";
            Text = $"{marker}{fileName} - {PROGRAM_NAME}";
        }
    }
}
