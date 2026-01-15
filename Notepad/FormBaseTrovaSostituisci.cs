using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void txtTrova_TextChanged(object sender, EventArgs e)
        {
            btnTrova.Enabled = txtTrova.TextLength > 0;
        }
    }
}
