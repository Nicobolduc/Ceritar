
namespace Ceritar.CVS.Models.Module_ActivesInstallations
{
    /// <summary>
    /// Cette classe représente le modèle objet d'une usager de l'application.
    /// TTU représente le préfixe des colonnes de la table "TTUser" correspondant au modèle dans la base de données.
    /// </summary>
    internal class mod_TTU_User
    {
        //Model attributes
        private string _strUserCode;
        private string _strPassword;
        private string _strFirstname;
        private string _strLastname;
        private string _strEmail;
        private mod_TTG_UerGroup _cUserGroup;

        //Working variables


#region "Properties"

        internal string UserCode
        {
            get { return _strUserCode; }
            set { _strUserCode = value; }
        }

        internal string Password
        {
            get { return _strPassword; }
            set { _strPassword = value; }
        }

        internal string Firstname
        {
            get { return _strFirstname; }
            set { _strFirstname = value; }
        }

        internal string Lastname
        {
            get { return _strLastname; }
            set { _strLastname = value; }
        }

        internal string Email
        {
            get { return _strEmail; }
            set { _strEmail = value; }
        }

#endregion


    }
}
