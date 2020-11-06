using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;
using RhinoAutomation.Helper;

namespace GrasshopperAutomation.GH_Components
{
    public class ShortestPath_Component : GH_Component
    {
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
            Point3d startPt = Point3d.Origin;
            List<Point3d> points = new List<Point3d>();

            if (!DA.GetData(0, ref startPt)) return;
            if (!DA.GetDataList(1, points)) return;

            List<Line> lns = HelperClass.MakeShortestPath(startPt, points,out List<Point3d> orderedPts);

            DA.SetDataList(0, lns);
            DA.SetDataList(1, orderedPts);
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