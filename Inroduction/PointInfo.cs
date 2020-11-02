
using System;
using System.Collections.Generic;

using System.IO;

namespace Inroduction
{
    /// <summary>
    /// this class deals with point
    /// </summary>
    public class PointInfo
    {
        #region Members
        //Memebers
        public double X;
        public double Y;
        public double Z;
        #endregion

        #region constructor
        //constructor
        public PointInfo(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        #endregion

        #region functions/Methods
        
        /// <summary>
        /// Gives csv stirng back
        /// </summary>
        /// <returns></returns>
        public string Get_CSVString()
        {
            string csv = $"{X}|{Y}|{Z}";

            // "3.45,5.0, 23"

            
            return csv;
        }

        /// <summary>
        /// Export data to csv
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="info"></param>
        public static void ExportToCSV(string filePath,List<string> info)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }


            File.WriteAllLines(filePath, info);
        }


        public static List<PointInfo> ReadFrom_CSV(string filePath)
        {
            List<PointInfo> pinfos = new List<PointInfo>();
            if (File.Exists(filePath)==false)
            {
                return pinfos;
            }
            
            //read all lines from csv
            string[] lines =  File.ReadAllLines(filePath);


            foreach (string stringValues in lines)
            {
                //1. Split sthe string using "," as a dilimiter
                string[] numberValues = stringValues.Split(new char[] { '|' });

                //2. check if the string array has returned 3 Values
                // and if string array at 0 is not double/int then go for next iteration
                if (numberValues.Length == 3)
                {
                    string xString = numberValues[0];
                    string yString = numberValues[1];
                    string zString = numberValues[2];

                    double xValue;
                    double yValue;
                    double zValue;

                    bool converted = double.TryParse(xString, out xValue);

                    if (converted == false)
                    {
                        continue;
                    }

                    double.TryParse(yString, out yValue);
                    double.TryParse(zString, out zValue);


                    //if it is double then cast it double and create pointInfo object

                    PointInfo pInfo = new PointInfo(xValue, yValue, zValue);

                    pinfos.Add(pInfo);

                }


            }

            return pinfos;
        }

        
        /// <summary>
        /// creates multiple points
        /// </summary>
        /// <param name="pointCounts"></param>
        /// <returns></returns>
        public static List<PointInfo> Get_PointInfo(int pointCounts)
        {
            //Empty PointInfo list
            List<PointInfo> newInofs = new List<PointInfo>();
            for (int i = 0; i < pointCounts; i++)
            {
                //New PointInfo object created for every for loop run
                PointInfo pi = new PointInfo(i, 0, 0);

                //Added to the pointInfo list
                newInofs.Add(pi);
            }

            return newInofs;
        }

        public static List<PointInfo> Get_PointInfo(int countX, int countY)
        {
            List<PointInfo> newInofs = new List<PointInfo>();

            //double sapcingX = 3;
            //double spacingY = 5;
            //x direction
            for (int i = 0; i < countX; i++)
            {

                //y direction
                for (int j = 0; j < countY; j++)
                {

                    PointInfo pi = new PointInfo(i, j, 0);

                    newInofs.Add(pi);
                }
            }

            return newInofs;
        }


        public static List<PointInfo> GenerateRandomPoints(int count,int xSize, int ySize, int zSize)
        {
            List<PointInfo> newInofs = new List<PointInfo>();

            Random rnd = new Random();


            int negX = -xSize;
            int negY = -ySize;
            int negZ = -zSize;

            for (int i = 0; i < count; i++)
            {
                int xCo = rnd.Next(negX, xSize);
                int yCo = rnd.Next(negY, ySize);
                int zCo = rnd.Next(negZ, zSize);


                PointInfo pI = new PointInfo(xCo, yCo, zCo);
                
                newInofs.Add(pI);
            }

            return newInofs;
        }

        #endregion

    }
}
