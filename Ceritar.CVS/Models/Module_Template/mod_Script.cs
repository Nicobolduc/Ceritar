using System;
using System.Collections.Generic;

namespace Ceritar.CVS.Models.Module_Template
{
    internal class mod_Script : mod_HierarchyComponent
    {
        private String _strLocation;
        private ushort _intSequenceNo;

        public String Emplacement
        {
            get { return _strLocation; }
            set { _strLocation = value; }
        }

        public ushort SequenceNo
        {
            get { return _intSequenceNo; }
            set { _intSequenceNo = value; }
        }
    }
}
