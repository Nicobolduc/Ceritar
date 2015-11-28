using Ceritar.TT3LightDLL.Static_Classes;

namespace Ceritar.TT3LightDLL.Controls
{
    public interface IFormController
    {
        void ShowForm(sclsConstants.DML_Mode vintFormMode, int rintItem_ID = 0, bool vblnIsModal = false);
    }
}
