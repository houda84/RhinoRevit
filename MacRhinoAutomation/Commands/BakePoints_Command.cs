using System;
using System.Collections.Generic;
using Inroduction;
using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;
using Rhino.DocObjects;
using Rhino.DocObjects.Tables;

namespace RhinoAutomation.Commands
{
    public class BakePoints_Command : Command
    {
        public BakePoints_Command()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static BakePoints_Command Instance
        {
            get; private set;
        }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName
        {
            get { return "Test_BakePoints"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            //declare a variable
           int countX = 10;
            int countY = 10;
            //ask user to enter input and assing it to count
            Result result =  RhinoGet.GetInteger("Give point count for X direction", true, ref countX);

            RhinoGet.GetInteger("Give point count for Y direction", true, ref countY);

            if (countX < 0 || countY < 0)
            {
                return Result.Failure;
            }

            //int bakedCount = RhinoAutomation.Helper.HelperClass.BakePoints(doc, count);

            List<Point3d> pts = Helper.HelperClass.Bake_GridPoints(countX, countY);

            foreach (Point3d point in pts)
            {
                doc.Objects.AddPoint(point);
            }

            RhinoApp.WriteLine($"{pts.Count} points baked");

            return Result.Success;
        }
    }
}
