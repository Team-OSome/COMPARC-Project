namespace COMPARC_Project_2
{
    partial class InputForm
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
            this.components = new System.ComponentModel.Container();
            this.programTB = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.enterProgLbl = new System.Windows.Forms.Label();
            this.simulateBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // programTB
            // 
            this.programTB.Location = new System.Drawing.Point(12, 25);
            this.programTB.Multiline = true;
            this.programTB.Name = "programTB";
            this.programTB.Size = new System.Drawing.Size(337, 504);
            this.programTB.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // enterProgLbl
            // 
            this.enterProgLbl.AutoSize = true;
            this.enterProgLbl.Location = new System.Drawing.Point(12, 9);
            this.enterProgLbl.Name = "enterProgLbl";
            this.enterProgLbl.Size = new System.Drawing.Size(76, 13);
            this.enterProgLbl.TabIndex = 2;
            this.enterProgLbl.Text = "Program Input:";
            // 
            // simulateBtn
            // 
            this.simulateBtn.Location = new System.Drawing.Point(1002, 506);
            this.simulateBtn.Name = "simulateBtn";
            this.simulateBtn.Size = new System.Drawing.Size(75, 23);
            this.simulateBtn.TabIndex = 3;
            this.simulateBtn.Text = "Simulate";
            this.simulateBtn.UseVisualStyleBackColor = true;
            this.simulateBtn.Click += new System.EventHandler(this.simulateBtn_Click);
            // 
            // InputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 561);
            this.Controls.Add(this.simulateBtn);
            this.Controls.Add(this.enterProgLbl);
            this.Controls.Add(this.programTB);
            this.Name = "InputForm";
            this.Text = "MIPS 64 - Flush - Set B";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox programTB;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label enterProgLbl;
        private System.Windows.Forms.Button simulateBtn;
    }
}

