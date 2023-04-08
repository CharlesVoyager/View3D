using System;
using System.Collections.Generic;

namespace View3D.model
{
    public delegate void OnPosChange(GCode code, float x, float y, float z);
    public delegate void OnPosChangeFast(float x, float y, float z);
    public class ExtruderData
    {
        public ExtruderData(int _id) { id = _id; }
        public int id;
    }
    public class GCodeAnalyzer
    {
        public event OnPosChange eventPosChanged;
        public event OnPosChangeFast eventPosChangedFast;
        public int activeExtruderId = 0;
        public ExtruderData activeExtruder = null;
        public Dictionary<int, ExtruderData> extruder = new Dictionary<int, ExtruderData>();
        public LinkedList<GCodeShort> unchangedLayer = new LinkedList<GCodeShort>();
        public float x = 0, y = 0, z = 0, f = 1000;
        public float lastX = 0, lastY = 0, lastZ = 0;
        public float xOffset = 0, yOffset = 0, zOffset = 0, lastZPrint = 0, layerZ = 0;
        public bool hasXHome = false, hasYHome = false, hasZHome = false;
        public bool privateAnalyzer = false;
        public int maxDrawMethod = 2;
        public bool drawing = true;
        public int layer = 0, lastlayer = 0;
        public double printingTime = 0;
        public bool isLaserOn, layerChange;

        public GCodeAnalyzer(bool privAnal)
        {
            privateAnalyzer = privAnal;
            foreach (int k in extruder.Keys)
                extruder[k] = new ExtruderData(k);
            if (!extruder.ContainsKey(activeExtruderId))
                extruder.Add(activeExtruderId, new ExtruderData(activeExtruderId));
            activeExtruder = extruder[activeExtruderId];
        }

        // set to start condition
        public void start()
        {
            activeExtruderId = 0;
            List<int> keys = new List<int>();
            foreach (int k in extruder.Keys)
                keys.Add(k);
            if (!keys.Contains(activeExtruderId))
                keys.Add(activeExtruderId);
            foreach (int k in keys)
                extruder[k] = new ExtruderData(k);
            activeExtruder = extruder[activeExtruderId];
            maxDrawMethod = 2;
            drawing = true;
            layer = 0;
            lastlayer = 0;
            layerZ = 0;
            x = y = z = lastZPrint = 0;
            xOffset = yOffset = zOffset = 0;
            lastX = 0; lastY = 0; lastZ = 0;
            hasXHome = hasYHome = hasZHome = false;
            if (!privateAnalyzer)
                Main.main.jobVisual.ResetQuality();
        }

        public void Analyze(GCode code)
        {
            if (code.hasG)
            {
                switch (code.G)
                {
                    case 0:
                    case 1:
                        if (code.hasX) x = xOffset + code.X;
                        if (code.hasY) y = yOffset + code.Y;
                        if (code.hasL)
                        {
                            z = zOffset + code.Z;
                        }
                        if (x < Main.printerSettings.XMin) { x = Main.printerSettings.XMin; hasXHome = false; }
                        if (y < Main.printerSettings.YMin) { y = Main.printerSettings.YMin; hasYHome = false; }
                        if (z < 0) { z = 0; hasZHome = false; }
                        if (x > Main.printerSettings.XMax) { hasXHome = false; }
                        if (y > Main.printerSettings.YMax) { hasYHome = false; }
                        if (z > Main.printerSettings.PrintAreaWidth) { hasZHome = false; }

                        if (eventPosChanged != null)
                            if (privateAnalyzer)
                                eventPosChanged(code, x, y, z);
                            else
                                Main.main.Invoke(eventPosChanged, code, x, y, z);

                        float dx = Math.Abs(x - lastX);
                        float dy = Math.Abs(y - lastY);
                        float dz = Math.Abs(z - lastZ);
                        if (z != lastZ) unchangedLayer.Clear();
                        lastX = x;
                        lastY = y;
                        lastZ = z;
                        break;
                    case 28:
                        {
                            bool homeAll = !(code.hasX || code.hasY || code.hasL);
                            if (code.hasX || homeAll) { xOffset = 0; x = Main.printerSettings.XHomePos; hasXHome = true; }
                            if (code.hasY || homeAll) { yOffset = 0; y = Main.printerSettings.YHomePos; hasYHome = true; }
                            if (code.hasL || homeAll) { zOffset = 0; z = Main.printerSettings.ZHomePos; hasZHome = true; }
                            if (eventPosChanged != null)
                                if (privateAnalyzer)
                                    eventPosChanged(code, x, y, z);
                                else
                                    Main.main.Invoke(eventPosChanged, code, x, y, z);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public void analyzeShort(GCodeShort code)
        {
            switch (code.compressedCommand)
            {              
                case 1: // G0/G1 command
                    if (code.x != -99999)
                    {
                        x = xOffset + code.x;
                    }
                    if (code.y != -99999)
                    {
                        y = yOffset + code.y;
                    }
                    //--- MODEL_SLA
                    if (code.z != -99999)
                    {
                        z = zOffset + code.z;
                        lastZPrint = code.z;
                        layer++;
                    }
                    //---

                    if (eventPosChangedFast != null)
                        eventPosChangedFast(x, y, z);
                    float dx = Math.Abs(x - lastX);
                    float dy = Math.Abs(y - lastY);
                    float dz = Math.Abs(z - lastZ);
                    lastX = x;
                    lastY = y;
                    lastZ = z;
                    break;
                case 4:     // G28 command
                    {
                        bool homeAll = !(code.hasX || code.hasY || code.hasL);
                        if (code.hasX || homeAll) { xOffset = 0; x = Main.printerSettings.XHomePos; hasXHome = true; }
                        if (code.hasY || homeAll) { yOffset = 0; y = Main.printerSettings.YHomePos; hasYHome = true; }
                        if (code.hasL || homeAll) { zOffset = 0; z = Main.printerSettings.ZHomePos; hasZHome = true; }
                    }
                    break;
                //--- MODEL_SLA
                case 13:    // laser on
                    isLaserOn = true;
                    break;
                case 14:    // laser off
                    isLaserOn = false;
                    break;
                //---
            }
            if (layer != lastlayer)
            {
                foreach (GCodeShort c in unchangedLayer)
                {
                    c.layer = layer;
                }
                unchangedLayer.Clear();
                layerZ = z;
                lastlayer = layer;
            }
            else if (z != layerZ)
                unchangedLayer.AddLast(code);
            code.layer = layer;
        }
    }
}
