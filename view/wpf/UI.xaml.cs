using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using View3D.model;
using System.Threading;

namespace View3D.view.wpf
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UI : Window
    {
        public bool killed = false;
        public string olddata;
        public DateTime newDate;
        public Dictionary<string, string> dict;
        public ThreeDControl threedview = null;
        public static string temp_buffer = "";
        public string SN = "";
        public float layerNow;
        public int layerTotal;
        private bool sliceToolEnabled = false;
        
        public UI()
        {
            InitializeComponent();
            VisualStateManager.GoToState(UI_view, "State2", true);
            VisualStateManager.GoToState(UI_move, "State2", true);
            VisualStateManager.GoToState(UI_rotate, "State2", true);
            VisualStateManager.GoToState(UI_resize_advance, "State2", true);

            translate();
            Main.main.languageChanged += translate;
        }

        private void translate()
        {
            view_toggleButton.ToolTip = Trans.T("B_VIEW");
            move_toggleButton.ToolTip = Trans.T("B_MOVE");
            rotate_toggleButton.ToolTip = Trans.T("B_ROTATE");
            resize_toggleButton.ToolTip = Trans.T("B_SCALE");
            info_toggleButton.ToolTip = Trans.T("B_INFO");
            remove_toggleButton.ToolTip = Trans.T("B_REMOVE");
            import_button.ToolTip = Trans.T("B_IMPORT");
            help_button.ToolTip = Trans.T("W_HELP");

            view_toggleButton.Content = Trans.T("B_VIEW");
            move_toggleButton.Content = Trans.T("B_MOVE");
            rotate_toggleButton.Content = Trans.T("B_ROTATE");
            resize_toggleButton.Content = Trans.T("B_SCALE");
            info_toggleButton.Content = Trans.T("B_INFO");
            remove_toggleButton.Content = Trans.T("B_REMOVE");
            import_button.Content = Trans.T("B_IMPORT");
            help_button.Content = Trans.T("W_HELP");
        }

        public void modifyUITextSize()
        {
            UI_resize_advance.btn_Scale.FontSize = 12;
            UI_resize_advance.button_mmtoinch.FontSize = 12;
            UI_resize_advance.button_inchtomm.FontSize = 12;
            UI_resize_advance.lbl_Size.FontSize = 12;

            move_toggleButton.FontSize = 12;
            import_button.FontSize = 12;
        }

        public void setbuttonVisable(bool flag)
        {
            if (flag == true)
            {
                move_toggleButton.Visibility = Visibility.Visible;
                rotate_toggleButton.Visibility = Visibility.Visible;
                resize_toggleButton.Visibility = Visibility.Visible;
                info_toggleButton.Visibility = Visibility.Visible;
            }
            else
            {
                move_toggleButton.Visibility = Visibility.Hidden;
                rotate_toggleButton.Visibility = Visibility.Hidden;
                resize_toggleButton.Visibility = Visibility.Hidden;
                info_toggleButton.Visibility = Visibility.Hidden;
            }
            if (Main.main.objectPlacement.listObjects.SelectedItems.Count == 0)
            {
                remove_toggleButton.Visibility = Visibility.Hidden;
            }
            else
            {
                remove_toggleButton.Visibility = Visibility.Visible;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Main.main.Close();
        }

        private void view_toggleButton_Checked(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(UI_view, "State1", true);

            move_toggleButton.IsChecked = false;
            rotate_toggleButton.IsChecked = false;
            resize_toggleButton.IsChecked = false;
            Main.main.gObjectInformation.Hide();
        }

        private void view_toggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(UI_view, "State2", true);
            Main.main.Focus();
        }

        public void move_toggleButton_Checked(object sender, RoutedEventArgs e)
        {
            if (false == confirmSupportEditModeBreak())
            {
                return;
            }

            //KPPH RCGREY - fix to bug for scenarios where move reset has been invoked in between support generation
            if (!move_toggleButton.IsEnabled)
                return;

            VisualStateManager.GoToState(UI_move, "State1", true);
            view_toggleButton.IsChecked = false;
            rotate_toggleButton.IsChecked = false;
            resize_toggleButton.IsChecked = false;

            Main.main.gObjectInformation.Hide();

            PrintModel stl = Main.main.objectPlacement.SingleSelectedModel;
            if (stl == null) return;

            Main.main.threedview.ui.UI_move.slider_moveX.Maximum = 1000;
            Main.main.threedview.ui.UI_move.slider_moveX.Minimum = -1000;
            Main.main.threedview.ui.UI_move.slider_moveY.Maximum = 1000;
            Main.main.threedview.ui.UI_move.slider_moveY.Minimum = -1000;
            Main.main.threedview.ui.UI_move.slider_moveZ.Maximum = 1000;
            Main.main.threedview.ui.UI_move.slider_moveZ.Minimum = -1000;
            Main.main.threedview.ui.UI_move.slider_moveX.Value = stl.Position.x;
            Main.main.threedview.ui.UI_move.slider_moveY.Value = stl.Position.y;
            Main.main.threedview.ui.UI_move.slider_moveZ.Value = stl.Position.z;

            double moveMax, moveMin;

            moveMax = (int)Math.Floor((Main.printerSettings.PrintAreaWidth - (stl.BoundingBox.xMax - stl.Position.x)) * 100) * 0.01;
            moveMin = (int)Math.Ceiling((stl.Position.x - stl.BoundingBox.xMin) * 100) * 0.01;

            double a = Main.printerSettings.PrintAreaWidth - (stl.BoundingBox.xMax - stl.Position.x);
            double b = stl.Position.x - stl.BoundingBox.xMin;

            a += 0;
            b += 0;

            //module is out of bound,it cannot move. 
            if (moveMin > moveMax)
            {
                //Tim---20191014---Fix model and support separate
                Main.main.threedview.ui.UI_move.slider_moveX.Value = (float)(moveMin + moveMax) / 2;
            }
            else if (moveMin <= stl.Position.x && stl.Position.x <= moveMax)//module is in of bound.
                Main.main.threedview.ui.UI_move.slider_moveX.Value = stl.Position.x;
            else if (stl.Position.x > moveMax)//model is out of bound(too big), but it can move.
                Main.main.threedview.ui.UI_move.slider_moveX.Value = moveMax;
            else // (moveMin > stl.Position.x)//model is out of bound(too small), but it can move.
                Main.main.threedview.ui.UI_move.slider_moveX.Value = moveMin;

            if (moveMin > moveMax)
            {
                Main.main.threedview.ui.UI_move.slider_moveX.Maximum = (float)(moveMin + moveMax) / 2;
                Main.main.threedview.ui.UI_move.slider_moveX.Minimum = (float)(moveMin + moveMax) / 2;
            }
            else
            {
                Main.main.threedview.ui.UI_move.slider_moveX.Maximum = moveMax;
                Main.main.threedview.ui.UI_move.slider_moveX.Minimum = moveMin;
            }


            moveMax = (int)Math.Floor((Main.printerSettings.PrintAreaDepth - (stl.BoundingBox.yMax - stl.Position.y)) * 100) * 0.01;
            moveMin = (int)Math.Ceiling((stl.Position.y - stl.BoundingBox.yMin) * 100) * 0.01;
            
            //module is out of bound,it can not move. 
            if (moveMin > moveMax)
            {
                //Tim---20191014---Fix model and support separate
                Main.main.threedview.ui.UI_move.slider_moveY.Value = (float)(moveMin + moveMax) / 2;
            }
            else if (moveMin <= stl.Position.y && stl.Position.y <= moveMax)//module is in of bound.
                Main.main.threedview.ui.UI_move.slider_moveY.Value = stl.Position.y;
            else if (stl.Position.y > moveMax)//model is out of bound(too big), but it can move.
                Main.main.threedview.ui.UI_move.slider_moveY.Value = moveMax;
            else // (moveMin > stl.Position.y)//model is out of bound(too small), but it can move.
                Main.main.threedview.ui.UI_move.slider_moveY.Value = moveMin;

            if (moveMin > moveMax)
            {
                Main.main.threedview.ui.UI_move.slider_moveY.Maximum = (float)(moveMin + moveMax) / 2;
                Main.main.threedview.ui.UI_move.slider_moveY.Minimum = (float)(moveMin + moveMax) / 2;
            }
            else
            {
                Main.main.threedview.ui.UI_move.slider_moveY.Maximum = moveMax;
                Main.main.threedview.ui.UI_move.slider_moveY.Minimum = moveMin;
            }

            moveMax = Main.printerSettings.PrintAreaHeight - (stl.BoundingBoxWOSupport.zMax - stl.Position.z);
            moveMin = stl.Position.z - stl.BoundingBoxWOSupport.zMin;
            if (moveMin > moveMax)
                moveMin = moveMax;
            if (moveMin <= stl.Position.z && moveMax >= stl.Position.z)
                Main.main.threedview.ui.UI_move.slider_moveZ.Value = stl.Position.z;
            else if (moveMax < stl.Position.z)
                Main.main.threedview.ui.UI_move.slider_moveZ.Value = moveMax;
            else // (moveMin > stl.Position.z)
                Main.main.threedview.ui.UI_move.slider_moveZ.Value = moveMin;
            Main.main.threedview.ui.UI_move.slider_moveZ.Maximum = moveMax;
            Main.main.threedview.ui.UI_move.slider_moveZ.Minimum = moveMin;

            Main.main.threedview.ui.UI_move.moveZ_textbox.Text = (Math.Round(Main.main.threedview.ui.UI_move.slider_moveZ.Value - Main.main.threedview.ui.UI_move.slider_moveZ.Minimum, 3)).ToString();
        }

        public void move_toggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(UI_move, "State2", true);
            Main.main.Focus();
        }

        private void import_button_Click(object sender, RoutedEventArgs e)
        {
            if (false == confirmSupportEditModeBreak())
            {
                return;
            }
            view_toggleButton.IsChecked = false;
            move_toggleButton.IsChecked = false;
            rotate_toggleButton.IsChecked = false;
            resize_toggleButton.IsChecked = false;
            Main.main.gObjectInformation.Hide();
            Main.main.toolGCodeLoad_Click(null, null);
            // not enable
            if (Main.main.objectPlacement.listObjects.Items.Count > 0)
            {
                Main.main.threedview.viewSilhouette = false;
                Main.main.threedview.clipDownward = true;
                Main.main.threedview.setclipLayerHeight = (double)0.1;

            }
            Main.main.Focus();

            //KPPH CP2 SW added by RCGREY for STL Slice Previewer
            Main.main.threedview.setMinMaxClippingLayer();
            Main.main.threedview.stlComp.Update3D();
        }

        public void IsTabStopAllToggleButton(object control, bool pIsTabStop)
        {
            if (control is ToggleButton)
                ((ToggleButton)control).IsTabStop = pIsTabStop;
            else
                if (control is Grid)
                    foreach (UIElement child in ((Grid)control).Children)
                        IsTabStopAllToggleButton(child, pIsTabStop);
            if (control is StackPanel)
                foreach (UIElement child in ((StackPanel)control).Children)
                    IsTabStopAllToggleButton(child, pIsTabStop);
        }

        private void help_button_Click(object sender, RoutedEventArgs e)
        {
            view_toggleButton.IsChecked = false;
            move_toggleButton.IsChecked = false;
            rotate_toggleButton.IsChecked = false;
            resize_toggleButton.IsChecked = false;

            Main.main.gObjectInformation.Hide();
            Main.main.threedview.button_helpInfo_Click(null, null);
            Main.main.Focus();
        }

        private void rotate_toggleButton_Checked(object sender, RoutedEventArgs e)
        {
            Main.main.threedview.ui.UI_rotate.sliderX.Value = Convert.ToDouble(Main.main.objectPlacement.textRotX.Text);
            Main.main.threedview.ui.UI_rotate.sliderY.Value = Convert.ToDouble(Main.main.objectPlacement.textRotY.Text);
            Main.main.threedview.ui.UI_rotate.sliderZ.Value = Convert.ToDouble(Main.main.objectPlacement.textRotZ.Text);
            VisualStateManager.GoToState(UI_rotate, "State1", true);
            view_toggleButton.IsChecked = false;
            move_toggleButton.IsChecked = false;
            resize_toggleButton.IsChecked = false;
            Main.main.gObjectInformation.Hide();
        }

        // Scale
        public void resize_toggleButton_Checked(object sender, RoutedEventArgs e)
        {           
            VisualStateManager.GoToState(UI_resize_advance, "State1", true);
            view_toggleButton.IsChecked = false;
            move_toggleButton.IsChecked = false;
            rotate_toggleButton.IsChecked = false;
            Main.main.gObjectInformation.Hide();

            PrintModel stl = Main.main.objectPlacement.SingleSelectedModel;
            if (stl == null) return;
            UI_move.slider_moveZ.Maximum = 1000;

            model.geom.RHBoundingBox bbox = stl.BoundingBoxWOSupport;
            UI_resize_advance.bboxnow = bbox.Size.x / Convert.ToDouble(Main.main.objectPlacement.textScaleX.Text);
            UI_resize_advance.bboynow = bbox.Size.y / Convert.ToDouble(Main.main.objectPlacement.textScaleY.Text);
            UI_resize_advance.bboznow = bbox.Size.z / Convert.ToDouble(Main.main.objectPlacement.textScaleZ.Text);

            UI_resize_advance.gIsShow = true;
            UI_resize_advance.dimX = bbox.Size.x;
            UI_resize_advance.updateTxt(Enums.Axis.X);
            UI_resize_advance.dimY = bbox.Size.y;
            UI_resize_advance.updateTxt(Enums.Axis.Y);
            UI_resize_advance.dimZ = bbox.Size.z;
            UI_resize_advance.updateTxt(Enums.Axis.Z);

            if (Convert.ToDouble(Main.main.objectPlacement.textRotX.Text) != 0 ||
                Convert.ToDouble(Main.main.objectPlacement.textRotY.Text) != 0 ||
                Convert.ToDouble(Main.main.objectPlacement.textRotZ.Text) != 0)
            {
                UI_resize_advance.chk_Uniform.IsChecked = true;
                UI_resize_advance.chk_Uniform.IsEnabled = false;
            }
            else
            {
                UI_resize_advance.chk_Uniform.IsEnabled = true;
            }

            if (UI_resize_advance.chk_Uniform.IsChecked == true)
            {
                UI_resize_advance.chk_Uniform_Checked(null, null);
            }

            UI_resize_advance.gIsShow = false;
            UI_resize_advance.button_mmtoinch.IsEnabled = true;
            UI_resize_advance.button_inchtomm.IsEnabled = false;
            UI_resize_advance.lbl_XUnits.Content = Trans.T("L_MM");
            UI_resize_advance.lbl_YUnits.Content = Trans.T("L_MM");
            UI_resize_advance.lbl_ZUnits.Content = Trans.T("L_MM");

            UI_resize_advance.txt_Scale.Text = "";
            UI_resize_advance.button_Reset.ToolTip = Trans.T("B_RESET");
            UI_resize_advance.button_Reset.Content = Trans.T("B_RESET");
            UI_resize_advance.lbl_Uniform.Content = Trans.T("L_UNIFORM");
            UI_resize_advance.lbl_Size.Content = Trans.T("L_SIZE");
            UI_resize_advance.btn_Scale.Content = Trans.T("B_APPLY");
            UI_resize_advance.button_mmtoinch.Content = Trans.T("B_SCALE_UP") + " (" + Trans.T("L_MM") + "→" + Trans.T("L_INCH") + ")";
            UI_resize_advance.button_inchtomm.Content = Trans.T("B_SCALE_DOWN") + " (" + Trans.T("L_INCH") + "→" + Trans.T("L_MM") + ")";

            modifyUITextSize();
        }

        private void rotate_toggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(UI_rotate, "State2", true);
            Main.main.Focus();
        }

        private void resize_toggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(UI_resize_advance, "State2", true);
            Main.main.Focus();
        }
     
        private bool confirmSupportEditModeBreak()
        {
            return true;
        }
   
        public void info_toggleButton_Click(object sender, RoutedEventArgs e)
        {
            view_toggleButton.IsChecked = false;
            move_toggleButton.IsChecked = false;
            rotate_toggleButton.IsChecked = false;
            resize_toggleButton.IsChecked = false;

            Main.main.gObjectInformation.Hide();
            Main.main.objectPlacement.showObjectInfo(null, null);
        }

        public void remove_toggleButton_Click(object sender, RoutedEventArgs e)
        {
            view_toggleButton.IsChecked = false;
            move_toggleButton.IsChecked = false;
            rotate_toggleButton.IsChecked = false;
            resize_toggleButton.IsChecked = false;

            Main.main.threedview.ui.UI_move.slider_moveX.Minimum = -1000;
            Main.main.threedview.ui.UI_move.slider_moveX.Maximum = 1000;
            Main.main.threedview.ui.UI_move.slider_moveY.Minimum = -1000;
            Main.main.threedview.ui.UI_move.slider_moveY.Maximum = 1000;

            Main.main.threedview.ui.OutofBound.Visibility = System.Windows.Visibility.Hidden;
            Main.main.threedview.ui.LayerSlider.Visibility = System.Windows.Visibility.Hidden;
            Main.main.threedview.ui.LayerNumber.Visibility = System.Windows.Visibility.Hidden;
            Main.main.threedview.clipviewEnabled = false;
            Main.main.gObjectInformation.Hide();
            Main.main.threedview.button_remove_Click(null, null);
            if (Main.main.objectPlacement.listObjects.Items.Count > 0)
            {
                Main.main.objectPlacement.updateSTLState((PrintModel)Main.main.objectPlacement.listObjects.Items[0].Tag);
            }
            else
            {
                //Modified by RCGREY for STL Slice Previewer
                Main.main.threedview.setMinMaxClippingLayer();
            }
            Main.main.Focus();
        }

        private void zoomin_toggleButton_Click(object sender, RoutedEventArgs e)
        {
            view_toggleButton.IsChecked = false;
            move_toggleButton.IsChecked = false;
            rotate_toggleButton.IsChecked = false;
            resize_toggleButton.IsChecked = false;

            Main.main.threedview.button_zoomIn_Click(null, null);
            Main.main.Focus();
        }

        private void zoomout_toggleButton_Click(object sender, RoutedEventArgs e)
        {
            view_toggleButton.IsChecked = false;
            move_toggleButton.IsChecked = false;
            rotate_toggleButton.IsChecked = false;
            resize_toggleButton.IsChecked = false;

            Main.main.threedview.button_zoomOut_Click(null, null);
            Main.main.Focus();
        }

        private void button_import_kill_Click(object sender, RoutedEventArgs e)
        {
            killed = true;
        }

        private void button_slice_kill_Click(object sender, RoutedEventArgs e)
        {
   
        }

        private void info_toggleButton_Checked(object sender, RoutedEventArgs e)
        {
            info_toggleButton.IsChecked = false;
        }

        private void remove_toggleButton_Checked(object sender, RoutedEventArgs e)
        {
            remove_toggleButton.IsChecked = false;
        }

        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public void ChangeLoginUI(bool isForceLogin)
        {
        }

        System.Windows.Forms.Timer timer;
        int sec = 0;

        #region STL Slice Previewer
        //Modified by RCGrey for STL Slice Previewer tool
        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double layerNum;

            if (Keyboard.IsKeyDown(Key.Right) || Keyboard.IsKeyDown(Key.Left))
            {
                LayerSlider.Value = (int)Main.main.threedview.clippingLayerCur;
                return;
            }
            if (Main.main.threedview.clipviewEnabled && LayerSlider.IsFocused)
            {
                LayerSlider.Value = (int)LayerSlider.Value;

                if (Main.main.threedview.ui.LayerSlider.Value <= 0)
                    Main.main.threedview.ui.LayerNumber.Visibility = System.Windows.Visibility.Hidden;
                else
                    Main.main.threedview.ui.LayerNumber.Visibility = System.Windows.Visibility.Visible;

                LayerNumber.Text = LayerSlider.Value.ToString();
                Main.main.threedview.UpdateLayerNumPosition();

                layerNum = LayerSlider.Value;
                Main.main.threedview.clippingLayerCur = layerNum;

                Main.main.threedview.stlComp.Update3D();
            }
        }
        #endregion
    }
    public static class MouseDownHelper
    {
        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached("IsEnabled",
        typeof(bool), typeof(MouseDownHelper), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnNotifyPropertyChanged)));

        private static void OnNotifyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as UIElement;
            if (element != null && e.NewValue != null)
            {
                if ((bool)e.NewValue)
                {
                    Register(element);
                }
                else
                {
                    UnRegister(element);
                }
            }
        }

        private static void Register(UIElement element)
        {
            element.MouseDown += element_MouseDown;
            element.MouseLeftButtonDown += element_MouseLeftButtonDown;
            element.MouseLeave += element_MouseLeave;
            element.MouseUp += element_MouseUp;
        }

        private static void UnRegister(UIElement element)
        {
            element.MouseDown -= element_MouseDown;
            element.MouseLeftButtonDown -= element_MouseLeftButtonDown;
            element.MouseLeave -= element_MouseLeave;
            element.MouseUp -= element_MouseUp;
        }

        private static void element_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var element = e.Source as UIElement;
            if (element != null)
            {
                SetIsMouseDown(element, true);
            }
        }

        private static void element_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var element = e.Source as UIElement;
            if (element != null)
            {
                SetIsMouseLeftButtonDown(element, true);
            }
        }

        private static void element_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var element = e.Source as UIElement;
            if (element != null)
            {
                SetIsMouseDown(element, false);
                SetIsMouseLeftButtonDown(element, false);
            }
        }

        private static void element_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var element = e.Source as UIElement;
            if (element != null)
            {
                SetIsMouseDown(element, false);
                SetIsMouseLeftButtonDown(element, false);
            }
        }

        internal static readonly DependencyPropertyKey IsMouseDownPropertyKey = DependencyProperty.RegisterAttachedReadOnly("IsMouseDown",
        typeof(bool), typeof(MouseDownHelper), new FrameworkPropertyMetadata(false));
        public static readonly DependencyProperty IsMouseDownProperty = IsMouseDownPropertyKey.DependencyProperty;

        internal static void SetIsMouseDown(UIElement element, bool value)
        {
            element.SetValue(IsMouseDownPropertyKey, value);
        }

        internal static readonly DependencyPropertyKey IsMouseLeftButtonDownPropertyKey = DependencyProperty.RegisterAttachedReadOnly("IsMouseLeftButtonDown",
        typeof(bool), typeof(MouseDownHelper), new FrameworkPropertyMetadata(false));
        public static readonly DependencyProperty IsMouseLeftButtonDownProperty = IsMouseLeftButtonDownPropertyKey.DependencyProperty;

        internal static void SetIsMouseLeftButtonDown(UIElement element, bool value)
        {
            element.SetValue(IsMouseLeftButtonDownPropertyKey, value);
        }
    }
}
