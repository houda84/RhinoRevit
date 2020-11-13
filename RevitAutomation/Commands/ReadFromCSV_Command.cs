using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Introduction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace RevitAutomation.Commands
{
    public class ReadFromCSV_Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            List<PointInfo> infos = PointInfo.ReadFrom_CSV("sads");

            List<XYZ> points = new List<XYZ>();

            infos.ForEach(i => points.Add(new XYZ(i.X, i.Y, i.Z)));

            return Result.Succeeded;
        }
    }
}
