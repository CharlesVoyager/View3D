using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using View3D.model;
using View3D.view;

namespace View3D.view.wpf
{
    /// <summary>
    /// Interaction logic for UI_resize.xaml
    /// </summary>
    public partial class UI_resize : UserControl
    {
        public bool gTextChange = false;
        public UI_resize()
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
        {
            //if (Main.main.show_tooltip)
            //{
                button_resize_reset.ToolTip = Trans.T("B_RESET");
            //}
            //if (Main.main.only_show_english_button)
            //{
                button_resize_reset.Content = Trans.T("B_RESET");
            //}
            button_mmtoinch.Content = Trans.T("L_MM")+" \r\n  > \r\n"+Trans.T("L_INCH")+" \r\n";
            button_inchtomm.Content = Trans.T("L_INCH") + " \r\n  > \r\n" + Trans.T("L_MM") + " \r\n";
        }

        private void slider_resize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                if (gTextChange == true)
                {
                    gTextChange = false;
                    return;
                }
                PrintModel stl = Main.main.objectPlacement.SingleSelectedModel;
                if (stl == null) return;
                stl.modifiedS = true;
                //double centerValue = (stl.maxScaleVector + stl.minScaleVector) / 2.0;
                //double scaleValue = stl.Scale.x;

                double m = stl.m;
                double b = stl.b;
                double scaleValue;
                float oriZmin;
                oriZmin = stl.zMin;

                if (slider_resize.Value > 1)
                {
                    scaleValue = m * (slider_resize.Value) + b;
                }
                else
                {
                    scaleValue = slider_resize.Value;
                }

                if (stl.maxScaleVector == 1.0)
                {
                    scaleValue = slider_resize.Value / 2;
                }
                resize_textbox.Text = ((int)(scaleValue * 100)).ToString(CultureInfo.InvariantCulture) ;
            }
            catch { }
        }

        private void slider_resize_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PrintModel stl = Main.main.objectPlacement.SingleSelectedModel;
            if(stl == null) return;
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

        public void button_resize_reset_Click(object sender, RoutedEventArgs e)
        {
            float oriZmin;
            PrintModel stl = Main.main.objectPlacement.SingleSelectedModel;
            
            oriZmin = stl.zMin;

            resize_textbox.Text = "100";
            Main.main.objectPlacement.textScaleX.Text = "1";
            Main.main.objectPlacement.check_stl_size_too_small();
            stl.modifiedS = false;
        }

        private void resize_textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                PrintModel stl = Main.main.objectPlacement.SingleSelectedModel;
                if (stl == null) return;
                stl.modifiedS = true;

                double m = stl.m;
                double b = stl.b;
                double marginHardLimit = 1.0;
                double dx = (Main.printerSettings.PrintAreaWidth - marginHardLimit * 2) / stl.originalModel.boundingBox.Size.x;
                double dy = (Main.printerSettings.PrintAreaDepth - marginHardLimit * 2) / stl.originalModel.boundingBox.Size.y;
                double dz = (Main.printerSettings.PrintAreaHeight - marginHardLimit * 2) / stl.originalModel.boundingBox.Size.z;

                double maxScaleVector = Math.Min(dx, Math.Min(dy, dz));

                if (maxScaleVector <= 1.0)
                {
                    maxScaleVector = 1.0;
                }

                if (m == 0.0)
                {
                    stl.m = (maxScaleVector - 1) / 1;
                }

                if (b == 0.0)
                {
                    stl.b = maxScaleVector - stl.m * 2;
                }

                double scaleValue = 0.000;
                float oriZmin;
                oriZmin = stl.zMin;
                Main.main.objectPlacement.textScaleX.Text = Convert.ToString(int.Parse(resize_textbox.Text) / 100.00); 
                stl.LandToZ(oriZmin);
                Main.main.threedview.ui.UI_move.slider_moveZ.Minimum = stl.Position.z - stl.BoundingBoxWOSupport.zMin;
                Main.main.objectPlacement.updateSTLState(stl);


                scaleValue = int.Parse(resize_textbox.Text) / 100.00;
                if (stl.maxScaleVector == 1.0)
                {
                    scaleValue = scaleValue * 2 ;
                }

                if (int.Parse(resize_textbox.Text) / 100.00 > 1)
                {
                    //scaleValue = m * (slider_resize.Value) + b;
                    scaleValue = (scaleValue - b) / m;
                }
                gTextChange = true;
                slider_resize.Value = scaleValue;
            }
            catch { }
        }

        private void button_mmtoinch_Checked(object sender, RoutedEventArgs e)
        {
            PrintModel model = Main.main.objectPlacement.SingleSelectedModel;
            if (model == null) return;
            Main.main.objectPlacement.DoInchOrScale(model, true);
        }

        private void button_inchtomm_Checked(object sender, RoutedEventArgs e)
        {
            PrintModel model = Main.main.objectPlacement.SingleSelectedModel;
            if (model == null) return;
            Main.main.objectPlacement.DoInchtomm(model);
        }

        private void button_mmtoinch_Click(object sender, RoutedEventArgs e)
        {
            PrintModel model = Main.main.objectPlacement.SingleSelectedModel;
            if (model == null) return;
            Main.main.objectPlacement.DoInchOrScale(model, true);
        }

        private void button_inchtomm_Click(object sender, RoutedEventArgs e)
        {
            PrintModel model = Main.main.objectPlacement.SingleSelectedModel;
            if (model == null) return;
            Main.main.objectPlacement.DoInchtomm(model);
        }
    }
}
