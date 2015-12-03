using System;
using System.Collections.Generic;

namespace Ceritar.CVS.Models.Module_Template
{
    internal class mod_Script : mod_HiCo_HierarchyComponent
    {
        //Model attributes
        private String _strLocation;
        private ushort _intScriptSequenceNo;

        //Working variables


#region "Properties"

        internal String Emplacement
        {
            get { return _strLocation; }
            set { _strLocation = value; }
        }

        internal ushort ScriptSequenceNo
        {
            get { return _intScriptSequenceNo; }
            set { _intScriptSequenceNo = value; }
        }

#endregion


        internal clsActionResults Validate()
        {
            throw new NotImplementedException();
        }

        internal override bool blnSave()
        {
            throw new NotImplementedException();
        }
    }
}
