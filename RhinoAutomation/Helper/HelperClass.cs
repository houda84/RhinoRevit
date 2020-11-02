using Inroduction;
using Rhino;
using Rhino.DocObjects.Tables;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhinoAutomation.Helper
{
    public static class HelperClass
    {
        public static int BakePoints(RhinoDoc doc, int count)
        {

            List<PointInfo> pInfos = PointInfo.Get_PointInfo(count);
            ObjectTable objectTable = doc.Objects;

            foreach (PointInfo pi in pInfos)
            {
                Point3d rhinoPoint = new Point3d(pi.X, pi.Y, pi.Z);
                Point3d rhinoPoint1 = new Point3d(pi.X, pi.Y, 100);

                Rhino.Geometry.Line ln = new Line(rhinoPoint, rhinoPoint1);

                objectTable.AddLine(ln);
                objectTable.AddPoint(rhinoPoint);
                objectTable.AddPoint(rhinoPoint1);
            }

            doc.Views.Redraw();

            return pInfos.Count;
        }

        public static List<Point3d> Bake_GridPoints(int countX, int countY)
        {
            //this is pointInfo list
            List<PointInfo> pInfos = PointInfo.Get_PointInfo(countX, countY);

            //Rhino geometry point3d list
            List<Point3d> points = new List<Point3d>();
            foreach (PointInfo p in pInfos)
            {
                Point3d pt = new Point3d(p.X, p.Y, p.Z);

                points.Add(pt);

            }

            return points;
        }

        public static List<Point3d> GetRandomPoints(int count, int xSize,
            int ySize, int zSize)
        {
            //this is pointInfo list
            List<PointInfo> pInfos = PointInfo.GenerateRandomPoints(count, xSize,
                ySize, zSize);

            //Rhino geometry point3d list
            List<Point3d> points = new List<Point3d>();
            foreach (PointInfo p in pInfos)
            {
                Point3d pt = new Point3d(p.X, p.Y, p.Z);

                points.Add(pt);

            }

            return points;
        }
    }
}
