namespace View3D.view
{
    partial class ThreeDControl
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

        #region Vom Komponenten-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThreeDControl));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.toolMoveViewpoint = new System.Windows.Forms.ToolStripButton();
            this.toolStripClear = new System.Windows.Forms.ToolStripButton();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.landObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mminchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inchmmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cloneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.objectInfomationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newWorkspacetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gl = new View3D.view.utils.RHOpenGL();
            this.toolStrip1.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(72, 69);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton4,
            this.toolStripButton6,
            this.toolStripButton7,
            this.toolMoveViewpoint,
            this.toolStripClear});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(77, 624);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Visible = false;
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.BackColor = System.Drawing.Color.Transparent;
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toolStripButton4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(74, 73);
            this.toolStripButton4.Text = "[－]";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.BackColor = System.Drawing.Color.Transparent;
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toolStripButton6.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(74, 73);
            this.toolStripButton6.Text = "Turn";
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.BackColor = System.Drawing.Color.Transparent;
            this.toolStripButton7.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.toolStripButton7.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolStripButton7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton7.Image")));
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(74, 73);
            this.toolStripButton7.Text = "Scale";
            // 
            // toolMoveViewpoint
            // 
            this.toolMoveViewpoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolMoveViewpoint.Image = ((System.Drawing.Image)(resources.GetObject("toolMoveViewpoint.Image")));
            this.toolMoveViewpoint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolMoveViewpoint.Name = "toolMoveViewpoint";
            this.toolMoveViewpoint.Size = new System.Drawing.Size(74, 23);
            this.toolMoveViewpoint.Text = "Move";
            this.toolMoveViewpoint.Visible = false;
            this.toolMoveViewpoint.Click += new System.EventHandler(this.toolMoveViewpoint_Click);
            // 
            // toolStripClear
            // 
            this.toolStripClear.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripClear.Image = ((System.Drawing.Image)(resources.GetObject("toolStripClear.Image")));
            this.toolStripClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripClear.Name = "toolStripClear";
            this.toolStripClear.Size = new System.Drawing.Size(74, 73);
            this.toolStripClear.Text = "Clear";
            this.toolStripClear.Visible = false;
            this.toolStripClear.Click += new System.EventHandler(this.toolStripClear_Click);
            // 
            // timer
            // 
            this.timer.Interval = 1;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.landObjectToolStripMenuItem,
            this.mminchToolStripMenuItem,
            this.inchmmToolStripMenuItem,
            this.resetObjectToolStripMenuItem,
            this.removeObjectToolStripMenuItem,
            this.cloneToolStripMenuItem,
            this.toolStripSeparator3,
            this.objectInfomationToolStripMenuItem,
            this.newWorkspacetoolStripMenuItem});
            this.contextMenu.Name = "contextMenuStrip1";
            this.contextMenu.Size = new System.Drawing.Size(242, 208);
            this.contextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenu_Opening);
            // 
            // landObjectToolStripMenuItem
            // 
            this.landObjectToolStripMenuItem.Name = "landObjectToolStripMenuItem";
            this.landObjectToolStripMenuItem.Size = new System.Drawing.Size(241, 24);
            this.landObjectToolStripMenuItem.Text = "Land Object";
            this.landObjectToolStripMenuItem.Click += new System.EventHandler(this.landObjectToolStripMenuItem_Click);
            // 
            // mminchToolStripMenuItem
            // 
            this.mminchToolStripMenuItem.Name = "mminchToolStripMenuItem";
            this.mminchToolStripMenuItem.Size = new System.Drawing.Size(241, 24);
            this.mminchToolStripMenuItem.Text = "Scale Up (mm>inch)";
            this.mminchToolStripMenuItem.Click += new System.EventHandler(this.mminchToolStripMenuItem_Click);
            // 
            // inchmmToolStripMenuItem
            // 
            this.inchmmToolStripMenuItem.Name = "inchmmToolStripMenuItem";
            this.inchmmToolStripMenuItem.Size = new System.Drawing.Size(241, 24);
            this.inchmmToolStripMenuItem.Text = "Scale Down (inch>mm)";
            this.inchmmToolStripMenuItem.Click += new System.EventHandler(this.inchmmToolStripMenuItem_Click);
            //
            // cloneToolStripMenuItem
            // 
            this.cloneToolStripMenuItem.Name = "cloneToolStripMenuItem";
            this.cloneToolStripMenuItem.Size = new System.Drawing.Size(241, 24);
            this.cloneToolStripMenuItem.Text = "Clone";
            this.cloneToolStripMenuItem.Click += new System.EventHandler(this.cloneToolStripMenuItem_Click);
            // 
            // resetObjectToolStripMenuItem
            // 
            this.resetObjectToolStripMenuItem.Name = "resetObjectToolStripMenuItem";
            this.resetObjectToolStripMenuItem.Size = new System.Drawing.Size(241, 24);
            this.resetObjectToolStripMenuItem.Text = "Reset Object";
            this.resetObjectToolStripMenuItem.Click += new System.EventHandler(this.resetObjectToolStripMenuItem_Click);
            // 
            // removeObjectToolStripMenuItem
            // 
            this.removeObjectToolStripMenuItem.Name = "removeObjectToolStripMenuItem";
            this.removeObjectToolStripMenuItem.Size = new System.Drawing.Size(241, 24);
            this.removeObjectToolStripMenuItem.Text = "Remove Object";
            this.removeObjectToolStripMenuItem.Click += new System.EventHandler(this.removeObjectToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(238, 6);
            // 
            // objectInfomationToolStripMenuItem
            // 
            this.objectInfomationToolStripMenuItem.Name = "objectInfomationToolStripMenuItem";
            this.objectInfomationToolStripMenuItem.Size = new System.Drawing.Size(241, 24);
            this.objectInfomationToolStripMenuItem.Text = "Object infomation";
            this.objectInfomationToolStripMenuItem.Click += new System.EventHandler(this.objectInfomationToolStripMenuItem_Click);
            // 
            // newWorkspacetoolStripMenuItem
            // 
            this.newWorkspacetoolStripMenuItem.Name = "newWorkspacetoolStripMenuItem";
            this.newWorkspacetoolStripMenuItem.Size = new System.Drawing.Size(241, 24);
            this.newWorkspacetoolStripMenuItem.Text = "New Workspace";
            this.newWorkspacetoolStripMenuItem.Click += new System.EventHandler(this.newWorkspacetoolStripMenuItem_Click);
            // 
            // gl
            // 
            this.gl.BackColor = System.Drawing.Color.Transparent;
            this.gl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gl.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gl.Location = new System.Drawing.Point(77, 0);
            this.gl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gl.Name = "gl";
            this.gl.Size = new System.Drawing.Size(753, 624);
            this.gl.TabIndex = 2;
            this.gl.VSync = false;
            this.gl.Paint += new System.Windows.Forms.PaintEventHandler(this.gl_Paint);
            this.gl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ThreeDControl_KeyDown);
            this.gl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ThreeDControl_KeyPress);
            this.gl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gl_MouseDown);
            this.gl.MouseEnter += new System.EventHandler(this.ThreeDControl_MouseEnter);
            this.gl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gl_MouseMove);
            this.gl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gl_MouseUp);
            this.gl.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.gl_MouseWheel);
            this.gl.Resize += new System.EventHandler(this.gl_Resize);
            // 
            // ThreeDControl
            // 
            this.Controls.Add(this.gl);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ThreeDControl";
            this.Size = new System.Drawing.Size(830, 624);
            this.Load += new System.EventHandler(this.ThreeDControl_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ThreeDControl_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ThreeDControl_KeyPress);
            this.MouseEnter += new System.EventHandler(this.ThreeDControl_MouseEnter);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolMoveViewpoint;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private System.Windows.Forms.ToolStripButton toolStripClear;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem landObjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetObjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeObjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem objectInfomationToolStripMenuItem;
        public utils.RHOpenGL gl;
        private System.Windows.Forms.ToolStripMenuItem newWorkspacetoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mminchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inchmmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cloneToolStripMenuItem;
    }
}
