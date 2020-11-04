using System;
using System.Drawing;
using Grasshopper;
using Grasshopper.Kernel;

namespace MacGrasshopperAutomation
{
    public class MacGrasshopperAutomationInfo : GH_AssemblyInfo
    {
        public override string Name => "MacGrasshopperAutomation Info";

        //Return a 24x24 pixel bitmap to represent this GHA library.
        public override Bitmap Icon => null;

        //Return a short string describing the purpose of this GHA library.
        public override string Description => "";

        public override Guid Id => new Guid("C20F1C18-1878-4929-9EC0-DC3D5009D14C");

        //Return a string identifying you or your company.
        public override string AuthorName => "";

        //Return a string representing your preferred contact details.
        public override string AuthorContact => "";
    }
}