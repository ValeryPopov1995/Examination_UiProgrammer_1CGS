using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace UiProgrammerTest.Scripts.UI.Views
{
    /// <summary>
    /// UI ����, ������������ ������ � ����������, ����������� � ������ ��������� ����. ������� ����� ���� ������ ���� ���� ���� Window � ���� ���� Popup
    /// </summary>
    public class View : MonoBehaviour
    {
        #region DECLARATIONS

        /// <summary>
        /// ��� ����. ������� ����� ���� ������ ���� ���� ���� Window � ���� ���� Popup
        /// </summary>
        public enum ViewType { Window, Popup }

        /// <summary>
        /// ���������� ��� ������ ����������� ����. true ���� ����� ��������  ��������� ����
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
        /// ����� ���������� �������, ��� ����� ��������
        /// </summary>
        public void DisableGameObject() => gameObject.SetActive(false);


        [Button("Show Only", enabledMode: EButtonEnableMode.Playmode)]
        /// <summary>
        /// ���������� ����, ����� ���������
        /// </summary>
        public void Show() => Show(true);

        /// <summary>
        /// ���������� ����
        /// </summary>
        /// <param name="hideOthers">������ ��������� ���� ����� ����</param>
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
        /// ������ ����
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
        /// ������ ���� ��� �������� ������� ���� (�� �������)
        /// </summary>
        /// <param name="openedView">����������� ����</param>
        /// <param name="hideOthers">�������� �� ��� ���� ��� �������� �������</param>
        private void Hide(View openedView, bool hideOthers)
        {
            if (this != openedView && _type == openedView._type && hideOthers) Hide();
        }
    }
}