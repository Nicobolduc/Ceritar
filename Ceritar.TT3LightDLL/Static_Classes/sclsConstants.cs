﻿
namespace Ceritar.TT3LightDLL.Static_Classes
{
    /// <summary>
    /// Cette classe statique contient des constantes génériques communes à toute l'application.
    /// </summary>
    public static class sclsConstants
    {
        public enum Language {
            FRENCH_QC = 1,
            ENGLISH_CA = 2
        }

        //Data manipulation language
        public enum DML_Mode
        {
            NO_MODE = 0,
            CONSULT_MODE = 1,
            INSERT_MODE = 2,
            UPDATE_MODE = 3,
            DELETE_MODE = 4
        }

        public enum Error_Message
        {
            ERROR_UNHANDLED = 11,
            ERROR_SAVE_MSG = 12
        }

        public enum Validation_Message
        {
            MANDATORY_VALUE = 3,
            NUMERIC_VALUE = 4,
            UNIQUE_ATTRIBUTE = 5,
            MANDATORY_GRID = 6,
            INVALID_REFERENCE_INTEGRITY = 7,
            INVALID_TIMESTAMP = 10,
            INVALID_PATH = 17,
            INVALID_ACTION_IF_CHANGE_MADE = 23,
            PATH_ACCESS_DENIED = 57,
            MAXIMUM_NUMBER_CHARACTER = 59
    }
    }
}
