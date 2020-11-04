using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace GrasshopperAutomation.GH_Components
{
    public class GridPoints : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GridPoints class.
        /// </summary>
        public GridPoints()
          : base("GridPoints", "gridPts",
              "Genarate grid of points",
              "GrasshopperAutomation", "Points")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            //1 bool parameter
            pManager.AddBooleanParameter("Bake", "bake", "Its going to bake geometry", GH_ParamAccess.item);

            // 2 integer parameters
            pManager.AddIntegerParameter("X_Count", "xCount", "point count in x direction", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Y_Count", "yCount", "Point count in y direction", GH_ParamAccess.item);

           // pManager.AddNumberParameter()
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("Points", "pts", "Grid of Points", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            //bool baking;
            bool bake = false;
            int xCount = 0;
            int yCount = 0;

           // double spaceX = 0;

            bool inputCorrect = DA.GetData(0, ref bake);
            if (inputCorrect == false)
            {
                return;
            }

            if (!DA.GetData(1, ref xCount)) return;
            if (!DA.GetData(2, ref yCount)) return;
            //if (!DA.GetData(3, ref spaceX)) return;

            //put your logic here
            List<Point3d> points = RhinoAutomation.Helper.HelperClass.Bake_GridPoints(xCount, yCount);

            //To Bake Logic
            if (bake == true)
            {
                Rhino.RhinoDoc doc = Rhino.RhinoDoc.ActiveDoc;

                foreach (Point3d pt in points)
                {
                    doc.Objects.AddPoint(pt);
                }
                
            }

            DA.SetDataList(0, points);
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
            get { return new Guid("55ecffbe-1dc6-4361-aa6c-c3a24acf3d85"); }
        }
    }
}