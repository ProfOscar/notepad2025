using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Notepad
{
    public partial class FormSostituisci : Notepad.FormBaseTrovaSostituisci
    {
        public FormSostituisci()
        {
            InitializeComponent();
            btnSostituisci.Enabled = btnSostituisciTutto.Enabled = false;
        }

        private void txtTrova_TextChanged(object sender, EventArgs e)
        {
            btnSostituisci.Enabled = btnSostituisciTutto.Enabled = txtTrova.TextLength > 0;
        }
    }
}
