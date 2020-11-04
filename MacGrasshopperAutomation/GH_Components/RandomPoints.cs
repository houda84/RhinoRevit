using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

using RhinoAutomation.Helper;

namespace GrasshopperAutomation.GH_Components
{
    public class RandomPoints : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the RandomPoints class.
        /// </summary>
        public RandomPoints()
          : base("RandomPoints", "randomPts",
              "Creates random points",
              "GrasshopperAutomation", "Points")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            //bool - simulate //0
            //1 bool parameter
            pManager.AddBooleanParameter("Simulate", "sim", "Its going to simulate geometry", GH_ParamAccess.item);

            //region x - 1
            pManager.AddNumberParameter("Xsize", "xSize", "size of x ", GH_ParamAccess.item);

            //region y - 2
            pManager.AddNumberParameter("Ysize", "ySize", "size of y ", GH_ParamAccess.item);

            //region z - 3
            pManager.AddNumberParameter("Zsize", "zSize", "size of z ", GH_ParamAccess.item);

            //point count - 4
            pManager.AddIntegerParameter("PointCount", "ptCount", "number of points", GH_ParamAccess.item,10);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("Points", "pts", "Random Points", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            bool sim = false;
            double xSize = 0;
            double ySize = 1;
            double zSize = 0;
            int count = 0;

            if (DA.GetData(0, ref sim) == false) return;
            if (DA.GetData(1, ref xSize) == false) return;
            if (DA.GetData(2, ref ySize) == false) return;
            if (DA.GetData(3, ref zSize) == false) return;

            DA.GetData(4, ref count);

            List<Point3d> points =
                HelperClass.GetRandomPoints(count,(int)xSize, (int)ySize, (int)zSize);

            if (sim)
                this.ExpireSolution(true);


            DA.SetDataList(0,points);
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
            get { return new Guid("02ffff7b-cf16-4ed1-bfc7-45f97e4d8eb7"); }
        }
    }
}