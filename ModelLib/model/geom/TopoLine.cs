using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using View3D;

namespace View3D.model.geom
{
    public class TopoLine
    {
        public struct Line
        {
            public static Line Empty;

            private PointF p1;
            private PointF p2;

            public Line(PointF p1, PointF p2)
            {
                this.p1 = p1;
                this.p2 = p2;
            }

            public PointF P1
            {
                get { return p1; }
                set { p1 = value; }
            }

            public PointF P2
            {
                get { return p2; }
                set { p2 = value; }
            }

            public float X1
            {
                get { return p1.X; }
                set { p1.X = value; }
            }

            public float X2
            {
                get { return p2.X; }
                set { p2.X = value; }
            }

            public float Y1
            {
                get { return p1.Y; }
                set { p1.Y = value; }
            }

            public float Y2
            {
                get { return p2.Y; }
                set { p2.Y = value; }
            }

            public double Distance
            {
                get
                {
                    return Math.Abs(Math.Sqrt(Math.Pow(X2 - X1, 2) + Math.Pow(Y2 - Y1, 2)));
                }
            }

            public RHVector3 MidPoint
            {
                get
                {
                    return new RHVector3((X1 + X2) / 2.0, (Y1 + Y2) / 2.0, 0);
                }
            }
        }

        public enum Intersection
        {
            None,
            Tangent,
            Intersection,
            Containment
        }

        public static Intersection IntersectionOf(Line line1, Line line2)
        {
            //  Fail if either line segment is zero-length.
            if (line1.X1 == line1.X2 && line1.Y1 == line1.Y2 || line2.X1 == line2.X2 && line2.Y1 == line2.Y2)
                return Intersection.None;

            if (line1.X1 == line2.X1 && line1.Y1 == line2.Y1 || line1.X2 == line2.X1 && line1.Y2 == line2.Y1)
                return Intersection.Intersection;
            if (line1.X1 == line2.X2 && line1.Y1 == line2.Y2 || line1.X2 == line2.X2 && line1.Y2 == line2.Y2)
                return Intersection.Intersection;

            //  (1) Translate the system so that point A is on the origin.
            line1.X2 -= line1.X1; line1.Y2 -= line1.Y1;
            line2.X1 -= line1.X1; line2.Y1 -= line1.Y1;
            line2.X2 -= line1.X1; line2.Y2 -= line1.Y1;

            //  Discover the length of segment A-B.
            double distAB = Math.Sqrt(line1.X2 * line1.X2 + line1.Y2 * line1.Y2);

            //  (2) Rotate the system so that point B is on the positive X axis.
            double theCos = line1.X2 / distAB;
            double theSin = line1.Y2 / distAB;
            double newX = line2.X1 * theCos + line2.Y1 * theSin;
            line2.Y1 = line2.Y1 * (float)theCos - line2.X1 * (float)theSin; line2.X1 = (float)newX;
            newX = line2.X2 * theCos + line2.Y2 * theSin;
            line2.Y2 = line2.Y2 * (float)theCos - line2.X2 * (float)theSin; line2.X2 = (float)newX;

            //  Fail if segment C-D doesn't cross line A-B.
            if (line2.Y1 < 0 && line2.Y2 < 0 || line2.Y1 >= 0 && line2.Y2 >= 0)
                return Intersection.None; ;

            //  (3) Discover the position of the intersection point along line A-B.
            double posAB = line2.X2 + (line2.X1 - line2.X2) * line2.Y2 / (line2.Y2 - line2.Y1);

            //  Fail if segment C-D crosses line A-B outside of segment A-B.
            if (posAB < 0 || posAB > distAB)
                return Intersection.None; 

            //  (4) Apply the discovered position to line A-B in the original coordinate system.
            return Intersection.Intersection;
        }
    }
}
