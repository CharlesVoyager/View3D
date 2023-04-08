﻿using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace View3D.model.geom
{
    public class SubmeshEdge
    {
        public int vertex1, vertex2;
        public int color;
        public SubmeshEdge(int v1, int v2, int col)
        {
            vertex1 = v1;
            vertex2 = v2;
            color = col;
        }
    }

    public class SubmeshTriangle
    {
        public int vertex1, vertex2, vertex3;
        public int color;

        public SubmeshTriangle(int v1, int v2, int v3, int col)
        {
            vertex1 = v1;
            vertex2 = v2;
            vertex3 = v3;
            color = col;
        }

        public void Normal(Submesh mesh, out float nx, out float ny, out float nz)
        {
            Vector3 v0 = mesh.vertices[vertex1];
            Vector3 v1 = mesh.vertices[vertex2];
            Vector3 v2 = mesh.vertices[vertex3];
            float a1 = v1.X - v0.X;
            float a2 = v1.Y - v0.Y;
            float a3 = v1.Z - v0.Z;
            float b1 = v2.X - v1.X;
            float b2 = v2.Y - v1.Y;
            float b3 = v2.Z - v1.Z;
            nx = a2 * b3 - a3 * b2;
            ny = a3 * b1 - a1 * b3;
            nz = a1 * b2 - a2 * b1;
            float length = (float)Math.Sqrt(nx * nx + ny * ny + nz * nz);
            if (length == 0)
            {
                nx = ny = 0;
                nz = 1;
            }
            else
            {
                nx /= length;
                ny /= length;
                nz /= length;
            }
        }
    }
    public class Submesh
    {

        public enum MeshColor {
            FrontBack   = -1,
            ErrorFace   = -2,
            ErrorEdge   = -3,
            OutSide     = -4,
            EdgeLoop    = -5,
            CutEdge     = -6,
            Normal      = -7,
            Edge        = -8,
            Back        = -9,
            TransBlue   = -10,
            OverhangLv1 = -11,
            OverhangLv2 = -12,
            OverhangLv3 = -13,
            // Cone Support Generator
            // Added mesh color - orange
            // Developer: RC Grey
            // 01-12-2017
            ConeSupport = -14,
            // Trunk Support Generator
            TreeSymbol = -15,
            TreeMesh = -16,
            TreeError = -17,
            TreeSlect = -18,
            TreeTest = -19
        };

        //KPPH CP2-SW RCGREY: STL Slice Previewer
        public enum ClipType
        {
            None = 0,
            Top,
            Bottom,
            Both
        };

        public const int MESHCOLOR_FRONTBACK = -1;
        public const int MESHCOLOR_ERRORFACE = -2;
        public const int MESHCOLOR_ERROREDGE = -3;
        public const int MESHCOLOR_OUTSIDE = -4;
        public const int MESHCOLOR_EDGE_LOOP = -5;
        public const int MESHCOLOR_CUT_EDGE = -6;
        public const int MESHCOLOR_NORMAL = -7;
        public const int MESHCOLOR_EDGE = -8;
        public const int MESHCOLOR_BACK = -9;
        //--- MODEL_SLA	// milton
        public const int MESHCOLOR_TRANS_BLUE = -10;
        public const int MESHCOLOR_PINK = -11;
        public const int MESHCOLOR_LIGHTPINK = -12;
        public const int MESHCOLOR_LIGHTPINK_WHITE = -13;
        // Tree/Cone Support Generator
        // Added mesh colors
        // Developer: RC Grey
        // 01-12-2017
        public const int MESHCOLOR_CONESUP = -14;
        public const int MESHCOLOR_TREESYM = -15;
        public const int MESHCOLOR_TREEMSH = -16;
        public const int MESHCOLOR_BRNHERR = -17;
        public const int MESHCOLOR_TREESEL = -18;
        public const int MESHCOLOR_TREETES = -19;
        //--
        public List<Vector3> vertices = new List<Vector3>();
        public List<SubmeshEdge> edges = new List<SubmeshEdge>();
        public List<SubmeshTriangle> triangles = new List<SubmeshTriangle>();
        public List<SubmeshTriangle> trianglesError = new List<SubmeshTriangle>();
        public bool selected = false;
        public int extruder = 0;

        //Modified by RCGREY for STL Slice Previewer tool
        public bool clipEnable = false;
        public ClipType clipMode = ClipType.Top;
        public double[] clipPlaneEq = { 0, 0, -1.0, 0.0 };
        public double[] clipPlaneEqbtm = { 0, 0, 1.0, 0.0 };
        public float zLayerHeight = 0;

        public float[] glVertices = null;
        public int[] glColors = null;
        public int[] glEdges = null;
        public int[] glTriangles = null;
        public int[] glTrianglesError = null;
        public int[] glBuffer = null;
        public float[] glNormals = null;

        IDraw drawer;

        public void Clear()
        {
            vertices.Clear();
            edges.Clear();
            triangles.Clear();
            trianglesError.Clear();
            ClearGL();
        }

        //int ConvertColorIndex(int idx)
        int ConvertColorIndex(int idx, Color frontBackColor)
        {
            if (drawer != null)
                //return drawer.GetColorRGBA((MeshColor)idx);
                return drawer.GetColorRGBA((MeshColor)idx, frontBackColor);
            else
                return ColorToRgba32(Color.White);
        }
        private int ColorToRgba32(Color c)
        {
            return (int)((c.A << 24) | (c.B << 16) | (c.G << 8) | c.R);
        }

        /// <summary>
        /// Remove unneded temporary data
        /// </summary>
        public void Compress()
        {
            Compress(false, 0);
        }

        public void Compress(bool override_color, int color)
        {
            glVertices = new float[3 * vertices.Count];
            glNormals = new float[3 * vertices.Count];
            glColors = new int[vertices.Count];
            glEdges = new int[edges.Count * 2];
            glTriangles = new int[triangles.Count * 3];
            glTrianglesError = new int[trianglesError.Count * 3];
            UpdateDrawLists();
            UpdateColors(override_color, color);
            vertices.Clear();
        }

        public int VertexId(RHVector3 v)
        {
            int pos = vertices.Count;
            vertices.Add(new Vector3((float)v.x, (float)v.y, (float)v.z));
            return pos;
        }

        public int VertexId(double x, double y, double z)
        {
            int pos = vertices.Count;
            vertices.Add(new Vector3((float)x, (float)y, (float)z));
            return pos;
        }

        public void AddEdge(RHVector3 v1, RHVector3 v2, int color)
        {
            edges.Add(new SubmeshEdge(VertexId(v1), VertexId(v2), color));
        }

        public void AddTriangle(RHVector3 v1, RHVector3 v2, RHVector3 v3, int color)
        {
            if (color == MESHCOLOR_ERRORFACE)
                trianglesError.Add(new SubmeshTriangle(VertexId(v1), VertexId(v2), VertexId(v3), color));
            else
                triangles.Add(new SubmeshTriangle(VertexId(v1), VertexId(v2), VertexId(v3), color));
        }

        private void ClearGL()
        {
            if (glBuffer != null)
            {
                GL.DeleteBuffers(glBuffer.Length, glBuffer);
                glBuffer = null;
            }
        }

        public void UpdateColors(bool override_color, int color)
        {
            Color frontBackColor;

            frontBackColor = Color.LightSkyBlue; 
            foreach (SubmeshTriangle t in triangles)
            {
                if (!override_color)
                {
                    glColors[t.vertex1] = glColors[t.vertex2] = glColors[t.vertex3] = ConvertColorIndex(t.color, frontBackColor);
                }
                else
                    glColors[t.vertex1] = glColors[t.vertex2] = glColors[t.vertex3] = color;
            }
            foreach (SubmeshTriangle t in trianglesError)
            {
                if (!override_color)
                    glColors[t.vertex1] = glColors[t.vertex2] = glColors[t.vertex3] = ConvertColorIndex(t.color, frontBackColor);
                else
                    glColors[t.vertex1] = glColors[t.vertex2] = glColors[t.vertex3] = color;
            }
            foreach (SubmeshEdge e in edges)
            {
                if (!override_color)
                    glColors[e.vertex1] = glColors[e.vertex2] = ConvertColorIndex(e.color, frontBackColor);
                else
                    glColors[e.vertex1] = glColors[e.vertex2] = color;
            }
            if (glBuffer != null)
            {
                // Bind current context to Array Buffer ID
                GL.BindBuffer(BufferTarget.ArrayBuffer, glBuffer[2]);
                // Send data to buffer
                GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(glColors.Length * sizeof(int)), glColors, BufferUsageHint.StaticDraw);
                // Validate that the buffer is the correct size
                int bufferSize;
                GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out bufferSize);
                if (glColors.Length * sizeof(int) != bufferSize)
                    throw new ApplicationException("Vertex array not uploaded correctly");
                // Clear the buffer Binding
                GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            }
        }

        public void UpdateDrawLists()
        {
            int idx = 0;
            foreach (SubmeshTriangle t in triangles)
            {
                int n1 = 3 * t.vertex1;
                int n2 = 3 * t.vertex2;
                int n3 = 3 * t.vertex3;
                Vector3 v1 = vertices[t.vertex1];
                Vector3 v2 = vertices[t.vertex2];
                Vector3 v3 = vertices[t.vertex3];
                t.Normal(this, out glNormals[n1], out glNormals[n1 + 1], out glNormals[n1 + 2]);
                glNormals[n2] = glNormals[n3] = glNormals[n1];
                glNormals[n2 + 1] = glNormals[n3 + 1] = glNormals[n1 + 1];
                glNormals[n2 + 2] = glNormals[n3 + 2] = glNormals[n1 + 2];
                glVertices[n1++] = v1.X;
                glVertices[n1++] = v1.Y;
                glVertices[n1] = v1.Z;
                glVertices[n2++] = v2.X;
                glVertices[n2++] = v2.Y;
                glVertices[n2] = v2.Z;
                glVertices[n3++] = v3.X;
                glVertices[n3++] = v3.Y;
                glVertices[n3] = v3.Z;
                glTriangles[idx++] = t.vertex1;
                glTriangles[idx++] = t.vertex2;
                glTriangles[idx++] = t.vertex3;
            }
            idx = 0;
            foreach (SubmeshTriangle t in trianglesError)
            {
                int n1 = 3 * t.vertex1;
                int n2 = 3 * t.vertex2;
                int n3 = 3 * t.vertex3;
                Vector3 v1 = vertices[t.vertex1];
                Vector3 v2 = vertices[t.vertex2];
                Vector3 v3 = vertices[t.vertex3];
                t.Normal(this, out glNormals[n1], out glNormals[n1 + 1], out glNormals[n1 + 2]);
                glNormals[n2] = glNormals[n3] = glNormals[n1];
                glNormals[n2 + 1] = glNormals[n3 + 1] = glNormals[n1 + 1];
                glNormals[n2 + 2] = glNormals[n3 + 2] = glNormals[n1 + 2];
                glVertices[n1++] = v1.X;
                glVertices[n1++] = v1.Y;
                glVertices[n1] = v1.Z;
                glVertices[n2++] = v2.X;
                glVertices[n2++] = v2.Y;
                glVertices[n2] = v2.Z;
                glVertices[n3++] = v3.X;
                glVertices[n3++] = v3.Y;
                glVertices[n3] = v3.Z;
                glTrianglesError[idx++] = t.vertex1;
                glTrianglesError[idx++] = t.vertex2;
                glTrianglesError[idx++] = t.vertex3;
            }
            idx = 0;
            foreach (SubmeshEdge e in edges)
            {
                int n1 = 3 * e.vertex1;
                int n2 = 3 * e.vertex2;
                Vector3 v1 = vertices[e.vertex1];
                Vector3 v2 = vertices[e.vertex2];
                glNormals[n1] = glNormals[n2] = 0;
                glNormals[n1 + 1] = glNormals[n2 + 1] = 0;
                glNormals[n1 + 2] = glNormals[n2 + 2] = 1;
                glVertices[n1++] = v1.X;
                glVertices[n1++] = v1.Y;
                glVertices[n1] = v1.Z;
                glVertices[n2++] = v2.X;
                glVertices[n2++] = v2.Y;
                glVertices[n2] = v2.Z;
                glEdges[idx++] = e.vertex1;
                glEdges[idx++] = e.vertex2;
            }
        }

        public OpenTK.Graphics.Color4 convertColor(Color col)
        {
            return new OpenTK.Graphics.Color4(col.R, col.G, col.B, col.A);
        }

        public void Draw(Submesh mesh, int method, Vector3 edgetrans, bool forceFaces = false)
        {
            if (drawer != null)
                this.drawer.Draw(this, method, edgetrans, forceFaces);
        }

        public virtual void SetDrawer(IDraw newDrawer)
        {
            this.drawer = newDrawer;
        }

    }
}
