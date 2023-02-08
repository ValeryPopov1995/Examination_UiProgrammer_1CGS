namespace UiProgrammerTest.Scripts.UI.Views
{
    /// <summary>
    /// Дочерние компоненты в составе UiView, выполняют действия, вызываемые при открытии/закрытии окна
    /// </summary>
    public interface IViewElement
    {
        void OnViewShow();

        void OnViewHide();
    }
}