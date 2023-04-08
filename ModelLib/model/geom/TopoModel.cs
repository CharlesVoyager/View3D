﻿using OpenTK;
using System;
using System.Collections.Generic;
using View3D.Extensions;

namespace View3D.model.geom
{
    public class TopoModel
    {
        static int MinVertexNumForProress = 2000; // large than all support part vertices number
        public enum FileFormat { Unknown, Binary, UTF8 };
        public const bool debugRepair = false;
        public const float epsilon = 0.001f;
        public TopoVertexStorage vertices = new TopoVertexStorage();
        public TopoTriangleStorage triangles = new TopoTriangleStorage();
        public static TopoTriangleStorage brimtriangles = new TopoTriangleStorage();

        public LinkedList<TopoEdge> edges = new LinkedList<TopoEdge>();
        public RHBoundingBox boundingBox = new RHBoundingBox();
        public HashSet<TopoTriangle> intersectingTriangles = new HashSet<TopoTriangle>();
        public int badEdges = 0;
        public int badTriangles = 0;
        public int shells = 0;
        public int updatedNormals = 0;
        public int loopEdges = 0;
        public int manyShardEdges = 0;
        public bool manifold = false;
        public bool normalsOriented = false;
        public bool intersectionsUpToDate = false;
        public bool selectedTrunkSup { get; set; } //02102017 Added by RC Grey for tree support
        public bool branchSupError { get; set; } //02102017 Added by RC Grey for tree support
        public bool selectedDepthAdjust { get; set; } //02282017 Added by RC Grey for cone/branch support for collision depth adjustments
        public bool selectedCell { get; set; } // 05-29-2018 - JOSHUA | for cell selection

        public bool sliceMainModel = false; // Added by RCGREY for STL Slice Previewer

        private static double OVERHANG_THRESHOLD = -0.3;         // 72 degree, Horizontal Level 0
        private static double OVERHANG_THRESHOLD_H_LV_1 = -0.7;  // 45 degree
        private static double OVERHANG_THRESHOLD_H_LV_2 = -0.85; // 32 degree
        public bool isShowOverhangArea = false;

        public void Clear()
        {
            //derek ,display brim test
            brimtriangles.Clear();
            ////  
            vertices.Clear();
            triangles.Clear();
            edges.Clear();
            boundingBox.Clear();
            intersectionsUpToDate = false;
            GC.Collect();
        }
        
        public TopoModel Copy()
        {
            TopoModel newModel = new TopoModel();
            int nOld = vertices.Count;
            int i = 0;
            List<TopoVertex> vcopy = new List<TopoVertex>(vertices.Count);
            foreach (TopoVertex v in vertices.v)
            {
                v.id = i++;
                TopoVertex newVert = new TopoVertex(v.id, v.pos);
                newModel.addVertex(newVert);
                vcopy.Add(newVert);
            }
            foreach (TopoTriangle t in triangles)
            {
                TopoTriangle triangle = new TopoTriangle(newModel, vcopy[t.vertices[0].id], vcopy[t.vertices[1].id], vcopy[t.vertices[2].id], t.normal.x, t.normal.y, t.normal.z);
                newModel.triangles.Add(triangle);
            }
            UpdateVertexNumbers();
            newModel.UpdateVertexNumbers();
            newModel.badEdges = 0;
            newModel.badTriangles = badTriangles;
            newModel.shells = shells;
            newModel.updatedNormals = updatedNormals;
            newModel.loopEdges = loopEdges;
            newModel.manyShardEdges = 0;
            newModel.manifold = manifold;
            newModel.normalsOriented = normalsOriented;
            return newModel;
        }

        public void Merge(TopoModel model, Matrix4 trans, Action<double> updateRate)
        {
            int nOld = vertices.Count;
            int i = 0;
            double cnt = 0;

            List<TopoVertex> vcopy = new List<TopoVertex>(model.vertices.Count);
            foreach (TopoVertex v in model.vertices.v)
            {
                v.id = i++;
                TopoVertex newVert = new TopoVertex(v.id, v.pos, trans);
                addVertex(newVert);
                vcopy.Add(newVert);

                cnt++;
                if (updateRate != null && (model.vertices.v.Count > MinVertexNumForProress)
                    && (cnt % (model.vertices.v.Count / 10) == 0))
                {
                    updateRate((double)cnt / model.vertices.v.Count *50.0);
                }
            }
            cnt = 0;
            foreach (TopoTriangle t in model.triangles)
            {
                TopoTriangle triangle = new TopoTriangle(this, vcopy[t.vertices[0].id], vcopy[t.vertices[1].id], vcopy[t.vertices[2].id]);
                triangle.RecomputeNormal();
                triangles.Add(triangle);

                cnt++;
                if (updateRate != null && (model.vertices.v.Count > MinVertexNumForProress)
                    && (cnt % (model.triangles.Count / 10) == 0))
                {
                    updateRate((double)cnt / model.triangles.Count *50.0 + 50);
                }
            }

            //RemoveUnusedDatastructures();
            intersectionsUpToDate = false;
        }

        public void addVertex(TopoVertex v)
        {
            vertices.Add(v);
            boundingBox.Add(v.pos);
        }

        public TopoVertex findVertexOrNull(RHVector3 pos)
        {
            return vertices.SearchPoint(pos);
        }

        public TopoVertex addVertex(RHVector3 pos)
        {
            TopoVertex newVertex = findVertexOrNull(pos);
            if (newVertex == null)
            {
                newVertex = new TopoVertex(vertices.Count, pos); // vertex id start from 0
                addVertex(newVertex);
            }
            return newVertex;
        }

        public void UpdateVertexNumbers()
        {
            int i = 1;
            foreach (TopoVertex v in vertices.v)
            {
                v.id = i++;
            }
        }

        public void UpdateIntersectingTriangles()
        {
            intersectionsUpToDate = true;
        }

        public TopoTriangle addTriangle(double p1x, double p1y, double p1z, double p2x, double p2y, double p2z,
            double p3x, double p3y, double p3z, double nx, double ny, double nz)
        {
            TopoVertex v1 = addVertex(new RHVector3(p1x, p1y, p1z));
            TopoVertex v2 = addVertex(new RHVector3(p2x, p2y, p2z));
            TopoVertex v3 = addVertex(new RHVector3(p3x, p3y, p3z));
            
            TopoTriangle triangle = new TopoTriangle(this, v1, v2, v3, nx, ny, nz);
            return AddTriangle(triangle);
        }

        public TopoTriangle addTriangle(RHVector3 p1, RHVector3 p2, RHVector3 p3, RHVector3 normal)
        {
            TopoVertex v1 = addVertex(p1);
            TopoVertex v2 = addVertex(p2);
            TopoVertex v3 = addVertex(p3);
            TopoTriangle triangle = new TopoTriangle(this, v1, v2, v3, normal.x, normal.y, normal.z);

            return AddTriangle(triangle);
        }

        public TopoTriangle AddTriangle(TopoTriangle triangle)
        {
            if (triangle.IsDegenerated())
                triangle.Unlink(this);
            else
                triangles.Add(triangle);
            return triangle;
        }

        public void removeTriangle(TopoTriangle triangle)
        {
            triangle.Unlink(this);
            triangles.Remove(triangle);
            brimtriangles.Remove(triangle);
        }

        public void UpdateNormals()
        {
        }

        public bool CheckNormals()
        {
            return true;
        }
        
        public double Surface()
        {
            double surface = 0;
            foreach (TopoTriangle t in triangles)
            {
                surface += t.Area();
            }
            return surface;
        }
        
        public double Volume()
        {
            double volume = 0;
            foreach (TopoTriangle t in triangles)
                volume += t.SignedVolume();
            return Math.Abs(volume);
        }
        
        public bool JoinTouchedOpenEdges(double limit)
        {
            return false;
        }

        public bool Analyse()
        {
            //RepairUnobtrusive();
            UpdateIntersectingTriangles();
            CheckNormals();
            manyShardEdges = 0;
            loopEdges = 0;
            foreach (TopoEdge edge in edges)
            {
                if (edge.connectedFaces < 2) 
                    loopEdges++;
                else if (edge.connectedFaces > 2) 
                    manyShardEdges++;
            }
            if (loopEdges + manyShardEdges == 0)
            {
                manifold = true;
            }
            else
            {
                manifold = false;
                ////MessageBox.Show(Trans.T("M_OBJECT_IS_NON_MANIFOLD"), Trans.T("W_OBJECT_IS_NON_MANIFOLD"));
            }
            UpdateVertexNumbers();

            return manifold;
        }

        public void checkEdgesOver2()
        {
            foreach (TopoEdge e in edges)
            {
                if (e.connectedFaces > 2)
                {
                    Console.WriteLine("Too many connected faces");
                    return;
                }
            }
        }

        public List<TopoModel> SplitIntoSurfaces()
        {
            return null;
        }

        public void CutMesh(Submesh mesh, RHVector3 normal, RHVector3 point,int defaultFaceColor)
        {
            TopoPlane plane = new TopoPlane(normal, point);
            //bool drawEdges = Main.threeDSettings.ShowEdges;
            foreach (TopoEdge e in edges)
                e.algHelper = 0; // Mark drawn edges, so we insert them only once
            foreach (TopoTriangle t in triangles)
            {
                int side = plane.testTriangleSideFast(t);
                mesh.AddTriangle(t.vertices[0].pos, t.vertices[1].pos, t.vertices[2].pos, defaultFaceColor);
            }
        }


        public void getTriInWorld(Matrix4 trans, TopoTriangle tInObj, out TopoTriangle tInWorld)
        {
            Vector4 ver1 = tInObj.vertices[0].pos.asVector4();
            Vector4 ver2 = tInObj.vertices[1].pos.asVector4();
            Vector4 ver3 = tInObj.vertices[2].pos.asVector4();
            ver1 = Vector4.Transform(ver1, trans);
            ver2 = Vector4.Transform(ver2, trans);
            ver3 = Vector4.Transform(ver3, trans);
            TopoVertex v1 = new TopoVertex(0, new RHVector3(ver1.X, ver1.Y, ver1.Z));
            TopoVertex v2 = new TopoVertex(1, new RHVector3(ver2.X, ver2.Y, ver2.Z));
            TopoVertex v3 = new TopoVertex(2, new RHVector3(ver3.X, ver3.Y, ver3.Z));
            tInWorld = new TopoTriangle(v1, v2, v3);
        }

        // milton
        public void FillMeshCheckRAM(Matrix4 modelMx, Submesh mesh, int defaultColor)
        {
            int cnt = 0;
            
            double triWorNormalValue = 0.0;
            foreach (TopoTriangle t in triangles)
            {
                TopoTriangle triWor = null; //modified by RCGREY for STL Slice Previewer

                if (0 == cnt % 50000)
                {
                    if (!Utils.RamTools.IsRamSizeValid())
                    {
                        throw new System.OutOfMemoryException();
                    }
                }
                getTriInWorld(modelMx, t, out triWor);
                triWorNormalValue = triWor.normal.z / triWor.normal.Length;

                if (isShowOverhangArea)
                {
                    if (triWorNormalValue < OVERHANG_THRESHOLD_H_LV_2)
                    {
                        if (triWor.vertices[0].pos.z > 0.01)
                            mesh.AddTriangle(t.vertices[0].pos, t.vertices[1].pos, t.vertices[2].pos, Submesh.MESHCOLOR_PINK);
                        else
                            mesh.AddTriangle(t.vertices[0].pos, t.vertices[1].pos, t.vertices[2].pos, defaultColor);
                    }
                    else if (triWorNormalValue < OVERHANG_THRESHOLD_H_LV_1)
                        mesh.AddTriangle(t.vertices[0].pos, t.vertices[1].pos, t.vertices[2].pos, Submesh.MESHCOLOR_LIGHTPINK);
                    else if (triWorNormalValue < OVERHANG_THRESHOLD)
                        mesh.AddTriangle(t.vertices[0].pos, t.vertices[1].pos, t.vertices[2].pos, Submesh.MESHCOLOR_LIGHTPINK_WHITE);
                    else
                        mesh.AddTriangle(t.vertices[0].pos, t.vertices[1].pos, t.vertices[2].pos, defaultColor);
                }
                else
                {
                    mesh.AddTriangle(t.vertices[0].pos, t.vertices[1].pos, t.vertices[2].pos, defaultColor);
                }
                cnt++;
            }
        }

        public void FillMesh(Submesh mesh,int defaultColor)
        {
            foreach (TopoTriangle t in triangles)
            {
                mesh.AddTriangle(t.vertices[0].pos, t.vertices[1].pos, t.vertices[2].pos, defaultColor);
            }
        }

    }
}
