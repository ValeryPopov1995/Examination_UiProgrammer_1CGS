using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace UiProgrammerTest.Scripts.UI.Views
{
    /// <summary>
    /// UI окно, отображающее кнопки и показатели, необходимые в данном состоянии игры. Открыто может быть только одно окно типа Window и одно типа Popup
    /// </summary>
    public class View : MonoBehaviour
    {
        #region DECLARATIONS

        /// <summary>
        /// Тип окна. Открыто может быть только одно окно типа Window и одно типа Popup
        /// </summary>
        public enum ViewType { Window, Popup }

        /// <summary>
        /// Вызывается при вызове отображения окна. true если нужно скрывать  остальные окна
        /// </summary>
        private static event Action<View, bool> _onShowView;

        [field: SerializeField] public ViewType _type { get; private set; } = ViewType.Window;
        [SerializeField] private bool _hideOnStart = true;
        private IViewElement[] _iViewElements;
        private bool _isShown = true;
        
        public UnityEvent<View> OnShowAnimation, OnHideAnimation;
        [SerializeField] private UnityEvent<View> OnShowFx, OnHideFx;

        #endregion



        private void Awake()
        {
            GetComponent<RectTransform>().localPosition = Vector3.zero;
            _iViewElements = GetComponentsInChildren<IViewElement>();

            _onShowView += Hide;
        }

        private void Start()
        {
            if (_hideOnStart) HideOnStart();
            else Show();
        }

        private void HideOnStart()
        {
            _isShown = false;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _onShowView -= Hide;
        }


        /// <summary>
        /// Метод выключения объекта, для ключа анимации
        /// </summary>
        public void DisableGameObject() => gameObject.SetActive(false);


        [Button("Show Only", enabledMode: EButtonEnableMode.Playmode)]
        /// <summary>
        /// Отобразить окно, скрыв остальные
        /// </summary>
        public void Show() => Show(true);

        /// <summary>
        /// Отобразить окно
        /// </summary>
        /// <param name="hideOthers">Скрыть остальные окна этого типа</param>
        public void Show(bool hideOthers = true)
        {
            if (_isShown) return;

            _isShown = true;
            for (int i = 0; i < _iViewElements.Length; i++) _iViewElements[i].OnViewShow();

            OnShowAnimation?.Invoke(this);
            OnShowFx?.Invoke(this);
            _onShowView?.Invoke(this, hideOthers);
        }

        [Button(enabledMode: EButtonEnableMode.Playmode)]
        /// <summary>
        /// Скрыть окно
        /// </summary>
        public void Hide()
        {
            if (!_isShown) return;

            _isShown = false;
            for (int i = 0; i < _iViewElements.Length; i++) _iViewElements[i].OnViewHide();

            OnHideAnimation?.Invoke(this);
            OnHideFx?.Invoke(this);
        }

        /// <summary>
        /// Скрыть окно при открытии другого окна (по событию)
        /// </summary>
        /// <param name="openedView">Открываемое окно</param>
        /// <param name="hideOthers">Скрывать ли это окно при открытии другого</param>
        private void Hide(View openedView, bool hideOthers)
        {
            if (this != openedView && _type == openedView._type && hideOthers) Hide();
        }
    }
}