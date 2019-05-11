using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolaPresents
{
    class Program
    {
        //Glow
        static bool GlowActive = true;
     
        // Radar
        static bool RadarHackActive = true;

        // Flash
        static bool FlashHackActive = false;
        
        static void Main(string[] args)
        {
            if (GlowActive)
            {
                // Start GlowHack
                Process process = Process.Start(".\\CSGH.exe");
                //GlowHack.Program.Main(null);
            }

            if (RadarHackActive)
            {
                // Start RadarHack 
                Process process = Process.Start(".\\CSRH.exe"); 
                //RadarHack.Program.Main(null);
            }

            if (FlashHackActive)
            {
                // Start RadarHack 
                Process process = Process.Start(".\\CSFH.exe");
                //RadarHack.Program.Main(null);
            }
        }
    }
}
