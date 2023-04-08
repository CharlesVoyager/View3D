﻿using OpenTK;
using System;

namespace View3D.model.geom
{
    public class RHVector3
    {
#if PRECISION_SINGLE
        public float x = 0, y = 0, z = 0;
#else
        public double x = 0, y = 0, z = 0;
#endif

        public RHVector3(double _x, double _y, double _z)
        {
            x = (float)_x;
            y = (float)_y;
            z = (float)_z;
        }
        public RHVector3(float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }

        public RHVector3(RHVector3 orig)
        {
            if (orig == null)
            {
                x = 0;
                y = 0;
                z = 0;
            }
            else
            {
                x = orig.x;
                y = orig.y;
                z = orig.z;
            }
        }

        public RHVector3(Vector3 orig)
        {
            x = orig.X;
            y = orig.Y;
            z = orig.Z;
        }

        public RHVector3(Vector4 orig)
        {
            x = orig.X/orig.W;
            y = orig.Y / orig.W;
            z = orig.Z / orig.W;
        }

#if PRECISION_SINGLE
        public Vector4 asVector4()
        {
            return new Vector4(x, y, z, 1);
        }

        public Vector3 asVector3()
        {
            return new Vector3(x, y, z);
        }

        public float Length
        {
            get
            {
                 return (float)Math.Sqrt(x * x + y * y + z * z);    // 模長 => 向量的大小
            }
        }
#else
        public Vector4 asVector4()
        {
            return new Vector4((float)x, (float)y, (float)z, 1);
        }

        public Vector3 asVector3()
        {
            return new Vector3((float)x, (float)y, (float)z);
        }

        public double Length
        {
            get
            {
                 return Math.Sqrt(x * x + y * y + z * z);    // 模長 => 向量的大小
            }
        }
#endif

        public void NormalizeSafe()
        {
#if PRECISION_SINGLE
            float l = Length;
#else
            double l = Length;
#endif
            if (l == 0)
            {
                x = y = 0;
                z = 0;
            }
            else
            {
                x /= l;
                y /= l;
                z /= l;
            }
        }

        public void StoreMinimum(RHVector3 vec)
        {
            x = Math.Min(x, vec.x);
            y = Math.Min(y, vec.y);
            z = Math.Min(z, vec.z);
        }

        public void StoreMaximum(RHVector3 vec)
        {
            x = Math.Max(x, vec.x);
            y = Math.Max(y, vec.y);
            z = Math.Max(z, vec.z);
        }

#if PRECISION_SINGLE
        public float Distance(RHVector3 point)
        {
            double dx = point.x - x;
            double dy = point.y - y;
            double dz = point.z - z;
            return (float)Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }
#else
        public double Distance(RHVector3 point)
        {
            double dx = point.x - x;
            double dy = point.y - y;
            double dz = point.z - z;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }
#endif

        public void Scale(double factor)
        {
            x *= (float)factor;
            y *= (float)factor;
            z *= (float)factor;
        }
        public void Scale(float factor)
        {
            x *= factor;
            y *= factor;
            z *= factor;
        }

        public double ScalarProduct(RHVector3 vector)
        {
            return x * vector.x + y * vector.y + z * vector.z;
        }

        public double AngleForNormalizedVectors(RHVector3 direction)
        {
            return Math.Acos(ScalarProduct(direction));
        }

        public double Angle(RHVector3 direction)
        {
            return Math.Acos(ScalarProduct(direction)/(Length*direction.Length));
        }

        public RHVector3 Subtract(RHVector3 vector)
        {
            return new RHVector3(x - vector.x, y - vector.y, z - vector.z);
        }

        public RHVector3 Add(RHVector3 vector)
        {
            return new RHVector3(x + vector.x, y + vector.y, z + vector.z);
        }

        public void SubtractInternal(RHVector3 vector)
        {
            x -= vector.x;
            y -= vector.y;
            z -= vector.z;
        }

        public void AddInternal(RHVector3 vector)
        {
            x += vector.x;
            y += vector.y;
            z += vector.z;
        }

        public RHVector3 CrossProduct(RHVector3 vector)
        {
            return new RHVector3(
                y*vector.z-z*vector.y,
                z*vector.x-x*vector.z,
                x*vector.y-y*vector.x);
        }

        public double this[int dimension]
        {
            get
            {
                if (dimension == 0) return x;
                else if (dimension == 1) return y;
                else return z;
            }
            set
            {
#if PRECISION_SINGLE
                if (dimension == 0) x = (float)value;
                else if (dimension == 1) y = (float)value;
                else z = (float)value;
#else
                if (dimension == 0) x = value;
                else if (dimension == 1) y = value;
                else z = value;
#endif
            }
        }

        //--- MODEL_SLA
        public override bool Equals(object obj)
        {
            RHVector3 compare = obj as RHVector3;
            if (x == compare.x && y == compare.y && z == compare.z)
                return true;
            else
                return false;
        }
        //---

        public override string ToString()
        {
            return "(" + x.ToString() + ";" + y.ToString() + ";" + z.ToString() + ")";
        }

    }
}
