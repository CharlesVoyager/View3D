namespace View3D.view
{
    partial class STLComposer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(STLComposer));
            this.panelControls = new System.Windows.Forms.Panel();
            this.panelCut = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelCutPosition = new System.Windows.Forms.Label();
            this.checkCutFaces = new System.Windows.Forms.CheckBox();
            this.panelAnalysis = new System.Windows.Forms.Panel();
            this.groupBoxObjectAnalysis = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.textModied = new System.Windows.Forms.Label();
            this.labelModified = new System.Windows.Forms.Label();
            this.textVertices = new System.Windows.Forms.Label();
            this.labelVertices = new System.Windows.Forms.Label();
            this.textEdges = new System.Windows.Forms.Label();
            this.labelEdges = new System.Windows.Forms.Label();
            this.textFaces = new System.Windows.Forms.Label();
            this.labelFaces = new System.Windows.Forms.Label();
            this.textLoopEdges = new System.Windows.Forms.Label();
            this.labelLoopEdges = new System.Windows.Forms.Label();
            this.textShells = new System.Windows.Forms.Label();
            this.labelShells = new System.Windows.Forms.Label();
            this.textIntersectingTriangles = new System.Windows.Forms.Label();
            this.labelIntersectingTriangles = new System.Windows.Forms.Label();
            this.textManifold = new System.Windows.Forms.Label();
            this.labelManifold = new System.Windows.Forms.Label();
            this.textHighlyConnected = new System.Windows.Forms.Label();
            this.labelHighConnected = new System.Windows.Forms.Label();
            this.textNormals = new System.Windows.Forms.Label();
            this.labelNormals = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textTransX = new System.Windows.Forms.TextBox();
            this.buttonLockAspect = new System.Windows.Forms.Button();
            this.imageList16 = new System.Windows.Forms.ImageList(this.components);
            this.textTransY = new System.Windows.Forms.TextBox();
            this.textScaleX = new System.Windows.Forms.TextBox();
            this.textScaleY = new System.Windows.Forms.TextBox();
            this.textRotX = new System.Windows.Forms.TextBox();
            this.textRotY = new System.Windows.Forms.TextBox();
            this.textTransZ = new System.Windows.Forms.TextBox();
            this.textScaleZ = new System.Windows.Forms.TextBox();
            this.textRotZ = new System.Windows.Forms.TextBox();
            this.listObjects = new System.Windows.Forms.ListView();
            this.columnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnMesh = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnCollision = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDelete = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.openFileSTL = new System.Windows.Forms.OpenFileDialog();
            this.saveSTL = new System.Windows.Forms.SaveFileDialog();
            this.cutAzimuthSlider = new MB.Controls.ColorSlider();
            this.cutInclinationSlider = new MB.Controls.ColorSlider();
            this.cutPositionSlider = new MB.Controls.ColorSlider();
            this.panelControls.SuspendLayout();
            this.panelCut.SuspendLayout();
            this.panelAnalysis.SuspendLayout();
            this.groupBoxObjectAnalysis.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControls
            // 
            this.panelControls.AutoScroll = true;
            this.panelControls.Controls.Add(this.panelCut);
            this.panelControls.Controls.Add(this.panelAnalysis);
            this.panelControls.Controls.Add(this.panel1);
            this.panelControls.Controls.Add(this.listObjects);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControls.Location = new System.Drawing.Point(0, 0);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(360, 576);
            this.panelControls.TabIndex = 0;
            // 
            // panelCut
            // 
            this.panelCut.Controls.Add(this.label5);
            this.panelCut.Controls.Add(this.label1);
            this.panelCut.Controls.Add(this.labelCutPosition);
            this.panelCut.Controls.Add(this.cutAzimuthSlider);
            this.panelCut.Controls.Add(this.cutInclinationSlider);
            this.panelCut.Controls.Add(this.cutPositionSlider);
            this.panelCut.Controls.Add(this.checkCutFaces);
            this.panelCut.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCut.Location = new System.Drawing.Point(0, 421);
            this.panelCut.Name = "panelCut";
            this.panelCut.Size = new System.Drawing.Size(360, 106);
            this.panelCut.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "Azimuth";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Inclination";
            // 
            // labelCutPosition
            // 
            this.labelCutPosition.AutoSize = true;
            this.labelCutPosition.Location = new System.Drawing.Point(12, 29);
            this.labelCutPosition.Name = "labelCutPosition";
            this.labelCutPosition.Size = new System.Drawing.Size(42, 12);
            this.labelCutPosition.TabIndex = 2;
            this.labelCutPosition.Text = "Position";
            // 
            // checkCutFaces
            // 
            this.checkCutFaces.AutoSize = true;
            this.checkCutFaces.Location = new System.Drawing.Point(12, 6);
            this.checkCutFaces.Name = "checkCutFaces";
            this.checkCutFaces.Size = new System.Drawing.Size(78, 16);
            this.checkCutFaces.TabIndex = 0;
            this.checkCutFaces.TabStop = false;
            this.checkCutFaces.Text = "Cut Objects";
            this.checkCutFaces.UseVisualStyleBackColor = true;
            this.checkCutFaces.CheckedChanged += new System.EventHandler(this.checkCutFaces_CheckedChanged);
            // 
            // panelAnalysis
            // 
            this.panelAnalysis.Controls.Add(this.groupBoxObjectAnalysis);
            this.panelAnalysis.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelAnalysis.Location = new System.Drawing.Point(0, 217);
            this.panelAnalysis.Name = "panelAnalysis";
            this.panelAnalysis.Size = new System.Drawing.Size(360, 204);
            this.panelAnalysis.TabIndex = 23;
            // 
            // groupBoxObjectAnalysis
            // 
            this.groupBoxObjectAnalysis.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxObjectAnalysis.BackColor = System.Drawing.Color.White;
            this.groupBoxObjectAnalysis.Controls.Add(this.tableLayoutPanel1);
            this.groupBoxObjectAnalysis.Location = new System.Drawing.Point(3, 6);
            this.groupBoxObjectAnalysis.Name = "groupBoxObjectAnalysis";
            this.groupBoxObjectAnalysis.Size = new System.Drawing.Size(354, 196);
            this.groupBoxObjectAnalysis.TabIndex = 0;
            this.groupBoxObjectAnalysis.TabStop = false;
            this.groupBoxObjectAnalysis.Text = "Object Analysis";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.textModied, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelModified, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textVertices, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.labelVertices, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.textEdges, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.labelEdges, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.textFaces, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.labelFaces, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.textLoopEdges, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelLoopEdges, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.textShells, 0, 9);
            this.tableLayoutPanel1.Controls.Add(this.labelShells, 1, 9);
            this.tableLayoutPanel1.Controls.Add(this.textIntersectingTriangles, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelIntersectingTriangles, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.textManifold, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelManifold, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.textHighlyConnected, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.labelHighConnected, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.textNormals, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.labelNormals, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 18);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 17F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(348, 175);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // textModied
            // 
            this.textModied.AutoSize = true;
            this.textModied.Location = new System.Drawing.Point(3, 0);
            this.textModied.Name = "textModied";
            this.textModied.Size = new System.Drawing.Size(51, 12);
            this.textModied.TabIndex = 18;
            this.textModied.Text = "Modified:";
            this.textModied.Click += new System.EventHandler(this.labelModified_Click);
            // 
            // labelModified
            // 
            this.labelModified.AutoSize = true;
            this.labelModified.Location = new System.Drawing.Point(264, 0);
            this.labelModified.Name = "labelModified";
            this.labelModified.Size = new System.Drawing.Size(33, 12);
            this.labelModified.TabIndex = 19;
            this.labelModified.Text = "label5";
            this.labelModified.Click += new System.EventHandler(this.labelModified_Click);
            // 
            // textVertices
            // 
            this.textVertices.AutoSize = true;
            this.textVertices.Location = new System.Drawing.Point(3, 102);
            this.textVertices.Name = "textVertices";
            this.textVertices.Size = new System.Drawing.Size(45, 12);
            this.textVertices.TabIndex = 0;
            this.textVertices.Text = "Vertices:";
            // 
            // labelVertices
            // 
            this.labelVertices.AutoSize = true;
            this.labelVertices.Location = new System.Drawing.Point(264, 102);
            this.labelVertices.Name = "labelVertices";
            this.labelVertices.Size = new System.Drawing.Size(33, 12);
            this.labelVertices.TabIndex = 1;
            this.labelVertices.Text = "label2";
            // 
            // textEdges
            // 
            this.textEdges.AutoSize = true;
            this.textEdges.Location = new System.Drawing.Point(3, 119);
            this.textEdges.Name = "textEdges";
            this.textEdges.Size = new System.Drawing.Size(36, 12);
            this.textEdges.TabIndex = 2;
            this.textEdges.Text = "Edges:";
            // 
            // labelEdges
            // 
            this.labelEdges.AutoSize = true;
            this.labelEdges.Location = new System.Drawing.Point(264, 119);
            this.labelEdges.Name = "labelEdges";
            this.labelEdges.Size = new System.Drawing.Size(33, 12);
            this.labelEdges.TabIndex = 3;
            this.labelEdges.Text = "label4";
            // 
            // textFaces
            // 
            this.textFaces.AutoSize = true;
            this.textFaces.Location = new System.Drawing.Point(3, 136);
            this.textFaces.Name = "textFaces";
            this.textFaces.Size = new System.Drawing.Size(33, 12);
            this.textFaces.TabIndex = 4;
            this.textFaces.Text = "Faces:";
            // 
            // labelFaces
            // 
            this.labelFaces.Location = new System.Drawing.Point(264, 136);
            this.labelFaces.Name = "labelFaces";
            this.labelFaces.Size = new System.Drawing.Size(81, 17);
            this.labelFaces.TabIndex = 20;
            // 
            // textLoopEdges
            // 
            this.textLoopEdges.AutoSize = true;
            this.textLoopEdges.Location = new System.Drawing.Point(3, 68);
            this.textLoopEdges.Name = "textLoopEdges";
            this.textLoopEdges.Size = new System.Drawing.Size(62, 12);
            this.textLoopEdges.TabIndex = 6;
            this.textLoopEdges.Text = "Loop edges:";
            // 
            // labelLoopEdges
            // 
            this.labelLoopEdges.AutoSize = true;
            this.labelLoopEdges.Location = new System.Drawing.Point(264, 68);
            this.labelLoopEdges.Name = "labelLoopEdges";
            this.labelLoopEdges.Size = new System.Drawing.Size(33, 12);
            this.labelLoopEdges.TabIndex = 7;
            this.labelLoopEdges.Text = "label8";
            // 
            // textShells
            // 
            this.textShells.AutoSize = true;
            this.textShells.Location = new System.Drawing.Point(3, 153);
            this.textShells.Name = "textShells";
            this.textShells.Size = new System.Drawing.Size(35, 12);
            this.textShells.TabIndex = 8;
            this.textShells.Text = "Shells:";
            // 
            // labelShells
            // 
            this.labelShells.AutoSize = true;
            this.labelShells.Location = new System.Drawing.Point(264, 153);
            this.labelShells.Name = "labelShells";
            this.labelShells.Size = new System.Drawing.Size(39, 12);
            this.labelShells.TabIndex = 9;
            this.labelShells.Text = "label10";
            // 
            // textIntersectingTriangles
            // 
            this.textIntersectingTriangles.AutoSize = true;
            this.textIntersectingTriangles.Location = new System.Drawing.Point(3, 34);
            this.textIntersectingTriangles.Name = "textIntersectingTriangles";
            this.textIntersectingTriangles.Size = new System.Drawing.Size(104, 12);
            this.textIntersectingTriangles.TabIndex = 10;
            this.textIntersectingTriangles.Text = "Intersecting triangles:";
            // 
            // labelIntersectingTriangles
            // 
            this.labelIntersectingTriangles.AutoSize = true;
            this.labelIntersectingTriangles.Location = new System.Drawing.Point(264, 34);
            this.labelIntersectingTriangles.Name = "labelIntersectingTriangles";
            this.labelIntersectingTriangles.Size = new System.Drawing.Size(11, 12);
            this.labelIntersectingTriangles.TabIndex = 11;
            this.labelIntersectingTriangles.Text = "0";
            // 
            // textManifold
            // 
            this.textManifold.AutoSize = true;
            this.textManifold.Location = new System.Drawing.Point(3, 17);
            this.textManifold.Name = "textManifold";
            this.textManifold.Size = new System.Drawing.Size(48, 12);
            this.textManifold.TabIndex = 12;
            this.textManifold.Text = "Manifold";
            // 
            // labelManifold
            // 
            this.labelManifold.AutoSize = true;
            this.labelManifold.Location = new System.Drawing.Point(264, 17);
            this.labelManifold.Name = "labelManifold";
            this.labelManifold.Size = new System.Drawing.Size(33, 12);
            this.labelManifold.TabIndex = 13;
            this.labelManifold.Text = "label6";
            // 
            // textHighlyConnected
            // 
            this.textHighlyConnected.AutoSize = true;
            this.textHighlyConnected.Location = new System.Drawing.Point(3, 85);
            this.textHighlyConnected.Name = "textHighlyConnected";
            this.textHighlyConnected.Size = new System.Drawing.Size(119, 12);
            this.textHighlyConnected.TabIndex = 14;
            this.textHighlyConnected.Text = "Highly connected edges:";
            // 
            // labelHighConnected
            // 
            this.labelHighConnected.AutoSize = true;
            this.labelHighConnected.Location = new System.Drawing.Point(264, 85);
            this.labelHighConnected.Name = "labelHighConnected";
            this.labelHighConnected.Size = new System.Drawing.Size(11, 12);
            this.labelHighConnected.TabIndex = 15;
            this.labelHighConnected.Text = "0";
            // 
            // textNormals
            // 
            this.textNormals.AutoSize = true;
            this.textNormals.Location = new System.Drawing.Point(3, 51);
            this.textNormals.Name = "textNormals";
            this.textNormals.Size = new System.Drawing.Size(44, 12);
            this.textNormals.TabIndex = 16;
            this.textNormals.Text = "Normals";
            // 
            // labelNormals
            // 
            this.labelNormals.AutoSize = true;
            this.labelNormals.Location = new System.Drawing.Point(264, 51);
            this.labelNormals.Name = "labelNormals";
            this.labelNormals.Size = new System.Drawing.Size(43, 12);
            this.labelNormals.TabIndex = 17;
            this.labelNormals.Text = "oriented";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textTransX);
            this.panel1.Controls.Add(this.buttonLockAspect);
            this.panel1.Controls.Add(this.textTransY);
            this.panel1.Controls.Add(this.textScaleX);
            this.panel1.Controls.Add(this.textScaleY);
            this.panel1.Controls.Add(this.textRotX);
            this.panel1.Controls.Add(this.textRotY);
            this.panel1.Controls.Add(this.textTransZ);
            this.panel1.Controls.Add(this.textScaleZ);
            this.panel1.Controls.Add(this.textRotZ);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 137);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(360, 80);
            this.panel1.TabIndex = 22;
            // 
            // textTransX
            // 
            this.textTransX.Location = new System.Drawing.Point(102, 5);
            this.textTransX.Name = "textTransX";
            this.textTransX.Size = new System.Drawing.Size(49, 22);
            this.textTransX.TabIndex = 2;
            this.textTransX.TabStop = false;
            this.textTransX.TextChanged += new System.EventHandler(this.textTransX_TextChanged);
            this.textTransX.Validating += new System.ComponentModel.CancelEventHandler(this.float_Validating);
            // 
            // buttonLockAspect
            // 
            this.buttonLockAspect.FlatAppearance.BorderSize = 0;
            this.buttonLockAspect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLockAspect.ImageIndex = 0;
            this.buttonLockAspect.ImageList = this.imageList16;
            this.buttonLockAspect.Location = new System.Drawing.Point(295, 27);
            this.buttonLockAspect.Name = "buttonLockAspect";
            this.buttonLockAspect.Size = new System.Drawing.Size(43, 21);
            this.buttonLockAspect.TabIndex = 20;
            this.buttonLockAspect.TabStop = false;
            this.buttonLockAspect.UseVisualStyleBackColor = true;
            this.buttonLockAspect.Click += new System.EventHandler(this.buttonLockAspect_Click);
            // 
            // imageList16
            // 
            this.imageList16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList16.ImageStream")));
            this.imageList16.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList16.Images.SetKeyName(0, "unlock16.png");
            this.imageList16.Images.SetKeyName(1, "lock16.png");
            this.imageList16.Images.SetKeyName(2, "ok16.png");
            this.imageList16.Images.SetKeyName(3, "bad16.png");
            this.imageList16.Images.SetKeyName(4, "trash16.png");
            // 
            // textTransY
            // 
            this.textTransY.Location = new System.Drawing.Point(171, 5);
            this.textTransY.Name = "textTransY";
            this.textTransY.Size = new System.Drawing.Size(49, 22);
            this.textTransY.TabIndex = 3;
            this.textTransY.TabStop = false;
            this.textTransY.TextChanged += new System.EventHandler(this.textTransY_TextChanged);
            this.textTransY.Validating += new System.ComponentModel.CancelEventHandler(this.float_Validating);
            // 
            // textScaleX
            // 
            this.textScaleX.Location = new System.Drawing.Point(102, 29);
            this.textScaleX.Name = "textScaleX";
            this.textScaleX.Size = new System.Drawing.Size(49, 22);
            this.textScaleX.TabIndex = 5;
            this.textScaleX.TabStop = false;
            this.textScaleX.TextChanged += new System.EventHandler(this.textScaleX_TextChanged);
            this.textScaleX.Validating += new System.ComponentModel.CancelEventHandler(this.float_Validating);
            // 
            // textScaleY
            // 
            this.textScaleY.Location = new System.Drawing.Point(171, 29);
            this.textScaleY.Name = "textScaleY";
            this.textScaleY.Size = new System.Drawing.Size(49, 22);
            this.textScaleY.TabIndex = 6;
            this.textScaleY.TabStop = false;
            this.textScaleY.TextChanged += new System.EventHandler(this.textScaleY_TextChanged);
            this.textScaleY.Validating += new System.ComponentModel.CancelEventHandler(this.float_Validating);
            // 
            // textRotX
            // 
            this.textRotX.Location = new System.Drawing.Point(102, 53);
            this.textRotX.Name = "textRotX";
            this.textRotX.Size = new System.Drawing.Size(49, 22);
            this.textRotX.TabIndex = 9;
            this.textRotX.TabStop = false;
            this.textRotX.TextChanged += new System.EventHandler(this.textRotX_TextChanged);
            this.textRotX.Validating += new System.ComponentModel.CancelEventHandler(this.float_Validating);
            // 
            // textRotY
            // 
            this.textRotY.Location = new System.Drawing.Point(171, 53);
            this.textRotY.Name = "textRotY";
            this.textRotY.Size = new System.Drawing.Size(49, 22);
            this.textRotY.TabIndex = 10;
            this.textRotY.TabStop = false;
            this.textRotY.TextChanged += new System.EventHandler(this.textRotY_TextChanged);
            this.textRotY.Validating += new System.ComponentModel.CancelEventHandler(this.float_Validating);
            // 
            // textTransZ
            // 
            this.textTransZ.Location = new System.Drawing.Point(240, 5);
            this.textTransZ.Name = "textTransZ";
            this.textTransZ.Size = new System.Drawing.Size(49, 22);
            this.textTransZ.TabIndex = 4;
            this.textTransZ.TabStop = false;
            this.textTransZ.TextChanged += new System.EventHandler(this.textTransZ_TextChanged);
            this.textTransZ.Validating += new System.ComponentModel.CancelEventHandler(this.float_Validating);
            // 
            // textScaleZ
            // 
            this.textScaleZ.Location = new System.Drawing.Point(240, 29);
            this.textScaleZ.Name = "textScaleZ";
            this.textScaleZ.Size = new System.Drawing.Size(49, 22);
            this.textScaleZ.TabIndex = 7;
            this.textScaleZ.TabStop = false;
            this.textScaleZ.Text = "1";
            this.textScaleZ.TextChanged += new System.EventHandler(this.textScaleZ_TextChanged);
            this.textScaleZ.Validating += new System.ComponentModel.CancelEventHandler(this.float_Validating);
            // 
            // textRotZ
            // 
            this.textRotZ.Location = new System.Drawing.Point(240, 53);
            this.textRotZ.Name = "textRotZ";
            this.textRotZ.Size = new System.Drawing.Size(49, 22);
            this.textRotZ.TabIndex = 11;
            this.textRotZ.TabStop = false;
            this.textRotZ.TextChanged += new System.EventHandler(this.textRotZ_TextChanged);
            this.textRotZ.Validating += new System.ComponentModel.CancelEventHandler(this.float_Validating);
            // 
            // listObjects
            // 
            this.listObjects.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnName,
            this.columnMesh,
            this.columnCollision,
            this.columnDelete});
            this.listObjects.Dock = System.Windows.Forms.DockStyle.Top;
            this.listObjects.FullRowSelect = true;
            this.listObjects.GridLines = true;
            this.listObjects.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listObjects.HideSelection = false;
            this.listObjects.Location = new System.Drawing.Point(0, 0);
            this.listObjects.Name = "listObjects";
            this.listObjects.OwnerDraw = true;
            this.listObjects.ShowGroups = false;
            this.listObjects.Size = new System.Drawing.Size(360, 137);
            this.listObjects.SmallImageList = this.imageList16;
            this.listObjects.TabIndex = 21;
            this.listObjects.TabStop = false;
            this.listObjects.UseCompatibleStateImageBehavior = false;
            this.listObjects.View = System.Windows.Forms.View.Details;
            this.listObjects.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.listObjects_DrawColumnHeader);
            this.listObjects.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.listObjects_DrawSubItem);
            this.listObjects.SelectedIndexChanged += new System.EventHandler(this.listSTLObjects_SelectedIndexChanged);
            this.listObjects.ClientSizeChanged += new System.EventHandler(this.listObjects_ClientSizeChanged);
            this.listObjects.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listSTLObjects_KeyDown);
            // 
            // columnName
            // 
            this.columnName.Text = "Name";
            // 
            // columnMesh
            // 
            this.columnMesh.Text = "Mesh";
            this.columnMesh.Width = 50;
            // 
            // columnCollision
            // 
            this.columnCollision.Text = "Collision";
            this.columnCollision.Width = 50;
            // 
            // columnDelete
            // 
            this.columnDelete.Text = "";
            this.columnDelete.Width = 26;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // openFileSTL
            // 
            this.openFileSTL.DefaultExt = "stl";
            this.openFileSTL.Filter = "STL-Files|*.stl;*.STL";
            this.openFileSTL.Multiselect = true;
            this.openFileSTL.Title = "Add STL file";
            // 
            // saveSTL
            // 
            this.saveSTL.DefaultExt = "stl";
            this.saveSTL.Filter = "STL-Files|*.stl;*.STL|3ws-Files|*.3ws;*.3WS";
            this.saveSTL.Title = "Save composition";
            // 
            // cutAzimuthSlider
            // 
            this.cutAzimuthSlider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cutAzimuthSlider.BackColor = System.Drawing.Color.Transparent;
            this.cutAzimuthSlider.BarInnerColor = System.Drawing.Color.LightGray;
            this.cutAzimuthSlider.BarOuterColor = System.Drawing.Color.Gray;
            this.cutAzimuthSlider.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.cutAzimuthSlider.ElapsedInnerColor = System.Drawing.Color.LightGray;
            this.cutAzimuthSlider.ElapsedOuterColor = System.Drawing.Color.Gray;
            this.cutAzimuthSlider.LargeChange = ((uint)(5u));
            this.cutAzimuthSlider.Location = new System.Drawing.Point(83, 78);
            this.cutAzimuthSlider.Maximum = 3600;
            this.cutAzimuthSlider.Name = "cutAzimuthSlider";
            this.cutAzimuthSlider.Size = new System.Drawing.Size(271, 18);
            this.cutAzimuthSlider.SmallChange = ((uint)(1u));
            this.cutAzimuthSlider.TabIndex = 1;
            this.cutAzimuthSlider.Text = "colorSlider1";
            this.cutAzimuthSlider.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
            this.cutAzimuthSlider.ThumbSize = 10;
            this.cutAzimuthSlider.Value = 0;
            this.cutAzimuthSlider.ValueChanged += new System.EventHandler(this.cutPositionSlider_ValueChanged);
            // 
            // cutInclinationSlider
            // 
            this.cutInclinationSlider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cutInclinationSlider.BackColor = System.Drawing.Color.Transparent;
            this.cutInclinationSlider.BarInnerColor = System.Drawing.Color.LightGray;
            this.cutInclinationSlider.BarOuterColor = System.Drawing.Color.Gray;
            this.cutInclinationSlider.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.cutInclinationSlider.ElapsedInnerColor = System.Drawing.Color.LightGray;
            this.cutInclinationSlider.ElapsedOuterColor = System.Drawing.Color.Gray;
            this.cutInclinationSlider.LargeChange = ((uint)(5u));
            this.cutInclinationSlider.Location = new System.Drawing.Point(83, 53);
            this.cutInclinationSlider.Maximum = 1800;
            this.cutInclinationSlider.Name = "cutInclinationSlider";
            this.cutInclinationSlider.Size = new System.Drawing.Size(271, 18);
            this.cutInclinationSlider.SmallChange = ((uint)(1u));
            this.cutInclinationSlider.TabIndex = 1;
            this.cutInclinationSlider.Text = "colorSlider1";
            this.cutInclinationSlider.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
            this.cutInclinationSlider.ThumbSize = 10;
            this.cutInclinationSlider.Value = 0;
            this.cutInclinationSlider.ValueChanged += new System.EventHandler(this.cutPositionSlider_ValueChanged);
            // 
            // cutPositionSlider
            // 
            this.cutPositionSlider.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cutPositionSlider.BackColor = System.Drawing.Color.Transparent;
            this.cutPositionSlider.BarInnerColor = System.Drawing.Color.LightGray;
            this.cutPositionSlider.BarOuterColor = System.Drawing.Color.Gray;
            this.cutPositionSlider.BorderRoundRectSize = new System.Drawing.Size(8, 8);
            this.cutPositionSlider.ElapsedInnerColor = System.Drawing.Color.LightGray;
            this.cutPositionSlider.ElapsedOuterColor = System.Drawing.Color.Gray;
            this.cutPositionSlider.LargeChange = ((uint)(5u));
            this.cutPositionSlider.Location = new System.Drawing.Point(83, 27);
            this.cutPositionSlider.Maximum = 1000;
            this.cutPositionSlider.Name = "cutPositionSlider";
            this.cutPositionSlider.Size = new System.Drawing.Size(271, 18);
            this.cutPositionSlider.SmallChange = ((uint)(1u));
            this.cutPositionSlider.TabIndex = 1;
            this.cutPositionSlider.Text = "colorSlider1";
            this.cutPositionSlider.ThumbRoundRectSize = new System.Drawing.Size(8, 8);
            this.cutPositionSlider.ThumbSize = 10;
            this.cutPositionSlider.Value = 500;
            this.cutPositionSlider.ValueChanged += new System.EventHandler(this.cutPositionSlider_ValueChanged);
            // 
            // STLComposer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControls);
            this.Name = "STLComposer";
            this.Size = new System.Drawing.Size(360, 576);
            this.panelControls.ResumeLayout(false);
            this.panelCut.ResumeLayout(false);
            this.panelCut.PerformLayout();
            this.panelAnalysis.ResumeLayout(false);
            this.groupBoxObjectAnalysis.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.TextBox textTransZ;
        public System.Windows.Forms.TextBox textTransY;
        public System.Windows.Forms.TextBox textTransX;
        public System.Windows.Forms.TextBox textRotZ;
        public System.Windows.Forms.TextBox textScaleZ;
        public System.Windows.Forms.TextBox textRotY;
        public System.Windows.Forms.TextBox textRotX;
        public System.Windows.Forms.TextBox textScaleY;
        public System.Windows.Forms.TextBox textScaleX;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.OpenFileDialog openFileSTL;
        public System.Windows.Forms.SaveFileDialog saveSTL;
        private System.Windows.Forms.ImageList imageList16;
        private System.Windows.Forms.Button buttonLockAspect;
        public System.Windows.Forms.ListView listObjects;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnMesh;
        private System.Windows.Forms.ColumnHeader columnCollision;
        private System.Windows.Forms.ColumnHeader columnDelete;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelAnalysis;
        private System.Windows.Forms.GroupBox groupBoxObjectAnalysis;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label textVertices;
        private System.Windows.Forms.Label labelVertices;
        private System.Windows.Forms.Label textEdges;
        private System.Windows.Forms.Label labelEdges;
        private System.Windows.Forms.Label textFaces;
        private System.Windows.Forms.Label labelFaces;
        private System.Windows.Forms.Label textLoopEdges;
        private System.Windows.Forms.Label labelLoopEdges;
        private System.Windows.Forms.Label textShells;
        private System.Windows.Forms.Label labelShells;
        private System.Windows.Forms.Label textIntersectingTriangles;
        private System.Windows.Forms.Label labelIntersectingTriangles;
        private System.Windows.Forms.Label textManifold;
        private System.Windows.Forms.Label labelManifold;
        private System.Windows.Forms.Label textHighlyConnected;
        private System.Windows.Forms.Label labelHighConnected;
        private System.Windows.Forms.Label textNormals;
        private System.Windows.Forms.Label labelNormals;
        private System.Windows.Forms.Label textModied;
        private System.Windows.Forms.Label labelModified;
        private System.Windows.Forms.Panel panelCut;
        private System.Windows.Forms.Label labelCutPosition;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        public MB.Controls.ColorSlider cutPositionSlider;
        public System.Windows.Forms.CheckBox checkCutFaces;
        public MB.Controls.ColorSlider cutAzimuthSlider;
        public MB.Controls.ColorSlider cutInclinationSlider;
        public System.Windows.Forms.Panel panelControls;
    }
}
