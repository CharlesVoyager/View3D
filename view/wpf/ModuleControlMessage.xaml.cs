using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using View3D.model;

namespace View3D.view.wpf
{
    /// <summary>
    /// ModuleControlMessage.xaml 的互動邏輯
    /// </summary>
    public partial class ModuleControlMessage : Window
    {
        public bool IsCancelled = false;
        public ModuleControlMessage()
        {
            InitializeComponent();
            translate();
        }

        private void translate()
        {
            btn_Cancel.Content = Trans.T("B_CANCEL");
            btn_PorceedUpdate.Content = Trans.T("M_PROCEED_DOWNLOAD");
            txtMessage.Text = Trans.T("M_INVALID_MODULE_DETECTED");
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            IsCancelled = true;
            this.Close();
        }

        private void btn_PorceedUpdate_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
    }
}
