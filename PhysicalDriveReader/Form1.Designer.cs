namespace PhysicalDriveReader
{
    partial class Form1
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
            this.hexBox = new Be.Windows.Forms.HexBox();
            this.cmbDrives = new System.Windows.Forms.ComboBox();
            this.txtSector = new System.Windows.Forms.TextBox();
            this.lblStatusMsg = new System.Windows.Forms.Label();
            this.btnReadSector = new System.Windows.Forms.Button();
            this.btnReadRandSector = new System.Windows.Forms.Button();
            this.lblMediaType = new System.Windows.Forms.Label();
            this.lblTotalSectors = new System.Windows.Forms.Label();
            this.lblTotalSize = new System.Windows.Forms.Label();
            this.lblBytesPerSector = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // hexBox
            // 
            this.hexBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hexBox.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hexBox.LineInfoForeColor = System.Drawing.Color.Empty;
            this.hexBox.LineInfoVisible = true;
            this.hexBox.Location = new System.Drawing.Point(12, 123);
            this.hexBox.Name = "hexBox";
            this.hexBox.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexBox.Size = new System.Drawing.Size(648, 469);
            this.hexBox.StringViewVisible = true;
            this.hexBox.TabIndex = 0;
            this.hexBox.UseFixedBytesPerLine = true;
            this.hexBox.VScrollBarVisible = true;
            // 
            // cmbDrives
            // 
            this.cmbDrives.FormattingEnabled = true;
            this.cmbDrives.Location = new System.Drawing.Point(12, 15);
            this.cmbDrives.Name = "cmbDrives";
            this.cmbDrives.Size = new System.Drawing.Size(121, 21);
            this.cmbDrives.TabIndex = 1;
            this.cmbDrives.SelectedIndexChanged += new System.EventHandler(this.cmbDrives_SelectedIndexChanged);
            // 
            // txtSector
            // 
            this.txtSector.Location = new System.Drawing.Point(12, 71);
            this.txtSector.Name = "txtSector";
            this.txtSector.Size = new System.Drawing.Size(121, 20);
            this.txtSector.TabIndex = 2;
            this.txtSector.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSector_KeyDown);
            // 
            // lblStatusMsg
            // 
            this.lblStatusMsg.AutoSize = true;
            this.lblStatusMsg.Location = new System.Drawing.Point(12, 107);
            this.lblStatusMsg.Name = "lblStatusMsg";
            this.lblStatusMsg.Size = new System.Drawing.Size(0, 13);
            this.lblStatusMsg.TabIndex = 3;
            // 
            // btnReadSector
            // 
            this.btnReadSector.Location = new System.Drawing.Point(174, 71);
            this.btnReadSector.Name = "btnReadSector";
            this.btnReadSector.Size = new System.Drawing.Size(108, 23);
            this.btnReadSector.TabIndex = 4;
            this.btnReadSector.Text = "Read Sector";
            this.btnReadSector.UseVisualStyleBackColor = true;
            this.btnReadSector.Click += new System.EventHandler(this.btnReadSector_Click);
            // 
            // btnReadRandSector
            // 
            this.btnReadRandSector.Location = new System.Drawing.Point(372, 71);
            this.btnReadRandSector.Name = "btnReadRandSector";
            this.btnReadRandSector.Size = new System.Drawing.Size(169, 23);
            this.btnReadRandSector.TabIndex = 5;
            this.btnReadRandSector.Text = "Read Random Sector";
            this.btnReadRandSector.UseVisualStyleBackColor = true;
            this.btnReadRandSector.Click += new System.EventHandler(this.btnReadRandSector_Click);
            // 
            // lblMediaType
            // 
            this.lblMediaType.AutoSize = true;
            this.lblMediaType.Location = new System.Drawing.Point(174, 15);
            this.lblMediaType.Name = "lblMediaType";
            this.lblMediaType.Size = new System.Drawing.Size(66, 13);
            this.lblMediaType.TabIndex = 6;
            this.lblMediaType.Text = "Media Type:";
            // 
            // lblTotalSectors
            // 
            this.lblTotalSectors.AutoSize = true;
            this.lblTotalSectors.Location = new System.Drawing.Point(174, 36);
            this.lblTotalSectors.Name = "lblTotalSectors";
            this.lblTotalSectors.Size = new System.Drawing.Size(73, 13);
            this.lblTotalSectors.TabIndex = 7;
            this.lblTotalSectors.Text = "Total Sectors:";
            // 
            // lblTotalSize
            // 
            this.lblTotalSize.AutoSize = true;
            this.lblTotalSize.Location = new System.Drawing.Point(372, 15);
            this.lblTotalSize.Name = "lblTotalSize";
            this.lblTotalSize.Size = new System.Drawing.Size(57, 13);
            this.lblTotalSize.TabIndex = 8;
            this.lblTotalSize.Text = "Total Size:";
            // 
            // lblBytesPerSector
            // 
            this.lblBytesPerSector.AutoSize = true;
            this.lblBytesPerSector.Location = new System.Drawing.Point(372, 36);
            this.lblBytesPerSector.Name = "lblBytesPerSector";
            this.lblBytesPerSector.Size = new System.Drawing.Size(89, 13);
            this.lblBytesPerSector.TabIndex = 9;
            this.lblBytesPerSector.Text = "Bytes Per Sector:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 604);
            this.Controls.Add(this.lblBytesPerSector);
            this.Controls.Add(this.lblTotalSize);
            this.Controls.Add(this.lblTotalSectors);
            this.Controls.Add(this.lblMediaType);
            this.Controls.Add(this.btnReadRandSector);
            this.Controls.Add(this.btnReadSector);
            this.Controls.Add(this.lblStatusMsg);
            this.Controls.Add(this.txtSector);
            this.Controls.Add(this.cmbDrives);
            this.Controls.Add(this.hexBox);
            this.Name = "Form1";
            this.Text = "Sector Reader";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Be.Windows.Forms.HexBox hexBox;
        private System.Windows.Forms.ComboBox cmbDrives;
        private System.Windows.Forms.TextBox txtSector;
        private System.Windows.Forms.Label lblStatusMsg;
        private System.Windows.Forms.Button btnReadSector;
        private System.Windows.Forms.Button btnReadRandSector;
        private System.Windows.Forms.Label lblMediaType;
        private System.Windows.Forms.Label lblTotalSectors;
        private System.Windows.Forms.Label lblTotalSize;
        private System.Windows.Forms.Label lblBytesPerSector;
    }
}

