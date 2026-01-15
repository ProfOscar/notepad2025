namespace Notepad
{
    partial class FormBaseTrovaSostituisci
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTrova = new System.Windows.Forms.Label();
            this.txtTrova = new System.Windows.Forms.TextBox();
            this.btnTrova = new System.Windows.Forms.Button();
            this.btnAnnulla = new System.Windows.Forms.Button();
            this.chkMiusMinus = new System.Windows.Forms.CheckBox();
            this.chkTestoIntorno = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblTrova
            // 
            this.lblTrova.AutoSize = true;
            this.lblTrova.Location = new System.Drawing.Point(1, 16);
            this.lblTrova.Name = "lblTrova";
            this.lblTrova.Size = new System.Drawing.Size(38, 13);
            this.lblTrova.TabIndex = 0;
            this.lblTrova.Text = "Trova:";
            // 
            // txtTrova
            // 
            this.txtTrova.Location = new System.Drawing.Point(45, 13);
            this.txtTrova.Name = "txtTrova";
            this.txtTrova.Size = new System.Drawing.Size(189, 20);
            this.txtTrova.TabIndex = 1;
            this.txtTrova.TextChanged += new System.EventHandler(this.txtTrova_TextChanged);
            // 
            // btnTrova
            // 
            this.btnTrova.Location = new System.Drawing.Point(252, 4);
            this.btnTrova.Name = "btnTrova";
            this.btnTrova.Size = new System.Drawing.Size(105, 23);
            this.btnTrova.TabIndex = 2;
            this.btnTrova.Text = "Trova successivo";
            this.btnTrova.UseVisualStyleBackColor = true;
            // 
            // btnAnnulla
            // 
            this.btnAnnulla.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAnnulla.Location = new System.Drawing.Point(252, 86);
            this.btnAnnulla.Name = "btnAnnulla";
            this.btnAnnulla.Size = new System.Drawing.Size(105, 23);
            this.btnAnnulla.TabIndex = 3;
            this.btnAnnulla.Text = "Annulla";
            this.btnAnnulla.UseVisualStyleBackColor = true;
            // 
            // chkMiusMinus
            // 
            this.chkMiusMinus.AutoSize = true;
            this.chkMiusMinus.Location = new System.Drawing.Point(12, 67);
            this.chkMiusMinus.Name = "chkMiusMinus";
            this.chkMiusMinus.Size = new System.Drawing.Size(126, 17);
            this.chkMiusMinus.TabIndex = 4;
            this.chkMiusMinus.Text = "Maiuscole/minuscole";
            this.chkMiusMinus.UseVisualStyleBackColor = true;
            // 
            // chkTestoIntorno
            // 
            this.chkTestoIntorno.AutoSize = true;
            this.chkTestoIntorno.Location = new System.Drawing.Point(12, 90);
            this.chkTestoIntorno.Name = "chkTestoIntorno";
            this.chkTestoIntorno.Size = new System.Drawing.Size(88, 17);
            this.chkTestoIntorno.TabIndex = 5;
            this.chkTestoIntorno.Text = "Testo intorno";
            this.chkTestoIntorno.UseVisualStyleBackColor = true;
            // 
            // FormBaseTrovaSostituisci
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnAnnulla;
            this.ClientSize = new System.Drawing.Size(361, 112);
            this.Controls.Add(this.chkTestoIntorno);
            this.Controls.Add(this.chkMiusMinus);
            this.Controls.Add(this.btnAnnulla);
            this.Controls.Add(this.btnTrova);
            this.Controls.Add(this.txtTrova);
            this.Controls.Add(this.lblTrova);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormBaseTrovaSostituisci";
            this.Text = "FormBaseTrovaSostituisci";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTrova;
        private System.Windows.Forms.CheckBox chkMiusMinus;
        private System.Windows.Forms.CheckBox chkTestoIntorno;
        protected System.Windows.Forms.Button btnAnnulla;
        protected System.Windows.Forms.Button btnTrova;
        protected System.Windows.Forms.TextBox txtTrova;
    }
}