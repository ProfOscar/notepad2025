namespace Notepad
{
    partial class FormSostituisci
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblSostituisci = new System.Windows.Forms.Label();
            this.txtSostituisci = new System.Windows.Forms.TextBox();
            this.btnSostituisci = new System.Windows.Forms.Button();
            this.btnSostituisciTutto = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtTrova
            // 
            this.txtTrova.Location = new System.Drawing.Point(85, 13);
            this.txtTrova.Size = new System.Drawing.Size(149, 20);
            this.txtTrova.TextChanged += new System.EventHandler(this.txtTrova_TextChanged);
            // 
            // lblSostituisci
            // 
            this.lblSostituisci.AutoSize = true;
            this.lblSostituisci.Location = new System.Drawing.Point(1, 44);
            this.lblSostituisci.Name = "lblSostituisci";
            this.lblSostituisci.Size = new System.Drawing.Size(78, 13);
            this.lblSostituisci.TabIndex = 6;
            this.lblSostituisci.Text = "Sostituisci con:";
            // 
            // txtSostituisci
            // 
            this.txtSostituisci.Location = new System.Drawing.Point(85, 41);
            this.txtSostituisci.Name = "txtSostituisci";
            this.txtSostituisci.Size = new System.Drawing.Size(149, 20);
            this.txtSostituisci.TabIndex = 7;
            // 
            // btnSostituisci
            // 
            this.btnSostituisci.Location = new System.Drawing.Point(252, 31);
            this.btnSostituisci.Name = "btnSostituisci";
            this.btnSostituisci.Size = new System.Drawing.Size(105, 23);
            this.btnSostituisci.TabIndex = 8;
            this.btnSostituisci.Text = "Sostituisci";
            this.btnSostituisci.UseVisualStyleBackColor = true;
            // 
            // btnSostituisciTutto
            // 
            this.btnSostituisciTutto.Location = new System.Drawing.Point(252, 59);
            this.btnSostituisciTutto.Name = "btnSostituisciTutto";
            this.btnSostituisciTutto.Size = new System.Drawing.Size(105, 23);
            this.btnSostituisciTutto.TabIndex = 9;
            this.btnSostituisciTutto.Text = "Sostituisci tutto";
            this.btnSostituisciTutto.UseVisualStyleBackColor = true;
            // 
            // FormSostituisci
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(369, 112);
            this.Controls.Add(this.btnSostituisciTutto);
            this.Controls.Add(this.btnSostituisci);
            this.Controls.Add(this.txtSostituisci);
            this.Controls.Add(this.lblSostituisci);
            this.Name = "FormSostituisci";
            this.Text = "Sostituisci";
            this.Controls.SetChildIndex(this.txtTrova, 0);
            this.Controls.SetChildIndex(this.btnTrova, 0);
            this.Controls.SetChildIndex(this.btnAnnulla, 0);
            this.Controls.SetChildIndex(this.lblSostituisci, 0);
            this.Controls.SetChildIndex(this.txtSostituisci, 0);
            this.Controls.SetChildIndex(this.btnSostituisci, 0);
            this.Controls.SetChildIndex(this.btnSostituisciTutto, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSostituisci;
        private System.Windows.Forms.TextBox txtSostituisci;
        private System.Windows.Forms.Button btnSostituisci;
        private System.Windows.Forms.Button btnSostituisciTutto;
    }
}
