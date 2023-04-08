using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;
using View3D.model.geom;
using System.Diagnostics;

namespace View3D.model
{
    public class ModelGLDraw : IDraw
    {
        #region Draw Function
        public void Draw(Submesh mesh, int method, Vector3 edgetrans, bool forceFaces = false)
        {
            GL.LightModel(LightModelParameter.LightModelTwoSide, 1);
            GL.Material(MaterialFace.Back, MaterialParameter.AmbientAndDiffuse, mesh.convertColor(Main.threeDSettings.insideFaces.BackColor));
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Emission, new OpenTK.Graphics.Color4(0, 0, 0, 0));
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, new float[] { 1.0f, 1.0f, 1.0f, 1.0f });
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, 50f);
            GL.Disable(EnableCap.CullFace);
            GL.Enable(EnableCap.Normalize);
            GL.LineWidth(1f);
            GL.DepthFunc(DepthFunction.Less);
            if (method == 2)
            {
                if (mesh.glBuffer == null)
                {
                    mesh.glBuffer = new int[6];
                    GL.GenBuffers(6, mesh.glBuffer);
                    GL.BindBuffer(BufferTarget.ArrayBuffer, mesh.glBuffer[0]);
                    GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(mesh.glVertices.Length * sizeof(float)), mesh.glVertices, BufferUsageHint.StaticDraw);
                    GL.BindBuffer(BufferTarget.ArrayBuffer, mesh.glBuffer[1]);
                    GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(mesh.glNormals.Length * sizeof(float)), mesh.glNormals, BufferUsageHint.StaticDraw);
                    GL.BindBuffer(BufferTarget.ArrayBuffer, mesh.glBuffer[2]);
                    GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(mesh.glColors.Length * sizeof(int)), mesh.glColors, BufferUsageHint.StaticDraw);
                    GL.BindBuffer(BufferTarget.ElementArrayBuffer, mesh.glBuffer[3]);
                    GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(mesh.glTriangles.Length * sizeof(int)), mesh.glTriangles, BufferUsageHint.StaticDraw);
                    GL.BindBuffer(BufferTarget.ElementArrayBuffer, mesh.glBuffer[4]);
                    GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(mesh.glEdges.Length * sizeof(int)), mesh.glEdges, BufferUsageHint.StaticDraw);
                    GL.BindBuffer(BufferTarget.ElementArrayBuffer, mesh.glBuffer[5]);
                    GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(mesh.glTrianglesError.Length * sizeof(int)), mesh.glTrianglesError, BufferUsageHint.StaticDraw);
                }
                GL.BindBuffer(BufferTarget.ArrayBuffer, mesh.glBuffer[0]);
                GL.VertexPointer(3, VertexPointerType.Float, 0, 0);
                GL.EnableClientState(ArrayCap.VertexArray);
                GL.BindBuffer(BufferTarget.ArrayBuffer, mesh.glBuffer[1]);
                GL.NormalPointer(NormalPointerType.Float, 0, 0);
                GL.EnableClientState(ArrayCap.NormalArray);
                GL.BindBuffer(BufferTarget.ArrayBuffer, mesh.glBuffer[2]);
                GL.ColorMaterial(MaterialFace.Front, ColorMaterialParameter.AmbientAndDiffuse);
                GL.ColorPointer(4, ColorPointerType.UnsignedByte, sizeof(int), IntPtr.Zero);
                GL.EnableClientState(ArrayCap.ColorArray);
                GL.Enable(EnableCap.ColorMaterial);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, mesh.glBuffer[3]);

                if (Main.threeDSettings.ShowFaces || forceFaces)
                {
                    //Modified by RCGREY for STL Slice Previewer
                    if (mesh.clipEnable)
                    {
                        if (mesh.clipMode == Submesh.ClipType.Top)
                        {
                            GL.ClearStencil(1);                            
                            ApplyClippingAndCappingTOP(mesh);
                        }
                        else if (mesh.clipMode == Submesh.ClipType.Bottom)
                        {
                            GL.ClearStencil(2);
                            ApplyClippingAndCappingBTM(mesh);
                        }
                        else if (mesh.clipMode == Submesh.ClipType.Both)
                        {
                            GL.ClearStencil(1);
                            ApplyClippingAndCappingTOP(mesh);
                            GL.ClearStencil(2);
                            ApplyClippingAndCappingBTM(mesh);
                        }

                        int stencilbits;

                        GL.GetInteger(GetPName.StencilBits, out stencilbits);

                        if (stencilbits == 0)
                            Debug.WriteLine("Stencil Buffer Empty");
                        //STL Slice Previewer
                    }
                    else
                        GL.DrawElements(PrimitiveType.Triangles, mesh.glTriangles.Length, DrawElementsType.UnsignedInt, 0);
                }

                GL.LightModel(LightModelParameter.LightModelTwoSide, 0);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, mesh.glBuffer[5]);
                GL.DrawElements(PrimitiveType.Triangles, mesh.glTrianglesError.Length, DrawElementsType.UnsignedInt, 0);
                GL.Disable(EnableCap.ColorMaterial);
                GL.DisableClientState(ArrayCap.NormalArray);
                GL.Disable(EnableCap.Lighting);
                GL.DepthFunc(DepthFunction.Lequal);
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, mesh.glBuffer[4]);
                GL.PushMatrix();
                GL.Translate(edgetrans);
                GL.DrawElements(PrimitiveType.Lines, mesh.glEdges.Length, DrawElementsType.UnsignedInt, 0);
                GL.PopMatrix();
                GL.Enable(EnableCap.Lighting);
                GL.DisableClientState(ArrayCap.ColorArray);
                GL.DisableClientState(ArrayCap.VertexArray);
                GL.DisableClientState(ArrayCap.ColorArray);
                GL.Disable(EnableCap.ColorMaterial);
            }
            else if (method == 1)
            {
                GL.EnableClientState(ArrayCap.VertexArray);
                GL.VertexPointer(3, VertexPointerType.Float, 0, mesh.glVertices);
                GL.EnableClientState(ArrayCap.NormalArray);
                GL.NormalPointer(NormalPointerType.Float, 0, mesh.glNormals);
                GL.EnableClientState(ArrayCap.ColorArray);
                GL.Enable(EnableCap.ColorMaterial);
                GL.ColorMaterial(MaterialFace.Front, ColorMaterialParameter.AmbientAndDiffuse);
                GL.ColorPointer(4, ColorPointerType.UnsignedByte, 0, mesh.glColors);
                GL.EnableClientState(ArrayCap.ColorArray);
                if (Main.threeDSettings.ShowFaces || forceFaces)
                    GL.DrawElements(PrimitiveType.Triangles, mesh.glTriangles.Length, DrawElementsType.UnsignedInt, mesh.glTriangles);
                GL.Material(MaterialFace.FrontAndBack, MaterialParameter.AmbientAndDiffuse, mesh.convertColor(Main.threeDSettings.errorModel.BackColor));
                GL.LightModel(LightModelParameter.LightModelTwoSide, 0);
                GL.DrawElements(PrimitiveType.Triangles, mesh.glTrianglesError.Length, DrawElementsType.UnsignedInt, mesh.glTrianglesError);
                GL.DepthFunc(DepthFunction.Lequal);
                GL.Disable(EnableCap.Lighting);
                GL.PushMatrix();
                GL.Translate(edgetrans);
                GL.DrawElements(PrimitiveType.Lines, mesh.glEdges.Length, DrawElementsType.UnsignedInt, mesh.glEdges);
                GL.PopMatrix();
                GL.Enable(EnableCap.Lighting);
                GL.Disable(EnableCap.ColorMaterial);
                GL.DisableClientState(ArrayCap.ColorArray);
                GL.DisableClientState(ArrayCap.VertexArray);
                GL.DisableClientState(ArrayCap.NormalArray);

            }
            else if (method == 0)
            {
                int n;
                GL.ColorMaterial(MaterialFace.Front, ColorMaterialParameter.AmbientAndDiffuse);
                GL.Enable(EnableCap.ColorMaterial);
                if (Main.threeDSettings.ShowFaces || forceFaces)
                {
                    GL.Begin(PrimitiveType.Triangles);
                    n = mesh.glTriangles.Length;
                    for (int i = 0; i < n; i++)
                    {
                        int p = mesh.glTriangles[i] * 3;
                        int col = mesh.glColors[mesh.glTriangles[i]];
                        GL.Color4(new OpenTK.Graphics.Color4((byte)(col & 255), (byte)((col >> 8) & 255), (byte)((col >> 16) & 255), (byte)(col >> 24)));
                        GL.Normal3(mesh.glNormals[p], mesh.glNormals[p + 1], mesh.glNormals[p + 2]);
                        GL.Vertex3(mesh.glVertices[p], mesh.glVertices[p + 1], mesh.glVertices[p + 2]);
                    }
                    GL.End();
                }
                GL.LightModel(LightModelParameter.LightModelTwoSide, 0);
                n = mesh.glTrianglesError.Length;
                if (n > 0)
                {
                    GL.Begin(PrimitiveType.Triangles);
                    for (int i = 0; i < n; i++)
                    {
                        int p = mesh.glTrianglesError[i] * 3;
                        int col = mesh.glColors[mesh.glTrianglesError[i]];
                        GL.Color4(new OpenTK.Graphics.Color4((byte)(col & 255), (byte)((col >> 8) & 255), (byte)((col >> 16) & 255), (byte)(col >> 24)));
                        GL.Normal3(mesh.glNormals[p], mesh.glNormals[p + 1], mesh.glNormals[p + 2]);
                        GL.Vertex3(mesh.glVertices[p], mesh.glVertices[p + 1], mesh.glVertices[p + 2]);
                    }
                    GL.End();
                }

                GL.Disable(EnableCap.Lighting);
                n = mesh.glEdges.Length;
                GL.PushMatrix();
                GL.Translate(edgetrans);

                if (n > 0)
                {
                    GL.Begin(PrimitiveType.Lines);
                    for (int i = 0; i < n; i++)
                    {
                        int p = mesh.glEdges[i] * 3;
                        int col = mesh.glColors[mesh.glEdges[i]];
                        GL.Color4(new OpenTK.Graphics.Color4((byte)(col & 255), (byte)((col >> 8) & 255), (byte)((col >> 16) & 255), (byte)(col >> 24)));
                        GL.Normal3(mesh.glNormals[p], mesh.glNormals[p + 1], mesh.glNormals[p + 2]);
                        GL.Vertex3(mesh.glVertices[p], mesh.glVertices[p + 1], mesh.glVertices[p + 2]);
                    }
                    GL.End();
                }
                GL.PopMatrix();
                GL.Enable(EnableCap.Lighting);
                GL.Disable(EnableCap.ColorMaterial);
            }
            GL.LightModel(LightModelParameter.LightModelTwoSide, 0);
            GL.Disable(EnableCap.Normalize);
        }

        public Vector3 GetTranslateVector()
        {
            return Main.main.threedview.cam.EdgeTranslation();
        }
        #endregion

        #region Color Setting
        public GetColorSettingHandler GetColorSetting { get; set; }

        public virtual int GetColorRGBA(Submesh.MeshColor colorCode, Color frontBackColor)
        {
            int idx = (int)colorCode;

            if (idx >= 0)
                return 255 << 24 | idx;

            Color color;
            if (GetColorSetting != null)
                //color = GetColorSetting(colorCode);
                color = GetColorSetting(colorCode, frontBackColor);
            else
                color = Color.Wheat;

            return ColorToRgba32(color);
        }

        private int ColorToRgba32(Color c)
        {
            return (int)((c.A << 24) | (c.B << 16) | (c.G << 8) | c.R);
        }
        #endregion

        #region Cut Face
        public bool IsCutFaceEnabled()
        {
            return Main.main.objectPlacement.checkCutFaces.Checked;
        }

        public bool IsEdgeShowEnabled()
        {
            return Main.threeDSettings.ShowEdges;
        }

        public RHVector3 GetCutPosition()
        {
            return Main.main.threedview.cutPos;
        }

        public RHVector3 GetCutDirection()
        {
            return Main.main.threedview.cutDirection;
        }

        public bool IsCutFaceUpdated()
        {
            return Main.main.threedview.updateCuts;
        }
        #endregion

        #region Cut Edge
        //Added by RCGREY for STL Slice Previewer 
        private void BeginCutEdgeConfig(Submesh mesh)
        {
            double[] planeEqDown = mesh.clipPlaneEq;
            double[] planeEqUP = { mesh.clipPlaneEq[0], mesh.clipPlaneEq[1], -mesh.clipPlaneEq[2], mesh.clipPlaneEq[3] };
            //double[] planeEqUP = { 0, 0, 1.0, cut_height + 0.5 };
            AttribMask flag = 0;
            flag = flag | AttribMask.EnableBit;
            flag = flag | AttribMask.PolygonBit;
            flag = flag | AttribMask.CurrentBit;
            flag = flag | AttribMask.DepthBufferBit;
            flag = flag | AttribMask.TransformBit;
            GL.PushAttrib(flag);

            GL.Enable(EnableCap.ClipPlane0);
            GL.Enable(EnableCap.ClipPlane1);
            GL.ClipPlane(ClipPlaneName.ClipPlane0, planeEqDown);
            GL.ClipPlane(ClipPlaneName.ClipPlane1, planeEqUP);


            GL.Disable(EnableCap.Lighting);
            GL.Disable(EnableCap.DepthTest);
            GL.Disable(EnableCap.CullFace);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
        }

        private void EndCutEdgeConfig()
        {
            GL.Disable(EnableCap.ClipPlane0);
            GL.Disable(EnableCap.ClipPlane1);
            GL.PopAttrib();
        }

        #endregion

        #region STL Slice Previewer
        public double GetMaxLayerHeight()
        {
            return Main.main.threedview.clippingLayerMax;
        }

        public double GetMinLayerHeight()
        {
            return Main.main.threedview.clippingLayerMin;
        }

        public float GetAppliedThickness()
        {
            return (float)Main.main.threedview.setclipLayerHeight;

        }

        public double GetClipPlaneHeight()
        {
            return Main.main.threedview.clippingLayerCur;
        }

        public float GetInverseClipLayerHeight()
        {
            return (float)(1 / Main.main.threedview.setclipLayerHeight);
        }

        private void BeginClipPlaneConfigTop(Submesh mesh)
        {
            double[] planeEqDown = mesh.clipPlaneEq;

            GL.Enable(EnableCap.ClipPlane3);
            GL.ClipPlane(ClipPlaneName.ClipPlane3, planeEqDown);
        }

        private void EndClipPlaneConfigTop()
        {
            GL.Disable(EnableCap.ClipPlane3);
        }

        private void BeginClipPlaneConfigBottom(Submesh mesh)
        {
            double[] planeEqDown = mesh.clipPlaneEqbtm;

            GL.Enable(EnableCap.ClipPlane4);
            GL.ClipPlane(ClipPlaneName.ClipPlane4, planeEqDown);
        }

        private void EndClipPlaneConfigBottom()
        {
            GL.Disable(EnableCap.ClipPlane4);
        }

        private void drawCappingPlane(Submesh mesh, float planSize, float red, float green, float blue, float alpha, bool positive_side)
        {
            int side = 1;
            //string captexture_file = @"captexture.jpg";

            Vector3 origVec = new Vector3(0, 0, -1); // original plane normal vector
            Vector3 targetVec = new Vector3((float)mesh.clipPlaneEq[0], (float)mesh.clipPlaneEq[1], (float)mesh.clipPlaneEq[2]); //upside plane

            if (!positive_side) //bottom side plane
            {
                origVec = new Vector3(0, 0, 1);
                targetVec = new Vector3((float)mesh.clipPlaneEqbtm[0], (float)mesh.clipPlaneEqbtm[1], (float)mesh.clipPlaneEqbtm[2]);
            }

            Vector3 cross = Vector3.Cross(origVec, targetVec);
            double innerProduct = Vector3.Dot(origVec, targetVec);
            double cosine = innerProduct / (origVec.Length * targetVec.Length);
            double angle = Math.Acos(cosine); // unit: radian
            Matrix4 rotMat = Matrix4.Identity;
            if (!positive_side)
                rotMat = new Matrix4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, -1, 0, 0, 0, 0, 0);
            if (angle != 0)
            {
                rotMat = Matrix4.CreateFromAxisAngle(cross, (float)angle);
                side = -1;
            }

            GL.PushMatrix();
            GL.Color4(red, green, blue, alpha);
            GL.Normal3(0.0f, 0.0f, 1.0f);

            GL.Begin(PrimitiveType.Quads);

            Matrix3 rotMat3 = new Matrix3(rotMat);
            if (positive_side) //top
            {
                GL.TexCoord2(0, 0); GL.Vertex3(Vector3.Transform(new Vector3(planSize, planSize, (float)mesh.clipPlaneEq[3]), rotMat3));
                GL.TexCoord2(1, 0); GL.Vertex3(Vector3.Transform(new Vector3(-planSize, planSize, (float)mesh.clipPlaneEq[3]), rotMat3));
                GL.TexCoord2(1, 1); GL.Vertex3(Vector3.Transform(new Vector3(-planSize, -planSize, (float)mesh.clipPlaneEq[3]), rotMat3));
                GL.TexCoord2(0, 1); GL.Vertex3(Vector3.Transform(new Vector3(planSize, -planSize, (float)mesh.clipPlaneEq[3]), rotMat3));
            }
            else // bottom
            {
                GL.TexCoord2(0, 0); GL.Vertex3(Vector3.Transform(new Vector3(planSize, -planSize, side * (float)mesh.clipPlaneEqbtm[3]), rotMat3));
                GL.TexCoord2(1, 0); GL.Vertex3(Vector3.Transform(new Vector3(-planSize, -planSize, side * (float)mesh.clipPlaneEqbtm[3]), rotMat3));
                GL.TexCoord2(1, 1); GL.Vertex3(Vector3.Transform(new Vector3(-planSize, planSize, side * (float)mesh.clipPlaneEqbtm[3]), rotMat3));
                GL.TexCoord2(0, 1); GL.Vertex3(Vector3.Transform(new Vector3(planSize, planSize, side * (float)mesh.clipPlaneEqbtm[3]), rotMat3));
            }
            GL.End();
            GL.Flush();
            GL.PopMatrix();
        }

        private Color skin = Color.FromArgb(1, Color.DarkTurquoise);

        private void ApplyClippingAndCappingTOP(Submesh mesh)
        {
            if (Main.main.threedview.disableStencilMask > 0)
            {
                GL.Clear(ClearBufferMask.StencilBufferBit);
                if (Main.main.threedview.disableStencilMask == 1)
                    Main.main.threedview.disableStencilMask = 0;
            }

            GL.ColorMask(false, false, false, false);
            GL.Disable(EnableCap.DepthTest);

            BeginClipPlaneConfigTop(mesh);

            GL.Enable(EnableCap.StencilTest);
            GL.Enable(EnableCap.CullFace);

            GL.StencilFunc(StencilFunction.Always, 1, 0);
            GL.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Incr);
            GL.CullFace(CullFaceMode.Front);
            GL.DrawElements(PrimitiveType.Triangles, mesh.glTriangles.Length, DrawElementsType.UnsignedInt, 0);

            GL.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Decr);
            GL.CullFace(CullFaceMode.Back);
            GL.DrawElements(PrimitiveType.Triangles, mesh.glTriangles.Length, DrawElementsType.UnsignedInt, 0);


            GL.Disable(EnableCap.CullFace);

            EndClipPlaneConfigTop();

            GL.ColorMask(true, true, true, true);
            GL.Enable(EnableCap.DepthTest);

            GL.StencilFunc(StencilFunction.Notequal, 1, ~0);
            drawCappingPlane(mesh, (float)Main.printerSettings.PrintAreaWidth / 2, skin.R, skin.G, skin.B, skin.A, true);
            
            GL.Disable(EnableCap.StencilTest);

            if (mesh.clipMode != Submesh.ClipType.Both)
            {
                BeginClipPlaneConfigTop(mesh);

                GL.DrawElements(PrimitiveType.Triangles, mesh.glTriangles.Length, DrawElementsType.UnsignedInt, 0);

                EndClipPlaneConfigTop();
            }

        }

        private void ApplyClippingAndCappingBTM(Submesh mesh)
        {
            if (Main.main.threedview.disableStencilMask > 0)
            {
                GL.Clear(ClearBufferMask.StencilBufferBit);
                if (Main.main.threedview.disableStencilMask == 1)
                    Main.main.threedview.disableStencilMask = 0;
            }

            GL.ColorMask(false, false, false, false);
            GL.Disable(EnableCap.DepthTest);

            BeginClipPlaneConfigBottom(mesh);
            GL.Enable(EnableCap.StencilTest);
            GL.Enable(EnableCap.CullFace);

            GL.StencilFunc(StencilFunction.Always, 2, 0);
            GL.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Incr);
            GL.CullFace(CullFaceMode.Front);
            GL.DrawElements(PrimitiveType.Triangles, mesh.glTriangles.Length, DrawElementsType.UnsignedInt, 0);

            GL.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Decr);
            GL.CullFace(CullFaceMode.Back);
            GL.DrawElements(PrimitiveType.Triangles, mesh.glTriangles.Length, DrawElementsType.UnsignedInt, 0);

            GL.Disable(EnableCap.CullFace);

            EndClipPlaneConfigBottom();

            GL.ColorMask(true, true, true, true);
            GL.Enable(EnableCap.DepthTest);

            GL.StencilFunc(StencilFunction.Notequal, 2, ~0);
            drawCappingPlane(mesh, 64.0f, skin.R, skin.G, skin.B, skin.A, false);

            GL.Disable(EnableCap.StencilTest);

            if (mesh.clipMode == Submesh.ClipType.Both)
                BeginClipPlaneConfigTop(mesh);

            BeginClipPlaneConfigBottom(mesh);
            GL.DrawElements(PrimitiveType.Triangles, mesh.glTriangles.Length, DrawElementsType.UnsignedInt, 0);
            EndClipPlaneConfigBottom();

            if (mesh.clipMode == Submesh.ClipType.Both)
                EndClipPlaneConfigTop();

            if (mesh.clipMode == Submesh.ClipType.Both && Main.main.threedview.viewSilhouette)
            {
                GL.PushAttrib(AttribMask.AllAttribBits);
                GL.PolygonMode(MaterialFace.Back, PolygonMode.Line);
                GL.LineWidth(0.001f);
                GL.Disable(EnableCap.Lighting);
                GL.DrawElements(PrimitiveType.Points, mesh.glTriangles.Length, DrawElementsType.UnsignedInt, 0);
                GL.Color4(255.0f, 0.0f, 0.0f, 0.1f);
                GL.Enable(EnableCap.Lighting);
                GL.PolygonMode(MaterialFace.Back, PolygonMode.Line);

                GL.PopAttrib();
            }
        }
        #endregion
    }
}
