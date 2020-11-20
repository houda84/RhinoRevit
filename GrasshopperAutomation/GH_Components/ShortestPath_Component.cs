using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Grasshopper.Kernel;
using Introduction;
using Introduction.Helper;
using Rhino.DocObjects;
using Rhino.Geometry;
using RhinoAutomation.Helper;

namespace GrasshopperAutomation.GH_Components
{
    public class ShortestPath_Component : GH_Component
    {
        //assign file names

        /// <summary>
        /// Initializes a new instance of the ShortestPath_Component class.
        /// </summary>
        public ShortestPath_Component()
          : base("ShortestPath", "shortestPath",
              "gives the shorts path from start destination to all points",
              "GrasshopperAutomation", "Lines")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("ExportData", "export", "export to csv to given folder", GH_ParamAccess.item,false);
            pManager.AddBooleanParameter("Bake", "bake", "bakes the line geometry", GH_ParamAccess.item, false);
            pManager.AddTextParameter("FolderPath", "folderPath", "files are saved in this location", GH_ParamAccess.item,"no path given");

            pManager.AddPointParameter("start point", "startPt", "", GH_ParamAccess.item);
            pManager.AddPointParameter("target points", "targetPts", "", GH_ParamAccess.list);


        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddLineParameter("Lines", "lns", "", GH_ParamAccess.list);
            pManager.AddPointParameter("Points", "pts", "ordered points follows shortes path logic", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            #region Getting Inputs from user
            bool export = false;
            bool bake = false;
            string folderPath = "";
            Point3d startPt = Point3d.Origin;
            List<Point3d> points = new List<Point3d>();

            DA.GetData(0, ref export);
            DA.GetData(1, ref bake);
            if (export)
            {
                if (!DA.GetData(2, ref folderPath)) return;
            }
            if (!DA.GetData(3, ref startPt)) return;
            if (!DA.GetDataList(4, points)) return;
            #endregion

            //Generate line network (this can be a any logic, not necessarily a short path or something)
            List<Point3d> orderedPts;
            List<Line> lns = HelperClass.MakeShortestPath(startPt, points,out orderedPts);

            //either export is true OR bake is true
            if (export||bake)
            {

                List<LineInfo> lineInfos = ConverTo_LineInfo(lns);

                //bake to Rhino
                if (bake)
                {
                    for (int i = 0; i < lns.Count; i++)
                    {
                        Line ln = lns[i];
                        LineInfo li = lineInfos[i];

                        ObjectAttributes objAtt = new ObjectAttributes();
                        objAtt.ColorSource = Rhino.DocObjects.ObjectColorSource.ColorFromObject;
                        objAtt.ObjectColor = System.Drawing.Color.FromArgb(li.Red, li.Green, li.Blue);
                        
                        Rhino.RhinoDoc.ActiveDoc.Objects.AddLine(ln, objAtt);
                    }
                }

                //do this only if export is true
                if (export)
                {
                    List<PointInfo> ptInfos = ConverTo_PointInfo(orderedPts);

                    string ptInfoFilePath = Introduction.Helper.FileIO.CreateFilePath(folderPath, FileIO.pointInfos_FileName);
                    string lnInfoFilePath = FileIO.CreateFilePath(folderPath, FileIO.lineInfos_FileName);

                    if (ptInfoFilePath == null || lnInfoFilePath == null)
                        return;

                    // c#  lambda expression (System.Linq)
                    List<string> ptStrings = ptInfos.Select(p => p.Get_CSVString())
                        .ToList();

                    //foreach (PointInfo pi in ptInfos)
                    //    ptStrings.Add(pi.Get_CSVString());
                    

                    List<string> lnsString = lineInfos.Select(ln => ln.Get_CSVString(ptInfos))
                        .ToList();

                    //add coulmn names
                    ptStrings.Insert(0, "X|Y|Z");
                    lnsString.Insert(0, "StartPoint_Index|EndPoint_Index|Length|Red|Blue|Green");


                    FileIO.ExportToCSV(ptInfoFilePath, ptStrings);
                    FileIO.ExportToCSV(lnInfoFilePath, lnsString);
                }
            }

            DA.SetDataList(0, lns);
            DA.SetDataList(1, orderedPts);
        }

        private List<PointInfo> ConverTo_PointInfo(List<Point3d> points)
        {
            List<PointInfo> ptInfos = new List<PointInfo>();

            points.ForEach(pt => ptInfos.Add(new PointInfo(pt.X, pt.Y, pt.Z)));

            return ptInfos;
        }

        private void GetPointInfo_ofLine(Line ln, out PointInfo startPoint, out PointInfo endPoint)
        {
            startPoint = new PointInfo(ln.FromX, ln.FromY, ln.FromZ);
            endPoint = new PointInfo(ln.ToX, ln.ToY, ln.ToZ);
        }

        private List<LineInfo> ConverTo_LineInfo(List<Line> lines)
        {
            List<LineInfo> lnInfos = new List<LineInfo>();

            foreach (Line line in lines)
            {
                PointInfo startPt, endPt;
                GetPointInfo_ofLine(line, out startPt, out endPt);

                LineInfo ln = new LineInfo(startPt, endPt);
                lnInfos.Add(ln);
            }
            return lnInfos;
        }



        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("e30791f4-7fc6-479c-b837-71afef2bae17"); }
        }
    }
}