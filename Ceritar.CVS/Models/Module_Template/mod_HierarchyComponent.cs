using System;
using System.Collections.Generic;

namespace Ceritar.CVS.Models.Module_Template
{
    internal abstract class mod_HierarchyComponent
    {
        private String _strName;

        public String Name
        {
            get { return _strName; }
            set { _strName = value; }
        }
    }
}
