using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using View3D.model;

namespace View3D.view.wpf
{
    /// <summary>
    /// Interaction logic for UI_resize.xaml
    /// </summary>
    public partial class UI_resize_advance : UserControl
    {
        public bool gTextChange = false;
        public string xyzbind = "x";

        public bool gIsShow = false;
        public double bboxnow;
        public double bboynow;
        public double bboznow;
        public bool IsScale = false;
        private string scaleKeyDown = "";
        public double dimX = 0.0, dimY = 0.0, dimZ = 0.0;

        public UI_resize_advance()
        {
            InitializeComponent();
            try
            {
                translate();
                Main.main.languageChanged += translate;
            }
            catch { }
        }

        private void translate()
        { }

        private void txtX_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gIsShow == true)
            {
                return;
            }
            PrintModel stl = Main.main.objectPlacement.SingleSelectedModel;
            if (stl == null) return;
            try
            {
                if (dimX == 0)
                {
                    dimX = 0.001;
                    updateTxt(Enums.Axis.X);
                }
                else
                {
                    dimX = unit2MMTransform(Convert.ToDouble(txtX.Text));
                        
                    if(dimX == 0)
                    {
                        dimX = 0.001;
                    }
                }

                Double tbefore = Convert.ToDouble(Main.main.objectPlacement.textScaleX.Text);
                Double tTempx = dimX / bboxnow;
                Double tMultScale = 0.0;

                tMultScale = tTempx / tbefore;
                Main.main.objectPlacement.textScaleX.Text = tTempx.ToString();
                if (chk_Uniform.IsChecked == true)// && gFlag == true)
                {
                    Double temp = Convert.ToDouble(Main.main.objectPlacement.textScaleY.Text) * tMultScale;
                    if (temp > 0)
                    {
                        Main.main.objectPlacement.textScaleY.Text = temp.ToString();
                    }

                    temp = Convert.ToDouble(Main.main.objectPlacement.textScaleZ.Text) * tMultScale;
                    if (temp > 0)
                    {
                        Main.main.objectPlacement.textScaleZ.Text = temp.ToString();
                    }

                    gIsShow = true;
                    IsScale = false;
                    if (xyzbind == "x")
                    {
                        updateSliderValue(Enums.Axis.X);
                    }
                    else if (xyzbind == "y")
                    {
                        updateSliderValue(Enums.Axis.Y);
                    }
                    else if (xyzbind == "z")
                    {
                        updateSliderValue(Enums.Axis.Z);
                    }
                    //Fix_resize_issue_160803
                    if (scaleKeyDown != "x")
                    {
                        dimX = stl.BoundingBoxWOSupport.Size.x;
                        updateTxt(Enums.Axis.X);
                    }
                    if (scaleKeyDown != "y")
                    {
                        dimY = stl.BoundingBoxWOSupport.Size.y;
                        updateTxt(Enums.Axis.Y);
                    }
                    if (scaleKeyDown != "z")
                    {
                        dimZ = stl.BoundingBoxWOSupport.Size.z;
                        updateTxt(Enums.Axis.Z);
                    }
                    gIsShow = false;
                    IsScale = true;
                }
            }
            catch { }
        }

        private void txtY_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (gIsShow == true)
            {
                return;
            }
            PrintModel stl = Main.main.objectPlacement.SingleSelectedModel;
            if (stl == null) return;
            try
            {
                if (dimY == 0)
                {
                    dimY = 0.001;
                    updateTxt(Enums.Axis.Y);
                }
                else
                {
                    dimY = unit2MMTransform(Convert.ToDouble(txtY.Text));

                    if (dimY == 0)
                    {
                        dimY = 0.001;
                    }
                }

                Double tbefore = Convert.ToDouble(Main.main.objectPlacement.textScaleY.Text);
                Double tTempy = dimY / bboynow;
                Double tMultScale = 0.0;

                tMultScale = tTempy / tbefore;

                Main.main.objectPlacement.textScaleY.Text = tTempy.ToString();
                if (chk_Uniform.IsChecked == true)//&& gFlag==true)
                {
                    Double temp = Convert.ToDouble(Main.main.objectPlacement.textScaleX.Text) * tMultScale;
                    if (temp > 0)
                    {
                        Main.main.objectPlacement.textScaleX.Text = temp.ToString();
                    }

                    temp = Convert.ToDouble(Main.main.objectPlacement.textScaleZ.Text) * tMultScale;
                    if (temp > 0)
                    {
                        Main.main.objectPlacement.textScaleZ.Text = temp.ToString();
                    }

                    gIsShow = true;
                    IsScale = false;
                    if (xyzbind == "x")
                    {
                        updateSliderValue(Enums.Axis.X);
                    }
                    else if (xyzbind == "y")
                    {
                        updateSliderValue(Enums.Axis.Y);
                    }
                    else if (xyzbind == "z")
                    {
                        updateSliderValue(Enums.Axis.Z);
                    }
                    //Fix_resize_issue_160803
                    if (scaleKeyDown != "x")
                    {
                        dimX = stl.BoundingBoxWOSupport.Size.x;
                        updateTxt(Enums.Axis.X);
                    }
                    if (scaleKeyDown != "y")
                    {
                        dimY = stl.BoundingBoxWOSupport.Size.y;
                        updateTxt(Enums.Axis.Y);
                    }
                    if (scaleKeyDown != "z")
                    {
                        dimZ = stl.BoundingBoxWOSupport.Size.z;
                        updateTxt(Enums.Axis.Z);
                    }
                    gIsShow = false;
                    IsScale = true;
                }
            }
            catch { }
        }

        private void txtZ_TextChanged(object sender, TextChangedEventArgs e)
        {
            PrintModel stl = Main.main.objectPlacement.SingleSelectedModel;
            if (gIsShow == true)
            {
                stl.LandToZ(stl.zMin);
                Main.main.threedview.ui.UI_move.slider_moveZ.Value = stl.Position.z;
                Main.main.threedview.ui.UI_move.slider_moveZ.Minimum = stl.Position.z - stl.BoundingBoxWOSupport.zMin;
                Main.main.objectPlacement.updateSTLState(stl);
                stl.modifiedM = false;
                return;
            }
            if (stl == null) return;
            try
            { 
                if (dimZ == 0)
                {
                    dimZ = 0.001;
                    updateTxt(Enums.Axis.Z);
                }
                else
                {
                    dimZ = unit2MMTransform(Convert.ToDouble(txtZ.Text));

                    if (dimY == 0)
                    {
                        dimY = 0.001;
                    }
                }

                Double tbefore = Convert.ToDouble(Main.main.objectPlacement.textScaleZ.Text);
                Double tTempz = dimZ / bboznow;
                Double tMultScale = 0.0;

                tMultScale = tTempz / tbefore;

                Main.main.objectPlacement.textScaleZ.Text = tTempz.ToString();
                if (chk_Uniform.IsChecked == true)//&& gFlag==true)
                {
                    Double temp = Convert.ToDouble(Main.main.objectPlacement.textScaleX.Text) * tMultScale;
                    if (temp > 0)
                    {
                        Main.main.objectPlacement.textScaleX.Text = temp.ToString();
                    }

                    temp = Convert.ToDouble(Main.main.objectPlacement.textScaleY.Text) * tMultScale;
                    if (temp > 0)
                    {
                        Main.main.objectPlacement.textScaleY.Text = temp.ToString();
                    }

                    gIsShow = true;
                    IsScale = false;
                    if (xyzbind == "x")
                    {
                        updateSliderValue(Enums.Axis.X);
                    }
                    else if (xyzbind == "y")
                    {
                        updateSliderValue(Enums.Axis.Y);
                    }
                    else if (xyzbind == "z")
                    {
                        updateSliderValue(Enums.Axis.Z);
                    }
                    //Fix_resize_issue_160803
                    if (scaleKeyDown != "x")
                    {
                        dimX = stl.BoundingBoxWOSupport.Size.x;
                        updateTxt(Enums.Axis.X);
                    }
                    if (scaleKeyDown != "y")
                    {
                        dimY = stl.BoundingBoxWOSupport.Size.y;
                        updateTxt(Enums.Axis.Y);
                    }
                    if (scaleKeyDown != "z")
                    {
                        dimZ = stl.BoundingBoxWOSupport.Size.z;
                        updateTxt(Enums.Axis.Z);
                    }

                    gIsShow = false;
                    IsScale = true;
                }
                    stl.LandToZ(stl.zMin);
                Main.main.threedview.ui.UI_move.slider_moveZ.Minimum = stl.Position.z - stl.BoundingBoxWOSupport.zMin;
                Main.main.objectPlacement.updateSTLState(stl);
            }
            catch { }
        }

        public void chk_Uniform_Checked(object sender, RoutedEventArgs e)
        {
            PrintModel stl = Main.main.objectPlacement.SingleSelectedModel;
            if (stl == null) return;
            try
            {
                slider_resize.IsEnabled = true;
                slider_resizeTemp.IsEnabled = true;
                txt_Scale.IsEnabled = true;
                btn_Scale.IsEnabled = true;
                checkMin();

                if (xyzbind == "x")
                {
                    IsScale = false;
                    updateSliderValue(Enums.Axis.X);
                    IsScale = true;
                }
                else if (xyzbind == "y")
                {
                    IsScale = false;
                    updateSliderValue(Enums.Axis.Y);
                    IsScale = true;
                }
                else if (xyzbind == "z")
                {
                    IsScale = false;
                    updateSliderValue(Enums.Axis.Z);
                    IsScale = true;
                }
            }
            catch { }
        }

        private void chk_Uniform_UnChecked(object sender, RoutedEventArgs e)
        {
            try
            {
                slider_resize.IsEnabled = false;
                slider_resizeTemp.IsEnabled = false;
                txt_Scale.IsEnabled = false;
                btn_Scale.IsEnabled = false;
            }
            catch { }
        }

        private void slider_resize_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            PrintModel stl = Main.main.objectPlacement.SingleSelectedModel;
            if (stl == null) return;
            stl.modifiedS = true;
            if (e.Delta > 0)
                slider_resize.Value += 0.01;
            else
                slider_resize.Value -= 0.01;
            e.Handled = true;
        }

        private void slider_resize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            PrintModel stl = Main.main.objectPlacement.SingleSelectedModel;
            if (stl == null) return;

            gIsShow = true;
            gIsShow = false;

            switch (xyzbind)
            {
                case "x":
                    if (IsScale == true)
                    {
                        dimX = unit2MMTransform(slider_resize.Value);
                        updateTxt(Enums.Axis.X);
                    }
                    break;
                case "y":
                    if (IsScale == true)
                    {
                        dimY = unit2MMTransform(slider_resize.Value);
                        updateTxt(Enums.Axis.Y);
                    }
                    break;
                case "z":
                    if (IsScale == true)
                    {
                        dimZ = unit2MMTransform(slider_resize.Value);
                        updateTxt(Enums.Axis.Z);
                    }
                    break;
            }

            //Modified by RCGREY for STL Slice Previewer
            //Main.main.threedview.setMinMaxClippingLayer();
        }

        public void checkMin()
        {
            double txMaxScalableValue = Convert.ToDouble(Main.printerSettings.PrintAreaWidth) / dimX;
            double tyMaxScalableValue = Convert.ToDouble(Main.printerSettings.PrintAreaDepth) / dimY;
            double tzMaxScalableValue = Convert.ToDouble(Main.printerSettings.PrintAreaHeight) / dimZ;
            double tMaxScalableValue = Math.Min(Math.Min(txMaxScalableValue, tyMaxScalableValue), Math.Min(tyMaxScalableValue, tzMaxScalableValue));

            IsScale = false;
            if (txMaxScalableValue == tMaxScalableValue)
            {
                xyzbind = "x";
                slider_resize.Maximum = (double)Main.printerSettings.PrintAreaWidth;

                if ((dimY * txMaxScalableValue > (double)Main.printerSettings.PrintAreaDepth)
                        || (dimZ * txMaxScalableValue > (double)Main.printerSettings.PrintAreaHeight))
                {
                    slider_resize.Maximum = Math.Floor(dimX * Math.Min(tyMaxScalableValue, tzMaxScalableValue) * 1000) / 1000;
                }
            }
            else if (tyMaxScalableValue == tMaxScalableValue)
            {
                xyzbind = "y";
                slider_resize.Maximum = (double)Main.printerSettings.PrintAreaDepth;

                if ((dimX * tyMaxScalableValue > (double)Main.printerSettings.PrintAreaWidth)
                        || (dimZ * tyMaxScalableValue > (double)Main.printerSettings.PrintAreaHeight))
                {
                    slider_resize.Maximum = Math.Floor(dimY * Math.Min(txMaxScalableValue, tzMaxScalableValue) * 1000) / 1000;
                }
            }
            else if (tzMaxScalableValue == tMaxScalableValue)
            {
                xyzbind = "z";
                slider_resize.Maximum = (double)Main.printerSettings.PrintAreaHeight;

                if ((dimX * tzMaxScalableValue > (double)Main.printerSettings.PrintAreaWidth)
                        || (dimY * tzMaxScalableValue > (double)Main.printerSettings.PrintAreaDepth))
                {
                    slider_resize.Maximum = Math.Floor(dimZ * Math.Min(txMaxScalableValue, tyMaxScalableValue) * 1000) / 1000;
                }
            }

            slider_resize.Maximum = unit2InchTransform(slider_resize.Maximum);

            IsScale = true;
        }

        public void button_Reset_Click(object sender, RoutedEventArgs e)
        {
            PrintModel stl = Main.main.objectPlacement.SingleSelectedModel;
            if (stl == null) return;
            model.geom.RHBoundingBox bbox = stl.BoundingBoxWOSupport;
            Main.main.objectPlacement.textScaleX.Text = "1";
            Main.main.objectPlacement.textScaleY.Text = "1";
            Main.main.objectPlacement.textScaleZ.Text = "1";
            bboxnow = bbox.Size.x;
            bboynow = bbox.Size.y;
            bboznow = bbox.Size.z;
            txt_Scale.Text = "";

            gIsShow = true;
            dimX = bbox.Size.x;
            updateTxt(Enums.Axis.X);
            dimY = bbox.Size.y;
            updateTxt(Enums.Axis.Y);
            dimZ = bbox.Size.z;
            updateTxt(Enums.Axis.Z);
            IsScale = false;
            if (xyzbind == "x")
            {
                updateSliderValue(Enums.Axis.X);
            }
            else if (xyzbind == "y")
            {
                updateSliderValue(Enums.Axis.Y);
            }
            else if (xyzbind == "z")
            {
                updateSliderValue(Enums.Axis.Z);
            }
            IsScale = true;
            gIsShow = false;
            checkMin();
            txt_Scale.Text = "";
            Main.main.objectPlacement.check_stl_size_too_small();
            //Modified by RCGREY for STL Slice Previewer
            //Main.main.threedview.setMinMaxClippingLayer();
        }

        private void button_mmtoinch_Click(object sender, RoutedEventArgs e)
        {

            //Main.main.threedview.ui.UI_resize_advance.button_Reset_Click(null, null);
            Main.main.threedview.ui.UI_resize_advance.button_mmtoinch.IsEnabled = false;
            Main.main.threedview.ui.UI_resize_advance.button_inchtomm.IsEnabled = true;
            PrintModel stl = Main.main.objectPlacement.SingleSelectedModel;
            if (stl == null) return;
            Main.main.objectPlacement.DoInchOrScale(stl, true);
        }

        private void button_inchtomm_Click(object sender, RoutedEventArgs e)
        {
            //Main.main.threedview.ui.UI_resize_advance.button_Reset_Click(null, null);
            Main.main.threedview.ui.UI_resize_advance.button_mmtoinch.IsEnabled = true;
            Main.main.threedview.ui.UI_resize_advance.button_inchtomm.IsEnabled = false;
            PrintModel model = Main.main.objectPlacement.SingleSelectedModel;
            if (model == null) return;
            Main.main.objectPlacement.DoInchtomm(model);
        }

        private void btn_Scale_Click(object sender, RoutedEventArgs e)
        {
            PrintModel stl = Main.main.objectPlacement.SingleSelectedModel;
            if (stl == null) return;

            model.geom.RHBoundingBox bbox = stl.BoundingBoxWOSupport;

            try
            {
                Double tbeforeX = Convert.ToDouble(Main.main.objectPlacement.textScaleX.Text);
                Double tbeforeY = Convert.ToDouble(Main.main.objectPlacement.textScaleY.Text);
                Double tbeforeZ = Convert.ToDouble(Main.main.objectPlacement.textScaleZ.Text);
                Double temp = Convert.ToDouble(txt_Scale.Text) / 100;
                Double tAddScaleX = tbeforeX * temp;
                Double tAddScaleY = tbeforeY * temp;
                Double tAddScaleZ = tbeforeZ * temp;

                Main.main.objectPlacement.textScaleX.Text = tAddScaleX.ToString("0.0000000000000");
                Main.main.objectPlacement.textScaleY.Text = tAddScaleY.ToString("0.0000000000000");
                Main.main.objectPlacement.textScaleZ.Text = tAddScaleZ.ToString("0.0000000000000");
                gIsShow = true;
                dimX = stl.BoundingBoxWOSupport.Size.x;
                updateTxt(Enums.Axis.X);
                dimY = stl.BoundingBoxWOSupport.Size.y;
                updateTxt(Enums.Axis.Y);
                dimZ = stl.BoundingBoxWOSupport.Size.z;
                updateTxt(Enums.Axis.Z);
                gIsShow = false;
                //checkMin();
                IsScale = false;
                if (xyzbind == "x")
                {
                    updateSliderValue(Enums.Axis.X);
                }
                else if (xyzbind == "y")
                {
                    updateSliderValue(Enums.Axis.Y);
                }
                else if (xyzbind == "z")
                {
                    updateSliderValue(Enums.Axis.Z);
                }
                IsScale = true;
            }
            catch { }
        }

        private void scaleLostFocus(object sender, RoutedEventArgs e)
        {
            PrintModel stl = Main.main.objectPlacement.SingleSelectedModel;
            if (stl == null) return;

            try
            {
                dimX = stl.BoundingBoxWOSupport.Size.x;
                updateTxt(Enums.Axis.X);
                dimY = stl.BoundingBoxWOSupport.Size.y;
                updateTxt(Enums.Axis.Y);
                dimZ = stl.BoundingBoxWOSupport.Size.z;
                updateTxt(Enums.Axis.Z);

                scaleKeyDown = "";
            }
            catch { }
        }

        private void scaleTextBoxKeyBoardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if ((TextBox)sender == txtX)
                scaleKeyDown = "x";
            else if ((TextBox)sender == txtY)
                scaleKeyDown = "y";
            else if ((TextBox)sender == txtZ)
                scaleKeyDown = "z";
        }

        public void updateTxt(Enums.Axis axis, bool doUnitTransform = true)
        {
            double dimForDisplay = 0.0;

            switch (axis)
            {
                case Enums.Axis.X:
                    dimForDisplay = dimX;
                    if (doUnitTransform)
                    {
                        dimForDisplay = unit2InchTransform(dimForDisplay, (int)Main.printerSettings.PrintAreaWidth);
                    }
                    txtX.Text = dimForDisplay.ToString("0.000");
                    break;

                case Enums.Axis.Y:
                    dimForDisplay = dimY;
                    if (doUnitTransform)
                    {
                        dimForDisplay = unit2InchTransform(dimForDisplay, (int)Main.printerSettings.PrintAreaDepth);                      
                    }
                    txtY.Text = dimForDisplay.ToString("0.000");
                    break;

                case Enums.Axis.Z:
                    dimForDisplay = dimZ;
                    if (doUnitTransform)
                    {
                        dimForDisplay = unit2InchTransform(dimForDisplay, (int)Main.printerSettings.PrintAreaHeight);
                    }
                    txtZ.Text = dimForDisplay.ToString("0.000");
                    break;
            }        
        }

        public void updateSliderValue(Enums.Axis axis)
        {
            double dimForDisplay = 0.0;
            switch (axis)
            {
                case Enums.Axis.X:
                    dimForDisplay = dimX;
                    break;

                case Enums.Axis.Y:
                    dimForDisplay = dimY;
                    break;

                case Enums.Axis.Z:
                    dimForDisplay = dimZ;
                    break;
            }

            slider_resize.Value = Convert.ToDouble(unit2InchTransform(dimForDisplay).ToString("0.000"));
        }

        // mm -> inch
        // Note: No conversion. Just return the input value. 
        private double unit2InchTransform(double inputValue, int margin = 0)
        {
            double transformedValue = inputValue;
            return transformedValue;
        }

        // inch -> mm
        // Note: No conversion. Just return the input value. 
        private double unit2MMTransform(double inputValue)
        {
            double transformedValue = inputValue;
            return transformedValue;
        }
    }
}
