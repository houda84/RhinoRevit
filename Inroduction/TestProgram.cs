﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction
{
    public  class TestProgram
    {
        public static void Main()
        {
            //this is a empting string list
            List<string> sInfos = new List<string>();
            sInfos.Add("X_Co|Y_Co|Z_Co");  // add tags to list

            PointInfo inP = new PointInfo(0, 0, 0);

            List<PointInfo> pInfos = PointInfo.Get_PointInfo(10);

            //for every pointInfo  get its csv string 
            //And add to string list
            foreach (PointInfo pi in pInfos)
            {
                string csvString = pi.Get_CSVString();
                sInfos.Add(csvString);

                Console.WriteLine(csvString);
            }

            //file to export
            string filePath = @"D:\WORK\MID-CD_CSharp Course\CSV_Files\pointInfo.csv";

            //Export to csv
            Introduction.Helper.FileIO.ExportToCSV(filePath, sInfos);

            Console.ReadLine();
        }
    }
}
