﻿using OpenTK;
using System.Collections.Generic;

namespace View3D.model
{
    public class Coord3D
    {
        public float x = 0, y = 0, z = 0;
        //Edward Add new value
        public float inix = 0, iniy = 0, iniz = 0;
        public Coord3D() { }
        public Coord3D(float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }
    }
    public abstract class ThreeDModel
    {
        private bool selected = false;
        public bool reset = false;
        private Coord3D position = new Coord3D();      // shift position
        private Coord3D rotation = new Coord3D();      // rotate vector
        private Coord3D scale = new Coord3D(1, 1, 1);  // scaler magnitude
        public LinkedList<ModelAnimation> animations = new LinkedList<ModelAnimation>();
        public float xMin = 0, yMin = 0, zMin = 0, xMax = 0, yMax = 0, zMax = 0;              // min and max print region
        public Matrix4 curPos = new Matrix4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);  // x, y, z, w
        public Matrix4 curPos2 = new Matrix4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
        // pre-rotate, diff-rotate?, pre-s?, pre-move?
        public float preRX = 0, preRY = 0, preRZ = 0, dRX = 0, dRY = 0, dRZ = 0, preSX = 0, preSY = 0, preSZ = 0, preMX = 0, preMY = 0, preMZ = 0, preRX2 = 0, preRY2 = 0, preRZ2 = 0;
        public float[] convexHull3DVtxOrg;
        public float[] convexHull3DVtx;
        public float[] convexHull2DVtx;

        public void addAnimation(ModelAnimation anim)
        {
            animations.AddLast(anim);
        }

        public void removeAnimationWithName(string aname)
        {
            bool found = true;
            while (found)
            {
                found = false;
                foreach (ModelAnimation a in animations)
                {
                    if (a.name.Equals(aname))
                    {
                        found = true;
                        animations.Remove(a);
                        break;
                    }
                }
            }
        }

        public bool hasAnimationWithName(string aname)
        {
            foreach (ModelAnimation a in animations)
            {
                if (a.name.Equals(aname))
                {
                    return true;
                }
            }
            return false;
        }

        public void clearAnimations()
        {
            animations.Clear();
        }

        public bool hasAnimations
        {
            get { return animations.Count > 0; }
        }

        public void AnimationBefore()
        {
            foreach (ModelAnimation a in animations)
                a.BeforeAction(this);
        }

        /// <summary>
        /// Plays the after action and removes finished animations.
        /// </summary>
        public void AnimationAfter()
        {
            bool remove = false;
            foreach (ModelAnimation a in animations)
            {
                a.AfterAction(this);
                remove |= a.AnimationFinished();
            }
            if (remove)
            {
                bool found = true;
                while (found)
                {
                    found = false;
                    foreach (ModelAnimation a in animations)
                    {
                        if (a.AnimationFinished())
                        {
                            found = true;
                            animations.Remove(a);
                            break;
                        }
                    }
                }
            }
        }

        public bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }
        public Coord3D Position
        {
            get { return position; }
            set { position = value; }
        }

        public Coord3D Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public Coord3D Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public virtual void ReduceQuality() {
        }

        public virtual void ResetQuality() { }
        /// <summary>
        /// Has the model changed since last paint?
        /// </summary>
        public virtual bool Changed
        {
            get { return false; }
        }

        public virtual void Clear() { }
        abstract public void Paint();
        abstract public void Paint2();
        public virtual Vector3 getCenter()
        {
            return new Vector3(0, 0, 0);
        }

        abstract public void SliceModel();
    }
}
