using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ceritar.TT3LightDLL.Static_Classes;

namespace Ceritar.CVS.Controllers.Interfaces
{
    /// <summary>
    /// Cette interface est utilisée par le controleur ctr_Template pour accéder aux informations
    /// d'une vue l'implémentant.
    /// </summary>
    public interface ITemplate
    {
        string GetTemplateName();
        int GetCeritarApplication_NRI();
        int GetTemplateType_NRI();
        int GetTemplate_NRI();
        ushort GetTemplate_TS();
        bool GetByDefaultValue();
        structHierarchyComponent GetRacineSystem();
        sclsConstants.DML_Mode GetDML_Action();
        List<structHierarchyComponent> GetHierarchyComponentList();
    }

    /// <summary>
    /// Cette structure représente les informations potentiellement contenues dans un niveau de hiérarchie.
    /// </summary>
    public struct structHierarchyComponent
    {
        public sclsConstants.DML_Mode Action;
        public int intHierarchyComponent_NRI;
        public int intHierarchyComponent_TS;
        public string strName;
        public string strLocation;
        public int intScriptSequenceNo;
        public int Parent_NRI;
        public ushort intNodeLevel;
        public ctr_Template.FolderType FolderType;
    }
}
