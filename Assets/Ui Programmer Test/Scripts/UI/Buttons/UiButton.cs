using UnityEngine;
using UnityEngine.UI;

namespace UiProgrammerTest.Scripts.UI.Buttons
{
    /// <summary>
    /// Кнопка с присвоенным полем типа Button
    /// </summary>
    [RequireComponent(typeof(Button))]
    internal abstract class UiButton : MonoBehaviour
    {
        protected Button _button;

        protected virtual void Awake()
        {
            _button = GetComponent<Button>();
        }
    }
}
