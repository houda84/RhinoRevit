using System;
using System.Collections.Generic;
using Inroduction;
using Rhino;
using Rhino.Commands;
using Rhino.Input;

namespace RhinoAutomation.Commands
{
    public class ExportPoints_Command : Command
    {
        static ExportPoints_Command _instance;
        public ExportPoints_Command()
        {
            _instance = this;
        }

        ///<summary>The only instance of the ExportPoints_Command command.</summary>
        public static ExportPoints_Command Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "Test_ExportPoints"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            //declare a variable
            int count = 10;

            //ask user to enter input and assing it to count
            Result result = RhinoGet.GetInteger("Give point count as Integer", true, ref count);

            if (count < 0)
            {
                return Result.Failure;
            }



            List<string> sInfos = new List<string>();
            sInfos.Add("X_Co|Y_Co|Z_Co");  // add tags to list

            PointInfo inP = new PointInfo(0, 0, 0);

            List<PointInfo> pInfos = PointInfo.Get_PointInfo(count);

            //for every pointInfo  get its csv string 
            //And add to string list
            foreach (PointInfo pi in pInfos)
            {
                string csvString = pi.Get_CSVString();
                sInfos.Add(csvString);
            }

            //file to export
            string filePath = @"D:\WORK\MID-CD_CSharp Course\CSV_Files\pointInfo.csv";

            //Export to csv
            PointInfo.ExportToCSV(filePath, sInfos);

            Rhino.RhinoApp.WriteLine($"{count} Points exported to CSV File");
            Rhino.UI.Dialogs.ShowMessage($"{count} Points exported to CSV File", "Information");

            return Result.Success;
        }
    }
}