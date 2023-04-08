namespace View3D.view
{
    partial class FormPrinterSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrinterSettings));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageConnection = new System.Windows.Forms.TabPage();
            this.panelConnector = new System.Windows.Forms.Panel();
            this.comboConnector = new System.Windows.Forms.ComboBox();
            this.panelPrinterType = new System.Windows.Forms.Panel();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.bindingConnectors = new System.Windows.Forms.BindingSource(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPageConnection.SuspendLayout();
            this.panelConnector.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingConnectors)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageConnection);
            this.tabControl1.Location = new System.Drawing.Point(0, 35);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(652, 468);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageConnection
            // 
            this.tabPageConnection.AutoScroll = true;
            this.tabPageConnection.BackColor = System.Drawing.Color.Transparent;
            this.tabPageConnection.Controls.Add(this.panelConnector);
            this.tabPageConnection.Location = new System.Drawing.Point(4, 25);
            this.tabPageConnection.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageConnection.Name = "tabPageConnection";
            this.tabPageConnection.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageConnection.Size = new System.Drawing.Size(644, 439);
            this.tabPageConnection.TabIndex = 0;
            this.tabPageConnection.Text = "Connection";
            this.tabPageConnection.UseVisualStyleBackColor = true;
            // 
            // panelConnector
            // 
            this.panelConnector.Controls.Add(this.comboConnector);
            this.panelConnector.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelConnector.Location = new System.Drawing.Point(4, 4);
            this.panelConnector.Margin = new System.Windows.Forms.Padding(4);
            this.panelConnector.Name = "panelConnector";
            this.panelConnector.Size = new System.Drawing.Size(636, 42);
            this.panelConnector.TabIndex = 18;
            // 
            // comboConnector
            // 
            this.comboConnector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboConnector.FormattingEnabled = true;
            this.comboConnector.Location = new System.Drawing.Point(126, 4);
            this.comboConnector.Margin = new System.Windows.Forms.Padding(4);
            this.comboConnector.Name = "comboConnector";
            this.comboConnector.Size = new System.Drawing.Size(249, 23);
            this.comboConnector.TabIndex = 1;
            this.comboConnector.SelectedIndexChanged += new System.EventHandler(this.comboConnector_SelectedIndexChanged);
            // 
            // panelPrinterType
            // 
            this.panelPrinterType.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPrinterType.Location = new System.Drawing.Point(0, 0);
            this.panelPrinterType.Margin = new System.Windows.Forms.Padding(4);
            this.panelPrinterType.Name = "panelPrinterType";
            this.panelPrinterType.Size = new System.Drawing.Size(613, 72);
            this.panelPrinterType.TabIndex = 19;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // FormPrinterSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(652, 586);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPrinterSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Printer settings";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPrinterSettings_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPageConnection.ResumeLayout(false);
            this.panelConnector.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingConnectors)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageConnection;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.Panel panelPrinterType;
        private System.Windows.Forms.Panel panelConnector;
        private System.Windows.Forms.ComboBox comboConnector;
        private System.Windows.Forms.BindingSource bindingConnectors;
    }
}