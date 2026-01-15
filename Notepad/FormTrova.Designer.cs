namespace Notepad
{
    partial class FormTrova
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
            this.gbSuGiu = new System.Windows.Forms.GroupBox();
            this.rdbGiu = new System.Windows.Forms.RadioButton();
            this.rdbSu = new System.Windows.Forms.RadioButton();
            this.gbSuGiu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAnnulla
            // 
            this.btnAnnulla.Location = new System.Drawing.Point(252, 53);
            // 
            // btnTrova
            // 
            this.btnTrova.Location = new System.Drawing.Point(252, 12);
            // 
            // gbSuGiu
            // 
            this.gbSuGiu.Controls.Add(this.rdbGiu);
            this.gbSuGiu.Controls.Add(this.rdbSu);
            this.gbSuGiu.Location = new System.Drawing.Point(144, 41);
            this.gbSuGiu.Name = "gbSuGiu";
            this.gbSuGiu.Size = new System.Drawing.Size(101, 43);
            this.gbSuGiu.TabIndex = 6;
            this.gbSuGiu.TabStop = false;
            this.gbSuGiu.Text = "Direzione";
            // 
            // rdbGiu
            // 
            this.rdbGiu.AutoSize = true;
            this.rdbGiu.Checked = true;
            this.rdbGiu.Location = new System.Drawing.Point(57, 16);
            this.rdbGiu.Name = "rdbGiu";
            this.rdbGiu.Size = new System.Drawing.Size(41, 17);
            this.rdbGiu.TabIndex = 1;
            this.rdbGiu.TabStop = true;
            this.rdbGiu.Text = "Giù";
            this.rdbGiu.UseVisualStyleBackColor = true;
            // 
            // rdbSu
            // 
            this.rdbSu.AutoSize = true;
            this.rdbSu.Location = new System.Drawing.Point(6, 16);
            this.rdbSu.Name = "rdbSu";
            this.rdbSu.Size = new System.Drawing.Size(38, 17);
            this.rdbSu.TabIndex = 0;
            this.rdbSu.Text = "Su";
            this.rdbSu.UseVisualStyleBackColor = true;
            // 
            // FormTrova
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(369, 112);
            this.Controls.Add(this.gbSuGiu);
            this.Name = "FormTrova";
            this.Text = "Trova";
            this.Controls.SetChildIndex(this.txtTrova, 0);
            this.Controls.SetChildIndex(this.btnTrova, 0);
            this.Controls.SetChildIndex(this.btnAnnulla, 0);
            this.Controls.SetChildIndex(this.gbSuGiu, 0);
            this.gbSuGiu.ResumeLayout(false);
            this.gbSuGiu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSuGiu;
        private System.Windows.Forms.RadioButton rdbSu;
        private System.Windows.Forms.RadioButton rdbGiu;
    }
}
