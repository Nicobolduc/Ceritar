using System;
using System.Collections.Generic;

namespace Ceritar.CVS.Models.Module_Template
{
    internal class mod_Script : mod_HierarchyComponent
    {
        //Model attributes
        private String _strLocation;
        private ushort _intSequenceNo;

        //Working variables


#region "Properties"

        internal String Emplacement
        {
            get { return _strLocation; }
            set { _strLocation = value; }
        }

        internal ushort SequenceNo
        {
            get { return _intSequenceNo; }
            set { _intSequenceNo = value; }
        }

#endregion

    }
}
