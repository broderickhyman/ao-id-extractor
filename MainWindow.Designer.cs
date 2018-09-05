namespace ao_id_extractor
{
    partial class MainWindow
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
            this.tbAOFolder = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbOutFolder = new System.Windows.Forms.TextBox();
            this.btnSelectOutFolder = new System.Windows.Forms.Button();
            this.btnSelectAOFolder = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbExtractionMode = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbExportType = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnExtract = new System.Windows.Forms.Button();
            this.tbConsole = new System.Windows.Forms.TextBox();
            this.btnSelectFromRegistry = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbAOFolder
            // 
            this.tbAOFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAOFolder.Location = new System.Drawing.Point(95, 46);
            this.tbAOFolder.Name = "tbAOFolder";
            this.tbAOFolder.Size = new System.Drawing.Size(396, 20);
            this.tbAOFolder.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnSelectFromRegistry);
            this.groupBox1.Controls.Add(this.btnSelectAOFolder);
            this.groupBox1.Controls.Add(this.btnSelectOutFolder);
            this.groupBox1.Controls.Add(this.tbOutFolder);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tbAOFolder);
            this.groupBox1.Location = new System.Drawing.Point(6, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(673, 78);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Paths";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Output Folder:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "AO Main Folder:";
            // 
            // tbOutFolder
            // 
            this.tbOutFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbOutFolder.Location = new System.Drawing.Point(95, 19);
            this.tbOutFolder.Name = "tbOutFolder";
            this.tbOutFolder.Size = new System.Drawing.Size(491, 20);
            this.tbOutFolder.TabIndex = 3;
            // 
            // btnSelectOutFolder
            // 
            this.btnSelectOutFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectOutFolder.Location = new System.Drawing.Point(592, 17);
            this.btnSelectOutFolder.Name = "btnSelectOutFolder";
            this.btnSelectOutFolder.Size = new System.Drawing.Size(75, 23);
            this.btnSelectOutFolder.TabIndex = 5;
            this.btnSelectOutFolder.Text = "Select";
            this.btnSelectOutFolder.UseVisualStyleBackColor = true;
            this.btnSelectOutFolder.Click += new System.EventHandler(this.btnSelectOutFolder_Click);
            // 
            // btnSelectAOFolder
            // 
            this.btnSelectAOFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectAOFolder.Location = new System.Drawing.Point(592, 44);
            this.btnSelectAOFolder.Name = "btnSelectAOFolder";
            this.btnSelectAOFolder.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAOFolder.TabIndex = 6;
            this.btnSelectAOFolder.Text = "Select";
            this.btnSelectAOFolder.UseVisualStyleBackColor = true;
            this.btnSelectAOFolder.Click += new System.EventHandler(this.btnSelectAOFolder_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cbExportType);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cbExtractionMode);
            this.groupBox2.Location = new System.Drawing.Point(6, 85);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(673, 56);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Settings";
            // 
            // cbExtractionMode
            // 
            this.cbExtractionMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbExtractionMode.FormattingEnabled = true;
            this.cbExtractionMode.Items.AddRange(new object[] {
            "Item Extraction",
            "Location Extraction",
            "Resource Extraction",
            "Dump All XML",
            "Extract Items & Locations & Resource"});
            this.cbExtractionMode.Location = new System.Drawing.Point(99, 19);
            this.cbExtractionMode.Name = "cbExtractionMode";
            this.cbExtractionMode.Size = new System.Drawing.Size(121, 21);
            this.cbExtractionMode.TabIndex = 0;
            this.cbExtractionMode.SelectedIndexChanged += new System.EventHandler(this.cbExtractionMode_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Extraction Mode:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(473, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Export Type:";
            // 
            // cbExportType
            // 
            this.cbExportType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbExportType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbExportType.FormattingEnabled = true;
            this.cbExportType.Items.AddRange(new object[] {
            "Text List",
            "JSON",
            "Both"});
            this.cbExportType.Location = new System.Drawing.Point(546, 19);
            this.cbExportType.Name = "cbExportType";
            this.cbExportType.Size = new System.Drawing.Size(121, 21);
            this.cbExportType.TabIndex = 4;
            this.cbExportType.SelectedIndexChanged += new System.EventHandler(this.cbExportType_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.tbConsole);
            this.groupBox3.Location = new System.Drawing.Point(6, 147);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(673, 316);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Console";
            // 
            // btnExtract
            // 
            this.btnExtract.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExtract.Location = new System.Drawing.Point(598, 471);
            this.btnExtract.Name = "btnExtract";
            this.btnExtract.Size = new System.Drawing.Size(75, 23);
            this.btnExtract.TabIndex = 4;
            this.btnExtract.Text = "Extract";
            this.btnExtract.UseVisualStyleBackColor = true;
            this.btnExtract.Click += new System.EventHandler(this.btnExtract_Click);
            // 
            // tbConsole
            // 
            this.tbConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbConsole.Location = new System.Drawing.Point(6, 20);
            this.tbConsole.Multiline = true;
            this.tbConsole.Name = "tbConsole";
            this.tbConsole.ReadOnly = true;
            this.tbConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbConsole.Size = new System.Drawing.Size(661, 290);
            this.tbConsole.TabIndex = 0;
            this.tbConsole.WordWrap = false;
            // 
            // btnSelectFromRegistry
            // 
            this.btnSelectFromRegistry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectFromRegistry.Location = new System.Drawing.Point(497, 44);
            this.btnSelectFromRegistry.Name = "btnSelectFromRegistry";
            this.btnSelectFromRegistry.Size = new System.Drawing.Size(89, 23);
            this.btnSelectFromRegistry.TabIndex = 7;
            this.btnSelectFromRegistry.Text = "From Registry";
            this.btnSelectFromRegistry.UseVisualStyleBackColor = true;
            this.btnSelectFromRegistry.Click += new System.EventHandler(this.btnSelectFromRegistry_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 506);
            this.Controls.Add(this.btnExtract);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(510, 403);
            this.Name = "MainWindow";
            this.Text = "AO Binary Extractor";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbAOFolder;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbOutFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelectAOFolder;
        private System.Windows.Forms.Button btnSelectOutFolder;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbExtractionMode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbExportType;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnExtract;
        private System.Windows.Forms.Button btnSelectFromRegistry;
        public System.Windows.Forms.TextBox tbConsole;
    }
}