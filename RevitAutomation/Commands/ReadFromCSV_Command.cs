
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

using Introduction;
using Introduction.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace RevitAutomation.Commands
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class ReadFromCSV_Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            
            //want to read from pointINfo.csv
            string folderPath = @"D:\WORK\MID-CD_CSharp Course\CSV_Files";
            string pFile = FileIO.CreateFilePath(folderPath, FileIO.pointInfos_FileName);
            string lFile = FileIO.CreateFilePath(folderPath, FileIO.lineInfos_FileName);
            //List<PointInfo> pInfos = PointInfo.ReadFrom_CSV(pFile);

            //read line info csv
            //and generate lineInfo object
            List<LineInfo> lnInfos = LineInfo.ReadFrom_CSV(lFile, pFile);


            //use that lineInfo object to create Revit line
     

            Autodesk.Revit.DB.Document doc = commandData.Application.ActiveUIDocument.Document;

            List<Line> revitLines = Helper.ConvertRevit.ConvertToRevitLines(lnInfos, doc,
                out List<Plane> planes);


            using (Transaction bake = new Transaction(doc, "bakeLines"))
            {
                bake.Start();
                for (int i = 0; i < revitLines.Count; i++)
                {
                    SketchPlane plane = SketchPlane.Create(doc, planes[i]);

                    // adding it to document
                    doc.Create.NewModelCurve(revitLines[i], plane);
                }

                bake.Commit();
            }

          

            return Result.Succeeded;
       
        }
    }
}
