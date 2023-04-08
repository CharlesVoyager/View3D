using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using View3D.model;
using View3D.view.utils;

namespace View3D.view
{
    public delegate void PrinterChanged(RegistryKey printerKey,bool printerChanged);

    public partial class FormPrinterSettings : Form
    {
        private double epsilon = 1e-4; // 0.0001
        public int PixelInX = 3024;//3544;
        public int PixelInY = 3024;//3544;
        public float PrintAreaWidth = 128;  // x-axis direction
        public float PrintAreaDepth = 128;  // y-axis direction
        public float PrintAreaHeight = 200; // z-axis direction
        public double SvgBmpShiftX = 0;
        public double SvgBmpShiftY = 0;
        public int ExtraBytesForNextPrintLine = 2;
		public int ExtraBytesForNextPrintLine_Grey = 2;
        public static FormPrinterSettings ps = null;
        public RegistryKey printerKey;
        public RegistryKey currentPrinterKey;
        public float XMin, XMax, YMin, YMax, BedLeft, BedFront;
        public float DumpAreaLeft;
        public float DumpAreaFront;
        public float DumpAreaWidth;
        public float DumpAreaDepth;
        public int printerType;
        int xhomeMode = 0, yhomeMode = 0, zhomemode = 0;

        public FormPrinterSettings()
        {
            ps = this;

            InitializeComponent();
            RegMemory.RestoreWindowPos("printerSettingsWindow", this);
            comboConnector.DataSource = bindingConnectors.DataSource;
            comboConnector.DisplayMember = "Name";
            comboConnector.ValueMember = "Id";
            load("");
            UpdateDimensions();

            Main.main.languageChanged += translate;
            translate();

            SvgBmpShiftX = ((double)(PixelInX) / (double)(PrintAreaWidth));
            SvgBmpShiftY = ((double)(PixelInY) / (double)(PrintAreaDepth));
        }

        public void translate()
        {;
            tabPageConnection.Text = Trans.T("TAB_CONNECTION");
            this.Text = Trans.T("W_PRINTER_SETTINGS");
        }

        public void load(string printername)
        {
            if (printername.Length == 0) return;
            RegistryKey p = printerKey.CreateSubKey(printername);
            currentPrinterKey = p;
            string id = (string)p.GetValue("connector","UsbConnector");
            int idx = 0;
            comboConnector.SelectedIndex = idx;
            comboConnector_SelectedIndexChanged(null, null);
            bool hasDump = 1==(int)p.GetValue("hasDumpArea", 0);
        }

        public void UpdateDimensions()
        {
            printerType = 0;
        }

        public bool PointInside(float x, float y, float z)
        {
            if (z < -0.1 || z > PrintAreaHeight) //0.0005
                return false;

            if (printerType < 2) 
            {
                if (x < BedLeft - epsilon || x > BedLeft + PrintAreaWidth + epsilon) return false;
                if (y < BedFront - epsilon || y > BedFront + PrintAreaDepth + epsilon) return false;
            }

            return true;
        }

        private void float_Validating(object sender, CancelEventArgs e)
        {
            TextBox box = (TextBox)sender;
            try
            {
                float.Parse(box.Text, NumberStyles.Float, GCode.format);
                errorProvider.SetError(box, "");
            }
            catch
            {
                errorProvider.SetError(box, Trans.T("L_NOT_A_NUMBER"));
            }
        }

        private void int_Validating(object sender, CancelEventArgs e)
        {
            TextBox box = (TextBox)sender;
            try
            {
                int.Parse(box.Text);
                errorProvider.SetError(box, "");
            }
            catch
            {
                errorProvider.SetError(box, Trans.T("L_NOT_AN_INTEGER"));
            }
        }

        private void FormPrinterSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            RegMemory.StoreWindowPos("printerSettingsWindow", this, false, false);
        }

        public float XHomePos
        {
            get
            {
                switch (xhomeMode)
                {
                    case 0: return XMin;
                    case 1: return XMax;
                    case 2: return 0;
                }
                return 0;
            }
        }

        public float YHomePos
        {
            get
            {
                switch (yhomeMode)
                {
                    case 0: return YMin;
                    case 1: return YMax;
                    case 2: return 0;
                }
                return 0;
            }
        }

        public float ZHomePos
        {
            get
            {
                switch (zhomemode)
                {
                    case 0: return 0;
                    case 1: return PrintAreaHeight;
                    case 2: return 0;
                }
                return 0;
            }
        }

        private void comboConnector_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
