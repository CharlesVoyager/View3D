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
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Interop;
using View3D.model;
using View3D.model.geom;
using View3D.view.utils;
using View3D.ModelObjectTool;


namespace View3D.view
{
    public partial class ThreeDControl : UserControl
    {
        // Threshold for layer height that can selected for user
        private static double MAX_PRINT = 100000000;
        private static double SCALE_25MICRON = 10 * 0.025;
        private static double SCALE_50MICRON = 10 * 0.05;
        private RHVector3 MouseDownPosition = new RHVector3(-1, -1, -1);
        bool isTouchTrunk = false;
        PrintModel TouchModel = null;

        public ThreeDCamera cam;
        float bedRadius;
        FormPrinterSettings ps = Main.printerSettings;
        bool loaded = false;
        float xDown, yDown;
        float xPos, yPos;
        float speedX, speedY;
        double normX = 0, normY = 0;
        float lastX, lastY;
        Stopwatch sw = new Stopwatch();
        Stopwatch fpsTimer = new Stopwatch();
        int mode = 0;
        int slowCounter = 0; // Indicates slow framerates
        uint timeCall = 0;
        public float zoom = 1.0f;
        public Matrix4 lookAt, persp, modelView;
        public float nearDist, farDist, aspectRatio, nearHeight, midHeight;
        public static double GLversion;
        int keyX = -1;
        int keyY = -1;

        public ThreeDView view = null;
        public STLComposer stlComp = null;
        public bool isShowWPF = true, isPreShowWPF = false;

        //Added by RCGREY for STL Slice Previewer
        public double clippingLayerCur = 999.0;
        public double clippingLayerMax = 999.0;
        public double clippingLayerMin = -1.0;
        public double setclipLayerHeight = 0.1;
        public bool clipDownward;
        public bool clipviewEnabled;
        public bool viewSilhouette;
        public bool enableStencilbufClear = true;
        public int disableStencilMask = 0;

        //Added by RC Grey for tree and cone support generation
        private static bool symbolDetected = false;
        public bool selectTrunkForBranchAdd = false;
        private List<RHVector3> branchPointDirRef = new List<RHVector3>();
        public View3D.view.wpf.UI ui = null;

        public ThreeDControl()
        {
            InitializeComponent();

            /* vic add start*/
            ui = new View3D.view.wpf.UI();
            WindowInteropHelper helper = new WindowInteropHelper(ui);
            helper.Owner = this.Handle;

            System.Windows.Forms.Integration.ElementHost.EnableModelessKeyboardInterop(ui);

            ui.ChangeLoginUI(true);

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            /* vic add end*/

            cam = new ThreeDCamera(this);
            SetCameraDefaults();
            cam.OrientIsometric();
            timer.Start();
            translate();
            Main.main.languageChanged += translate;
            tooltips();
        }

        private void tooltips()
        {
            // Create the ToolTip and associate with the Form container.
            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;
        }
        private void translate()
        {
            toolMoveViewpoint.ToolTipText = Trans.T("L_MOVE_VIEWPOINT");
            toolStripClear.ToolTipText = Trans.T("T_CLEAR_OBJECTS");
            landObjectToolStripMenuItem.Text = Trans.T("B_LAND");
            resetObjectToolStripMenuItem.Text = Trans.T("B_RESET");
            removeObjectToolStripMenuItem.Text = Trans.T("B_REMOVE");
            objectInfomationToolStripMenuItem.Text = Trans.T("B_INFO");
            newWorkspacetoolStripMenuItem.Text = Trans.T("B_NEW_WORKSPACE");
            mminchToolStripMenuItem.Text = Trans.T("B_SCALE_UP") + " (" + Trans.T("L_MM") + ">" + Trans.T("L_INCH") + ")";
            inchmmToolStripMenuItem.Text = Trans.T("B_SCALE_DOWN") + " (" + Trans.T("L_INCH") + ">" + Trans.T("L_MM") + ")";
            cloneToolStripMenuItem.Text = Trans.T("B_CLONE_OBJECT");
        }

        public void SetComp(STLComposer comp)
        {
            this.stlComp = comp;
        }

        public void SetView(ThreeDView view)
        {
            this.view = view;
            toolStripClear.Visible = view.autoupdateable;
            toolStripClear.Enabled = view.objectsSelected;
            UpdateChanges();
        }

        public void MakeVisible(bool vis)
        {
            if (vis)
            {
                if (!Controls.Contains(gl))
                    Controls.Add(gl);
            }
            else
            {
                if (Controls.Contains(gl))
                    Controls.Remove(gl);
            }
            gl.Visible = vis;
        }

        public void SetObjectSelected(bool sel)
        {
            if (Main.main.objectPlacement.listObjects.SelectedItems.Count == 1)
            {
                ui.setbuttonVisable(sel);
            }
            else
            {
                ui.setbuttonVisable(false);
            }
            toolStripClear.Enabled = sel;
            view.objectsSelected = sel;
        }

        #region STL Slice Previewer
        public void setMinMaxClippingLayer()
        {
            double sliderval = 999;
            double zMin = Main.printerSettings.Height;
            double layerMultiplier = 1 / setclipLayerHeight;
            bool checkDirection = clipDownward;

            updateMaximumClipLevel();

            clipviewEnabled = false;
            ui.LayerSlider.Visibility = System.Windows.Visibility.Hidden;
            ui.LayerNumber.Visibility = System.Windows.Visibility.Hidden;          

            if (sliderval < clippingLayerMax && sliderval > clippingLayerMin)
            {
                ui.LayerSlider.Value = (int)sliderval;
                ui.LayerNumber.Text = ui.LayerSlider.Value.ToString();
                UpdateLayerNumPosition();
            }

            ui.LayerSlider.Value = ui.LayerSlider.Minimum;
            ui.LayerNumber.Text = ui.LayerSlider.Value.ToString();
            UpdateLayerNumPosition();

            if (ui.LayerSlider.Value <= 0)
                ui.LayerNumber.Visibility = System.Windows.Visibility.Hidden;

            clippingLayerCur = (double)ui.LayerSlider.Value;
        }

        public void updateMaximumClipLevel()
        {
            double zMax = 0;
            double zMin = Main.printerSettings.Height;
            double layerMultiplier = 1 / setclipLayerHeight;
            //Debug.WriteLine("updateMaximumClipLevel");
            if (clipviewEnabled)
            {
                zMax = Math.Round(zMax, 2, MidpointRounding.AwayFromZero);
                clippingLayerMax = (zMax * layerMultiplier); // layer height
                clippingLayerMax = Math.Round(clippingLayerMax, 0, MidpointRounding.AwayFromZero);

                zMin = Math.Round(zMin, 2, MidpointRounding.AwayFromZero);
                clippingLayerMin = (zMin * layerMultiplier); // layer height
                clippingLayerMin = Math.Round(clippingLayerMin, 0, MidpointRounding.AwayFromZero);

                if (clipDownward)
                {
                    //Debug.WriteLine("Downward Direction");
                    ui.LayerSlider.Maximum = (int) (clippingLayerMax - clippingLayerMin);
                    ui.LayerSlider.Minimum = 0;
                    ui.LayerSlider.CoerceValue(System.Windows.Controls.Slider.ValueProperty);

        }
                else
                {
                    //Debug.WriteLine("Upward Direction");
                    ui.LayerSlider.Minimum = 0;
                    ui.LayerSlider.Maximum = (int) (clippingLayerMax - clippingLayerMin);
                    ui.LayerSlider.CoerceValue(System.Windows.Controls.Slider.ValueProperty);
                }
            }
        }

        //Add layer number text box display
        public void UpdateLayerNumPosition()
        {
            if (ui.LayerNumber.Visibility == System.Windows.Visibility.Hidden) return;

            double yvalratio = ui.LayerSlider.Value / (ui.LayerSlider.Maximum - ui.LayerSlider.Minimum);
            double absolutePos = ((ui.Height - ui.LayerSlider.RenderSize.Height) / 2) +
                (ui.LayerSlider.RenderSize.Height * yvalratio) - 20; // 20 is offset to texbox size;
            // Fixed a bug caused by yvalratio = NaN...
            if (Double.IsNaN(yvalratio))
            { yvalratio = 0; }
            if (Double.IsNaN(absolutePos))
            { absolutePos = 0; }
            ui.LayerNumber.Margin = new System.Windows.Thickness(0, 0, 70, absolutePos);
        }
        #endregion

        public void UpdateChanges()
        {
            gl.Invalidate();
        }
        public void SetupViewport()
        {
            try
            {
                int w = gl.Width;
                int h = gl.Height;

                bedRadius = (float)(1.5 * Math.Sqrt((ps.PrintAreaDepth * ps.PrintAreaDepth + ps.PrintAreaHeight * ps.PrintAreaHeight + ps.PrintAreaWidth * ps.PrintAreaWidth) * 0.25));
                GL.Viewport(0, 0, w, h);
                
                GL.MatrixMode(MatrixMode.Projection);
                //GL.LoadIdentity();

                if (cam == null) return;

                float angle;
                Vector3 camPos = cam.CameraPosition;
                float dist = (float)cam.distance;
                nearDist = Math.Max(1, dist - bedRadius);
                farDist = Math.Max(bedRadius * 2, dist + bedRadius);
                midHeight = 2.0f * (float)Math.Tan(cam.angle) * dist;
                nearHeight = 2.0f * (float)Math.Tan(cam.angle) * nearDist;
                aspectRatio = (float)w / (float)h;
                angle = (float)(cam.angle) * 2.0f;
                persp = Matrix4.CreatePerspectiveFieldOfView((float)(angle), aspectRatio, nearDist, farDist);
                GL.LoadMatrix(ref persp);
                // GL.Ortho(0, w, 0, h, -1, 1); // Bottom-left corner pixel has coordinate (0, 0)
            }
            catch { }
        }

        public OpenTK.Graphics.Color4 convertColor(Color col)
        {
            return new OpenTK.Graphics.Color4(col.R, col.G, col.B, col.A);
        }

        private void AddLights()
        {
            //Enable lighting
            GL.Light(LightName.Light0, LightParameter.Ambient, new float[] { 0.2f, 0.2f, 0.2f, 1f });
            GL.Light(LightName.Light0, LightParameter.Diffuse, new float[] { 0, 0, 0, 0 });
            GL.Light(LightName.Light0, LightParameter.Specular, new float[] { 0, 0, 0, 0 });
            GL.Enable(EnableCap.Light0);
            if (Main.threeDSettings.enableLight1.Checked)
            {
                GL.Light(LightName.Light1, LightParameter.Ambient, Main.threeDSettings.Ambient1());
                GL.Light(LightName.Light1, LightParameter.Diffuse, Main.threeDSettings.Diffuse1());
                GL.Light(LightName.Light1, LightParameter.Specular, Main.threeDSettings.Specular1());
                GL.Light(LightName.Light1, LightParameter.Position, Main.threeDSettings.Dir1());
                //  GL.Light(LightName.Light1, LightParameter.SpotExponent, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
                GL.Enable(EnableCap.Light1);
            }
            else GL.Disable(EnableCap.Light1);
            if (Main.threeDSettings.enableLight2.Checked)
            {
                GL.Light(LightName.Light2, LightParameter.Ambient, Main.threeDSettings.Ambient2());
                GL.Light(LightName.Light2, LightParameter.Diffuse, Main.threeDSettings.Diffuse2());
                GL.Light(LightName.Light2, LightParameter.Specular, Main.threeDSettings.Specular2());
                GL.Light(LightName.Light2, LightParameter.Position, Main.threeDSettings.Dir2());
                /*  GL.Light(LightName.Light2, LightParameter.Diffuse, new float[] { 0.7f, 0.7f, 0.7f, 1f });
                  GL.Light(LightName.Light2, LightParameter.Specular, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
                  GL.Light(LightName.Light2, LightParameter.Position, (new Vector4(100f, 200f, 300f, 0)));*/
                GL.Light(LightName.Light2, LightParameter.SpotExponent, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
                GL.Enable(EnableCap.Light2);
            }
            else GL.Disable(EnableCap.Light2);
            if (Main.threeDSettings.enableLight3.Checked)
            {
                GL.Light(LightName.Light3, LightParameter.Ambient, Main.threeDSettings.Ambient3());
                GL.Light(LightName.Light3, LightParameter.Diffuse, Main.threeDSettings.Diffuse3());
                GL.Light(LightName.Light3, LightParameter.Specular, Main.threeDSettings.Specular3());
                GL.Light(LightName.Light3, LightParameter.Position, Main.threeDSettings.Dir3());
                /*  GL.Light(LightName.Light3, LightParameter.Diffuse, new float[] { 0.8f, 0.8f, 0.8f, 1f });
                  GL.Light(LightName.Light3, LightParameter.Specular, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
                  GL.Light(LightName.Light3, LightParameter.Position, (new Vector4(100f, -200f, 200f, 0)));*/
                GL.Light(LightName.Light3, LightParameter.SpotExponent, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
                GL.Enable(EnableCap.Light3);
            }
            else GL.Disable(EnableCap.Light3);
            if (Main.threeDSettings.enableLight4.Checked)
            {
                GL.Light(LightName.Light4, LightParameter.Ambient, Main.threeDSettings.Ambient4());
                GL.Light(LightName.Light4, LightParameter.Diffuse, Main.threeDSettings.Diffuse4());
                GL.Light(LightName.Light4, LightParameter.Specular, Main.threeDSettings.Specular4());
                GL.Light(LightName.Light4, LightParameter.Position, Main.threeDSettings.Dir4());
                /* GL.Light(LightName.Light4, LightParameter.Diffuse, new float[] { 0.7f, 0.7f, 0.7f, 1f });
                 GL.Light(LightName.Light4, LightParameter.Specular, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
                 GL.Light(LightName.Light4, LightParameter.Position, (new Vector4(170f, -100f, -250f, 0)));*/
                GL.Light(LightName.Light4, LightParameter.SpotExponent, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
                GL.Enable(EnableCap.Light4);
            }
            else GL.Disable(EnableCap.Light4);

            GL.Enable(EnableCap.Lighting);
        }

        private void DetectDrawingMethod()
        {
            // Check drawing method
            int om = Main.threeDSettings.drawMethod;
            switch (Main.threeDSettings.comboDrawMethod.SelectedIndex)
            {
                case 0: // Autodetect;
                    if (Main.threeDSettings.useVBOs && Main.threeDSettings.openGLVersion >= 1.499)
                        Main.threeDSettings.drawMethod = 2;
                    else if (Main.threeDSettings.openGLVersion >= 1.099)
                        Main.threeDSettings.drawMethod = 1;
                    else
                        Main.threeDSettings.drawMethod = 0;
                    break;
                case 1: // VBOs
                    Main.threeDSettings.drawMethod = 2;
                    break;
                case 2: // drawElements
                    Main.threeDSettings.drawMethod = 1;
                    break;
                case 3: // elements
                    Main.threeDSettings.drawMethod = 0;
                    break;
            }
        }

        public static bool reDrawModel = false;

        private void DrawModels(bool showBbox = true)
        {
            GL.Enable(EnableCap.PolygonSmooth);
            GL.Enable(EnableCap.LineSmooth);
            if (Main.main.tab.SelectedIndex > 1)
                GL.Enable(EnableCap.CullFace);
            else
                GL.Disable(EnableCap.CullFace);

            foreach (ThreeDModel model in view.models)
            {
                if (!model.reset)
                    model.curPos = model.curPos2;
                else
                {
                    model.curPos = new Matrix4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
                    model.reset = false;
                }

                Matrix4 curPosT = model.curPos;
                curPosT.Transpose();
                GL.MatrixMode(MatrixMode.Modelview);

                GL.PushMatrix();
                model.AnimationBefore();
                GL.Translate(model.Position.x, model.Position.y, model.Position.z);
                GL.MultMatrix(ref curPosT);
                GL.Scale(model.Scale.x, model.Scale.y, model.Scale.z);
                model.Paint();
                model.AnimationAfter();
                GL.PopMatrix();

                if ((model.Selected)&&(showBbox))
                {
                    GL.PushMatrix();
                    model.AnimationBefore();
                    Color col = Main.threeDSettings.selectionBox.BackColor;
                    GL.Material(MaterialFace.FrontAndBack, MaterialParameter.AmbientAndDiffuse, new OpenTK.Graphics.Color4(0, 0, 0, 255));
                    GL.Material(MaterialFace.Front, MaterialParameter.Emission, new OpenTK.Graphics.Color4(0, 0, 0, 0));
                    GL.Material(MaterialFace.Front, MaterialParameter.Specular, new float[] { 0.0f, 0.0f, 0.0f, 1.0f });
                    GL.Material(
                        MaterialFace.Front,
                        MaterialParameter.Emission,
                        new OpenTK.Graphics.Color4(col.R, col.G, col.B, col.A));
                    GL.Begin(PrimitiveType.Lines);
                    GL.Vertex3(model.xMin, model.yMin, model.zMin);
                    GL.Vertex3(model.xMax, model.yMin, model.zMin);

                    GL.Vertex3(model.xMin, model.yMin, model.zMin);
                    GL.Vertex3(model.xMin, model.yMax, model.zMin);

                    GL.Vertex3(model.xMin, model.yMin, model.zMin);
                    GL.Vertex3(model.xMin, model.yMin, model.zMax);

                    GL.Vertex3(model.xMax, model.yMax, model.zMax);
                    GL.Vertex3(model.xMin, model.yMax, model.zMax);

                    GL.Vertex3(model.xMax, model.yMax, model.zMax);
                    GL.Vertex3(model.xMax, model.yMin, model.zMax);

                    GL.Vertex3(model.xMax, model.yMax, model.zMax);
                    GL.Vertex3(model.xMax, model.yMax, model.zMin);

                    GL.Vertex3(model.xMin, model.yMax, model.zMax);
                    GL.Vertex3(model.xMin, model.yMax, model.zMin);

                    GL.Vertex3(model.xMin, model.yMax, model.zMax);
                    GL.Vertex3(model.xMin, model.yMin, model.zMax);

                    GL.Vertex3(model.xMax, model.yMax, model.zMin);
                    GL.Vertex3(model.xMax, model.yMin, model.zMin);

                    GL.Vertex3(model.xMax, model.yMax, model.zMin);
                    GL.Vertex3(model.xMin, model.yMax, model.zMin);

                    GL.Vertex3(model.xMax, model.yMin, model.zMax);
                    GL.Vertex3(model.xMin, model.yMin, model.zMax);

                    GL.Vertex3(model.xMax, model.yMin, model.zMax);
                    GL.Vertex3(model.xMax, model.yMin, model.zMin);

                    GL.End();
                    model.AnimationAfter();
                    GL.PopMatrix();
                }
            }
        }
 
        private void DrawPrintbedFrame()
        {
            float dx1 = ps.DumpAreaLeft;
            float dx2 = dx1 + ps.DumpAreaWidth;
            float dy1 = ps.DumpAreaFront;
            float dy2 = dy1 + ps.DumpAreaDepth;
            GL.LineWidth(2f);
            GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
            GL.BlendFunc((BlendingFactor)BlendingFactorSrc.SrcAlpha, (BlendingFactor)BlendingFactorDest.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Blend);

            Color doorcol = Color.Red;
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.AmbientAndDiffuse, new OpenTK.Graphics.Color4(0, 0, 0, 180));
            GL.Material(MaterialFace.Front, MaterialParameter.Emission, new OpenTK.Graphics.Color4(0, 0, 0, 0));
            GL.Material(MaterialFace.Front, MaterialParameter.Specular, new float[] { 0.0f, 0.0f, 0.0f, 1.0f });
            GL.Enable(EnableCap.LineSmooth);
            GL.Material(
                MaterialFace.Front,
                MaterialParameter.Emission,
                new OpenTK.Graphics.Color4(doorcol.R, doorcol.G, doorcol.B, doorcol.A));
            {
                GL.Begin(PrimitiveType.LineStrip);
                int pad = 2;
                int tri = 10;
                GL.Vertex3(-pad, ps.PrintAreaDepth + pad, -pad);
                GL.Vertex3(-pad, -pad, -pad);
                GL.Vertex3(ps.PrintAreaWidth + pad, -pad, -pad);
                GL.Vertex3(ps.PrintAreaWidth + pad, ps.PrintAreaDepth + pad, -pad);
                GL.Vertex3(ps.PrintAreaWidth / 2 + tri, ps.PrintAreaDepth + pad, -pad);
                GL.Vertex3(ps.PrintAreaWidth / 2, ps.PrintAreaDepth + pad + tri, -pad);
                GL.Vertex3(ps.PrintAreaWidth / 2 - tri, ps.PrintAreaDepth + pad, -pad);
                GL.Vertex3(-pad, ps.PrintAreaDepth + pad, -pad);
                GL.End();
            }

            GL.LineWidth(1f);
            Color col = Main.threeDSettings.printerFrame.BackColor;
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.AmbientAndDiffuse, new OpenTK.Graphics.Color4(0, 0, 0, 255));
            GL.Material(MaterialFace.Front, MaterialParameter.Emission, new OpenTK.Graphics.Color4(0, 0, 0, 0));
            GL.Material(MaterialFace.Front, MaterialParameter.Specular, new float[] { 0.0f, 0.0f, 0.0f, 1.0f });
            GL.Material(
                MaterialFace.Front,
                MaterialParameter.Emission,
                new OpenTK.Graphics.Color4(col.R, col.G, col.B, col.A));
            GL.Enable(EnableCap.LineSmooth);
            if (Main.threeDSettings.showPrintbed.Checked)
            {
                int i;
                // Draw origin
                GL.Disable(EnableCap.CullFace);
                GL.Begin(PrimitiveType.Triangles);
                GL.Normal3(0, 0, 1);

                GL.End();
                GL.Begin(PrimitiveType.Lines);
                if (ps.printerType < 2)
                {
                    // Original Print cube
                    GL.Vertex3(ps.BedLeft, ps.BedFront, 0);
                    GL.Vertex3(ps.BedLeft, ps.BedFront, ps.PrintAreaHeight);
                    GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront, 0);
                    GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront, ps.PrintAreaHeight);
                    GL.Vertex3(ps.BedLeft, ps.BedFront + ps.PrintAreaDepth, 0);
                    GL.Vertex3(ps.BedLeft, ps.BedFront + ps.PrintAreaDepth, ps.PrintAreaHeight);
                    GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront + ps.PrintAreaDepth, 0);
                    GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront + ps.PrintAreaDepth, ps.PrintAreaHeight);
                    GL.Vertex3(ps.BedLeft, ps.BedFront, ps.PrintAreaHeight);
                    GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront, ps.PrintAreaHeight);
                    GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront, ps.PrintAreaHeight);
                    GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront + ps.PrintAreaDepth, ps.PrintAreaHeight);
                    GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront + ps.PrintAreaDepth, ps.PrintAreaHeight);
                    GL.Vertex3(ps.BedLeft, ps.BedFront + ps.PrintAreaDepth, ps.PrintAreaHeight);
                    GL.Vertex3(ps.BedLeft, ps.BedFront + ps.PrintAreaDepth, ps.PrintAreaHeight);
                    GL.Vertex3(ps.BedLeft, ps.BedFront, ps.PrintAreaHeight);

                    if (ps.printerType == 1)
                    {
                        if (dy1 != 0)
                        {
                            GL.Vertex3(ps.BedLeft + dx1, ps.BedFront + dy1, 0);
                            GL.Vertex3(ps.BedLeft + dx2, ps.BedFront + dy1, 0);
                        }
                        GL.Vertex3(ps.BedLeft + dx2, ps.BedFront + dy1, 0);
                        GL.Vertex3(ps.BedLeft + dx2, ps.BedFront + dy2, 0);
                        GL.Vertex3(ps.BedLeft + dx2, ps.BedFront + dy2, 0);
                        GL.Vertex3(ps.BedLeft + dx1, ps.BedFront + dy2, 0);
                        GL.Vertex3(ps.BedLeft + dx1, ps.BedFront + dy2, 0);
                        GL.Vertex3(ps.BedLeft + dx1, ps.BedFront + dy1, 0);
                    }
                    float dx = 10; // ps.PrintAreaWidth / 20f;
                    float dy = 10; // ps.PrintAreaDepth / 20f;
                    float x, y;
                    for (i = 0; i < 200; i++)
                    {
                        x = (float)i * dx;
                        if (x >= ps.PrintAreaWidth)
                            x = ps.PrintAreaWidth;
                        if (ps.printerType == 1 && x >= dx1 && x <= dx2)
                        {
                            GL.Vertex3(ps.BedLeft + x, ps.BedFront, 0);
                            GL.Vertex3(ps.BedLeft + x, ps.BedFront + dy1, 0);
                            GL.Vertex3(ps.BedLeft + x, ps.BedFront + dy2, 0);
                            GL.Vertex3(ps.BedLeft + x, ps.BedFront + ps.PrintAreaDepth, 0);
                        }
                        else
                        {
                            GL.Vertex3(ps.BedLeft + x, ps.BedFront, 0);
                            GL.Vertex3(ps.BedLeft + x, ps.BedFront + ps.PrintAreaDepth, 0);
                        }
                        if (x >= ps.PrintAreaWidth) break;
                    }
                    for (i = 0; i < 200; i++)
                    {
                        y = (float)i * dy;
                        if (y > ps.PrintAreaDepth)
                            y = ps.PrintAreaDepth;
                        if (ps.printerType == 1 && y >= dy1 && y <= dy2)
                        {
                            GL.Vertex3(ps.BedLeft, ps.BedFront + y, 0);
                            GL.Vertex3(ps.BedLeft + dx1, ps.BedFront + y, 0);
                            GL.Vertex3(ps.BedLeft + dx2, ps.BedFront + y, 0);
                            GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront + y, 0);
                        }
                        else
                        {
                            GL.Vertex3(ps.BedLeft, ps.BedFront + y, 0);
                            GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront + y, 0);
                        }
                        if (y >= ps.PrintAreaDepth)
                            break;
                    }
                }
                GL.End();
                GL.DepthFunc(DepthFunction.Less);
            }
        }

        private void DrawPrintbedBase()
        {
            float dx1 = ps.DumpAreaLeft;
            float dx2 = dx1 + ps.DumpAreaWidth;
            float dy1 = ps.DumpAreaFront;
            float dy2 = dy1 + ps.DumpAreaDepth;

            if (Main.threeDSettings.showPrintbed.Checked)
            {
                GL.Disable(EnableCap.CullFace);
                GL.Enable(EnableCap.Blend);	// Turn Blending On
                GL.BlendFunc((BlendingFactor)BlendingFactorSrc.SrcAlpha, (BlendingFactor)BlendingFactorDest.OneMinusSrcAlpha);
                GL.DepthFunc(DepthFunction.Less);
                //GL.Disable(EnableCap.Lighting);

                // Draw bottom
                Color col = Main.threeDSettings.printerBase.BackColor;
                float[] transblack = new float[] { 0, 0, 0, 0 };
                GL.Material(MaterialFace.FrontAndBack, MaterialParameter.AmbientAndDiffuse, new OpenTK.Graphics.Color4(col.R, col.G, col.B, 130));
                GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, transblack);
                GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Emission, transblack);
                GL.PushMatrix();
                if (cam.phi < Math.PI / 2)
                    GL.Translate(0, 0, -0.04);
                else
                    GL.Translate(0, 0, +0.04);
                if (ps.printerType < 2 || ps.printerType == 3)
                {
                    GL.Begin(PrimitiveType.Quads);
                    GL.Normal3(0, 0, 1);

                    if (ps.printerType == 1)
                    {
                        if (dy1 > 0)
                        {
                            GL.Vertex3(ps.BedLeft, ps.BedFront, 0);
                            GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront, 0);
                            GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront + dy1, 0);
                            GL.Vertex3(ps.BedLeft, ps.BedFront + dy1, 0);
                        }
                        if (dy2 < ps.PrintAreaDepth)
                        {
                            GL.Vertex3(ps.BedLeft, ps.BedFront + dy2, 0);
                            GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront + dy2, 0);
                            GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront + ps.PrintAreaDepth, 0);
                            GL.Vertex3(ps.BedLeft, ps.BedFront + ps.PrintAreaDepth, 0);
                        }
                        if (dx1 > 0)
                        {
                            GL.Vertex3(ps.BedLeft, ps.BedFront + dy1, 0);
                            GL.Vertex3(ps.BedLeft + dx1, ps.BedFront + dy1, 0);
                            GL.Vertex3(ps.BedLeft + dx1, ps.BedFront + dy2, 0);
                            GL.Vertex3(ps.BedLeft, ps.BedFront + dy2, 0);
                        }
                        if (dx2 < ps.PrintAreaWidth)
                        {
                            GL.Vertex3(ps.BedLeft + dx2, ps.BedFront + dy1, 0);
                            GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront + dy1, 0);
                            GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront + dy2, 0);
                            GL.Vertex3(ps.BedLeft + dx2, ps.BedFront + dy2, 0);
                        }
                    }
                    else
                    {
                        GL.Vertex3(ps.BedLeft, ps.BedFront, 0);
                        GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront, 0);
                        GL.Vertex3(ps.BedLeft + ps.PrintAreaWidth, ps.BedFront + ps.PrintAreaDepth, 0);
                        GL.Vertex3(ps.BedLeft + 0, ps.BedFront + ps.PrintAreaDepth, 0);
                    }

                    GL.End();
                }
                GL.PopMatrix();
                GL.Disable(EnableCap.Blend);
            }
        }

        bool oldCut = false;
        int oldPosition = -1;
        int oldInclination = -1;
        int oldAzimuth = -1;
        public bool updateCuts = false;
        public RHVector3 cutPos = new RHVector3(0, 0, 0);
        public RHVector3 cutDirection = new RHVector3(0, 0, 1);
        RHBoundingBox cutBBox = new RHBoundingBox();

        void UpdateCutData()
        {
            updateCuts = true;
            cutBBox.Clear();
            foreach (PrintModel model in Main.main.objectPlacement.ListObjects(false))
            {
                cutBBox.Add(model.BoundingBoxWOSupport);
            }
            oldPosition = Main.main.objectPlacement.cutPositionSlider.Value;
            oldInclination = Main.main.objectPlacement.cutInclinationSlider.Value;
            oldAzimuth = Main.main.objectPlacement.cutAzimuthSlider.Value;
            double inclination = (double)oldInclination * Math.PI / 1800.0;
            double azimuth = (double)oldAzimuth * Math.PI / 1800.0;

            cutDirection.x = Math.Sin(inclination) * Math.Cos(azimuth);
            cutDirection.y = Math.Sin(inclination) * Math.Sin(azimuth);
            cutDirection.z = Math.Cos(inclination);

            RHVector3 center = cutBBox.Center;
            TopoPlane plane = new TopoPlane(cutDirection, center);

            double min = 0, max = 0, dist;

            RHVector3 p = new RHVector3(cutBBox.xMin, cutBBox.yMin, cutBBox.zMin);
            dist = plane.VertexDistance(p);
            max = Math.Max(dist, max);
            min = Math.Min(dist, min);
            p = new RHVector3(cutBBox.xMax, cutBBox.yMin, cutBBox.zMin);
            dist = plane.VertexDistance(p);
            max = Math.Max(dist, max);
            min = Math.Min(dist, min);
            p = new RHVector3(cutBBox.xMin, cutBBox.yMax, cutBBox.zMin);
            dist = plane.VertexDistance(p);
            max = Math.Max(dist, max);
            min = Math.Min(dist, min);
            p = new RHVector3(cutBBox.xMax, cutBBox.yMax, cutBBox.zMin);
            dist = plane.VertexDistance(p);
            max = Math.Max(dist, max);
            min = Math.Min(dist, min);
            p = new RHVector3(cutBBox.xMin, cutBBox.yMin, cutBBox.zMax);
            dist = plane.VertexDistance(p);
            max = Math.Max(dist, max);
            min = Math.Min(dist, min);
            p = new RHVector3(cutBBox.xMax, cutBBox.yMin, cutBBox.zMax);
            dist = plane.VertexDistance(p);
            max = Math.Max(dist, max);
            min = Math.Min(dist, min);
            p = new RHVector3(cutBBox.xMin, cutBBox.yMax, cutBBox.zMax);
            dist = plane.VertexDistance(p);
            max = Math.Max(dist, max);
            min = Math.Min(dist, min);
            p = new RHVector3(cutBBox.xMax, cutBBox.yMax, cutBBox.zMax);
            dist = plane.VertexDistance(p);
            max = Math.Max(dist, max);
            min = Math.Min(dist, min);

            double spos = min + 1.001 * (max - min) * (double)oldPosition / 1000.0;

            cutPos.x = center.x + spos * cutDirection.x;
            cutPos.y = center.y + spos * cutDirection.y;
            cutPos.z = center.z + spos * cutDirection.z;
        }

        public void gl_Paint(object sender, PaintEventArgs e)
        {
            if (view == null) return;
            try
            {
                if (!loaded) return;

                // disable UI icons when user rotate object
                if (isPreShowWPF != isShowWPF)
                {
                    if (isShowWPF)
                    {
                        ui.Show();
                        this.Focus();
                    }
                    else
                    {
                        ui.Hide();
                        this.Focus();
                    }
                }
                isPreShowWPF = isShowWPF;

                Point location = gl.PointToScreen(Point.Empty);
                ui.Left = (double)location.X / dpiX * 96;
                ui.Top = (double)location.Y / dpiY * 96;
                DetectDrawingMethod();
                if (oldCut != Main.main.objectPlacement.checkCutFaces.Checked || 
                    oldPosition != Main.main.objectPlacement.cutPositionSlider.Value || 
                    oldInclination != Main.main.objectPlacement.cutInclinationSlider.Value || 
                    oldAzimuth != Main.main.objectPlacement.cutAzimuthSlider.Value || 
                    updateCuts)
                {
                    UpdateCutData();
                }
                fpsTimer.Reset();
                fpsTimer.Start();
                gl.MakeCurrent();
                GL.Enable(EnableCap.Multisample);
                GL.ClearColor(Main.threeDSettings.backgroundTop.BackColor);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
                
                // Draw gradient background
                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();
                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadIdentity();
                GL.Disable(EnableCap.DepthTest);
                GL.Disable(EnableCap.Lighting);

                GL.Begin(PrimitiveType.Quads);
                GL.Color4(convertColor(Main.threeDSettings.backgroundBottom.BackColor)); // Silver -> CornflowerBlue
                GL.Vertex2(-1.0, -1.0);
                GL.Vertex2(1.0, -1.0);
                GL.Color4(convertColor(Main.threeDSettings.backgroundTop.BackColor));    // White -> WhiteSmoke   
                GL.Vertex2(1.0, 1.0);
                GL.Vertex2(-1.0, 1.0);
                GL.End();

                GL.Enable(EnableCap.DepthTest);
                SetupViewport();
                Vector3 camPos = cam.CameraPosition;
                lookAt = Matrix4.LookAt(camPos.X, camPos.Y, camPos.Z, cam.viewCenter.X, cam.viewCenter.Y, cam.viewCenter.Z, 0, 0, 1.0f);
                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadMatrix(ref lookAt);
                GL.ShadeModel(ShadingModel.Smooth);
                GL.Enable(EnableCap.LineSmooth);
                AddLights();
                //Enable Backfaceculling
                GL.Enable(EnableCap.CullFace);
                GL.Enable(EnableCap.PolygonSmooth);
                //GL.Enable(EnableCap.Blend);
                GL.LineWidth(2f);
                GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
                //GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
                Color col = Main.threeDSettings.printerBase.BackColor;
                //GL.Translate(-ps.BedLeft-ps.PrintAreaWidth * 0.5f,-ps.BedFront -ps.PrintAreaDepth * 0.5f, -0.5f * ps.PrintAreaHeight);
                GL.GetFloat(GetPName.ModelviewMatrix, out modelView);
                GL.Material(
                    MaterialFace.Front,
                    MaterialParameter.Specular,
                    new OpenTK.Graphics.Color4(255, 255, 255, 255));

                    GL.Disable(EnableCap.DepthTest);
                    DrawPrintbedBase();
                    GL.Enable(EnableCap.DepthTest);
                    DrawPrintbedFrame();
                    Main.main.threedview.ui.WarnOverlapArea.Visibility = System.Windows.Visibility.Hidden;

                try
                {
                    DrawModels();
                }
                catch (System.OutOfMemoryException)
                {
                    //Debug.WriteLine("Exception: "+exception + 
                    //" available memory is " + new Microsoft.VisualBasic.Devices.ComputerInfo().AvailablePhysicalMemory.ToString("N0"));
                    if (view.models.Count > 0)
                    {
                        Main.main.objectPlacement.RemoveLastModel();
                        UpdateChanges();
                    }
                    Main.main.threedview.ui.BusyWindow.Visibility = System.Windows.Visibility.Hidden;
                    MessageBox.Show("Error(" + (short)View3D.Protocol.ErrorCode.LOAD_FILE_FAIL + "): " + Trans.T("M_LOAD_FILE_FAIL"));	// Please downsize the model
                    GC.Collect();
                }

                gl.SwapBuffers();
                fpsTimer.Stop();

                double framerate = 1.0 / fpsTimer.Elapsed.TotalSeconds;
                if (framerate < 30)
                {
                    slowCounter++;
                    if (slowCounter >= 10)
                    {
                        slowCounter = 0;
                        foreach (ThreeDModel model in view.models)
                        {
                            model.ReduceQuality();
                        }
                    }
                }
                else if (slowCounter > 0)
                    slowCounter--;
            }
            catch { }
            updateCuts = false;
            oldCut = Main.main.objectPlacement.checkCutFaces.Checked;
        }

        static bool configureSettings = true;
        private void ThreeDControl_Load(object sender, EventArgs e)
        {
            if (configureSettings)
            {
                try
                {
                    string sv = GL.GetString(StringName.Version).Trim();

                    int p = sv.IndexOf(" ");
                    if (p > 0) sv = sv.Substring(0, p);
                    p = sv.IndexOf('.');
                    if (p > 0)
                    {
                        p = sv.IndexOf('.', p + 1);
                        if (p > 0)
                            sv = sv.Substring(0, p);
                        GLversion = Convert.ToDouble(sv, CultureInfo.InvariantCulture);
                    }
                    try
                    {
                        float val = 0;
                        float.TryParse(sv, NumberStyles.Float, GCode.format, out val);
                        Main.threeDSettings.openGLVersion = val;
                    }
                    catch
                    {
                        Main.threeDSettings.openGLVersion = 1.1f;
                    }
                    string extensions = GL.GetString(StringName.Extensions);
                    Main.threeDSettings.useVBOs = false;
                    foreach (string s in extensions.Split(' '))
                    {
                        if (s.Equals("GL_ARB_vertex_buffer_object")/* && Main.threeDSettings.openGLVersion>1.49*/)
                        {
                            Main.threeDSettings.useVBOs = true;
                        }
                    }
                }
                catch { }
                configureSettings = false;
            }
            loaded = true;
            SetupViewport();
        }

        public uint lastDepth = 0;
        public Geom3DLine pickLine = null; // Last pick up line ray
        public Geom3DLine viewLine = null; // Direction of view
        public Geom3DVector pickPoint = new Geom3DVector(0, 0, 0); // Koordinates of last pick

        public void UpdatePickLine(int x, int y)
        {
            if (view == null) return;
            // Intersection on bottom plane

            int window_y = (Height - y) - Height / 2;
            double norm_y = (double)window_y / (double)(Height / 2);
            int window_x = x - Width / 2;
            double norm_x = (double)window_x / (double)(Width / 2);
            float fpy = (float)(nearHeight * 0.5 * norm_y) * 1f;
            float fpx = (float)(nearHeight * 0.5 * aspectRatio * norm_x) * 1f;

            Vector4 frontPointN = new Vector4(0, 0, 0, 1);
            Vector4 dirN = new Vector4(fpx, fpy, -nearDist, 0);
            Matrix4 ntrans;
            Vector3 camPos = cam.CameraPosition;
            ntrans = Matrix4.LookAt(camPos.X, camPos.Y, camPos.Z, cam.viewCenter.X, cam.viewCenter.Y, cam.viewCenter.Z, 0, 0, 1.0f);
            ntrans = Matrix4.Invert(ntrans);
            Vector4 frontPoint = ntrans.Row3;
            Vector4 dirVec = Vector4.Transform(dirN, ntrans);
            pickLine = new Geom3DLine(new Geom3DVector(frontPoint.X / frontPoint.W, frontPoint.Y / frontPoint.W, frontPoint.Z / frontPoint.W),
                new Geom3DVector(dirVec.X, dirVec.Y, dirVec.Z), true);
            pickLine.dir.normalize();
        }

        private ThreeDModel Picktest(int x, int y)
        {
            Vector3 hitP = new Vector3();
            Vector3 near, far;
            Ray ray = RayCasting.GenerateRay(x, y, out near, out far);
            float length = float.MaxValue;
            Vector3[] triangle = new Vector3[3];
            Vector3[] curTri = new Vector3[3];
            ThreeDModel nearestModel = null;
            foreach (PrintModel model in stlComp.models)
            {
                if (RayCasting.RaycastAABB(ray, model))
                {
                    if (RayCasting.RaycastAABB(ray, model))
                    {
                        int id;
                        float output;
                        float[] rayPos = { ray.Position.X, ray.Position.Y, ray.Position.Z };
                        float[] rayNor = { ray.Normal.X, ray.Normal.Y, ray.Normal.Z };
                        ModelMatrix mtx = ModelObjectToolHelper.ToModelMatrix(model.trans);

                        if (ModelObjectToolWrapper.Instance.Tool.RayIntersectTriangle(mtx, model.submesh.glVertices,
                            rayPos, rayNor, out id, out output) == true)
                        {
                            hitP = ray.Position + ray.Normal * output;
                            Line line = new Line(near, hitP);
                            if (line.Length <= length)
                            {
                                length = line.Length;
                                nearestModel = model;
                            }
                        }
                        GC.Collect();
                    }
                }
            }
            return nearestModel;
        }

        float dpiX, dpiY;

        public void gl_Resize(object sender, EventArgs e)
        {
            Graphics graphics = this.CreateGraphics();
            dpiX = graphics.DpiX;
            dpiY = graphics.DpiY;

            ui.Width = gl.Size.Width / dpiX * 96;
            ui.Height = gl.Size.Height / dpiY * 96;

            SetupViewport();
            UpdateLayerNumPosition();
            gl.Invalidate();
        }

        Geom3DPlane movePlane = new Geom3DPlane(new Geom3DVector(0, 0, 0), new Geom3DVector(0, 0, 1)); // Plane where object movement occurs
        Geom3DVector moveStart = new Geom3DVector(0, 0, 0), moveLast = new Geom3DVector(0, 0, 0), movePos = new Geom3DVector(0, 0, 0);

        private void gl_MouseDown(object sender, MouseEventArgs e)
        {
            if (System.Environment.OSVersion.Version.Major <= 5)
                ui.ui_grid.Visibility = System.Windows.Visibility.Hidden;

            ui.view_toggleButton.IsChecked = false;
            ui.move_toggleButton.IsChecked = false;
            ui.resize_toggleButton.IsChecked = false;

            keyX = e.X;
            keyY = e.Y;

            cam.PreparePanZoomRot();
            lastX = xDown = e.X;
            lastY = yDown = e.Y;
            movePlane = new Geom3DPlane(new Geom3DVector(0, 0, 0), new Geom3DVector(0, 0, 1));
            moveStart = moveLast = new Geom3DVector(0, 0, 0);
            UpdatePickLine(e.X, e.Y);
            movePlane.intersectLine(pickLine, moveStart);

            gl.Invalidate();
        }

        private void gl_MouseMove(object sender, MouseEventArgs e)
        {
            Keys k = Control.ModifierKeys;
            if ( k == Keys.Alt )
                return;

            double window_y = (gl.Height - e.Y) - gl.Height / 2;
            normY = window_y * 2.0 / (double)(gl.Height);
            double window_x = e.X - gl.Width / 2;
            normX = window_x * 2.0 / (double)(gl.Width);

            if (e.Button == MouseButtons.None)
            {
                speedX = speedY = 0;
                isShowWPF = true;
                gl.Invalidate();
                return;
            }

            isShowWPF = false;

            xPos = e.X;
            yPos = e.Y;
            UpdatePickLine(e.X, e.Y);
            movePos = new Geom3DVector(0, 0, 0);
            movePlane.intersectLine(pickLine, movePos);
            float d = Math.Min(gl.Width, gl.Height) / 3;
            speedX = Math.Max(-1, Math.Min(1, (xPos - xDown) / d));
            speedY = Math.Max(-1, Math.Min(1, (yPos - yDown) / d));
        }

        private void gl_MouseUp(object sender, MouseEventArgs e)
        {
            if (System.Environment.OSVersion.Version.Major <= 5)
                ui.ui_grid.Visibility = System.Windows.Visibility.Visible;

            ThreeDModel sel = null;

            // not in Support Editor mode, check if changing the 'selected' object. Otherwise, not change.
            if (true)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (Main.main.tab.SelectedIndex == 0 && keyX == e.X && keyY == e.Y)
                    {
                        sel = Picktest(e.X, e.Y);
                    }
                    if (sel != null)
                    {
                        movePlane = new Geom3DPlane(pickPoint, new Geom3DVector(0, 0, 1));
                        moveStart = moveLast = new Geom3DVector(pickPoint);
                    }
                    if (sel != null && view.eventObjectMoved != null)
                    {
                        view.eventObjectSelected(sel);
                    }
                    else if (keyX == e.X && keyY == e.Y)
                        Main.main.objectPlacement.listObjects.SelectedItems.Clear();
                }
                if (e.Button == MouseButtons.Right)
                {
                    if (Main.main.tab.SelectedIndex == 0 && keyX == e.X && keyY == e.Y)
                    {
                        sel = Picktest(e.X, e.Y);
                    }
                    if (sel != null)
                    {
                        movePlane = new Geom3DPlane(pickPoint, new Geom3DVector(0, 0, 1));
                        moveStart = moveLast = new Geom3DVector(pickPoint);
                    }
                    if (sel != null && view.eventObjectMoved != null)
                    {
                        view.eventObjectSelected(sel);
                        contextMenu.Show(Cursor.Position);
                    }
                    else if (keyX == e.X && keyY == e.Y)
                    {
                        contextMenu.Show(Cursor.Position);
                    }
                }
            }

            speedX = speedY = 0;
            gl.Invalidate();
        }

        private void gl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta != 0)
            {
                cam.PreparePanZoomRot();
                cam.Zoom(1f - e.Delta / 6000f);
                zoom *= 1f - e.Delta / 2000f;

                if (zoom < 0.002) zoom = 0.002f;
                if (zoom > 5.9) zoom = 5.9f;
                //userPosition.Y += e.Delta;

      
                gl.Invalidate();
            }
        }

        void Application_Idle(object sender, EventArgs e)
        {
            if (!loaded || (speedX == 0 && speedY == 0)) return;
            // no guard needed -- we hooked into the event in Load handler

            sw.Stop(); // we've measured everything since last Idle run
            double milliseconds = sw.Elapsed.TotalMilliseconds;
            sw.Reset(); // reset stopwatch
            sw.Start(); // restart stopwatch
            Keys k = Control.ModifierKeys;
            int emode = mode;
            if (k == Keys.Shift || Control.MouseButtons == MouseButtons.Middle) emode = 2;
            if (k == Keys.Control) emode = 0;
            //if (k == Keys.Alt || Control.MouseButtons == MouseButtons.Right) emode = 4;
            if (k == Keys.Alt) emode = 4;
            if (emode == 0) // Rotate
            {
                if (!symbolDetected)
                {
                    float d = Math.Min(gl.Width, gl.Height) / 3;
                    speedX = (xPos - xDown) / d;
                    speedY = (yPos - yDown) / d;
                    cam.Rotate(-speedX * 0.9, speedY * 0.9);
                    gl.Invalidate();
                }
            }
            else if (emode == 1) // Pan
            {
                speedX = (xPos - xDown) / gl.Width;
                speedY = (yPos - yDown) / gl.Height;
                Vector3 planeVec = Vector3.Subtract(new Vector3(moveStart.x, moveStart.y, moveStart.z), cam.CameraPosition);
                float dot = Vector3.Dot(planeVec, cam.ViewDirection());
                double len = (dot > 0 ? planeVec.Length : -1);
                cam.Pan(speedX * 200, speedY * 200, len);
                gl.Invalidate();
            }
            else if (emode == 2)
            {
                speedX = (xPos - xDown) / gl.Width;
                speedY = (yPos - yDown) / gl.Height;
                Vector3 planeVec = Vector3.Subtract(new Vector3(moveStart.x, moveStart.y, moveStart.z), cam.CameraPosition);
                float dot = Vector3.Dot(planeVec, cam.ViewDirection());
                double len = (dot > 0 ? planeVec.Length : -1);
                cam.Pan(-speedX, -speedY, len);
                gl.Invalidate();
            }
            else if (emode == 3)
            {
                cam.Zoom(1 - speedY / 3f);
                gl.Invalidate();

            }
            else if (emode == 4)
            {
                Geom3DVector diff = movePos.sub(moveLast);
                moveLast = movePos;
                speedX = (xPos - lastX) * 200 * zoom / gl.Width;
                speedY = (yPos - lastY) * 200 * zoom / gl.Height;
                if (view.eventObjectMoved != null)
                {
                    List<PrintModel> selModels = new List<PrintModel>();
                    List<float> prevX = new List<float>();
                    List<float> prevY = new List<float>();
                    foreach (PrintModel stl in stlComp.ListObjects(true))
                    {
                        selModels.Add(stl);
                        prevX.Add(stl.Position.x);
                        prevY.Add(stl.Position.y);
                    }

                    view.eventObjectMoved(diff.x, diff.y);

                    for (int i = 0; i < selModels.Count; i++)
                    {
                        PrintModel stl = selModels[i];
                        float actualMoveX = stl.Position.x - prevX[i], actualMoveY = stl.Position.y - prevY[i];
                    }
                }
                lastX = xPos;
                lastY = yPos;
                gl.Invalidate();
            }
        }

        public void SetMode(int _mode)
        {
            mode = _mode;
            // toolMove.Checked = mode == 1;
            toolMoveViewpoint.Checked = mode == 2;
        }

        private void SetCameraDefaults()
        {
            cam.viewCenter = new Vector3(0, 0, 0);
            cam.defaultDistance = 1.6f * (float)Math.Sqrt(ps.PrintAreaDepth * ps.PrintAreaDepth + ps.PrintAreaWidth * ps.PrintAreaWidth + ps.PrintAreaHeight * ps.PrintAreaHeight);
            cam.minDistance = 0.001 * cam.defaultDistance;
        }

        public void frontView()
        {
            SetCameraDefaults();
            cam.OrientFront();
            gl.Invalidate();
        }

        public void backView()
        {
            SetCameraDefaults();
            cam.OrientBack();
            gl.Invalidate();
        }

        public void leftView()
        {
            SetCameraDefaults();
            cam.OrientLeft();
            gl.Invalidate();
        }

        public void rightView()
        {
            SetCameraDefaults();
            cam.OrientRight();
            gl.Invalidate();
        }

        public void topView()
        {
            SetCameraDefaults();
            cam.OrientTop();
            gl.Invalidate();
        }

        public void bottomView()
        {
            SetCameraDefaults();
            cam.OrientBottom();
            gl.Invalidate();
        }

        public void isometricView()
        {
            SetCameraDefaults();
            cam.OrientIsometric();
            gl.Invalidate();
        }

        public void FitPrinter()
        {
            cam.FitPrinter();
            gl.Invalidate();
        }

        private void toolMoveViewpoint_Click(object sender, EventArgs e)
        {
            SetMode(2);
        }

        private void ThreeDControl_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == '-')
            {
                zoom *= 1.05f;
                if (zoom > 10) zoom = 10;
                gl.Invalidate();
                e.Handled = true;
            }
            if (e.KeyChar == '+')
            {
                zoom *= 0.95f;
                if (zoom < 0.01) zoom = 0.01f;
                gl.Invalidate();
                e.Handled = true;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            Application_Idle(sender, e);
            timeCall++;
            foreach (ThreeDModel m in view.models)
            {
                if (m.Changed || m.hasAnimations)
                {
                    if ((Main.threeDSettings.drawMethod == 0 && (timeCall % 9) != 0))
                        return;
                    if (m.hasAnimations && Main.threeDSettings.drawMethod != 0)
                        gl.Invalidate();
                    else if ((timeCall % 3) == 0)
                        gl.Invalidate();
                    return;
                }
            }
        }

        private void toolStripClear_Click(object sender, EventArgs e)
        {
            if (view.editor)
            {
                Main.main.objectPlacement.buttonRemoveSTL_Click(null, null);
            }
            foreach (ThreeDModel m in view.models)
            {
                m.Clear();
            }
            gl.Invalidate();
        }

        private void ThreeDControl_MouseEnter(object sender, EventArgs e)
        {
            // Focus();
        }

        public void ThreeDControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (Main.main.tab.SelectedTab == Main.main.tabModel && e.Handled == false)
            {
                Main.main.objectPlacement.listSTLObjects_KeyDown(sender, e);
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            cam.PreparePanZoomRot();
            cam.Zoom(1.1);
            if (zoom < 0.002) zoom = 0.002f;
            if (zoom > 5.9) zoom = 5.9f;
        
            gl.Invalidate();
            SetMode(3);
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        { }

        public void button_zoomIn_Click(object sender, EventArgs e)
        {
            cam.PreparePanZoomRot();
            cam.Zoom(0.9);
            if (zoom < 0.002) zoom = 0.002f;
            if (zoom > 5.9) zoom = 5.9f;

            gl.Invalidate();
        }

        public void button_zoomOut_Click(object sender, EventArgs e)
        {
            cam.PreparePanZoomRot();
            cam.Zoom(1.1);
            if (zoom < 0.002) zoom = 0.002f;
            if (zoom > 5.9) zoom = 5.9f;
            gl.Invalidate();
        }

        public void button_remove_Click(object sender, EventArgs e)
        {
            if (view.editor)
            {
                Main.main.objectPlacement.buttonRemoveSTL_Click(null, null);
            }
            foreach (ThreeDModel m in view.models)
            {
                m.Clear();
            }
            gl.Invalidate();

            if (view.models.Count == 0)
            {
                Main.main.toolStripSaveJob.Enabled = false;
                setMinMaxClippingLayer();
            }
            Main.main.objectPlacement.updateOutside();
        }

        public void button_helpInfo_Click(object sender, EventArgs e)
        {
            Main.main.toolStripButton_helpInfo_Click(null, null);
        }

        private void contextMenu_Opening(object sender, CancelEventArgs e)
        {
            PrintModel model = Main.main.objectPlacement.SingleSelectedModel;
            if (model == null)
            {
                landObjectToolStripMenuItem.Visible = false;
                objectInfomationToolStripMenuItem.Visible = false;
                toolStripSeparator3.Visible = false;
                resetObjectToolStripMenuItem.Visible = false;
                removeObjectToolStripMenuItem.Visible = false;
                mminchToolStripMenuItem.Visible = false;
                inchmmToolStripMenuItem.Visible = false;
                cloneToolStripMenuItem.Visible = false;
                return;
            }
            else
            {
                landObjectToolStripMenuItem.Visible = true;
                objectInfomationToolStripMenuItem.Visible = true;
                toolStripSeparator3.Visible = true;
                resetObjectToolStripMenuItem.Visible = true;
                removeObjectToolStripMenuItem.Visible = true;
                mminchToolStripMenuItem.Visible = true;
                inchmmToolStripMenuItem.Visible = true;
                cloneToolStripMenuItem.Visible = true;
             }
        }

        private void landObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui.UI_move.button_land_Click(null, null);
        }

        private void resetObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ui.UI_resize_advance.button_Reset_Click(null, null);
            ui.UI_rotate.button_rotate_reset_Click(null, null);
            ui.UI_move.button_move_reset_Click(null, null);
        }

        private void removeObjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.main.threedview.ui.remove_toggleButton_Click(null, null);
        }

        private void objectInfomationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Main.main.objectPlacement.showObjectInfo(null, null);
        }

        private void newWorkspacetoolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void mminchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintModel model = Main.main.objectPlacement.SingleSelectedModel;
            if (model == null) return;
            Main.main.objectPlacement.DoInchOrScale(model, true);
        }

        private void inchmmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintModel model = Main.main.objectPlacement.SingleSelectedModel;
            if (model == null) return;
            Main.main.objectPlacement.DoInchtomm(model);
        }

        private void cloneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.stlComp.CloneObject();
        }

        public void TransConvexHull3D(ThreeDModel model)
        {
            float[] tempConvexHullAry = new float[model.convexHull3DVtxOrg.Length];

            for (int i = 0; i < model.convexHull3DVtxOrg.Length / 3; i++)
            {
                Vector4 ver = new Vector4(model.convexHull3DVtxOrg[i * 3], model.convexHull3DVtxOrg[i * 3 + 1], model.convexHull3DVtxOrg[i * 3 + 2], 0);

                ver.X *= model.Scale.x;
                ver.Y *= model.Scale.y;
                ver.Z *= model.Scale.z;

                ver = Vector4.Transform(ver, (Matrix4.Invert(model.curPos2)));
                //ver = Vector4.Transform(ver, (getRotationMatrix(model.Rotation.x, model.Rotation.y, model.Rotation.z)));

                ver.X += model.Position.x;
                ver.Y += model.Position.y;
                ver.Z += model.Position.z;

                tempConvexHullAry[i * 3] = ver.X;
                tempConvexHullAry[i * 3 + 1] = ver.Y;
                tempConvexHullAry[i * 3 + 2] = ver.Z;
            }
            model.convexHull3DVtx = tempConvexHullAry;
        }
    }
}
