﻿using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace View3D.view.utils
{
    public class RegMemory
    {
        public static int GetInt(string r, int def)
        {
            return 0;

        }
        public static void SetInt(string r, int val)
        {

        }
        public static string GetString(string r, string def)
        {
            return "";
        }
        public static void SetString(string r, string val)
        {
        }
        public static string WindowPosToString(Form f, bool state)
        {
            Rectangle rest = f.DesktopBounds;
            return rest.X.ToString() + "|" +
                rest.Y.ToString() + (state ? f.WindowState.ToString() : "");
        }
        public static string WindowPosSizeToString(Form f, bool state)
        {
            Rectangle rest = f.DesktopBounds;
            if (f.WindowState != FormWindowState.Maximized)
                return rest.X.ToString() + "|" +
                    rest.Y.ToString() + "|" +
                    rest.Width.ToString() + "|" +
                    rest.Height.ToString() + "|" +
                    (state ? FormWindowState.Normal.ToString() : "");
            rest = f.RestoreBounds;
            return rest.X.ToString() + "|" +
                rest.Y.ToString() + "|" +
                rest.Width.ToString() + "|" +
                rest.Height.ToString() + "|" +
                (state ? f.WindowState.ToString() : "");
        }
        private static bool IsVisibleOnAnyScreen(Rectangle rect)
        {
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.WorkingArea.IntersectsWith(rect))
                {
                    return true;
                }
            }
            return false;
        }
        private static bool IsVisibleOnAnyScreen(Point pnt)
        {
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.WorkingArea.Contains(pnt))
                {
                    return true;
                }
            }
            return false;
        }
        public static void StringToWindowPos(Form f, string pos, int screenId)
        {
            if (string.IsNullOrEmpty(pos)) return;
            if (screenId > Screen.AllScreens.Length) screenId = 0;
            Screen screen = Screen.AllScreens[screenId];
            string[] numbers = pos.Split('|');
            Point windowPoint = new Point(int.Parse(numbers[0]),
                int.Parse(numbers[1]));
            Size windowSize = f.Size;
            Rectangle winBounds;
            if (numbers.Length >= 4)
            {
                windowSize = new Size(int.Parse(numbers[2]),
                    int.Parse(numbers[3]));
                winBounds = new Rectangle(windowPoint, windowSize);
            } else
                winBounds = new Rectangle(windowPoint, f.Size);
            string windowString = "Normal";
            if (numbers.Length == 3 || numbers.Length == 5)
                windowString = numbers[numbers.Length - 1];
            if (windowString == "Normal")
            {

                bool locOkay = IsVisibleOnAnyScreen(windowPoint);
                bool okay = IsVisibleOnAnyScreen(winBounds);

                if (okay == true)
                {
                    f.StartPosition = FormStartPosition.Manual;
                    f.DesktopBounds = winBounds;
                    f.WindowState = FormWindowState.Normal;
                }
            }
            else if (windowString == "Maximized")
            {
                f.StartPosition = FormStartPosition.Manual;
                f.DesktopBounds = winBounds;
                f.WindowState = FormWindowState.Maximized;
            }
        }
        public static void StoreWindowPos(string name, Form f, bool storeSize, bool storeState)
        {
            string s = storeSize ? WindowPosSizeToString(f, storeState) : WindowPosToString(f, storeState);
            string s2 = GetString(name, "");
            if (s == s2) return;
            SetString(name, s);
            Screen sc = Screen.FromControl(f);
            int scIdx = 0, i = 0;
            foreach (Screen testScreen in Screen.AllScreens)
            {
                if (testScreen == sc) scIdx = i;
                i++;
            }
            SetInt(name + "Screen", scIdx);
        }
        public static void RestoreWindowPos(string name, Form f)
        {
            string s = GetString(name, "");
            if (s == "") return;
            StringToWindowPos(f, s, GetInt(name + "Screen", 0));
        }
        public class HistoryFile
        {
            public string file;
            public HistoryFile(string fname)
            {
                file = fname;
            }
            public override string ToString()
            {
                int p = file.LastIndexOf(Path.DirectorySeparatorChar);
                if (p < 0) return file;
                return file.Substring(p + 1);
            }
        }
        public class FilesHistory
        {
            public LinkedList<HistoryFile> list = new LinkedList<HistoryFile>();
            string name;
            int maxLength;
            public FilesHistory(string id, int max)
            {
                name = id;
                maxLength = max;
                string l = RegMemory.GetString(name, "");
                foreach (string fn in l.Split('|'))
                {
                    if (fn.Length > 0 && File.Exists(fn))
                    {
                        list.AddLast(new HistoryFile(fn));
                        if (list.Count == max) break;
                    }
                }
            }
            public void Save(string fname)
            {
                if (list.Count > 0 && list.First.Value.file == fname) return;
                foreach (HistoryFile f in list)
                {
                    if (f.file == fname)
                    {
                        list.Remove(f);
                        break;
                    }
                }
                list.AddFirst(new HistoryFile(fname));
                while (list.Count > maxLength)
                    list.RemoveLast();
                // Build string
                string store = "";
                foreach (HistoryFile f in list)
                    store += "|" + f.file;
                RegMemory.SetString(name, store.Substring(1));
            }
        }
    }
}
