namespace View3D
{
    partial class Main
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonSupport = new System.Windows.Forms.ToolStripButton();
            this.toolStripSaveJob = new System.Windows.Forms.ToolStripButton();
            this.openGCode = new System.Windows.Forms.OpenFileDialog();
            this.saveJobDialog = new System.Windows.Forms.SaveFileDialog();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.splitLog = new System.Windows.Forms.SplitContainer();
            this.splitInfoEdit = new System.Windows.Forms.SplitContainer();
            this.tabControlView = new System.Windows.Forms.TabControl();
            this.tabPage3DView = new System.Windows.Forms.TabPage();
            this.tab = new System.Windows.Forms.TabControl();
            this.tabModel = new System.Windows.Forms.TabPage();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printerSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.timeperiodMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minutes60ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minutes30ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minutes15ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minutes10ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minutes5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minuteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.buildAverageOverMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.secondsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minuteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minutesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minutesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.tdSettings = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitLog)).BeginInit();
            this.splitLog.Panel1.SuspendLayout();
            this.splitLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitInfoEdit)).BeginInit();
            this.splitInfoEdit.Panel1.SuspendLayout();
            this.splitInfoEdit.Panel2.SuspendLayout();
            this.splitInfoEdit.SuspendLayout();
            this.tabControlView.SuspendLayout();
            this.tab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tdSettings)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(97, 49);
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1105, 31);
            this.toolStrip.TabIndex = 2;
            this.toolStrip.Text = "toolStrip1";
            this.toolStrip.Visible = false;
            // 
            // toolStripButtonSupport
            // 
            this.toolStripButtonSupport.Name = "toolStripButtonSupport";
            this.toolStripButtonSupport.Size = new System.Drawing.Size(29, 28);
            // 
            // toolStripSaveJob
            // 
            this.toolStripSaveJob.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSaveJob.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSaveJob.Enabled = false;
            this.toolStripSaveJob.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolStripSaveJob.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripSaveJob.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSaveJob.Name = "toolStripSaveJob";
            this.toolStripSaveJob.Size = new System.Drawing.Size(29, 28);
            this.toolStripSaveJob.Text = "Export";
            this.toolStripSaveJob.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripSaveJob.Click += new System.EventHandler(this.toolStripSaveJob_Click);
            // 
            // openGCode
            // 
            this.openGCode.DefaultExt = "3wn";
            this.openGCode.Filter = "STL-Files|*.stl;";
            this.openGCode.Title = "Import 3wn";
            // 
            // saveJobDialog
            // 
            this.saveJobDialog.DefaultExt = "3wn";
            this.saveJobDialog.Filter = "3wn|*.3wn";
            this.saveJobDialog.Title = "Save 3wn";
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // splitLog
            // 
            this.splitLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitLog.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitLog.Location = new System.Drawing.Point(0, 0);
            this.splitLog.Margin = new System.Windows.Forms.Padding(4);
            this.splitLog.Name = "splitLog";
            this.splitLog.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitLog.Panel1
            // 
            this.splitLog.Panel1.Controls.Add(this.splitInfoEdit);
            this.splitLog.Panel2Collapsed = true;
            this.splitLog.Size = new System.Drawing.Size(1105, 765);
            this.splitLog.SplitterDistance = 359;
            this.splitLog.SplitterWidth = 5;
            this.splitLog.TabIndex = 4;
            // 
            // splitInfoEdit
            // 
            this.splitInfoEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitInfoEdit.Location = new System.Drawing.Point(0, 0);
            this.splitInfoEdit.Margin = new System.Windows.Forms.Padding(4);
            this.splitInfoEdit.Name = "splitInfoEdit";
            // 
            // splitInfoEdit.Panel1
            // 
            this.splitInfoEdit.Panel1.Controls.Add(this.tabControlView);
            this.splitInfoEdit.Panel1MinSize = 0;
            // 
            // splitInfoEdit.Panel2
            // 
            this.splitInfoEdit.Panel2.Controls.Add(this.tab);
            this.splitInfoEdit.Panel2MinSize = 0;
            this.splitInfoEdit.Size = new System.Drawing.Size(1103, 763);
            this.splitInfoEdit.SplitterDistance = 702;
            this.splitInfoEdit.SplitterWidth = 1;
            this.splitInfoEdit.TabIndex = 4;
            // 
            // tabControlView
            // 
            this.tabControlView.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControlView.Controls.Add(this.tabPage3DView);
            this.tabControlView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlView.ItemSize = new System.Drawing.Size(0, 1);
            this.tabControlView.Location = new System.Drawing.Point(0, 0);
            this.tabControlView.Margin = new System.Windows.Forms.Padding(4);
            this.tabControlView.Name = "tabControlView";
            this.tabControlView.Padding = new System.Drawing.Point(0, 0);
            this.tabControlView.SelectedIndex = 0;
            this.tabControlView.Size = new System.Drawing.Size(702, 763);
            this.tabControlView.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControlView.TabIndex = 0;
            this.tabControlView.TabStop = false;
            this.tabControlView.Visible = false;
            // 
            // tabPage3DView
            // 
            this.tabPage3DView.BackColor = System.Drawing.Color.Transparent;
            this.tabPage3DView.Location = new System.Drawing.Point(4, 5);
            this.tabPage3DView.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage3DView.Name = "tabPage3DView";
            this.tabPage3DView.Size = new System.Drawing.Size(694, 754);
            this.tabPage3DView.TabIndex = 0;
            this.tabPage3DView.Text = "3D View";
            // 
            // tab
            // 
            this.tab.Controls.Add(this.tabModel);
            this.tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab.Location = new System.Drawing.Point(0, 0);
            this.tab.Margin = new System.Windows.Forms.Padding(4);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(400, 763);
            this.tab.TabIndex = 3;
            this.tab.SelectedIndexChanged += new System.EventHandler(this.tab_SelectedIndexChanged);
            // 
            // tabModel
            // 
            this.tabModel.BackColor = System.Drawing.Color.Transparent;
            this.tabModel.Location = new System.Drawing.Point(4, 25);
            this.tabModel.Margin = new System.Windows.Forms.Padding(4);
            this.tabModel.Name = "tabModel";
            this.tabModel.Size = new System.Drawing.Size(392, 734);
            this.tabModel.TabIndex = 2;
            this.tabModel.Text = "Object placements";
            this.tabModel.UseVisualStyleBackColor = true;
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(178, 6);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(178, 6);
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            this.languageToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.languageToolStripMenuItem.Text = "Language";
            // 
            // printerSettingsToolStripMenuItem
            // 
            this.printerSettingsToolStripMenuItem.Name = "printerSettingsToolStripMenuItem";
            this.printerSettingsToolStripMenuItem.Size = new System.Drawing.Size(260, 22);
            this.printerSettingsToolStripMenuItem.Text = "&Printer settings";
            this.printerSettingsToolStripMenuItem.Click += new System.EventHandler(this.printerSettingsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(238, 6);
            // 
            // timeperiodMenuItem
            // 
            this.timeperiodMenuItem.Name = "timeperiodMenuItem";
            this.timeperiodMenuItem.Size = new System.Drawing.Size(241, 24);
            this.timeperiodMenuItem.Text = "Timeperiod";
            // 
            // minutes60ToolStripMenuItem
            // 
            this.minutes60ToolStripMenuItem.Name = "minutes60ToolStripMenuItem";
            this.minutes60ToolStripMenuItem.Size = new System.Drawing.Size(69, 22);
            // 
            // minutes30ToolStripMenuItem
            // 
            this.minutes30ToolStripMenuItem.Name = "minutes30ToolStripMenuItem";
            this.minutes30ToolStripMenuItem.Size = new System.Drawing.Size(69, 22);
            // 
            // minutes15ToolStripMenuItem
            // 
            this.minutes15ToolStripMenuItem.Name = "minutes15ToolStripMenuItem";
            this.minutes15ToolStripMenuItem.Size = new System.Drawing.Size(69, 22);
            // 
            // minutes10ToolStripMenuItem
            // 
            this.minutes10ToolStripMenuItem.Name = "minutes10ToolStripMenuItem";
            this.minutes10ToolStripMenuItem.Size = new System.Drawing.Size(69, 22);
            // 
            // minutes5ToolStripMenuItem
            // 
            this.minutes5ToolStripMenuItem.Name = "minutes5ToolStripMenuItem";
            this.minutes5ToolStripMenuItem.Size = new System.Drawing.Size(69, 22);
            // 
            // minuteToolStripMenuItem1
            // 
            this.minuteToolStripMenuItem1.Name = "minuteToolStripMenuItem1";
            this.minuteToolStripMenuItem1.Size = new System.Drawing.Size(69, 22);
            // 
            // buildAverageOverMenuItem
            // 
            this.buildAverageOverMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.secondsToolStripMenuItem,
            this.minuteToolStripMenuItem,
            this.minutesToolStripMenuItem,
            this.minutesToolStripMenuItem1});
            this.buildAverageOverMenuItem.Name = "buildAverageOverMenuItem";
            this.buildAverageOverMenuItem.Size = new System.Drawing.Size(241, 24);
            this.buildAverageOverMenuItem.Text = "Build average over ...";
            // 
            // secondsToolStripMenuItem
            // 
            this.secondsToolStripMenuItem.Name = "secondsToolStripMenuItem";
            this.secondsToolStripMenuItem.Size = new System.Drawing.Size(83, 26);
            // 
            // minuteToolStripMenuItem
            // 
            this.minuteToolStripMenuItem.Name = "minuteToolStripMenuItem";
            this.minuteToolStripMenuItem.Size = new System.Drawing.Size(83, 26);
            // 
            // minutesToolStripMenuItem
            // 
            this.minutesToolStripMenuItem.Name = "minutesToolStripMenuItem";
            this.minutesToolStripMenuItem.Size = new System.Drawing.Size(83, 26);
            // 
            // minutesToolStripMenuItem1
            // 
            this.minutesToolStripMenuItem1.Name = "minutesToolStripMenuItem1";
            this.minutesToolStripMenuItem1.Size = new System.Drawing.Size(83, 26);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(183, 6);
            // 
            // tdSettings
            // 
            this.tdSettings.DataSource = typeof(View3D.view.ThreeDSettings);
            this.tdSettings.DataMemberChanged += new System.EventHandler(this.tdSettings_DataMemberChanged);
            this.tdSettings.CurrentItemChanged += new System.EventHandler(this.tdSettings_CurrentChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1105, 765);
            this.Controls.Add(this.splitLog);
            this.Controls.Add(this.toolStrip);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(796, 737);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View 3D STL Files";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.SizeChanged += new System.EventHandler(this.Main_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Main_KeyDown);
            this.Move += new System.EventHandler(this.Main_Move);
            this.Resize += new System.EventHandler(this.Main_Resize);
            this.splitLog.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitLog)).EndInit();
            this.splitLog.ResumeLayout(false);
            this.splitInfoEdit.Panel1.ResumeLayout(false);
            this.splitInfoEdit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitInfoEdit)).EndInit();
            this.splitInfoEdit.ResumeLayout(false);
            this.tabControlView.ResumeLayout(false);
            this.tab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tdSettings)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        public System.Windows.Forms.OpenFileDialog openGCode;
        public System.Windows.Forms.TabControl tab;
        public System.Windows.Forms.TabPage tabModel;
        public System.Windows.Forms.SaveFileDialog saveJobDialog;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.SplitContainer splitLog;
        public System.Windows.Forms.SplitContainer splitInfoEdit;
        public System.Windows.Forms.ToolStripButton toolStripSaveJob;
        public System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonSupport;
        private System.Windows.Forms.BindingSource tdSettings;
        public System.Windows.Forms.TabControl tabControlView;
        public System.Windows.Forms.TabPage tabPage3DView;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printerSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        public System.Windows.Forms.ToolStripMenuItem timeperiodMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minutes60ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minutes30ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minutes15ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minutes10ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minutes5ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minuteToolStripMenuItem1;
        public System.Windows.Forms.ToolStripMenuItem buildAverageOverMenuItem;
        private System.Windows.Forms.ToolStripMenuItem secondsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minuteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minutesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minutesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
    }
}

