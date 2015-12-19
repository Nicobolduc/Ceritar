using Ceritar.CVS.Models.Module_Template;
using Ceritar.CVS.Models.Module_ActivesInstallations;
using System.Collections.Generic;

namespace Ceritar.CVS.Models.Module_ActivesInstallations
{
    /// <summary>
    /// Cette classe représente le modèle objet d'une révision de version.
    /// Rev représente le préfixe des colonnes de la table "Revision" correspondant au modèle dans la base de données.
    /// </summary>
    internal class mod_Rev_Revision
    {
        //Model attributes
        private mod_Tpl_HierarchyTemplate _cTemplateSource;
        private mod_Ver_Version _cVersion;
        private mod_TTU_User _cCreatedByUser;
        private List<string> _lstModifications;
        private string _strLocation_Release;
        private string _strLocation_Scripts;
        private string _strCreationDate;
        private ushort _intRevisionNo;

        //Working variables


#region "Properties"

        internal mod_Tpl_HierarchyTemplate TemplateSource
        {
            get { return _cTemplateSource; }
            set { _cTemplateSource = value; }
        }

        internal mod_Ver_Version Version
        {
            get { return _cVersion; }
            set { _cVersion = value; }
        }

        internal mod_TTU_User CreatedByUser
        {
            get { return _cCreatedByUser; }
            set { _cCreatedByUser = value; }
        }

        internal List<string> LstModifications
        {
            get { return _lstModifications; }
            set { _lstModifications = value; }
        }

        internal string Path_Release
        {
            get { return _strLocation_Release; }
            set { _strLocation_Release = value; }
        }

        internal string Path_Scripts
        {
            get { return _strLocation_Scripts; }
            set { _strLocation_Scripts = value; }
        }

        internal string CreationDate
        {
            get { return _strCreationDate; }
            set { _strCreationDate = value; }
        }

        internal ushort RevisionNo
        {
            get { return _intRevisionNo; }
            set { _intRevisionNo = value; }
        }

#endregion


    }
}
