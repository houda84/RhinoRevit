using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Grasshopper.Kernel;
using Rhino.DocObjects;
using rh = Rhino.Geometry;
using RhinoAutomation.Helper;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

using RhinoInside.Revit;
using RhinoInside.Revit.Convert.Geometry;

namespace GrasshopperAutomation.GH_Components
{
    public class ShortestPath_Component : GH_Component
    {
        static List<Guid> oldLines = new List<Guid>();
        static List<ElementId> oldIds = new List<ElementId>();
        /// <summary>
        /// Initializes a new instance of the ShortestPath_Component class.
        /// </summary>
        public ShortestPath_Component()
          : base("ShortestPath", "shortestPath",
              "gives the shorts path from start destination to all points",
              "Revit", "Lines")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("Bake", "bake", "bakes the line geometry", GH_ParamAccess.item, false);

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

            pManager.AddGenericParameter("RevitLines", "rvtLines", "models curves in revit doc", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            #region Getting Inputs from user
            bool bake = false;
            rh.Point3d startPt = rh.Point3d.Origin;
            List<rh.Point3d> points = new List<rh.Point3d>();

            DA.GetData(0, ref bake);

            if (!DA.GetData(1, ref startPt)) return;
            if (!DA.GetDataList(2, points)) return;
            #endregion

            //Generate line network (this can be a any logic, not necessarily a short path or something)
            List<rh.Point3d> orderedPts;
            List<rh.Line> lns = HelperClass.MakeShortestPath(startPt, points, out orderedPts);
            List<Line> rvtLines = lns.Select(l => l.ToLine()).ToList();
            //Get revit document from RhinoInside 
            Document docRevit = RhinoInside.Revit.Revit.ActiveDBDocument; //revit document
            Rhino.RhinoDoc docRhino = Rhino.RhinoDoc.ActiveDoc; //rhino document


            if (bake)
            {
                if (oldLines.Count > 0 && oldIds.Count > 0)
                {
                    docRhino.Objects.Delete(oldLines, true);

                    using (Transaction tra = new Transaction(docRevit, "delete lines"))
                    {
                        tra.Start();

                        oldIds = oldIds.Where(o => docRevit.GetElement(o) != null)
                            .ToList();

                        docRevit.Delete(oldIds);
                        tra.Commit();
                    }
                       
                    oldIds.Clear();
                    oldLines.Clear();
                }

                Random rand = new Random();
                for (int i = 0; i < lns.Count; i++)
                {
                    rh.Line ln = lns[i];

                    ObjectAttributes objAtt = new ObjectAttributes();
                    objAtt.ColorSource = Rhino.DocObjects.ObjectColorSource.ColorFromObject;
                    objAtt.ObjectColor = System.Drawing.Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));

                    Guid oldId = docRhino.Objects.AddLine(ln, objAtt);
                    oldLines.Add(oldId);
                }

                if (!docRevit.IsFamilyDocument)
                {

                    List<Plane> planes = rvtLines
                        .Select(l => Plane.CreateByThreePoints(XYZ.Zero, l.GetEndPoint(0), l.GetEndPoint(1)))
                        .ToList();
     
                    List<ModelCurve> modelCurves = BakeToRevit(docRevit, rvtLines, planes);
                    modelCurves.ForEach(m => oldIds.Add(m.Id));
                }

            }

            DA.SetDataList(0, lns);
            DA.SetDataList(1, orderedPts);
            DA.SetDataList(2, rvtLines);
        }

        private List<ModelCurve> BakeToRevit(Document doc, List<Line> revitLns,List<Plane> planes)
        {
            List<ModelCurve> mCs = new List<ModelCurve>();
            using (Transaction bake = new Transaction(doc, "Bake modelCurves"))
            {
                bake.Start();
                for (int i = 0; i < revitLns.Count; i++)
                {
                    SketchPlane sktPlane = SketchPlane.Create(doc, planes[i]);
                    ModelCurve c = doc.Create.NewModelCurve(revitLns[i], sktPlane);
                    mCs.Add(c);
                }
                bake.Commit();
            }
            return mCs;
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
            get { return new Guid("9C19ACE6-299D-4B3A-ADF9-617B7387FC7E"); }
        }
    }
}