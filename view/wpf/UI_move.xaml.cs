using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using View3D.model;

namespace View3D.view.wpf
{
    /// <summary>
    /// Interaction logic for UI_move.xaml
    /// </summary>
    public partial class UI_move : UserControl
    {
        public UI_move()
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
                button_move_reset.ToolTip = Trans.T("B_RESET");
                button_land.ToolTip = Trans.T("B_LAND");
            //}
            //if (Main.main.only_show_english_button)
            //{
                button_move_reset.Content = Trans.T("B_RESET");
                button_land.Content = Trans.T("B_LAND");
            //}
        }

        protected override void OnPreviewTextInput( TextCompositionEventArgs e)
        {
            if (e.Text == "")
            {
                return;
            }

            char c = Convert.ToChar(e.Text);
            if (Char.IsNumber(c) || e.Text.Trim() == ".")
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
            if (e.Text.Trim() == ".")
            {
                if (moveX_textbox.IsFocused)
                {
                    if (moveX_textbox.Text.IndexOf(".") != -1)
                    {
                        e.Handled = true;
                    }
                }
                else if (moveY_textbox.IsFocused)
                {
                    if (moveY_textbox.Text.IndexOf(".") != -1)
                    {
                        e.Handled = true;
                    }
                }
                else if (moveZ_textbox.IsFocused)
                {
                    if (moveZ_textbox.Text.IndexOf(".") != -1)
                    {
                        e.Handled = true;
                    }
                }
            }
            base.OnPreviewTextInput(e);
        }

        public void button_move_reset_Click(object sender, RoutedEventArgs e)
        {
            PrintModel stl = Main.main.objectPlacement.SingleSelectedModel;
            if (stl == null) return;

            slider_moveX.Value = stl.Position.inix;
            slider_moveY.Value = stl.Position.iniy;
            slider_moveZ.Value = stl.Position.iniz;

            //Fix bug 180,
            slider_moveX.Value = Math.Round(slider_moveX.Value);
            slider_moveY.Value = Math.Round(slider_moveY.Value);
            if (slider_moveZ.Value != Convert.ToDouble(moveZ_textbox.Text) + slider_moveZ.Minimum)
            {
                slider_moveZ.Value = Convert.ToDouble(moveZ_textbox.Text) + slider_moveZ.Minimum;
            }

            moveX_textbox.Text = slider_moveX.Value.ToString();
            moveY_textbox.Text = slider_moveY.Value.ToString();
            moveZ_textbox.Text = (Math.Round(slider_moveZ.Value - slider_moveZ.Minimum, 3)).ToString();

            Main.main.objectPlacement.landModel(stl);
            Main.main.objectPlacement.updateSTLState(stl);
            Main.main.threedview.UpdateChanges();
            //Modified by RCGREY for STL Slice Previewer
            //Main.main.threedview.setMinMaxClippingLayer();


            stl.modifiedM = false;
        }

        public void button_land_Click(object sender, RoutedEventArgs e)
        {
            
            PrintModel stl = Main.main.objectPlacement.SingleSelectedModel;
            if (stl == null) return;


            Main.main.objectPlacement.landModel(stl);

            slider_moveX.Value = Math.Round(stl.Position.x);
            slider_moveY.Value = Math.Round(stl.Position.y);
            slider_moveZ.Value = stl.Position.z;
            moveX_textbox.Text = slider_moveX.Value.ToString();
            moveY_textbox.Text = slider_moveY.Value.ToString();
            //Fix bug 180
            moveZ_textbox.Text = (Math.Round(slider_moveZ.Value - slider_moveZ.Minimum, 1)).ToString();

            Main.main.objectPlacement.updateSTLState(stl);
            Main.main.threedview.UpdateChanges();
            //Modified by RCGREY for STL Slice Previewer
            //Main.main.threedview.setMinMaxClippingLayer();

        }

        private void slider_moveX_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                slider_moveX.Value++;
            else
                slider_moveX.Value--;
            e.Handled = true;   
        }

        private void slider_moveY_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                slider_moveY.Value++;
            else
                slider_moveY.Value--;
            e.Handled = true;     
        }

        private void slider_moveZ_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                slider_moveZ.Value++;
            else
                slider_moveZ.Value--;
            e.Handled = true;   
        }

        private void slider_moveX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                if (Math.Abs(e.OldValue - e.NewValue) > 0.01)
                {
                    PrintModel stl = Main.main.objectPlacement.SingleSelectedModel;
                    if (stl == null) return;
                    stl.modifiedM = true;
                    Main.main.objectPlacement.textTransX.Text = Convert.ToString(slider_moveX.Value);
                //Modified by RCGREY for STL Slice Previewer
                Main.main.threedview.setMinMaxClippingLayer();
                Main.main.threedview.UpdateChanges();
                }
            }
            catch { }
        }

        private void slider_moveY_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                if (Math.Abs(e.OldValue - e.NewValue) > 0.01)
                {
                    PrintModel stl = Main.main.objectPlacement.SingleSelectedModel;
                    if (stl == null) return;
                    stl.modifiedM = true;
                    Main.main.objectPlacement.textTransY.Text = Convert.ToString(slider_moveY.Value);
                //Modified by RCGREY for STL Slice Previewer
                Main.main.threedview.setMinMaxClippingLayer();
                Main.main.threedview.UpdateChanges();

                }
            }
            catch { }
        }

        private void slider_moveZ_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                if (Math.Abs(e.OldValue - e.NewValue) > 0.0001)
                {
                    PrintModel stl = Main.main.objectPlacement.SingleSelectedModel;
                    if (stl == null) return;
                    stl.modifiedM = true;

                    Main.main.objectPlacement.textTransZ.Text = Convert.ToString(slider_moveZ.Value);
                    moveZ_textbox.Text = (Math.Round(slider_moveZ.Value, 3) - Math.Round(slider_moveZ.Minimum, 3)).ToString();
                //Modified by RCGREY for STL Slice Previewer                
                Main.main.threedview.updateMaximumClipLevel();
                Main.main.threedview.UpdateChanges();
                }
            }
            catch { }
        }

        private void moveX_textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    PrintModel stl = Main.main.objectPlacement.SingleSelectedModel;
                    if (stl == null) return;
                    slider_moveX.Value = Convert.ToDouble(moveX_textbox.Text);
                    if (slider_moveX.Value != Convert.ToDouble(moveX_textbox.Text))
                    {
                        moveX_textbox.Text = slider_moveX.Value.ToString();
                    }
                    Main.main.DoCommand(stl);
                }
                catch { }
            }
            if (moveX_textbox.Text.Trim() == "")
            {
                moveX_textbox.Text = "";
            }
        }

        private void moveY_textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    PrintModel stl = Main.main.objectPlacement.SingleSelectedModel;
                    if (stl == null) return;
                    slider_moveY.Value = Convert.ToDouble(moveY_textbox.Text);
                    if (slider_moveY.Value != Convert.ToDouble(moveY_textbox.Text))
                    {
                        moveY_textbox.Text = slider_moveY.Value.ToString();
                    }
                    Main.main.DoCommand(stl);
                }
                catch { }
            }
            if (moveY_textbox.Text.Trim() == "")
            {
                moveY_textbox.Text = "";
            }
        }

        private void moveZ_textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    PrintModel stl = Main.main.objectPlacement.SingleSelectedModel;
                    if (stl == null) return;
                    Double temp=slider_moveZ.Value;
                        slider_moveZ.Value = Convert.ToDouble(moveZ_textbox.Text) + slider_moveZ.Minimum;
                    if (temp == slider_moveZ.Value)
                    {
                        moveZ_textbox.Text = slider_moveZ.Value.ToString();
                    }
                    Main.main.DoCommand(stl);
                }
                catch { }
            }
            if (moveZ_textbox.Text.Trim() == "")
            {
                moveZ_textbox.Text = "";
            }
        }

        private void slider_moveX_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            PrintModel stl = Main.main.objectPlacement.SingleSelectedModel;
            if (stl == null) return;
            Main.main.DoCommand(stl);
        }

        private void slider_moveY_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            PrintModel stl = Main.main.objectPlacement.SingleSelectedModel;
            if (stl == null) return;
            Main.main.DoCommand(stl);
        }

        private void slider_moveZ_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            PrintModel stl = Main.main.objectPlacement.SingleSelectedModel;
            if (stl == null) return;
            Main.main.DoCommand(stl);
        }

        private void slider_moveX_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right || e.Key == Key.Up || e.Key == Key.Left || e.Key == Key.Down)
            {
                PrintModel stl = Main.main.objectPlacement.SingleSelectedModel;
                if (stl == null) return;
                Main.main.DoCommand(stl);
            }
        }

        private void slider_moveY_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right || e.Key == Key.Up || e.Key == Key.Left || e.Key == Key.Down)
            {
                PrintModel stl = Main.main.objectPlacement.SingleSelectedModel;
                if (stl == null) return;
                Main.main.DoCommand(stl);
            }
        }

        private void slider_moveZ_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right || e.Key == Key.Up || e.Key == Key.Left || e.Key == Key.Down)
            {
                PrintModel stl = Main.main.objectPlacement.SingleSelectedModel;
                if (stl == null) return;
                Main.main.DoCommand(stl);
            }
        }

        private void moveX_textbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (moveX_textbox.Text.Trim() == "")
            {
                moveX_textbox.Text = slider_moveX.Value.ToString();
            }
        }

        private void moveY_textbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (moveY_textbox.Text.Trim() == "")
            {
                moveY_textbox.Text = slider_moveY.Value.ToString();
            }
        }
    }
}