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
            this.registerButton = new System.Windows.Forms.Button();
            this.memoryButton = new System.Windows.Forms.Button();
            this.opcodePanel = new System.Windows.Forms.Panel();
            this.opcodeMapPanel = new System.Windows.Forms.Panel();
            this.stepButton = new System.Windows.Forms.Button();
            this.skipButton = new System.Windows.Forms.Button();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.opcodeLbl = new System.Windows.Forms.Label();
            this.opcodePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // programTB
            // 
            this.programTB.Location = new System.Drawing.Point(16, 31);
            this.programTB.Margin = new System.Windows.Forms.Padding(4);
            this.programTB.Multiline = true;
            this.programTB.Name = "programTB";
            this.programTB.Size = new System.Drawing.Size(307, 619);
            this.programTB.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // enterProgLbl
            // 
            this.enterProgLbl.AutoSize = true;
            this.enterProgLbl.Location = new System.Drawing.Point(16, 11);
            this.enterProgLbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.enterProgLbl.Name = "enterProgLbl";
            this.enterProgLbl.Size = new System.Drawing.Size(101, 17);
            this.enterProgLbl.TabIndex = 2;
            this.enterProgLbl.Text = "Program Input:";
            // 
            // simulateBtn
            // 
            this.simulateBtn.Location = new System.Drawing.Point(360, 622);
            this.simulateBtn.Margin = new System.Windows.Forms.Padding(4);
            this.simulateBtn.Name = "simulateBtn";
            this.simulateBtn.Size = new System.Drawing.Size(130, 28);
            this.simulateBtn.TabIndex = 3;
            this.simulateBtn.Text = "Simulate";
            this.simulateBtn.UseVisualStyleBackColor = true;
            this.simulateBtn.Click += new System.EventHandler(this.simulateBtn_Click);
            // 
            // registerButton
            // 
            this.registerButton.Location = new System.Drawing.Point(929, 31);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(100, 27);
            this.registerButton.TabIndex = 4;
            this.registerButton.Text = "Registers";
            this.registerButton.UseVisualStyleBackColor = true;
            this.registerButton.Click += new System.EventHandler(this.registerButton_Click);
            // 
            // memoryButton
            // 
            this.memoryButton.Location = new System.Drawing.Point(929, 64);
            this.memoryButton.Name = "memoryButton";
            this.memoryButton.Size = new System.Drawing.Size(100, 27);
            this.memoryButton.TabIndex = 5;
            this.memoryButton.Text = "Memory";
            this.memoryButton.UseVisualStyleBackColor = true;
            // 
            // opcodePanel
            // 
            this.opcodePanel.BackColor = System.Drawing.SystemColors.Control;
            this.opcodePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.opcodePanel.Location = new System.Drawing.Point(360, 31);
            this.opcodePanel.Name = "opcodePanel";
            this.opcodePanel.Size = new System.Drawing.Size(544, 269);
            this.opcodePanel.TabIndex = 6;
            // 
            // opcodeMapPanel
            // 
            this.opcodeMapPanel.BackColor = System.Drawing.SystemColors.Control;
            this.opcodeMapPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.opcodeMapPanel.Location = new System.Drawing.Point(360, 354);
            this.opcodeMapPanel.Name = "opcodeMapPanel";
            this.opcodeMapPanel.Size = new System.Drawing.Size(1158, 164);
            this.opcodeMapPanel.TabIndex = 7;
            // 
            // stepButton
            // 
            this.stepButton.Location = new System.Drawing.Point(360, 524);
            this.stepButton.Name = "stepButton";
            this.stepButton.Size = new System.Drawing.Size(62, 50);
            this.stepButton.TabIndex = 8;
            this.stepButton.Text = ">";
            this.stepButton.UseVisualStyleBackColor = true;
            // 
            // skipButton
            // 
            this.skipButton.Location = new System.Drawing.Point(428, 524);
            this.skipButton.Name = "skipButton";
            this.skipButton.Size = new System.Drawing.Size(62, 50);
            this.skipButton.TabIndex = 9;
            this.skipButton.Text = ">>>";
            this.skipButton.UseVisualStyleBackColor = true;
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // opcodeLbl
            // 
            this.opcodeLbl.AutoSize = true;
            this.opcodeLbl.Location = new System.Drawing.Point(357, 11);
            this.opcodeLbl.Name = "opcodeLbl";
            this.opcodeLbl.Size = new System.Drawing.Size(58, 17);
            this.opcodeLbl.TabIndex = 10;
            this.opcodeLbl.Text = "Opcode";
            // 
            // InputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1579, 690);
            this.Controls.Add(this.opcodeLbl);
            this.Controls.Add(this.skipButton);
            this.Controls.Add(this.stepButton);
            this.Controls.Add(this.opcodeMapPanel);
            this.Controls.Add(this.opcodePanel);
            this.Controls.Add(this.memoryButton);
            this.Controls.Add(this.registerButton);
            this.Controls.Add(this.simulateBtn);
            this.Controls.Add(this.enterProgLbl);
            this.Controls.Add(this.programTB);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "InputForm";
            this.Text = "MIPS 64 - Flush - Set B";
            this.Load += new System.EventHandler(this.InputForm_Load);
            this.opcodePanel.ResumeLayout(false);
            this.opcodePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox programTB;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label enterProgLbl;
        private System.Windows.Forms.Button simulateBtn;
        private System.Windows.Forms.Button registerButton;
        private System.Windows.Forms.Button memoryButton;
        private System.Windows.Forms.Panel opcodePanel;
        private System.Windows.Forms.Panel opcodeMapPanel;
        private System.Windows.Forms.Button stepButton;
        private System.Windows.Forms.Button skipButton;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Label opcodeLbl;
        
    }
}

