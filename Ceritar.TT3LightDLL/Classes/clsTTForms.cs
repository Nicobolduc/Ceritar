using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceritar.TT3LightDLL.Classes
{
    public sealed class clsTTForms
    {
        //Private class members
        private static clsTTForms _myUniqueInstance = null;
        private int intGenericList_Count = 0;
        private System.Drawing.Point mcGenListLocation = new System.Drawing.Point(0, 0);
        

#region "Constructors"

        private clsTTForms()
        {

        }

#endregion


#region "Properties"

        public static clsTTForms GetTTForms
        {
            get
            {
                if (_myUniqueInstance == null)
                {
                    _myUniqueInstance = new clsTTForms();
                }

                return _myUniqueInstance;
            }
        }

        public int GenericList_Count
        {
            get { return intGenericList_Count; }
            set { intGenericList_Count = value; }
        }

#endregion


        private bool bln_ResetGenericListCascade()
        {
            if (intGenericList_Count == 5 || intGenericList_Count == 0)
            {
                intGenericList_Count = 1;
                
                return true;
            }

            intGenericList_Count++;

            return false;
        }

        public System.Drawing.Point GetGenericListLocation()
        {
            if (bln_ResetGenericListCascade())
            {
                mcGenListLocation = new System.Drawing.Point(0, 0);
            }
            else
            {
                mcGenListLocation = new System.Drawing.Point(mcGenListLocation.X + 40, mcGenListLocation.Y + 40);
            }

            return mcGenListLocation;
        }
    }
}
