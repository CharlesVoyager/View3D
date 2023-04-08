using GLNKG;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XYZ.model.geom;

namespace XYZ.model
{
    public class Layer
    {
        private Double zLowBound = 0;// inclusive
        private Double span = 5;
        public int XNum { get; set; }
        public int YNum { get; set; }
        public List<Cube> cubeList = null;

        public Layer(Double zLow, Double span, int xNum, int yNum)
        {
            cubeList = new List<Cube>();
            this.zLowBound = zLow;
            this.span = span;
            this.XNum = xNum;
            this.YNum = yNum;
        }

        public Layer(Double zLow, Double span)
        {
            cubeList = new List<Cube>();
            this.zLowBound = zLow;
            this.span = span;
            this.XNum = 0;
            this.YNum = 0;
        }

        ~Layer() { Clean(); }

        public void Clean()
        {
            if (null != cubeList)
            {
                foreach (Cube cube in cubeList)
                    cube.Clean();
                cubeList.Clear();
            }
            //cubeList = null;
        }

        public Double ZLowBound
        {
            get { return zLowBound; }
        }

    }

    public class Cube
    {
#if PRECISION_SINGLE
        private Single xLowBound = 0;// inclusive
        private Single yLowBound = 0;// inclusive
        private Single zLowBound = 0;// exclusive
        private Single span = 5;
#else
        private Double xLowBound = 0;// inclusive
        private Double yLowBound = 0;// inclusive
        private Double zLowBound = 0;// exclusive
        private Double span = 5;
#endif
        private int groupIdx = -1;

        public List<Tuple<int, int>> verIdxTriIdxList = null;

        public Cube(Double xLow, Double yLow, Double zLow, Double span)
        {
            verIdxTriIdxList = new List<Tuple<int, int>>();
#if PRECISION_SINGLE
            this.xLowBound = (float)xLow;
            this.yLowBound = (float)yLow;
            this.zLowBound = (float)zLow;
            this.span = (float)span;
#else
            this.xLowBound = xLow;
            this.yLowBound = yLow;
            this.zLowBound = zLow;
            this.span = span;
#endif
        }

        ~Cube() { Clean(); }

        public void Clean()
        {
            if (null != verIdxTriIdxList)
                verIdxTriIdxList.Clear();
            //verIdxTriIdxList = null;
        }
        public int GroupIdx
        {
            get { return groupIdx; }
            set { groupIdx = value; }
        }

#if PRECISION_SINGLE
        public Single XLowBound
        {
            get { return xLowBound; }
        }
        public Single YLowBound
        {
            get { return yLowBound; }
        }
        public Single ZLowBound
        {
            get { return zLowBound; }
        }
        public Single Span
        {
            get { return span; }
        }
#else
        public Double XLowBound
        {
            get { return xLowBound; }
        }
        public Double YLowBound
        {
            get { return yLowBound; }
        }
        public Double ZLowBound
        {
            get { return zLowBound; }
        }
        public Double Span
        {
            get { return span; }
        }
#endif
    }

    //public class CubeMatrix : IEnumerable
    public class CubeMatrix
    {
        //private Cube[,,] CubeMat;
        public Cube[,,] CubeMat;
        public RHVector3 MinPoint { get; set; }
        public RHVector3 MaxPoint { get; set; }
        public Double Span { get; set; }
        public int XNum { get; set; }
        public int YNum { get; set; }
        public int ZNum { get; set; }
        public int Count { get; set; }

        public CubeMatrix()
        {
            CubeMat = null;
            XNum = YNum = ZNum = 0;
            Count = 0;
        }

        /// <summary>
        /// generate empty cube matrix
        /// </summary>
        /// <param name="minPoint">minimum point of bounding box of the specified space</param>
        /// <param name="maxPoint">maximum point of bounding box of the specified space</param>
        /// <param name="span">step unit of cube matrix</param>
        public CubeMatrix(RHVector3 minPoint, RHVector3 maxPoint, double span)
        {
            this.MinPoint = minPoint;
            this.MaxPoint = maxPoint;
            this.Span = span;

            int xNum, yNum, zNum;
            xNum = (int)((MaxPoint.x - MinPoint.x) / span) + 1;
            yNum = (int)((MaxPoint.y - MinPoint.y) / span) + 1;
            zNum = (int)((MaxPoint.z - MinPoint.z) / span) + 1;

            CubeMat = new Cube[xNum, yNum, zNum];
            // From min. z-pos to Max. z-pos by increment (span/10)mm
            //for (Double zStep = MinPoint.z; zStep <= MaxPoint.z; zStep += Span)
            Double zStep, yStep, xStep;
            zStep = MinPoint.z;
            for (int k = 0; k < zNum; k++)
            {
                Debug.Assert(zStep <= MaxPoint.z);
                xStep = MinPoint.x;
                for (int i = 0; i < xNum; i++)
                {
                    Debug.Assert(xStep <= MaxPoint.x);
                    yStep = MinPoint.y;
                    for (int j = 0; j < yNum; j++)
                    //for (Double yStep = MinPoint.y; yStep <= MaxPoint.y; yStep += Span)
                    {
                        Debug.Assert(yStep <= MaxPoint.y);
                        CubeMat[i, j, k] = new Cube(xStep, yStep, zStep, Span);    // layer.cubeList紀錄在同一個X位置下,Y的起始與結束位置
                        yStep += Span;
                    }
                    xStep += Span;
                }
                zStep += Span;
            }
            XNum = xNum;
            YNum = yNum;
            ZNum = zNum;
            Count = XNum * YNum * ZNum;
        }

        public void Clean()
        {
            if (null != CubeMat)
            {
                Array.Clear(CubeMat, 0, CubeMat.Length);
                CubeMat = null;
            }
            XNum = YNum = ZNum = 0;
            Count = 0;
        }

#if CUBEMATRIX_IENUMERATOR
        public IEnumerator<Cube> GetEnumerator()
        {
            return CubeMatrix.GetEnumerator();
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator;
        }
#endif

        public Cube GetIndex(int xIdx, int yIdx, int zIdx)
        {
            return CubeMat[xIdx, yIdx, zIdx];
        }

        /// <summary>
        /// Set triangle and its lowest vertex to cube matrix
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="posIdx"></param>
        /// <param name="triIdx"></param>
        public void SetTriangle(RHVector3 pos, int posIdx, int triIdx)
        {
            int xIdx = (int)((pos.x - MinPoint.x) / Span);
            int yIdx = (int)((pos.y - MinPoint.y) / Span);
            int zIdx = (int)((pos.z - MinPoint.z) / Span);
            if (zIdx < ZNum && xIdx < XNum && yIdx < YNum)
            {
                // record lowest Z-pos in verIdxTriIdxList, i: index of triangle vertex, triIdx: triangle index 
                CubeMat[xIdx, yIdx, zIdx].verIdxTriIdxList.Add(new Tuple<int, int>(posIdx, triIdx));
            }
        }

        /// <summary>
        /// Build cube matrix. Cubes store lowest vertex of triangle.
        /// </summary>
        /// <param name="zAxisNormalThresh">threshold of z-axis component of normal vector</param>
        /// <param name="submesh">triangle mesh</param>
        public int Build(PrintModel model, Submesh submesh, double zAxisNormalThresh)
        {
            int totalTriangle;
            int processing_step;

            if (0 == submesh.triangles.Count) return -1;

            // for each triangle, store information of the lower z position of vertex in cube of layer
            //
            int yEleNum = (int)((MaxPoint.y - MinPoint.y) / Span) + 1; // y element number of one column of cube
                                                                       //model.UpdateMatrix();

            totalTriangle = submesh.triangles.Count;
            processing_step = totalTriangle / 32;
            for (int triIdx = 0; triIdx < totalTriangle; triIdx++)    // total triangles
            {
                Double lowsetZ = Double.PositiveInfinity; // lowest z
                RHVector3 pos;
                int vId;
                int vtxIdxLowZ = 0; // vertex index of lowest z
                int[] triVId = new int[3];

                // get the lowest vertex in the triangle
                for (int i = 0; i < 3; i++)
                {
                    vId = model.originalModel.triangles.triangles[triIdx].vertices[i].id;
                    pos = model.vtxPosWorldCor[vId];
                    if (pos.z < lowsetZ)
                    {
                        lowsetZ = pos.z;
                        vtxIdxLowZ = i;
                    }
                    triVId[i] = vId;
                }

#if SUP_DEUBG
                bool cand_triangle = false;
                Debug.Write("TRI " + triIdx.ToString() + " vtx ");
                for (int i = 0; i < triWor.vertices.Count(); i++)
                {
                    Debug.Write("(");
                    Debug.Write(triWor.vertices[i].pos.x.ToString("N3"));
                    Debug.Write(",");
                    Debug.Write(triWor.vertices[i].pos.y.ToString("N3"));
                    Debug.Write(",");
                    Debug.Write(triWor.vertices[i].pos.z.ToString("N3"));
                    Debug.Write(")");
                    Debug.Write((i == vtx_idx_lowz ? "*" : " "));
                    Debug.Write(", ");
                }
                Debug.Write("norZ ");
                Debug.Write(triWor.normal.z.ToString("N3"));
                Debug.Write("(unit: ");
                Debug.Write((triWor.normal.z / triWor.normal.Length).ToString("N3"));
                Debug.Write(")");
                if ((triWor.normal.z / triWor.normal.Length) <= upperBound)
                    cand_triangle = true;
                Debug.WriteLine((true == cand_triangle ? "*" : " "));
#endif

                // upperBound: z-axis normal vector, support will be added when normal vector large than -0.3
                // arc-cos(0.3) = 60 degree, the degree is between z-axis and vector
                if ((model.triNormalWorldCor[triIdx].z / model.triNormalWorldCor[triIdx].Length) > zAxisNormalThresh) continue;     // Z-axis向量小於-0.3的才會考慮長支撐

                // Add all lowest verWor(s), the verWor(s) may at the same z coordinate
                for (int i = 0; i < 3; i++)
                {
                    if (Math.Abs(model.vtxPosWorldCor[triVId[i]].z - lowsetZ) < 0.001)
                    {
                        // record lowest Z-pos in verIdxTriIdxList, i: index of triangle vertex, triIdx: triangle index 
                        SetTriangle(model.vtxPosWorldCor[triVId[i]], i, triIdx);
                    }
                }

                if (triIdx % processing_step == 0)
                {
                    Main.main.threedview.ui.BusyWindow.busyProgressbar.Value = ((double)triIdx / totalTriangle) * 100.0 + 100.0;
                    Application.DoEvents();
                }
            }

            return 0;
        }

        /// <summary>
        /// Build cube matrix. Cubes store contained triangles.
        /// </summary>
        /// <param name="zAxisNormalThresh">threshold of z-axis component of normal vector</param>
        /// <param name="submesh">triangle mesh</param>
        public int BuildForCD(PrintModel model, Submesh submesh, double zAxisNormalThresh)
        {
            int totalTriangle;
            int processing_step;

            if (0 == submesh.triangles.Count) return -1;

            // for each triangle, store information of the lower z position of vertex in cube of layer
            //
            int yEleNum = (int)((MaxPoint.y - MinPoint.y) / Span) + 1; // y element number of one column of cube
            //model.UpdateMatrix();

            totalTriangle = submesh.triangles.Count;
            processing_step = totalTriangle / 32;
            for (int triIdx = 0; triIdx < totalTriangle; triIdx++)    // total triangles
            {
                Double lowsetZ = Double.PositiveInfinity; // lowest z
                RHVector3 pos;
                int vId;
                int vtxIdxLowZ = 0; // vertex index of lowest z
                int[] triVId = new int[3];
                RHBoundingBox triBBox = new RHBoundingBox();

                // get the lowest vertex in the triangle
                for (int i = 0; i < 3; i++)
                {
                    vId = model.originalModel.triangles.triangles[triIdx].vertices[i].id;
                    pos = model.vtxPosWorldCor[vId];
                    if (pos.z < lowsetZ)
                    {
                        lowsetZ = pos.z;
                        vtxIdxLowZ = i;
                    }
                    triVId[i] = vId;
                    triBBox.Add(pos);
                }

                // upperBound: z-axis normal vector, support will be added when normal vector large than -1.01 (all triangles)
                //if ((model.triNormalWorldCor[triIdx].z / model.triNormalWorldCor[triIdx].Length) > zAxisNormalThresh) continue;

                // record triangle index to every cube which is intersection with
                int xIdxS, yIdxS, zIdxS; // start cube index of triangle bounding box
                int xIdxE, yIdxE, zIdxE; // end cube index of triangle bounding box
                xIdxS = (int)((triBBox.xMin - MinPoint.x) / Span);
                yIdxS = (int)((triBBox.yMin - MinPoint.y) / Span);
                zIdxS = (int)((triBBox.zMin - MinPoint.z) / Span);
                xIdxE = (int)((triBBox.xMax - MinPoint.x) / Span);
                yIdxE = (int)((triBBox.yMax - MinPoint.y) / Span);
                zIdxE = (int)((triBBox.zMax - MinPoint.z) / Span);
                for (int k = zIdxS; k <= zIdxE; k++)
                {
                    for (int j = yIdxS; j <= yIdxE; j++)
                    {
                        for (int i = xIdxS; i <= xIdxE; i++)
                        {
                            if (k < ZNum && i < XNum && j < YNum)
                            {
                                // record lowest Z-pos in verIdxTriIdxList, i: index of triangle vertex, triIdx: triangle index 
                                CubeMat[i, j, k].verIdxTriIdxList.Add(new Tuple<int, int>(vtxIdxLowZ, triIdx));
                            }
                        }
                    }
                }

                if (triIdx % processing_step == 0)
                {
                    Main.main.threedview.ui.BusyWindow.busyProgressbar.Value = ((double)triIdx / totalTriangle) * 50.0 + 50.0;
                    Application.DoEvents();
                }
            }

            return 0;
        }

#if CUBEMATENUM
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator;
        }
        public CubeMatEnum GetEnumerator()
        {
            return new CubeMatEnum(_cube);
        }
#endif
    }

#if CUBEMATENUM
    public class CubeMatEnum : IEnumerator
    {
        public Cube[] _cube;

        //Enumerators are positioned before the first element
        //until the first MoveNext() call.
        int position = -1;

        public CubeMatEnum(Cube[] list)
        {
            _cube = list;
        }

        public object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public bool MoveNext()
        {
            position++;
            return (position < _cube.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        public Cube Current
        {
            get
            {
                try
                {
                    return _cube[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
#endif
}
