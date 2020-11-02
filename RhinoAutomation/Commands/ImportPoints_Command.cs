using System;
using System.Collections.Generic;
using Inroduction;
using Rhino;
using Rhino.Commands;

namespace RhinoAutomation.Commands
{
    public class ImportPoints_Command : Command
    {
        static ImportPoints_Command _instance;
        public ImportPoints_Command()
        {
            _instance = this;
        }

        ///<summary>The only instance of the ImportPoints_Command command.</summary>
        public static ImportPoints_Command Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "Test_ImportPoints"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            //file to export
            string filePath = @"D:\WORK\MID-CD_CSharp Course\CSV_Files\pointInfo.csv";

            List<PointInfo> pInfos = PointInfo.ReadFrom_CSV(filePath);


            foreach (PointInfo pi in pInfos)
            {
                Rhino.Geometry.Point3d rhinoPoint = new Rhino.Geometry.Point3d(pi.X, pi.Y, pi.Z);

                doc.Objects.AddPoint(rhinoPoint);
            }
            doc.Views.Redraw();
            return Result.Success;
        }
    }
}