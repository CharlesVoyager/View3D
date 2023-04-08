using System;
using System.IO;
using View3D.model;
using View3D.model.geom;
using View3D.MeshInOut;
using System.Windows.Forms;

namespace View3D.view
{
    public class ModelInOut
    {
        public bool TwoStageUpdateProcess { get; set; }
        public event EventHandler AbortTask;
        private bool saveTaskAbort;
        private int mergedCount, mergeCountTotal;

        public ModelInOut()
        {
            TwoStageUpdateProcess = false;
        }
        public void Load(string file, ModelData model)
        {
            EnableBusyWindowNoCancleButton();

            string lname = model.FileName.ToLower();
            if (lname.EndsWith(".stl"))
            {
                MeshIOStl fileMesh = new MeshIOStl();
                AbortTask += fileMesh.TaskAbort;

                try
                {
                    fileMesh.Load(model.FileName, model.originalModel, OnProcessUpdate);
                }
                catch
                {
                    MessageBox.Show(Trans.T("M_LOAD_STL_FILE_ERROR"), Trans.T("W_LOAD_STL_FILE_ERROR"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                AbortTask -= fileMesh.TaskAbort;
            }

            DisableBusyWindow();

            FileInfo info = new FileInfo(file);
            model.name = info.Name;
        }

        public void LoadWOCatch(string file, ModelData model)
        {
            EnableBusyWindow();

            string lname = model.FileName.ToLower();

            IMeshInOut fileMesh;
            Action<int> updateRateFunc;
            if (lname.EndsWith(".stl"))
            {
                fileMesh = new MeshIOStl();
                updateRateFunc = OnProcessUpdate;
            }
            else
            {
                fileMesh = new MeshIOBase();
                updateRateFunc = OnProcessUpdate;
            }
            AbortTask += fileMesh.TaskAbort;
            fileMesh.LoadWOCatch(model.FileName, model.originalModel, updateRateFunc);
            AbortTask -= fileMesh.TaskAbort;

            DisableBusyWindow();

            FileInfo info = new FileInfo(file);
            model.name = info.Name;
        }
        public void Save(string filename, TopoModel model, Setting outSetting)
        { 
        }

        #region EvenHandler
        private void EnableBusyWindow()
        {
            // BusyWindow start
            Main.main.threedview.ui.BusyWindow.labelBusyMessage.Text = Trans.T("L_MODELING");
            Main.main.threedview.ui.BusyWindow.killed = false;
            Main.main.threedview.ui.BusyWindow.Visibility = System.Windows.Visibility.Visible;
            Main.main.threedview.ui.BusyWindow.buttonCancel.Visibility = System.Windows.Visibility.Visible;
            Main.main.threedview.ui.BusyWindow.busyProgressbar.IsIndeterminate = false;
            Main.main.threedview.ui.BusyWindow.busyProgressbar.Maximum = 100;
            Main.main.threedview.ui.BusyWindow.busyProgressbar.Value = 0; 
            Main.main.threedview.ui.BusyWindow.AbortTask += OnUIAbort;
            Main.main.threedview.ui.BusyWindow.StartTimer();
            System.Windows.Forms.Application.DoEvents();
        }

        private void EnableBusyWindowNoCancleButton()
        {
            // BusyWindow start
            Main.main.threedview.ui.BusyWindow.labelBusyMessage.Text = Trans.T("L_MODELING");
            Main.main.threedview.ui.BusyWindow.killed = false;
            Main.main.threedview.ui.BusyWindow.Visibility = System.Windows.Visibility.Visible;
            Main.main.threedview.ui.BusyWindow.busyProgressbar.IsIndeterminate = false;
            Main.main.threedview.ui.BusyWindow.busyProgressbar.Maximum = 100;
            Main.main.threedview.ui.BusyWindow.busyProgressbar.Value = 0; 
            Main.main.threedview.ui.BusyWindow.AbortTask += OnUIAbort;
            Main.main.threedview.ui.BusyWindow.StartTimer();
            System.Windows.Forms.Application.DoEvents();
        }

        private void DisableBusyWindow()
        {
            Main.main.threedview.ui.BusyWindow.AbortTask -= OnUIAbort;
            Main.main.threedview.ui.BusyWindow.Visibility = System.Windows.Visibility.Hidden;
        }

        public void OnProcessUpdate(int rate)
        {
            if (Main.main.threedview.ui.BusyWindow.Visibility == System.Windows.Visibility.Visible)
            {
                Main.main.threedview.ui.BusyWindow.busyProgressbar.Value = rate; 
            }
            Application.DoEvents();
        }

        public void OnProcessUpdate3wsLoadStageLoadStl(int rate)
        {
            if (Main.main.threedview.ui.BusyWindow.Visibility == System.Windows.Visibility.Visible)
            {
                Main.main.threedview.ui.BusyWindow.busyProgressbar.Value = rate / 2; 
            }
            Application.DoEvents();
        }

        public void OnProcessUpdateSaveStageMerge(double value)
        {
            if (Main.main.threedview.ui.BusyWindow.Visibility == System.Windows.Visibility.Visible &&
                Main.main.threedview.ui.BusyWindow.increment != 0.0)
            {
                if (Main.main.threedview.ui.BusyWindow.busyProgressbar.Value < Main.main.threedview.ui.BusyWindow.firstStagePercent)
                {
                    Main.main.threedview.ui.BusyWindow.busyProgressbar.Value = ((value + mergedCount * 100) / mergeCountTotal) * Main.main.threedview.ui.BusyWindow.firstStagePercent / 100;
                }
                Application.DoEvents();
            }
        }

        public void OnProcessUpdateSaveStage2nd(int rate)
        {
            if (Main.main.threedview.ui.BusyWindow.Visibility == System.Windows.Visibility.Visible &&
                Main.main.threedview.ui.BusyWindow.increment != 0.0)
            {
                Main.main.threedview.ui.BusyWindow.busyProgressbar.Value =
                        (rate) * (100.0 - Main.main.threedview.ui.BusyWindow.firstStagePercent)
                        + Main.main.threedview.ui.BusyWindow.firstStagePercent;
            }
            Application.DoEvents();
        }

        public void OnUIAbort(object sender, EventArgs e)
        {
            saveTaskAbort = true;

            if (AbortTask != null)
                AbortTask(sender, e);
        }
        #endregion

    }
}
