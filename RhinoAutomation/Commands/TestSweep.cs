using System;
using System.Collections.Generic;
using System.Linq;
using Rhino;
using Rhino.Commands;
using Rhino.DocObjects;
using Rhino.Geometry;
using Rhino.Input;

namespace RhinoAutomation.Commands
{
    public class TestSweep : Command
    {
        static TestSweep _instance;
        public TestSweep()
        {
            _instance = this;
        }

        ///<summary>The only instance of the TestSweep command.</summary>
        public static TestSweep Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "TestSweep"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            // TODO: complete command.
            RhinoGet.GetOneObject("selcte profile section",true, ObjectType.InstanceReference, out ObjRef objRef);

            RhinoGet.GetOneObject("Select Profile Path", true, ObjectType.Curve, out ObjRef oRef);

            Polyline pl = null;
            oRef?.Curve().TryGetPolyline(out pl);

            if (pl==null)
                return Result.Failure;


            if(objRef.Object() is InstanceObject block)
            {
                List<Curve> cvs = block.GetSubObjects()
                    .Where(c => c.Geometry is Curve)
                    .Select(c => c.Geometry as Curve)
                    .OrderByDescending(c => AreaMassProperties.Compute(c).Area)
                    .ToList();

                Curve outline = cvs[0];

                outline.TryGetPlane(out Plane profileBasePlane);

                pl.SegmentAt(0).ToNurbsCurve().PerpendicularFrameAt(0, out Plane polyLinePlane);

                Transform xForm = Transform.PlaneToPlane(profileBasePlane, polyLinePlane);
                Brep outerBrep = null;
                int i = 0;
                List<Brep> otherBrps = new List<Brep>();
                foreach (Curve curve in cvs)
                {
                    curve.Transform(xForm);

                    Brep[] brps = Brep.CreateFromSweep(pl.ToNurbsCurve(), curve, pl.IsClosed, doc.ModelAbsoluteTolerance);

                     if (brps == null)
                        continue;

                    if (brps.Length == 1)
                    {
                        if (i == 0)
                        {
                            if (!pl.IsClosed)
                            {
                                outerBrep = brps[0].CapPlanarHoles(doc.ModelAbsoluteTolerance);
                                i++;
                            }
                            else
                            {
                                outerBrep = brps[0];
                                i++;
                            }

                        }
                        else if (!pl.IsClosed)
                        {

                            Brep b = brps[0].CapPlanarHoles(doc.ModelAbsoluteTolerance);
                            if (b != null)
                                otherBrps.Add(b);
                        }
                        else if (pl.IsClosed)
                        {
                            Brep b = brps[0];
                            if (b != null)
                                otherBrps.Add(b);
                        }

                    }
                    else
                    {
                        foreach (Brep brep in brps)
                        {
                            doc.Objects.AddBrep(brep);
                        }
                    }




                }

                if (outerBrep != null)
                {
                    foreach (Brep b in otherBrps)
                    {
                        if (outerBrep.SolidOrientation != b.SolidOrientation)
                            b.Flip();

                       Brep[] brps = Brep.CreateBooleanDifference(outerBrep, b, doc.ModelAbsoluteTolerance);
                        if (brps == null)
                            return Result.Failure;
                        if (brps.Length == 1)
                            outerBrep = brps[0];
                    }

                    doc.Objects.AddBrep(outerBrep);
                    doc.Views.Redraw();
                }
            }



            return Result.Success;
        }
    }
}