using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4Rassohin
{
    class Program
    {
        static void Main(string[] args)
        {
            ClipClass clip = new ClipClass();
            clip.clip();
            Console.WriteLine("x0: {0}, y0: {1}, x1: {2}, y1: {3}", Convert.ToString(clip.x0), Convert.ToString(clip.y0), Convert.ToString(clip.x1), Convert.ToString(clip.y1));
            Console.ReadLine();

        }
    }
}
