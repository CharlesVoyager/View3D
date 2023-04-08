namespace View3D.view
{
    partial class ObjectInformation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObjectInformation));
            this.infoVolume = new System.Windows.Forms.Label();
            this.infoSizeZ = new System.Windows.Forms.Label();
            this.infoSizeY = new System.Windows.Forms.Label();
            this.infoSizeX = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelFileFormat = new System.Windows.Forms.Label();
            this.label_format = new System.Windows.Forms.Label();
            this.label_collision_flag = new System.Windows.Forms.Label();
            this.label_collision = new System.Windows.Forms.Label();
            this.label_volume = new System.Windows.Forms.Label();
            this.label_filename = new System.Windows.Forms.Label();
            this.lb_ObjInfoFileName = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label_positionZ = new System.Windows.Forms.Label();
            this.label_positionY = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label_size = new System.Windows.Forms.Label();
            this.label_positionX = new System.Windows.Forms.Label();
            this.label_position = new System.Windows.Forms.Label();
            this.label_dimension = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // infoVolume
            // 
            this.infoVolume.AutoSize = true;
            this.infoVolume.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.infoVolume.Location = new System.Drawing.Point(279, 56);
            this.infoVolume.Name = "infoVolume";
            this.infoVolume.Size = new System.Drawing.Size(42, 16);
            this.infoVolume.TabIndex = 1;
            this.infoVolume.Text = "label2";
            this.infoVolume.Visible = false;
            // 
            // infoSizeZ
            // 
            this.infoSizeZ.AutoSize = true;
            this.infoSizeZ.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.infoSizeZ.Location = new System.Drawing.Point(375, 85);
            this.infoSizeZ.Name = "infoSizeZ";
            this.infoSizeZ.Size = new System.Drawing.Size(77, 16);
            this.infoSizeZ.TabIndex = 0;
            this.infoSizeZ.Text = "Dimensions:";
            this.infoSizeZ.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // infoSizeY
            // 
            this.infoSizeY.AutoSize = true;
            this.infoSizeY.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.infoSizeY.Location = new System.Drawing.Point(259, 85);
            this.infoSizeY.Name = "infoSizeY";
            this.infoSizeY.Size = new System.Drawing.Size(77, 16);
            this.infoSizeY.TabIndex = 0;
            this.infoSizeY.Text = "Dimensions:";
            this.infoSizeY.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // infoSizeX
            // 
            this.infoSizeX.AutoSize = true;
            this.infoSizeX.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.infoSizeX.Location = new System.Drawing.Point(142, 85);
            this.infoSizeX.Name = "infoSizeX";
            this.infoSizeX.Size = new System.Drawing.Size(77, 16);
            this.infoSizeX.TabIndex = 0;
            this.infoSizeX.Text = "Dimensions:";
            this.infoSizeX.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // buttonClose
            // 
            this.buttonClose.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClose.Location = new System.Drawing.Point(376, 245);
            this.buttonClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(131, 26);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelFileFormat);
            this.groupBox2.Controls.Add(this.label_format);
            this.groupBox2.Controls.Add(this.label_collision_flag);
            this.groupBox2.Controls.Add(this.label_collision);
            this.groupBox2.Controls.Add(this.infoVolume);
            this.groupBox2.Controls.Add(this.label_volume);
            this.groupBox2.Controls.Add(this.label_filename);
            this.groupBox2.Controls.Add(this.lb_ObjInfoFileName);
            this.groupBox2.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(14, 13);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(493, 90);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // labelFileFormat
            // 
            this.labelFileFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFileFormat.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelFileFormat.Location = new System.Drawing.Point(176, 56);
            this.labelFileFormat.Name = "labelFileFormat";
            this.labelFileFormat.Size = new System.Drawing.Size(306, 16);
            this.labelFileFormat.TabIndex = 6;
            this.labelFileFormat.Text = "label2";
            this.labelFileFormat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_format
            // 
            this.label_format.AutoSize = true;
            this.label_format.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_format.Location = new System.Drawing.Point(19, 56);
            this.label_format.Name = "label_format";
            this.label_format.Size = new System.Drawing.Size(73, 16);
            this.label_format.TabIndex = 7;
            this.label_format.Text = "File Format:";
            // 
            // label_collision_flag
            // 
            this.label_collision_flag.AutoSize = true;
            this.label_collision_flag.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_collision_flag.Location = new System.Drawing.Point(430, 56);
            this.label_collision_flag.Name = "label_collision_flag";
            this.label_collision_flag.Size = new System.Drawing.Size(42, 16);
            this.label_collision_flag.TabIndex = 5;
            this.label_collision_flag.Text = "label1";
            this.label_collision_flag.Visible = false;
            // 
            // label_collision
            // 
            this.label_collision.AutoSize = true;
            this.label_collision.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_collision.Location = new System.Drawing.Point(357, 56);
            this.label_collision.Name = "label_collision";
            this.label_collision.Size = new System.Drawing.Size(59, 16);
            this.label_collision.TabIndex = 4;
            this.label_collision.Text = "Collision:";
            this.label_collision.Visible = false;
            // 
            // label_volume
            // 
            this.label_volume.AutoSize = true;
            this.label_volume.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_volume.Location = new System.Drawing.Point(192, 56);
            this.label_volume.Name = "label_volume";
            this.label_volume.Size = new System.Drawing.Size(54, 16);
            this.label_volume.TabIndex = 2;
            this.label_volume.Text = "Volume:";
            this.label_volume.Visible = false;
            // 
            // label_filename
            // 
            this.label_filename.AutoSize = true;
            this.label_filename.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_filename.Location = new System.Drawing.Point(19, 31);
            this.label_filename.Name = "label_filename";
            this.label_filename.Size = new System.Drawing.Size(63, 16);
            this.label_filename.TabIndex = 1;
            this.label_filename.Text = "Filename:";
            // 
            // lb_ObjInfoFileName
            // 
            this.lb_ObjInfoFileName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_ObjInfoFileName.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lb_ObjInfoFileName.Location = new System.Drawing.Point(176, 31);
            this.lb_ObjInfoFileName.Name = "lb_ObjInfoFileName";
            this.lb_ObjInfoFileName.Size = new System.Drawing.Size(306, 16);
            this.lb_ObjInfoFileName.TabIndex = 0;
            this.lb_ObjInfoFileName.Text = "label1";
            this.lb_ObjInfoFileName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label_positionZ);
            this.groupBox3.Controls.Add(this.label_positionY);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label_size);
            this.groupBox3.Controls.Add(this.label_positionX);
            this.groupBox3.Controls.Add(this.label_position);
            this.groupBox3.Controls.Add(this.label_dimension);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.infoSizeZ);
            this.groupBox3.Controls.Add(this.infoSizeX);
            this.groupBox3.Controls.Add(this.infoSizeY);
            this.groupBox3.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(14, 111);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(493, 115);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            // 
            // label_positionZ
            // 
            this.label_positionZ.AutoSize = true;
            this.label_positionZ.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_positionZ.Location = new System.Drawing.Point(375, 56);
            this.label_positionZ.Name = "label_positionZ";
            this.label_positionZ.Size = new System.Drawing.Size(42, 16);
            this.label_positionZ.TabIndex = 9;
            this.label_positionZ.Text = "label1";
            // 
            // label_positionY
            // 
            this.label_positionY.AutoSize = true;
            this.label_positionY.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_positionY.Location = new System.Drawing.Point(259, 56);
            this.label_positionY.Name = "label_positionY";
            this.label_positionY.Size = new System.Drawing.Size(42, 16);
            this.label_positionY.TabIndex = 8;
            this.label_positionY.Text = "label1";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label14.Location = new System.Drawing.Point(375, 29);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(15, 16);
            this.label14.TabIndex = 7;
            this.label14.Text = "Z";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label13.Location = new System.Drawing.Point(259, 29);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(16, 16);
            this.label13.TabIndex = 6;
            this.label13.Text = "Y";
            // 
            // label_size
            // 
            this.label_size.AutoSize = true;
            this.label_size.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_size.Location = new System.Drawing.Point(19, 85);
            this.label_size.Name = "label_size";
            this.label_size.Size = new System.Drawing.Size(35, 16);
            this.label_size.TabIndex = 4;
            this.label_size.Text = "Size:";
            // 
            // label_positionX
            // 
            this.label_positionX.AutoSize = true;
            this.label_positionX.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_positionX.Location = new System.Drawing.Point(142, 56);
            this.label_positionX.Name = "label_positionX";
            this.label_positionX.Size = new System.Drawing.Size(42, 16);
            this.label_positionX.TabIndex = 3;
            this.label_positionX.Text = "label1";
            // 
            // label_position
            // 
            this.label_position.AutoSize = true;
            this.label_position.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_position.Location = new System.Drawing.Point(19, 56);
            this.label_position.Name = "label_position";
            this.label_position.Size = new System.Drawing.Size(55, 16);
            this.label_position.TabIndex = 2;
            this.label_position.Text = "Position:";
            // 
            // label_dimension
            // 
            this.label_dimension.AutoSize = true;
            this.label_dimension.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_dimension.Location = new System.Drawing.Point(19, 28);
            this.label_dimension.Name = "label_dimension";
            this.label_dimension.Size = new System.Drawing.Size(71, 16);
            this.label_dimension.TabIndex = 1;
            this.label_dimension.Text = "Dimension:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial Unicode MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label12.Location = new System.Drawing.Point(142, 29);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(16, 16);
            this.label12.TabIndex = 0;
            this.label12.Text = "X";
            // 
            // ObjectInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 284);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonClose);
            this.Font = new System.Drawing.Font("Arial", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ObjectInformation";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Object Informations";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ObjectInformation_FormClosed);
            this.Load += new System.EventHandler(this.ObjectInformation_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label infoVolume;
        private System.Windows.Forms.Label infoSizeZ;
        private System.Windows.Forms.Label infoSizeY;
        private System.Windows.Forms.Label infoSizeX;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lb_ObjInfoFileName;
        private System.Windows.Forms.Label label_collision_flag;
        private System.Windows.Forms.Label label_collision;
        private System.Windows.Forms.Label label_volume;
        private System.Windows.Forms.Label label_filename;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label_positionZ;
        private System.Windows.Forms.Label label_positionY;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label_size;
        private System.Windows.Forms.Label label_positionX;
        private System.Windows.Forms.Label label_position;
        private System.Windows.Forms.Label label_dimension;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label labelFileFormat;
        private System.Windows.Forms.Label label_format;
    }
}