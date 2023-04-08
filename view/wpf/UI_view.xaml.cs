using System.Windows;
using System.Windows.Controls;
using View3D.model;

namespace View3D.view.wpf
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UI_view : UserControl
    {
        public UI_view()
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
                top_button.ToolTip = Trans.T("B_TOP");
                left_button.ToolTip = Trans.T("B_LEFT");
                right_button.ToolTip = Trans.T("B_RIGHT");
                front_button.ToolTip = Trans.T("B_FRONT");
                back_button.ToolTip = Trans.T("B_BACK");
                bottom_button.ToolTip = Trans.T("B_BOTTOM");
                view_resetButton.ToolTip = Trans.T("B_RESET");
            //}
            //if (Main.main.only_show_english_button)
            //{
                top_button.Content = Trans.T("B_TOP");
                left_button.Content = Trans.T("B_LEFT");
                right_button.Content = Trans.T("B_RIGHT");
                front_button.Content = Trans.T("B_FRONT");
                back_button.Content = Trans.T("B_BACK");
                bottom_button.Content = Trans.T("B_BOTTOM");
                view_resetButton.Content = Trans.T("B_RESET");
            //}
        }

        public void modifyViewTextSize()
        {
                right_button.FontSize = 12;
        }

        private void top_button_Click(object sender, RoutedEventArgs e)
        {
            Main.main.threedview.topView();
        }

        private void bottom_button_Click(object sender, RoutedEventArgs e)
        {
            Main.main.threedview.bottomView();
        }

        private void front_button_Click(object sender, RoutedEventArgs e)
        {
            Main.main.threedview.frontView();
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            Main.main.threedview.backView();
        }

        private void left_button_Click(object sender, RoutedEventArgs e)
        {
            Main.main.threedview.leftView();
        }

        private void right_button_Click(object sender, RoutedEventArgs e)
        {
            Main.main.threedview.rightView();
        }

        private void view_resetButton_Click(object sender, RoutedEventArgs e)
        {
            Main.main.threedview.isometricView();
        }
    }
}
