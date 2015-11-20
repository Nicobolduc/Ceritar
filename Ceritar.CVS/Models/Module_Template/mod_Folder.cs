using System.Collections.Generic;

namespace Ceritar.CVS.Models.Module_Template
{
    internal class mod_Folder : mod_HierarchyComponent
    {
        //Model attributes
        private FolderType _folderType;
        private List<mod_HierarchyComponent> _lstChildrensComponents;
        private mod_HierarchyComponent _cParentFolder;

        public enum FolderType
        {
            Normal = 1,
            Executable = 2,
            TTApp = 3,
            Script = 4,
            Other = 5
        }

        //Working variables



#region "Properties"

        internal FolderType Type
        {
            get { return _folderType; }
            set { _folderType = value; }
        }

        internal List<mod_HierarchyComponent> LstChildrensComponents
        {
            get { return _lstChildrensComponents; }
            set { _lstChildrensComponents = value; }
        }

        internal mod_HierarchyComponent ParentFolder
        {
            get { return _cParentFolder; }
            set { _cParentFolder = value; }
        }

#endregion



    }
}