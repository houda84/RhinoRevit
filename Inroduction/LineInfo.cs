

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Introduction
{
    public class LineInfo
    {
        public PointInfo StartPoint;
        public PointInfo EndPoint;

        public int Red;
        public int Blue;
        public int Green;

        private static Random rand = new Random();

        public LineInfo()
        {

        }
        public LineInfo(PointInfo startPt, PointInfo endPt)
        {
            this.StartPoint = startPt;
            this.EndPoint = endPt;

            SetRandomRGB_Data();
        }

        private void SetRandomRGB_Data()
        {
            this.Red = rand.Next(0, 255);
            this.Blue = rand.Next(0, 255);
            this.Green = rand.Next(0, 255);
        }

        public double LineLength()
        {
            double xSqr = Math.Pow(EndPoint.X - StartPoint.X, 2);
            double ySqr = Math.Pow(EndPoint.Y - StartPoint.Y, 2);
            double zSqr = Math.Pow(EndPoint.Z - EndPoint.Z, 2);

            return Math.Sqrt(xSqr + ySqr + zSqr);
        }

        public string Get_CSVString(List<PointInfo> points)
        {
            int startPoint_Index = FindIndex(points, StartPoint);
            int endPoint_Index = FindIndex(points, EndPoint);

            string lineInfoString = $"{startPoint_Index}|{endPoint_Index}|{LineLength()}|{Red}|{Green}|{Blue}";

            return lineInfoString;
        }

        private int FindIndex(List<PointInfo> points, PointInfo point)
        {
            //return points.FindIndex(p => p.X == point.X && p.Y == point.Y && p.Z == point.Z);

            for(int i=0;i<points.Count;i++)
            {
                if (point.X == points[i].X && point.Y == points[i].Y && point.Z == points[i].Z)
                {
                    return i;
                }
            }

            return -1;
        }

        public static LineInfo GetLineInfo_FromString(string s,List<PointInfo>points)
        {
            LineInfo ln = new LineInfo();
            string[] cols = s.Split(new char[] { '|' });

            if (int.TryParse(cols[0], out int startIndex))
                  ln.StartPoint = points[startIndex];

            if (int.TryParse(cols[1], out int endIndex))
                ln.EndPoint = points[endIndex];

            if (int.TryParse(cols[2], out int red))
                ln.Red = red;

            if (int.TryParse(cols[2], out int blue))
                ln.Blue = blue;

            if (int.TryParse(cols[2], out int green))
                ln.Green = green;

            return ln;
        }

    }
}
