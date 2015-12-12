
namespace Ceritar.TT3LightDLL.Static_Classes
{
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
            INVALID_TIMESTAMP = 10
        }
    }
}
