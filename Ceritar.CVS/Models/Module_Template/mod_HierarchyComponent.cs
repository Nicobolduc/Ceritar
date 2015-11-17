using System;
using System.Collections.Generic;

namespace Ceritar.CVS.Models.Module_Template
{
    internal abstract class mod_HierarchyComponent
    {
        //Model attributes
        private String _strNameOnDisk;

        //Working variables


#region "Properties"

        public String NameOnDisk
        {
            get { return _strNameOnDisk; }
            set { _strNameOnDisk = value; }
        }

#endregion

    }
}
