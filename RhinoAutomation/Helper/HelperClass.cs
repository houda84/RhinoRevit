using Introduction;
using Rhino;
using Rhino.DocObjects.Tables;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
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

        public static List<Point3d> GetRandomPoints(int count, int xSize, int ySize, int zSize,out List<Line> lines)
        {
            lines = new List<Line>();
            List<PointInfo> pInfos = PointInfo.GenerateRandomPoints(count*2, xSize,
                                ySize, zSize);
            

            List<Point3d> points = new List<Point3d>();
            foreach (PointInfo p in pInfos)
            {
                Point3d pt = new Point3d(p.X, p.Y, p.Z);

                points.Add(pt);

            }


            List<Point3d> pts = new List<Point3d>(points);

            Random rand = new Random();
            
            while (points.Count >= 2)
            {
                count = points.Count;

                int p1Index = rand.Next(0, count);
                int p2Index = rand.Next(0, count);
                if (p1Index != p2Index)
                {
                    Point3d p1 = points[p1Index];
                    Point3d p2 = points[p2Index];

                    Line ln = new Line(p1, p2);

                    lines.Add(ln);

                    points.Remove(p1);
                    points.Remove(p2);
                }

            }

            return pts;
        }

        public static List<Line> MakeShortestPath(Point3d startPt,List<Point3d> points,out List<Point3d> orderedPoints)
        {
            List<Line> lns = new List<Line>();
            orderedPoints = new List<Point3d>();
            List<Point3d> pts = new List<Point3d>(points);


            Point3d p1 = startPt;
            //p2 is closest point to p1
            Point3d p2 = FindClosestPoint(p1, points);

            Line ln = new Line(p1, p2);
            lns.Add(ln);
            orderedPoints.Add(p1);
            orderedPoints.Add(p2);

            while (points.Count >= 2)
            {
               
                points.Remove(p1);
                points.Remove(p2);

                p1 = p2;
                p2 = FindClosestPoint(p1, points);

                ln = new Line(p1, p2);
                lns.Add(ln);

                orderedPoints.Add(p2);
                //orderedPoints.Add(p2);


            }
            return lns;
        }

        private static Point3d FindClosestPoint(Point3d pointCloseTo, List<Point3d> pts)
        {
            double min = double.MaxValue;
            Point3d closestPoint = Point3d.Unset;

            List<Point3d> points = new List<Point3d>(pts);

            points.Remove(pointCloseTo);

            for (int i = 0; i < points.Count; i++)
            {
                Point3d pTest = points[i];
                double dist = pointCloseTo.DistanceToSquared(pTest);
                if (dist < min)
                {
                    min = dist;
                    closestPoint = points[i];
                }
            }

            return closestPoint;
        }

    }
}
