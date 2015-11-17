
namespace Ceritar.CVS.Models.Module_Template
{
    internal class mod_HierarchyTemplate  
    {
        //Model attributes
        private string _strTemplateName;
        private TemplateType _templateType;
        private bool _byDefault;
        private mod_HierarchyComponent _cRacine;

        public enum TemplateType
        {
            Version = 1,
            Revision = 2
        }

        //Working variables

#region "Properties"

        internal string TemplateName
        {
            get { return _strTemplateName; }
            set { _strTemplateName = value; }
        }

        internal TemplateType TemplatType
        {
            get { return _templateType; }
            set { _templateType = value; }
        }

        internal bool IsByDefault
        {
            get { return _byDefault; }
            set { _byDefault = value; }
        }

        internal mod_HierarchyComponent Racine
        {
            get { return _cRacine; }
            set { _cRacine = value; }
        }

#endregion

    }
}
