using UnityEngine;

namespace UiProgrammerTest.Scripts.UI.Views
{
    /// <summary>
    /// Хранит последние закрытые окна для метод возврата на предыдущее окно
    /// </summary>
    public class CoreView : MonoBehaviour
    {
        public View LastHidenView { get; private set; }
        public View LastHidenViewWindow { get; private set; }
        public View LastHidenViewPopup { get; private set; }

        private void Awake()
        {
            var views = GetComponentsInChildren<View>();
            for (int i = 0; i < views.Length; i++)
                views[i].OnHideAnimation.AddListener(UpdateLastHidenViews);
        }

        private void OnDestroy()
        {
            var views = GetComponentsInChildren<View>();
            for (int i = 0; i < views.Length; i++)
                views[i].OnHideAnimation.RemoveListener(UpdateLastHidenViews);
        }

        private void UpdateLastHidenViews(View view)
        {
            LastHidenView = view;

            if (view._type == View.ViewType.Window)
                LastHidenViewWindow = view;
            else
                LastHidenViewPopup = view;
        }

        public void ShowPreviousWindow()
        {
            LastHidenViewWindow.Show();
        }
    }
}