using Autodesk.Revit.DB;
using Introduction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAutomation.Helper
{
    public static class ConvertRevit
    {
        public static List<Line> ConvertToRevitLines(List<LineInfo> lnsInfo, Document doc, out List<Plane> planes)
        {
            List<Line> lines = new List<Line>();
            planes = new List<Plane>();

            foreach (LineInfo line in lnsInfo)
            {
                XYZ startPt = new XYZ(line.StartPoint.X/304.8, line.StartPoint.Y/304.8, line.StartPoint.Z/304.8);

                XYZ endPt = new XYZ(line.EndPoint.X / 304.8, line.EndPoint.Y / 304.8, line.EndPoint.Z / 304.8);

                Line ln = Line.CreateBound(startPt, endPt);
                //Curve curve = ln.Clone();

                Plane pln = Plane.CreateByThreePoints(XYZ.Zero, startPt, endPt);
                
                lines.Add(ln);
                planes.Add(pln);
            }
            return lines;
        }

        public static List<Line> ConvertToRevitLines(List<Rhino.Geometry.Line> lnsInfo, Document doc, out List<Plane> planes)
        {
            List<Line> lines = new List<Line>();
            planes = new List<Plane>();

            foreach (Rhino.Geometry.Line line in lnsInfo)
            {
                XYZ startPt = new XYZ(line.FromX / 304.8, line.FromY / 304.8, line.FromZ / 304.8);
                XYZ endPt = new XYZ(line.ToX / 304.8, line.ToY / 304.8, line.ToZ / 304.8);

                Line ln = Line.CreateBound(startPt, endPt);

                Plane pln = Plane.CreateByThreePoints(XYZ.Zero, startPt, endPt);

                lines.Add(ln);
                planes.Add(pln);
            }
            return lines;
        }
    }
}
