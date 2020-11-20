using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB.Structure;

namespace RevitAutomation.Helper
{
    public static class FilterData
    {
        public static FamilySymbol GetStructuralFrame(Document doc,string familySymbolName)
        {
            Element element = new FilteredElementCollector(doc)
                .OfClass(typeof(FamilySymbol))
                .FirstOrDefault(f => f.Name == familySymbolName);


            return element as FamilySymbol;
        }

        public static Level GetLevel(Document doc, string levelName)
        {
            Element element = new FilteredElementCollector(doc)
                .OfClass(typeof(Level))
                .FirstOrDefault(f => f.Name == levelName);


            Level lvl = element as Level;

            return lvl;
        }
    }
}
