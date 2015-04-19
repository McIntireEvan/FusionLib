using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace FusionLib.Utils
{
    /// <summary>
    /// A utils class containing things related to performance such as memory usage and framerate.
    /// </summary>
    public static class Performance
    {
        /// <summary>
        /// Get the current framerate
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public static double GetFramerate(GameTime g)
        {
            return 1 / g.ElapsedGameTime.TotalSeconds;
        }

        /// <summary>
        /// Get the current memory usage
        /// </summary>
        /// <param name="suffix">Suffix of byte amound (b, kb, mb)</param>
        /// <returns></returns>
        public static double GetMemoryUsage(String suffix)
        {
            suffix = suffix.ToLower();
            Process proc = Process.GetCurrentProcess();
            double mem = proc.PrivateMemorySize64;
            if (suffix.Equals("b"))
            {
                return mem;
            }
            else if (suffix.Equals("kb"))
            {
                return mem / 1024;
            }
            else if (suffix.Equals("mb"))
            {
                return (mem / 1024) / 1024;
            }
            else
            {
                return proc.PrivateMemorySize64;
            }
        }
    }
}