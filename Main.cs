/*
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
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using View3D.model;
using View3D.model.geom;
using View3D.view;
using View3D.view.utils;

namespace View3D
{
    public delegate void languageChangedEvent();

    public partial class Main : Form
    {
        public static uint checkJobNum = 5;             // freq. for prompting user to clean floater
        public static byte[] DisplayByte;

        private bool IsStlimport = false;
        private bool Write3wnType = false;
        private int advanced_mode = 0;
        private static bool check_loadfile = false;
        private bool have3dObject = false;
        public ObjectInformation objectInformation = new ObjectInformation();
        public string offlinefilename;
        public event languageChangedEvent languageChanged;
        public static Main main;
        public static FormPrinterSettings printerSettings;
        public static PrinterModel printerModel;
        public static ThreeDSettings threeDSettings;
        private string basicTitle = "";
        private string machinemodel = "";
        private string file_version_number = "";
        private static string tempfile = "";
        public static bool IsMono = Type.GetType("Mono.Runtime") != null;
        private static int thumbnailWidth = 432;
        private static int thumbnailHeigth = 432;

        public ThreeDControl threedview = null;
        public ThreeDView jobPreview = null;
        public ThreeDView printPreview = null;
        public GCodeVisual jobVisual = new GCodeVisual();
        public GCodeVisual printVisual = null;
        public STLComposer objectPlacement = null;
        public volatile GCodeVisual newVisual = null;
        private volatile bool jobPreviewThreadFinished = true;
        public volatile Thread previewThread = null;
        public RegMemory.FilesHistory fileHistory = new RegMemory.FilesHistory("fileHistory", 2);
        public int refreshCounter = 0;
        bool recalcJobPreview = false;
        List<GCodeShort> previewArray0, previewArray1, previewArray2;
        public Trans trans = null;
        public double gcodePrintingTime = 0;
        private int offleinconnect = 0;
        public DateTime Fw_updata;
        private int fw_upgrade = 0;
        public ObjectInformation gObjectInformation = new ObjectInformation();
        public DateTime gStartTime;

        public string PP120xPHWID = "";
        public string MACAddress_LAN = "";
        public string MACAddress_WIFI = "";
        public static bool IsDirectPrint = false;
        public static bool FormattingTimeOut = false;
        public static bool IsPerformanceMode = false;

        public class JobUpdater
        {
            GCodeVisual visual = null;
            // This method will be called when the thread is started.
            public void DoWork()
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                visual = new GCodeVisual();
                visual.showSelection = true;
                visual.parseGCodeShortArray(Main.main.previewArray0, true, 0);
                visual.parseGCodeShortArray(Main.main.previewArray1, false, 1);
                visual.parseGCodeShortArray(Main.main.previewArray2, false, 2);
                Main.main.previewArray0 = Main.main.previewArray1 = Main.main.previewArray2 = null;
                visual.Reduce();
                if (check_loadfile == true)
                {
                    Main.main.gcodePrintingTime = visual.ana.printingTime;
                    string returnValue;
                    string fileLow = tempfile.ToLower();

                    //if (fileLow.EndsWith(".stl") || fileLow.EndsWith(".obj"))
                    if (fileLow.EndsWith(".stl"))
                    { 
                    }
                    else if (fileLow.EndsWith(".3w") || fileLow.EndsWith(".3wn"))
                    {
                    }
                    check_loadfile = false;
                }
                //visual.stats();
                Main.main.newVisual = visual;
                Main.main.jobPreviewThreadFinished = true;
                Main.main.previewThread = null;
                sw.Stop();
            }
        }

        //From Managed.Windows.Forms/XplatUI

        public float dpiX, dpiY;

        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                /* vic add for invisible infoeditor */
                if (advanced_mode == 0)
                {
                    splitInfoEdit.SplitterDistance = main.Width;
                    splitInfoEdit.IsSplitterFixed = true;
                }

                var mainScreen = Screen.PrimaryScreen;
                this.Width = mainScreen.WorkingArea.Width - 100;
                this.Height = mainScreen.WorkingArea.Height - 100;
                CenterToScreen();

                //everything done.  Now look at command line
                ProcessCommandLine();
                gStartTime = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public Main(string args)
        {
            CheckIfIsWindows10();

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US", false);

            Graphics graphics = this.CreateGraphics();
            dpiX = graphics.DpiX;
            dpiY = graphics.DpiY;

            main = this;

            trans = new Trans(Application.StartupPath + Path.DirectorySeparatorChar + "data" + Path.DirectorySeparatorChar + "translations");
            printerSettings = new FormPrinterSettings();
            printerModel = new PrinterModel();
            threeDSettings = new ThreeDSettings();

            InitializeComponent();

            tdSettings.DataSource = threeDSettings;
            if (WindowState == FormWindowState.Maximized)
                Application.DoEvents();
            splitLog.SplitterDistance = RegMemory.GetInt("logSplitterDistance", splitLog.SplitterDistance);

            splitLog.Panel2Collapsed = true;
            objectPlacement = new STLComposer();
            objectPlacement.Dock = DockStyle.Fill;
            tabModel.Controls.Add(objectPlacement);

            threedview = new ThreeDControl();
            threedview.Dock = DockStyle.Fill;

            // milton
            threedview.SetComp(objectPlacement);
            splitInfoEdit.Panel1.Controls.Add(threedview);

            printPreview = new ThreeDView();
            printPreview.SetEditor(false);
            printPreview.autoupdateable = true;
            printVisual = new GCodeVisual();
            printVisual.liveView = true;
            printPreview.models.AddLast(printVisual);
            basicTitle = Text;
            jobPreview = new ThreeDView();
            jobPreview.SetEditor(false);
            jobPreview.models.AddLast(jobVisual);

            // Modify UI font size
            Main.main.threedview.ui.modifyUITextSize();
            Main.main.threedview.ui.UI_view.modifyViewTextSize();

            assign3DView();

            if (IsMono)
            {
                toolStrip.Height = 56;
            }

            string titleAdd = "";

            if (titleAdd.Length > 0)
            {
                int p = basicTitle.IndexOf(' ');
                basicTitle = basicTitle.Substring(0, p) + titleAdd + basicTitle.Substring(p);
                Text = basicTitle;
            }

            UpdateToolbarSize();
            // Add languages
            foreach (Translation t in trans.translations.Values)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(t.language, null, languageSelected);
                item.Tag = t;
                languageToolStripMenuItem.DropDownItems.Add(item);
            }
            languageChanged += translate;
            translate();

            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);
 
            gObjectInformation.Owner = this;
        }


        void ProcessCommandLine()
        {
            string[] args = Environment.GetCommandLineArgs();

            if (args.Length < 1) return;

            //for now, just check the last arg and load it. Could add other inputs/commands later.
            for (int i = 1; i < args.Length; i++)
            {
                string file = args[i];

                if (File.Exists(file))
                {
                    LoadGCodeOrSTL(file);
                }
            }
        }

        void Form1_DragEnter(object sender, DragEventArgs e)
        {
            bool tCanSupport = true;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string file in files)
                {
                    if (
                        file.ToUpper().EndsWith(".STL") == false
                        && file.ToUpper().EndsWith(".3WN") == false
                        && file.ToUpper().EndsWith(".3WS") == false
                        && file.ToUpper().EndsWith(".3W") == false
                        //&& file.ToUpper().EndsWith(".OBJ") == false 
                        && file.ToUpper().EndsWith(".NKG") == false
                        )
                    {
                        tCanSupport = false;
                        break;
                    }
                }
                if (tCanSupport)
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
        }

        void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string file in files) LoadGCodeOrSTL(file);
        }

        public void translate()
        {
            languageToolStripMenuItem.Text = Trans.T("M_LANGUAGE");
            printerSettingsToolStripMenuItem.Text = Trans.T("M_PRINTER_SETTINGS");
            timeperiodMenuItem.Text = Trans.T("M_TIMEPERIOD");
            buildAverageOverMenuItem.Text = Trans.T("M_BUILD_AVERAGE_OVER");
            secondsToolStripMenuItem.Text = Trans.T("M_30_SECONDS");
            minuteToolStripMenuItem.Text = Trans.T("M_1_MINUTE");
            minuteToolStripMenuItem1.Text = Trans.T("M_1_MINUTE");
            minutesToolStripMenuItem.Text = Trans.T("M_2_MINUTES");
            minutesToolStripMenuItem1.Text = Trans.T("M_5_MINUTES");
            minutes5ToolStripMenuItem.Text = Trans.T("M_5_MINUTES");
            minutes10ToolStripMenuItem.Text = Trans.T("M_10_MINUTES");
            minutes15ToolStripMenuItem.Text = Trans.T("M_15_MINUTES");
            minutes30ToolStripMenuItem.Text = Trans.T("M_30_MINUTES");
            minutes60ToolStripMenuItem.Text = Trans.T("M_60_MINUTES");
            tabPage3DView.Text = Trans.T("TAB_3D_VIEW");
            tabModel.Text = Trans.T("TAB_OBJECT_PLACEMENT");
            toolStripSaveJob.Text = Trans.T("M_SAVE_JOB");
            toolStripSaveJob.ToolTipText = Trans.T("M_SAVE_JOB");
            openGCode.Title = Trans.T("W_IMPORT_FILE"); // Import G-Code, STL
            saveJobDialog.Title = Trans.T("W_SAVE_FILE_3WN"); //Save G-Code      
            foreach (ToolStripMenuItem item in languageToolStripMenuItem.DropDownItems)
            {
                item.Checked = item.Tag == trans.active;
            }
        }

        public void UpdateToolbarSize()
        {
            foreach (ToolStripItem it in toolStrip.Items)
            {
                //it.DisplayStyle = ToolStripItemDisplayStyle.Text;
                it.DisplayStyle = ToolStripItemDisplayStyle.Image;
            }
        }

        private void languageSelected(object sender, EventArgs e)
        {
            ToolStripItem it = (ToolStripItem)sender;
            trans.selectLanguage((Translation)it.Tag);
            if (languageChanged != null)
                languageChanged();
        }

        public void FormToFront(Form f)
        {
            // Make this form the active form and make it TopMost
            //f.ShowInTaskbar = false;
            /*f.TopMost = true;
            f.Focus();*/
            f.BringToFront();
            // f.TopMost = false;
        }

        public void toolGCodeLoad_Click(object sender, EventArgs e)
        {
            if (openGCode.ShowDialog() == DialogResult.OK)
            {
                Main.main.threedview.Enabled = false;
                //editor.setContent(0, "");
                LoadGCodeOrSTL(openGCode.FileName);
                Main.main.threedview.Enabled = true;

                //Modified by RCGREY for STL Slice Previewer
                Main.main.threedview.setMinMaxClippingLayer();
            }
        }

        // Called when importing a STL file.
        public void LoadGCodeOrSTL(string file)
        {
            toolStripSaveJob.Enabled = true;

            if (!File.Exists(file)) return;

            FileInfo f = new FileInfo(file);
            Main.threeDSettings.filament.BackColor = System.Drawing.Color.Chocolate;

            tempfile = file;
            offlinefilename = f.Name;
            string fileLow = file.ToLower();

            if (fileLow.EndsWith(".stl"))
            {
                if (File.Exists("composition.3wn"))
                    System.IO.File.Delete("composition.3wn");
                if (File.Exists("SaveFile.3wn"))
                    System.IO.File.Delete("SaveFile.3wn");
        
                IsStlimport = true;
                have3dObject = true;

                tab.SelectTab(tabModel);
                objectPlacement.openAndAddObject(file);
            }
        }

        private void printerSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printerSettings.Show(this);
            FormToFront(printerSettings);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            RegMemory.StoreWindowPos("mainWindow", this, true, true);

            if (previewThread != null)
                previewThread.Join();

            //Edward 20140515 Add terminal XYZWare
            Environment.Exit(Environment.ExitCode);
        }

        private void JobPreview()
        {
            recalcJobPreview = true;
        }

        public void Update3D()
        {
            if (threedview != null)
                threedview.UpdateChanges();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (fw_upgrade != 0)
            {
                DateTime NextTime = DateTime.Now;
                TimeSpan fw_ts = NextTime - Main.main.Fw_updata;

                if (fw_ts.TotalSeconds > 100 && fw_ts.TotalSeconds < 1000000)
                {
                    Main.main.Fw_updata = DateTime.Now;
                    fw_upgrade = 0;
                    try
                    {
                        MessageBox.Show(Trans.T("L_FW_UPGRAD_SUCCESS"), Trans.T("L_FW_UPGRAD_HEAD"));

                    }
                    catch { }
                }
            }
            if (newVisual != null)
            {
                jobPreview.models.RemoveLast();
                jobVisual.Clear();
                jobVisual = newVisual;
                jobPreview.models.AddLast(jobVisual);
                threedview.UpdateChanges();
                newVisual = null;
            }
            if (recalcJobPreview && jobPreviewThreadFinished)
            {
                previewArray0 = new List<GCodeShort>();
                previewArray1 = new List<GCodeShort>();
                previewArray2 = new List<GCodeShort>();
                recalcJobPreview = false;
                jobPreviewThreadFinished = false;

                JobUpdater workerObject = new JobUpdater();
                previewThread = new Thread(workerObject.DoWork);
                previewThread.Start();
            }
            if (refreshCounter > 0)
            {
                if (--refreshCounter == 0)
                {
                    Invalidate();
                }
            }
            if (Main.main.threedview != null && gStartTime != null)
            {
                gStartTime = DateTime.Now;
            }
        }

        public void assign3DView()
        {
            if (tab == null) return;
            switch (tab.SelectedIndex)
            {
                case 0:
                case 1:
                    threedview.SetView(objectPlacement.cont);
                    break;
                case 2:
                    threedview.SetView(jobPreview);
                    threedview.ui.info_toggleButton.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 3:
                    threedview.SetView(printPreview);
                    break;
            }
        }

        private void tab_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Vic add for change save stl behavior
            if (tab.SelectedTab == tabModel)
                toolStripSaveJob.Enabled = true;
            else
                toolStripSaveJob.Enabled = false;


            if (tab.SelectedTab == tabModel)
            {
                tabControlView.SelectedIndex = 0;
            }
            assign3DView();
        }

        private void Main_Resize(object sender, EventArgs e)
        {

        }

        private void Main_Shown(object sender, EventArgs e)
        {
        }

        public void toolStripSaveJob_Click(object sender, EventArgs e)
        {
            toolStripSaveJobFun();
        }

        private void Main_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (tabControlView.SelectedIndex == 0)
                {
                    threedview.ThreeDControl_KeyDown(sender, e);
                }
            }
            catch { }
        }

        static bool firstSizeCall = true;

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            if (firstSizeCall)
            {
                firstSizeCall = false;
            }
        }

        private void tdSettings_DataMemberChanged(object sender, EventArgs e)
        {
        }

        private void tdSettings_CurrentChanged(object sender, EventArgs e)
        {
        }

        public void toolStripButton_helpInfo_Click(object sender, EventArgs e)
        {
        }

        private void Main_Move(object sender, EventArgs e)
        {
            Point location = threedview.gl.PointToScreen(Point.Empty);
            threedview.ui.Left = (double)location.X / dpiX * 96;
            threedview.ui.Top = (double)location.Y / dpiY * 96;
        }

        public bool toolStripSaveJobFun()
        {
            objectPlacement.saveSTL.FileName = "";


            if (objectPlacement.saveSTL.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Main.main.threedview.ui.BusyWindow.labelBusyMessage.Text = Trans.T("B_SAVING");
                    Main.main.threedview.ui.BusyWindow.Visibility = System.Windows.Visibility.Visible;
                    Main.main.threedview.ui.BusyWindow.buttonCancel.Visibility = System.Windows.Visibility.Visible;
                    Main.main.threedview.ui.BusyWindow.busyProgressbar.IsIndeterminate = false;
                    Main.main.threedview.ui.BusyWindow.busyProgressbar.Value = 0;
                    Main.main.threedview.ui.BusyWindow.busyProgressbar.Maximum = 100;
                    Main.main.threedview.ui.BusyWindow.StartTimer();

                    objectPlacement.saveComposition(objectPlacement.saveSTL.FileName);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void DoCommand(PrintModel stl)
        {
            try
            {
                float[] pos = new float[9];
                pos[0] = stl.Position.x; pos[1] = stl.Position.y; pos[2] = stl.Position.z;
                pos[3] = stl.Rotation.x; pos[4] = stl.Rotation.y; pos[5] = stl.Rotation.z;
                pos[6] = stl.Scale.x; pos[7] = stl.Scale.y; pos[8] = stl.Scale.z;
            }
            catch { }
        }

        public Color GetColorSetting(Submesh.MeshColor color, Color frontBackColor)
        {
            switch (color)
            {
                case Submesh.MeshColor.FrontBack:
                    return frontBackColor;
                case Submesh.MeshColor.Back:
                    return threeDSettings.insideFaces.BackColor;
                case Submesh.MeshColor.ErrorFace:
                    return threeDSettings.errorModel.BackColor;
                case Submesh.MeshColor.ErrorEdge:
                    return threeDSettings.errorModelEdge.BackColor;
                case Submesh.MeshColor.OutSide:
                    return threeDSettings.outsidePrintbed.BackColor;
                case Submesh.MeshColor.EdgeLoop:
                    return threeDSettings.edges.BackColor;
                case Submesh.MeshColor.CutEdge:
                    return threeDSettings.cutFaces.BackColor;
                case Submesh.MeshColor.Normal:
                    return Color.Blue;
                case Submesh.MeshColor.Edge:
                    return threeDSettings.edges.BackColor;
                case Submesh.MeshColor.TransBlue:
                    return Color.FromArgb(128, 0, 0, 255);
                case Submesh.MeshColor.OverhangLv1: // pink
                    return Color.FromArgb(255, 255, 140, 140);
                case Submesh.MeshColor.OverhangLv2: // light pink
                    return Color.FromArgb(255, 255, 190, 190);
                case Submesh.MeshColor.OverhangLv3: // light pink white
                    return Color.FromArgb(255, 250, 215, 205);
                case Submesh.MeshColor.ConeSupport:
                    return Color.FromArgb(255, 128, 0); //orange
                case Submesh.MeshColor.TreeSymbol:
                    return Color.FromArgb(255, 255, 102); //yellow
                case Submesh.MeshColor.TreeMesh:
                    return Color.FromArgb(0, 255, 128); //light green
                case Submesh.MeshColor.TreeError:
                    return Color.FromArgb(255, 102, 102); //red
                case Submesh.MeshColor.TreeSlect:
                    return Color.FromArgb(102, 255, 255); //light blue
                case Submesh.MeshColor.TreeTest:
                    return Color.Transparent; //yellow

                default:
                    return Color.White;
            }
        }
        public static bool IsOpenModeSelected = false;


        public bool IsWindows10 = false;
        public void CheckIfIsWindows10()
        {
            var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");

            string productName = (string)reg.GetValue("ProductName");

            if (productName.StartsWith("Windows 10"))
            {
                IsWindows10 = true;
            }
            else
            {
                IsWindows10 = false;
            }
        }
    }
}