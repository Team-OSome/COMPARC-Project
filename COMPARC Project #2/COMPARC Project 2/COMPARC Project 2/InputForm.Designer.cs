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
            this.pipelineMapPanel = new System.Windows.Forms.Panel();
            this.stepButton = new System.Windows.Forms.Button();
            this.skipButton = new System.Windows.Forms.Button();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.opcodeLbl = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pipelineMapLbl = new System.Windows.Forms.Label();
            this.internalPipelineRegistersLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // programTB
            // 
            this.programTB.Location = new System.Drawing.Point(12, 25);
            this.programTB.Multiline = true;
            this.programTB.Name = "programTB";
            this.programTB.Size = new System.Drawing.Size(231, 481);
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
            this.enterProgLbl.Location = new System.Drawing.Point(12, 9);
            this.enterProgLbl.Name = "enterProgLbl";
            this.enterProgLbl.Size = new System.Drawing.Size(76, 13);
            this.enterProgLbl.TabIndex = 2;
            this.enterProgLbl.Text = "Program Input:";
            // 
            // simulateBtn
            // 
            this.simulateBtn.Location = new System.Drawing.Point(145, 514);
            this.simulateBtn.Name = "simulateBtn";
            this.simulateBtn.Size = new System.Drawing.Size(98, 23);
            this.simulateBtn.TabIndex = 3;
            this.simulateBtn.Text = "Simulate";
            this.simulateBtn.UseVisualStyleBackColor = true;
            this.simulateBtn.Click += new System.EventHandler(this.simulateBtn_Click);
            // 
            // registerButton
            // 
            this.registerButton.Location = new System.Drawing.Point(502, 25);
            this.registerButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(75, 22);
            this.registerButton.TabIndex = 4;
            this.registerButton.Text = "Registers";
            this.registerButton.UseVisualStyleBackColor = true;
            this.registerButton.Click += new System.EventHandler(this.registerButton_Click);
            // 
            // memoryButton
            // 
            this.memoryButton.Location = new System.Drawing.Point(502, 51);
            this.memoryButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.memoryButton.Name = "memoryButton";
            this.memoryButton.Size = new System.Drawing.Size(75, 22);
            this.memoryButton.TabIndex = 5;
            this.memoryButton.Text = "Memory";
            this.memoryButton.UseVisualStyleBackColor = true;
            // 
            // opcodePanel
            // 
            this.opcodePanel.BackColor = System.Drawing.SystemColors.Control;
            this.opcodePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.opcodePanel.Location = new System.Drawing.Point(270, 25);
            this.opcodePanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.opcodePanel.Name = "opcodePanel";
            this.opcodePanel.Size = new System.Drawing.Size(194, 271);
            this.opcodePanel.TabIndex = 6;
            // 
            // pipelineMapPanel
            // 
            this.pipelineMapPanel.BackColor = System.Drawing.SystemColors.Control;
            this.pipelineMapPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pipelineMapPanel.Location = new System.Drawing.Point(270, 334);
            this.pipelineMapPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pipelineMapPanel.Name = "pipelineMapPanel";
            this.pipelineMapPanel.Size = new System.Drawing.Size(543, 172);
            this.pipelineMapPanel.TabIndex = 7;
            // 
            // stepButton
            // 
            this.stepButton.Location = new System.Drawing.Point(1042, 496);
            this.stepButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.stepButton.Name = "stepButton";
            this.stepButton.Size = new System.Drawing.Size(46, 41);
            this.stepButton.TabIndex = 8;
            this.stepButton.Text = ">";
            this.stepButton.UseVisualStyleBackColor = true;
            // 
            // skipButton
            // 
            this.skipButton.Location = new System.Drawing.Point(1093, 496);
            this.skipButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.skipButton.Name = "skipButton";
            this.skipButton.Size = new System.Drawing.Size(46, 41);
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
            this.opcodeLbl.Location = new System.Drawing.Point(268, 9);
            this.opcodeLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.opcodeLbl.Name = "opcodeLbl";
            this.opcodeLbl.Size = new System.Drawing.Size(45, 13);
            this.opcodeLbl.TabIndex = 10;
            this.opcodeLbl.Text = "Opcode";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(835, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(304, 466);
            this.panel1.TabIndex = 11;
            // 
            // pipelineMapLbl
            // 
            this.pipelineMapLbl.AutoSize = true;
            this.pipelineMapLbl.Location = new System.Drawing.Point(271, 316);
            this.pipelineMapLbl.Name = "pipelineMapLbl";
            this.pipelineMapLbl.Size = new System.Drawing.Size(68, 13);
            this.pipelineMapLbl.TabIndex = 12;
            this.pipelineMapLbl.Text = "Pipeline Map";
            // 
            // internalPipelineRegistersLbl
            // 
            this.internalPipelineRegistersLbl.AutoSize = true;
            this.internalPipelineRegistersLbl.Location = new System.Drawing.Point(835, 8);
            this.internalPipelineRegistersLbl.Name = "internalPipelineRegistersLbl";
            this.internalPipelineRegistersLbl.Size = new System.Drawing.Size(129, 13);
            this.internalPipelineRegistersLbl.TabIndex = 13;
            this.internalPipelineRegistersLbl.Text = "Internal Pipeline Registers";
            // 
            // InputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 561);
            this.Controls.Add(this.internalPipelineRegistersLbl);
            this.Controls.Add(this.pipelineMapLbl);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.opcodeLbl);
            this.Controls.Add(this.skipButton);
            this.Controls.Add(this.stepButton);
            this.Controls.Add(this.pipelineMapPanel);
            this.Controls.Add(this.opcodePanel);
            this.Controls.Add(this.memoryButton);
            this.Controls.Add(this.registerButton);
            this.Controls.Add(this.simulateBtn);
            this.Controls.Add(this.enterProgLbl);
            this.Controls.Add(this.programTB);
            this.Name = "InputForm";
            this.Text = "MIPS 64 - Flush - Set B";
            this.Load += new System.EventHandler(this.InputForm_Load);
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
        private System.Windows.Forms.Panel pipelineMapPanel;
        private System.Windows.Forms.Button stepButton;
        private System.Windows.Forms.Button skipButton;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Label opcodeLbl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label pipelineMapLbl;
        private System.Windows.Forms.Label internalPipelineRegistersLbl;
        
    }
}

