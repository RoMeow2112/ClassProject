namespace _19110085_NguyenTranKhai_QLSV
{
    partial class StaticsForm
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
            this.panTotal = new System.Windows.Forms.Panel();
            this.LabelTotal = new System.Windows.Forms.Label();
            this.panMale = new System.Windows.Forms.Panel();
            this.LabelMale = new System.Windows.Forms.Label();
            this.panFemale = new System.Windows.Forms.Panel();
            this.LabelFemale = new System.Windows.Forms.Label();
            this.panTotal.SuspendLayout();
            this.panMale.SuspendLayout();
            this.panFemale.SuspendLayout();
            this.SuspendLayout();
            // 
            // panTotal
            // 
            this.panTotal.BackColor = System.Drawing.Color.Blue;
            this.panTotal.Controls.Add(this.LabelTotal);
            this.panTotal.Location = new System.Drawing.Point(12, 12);
            this.panTotal.Name = "panTotal";
            this.panTotal.Size = new System.Drawing.Size(776, 189);
            this.panTotal.TabIndex = 0;
            // 
            // LabelTotal
            // 
            this.LabelTotal.AutoSize = true;
            this.LabelTotal.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTotal.ForeColor = System.Drawing.Color.White;
            this.LabelTotal.Location = new System.Drawing.Point(23, 63);
            this.LabelTotal.Name = "LabelTotal";
            this.LabelTotal.Size = new System.Drawing.Size(0, 55);
            this.LabelTotal.TabIndex = 0;
            this.LabelTotal.MouseEnter += new System.EventHandler(this.LabelTotal_MouseEnter);
            this.LabelTotal.MouseLeave += new System.EventHandler(this.LabelTotal_MouseLeave);
            // 
            // panMale
            // 
            this.panMale.BackColor = System.Drawing.Color.LimeGreen;
            this.panMale.Controls.Add(this.LabelMale);
            this.panMale.Location = new System.Drawing.Point(12, 207);
            this.panMale.Name = "panMale";
            this.panMale.Size = new System.Drawing.Size(384, 231);
            this.panMale.TabIndex = 1;
            // 
            // LabelMale
            // 
            this.LabelMale.AutoSize = true;
            this.LabelMale.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelMale.ForeColor = System.Drawing.Color.White;
            this.LabelMale.Location = new System.Drawing.Point(27, 91);
            this.LabelMale.Name = "LabelMale";
            this.LabelMale.Size = new System.Drawing.Size(0, 32);
            this.LabelMale.TabIndex = 0;
            this.LabelMale.MouseEnter += new System.EventHandler(this.LabelMale_MouseEnter);
            this.LabelMale.MouseLeave += new System.EventHandler(this.LabelMale_MouseLeave);
            // 
            // panFemale
            // 
            this.panFemale.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panFemale.Controls.Add(this.LabelFemale);
            this.panFemale.Location = new System.Drawing.Point(404, 207);
            this.panFemale.Name = "panFemale";
            this.panFemale.Size = new System.Drawing.Size(384, 231);
            this.panFemale.TabIndex = 2;
            // 
            // LabelFemale
            // 
            this.LabelFemale.AutoSize = true;
            this.LabelFemale.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelFemale.ForeColor = System.Drawing.Color.White;
            this.LabelFemale.Location = new System.Drawing.Point(38, 91);
            this.LabelFemale.Name = "LabelFemale";
            this.LabelFemale.Size = new System.Drawing.Size(0, 32);
            this.LabelFemale.TabIndex = 0;
            this.LabelFemale.MouseEnter += new System.EventHandler(this.LabelFemale_MouseEnter);
            this.LabelFemale.MouseLeave += new System.EventHandler(this.LabelFemale_MouseLeave);
            // 
            // StaticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panFemale);
            this.Controls.Add(this.panMale);
            this.Controls.Add(this.panTotal);
            this.Name = "StaticsForm";
            this.Text = "StaticsForm";
            this.Load += new System.EventHandler(this.StaticsForm_Load);
            this.panTotal.ResumeLayout(false);
            this.panTotal.PerformLayout();
            this.panMale.ResumeLayout(false);
            this.panMale.PerformLayout();
            this.panFemale.ResumeLayout(false);
            this.panFemale.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panTotal;
        private System.Windows.Forms.Panel panMale;
        private System.Windows.Forms.Panel panFemale;
        private System.Windows.Forms.Label LabelTotal;
        private System.Windows.Forms.Label LabelMale;
        private System.Windows.Forms.Label LabelFemale;
    }
}