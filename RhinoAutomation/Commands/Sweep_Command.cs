using System;
using System.Collections.Generic;
using System.Linq;
using Rhino;
using Rhino.Commands;
using Rhino.DocObjects;
using Rhino.Geometry;
using Rhino.Input;

namespace RhinoAutomation.Commands
{
    public class Sweep_Command : Command
    {
        static Sweep_Command _instance;
        public Sweep_Command()
        {
            _instance = this;
        }

        ///<summary>The only instance of the TestSweep command.</summary>
        public static Sweep_Command Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "Test_Sweep"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {

            return Result.Success;
        }
    }
}
