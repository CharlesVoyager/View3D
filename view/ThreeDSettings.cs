﻿/*
   Copyright 2011 repetier repetierdev@gmail.com

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/
using OpenTK;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using View3D.model;
using View3D.view.utils;

namespace View3D.view
{
    public partial class ThreeDSettings : Form, INotifyPropertyChanged
    {  
        RegistryKey threedKey = null;
        public bool useVBOs = false;
        public int drawMethod = 0;          // 0 = elements, 1 = drawElements, 2 = VBO
        public float openGLVersion = 1.0f;  // Version for feature detection
        private bool _showEdges = false;
        private bool _showFaces = true;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        public ThreeDSettings()
        {
            InitializeComponent();
            tdSettings.DataSource = this;
            RegMemory.RestoreWindowPos("threeDSettingsWindow", this);
            if (Main.IsMono)
                buttonOK.Location = new Point(buttonOK.Location.X,buttonOK.Location.Y-10);

            comboDrawMethod.SelectedIndex = 0;
            RegistryToForm();
            translate();
            Main.main.languageChanged += translate;
        }

        public void translate()
        {
            Text = Trans.T("W_3D_VISUALIZATION_SETTINGS");
            groupColors.Text = Trans.T("L_COLORS");
            groupColors2.Text = Trans.T("L_COLORS");
            groupEditor.Text = Trans.T("L_EDITOR");
            groupPrintbed.Text = Trans.T("L_PRINTBED");
            groupVisualization.Text = Trans.T("L_VISUALIZATION");
            labelAmbientColor.Text = Trans.T("L_AMBIENT_COLOR");
            labelBackgroundTop.Text = Trans.T("L_BACKGROUND_TOP");
            labelBackgroundBottom.Text = Trans.T("L_BACKGROUND_BOTTOM");
            labelPrinterFrame.Text = Trans.T("L_PRINTER_FRAME");
            labelDiffuseColor.Text = Trans.T("L_DIFFUSE_COLOR");
            labelDrawMethod.Text = Trans.T("L_DRAW_METHOD");
            labelEdges.Text = Trans.T("L_EDGES");
            labelFaces.Text = Trans.T("L_FACES");
            labelLight1.Text = Trans.T1("L_LIGHT_X", "1");
            labelLight2.Text = Trans.T1("L_LIGHT_X", "2");
            labelLight3.Text = Trans.T1("L_LIGHT_X", "3");
            labelLight4.Text = Trans.T1("L_LIGHT_X", "4");
            labelObjectsOutsidePrintbed.Text = Trans.T("L_OBJECTS_OUTSIDE_PRINTBED");
            labelPrinterBase.Text = Trans.T("L_PRINTER_BASE");
            labelSelectedFaces.Text = Trans.T("L_SELECTED_FACES");
            labelSelectedFilament.Text = Trans.T("L_SELECTED_FILAMENT");
            labelSpecularColor.Text = Trans.T("L_SPECULAR_COLOR");
            labelXDirection.Text = Trans.T("L_X_DIRECTION");
            labelYDirection.Text = Trans.T("L_Y_DIRECTION");
            labelZDirection.Text = Trans.T("L_Z_DIRECTION");
            enableLight1.Text = enableLight2.Text = enableLight3.Text = enableLight4.Text = Trans.T("L_ENABLE_LIGHT");
            showEdges.Text = Trans.T("L_SHOW_EDGES");
            showFaces.Text = Trans.T("L_SHOW_FACES");
            showPrintbed.Text = Trans.T("L_SHOW_PRINTBED");
            tabGeneral.Text = Trans.T("TAB_GENERAL");
            tabFilament.Text = Trans.T("TAB_FILAMENT");
            tabModel.Text = Trans.T("TAB_MODEL");
            tabLights.Text = Trans.T("TAB_LIGHTS");
            labelModelError.Text = Trans.T("L_MODEL_ERRORS");
            labelInsideFaces.Text = Trans.T("L_INNER_FACES");
            /*  Autodetect best
                VBOs (fastest)
                Arrays (medium)
                Immediate (slow)    */
            comboDrawMethod.Items[0] = Trans.T("L_DRAW_METHOD_AUTODETECT");
            comboDrawMethod.Items[1] = Trans.T("L_DRAW_METHOD_VBOS");
            comboDrawMethod.Items[2] = Trans.T("L_DRAW_METHOD_ARRAYS");
            comboDrawMethod.Items[3] = Trans.T("L_DRAW_METHOD_IMMEDIATE");
            buttonOK.Text = Trans.T("B_OK");
            labelSelectionBox.Text = Trans.T("L_SELECTION_BOX");
            labelTravelMoves.Text = Trans.T("L_TRAVEL_MOVES:");
            checkDisableTravelMoves.Text = Trans.T("L_DISABLE_TRAVEL_MOVES_VIS");
            checkCorrectNormals.Text = Trans.T("L_CORRECT_NORMALS");
        }

        public bool ShowEdges
        {
            get { return _showEdges; }
            set
            {
                if (value == _showEdges) return;
                _showEdges = value;
                threedKey.SetValue("showEdges", _showEdges ? 1 : 0);
                OnPropertyChanged(new PropertyChangedEventArgs("ShowEdges"));
                Main.main.Update3D();
            }
        }

        public bool ShowFaces
        {
            get { return _showFaces; }
            set
            {
                if (value == _showFaces) return;
                _showFaces = value;
                threedKey.SetValue("showFaces", _showFaces ? 1 : 0);
                OnPropertyChanged(new PropertyChangedEventArgs("ShowFaces"));
                Main.main.Update3D();
            }
        }

        public void FormToRegistry()
        {
            try
            {
                threedKey.SetValue("backgroundTopColor", backgroundTop.BackColor.ToArgb());
                threedKey.SetValue("backgroundBottomColor", backgroundBottom.BackColor.ToArgb());
                threedKey.SetValue("facesColor", faces.BackColor.ToArgb());
                threedKey.SetValue("edgesColor", edges.BackColor.ToArgb());
                threedKey.SetValue("selectedFacesColor", selectedFaces.BackColor.ToArgb());
                threedKey.SetValue("printerBaseColor", printerBase.BackColor.ToArgb());
                threedKey.SetValue("printerFrameColor", printerFrame.BackColor.ToArgb());
                threedKey.SetValue("filamenColor", filament.BackColor.ToArgb());
                threedKey.SetValue("travelColor", travelMoves.BackColor.ToArgb());
                threedKey.SetValue("selectedFilamentColor", selectedFilament.BackColor.ToArgb());
                threedKey.SetValue("outsidePrintbedColor", outsidePrintbed.BackColor.ToArgb());
                threedKey.SetValue("showEdges", _showEdges ? 1 : 0);
                threedKey.SetValue("showFaces", _showFaces ? 1 : 0);
                threedKey.SetValue("showPrintbed", showPrintbed.Checked ? 1 : 0);
                threedKey.SetValue("disableTravelVisualization", checkDisableTravelMoves.Checked ? 1 : 0);
                threedKey.SetValue("correctNormals", checkCorrectNormals.Checked ? 1 : 0);
                threedKey.SetValue("enableLight1", enableLight1.Checked ? 1 : 0);
                threedKey.SetValue("enableLight2", enableLight2.Checked ? 1 : 0);
                threedKey.SetValue("enableLight3", enableLight3.Checked ? 1 : 0);
                threedKey.SetValue("enableLight4", enableLight4.Checked ? 1 : 0);
                // threedKey.SetValue("useVBOs", useVBOs ? 1 : 0);
                threedKey.SetValue("drawMethod", comboDrawMethod.SelectedIndex);

                // Light settings
                threedKey.SetValue("ambient1Color", ambient1.BackColor.ToArgb());
                threedKey.SetValue("diffuse1Color", diffuse1.BackColor.ToArgb());
                threedKey.SetValue("specular1Color", specular1.BackColor.ToArgb());
                threedKey.SetValue("ambient2Color", ambient2.BackColor.ToArgb());
                threedKey.SetValue("diffuse2Color", diffuse2.BackColor.ToArgb());
                threedKey.SetValue("specular2Color", specular2.BackColor.ToArgb());
                threedKey.SetValue("ambient3Color", ambient3.BackColor.ToArgb());
                threedKey.SetValue("diffuse3Color", diffuse3.BackColor.ToArgb());
                threedKey.SetValue("specular3Color", specular3.BackColor.ToArgb());
                threedKey.SetValue("ambient4Color", ambient4.BackColor.ToArgb());
                threedKey.SetValue("diffuse4Color", diffuse4.BackColor.ToArgb());
                threedKey.SetValue("specular4Color", specular4.BackColor.ToArgb());
                threedKey.SetValue("selectionBoxColor", selectionBox.BackColor.ToArgb());
                threedKey.SetValue("errorModelColor", errorModel.BackColor.ToArgb());
                threedKey.SetValue("insideFacesColor", insideFaces.BackColor.ToArgb());
                threedKey.SetValue("light1X", xdir1.Text);
                threedKey.SetValue("light1Y", ydir1.Text);
                threedKey.SetValue("light1Z", zdir1.Text);
                threedKey.SetValue("light2X", xdir2.Text);
                threedKey.SetValue("light2Y", ydir2.Text);
                threedKey.SetValue("light2Z", zdir2.Text);
                threedKey.SetValue("light3X", xdir3.Text);
                threedKey.SetValue("light3Y", ydir3.Text);
                threedKey.SetValue("light3Z", zdir3.Text);
                threedKey.SetValue("light4X", xdir4.Text);
                threedKey.SetValue("light4Y", ydir4.Text);
                threedKey.SetValue("light4Z", zdir4.Text);
            }
            catch { }
        }

        private void RegistryToForm()
        {
            try
            {
                if (threedKey == null)
                    return;

                backgroundTop.BackColor = Color.FromArgb((int)threedKey.GetValue("backgroundTopColor",backgroundTop.BackColor.ToArgb()));
                backgroundBottom.BackColor = Color.FromArgb((int)threedKey.GetValue("backgroundBottomColor", backgroundBottom.BackColor.ToArgb()));
                faces.BackColor = Color.FromArgb((int)threedKey.GetValue("facesColor", faces.BackColor.ToArgb()));
                edges.BackColor = Color.FromArgb((int)threedKey.GetValue("edgesColor", faces.BackColor.ToArgb()));
                selectedFaces.BackColor = Color.FromArgb((int)threedKey.GetValue("selectedFacesColor", selectedFaces.BackColor.ToArgb()));
                printerBase.BackColor = Color.FromArgb((int)threedKey.GetValue("printerBaseColor", printerBase.BackColor.ToArgb()));
                printerFrame.BackColor = Color.FromArgb((int)threedKey.GetValue("printerFrameColor", printerFrame.BackColor.ToArgb()));
                filament.BackColor = Color.FromArgb((int)threedKey.GetValue("filamenColor", filament.BackColor.ToArgb()));
                travelMoves.BackColor = Color.FromArgb((int)threedKey.GetValue("travelColor", travelMoves.BackColor.ToArgb()));
                selectedFilament.BackColor = Color.FromArgb((int)threedKey.GetValue("selectedFilamentColor", selectedFilament.BackColor.ToArgb()));
                outsidePrintbed.BackColor = Color.FromArgb((int)threedKey.GetValue("outsidePrintbedColor", outsidePrintbed.BackColor.ToArgb()));
                _showEdges = 0 != (int)threedKey.GetValue("showEdges", _showEdges ? 1 : 0);
                _showFaces = 0 != (int)threedKey.GetValue("showFaces", _showFaces ? 1 : 0);
                showPrintbed.Checked = 0 != (int)threedKey.GetValue("showPrintbed", showPrintbed.Checked ? 1 : 0);
                checkDisableTravelMoves.Checked = 0 != (int)threedKey.GetValue("disableTravelVisualization", checkDisableTravelMoves.Checked ? 1 : 0);
                enableLight1.Checked = 0 != (int)threedKey.GetValue("enableLight1", enableLight1.Checked ? 1 : 0);
                enableLight2.Checked = 0 != (int)threedKey.GetValue("enableLight2", enableLight2.Checked ? 1 : 0);
                enableLight3.Checked = 0 != (int)threedKey.GetValue("enableLight3", enableLight3.Checked ? 1 : 0);
                enableLight4.Checked = 0 != (int)threedKey.GetValue("enableLight4", enableLight4.Checked ? 1 : 0);

                // useVBOs = 0 != (int)threedKey.GetValue("useVBOs", useVBOs.Checked ? 1 : 0);
                comboDrawMethod.SelectedIndex = (int)threedKey.GetValue("drawMethod", 0);
                checkCorrectNormals.Checked = 0 != (int)threedKey.GetValue("correctNormals", checkCorrectNormals.Checked ? 1 : 0);
                ambient1.BackColor = Color.FromArgb((int)threedKey.GetValue("ambient1Color", ambient1.BackColor.ToArgb()));
                diffuse1.BackColor = Color.FromArgb((int)threedKey.GetValue("diffuse1Color", diffuse1.BackColor.ToArgb()));
                specular1.BackColor = Color.FromArgb((int)threedKey.GetValue("specular1Color", specular1.BackColor.ToArgb()));
                ambient2.BackColor = Color.FromArgb((int)threedKey.GetValue("ambient2Color", ambient2.BackColor.ToArgb()));
                diffuse2.BackColor = Color.FromArgb((int)threedKey.GetValue("diffuse2Color", diffuse2.BackColor.ToArgb()));
                specular2.BackColor = Color.FromArgb((int)threedKey.GetValue("specular2Color", specular2.BackColor.ToArgb()));
                ambient3.BackColor = Color.FromArgb((int)threedKey.GetValue("ambient3Color", ambient3.BackColor.ToArgb()));
                diffuse3.BackColor = Color.FromArgb((int)threedKey.GetValue("diffuse3Color", diffuse3.BackColor.ToArgb()));
                specular3.BackColor = Color.FromArgb((int)threedKey.GetValue("specular3Color", specular3.BackColor.ToArgb()));
                ambient4.BackColor = Color.FromArgb((int)threedKey.GetValue("ambient4Color", ambient4.BackColor.ToArgb()));
                diffuse4.BackColor = Color.FromArgb((int)threedKey.GetValue("diffuse4Color", diffuse4.BackColor.ToArgb()));
                specular4.BackColor = Color.FromArgb((int)threedKey.GetValue("specular4Color", specular4.BackColor.ToArgb()));
                selectionBox.BackColor = Color.FromArgb((int)threedKey.GetValue("selectionBoxColor", selectionBox.BackColor.ToArgb()));
                errorModel.BackColor = Color.FromArgb((int)threedKey.GetValue("errorModelColor", errorModel.BackColor.ToArgb()));
                insideFaces.BackColor = Color.FromArgb((int)threedKey.GetValue("insideFacesColor", insideFaces.BackColor.ToArgb()));
                xdir1.Text = (string)threedKey.GetValue("light1X", xdir1.Text);
                ydir1.Text = (string)threedKey.GetValue("light1Y", ydir1.Text);
                zdir1.Text = (string)threedKey.GetValue("light1Z", zdir1.Text);
                xdir2.Text = (string)threedKey.GetValue("light2X", xdir2.Text);
                ydir2.Text = (string)threedKey.GetValue("light2Y", ydir2.Text);
                zdir2.Text = (string)threedKey.GetValue("light2Z", zdir2.Text);
                xdir3.Text = (string)threedKey.GetValue("light3X", xdir3.Text);
                ydir3.Text = (string)threedKey.GetValue("light3Y", ydir3.Text);
                zdir3.Text = (string)threedKey.GetValue("light3Z", zdir3.Text);
                xdir4.Text = (string)threedKey.GetValue("light4X", xdir4.Text);
                ydir4.Text = (string)threedKey.GetValue("light4Y", ydir4.Text);
                zdir4.Text = (string)threedKey.GetValue("light4Z", zdir4.Text);
                GCodePath.correctNorms = checkCorrectNormals.Checked;
                
                if (threedKey.GetValue("backgroundColor", null) != null)
                {
                    threedKey.DeleteValue("backgroundColor");
                }
            }
            catch { }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            FormToRegistry();
            Hide();
        }

        private void faces_Click(object sender, EventArgs e)
        {
            colorDialog.Color = faces.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                faces.BackColor = colorDialog.Color;
                Main.main.Update3D();
            }
        }

        private void edges_Click(object sender, EventArgs e)
        {
            colorDialog.Color = edges.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                edges.BackColor = colorDialog.Color;
                Main.main.threedview.updateCuts = true;
                Main.main.Update3D();
            }
        }

        private void selectedFaces_Click(object sender, EventArgs e)
        {
            colorDialog.Color = selectedFaces.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                selectedFaces.BackColor = colorDialog.Color;
                Main.main.threedview.updateCuts = true;
                Main.main.Update3D();
            }
        }

        private void changecolor_Click(object sender, EventArgs e)
        {
            Panel p = (Panel)sender;
            colorDialog.Color = p.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                p.BackColor = colorDialog.Color;
                Main.main.threedview.updateCuts = true;
                Main.main.Update3D();
            }
        }

        private void textLayerHeight_TextChanged(object sender, EventArgs e)
        {
            Main.main.Update3D();
        }

        private void light_TextChanged(object sender, EventArgs e)
        {
            Main.main.Update3D();
        }

        private void showEdges_CheckedChanged(object sender, EventArgs e)
        {
            Main.main.Update3D();
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

        private void ThreeDSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            RegMemory.StoreWindowPos("threeDSettingsWindow", this, false, false);
        }

        private void comboDrawMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Main.main.Update3D();
        }

        private void outsidePrintbed_Click(object sender, EventArgs e)
        {
            colorDialog.Color = printerBase.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                outsidePrintbed.BackColor = colorDialog.Color;
                Main.main.threedview.updateCuts = true;
                Main.main.Update3D();
            }
        }

        private void lightcolor_Click(object sender, EventArgs e)
        {
            Panel p = (Panel)sender;
            colorDialog.Color = p.BackColor;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                p.BackColor = colorDialog.Color;
                Main.main.threedview.updateCuts = true;
                Main.main.Update3D();
            }
        }

        private float[] toGLColor(Color c)
        {
            float[] a = new float[4];
            a[3] = 1;
            a[0] = (float)c.R / 255.0f;
            a[1] = (float)c.G / 255.0f;
            a[2] = (float)c.B / 255.0f;
            return a;
        }

        private Vector4 toDir(TextBox x,TextBox y,TextBox z) {
            float xf, yf, zf;
            float.TryParse(x.Text, NumberStyles.Float, GCode.format, out xf);
            float.TryParse(y.Text, NumberStyles.Float, GCode.format, out yf);
            float.TryParse(z.Text, NumberStyles.Float, GCode.format, out zf);
            return new Vector4(xf, yf, zf, 0);
        }

        public Vector4 Dir1() { return toDir(xdir1, ydir1, zdir1); }
        public Vector4 Dir2() { return toDir(xdir2, ydir2, zdir2); }
        public Vector4 Dir3() { return toDir(xdir3, ydir3, zdir3); }
        public Vector4 Dir4() { return toDir(xdir4, ydir4, zdir4); }
        public float[] Diffuse1() { return toGLColor(diffuse1.BackColor); }
        public float[] Ambient1() { return toGLColor(ambient1.BackColor); }
        public float[] Specular1() { return toGLColor(specular1.BackColor); }
        public float[] Diffuse2() { return toGLColor(diffuse2.BackColor); }
        public float[] Ambient2() { return toGLColor(ambient2.BackColor); }
        public float[] Specular2() { return toGLColor(specular2.BackColor); }
        public float[] Diffuse3() { return toGLColor(diffuse3.BackColor); }
        public float[] Ambient3() { return toGLColor(ambient3.BackColor); }
        public float[] Specular3() { return toGLColor(specular3.BackColor); }
        public float[] Diffuse4() { return toGLColor(diffuse4.BackColor); }
        public float[] Ambient4() { return toGLColor(ambient4.BackColor); }
        public float[] Specular4() { return toGLColor(specular4.BackColor); }

        public void checkDisableTravelMoves_CheckedChanged(object sender, EventArgs e)
        {
            Main.main.Update3D();
        }

        private void checkCorrectNormals_CheckedChanged(object sender, EventArgs e)
        {
            GCodePath.correctNorms = checkCorrectNormals.Checked;
            Main.main.Update3D();
        }
    }
}
