using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphis
{
    [Serializable]
    internal class Line2
    {
        public string StartX;


        public Line2 copy (Line line, Line2 line2)
        {
            line2.StartX = line.Start.X.ToString();
            line2.StartX += ",";
            line2.StartX += line.Start.Y.ToString();
            line2.StartX += ",";
            line2.StartX += line.StartZ.ToString();
            line2.StartX += ",";
            line2.StartX += line.End.X.ToString();
            line2.StartX += ",";
            line2.StartX += line.End.Y.ToString();
            line2.StartX += ",";
            line2.StartX += line.EndZ.ToString();
            return line2;
        }

        public static Line read(Line line, Line2 x) 
        {
            String[] substrings = x.StartX.Split(',');
            foreach (string substring in substrings)
            {
                int number = Int32.Parse(substring);
                line.Start.X = number;
            }
            return line;
        }
    }
}
