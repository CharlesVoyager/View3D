using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XYZ;
using XYZ.model;
using XYZ.view.utils;
using XYZ.view;
namespace XYZ.view
{
    public partial class Gcode_Select : Form
    {
        public Gcode_Select()
        {
            InitializeComponent();
            Select_connect.SelectedIndex = 0;
        }
        
        private void Wireless_Select_Click(object sender, EventArgs e)
        {
        }

        private void Usb_Select_Click(object sender, EventArgs e)
        {
            if (Select_connect.SelectedIndex == 0)
            {
           
            }
            else if (Select_connect.SelectedIndex == 1)
            {
             
            }
        }
    }
}
