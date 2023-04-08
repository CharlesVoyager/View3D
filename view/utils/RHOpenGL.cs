using OpenTK.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace View3D.view.utils
{
    public class RHOpenGL : OpenTK.GLControl
    {
        public RHOpenGL() 
            : base(new GraphicsMode(32, 24, 8, 4), 2, 0, GraphicsContextFlags.ForwardCompatible)
        {
        }
        public static int MaxAntiAlias()
        {
            var aa_modes = new List<int>();
            int aa = 0;
            do
            {
                GraphicsMode mode = new GraphicsMode(32, 24, 0, aa);
                Console.WriteLine("Samples:"+mode.Samples);
                if (!aa_modes.Contains(mode.Samples))
                    aa_modes.Add(aa);
                aa += 2;
            } while (aa <= 32);
            int best = aa_modes.Last();
            Console.WriteLine("Best AntiAlias:" + best);
            return best;
        }
    }
}
