using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ceritar.TT3LightDLL.Static_Classes;

namespace Ceritar.CVS.Controllers.Interfaces
{
    public interface ITemplate
    {
        string GetTemplateName();
        int GetCeritarApplication_NRI();
        int GetTemplateType_NRI();
        int GetTemplate_NRI();
        ushort GetTemplate_TS();
        bool GetByDefaultValue();
        sclsConstants.DML_Mode GetDML_Action();
        List<structHierarchyComponent> GetHierarchyComponentList();
    }

    public struct structHierarchyComponent
    {
        public int intHierarchyComponent_NRI;
        public int intHierarchyComponent_TS;
        public bool blnIsFolder;
        public string strName;
        public string strLocation;
        public int intScriptSequenceNo;
        public int Parent_NRI;
        public int intNodeLevel;
    }
}
