using System;
using System.Drawing;
using Grasshopper;
using Grasshopper.Kernel;

namespace LinesFromRandomPts
{
    public class LinesFromRandomPtsInfo : GH_AssemblyInfo
    {
        public override string Name => "LinesFromRandomPts Info";

        //Return a 24x24 pixel bitmap to represent this GHA library.
        public override Bitmap Icon => null;

        //Return a short string describing the purpose of this GHA library.
        public override string Description => "";

        public override Guid Id => new Guid("88EB380E-2C3B-4BC3-BE28-9922ECC055F7");

        //Return a string identifying you or your company.
        public override string AuthorName => "";

        //Return a string representing your preferred contact details.
        public override string AuthorContact => "";
    }
}