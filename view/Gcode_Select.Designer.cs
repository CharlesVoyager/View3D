namespace XYZ.view
{
    partial class Gcode_Select
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
            this.Usb_Select = new System.Windows.Forms.Button();
            this.Select_connect = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // Usb_Select
            // 
            this.Usb_Select.Location = new System.Drawing.Point(211, 35);
            this.Usb_Select.Name = "Usb_Select";
            this.Usb_Select.Size = new System.Drawing.Size(75, 23);
            this.Usb_Select.TabIndex = 1;
            this.Usb_Select.Text = "OK";
            this.Usb_Select.UseVisualStyleBackColor = true;
            this.Usb_Select.Click += new System.EventHandler(this.Usb_Select_Click);
            // 
            // Select_connect
            // 
            this.Select_connect.FormattingEnabled = true;
            this.Select_connect.Items.AddRange(new object[] {
            "Wireless",
            "USB"});
            this.Select_connect.Location = new System.Drawing.Point(30, 35);
            this.Select_connect.Name = "Select_connect";
            this.Select_connect.Size = new System.Drawing.Size(121, 20);
            this.Select_connect.TabIndex = 2;
            // 
            // Gcode_Select
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 232);
            this.Controls.Add(this.Select_connect);
            this.Controls.Add(this.Usb_Select);
            this.Name = "Gcode_Select";
            this.Text = "3W_Select";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Usb_Select;
        private System.Windows.Forms.ComboBox Select_connect;
    }
}