using Ceritar.TT3LightDLL.Static_Classes;

namespace Ceritar.TT3LightDLL.Controls
{
    /// <summary>
    /// Cette interface est implémentées par toutes les Forms qui possèdent le contrôle "ctlFormController" afin de permettre aux classes externes d'avoir accès au contrôle dans un contexte où 
    /// l'on ne sait pas quel sera le type de la Form à utiliser.
    /// </summary>
    public interface IFormController
    {
        ctlFormController GetFormController();
    }
}
