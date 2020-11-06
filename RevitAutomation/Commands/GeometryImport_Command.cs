using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAutomation.Commands
{
    class GeometryImport_Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;

            SATImportOptions satOptions = new SATImportOptions();
            satOptions.Placement = ImportPlacement.Origin;

            string filePath = "";
            ElementId elmId = doc.Import(filePath,satOptions,doc.ActiveView);

            if (elmId == null)
                return Result.Failed;



            return Result.Succeeded;
        }
    }
}
