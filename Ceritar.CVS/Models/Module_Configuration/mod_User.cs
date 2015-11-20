﻿
namespace Ceritar.CVS.Models.Module_Configuration
{
    internal class mod_User
    {
        //Model attributes
        private string _strUserCode;
        private string _strPassword;
        private string _strFirstname;
        private string _strLastname;
        private string _strEmail;
        private short _intLanguage = 1;
        private mod_UerGroup _cUserGroup;

        //Working variables


#region "Properties"

        internal short GetLanguage
        {
            get { return _intLanguage; }
        }

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

        internal short Language
        {
            get { return _intLanguage; }
            set { _intLanguage = value; }
        }

#endregion


    }
}